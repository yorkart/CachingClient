//-----------------------------------------------------------------------------------------
//   <copyright company="同程网" file="ServerNodeConnectionPool.cs">
//      所属项目：Enyim.Caching._MemcachedNode
//      创 建 人：王跃
//      创建日期：2012-6-4 12:46:58
//      用    途：请一定在此描述用途
//
//      更新记录:
//
//   </copyright> 
//-----------------------------------------------------------------------------------------

namespace Enyim.Caching._MemcachedNode {
    using System;
    using System.Data;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using Enyim.Caching.Memcached;
    using Enyim.Collections;
    using System.Net;

    /// <summary>
    /// ServerNodeConnectionPool 概要
    /// </summary>
    internal class ServerNodeConnectionPool : IDisposable {
        private static readonly Enyim.Caching.ILog log = Enyim.Caching.LogManager.GetLogger(typeof(ServerNodeConnectionPool).FullName.Replace("+", "."));

        private MemcachedNode ownerNode;
        private InterlockedStack<PooledSocket> freeItems;
        private Semaphore semaphore;

        private bool isAlive;
        private bool isDisposed;

        public ServerNodeConnectionPool(MemcachedNode memcachedNode) {
            this.ownerNode = memcachedNode;
            this.semaphore = new Semaphore(this.MinItems, this.MaxItems);
            this.freeItems = new InterlockedStack<PooledSocket>();
            this.isAlive = true;// 默认为true，因为创建socket时如果失败，此标记将被回调方法标识为false
        }
        public bool IsAlive {
            get { return this.isAlive; }
        }
        internal int MinItems {
            get { return this.ownerNode.NodeAdapter.MinPoolSize; }
        }
        internal int MaxItems {
            get { return this.ownerNode.NodeAdapter.MaxPoolSize; }
        }
        internal EndPoint ServerAddress {
            get { return this.ownerNode.NodeAdapter.ServerAddress; }
        }

        private PooledSocket CreateSocket() {
            var ps = this.ownerNode.CreateSocket();
            ps.CleanupCallback = this.ReleaseSocket;

            return ps;
        }

        /// <summary>
        /// 释放一个socket到连接池中
        /// Releases an item back into the pool
        /// </summary>
        /// <param name="socket"></param>
        private void ReleaseSocket(PooledSocket socket) {
            if (log.IsDebugEnabled) {
                log.Debug("Releasing socket " + socket.InstanceId);
                log.Debug("Are we alive? " + this.isAlive);
            }

            if (this.isAlive) {
                // is it still working (i.e. the server is still connected)
                if (socket.IsAlive) {
                    // mark the item as free
                    this.freeItems.Push(socket);

                    // signal the event so if someone is waiting for it can reuse this item
                    this.semaphore.Release();
                } else {
                    // kill this item
                    socket.Destroy();

                    // mark ourselves as not working for a while
                    this.MarkAsDead();

                    // make sure to signal the Acquire so it can create a new conenction
                    // if the failure policy keeps the pool alive
                    this.semaphore.Release();
                }
            } else {
                // one of our previous sockets has died, so probably all of them 
                // are dead. so, kill the socket (this will eventually clear the pool as well)
                socket.Destroy();
            }
        }

        private void MarkAsDead() {
            if (log.IsDebugEnabled) {
                log.DebugFormat("Mark as dead was requested for {0}", this.ServerAddress);
            }
            var shouldFail = ownerNode.FailurePolicy.ShouldFail();

            //if (log.IsDebugEnabled) log.Debug("FailurePolicy.ShouldFail(): " + shouldFail);
            this.isAlive = false;
            this.ownerNode.Failed_Event();
        }

        internal void InitPool() {
            try {
                for (int i = 0; i < this.MinItems; i++) {
                    this.freeItems.Push(this.CreateSocket());

                    // cannot connect to the server
                    if (!this.isAlive)
                        break;
                }

                if (log.IsDebugEnabled)
                    log.DebugFormat("Pool has been inited for {0} with {1} sockets", this.ServerAddress, this.MinItems);

            } catch (Exception e) {
                log.Error("Could not init pool.", e);

                this.MarkAsDead();
            }
        }

        ~ServerNodeConnectionPool() {
            try { ((IDisposable)this).Dispose(); } catch { }
        }

        /// <summary>
        /// 释放所有资源实例
        /// Releases all resources allocated by this instance
        /// </summary>
        public void Dispose() {
            // this is not a graceful shutdown
            // if someone uses a pooled item then 99% that an exception will be thrown
            // somewhere. But since the dispose is mostly used when everyone else is finished
            // this should not kill any kittens
            if (!this.isDisposed) {
                this.isAlive = false;
                this.isDisposed = true;

                PooledSocket ps;

                while (this.freeItems.TryPop(out ps)) {
                    try { ps.Destroy(); } catch { }
                }

                this.ownerNode = null;
                this.semaphore.Close();
                this.semaphore = null;
                this.freeItems = null;
            }
        }

        void IDisposable.Dispose() {
            this.Dispose();
        }
    }
}