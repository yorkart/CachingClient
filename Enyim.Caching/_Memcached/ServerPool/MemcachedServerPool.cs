//-----------------------------------------------------------------------------------------
//   <copyright company="同程网" file="TextServerPool.cs">
//      所属项目：Enyim.Caching._Memcached._ServerPool
//      创 建 人：王跃
//      创建日期：2012-6-5 20:39:50
//      用    途：请一定在此描述用途
//
//      更新记录:
//
//   </copyright> 
//-----------------------------------------------------------------------------------------

namespace Enyim.Caching._Memcached.ServerPool {
    using System;
    using System.Data;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using Enyim.Caching._Configuration;
    using Enyim.Caching._Memcached.Configuration;
    using System.Net;
    using System.Threading;
    using Enyim.Caching.Memcached;

    /// <summary>
    /// TextServerPool 概要
    /// </summary>
    public class MemcachedServerPool : IServerPool, IDisposable {
        private static readonly Enyim.Caching.ILog log = Enyim.Caching.LogManager.GetLogger(typeof(MemcachedServerPool));

        // 配置文件
        private MemcachedConfig configuration;
        private IOperationFactory opFactory;
        private IMemcachedNodeLocator nodeLocator;

        private IMemcachedNode[] allNodes;

        private object DeadSync = new Object();

        // 死链重试定时器
        private System.Threading.Timer resurrectTimer;
        // 设置当前定时器是否处于活动状态
        private bool isTimerActive;
        // 死链重试等待时间
        private long deadTimeoutMsec;
        private bool isDisposed;
        private event Action<IMemcachedNode> nodeFailed;

        public MemcachedServerPool(IOperationFactory opFactory) {
            if (configuration == null) {
                throw new ArgumentNullException("NoConfig");
            }
            this.configuration = ConfigurationProvider.Instance.CurConfig;
            this.opFactory = opFactory;
            //this.deadTimeoutMsec = (long)this.configuration.SocketPool.DeadTimeout.TotalMilliseconds;
        }


        ~MemcachedServerPool() {
            try { ((IDisposable)this).Dispose(); } catch { }
        }

        protected virtual IMemcachedNode CreateNode(MemcachedNodeConfig config) {
            return new Enyim.Caching._Memcached.MemcachedNode.MemcachedNode(config);
        }

        /// <summary>
        /// 节点失败时 定时器回调函数
        /// </summary>
        /// <param name="state"></param>
        private void rezCallback(object state) {
            var isDebug = log.IsDebugEnabled;

            if (isDebug) log.Debug("Checking the dead servers.");

            // how this works:
            // 1. timer is created but suspended
            // 2. Locate encounters a dead server, so it starts the timer which will trigger after deadTimeout has elapsed
            // 3. if another server goes down before the timer is triggered, nothing happens in Locate (isRunning == true).
            //		however that server will be inspected sooner than Dead Timeout.
            //		   S1 died   S2 died    dead timeout
            //		|----*--------*------------*-
            //           |                     |
            //          timer start           both servers are checked here
            // 4. we iterate all the servers and record it in another list
            // 5. if we found a dead server whihc responds to Ping(), the locator will be reinitialized
            // 6. if at least one server is still down (Ping() == false), we restart the timer
            // 7. if all servers are up, we set isRunning to false, so the timer is suspended
            // 8. GOTO 2

            lock (this.DeadSync) {
                if (this.isDisposed) {
                    if (log.IsWarnEnabled) log.Warn("IsAlive timer was triggered but the pool is already disposed. Ignoring.");

                    return;
                }

                var nodes = this.allNodes;
                var aliveList = new List<IMemcachedNode>(nodes.Length);
                var changed = false;
                var deadCount = 0;

                // 遍历节点 重新整理出活动的节点
                for (var i = 0; i < nodes.Length; i++) {
                    var n = nodes[i];
                    if (n.IsAlive) {
                        if (isDebug) log.DebugFormat("Alive: {0}", n.EndPoint);

                        aliveList.Add(n);
                    } else {
                        if (isDebug) log.DebugFormat("Dead: {0}", n.EndPoint);

                        if (n.Ping()) { // 对宕机节点进行ping
                            changed = true;
                            aliveList.Add(n);

                            if (isDebug) log.Debug("Ping ok.");
                        } else {
                            if (isDebug) log.Debug("Still dead.");

                            deadCount++;
                        }
                    }
                }

                // reinit the locator
                // 如果节点有改变 则初始化节点定位器中的节点集合
                if (changed) {
                    if (isDebug) log.Debug("Reinitializing the locator.");

                    this.nodeLocator.Initialize(aliveList);
                }

                // stop or restart the timer
                if (deadCount == 0) { // 如果没有宕机节点 则设置定时器为非活动状态
                    if (isDebug) log.Debug("deadCount == 0, stopping the timer.");

                    this.isTimerActive = false;
                } else { // 如果有宕机 ， 则再次重试
                    if (isDebug) log.DebugFormat("deadCount == {0}, starting the timer.", deadCount);

                    this.resurrectTimer.Change(this.deadTimeoutMsec, Timeout.Infinite);
                }
            }
        }

        private void NodeFail(IMemcachedNode node) {
            var isDebug = log.IsDebugEnabled;
            if (isDebug) log.DebugFormat("Node {0} is dead.", node.EndPoint);

            // the timer is stopped until we encounter the first dead server
            // when we have one, we trigger it and it will run after DeadTimeout has elapsed
            lock (this.DeadSync) {
                if (this.isDisposed) {
                    if (log.IsWarnEnabled) {
                        log.Warn("Got a node fail but the pool is already disposed. Ignoring.");
                    }
                    return;
                }

                // bubble up the fail event to the client
                var fail = this.nodeFailed;
                if (fail != null) { // 执行失败事件
                    fail(node);
                }
                // re-initialize the locator
                var newLocator = this.configuration.NodeLocator;
                newLocator.Initialize(allNodes.Where(n => n.IsAlive).ToArray());
                Interlocked.Exchange(ref this.nodeLocator, newLocator);

                // the timer is stopped until we encounter the first dead server
                // when we have one, we trigger it and it will run after DeadTimeout has elapsed
                if (!this.isTimerActive) {
                    if (isDebug) log.Debug("Starting the recovery timer.");

                    if (this.resurrectTimer == null) {// 死链重试连接一次
                        this.resurrectTimer = new Timer(this.rezCallback, null, this.deadTimeoutMsec, Timeout.Infinite);
                    } else { // 如果定时器已经创建，则给定时器一个重试机会
                        this.resurrectTimer.Change(this.deadTimeoutMsec, Timeout.Infinite);
                    }
                    // 标识当期时间定时器已经处于活动状态 - 开启
                    this.isTimerActive = true;

                    if (isDebug) log.Debug("Timer started.");
                }
            }
        }

        #region [ IServerPool                  ]
        IMemcachedNode IServerPool.Locate(string key) {
            var node = this.nodeLocator.Locate(key);

            return node;
        }

        Enyim.Caching.Memcached.IOperationFactory IServerPool.OperationFactory {
            get { return this.opFactory; }
        }

        IEnumerable<IMemcachedNode> IServerPool.GetWorkingNodes() {
            return this.nodeLocator.GetWorkingNodes();
        }

        void IServerPool.Start() {
            this.allNodes = this.configuration.ServerNodes.
                                Select(nodeConfig => {
                                    IMemcachedNode node = this.CreateNode(nodeConfig);
                                    node.Failed += this.NodeFail;

                                    return node;
                                }).
                                ToArray();

            // initialize the locator
            var locator = this.configuration.NodeLocator;
            locator.Initialize(allNodes);

            this.nodeLocator = locator;
        }

        event Action<IMemcachedNode> IServerPool.NodeFailed {
            add { this.nodeFailed += value; }
            remove { this.nodeFailed -= value; }
        }

        #endregion
        #region [ IDisposable                  ]

        void IDisposable.Dispose() {
            GC.SuppressFinalize(this);

            lock (this.DeadSync) {
                if (this.isDisposed) return;

                this.isDisposed = true;

                // dispose the locator first, maybe it wants to access 
                // the nodes one last time
                var nd = this.nodeLocator as IDisposable;
                if (nd != null)
                    try { nd.Dispose(); } catch (Exception e) { if (log.IsErrorEnabled) log.Error(e); }

                this.nodeLocator = null;

                for (var i = 0; i < this.allNodes.Length; i++)
                    try { this.allNodes[i].Dispose(); } catch (Exception e) { if (log.IsErrorEnabled) log.Error(e); }

                // stop the timer
                if (this.resurrectTimer != null)
                    using (this.resurrectTimer)
                        this.resurrectTimer.Change(Timeout.Infinite, Timeout.Infinite);

                this.allNodes = null;
                this.resurrectTimer = null;
            }
        }

        #endregion

    }
}