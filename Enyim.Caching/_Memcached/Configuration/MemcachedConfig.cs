using System;
using System.Linq;
using System.Collections.Generic;
using Enyim.Caching._Configuration;
using System.Net;
using Enyim.Caching.Memcached;
using Enyim.Caching._Memcached._ServerPool;

namespace Enyim.Caching._Memcached._Configuration {

    public class MemcachedConfig {

        private MemcachedConfiguration config;

        public MemcachedConfig(MemcachedConfiguration config) {
            this.config = config;
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

            public IServerPool ServerPool {
                get {
                    switch (this.sourceConfig.Protocol.ToUpper()) {
                        case "TEXT":
                            return new TextServerPool(config);
                        case "BINARY":
                            return new BinaryServerPool(config);
                    }
                    throw new ArgumentOutOfRangeException("Unknown protocol: " + this.sourceConfig.Protocol);
                }
            }
        }
    }

}
