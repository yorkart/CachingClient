//-----------------------------------------------------------------------------------------
//   <copyright company="同程网" file="MemcachedClient.cs">
//      所属项目：Enyim.Caching._Memcached
//      创 建 人：王跃
//      创建日期：2012-6-5 20:15:09
//      用    途：请一定在此描述用途
//
//      更新记录:
//
//   </copyright> 
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using Enyim.Caching._Configuration;
using Enyim.Caching.Memcached;


namespace Enyim.Caching._Memcached {

    /// <summary>
    /// MemcachedClient 概要
    /// </summary>
    public class MemcachedClient : IMemcachedClient {

        private MemcachedConfiguration configuration;
        private IServerPool pool;
        public MemcachedClient(MemcachedConfiguration configuration) {
            this.configuration = configuration;
        }


        public object Get(string key) {
            throw new NotImplementedException();
        }

        public T Get<T>(string key) {
            throw new NotImplementedException();
        }

        public IDictionary<string, object> Get(IEnumerable<string> keys) {
            throw new NotImplementedException();
        }

        public bool TryGet(string key, out object value) {
            throw new NotImplementedException();
        }

        public bool TryGetWithCas(string key, out Memcached.CasResult<object> value) {
            throw new NotImplementedException();
        }

        public Memcached.CasResult<object> GetWithCas(string key) {
            throw new NotImplementedException();
        }

        public Memcached.CasResult<T> GetWithCas<T>(string key) {
            throw new NotImplementedException();
        }

        public IDictionary<string, Memcached.CasResult<object>> GetWithCas(IEnumerable<string> keys) {
            throw new NotImplementedException();
        }

        public bool Append(string key, ArraySegment<byte> data) {
            throw new NotImplementedException();
        }

        public Memcached.CasResult<bool> Append(string key, ulong cas, ArraySegment<byte> data) {
            throw new NotImplementedException();
        }

        public bool Prepend(string key, ArraySegment<byte> data) {
            throw new NotImplementedException();
        }

        public Memcached.CasResult<bool> Prepend(string key, ulong cas, ArraySegment<byte> data) {
            throw new NotImplementedException();
        }

        public bool Store(Memcached.StoreMode mode, string key, object value) {
            throw new NotImplementedException();
        }

        public bool Store(Memcached.StoreMode mode, string key, object value, DateTime expiresAt) {
            throw new NotImplementedException();
        }

        public bool Store(Memcached.StoreMode mode, string key, object value, TimeSpan validFor) {
            throw new NotImplementedException();
        }

        public Memcached.CasResult<bool> Cas(Memcached.StoreMode mode, string key, object value) {
            throw new NotImplementedException();
        }

        public Memcached.CasResult<bool> Cas(Memcached.StoreMode mode, string key, object value, ulong cas) {
            throw new NotImplementedException();
        }

        public Memcached.CasResult<bool> Cas(Memcached.StoreMode mode, string key, object value, DateTime expiresAt, ulong cas) {
            throw new NotImplementedException();
        }

        public Memcached.CasResult<bool> Cas(Memcached.StoreMode mode, string key, object value, TimeSpan validFor, ulong cas) {
            throw new NotImplementedException();
        }

        public ulong Decrement(string key, ulong defaultValue, ulong delta) {
            throw new NotImplementedException();
        }

        public ulong Decrement(string key, ulong defaultValue, ulong delta, DateTime expiresAt) {
            throw new NotImplementedException();
        }

        public ulong Decrement(string key, ulong defaultValue, ulong delta, TimeSpan validFor) {
            throw new NotImplementedException();
        }

        public Memcached.CasResult<ulong> Decrement(string key, ulong defaultValue, ulong delta, ulong cas) {
            throw new NotImplementedException();
        }

        public Memcached.CasResult<ulong> Decrement(string key, ulong defaultValue, ulong delta, DateTime expiresAt, ulong cas) {
            throw new NotImplementedException();
        }

        public Memcached.CasResult<ulong> Decrement(string key, ulong defaultValue, ulong delta, TimeSpan validFor, ulong cas) {
            throw new NotImplementedException();
        }

        public ulong Increment(string key, ulong defaultValue, ulong delta) {
            throw new NotImplementedException();
        }

        public ulong Increment(string key, ulong defaultValue, ulong delta, DateTime expiresAt) {
            throw new NotImplementedException();
        }

        public ulong Increment(string key, ulong defaultValue, ulong delta, TimeSpan validFor) {
            throw new NotImplementedException();
        }

        public Memcached.CasResult<ulong> Increment(string key, ulong defaultValue, ulong delta, ulong cas) {
            throw new NotImplementedException();
        }

        public Memcached.CasResult<ulong> Increment(string key, ulong defaultValue, ulong delta, DateTime expiresAt, ulong cas) {
            throw new NotImplementedException();
        }

        public Memcached.CasResult<ulong> Increment(string key, ulong defaultValue, ulong delta, TimeSpan validFor, ulong cas) {
            throw new NotImplementedException();
        }

        public bool Remove(string key) {
            throw new NotImplementedException();
        }

        public void FlushAll() {
            throw new NotImplementedException();
        }

        public Memcached.ServerStats Stats() {
            throw new NotImplementedException();
        }

        public Memcached.ServerStats Stats(string type) {
            throw new NotImplementedException();
        }

        public event Action<Memcached.IMemcachedNode> NodeFailed;

        public void Dispose() {
            throw new NotImplementedException();
        }
    }
}