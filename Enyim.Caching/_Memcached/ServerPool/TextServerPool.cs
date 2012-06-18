//-----------------------------------------------------------------------------------------
//   <copyright company="同程网" file="TextServerPool.cs">
//      所属项目：Enyim.Caching._Memcached._ServerPool
//      创 建 人：王跃
//      创建日期：2012-6-5 20:39:50
//      用    途：请一定在此描述用途
//
//      更新记录:
//
//   </copyright> 
//-----------------------------------------------------------------------------------------

namespace Enyim.Caching._Memcached.ServerPool {
    using System;
    using System.Data;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using Enyim.Caching._Configuration;
    using Enyim.Caching._Memcached.Configuration;

    /// <summary>
    /// TextServerPool 概要
    /// </summary>
    public class TextServerPool : IServerPool{
        // 配置文件
        private MemcachedConfig configuration;


        public TextServerPool(MemcachedConfig configuration) {
            if (configuration == null) {
                throw new ArgumentNullException("socketConfig");
            }
            this.configuration = configuration;
        }

        public _MemcachedNode.IMemcachedNode Locate(string key) {
            throw new NotImplementedException();
        }

        public Memcached.IOperationFactory OperationFactory {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<_MemcachedNode.IMemcachedNode> GetWorkingNodes() {
            throw new NotImplementedException();
        }

        public void Start() {
            throw new NotImplementedException();
        }

        public event Action<_MemcachedNode.IMemcachedNode> NodeFailed;

        public void Dispose() {
            throw new NotImplementedException();
        }
    }
}
