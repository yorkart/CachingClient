//-----------------------------------------------------------------------------------------
//   <copyright company="同程网" file="NodeAdapter.cs">
//      所属项目：Enyim.Caching._MemcachedNode
//      创 建 人：王跃
//      创建日期：2012-6-4 12:19:20
//      用    途：请一定在此描述用途
//
//      更新记录:
//
//   </copyright> 
//-----------------------------------------------------------------------------------------

namespace Enyim.Caching._MemcachedNode {
    using System;
    using System.Linq;
    using System.Net;
    using Enyim.Caching._Configuration;

    /// <summary>
    /// NodeAdapter 概要
    /// </summary>
    public class ServerNodeAdapter {

        private ServerNodeConfiguration node;

        public ServerNodeAdapter(ServerNodeConfiguration node) {
            this.node = node;
            if (this.node.BeginHashKey >= this.node.EndHashKey) {
                throw new InvalidOperationException("EndHashKey must be rather than BeginHashKey", null);
            }
            if (this.node.MinPoolSize >= this.node.MaxPoolSize) {
                throw new InvalidOperationException("MaxPoolSize must be rather than MinPoolSize", null);
            }
        }

        public IPEndPoint ServerAddress {
            get { return ResolveToEndPoint(this.node.ServerAddress); }
        }

        public int MinPoolSize {
            get { return this.node.MinPoolSize; }
        }

        public int MaxPoolSize {
            get { return this.node.MaxPoolSize; }
        }

        public int BeginHashKey {
            get { return this.node.BeginHashKey; }
        }

        public int EndHashKey {
            get { return this.node.EndHashKey; }
        }

        public TimeSpan ConnectionTimeout {
            get {
                if (this.node.ConnectionTimeout < 0) {
                    throw new InvalidOperationException("ConnectionTimeout must be larger >= 0", null);
                }
                return new TimeSpan(0, 0, 0, 0, this.node.ConnectionTimeout);
            }
        }

        public TimeSpan ReceiveTimeout {
            get {
                if (this.node.ReceiveTimeout < 0) {
                    throw new InvalidOperationException("ReceiveTimeout must be larger >= 0", null);
                } 
                return new TimeSpan(0, 0, 0, 0, this.node.ReceiveTimeout);
            }
        }

        public TimeSpan DeadTimeout {
            get {
                if (this.node.DeadTimeout < 0) {
                    throw new InvalidOperationException("DeadTimeout must be larger >= 0", null);
                } 
                return new TimeSpan(0, 0, 0, 0, this.node.DeadTimeout);
            }
        }

        public TimeSpan QueueTimeOut {
            get {
                if (this.node.QueueTimeOut < 0) {
                    throw new InvalidOperationException("QueueTimeOut must be larger >= 0", null);
                }
                return new TimeSpan(0, 0, 0, 0, this.node.QueueTimeOut);
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
            if (String.IsNullOrEmpty(host))
                throw new ArgumentNullException("host");

            IPAddress address;

            // parse as an IP address
            if (!IPAddress.TryParse(host, out address)) {
                // not an ip, resolve from dns
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