using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enyim.Caching._Configuration {
    public class ServerNodeConfiguration {
        /// <summary>
        /// 服务器地址 [ip,port]格式
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
        /// <summary>
        /// 接收超时 
        /// </summary>
        public int ReceiveTimeout { get; set; }
        /// <summary>
        /// 节点宕机后重试时间间隔
        /// </summary>
        public int DeadTimeout { get; set; }

        public int QueueTimeOut { get; set; }
    }
}