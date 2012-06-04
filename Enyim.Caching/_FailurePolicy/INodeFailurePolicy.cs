//-----------------------------------------------------------------------------------------
//   <copyright company="同程网" file="INodeFailurePolicy.cs">
//      所属项目：Enyim.Caching._FailurePolicy
//      创 建 人：王跃
//      创建日期：2012-6-4 20:54:21
//      用    途：请一定在此描述用途
//
//      更新记录:
//
//   </copyright> 
//-----------------------------------------------------------------------------------------

namespace Enyim.Caching._FailurePolicy {
    using System;
    using System.Data;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using Enyim.Caching._MemcachedNode;

    /// <summary>
    /// INodeFailurePolicy 概要
    /// </summary>
    public interface INodeFailurePolicy {
        bool ShouldFail();
    }

    public interface INodeFailurePolicyFactory {
        INodeFailurePolicy Create(IMemcachedNode node);
    }
}