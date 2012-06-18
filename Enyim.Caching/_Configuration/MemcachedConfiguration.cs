using System.Collections.Generic;

namespace Enyim.Caching._Configuration {

    public class MemcachedConfiguration {
        /// <summary>
        /// 服务节点集合
        /// </summary>
        public IList<MemcachedNodeConfiguration> ServerList { get; set; }

        //public ServerNodeConfiguration DefaultServer { get; set; }

        /// <summary>
        /// 协议
        /// "TEXT" / "BINARY"
        /// </summary>
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
