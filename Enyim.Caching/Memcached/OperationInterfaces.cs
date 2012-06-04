using System;
using System.Net;
using System.Collections.Generic;
using Enyim.Caching.Memcached.Protocol;

namespace Enyim.Caching.Memcached {
    public interface IOperation {
        IList<ArraySegment<byte>> GetBuffer();
        bool ReadResponse(PooledSocket socket);

        int StatusCode { get; }

        /// <summary>
        /// 'next' is called when the operation completes. The bool parameter indicates the success of the operation.
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        bool ReadResponseAsync(PooledSocket socket, Action<bool> next);
    }

    public interface ISingleItemOperation : IOperation {
        string Key { get; }

        /// <summary>
        /// The CAS value returned by the server after executing the command.
        /// </summary>
        ulong CasValue { get; }
    }

    public interface IMultiItemOperation : IOperation {
        IList<string> Keys { get; }
        Dictionary<string, ulong> Cas { get; }
    }

    public interface IGetOperation : ISingleItemOperation {
        CacheItem Result { get; }
    }

    public interface IMultiGetOperation : IMultiItemOperation {
        Dictionary<string, CacheItem> Result { get; }
    }

    public interface IStoreOperation : ISingleItemOperation {
        StoreMode Mode { get; }
    }

    public interface IDeleteOperation : ISingleItemOperation {
    }

    public interface IConcatOperation : ISingleItemOperation {
        ConcatenationMode Mode { get; }
    }

    public interface IMutatorOperation : ISingleItemOperation {
        MutationMode Mode { get; }
        ulong Result { get; }
    }

    public interface IStatsOperation : IOperation {
        Dictionary<string, string> Result { get; }
    }

    public interface IFlushOperation : IOperation {
    }

    public struct CasResult<T> {
        public T Result { get; set; }
        public ulong Cas { get; set; }
        public int StatusCode { get; set; }
    }
}