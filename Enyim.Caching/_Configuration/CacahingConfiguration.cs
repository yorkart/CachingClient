using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enyim.Caching._Memcached;
using Enyim.Caching.Memcached;
using Enyim.Caching.Memcached.Protocol.Binary;

namespace Enyim.Caching._Configuration {

    public class CacahingConfiguration {
        /// <summary>
        /// 服务节点集合
        /// </summary>
        public IList<ServerNodeConfiguration> ServerList { get; set; }

        public ServerNodeConfiguration DefaultServer { get; set; }

        public string Protocol {get;set;} 

        public Enyim.Caching._Memcached.IServerPool CreateServerPool() {
            //switch (this.Protocol)
            //{
            //    case MemcachedProtocol.Text.ToString(): 
            //        return new DefaultServerPool(this, new Memcached.Protocol.Text.TextOperationFactory());
            //    case MemcachedProtocol.Binary.ToString(): 
            //        return new BinaryPool(this);
            //}

            //throw new ArgumentOutOfRangeException("Unknown protocol: " + (int)this.Protocol);
            return null;
        }
    }
}
