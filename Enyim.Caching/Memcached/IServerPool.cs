using System;
using System.Collections.Generic;

namespace Enyim.Caching.Memcached
{
    /// <summary>
    /// Provides custom server pool implementations
    /// �ṩ���������ӳأ�ʵ���ࣺDefaultServerPool
	/// </summary>
	public interface IServerPool : IDisposable
	{
        /// <summary>
        /// ����KEY��λ�������ڵ�
        /// </summary>
        /// <param name="key">��Ҫ�Է�����������key</param>
        /// <returns></returns>
		IMemcachedNode Locate(string key);
        /// <summary>
        /// ��������
        /// </summary>
		IOperationFactory OperationFactory { get; }
        /// <summary>
        /// ��ȡ��ǰ���еĽڵ㼯��
        /// </summary>
        /// <returns></returns>
		IEnumerable<IMemcachedNode> GetWorkingNodes();
        /// <summary>
        /// �������ӳأ�����������
        /// </summary>
		void Start();
        /// <summary>
        /// �ڵ�ʧ�ܻص��¼�
        /// </summary>
		event Action<IMemcachedNode> NodeFailed;
	}
}
