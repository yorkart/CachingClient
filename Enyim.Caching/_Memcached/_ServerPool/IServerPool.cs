//-----------------------------------------------------------------------------------------
//   <copyright company="同程网" file="IServerPool.cs">
//      所属项目：Enyim.Caching._Memcached
//      创 建 人：王跃
//      创建日期：2012-6-5 20:22:28
//      用    途：请一定在此描述用途
//
//      更新记录:
//
//   </copyright> 
//-----------------------------------------------------------------------------------------

namespace Enyim.Caching._Memcached {
    using System;
    using System.Data;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using Enyim.Caching._MemcachedNode;

    /// <summary>
    /// Provides custom server pool implementations
    /// 提供服务器连接池，实现类：DefaultServerPool
    /// </summary>
    public interface IServerPool : IDisposable {
        /// <summary>
        /// 按照KEY定位服务器节点
        /// </summary>
        /// <param name="key">需要对服务器操作的key</param>
        /// <returns></returns>
        IMemcachedNode Locate(string key);
        /// <summary>
        /// 操作工厂
        /// </summary>
        Enyim.Caching.Memcached.IOperationFactory OperationFactory { get; }
        /// <summary>
        /// 获取当前运行的节点集合
        /// </summary>
        /// <returns></returns>
        IEnumerable<IMemcachedNode> GetWorkingNodes();
        /// <summary>
        /// 开启连接池，打开所有连接
        /// </summary>
        void Start();
        /// <summary>
        /// 节点失败回调事件
        /// </summary>
        event Action<IMemcachedNode> NodeFailed;
    }
}
