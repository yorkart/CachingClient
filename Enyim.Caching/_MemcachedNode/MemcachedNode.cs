using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enyim.Caching._Configuration;
using System.Net;
using Enyim.Caching._FailurePolicy;
using System.Threading;
using Enyim.Caching.Memcached;

namespace Enyim.Caching._MemcachedNode {

    public class MemcachedNode : IMemcachedNode {
        private static readonly Enyim.Caching.ILog log = Enyim.Caching.LogManager.GetLogger(typeof(MemcachedNode));
        private static readonly object SyncRoot = new Object();

        private ServerNodeAdapter nodeAdapter; // 节点配置本地转换器
        private ServerNodeConnectionPool connectionPool; // 该节点连接池
        private INodeFailurePolicy failurePolicy = null;

        public MemcachedNode(ServerNodeConfiguration node) {
            this.nodeAdapter = new ServerNodeAdapter(node);
            this.connectionPool = new ServerNodeConnectionPool(this);
        }

        public ServerNodeAdapter NodeAdapter { 
            get { return this.nodeAdapter; } 
        }

        public event Action<IMemcachedNode> Failed;
        internal void Failed_Event() {
            this.Failed(this);
        }

        public bool IsAlive {
            get { return this.connectionPool.IsAlive; }
        }

        /// <summary>
        /// 失败策略
        /// </summary>
        internal INodeFailurePolicy FailurePolicy {
            get {
                return this.failurePolicy ?? (this.failurePolicy = new FailImmediatelyPolicyFactory().Create(this)); 
            }
        }

        protected internal virtual PooledSocket CreateSocket() {
            return new PooledSocket(
                this.nodeAdapter.ServerAddress, 
                this.nodeAdapter.ConnectionTimeout,
                this.nodeAdapter.ReceiveTimeout
            );
        }

        public bool Ping() {
            // is the server working?
            if (this.IsAlive)
                return true;

            // this codepath is (should be) called very rarely
            // if you get here hundreds of times then you have bigger issues
            // and try to make the memcached instaces more stable and/or increase the deadTimeout
            try {
                // we could connect to the server, let's recreate the socket pool
                lock (SyncRoot) {
                    //if (this.isDisposed) return false;

                    // try to connect to the server
                    using (var socket = this.CreateSocket()) ;

                    if (this.connectionPool.IsAlive)
                        return true;

                    // it's easier to create a new pool than reinitializing a dead one
                    // rewrite-then-dispose to avoid a race condition with Acquire (which does no locking)
                    var oldPool = this.connectionPool;
                    var newPool = new ServerNodeConnectionPool(this);

                    Interlocked.Exchange(ref this.connectionPool, newPool);

                    //try { oldPool.Dispose(); } catch { }
                }

                return true;
            }
                //could not reconnect
            catch { return false; }
        }

    }

}
