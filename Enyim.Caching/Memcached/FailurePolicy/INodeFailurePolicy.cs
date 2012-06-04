using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enyim.Caching.Memcached
{
	public interface INodeFailurePolicy
	{
		bool ShouldFail();
	}

	public interface INodeFailurePolicyFactory
	{
		INodeFailurePolicy Create(IMemcachedNode node);
	}
}