using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enyim.Caching._Configuration;
using System.Net;

namespace Enyim.Caching._MemcachedNode {

    public class DefaultMemcachedNode : IMemcachedNode {

        private ServerNode node;

        public DefaultMemcachedNode(ServerNode node) {
            this.node = node;
        }

        public int MinPoolSize {
            get { return this.node.MinPoolSize; }
        }

        public int MaxPoolSize {
            get { return this.node.MaxPoolSize; }
        }

        public IPEndPoint ServerAddress {
            get { return ResolveToEndPoint(this.node.ServerAddress); }
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
            get { return new TimeSpan(0, 0, 0, 0, this.node.ReceiveTimeout); }
        }

        public TimeSpan DeadTimeout {
            get { return new TimeSpan(0, 0, 0, 0, this.node.DeadTimeout); }
        }

        public TimeSpan QueueTimeOut {
            get { return new TimeSpan(0, 0, 0, 0, this.node.QueueTimeOut); }
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
