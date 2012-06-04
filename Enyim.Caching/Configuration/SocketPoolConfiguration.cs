using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enyim.Caching.Memcached;

namespace Enyim.Caching.Configuration
{
    /// <summary>
    /// ���ӳ�����
    /// </summary>
	public class SocketPoolConfiguration : ISocketPoolConfiguration
	{
		private int minPoolSize = 10; // ��С������
		private int maxPoolSize = 20; // ���������
		private TimeSpan connectionTimeout = new TimeSpan(0, 0, 10); // ���ӳ�ʱ
		private TimeSpan receiveTimeout = new TimeSpan(0, 0, 10); // �������ݳ�ʱ
		private TimeSpan deadTimeout = new TimeSpan(0, 0, 10); // �������ӳ�ʱ
		private TimeSpan queueTimeout = new TimeSpan(0, 0, 0, 0, 100); // ���еȴ���ʱ
		private INodeFailurePolicyFactory policyFactory = new FailImmediatelyPolicyFactory(); // �ڵ�ʧ�ܲ���

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
