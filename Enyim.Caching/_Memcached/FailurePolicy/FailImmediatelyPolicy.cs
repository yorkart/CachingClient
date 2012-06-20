//-----------------------------------------------------------------------------------------
//   <copyright company="同程网" file="FailImmediatelyPolicy.cs">
//      所属项目：Enyim.Caching._MemcachedNode
//      创 建 人：王跃
//      创建日期：2012-6-4 20:55:55
//      用    途：请一定在此描述用途
//
//      更新记录:
//
//   </copyright> 
//-----------------------------------------------------------------------------------------

using System;
using Enyim.Caching._Memcached;
using Enyim.Caching.Memcached;

namespace Enyim.Caching.FailurePolicy {

    /// <summary>
    /// Fails a node immediately when an error occures. This is the default policy.
    /// 节点错误引发
    /// </summary>
    public sealed class FailImmediatelyPolicy : INodeFailurePolicy {
        
        bool INodeFailurePolicy.ShouldFail() {
            return true;
        }
    }
}