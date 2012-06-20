//-----------------------------------------------------------------------------------------
//   <copyright company="同程网" file="IMemcachedClientConfig.cs">
//      所属项目：Enyim.Caching._Memcached
//      创 建 人：王跃
//      创建日期：2012-6-19 12:45:56
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
    using Enyim.Caching._Memcached.Configuration;
    using Enyim.Caching.Memcached;

    /// <summary>
    /// IMemcachedClientConfig 概要
    /// </summary>
    public interface IMemcachedClientConfig {

        MemcachedNodeConfig[] ServerNodes { get; }

        IMemcachedKeyTransformer KeyTransformer { get; }

        IMemcachedNodeLocator NodeLocator { get; }

        ITranscoder Transcoder { get; }

        IServerPool ServerPool { get; }

        IPerformanceMonitor PerformanceMonitor { get; }

    }
}
