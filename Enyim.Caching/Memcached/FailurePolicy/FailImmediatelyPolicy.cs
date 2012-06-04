using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enyim.Caching.Memcached
{
	/// <summary>
	/// Fails a node immediately when an error occures. This is the default policy.
    /// 节点错误引发
	/// </summary>
	public sealed class FailImmediatelyPolicy : INodeFailurePolicy
	{
		bool INodeFailurePolicy.ShouldFail()
		{
			return true;
		}
	}

	/// <summary>
	/// Creates instances of <see cref="T:FailImmediatelyPolicy"/>.
	/// </summary>
	public class FailImmediatelyPolicyFactory : INodeFailurePolicyFactory
	{
		private static readonly INodeFailurePolicy PolicyInstance = new FailImmediatelyPolicy();

		INodeFailurePolicy INodeFailurePolicyFactory.Create(IMemcachedNode node)
		{
			return PolicyInstance;
		}
	}
}