using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Enyim.Caching._Memcached.Configuration {
    
    
    public class MemcachedNodeConfig {

        
        public MemcachedNodeConfig(MemcachedNodeConfiguration nodeConfig) {
            new MemcachedConfigValidate(this,nodeConfig).Validate();
        }

        public IPEndPoint ServerAddress {
            get;
            protected set;
        }

        public IPEndPoint[] BackupServerAddress {
            get;
            protected set;
        }

        public int MinPoolSize {
            get;
            protected set;
        }

        public int MaxPoolSize {
            get;
            protected set;
        }

        public TimeSpan ConnectionTimeout {
            get;
            protected set;
        }

        public TimeSpan ReceiveTimeout {
            get;
            protected set;
        }

        public TimeSpan DeadTimeout {
            get;
            protected set;
        }

        public TimeSpan QueueTimeOut {
            get;
            protected set;
        }


        private class MemcachedConfigValidate {

            private MemcachedNodeConfig nodeConfig;
            private MemcachedNodeConfiguration nodeSourceConfig;

            public MemcachedConfigValidate(MemcachedNodeConfig nodeConfig, MemcachedNodeConfiguration nodeSourceConfig) {
                this.nodeConfig = nodeConfig;
                this.nodeSourceConfig = nodeSourceConfig;
            }

            public void Validate() {
                this.nodeConfig.ServerAddress = this.ServerAddress;
                this.nodeConfig.BackupServerAddress = this.BackupServerAddress;

                if (this.MinPoolSize >= this.MaxPoolSize) {
                    throw new InvalidOperationException("MaxPoolSize must be rather than MinPoolSize", null);
                }
                this.nodeConfig.MinPoolSize = this.MinPoolSize;
                this.nodeConfig.MaxPoolSize = this.MaxPoolSize;

                this.nodeConfig.ConnectionTimeout = this.ConnectionTimeout;
                this.nodeConfig.ReceiveTimeout = this.ReceiveTimeout;
                this.nodeConfig.DeadTimeout = this.DeadTimeout;
                this.nodeConfig.QueueTimeOut = this.QueueTimeOut;
            }

            private IPEndPoint ServerAddress {
                get { return ResolveToEndPoint(this.nodeSourceConfig.ServerAddress); }
            }

            private IPEndPoint[] BackupServerAddress {
                get {
                    if (string.IsNullOrWhiteSpace(this.nodeSourceConfig.BackupServerAddress)) {
                        return null;
                    }
                    string[] addresses = this.nodeSourceConfig.BackupServerAddress.Split(';');
                    IList<IPEndPoint> addressList = new List<IPEndPoint>();
                    foreach (string address in addresses) {
                        addressList.Add(ResolveToEndPoint(address));
                    }
                    return addressList.ToArray<IPEndPoint>();
                }
            }

            private int MinPoolSize {
                get {
                    if (this.nodeSourceConfig.MinPoolSize < 0) {
                        throw new InvalidOperationException("MinPoolSize must be larger >= 0", null);
                    }
                    return this.nodeConfig.MinPoolSize; 
                }
            }

            private int MaxPoolSize {
                get {
                    if (this.nodeSourceConfig.MaxPoolSize < 1) {
                        throw new InvalidOperationException("MaxPoolSize must be larger >= 1", null);
                    } 
                    return this.nodeConfig.MaxPoolSize;
                }
            }

            private TimeSpan ConnectionTimeout {
                get {
                    if (this.nodeSourceConfig.ConnectionTimeout < 0) {
                        throw new InvalidOperationException("ConnectionTimeout must be larger >= 0", null);
                    }
                    return new TimeSpan(0, 0, 0, 0, this.nodeSourceConfig.ConnectionTimeout);
                }
            }

            private TimeSpan ReceiveTimeout {
                get {
                    if (this.nodeSourceConfig.ReceiveTimeout < 0) {
                        throw new InvalidOperationException("ReceiveTimeout must be larger >= 0", null);
                    }
                    return new TimeSpan(0, 0, 0, 0, this.nodeSourceConfig.ReceiveTimeout);
                }
            }

            private TimeSpan DeadTimeout {
                get {
                    if (this.nodeSourceConfig.DeadTimeout < 0) {
                        throw new InvalidOperationException("DeadTimeout must be larger >= 0", null);
                    }
                    return new TimeSpan(0, 0, 0, 0, this.nodeSourceConfig.DeadTimeout);
                }
            }

            private TimeSpan QueueTimeOut {
                get {
                    if (this.nodeSourceConfig.QueueTimeOut < 0) {
                        throw new InvalidOperationException("QueueTimeOut must be larger >= 0", null);
                    }
                    return new TimeSpan(0, 0, 0, 0, this.nodeSourceConfig.QueueTimeOut);
                }
            }



            private IPEndPoint ResolveToEndPoint(string value) {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentNullException("value");

                var parts = value.Split(',');
                if (parts.Length != 2)
                    throw new ArgumentException("host,port is expected", "value");

                int port;
                if (!Int32.TryParse(parts[1], out port))
                    throw new ArgumentException("Cannot parse port: " + parts[1], "value");

                return ResolveToEndPoint(parts[0], port);
            }

            private IPEndPoint ResolveToEndPoint(string host, int port) {
                if (String.IsNullOrEmpty(host)) {
                    throw new ArgumentNullException("host");
                }
                IPAddress address;

                // 尝试解析地址
                if (!IPAddress.TryParse(host, out address)) {
                    // 如果不是一个IP 尝试从DNS解析
                    // TODO we need to find a way to specify whihc ip should be used when the host has several
                    var entry = System.Net.Dns.GetHostEntry(host);
                    address = entry.AddressList.FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);

                    if (address == null) {
                        throw new ArgumentException(String.Format("Could not resolve host '{0}'.", host));
                    }
                }

                return new IPEndPoint(address, port);
            }
        }
    }
}
