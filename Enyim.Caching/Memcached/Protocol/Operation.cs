using System;
using System.Collections.Generic;

namespace Enyim.Caching.Memcached.Protocol
{
	/// <summary>
	/// Base class for implementing operations.
	/// </summary>
	public abstract class Operation : IOperation
	{
		private static readonly Enyim.Caching.ILog log = Enyim.Caching.LogManager.GetLogger(typeof(Operation));

		protected Operation() { }

		internal protected abstract IList<ArraySegment<byte>> GetBuffer();
		internal protected abstract bool ReadResponse(PooledSocket socket);
		internal protected abstract bool ReadResponseAsync(PooledSocket socket, Action<bool> next);

		IList<ArraySegment<byte>> IOperation.GetBuffer()
		{
			return this.GetBuffer();
		}

		bool IOperation.ReadResponse(PooledSocket socket)
		{
			return this.ReadResponse(socket);
		}

		bool IOperation.ReadResponseAsync(PooledSocket socket, Action<bool> next)
		{
			return this.ReadResponseAsync(socket, next);
		}

		int IOperation.StatusCode
		{
			get { return this.StatusCode; }
		}

		public int StatusCode { get; protected set; }
	}
}
