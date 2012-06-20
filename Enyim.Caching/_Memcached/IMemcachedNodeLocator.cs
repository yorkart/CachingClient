////-----------------------------------------------------------------------------------------
////   <copyright company="同程网" file="IMemcachedNodeLocator.cs">
////      所属项目：Enyim.Caching._Memcached
////      创 建 人：王跃
////      创建日期：2012-6-20 13:09:16
////      用    途：请一定在此描述用途
////
////      更新记录:
////
////   </copyright> 
////-----------------------------------------------------------------------------------------

//namespace Enyim.Caching._Memcached {
//    using System;
//    using System.Data;
//    using System.Collections;
//    using System.Collections.Generic;
//    using System.Text;
//    using Enyim.Caching._MemcachedNode;

//    /// <summary>
//    /// Defines a locator class which maps item keys to memcached servers.
//    /// </summary>
//    public interface IMemcachedNodeLocator__ {
//        /// <summary>
//        /// Initializes the locator.
//        /// </summary>
//        /// <param name="nodes">The memcached nodes defined in the configuration.</param>
//        /// <remarks>This called first when the server pool is initialized, and subsequently every time 
//        /// when a node goes down or comes back. If your locator has its own logic to deal with dead nodes 
//        /// then ignore all calls but the first. Otherwise make sure that your implementation can handle 
//        /// simultaneous calls to Initialize and Locate in a thread safe manner.</remarks>
//        /// <seealso cref="T:DefaultNodeLocator"/>
//        /// <seealso cref="T:KetamaNodeLocator"/>
//        void Initialize(IList<IMemcachedNode> nodes);

//        /// <summary>
//        /// Returns the memcached node the specified key belongs to.
//        /// </summary>
//        /// <param name="key">The key of the item to be located.</param>
//        /// <returns>The <see cref="T:MemcachedNode"/> the specifed item belongs to</returns>
//        IMemcachedNode Locate(string key);

//        /// <summary>
//        /// Returns all the working nodes currently available to the locator.
//        /// </summary>
//        /// <remarks>It should return an instance which is safe to enumerate multiple times and provides the same results every time.</remarks>
//        /// <returns></returns>
//        IEnumerable<IMemcachedNode> GetWorkingNodes();
//    }
//}
