using System;
using System.Collections.Generic;

namespace Enyim.Caching.Memcached
{
    /// <summary>
    /// Provides custom server pool implementations
    /// 提供服务器连接池，实现类：DefaultServerPool
	/// </summary>
	public interface IServerPool : IDisposable
	{
        /// <summary>
        /// 按照KEY定位服务器节点
        /// </summary>
        /// <param name="key">需要对服务器操作的key</param>
        /// <returns></returns>
		IMemcachedNode Locate(string key);
        /// <summary>
        /// 操作工厂
        /// </summary>
		IOperationFactory OperationFactory { get; }
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
