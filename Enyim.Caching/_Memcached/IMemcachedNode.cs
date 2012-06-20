//using System;
//using System.Net;
//using Enyim.Caching.Memcached;

//namespace Enyim.Caching._MemcachedNode {

//    public interface IMemcachedNode__ : IDisposable {
//        IPEndPoint EndPoint { get; }
//        bool IsAlive { get; }
//        bool Ping();

//        bool Execute(IOperation op);
//        bool ExecuteAsync(IOperation op, Action<bool> next);

//        event Action<IMemcachedNode> Failed;
//    }
//}
