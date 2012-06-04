using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enyim.Caching._Configuration {
    public class ServerNode {
        /// <summary>
        /// 服务器地址 ip,port格式
        /// </summary>
        public string ServerAddress { get; set; }

        public int MinPoolSize { get; set; }

        public int MaxPoolSize { get; set; }

        public int BeginHashKey { get; set; }

        public int EndHashKey { get; set; }
        /// <summary>
        /// 连接超时 毫秒为单位
        /// </summary>
        public int ConnectionTimeout { get; set; }

        public int ReceiveTimeout { get; set; }

        public int DeadTimeout { get; set; }

        public int QueueTimeOut { get; set; }
    }
}