using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enyim.Caching._Configuration;
using System.Net;

namespace Enyim.Caching._MemcachedNode {

    public interface IMemcachedNode {

        IPEndPoint ServerAddress { get; }

        int MinPoolSize { get; }

        int MaxPoolSize { get; }

        int BeginHashKey { get; }

        int EndHashKey { get; }

        TimeSpan ConnectionTimeout { get; }

        TimeSpan ReceiveTimeout { get; }

        TimeSpan DeadTimeout { get; }

        TimeSpan QueueTimeOut { get; }
    }
}
