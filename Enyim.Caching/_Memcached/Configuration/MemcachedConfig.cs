using System;
using System.Collections.Generic;
using System.Linq;
using Enyim.Caching._Configuration;
using Enyim.Caching.Memcached;
using Enyim.Caching.Memcached.Protocol.Binary;
using Enyim.Caching.Memcached.Protocol.Text;

namespace Enyim.Caching._Memcached.Configuration {

    public class MemcachedConfig : IMemcachedClientConfig {

        private MemcachedConfiguration config;

        public MemcachedConfig(MemcachedConfiguration config) {
            this.config = config;
            new MemcachedConfigValidate(this, config).Validate();
        }

        /// <summary>
        /// 虚拟节点基数
        /// </summary>
        public int VirtualNodeRadix {
            get;
            protected set;
        }

        /// <summary>
        /// 节点宕机后重试时间间隔
        /// </summary>
        public int DeadTimeout {
            get;
            protected set;
        }

        public MemcachedNodeConfig[] ServerNodes { 
            get; 
            protected set; 
        }

        public IServerPool ServerPool {
            get;
            protected set;
        }

        private class MemcachedConfigValidate {

            private MemcachedConfig config;
            private MemcachedConfiguration sourceConfig;

            public MemcachedConfigValidate(MemcachedConfig config, MemcachedConfiguration sourceConfig) {
                this.config = config;
                this.sourceConfig = sourceConfig;
            }

            public void Validate() {
                this.config.ServerNodes = this.ServerNodes;
                this.config.VirtualNodeRadix = this.VirtualNodeRadix;
            }

            public int VirtualNodeRadix {
                get { return this.sourceConfig.VirtualNodeRadix; }
            }

            private MemcachedNodeConfig[] ServerNodes {
                get {
                    if (this.sourceConfig.ServerList == null || this.sourceConfig.ServerList.Count == 0) {
                        throw new ArgumentException("ServerList's count must be larger > 0");
                    }
                    IList<MemcachedNodeConfig> nodeConfigList = new List<MemcachedNodeConfig>();
                    foreach (MemcachedNodeConfiguration sourceNodeConfig in this.sourceConfig.ServerList) {
                        nodeConfigList.Add(new MemcachedNodeConfig(sourceNodeConfig));
                    }
                    return nodeConfigList.ToArray();
                }
            }

            /// <summary>
            /// IServerPool
            /// </summary>
            public IServerPool ServerPool {
                get { return new Enyim.Caching._Memcached.ServerPool.MemcachedServerPool(this.OperationFactory); }
            }

            /// <summary>
            /// IServerPool
            /// </summary>
            public IOperationFactory OperationFactory {
                get {
                    switch (this.sourceConfig.Protocol.ToUpper()) {
                        case "TEXT":
                            return new TextOperationFactory();
                        case "BINARY":
                            return new BinaryOperationFactory();
                    }
                    throw new ArgumentOutOfRangeException("Unknown protocol: " + this.sourceConfig.Protocol);
                }
            }
        }


        public IMemcachedKeyTransformer KeyTransformer {
            get { throw new NotImplementedException(); }
        }

        public IMemcachedNodeLocator NodeLocator {
            get { throw new NotImplementedException(); }
        }

        public ITranscoder Transcoder {
            get { throw new NotImplementedException(); }
        }

        public IPerformanceMonitor PerformanceMonitor {
            get { throw new NotImplementedException(); }
        }
    }

}
