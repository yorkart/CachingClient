using System;
using Enyim.Caching.Memcached;

namespace Enyim.Caching.Configuration
{
	/// <summary>
    /// 连接池配置接口
	/// Defines an interface for configuring the socket pool for the <see cref="T:MemcachedClient"/>.
	/// </summary>
	public interface ISocketPoolConfiguration
	{
		/// <summary>
        /// 最小连接数
		/// Gets or sets a value indicating the minimum amount of sockets per server in the socket pool.
		/// </summary>
		/// <returns>The minimum amount of sockets per server in the socket pool.</returns>
		int MinPoolSize
		{
			get;
			set;
		}

		/// <summary>
        /// 最大连接数
		/// Gets or sets a value indicating the maximum amount of sockets per server in the socket pool.
		/// </summary>
		/// <returns>The maximum amount of sockets per server in the socket pool.</returns>
		int MaxPoolSize
		{
			get;
			set;
		}

		/// <summary>
        /// 连接超时
		/// Gets or sets a value that specifies the amount of time after which the connection attempt will fail.
		/// </summary>
		/// <returns>The value of the connection timeout.</returns>
		TimeSpan ConnectionTimeout
		{
			get;
			set;
		}

		/// <summary>
        /// 队列等待超时
		/// Gets or sets a value that specifies the amount of time after which the getting a connection from the pool will fail.
		/// </summary>
		/// <returns>The value of the queue timeout.</returns>
		TimeSpan QueueTimeout
		{
			get;
			set;
		}

		/// <summary>
        /// 接收超时
		/// Gets or sets a value that specifies the amount of time after which receiving data from the socket will fail.
		/// </summary>
		/// <returns>The value of the receive timeout.</returns>
		TimeSpan ReceiveTimeout
		{
			get;
			set;
		}

		/// <summary>
        /// 死链连接超时
		/// Gets or sets a value that specifies the amount of time after which an unresponsive (dead) server will be checked if it is working.
		/// </summary>
		/// <returns>The value of the dead timeout.</returns>
		TimeSpan DeadTimeout
		{
			get;
			set;
		}

        /// <summary>
        /// 节点失败时策略
        /// </summary>
		INodeFailurePolicyFactory FailurePolicyFactory { get; set; }
	}
}