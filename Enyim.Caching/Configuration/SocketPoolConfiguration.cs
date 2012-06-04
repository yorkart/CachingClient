using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enyim.Caching.Memcached;

namespace Enyim.Caching.Configuration
{
    /// <summary>
    /// 连接池配置
    /// </summary>
	public class SocketPoolConfiguration : ISocketPoolConfiguration
	{
		private int minPoolSize = 10; // 最小连接数
		private int maxPoolSize = 20; // 最大连接数
		private TimeSpan connectionTimeout = new TimeSpan(0, 0, 10); // 连接超时
		private TimeSpan receiveTimeout = new TimeSpan(0, 0, 10); // 接收数据超时
		private TimeSpan deadTimeout = new TimeSpan(0, 0, 10); // 死链连接超时
		private TimeSpan queueTimeout = new TimeSpan(0, 0, 0, 0, 100); // 队列等待超时
		private INodeFailurePolicyFactory policyFactory = new FailImmediatelyPolicyFactory(); // 节点失败策略

		int ISocketPoolConfiguration.MinPoolSize
		{
			get { return this.minPoolSize; }
			set
			{
				if (value < 0)
					throw new ArgumentOutOfRangeException("value", "MinPoolSize must be >= 0!");

				if (value > this.maxPoolSize)
					throw new ArgumentOutOfRangeException("value", "MinPoolSize must be <= MaxPoolSize!");

				this.minPoolSize = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating the maximum amount of sockets per server in the socket pool.
		/// </summary>
		/// <returns>The maximum amount of sockets per server in the socket pool. The default is 20.</returns>
		/// <remarks>It should be 0.75 * (number of threads) for optimal performance.</remarks>
		int ISocketPoolConfiguration.MaxPoolSize
		{
			get { return this.maxPoolSize; }
			set
			{
				if (value < this.minPoolSize)
					throw new ArgumentOutOfRangeException("value", "MaxPoolSize must be >= MinPoolSize!");

				this.maxPoolSize = value;
			}
		}

		TimeSpan ISocketPoolConfiguration.ConnectionTimeout
		{
			get { return this.connectionTimeout; }
			set
			{
				if (value < TimeSpan.Zero)
					throw new ArgumentOutOfRangeException("value", "value must be positive");

				this.connectionTimeout = value;
			}
		}

		TimeSpan ISocketPoolConfiguration.ReceiveTimeout
		{
			get { return this.receiveTimeout; }
			set
			{
				if (value < TimeSpan.Zero)
					throw new ArgumentOutOfRangeException("value", "value must be positive");

				this.receiveTimeout = value;
			}
		}

		TimeSpan ISocketPoolConfiguration.QueueTimeout
		{
			get { return this.queueTimeout; }
			set
			{
				if (value < TimeSpan.Zero)
					throw new ArgumentOutOfRangeException("value", "value must be positive");

				this.queueTimeout = value;
			}
		}

		TimeSpan ISocketPoolConfiguration.DeadTimeout
		{
			get { return this.deadTimeout; }
			set
			{
				if (value < TimeSpan.Zero)
					throw new ArgumentOutOfRangeException("value", "value must be positive");

				this.deadTimeout = value;
			}
		}

		INodeFailurePolicyFactory ISocketPoolConfiguration.FailurePolicyFactory
		{
			get { return this.policyFactory; }
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				this.policyFactory = value;
			}
		}
	}
}
