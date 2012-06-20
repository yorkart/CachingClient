using System.Collections.Generic;

namespace Enyim.Caching._Configuration {

    public class MemcachedConfiguration {
        /// <summary>
        /// 服务节点集合
        /// </summary>
        public IList<MemcachedNodeConfiguration> ServerList { get; set; }

        /// <summary>
        /// 服务器回传的身份验证key
        /// </summary>
        public string AuthenticationKey { get; set; }

        /// <summary>
        /// 协议
        /// "TEXT" / "BINARY"
        /// </summary>
        public string Protocol { get; set; }

        /// <summary>
        /// 虚拟节点基数
        /// </summary>
        public int VirtualNodeRadix { get; set; }
    }
}
