using System;
using System.Net;
using System.Collections.Generic;
using Enyim.Caching.Memcached.Protocol;

namespace Enyim.Caching.Memcached
{
	public interface IMemcachedNode : IDisposable
	{
		IPEndPoint EndPoint { get; }
		bool IsAlive { get; }
		bool Ping();

		bool Execute(IOperation op);
		bool ExecuteAsync(IOperation op, Action<bool> next);

	//	PooledSocket CreateSocket(TimeSpan connectionTimeout, TimeSpan receiveTimeout);

		event Action<IMemcachedNode> Failed;

		//IAsyncResult BeginExecute(IOperation op, AsyncCallback callback, object state);
		//bool EndExecute(IAsyncResult result);
	}
}