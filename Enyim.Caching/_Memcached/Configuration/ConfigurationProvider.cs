//-----------------------------------------------------------------------------------------
//   <copyright company="同程网" file="ConfigManager.cs">
//      所属项目：Enyim.Caching._Memcached.MemcachedNode
//      创 建 人：王跃
//      创建日期：2012-6-19 12:40:16
//      用    途：请一定在此描述用途
//
//      更新记录:
//
//   </copyright> 
//-----------------------------------------------------------------------------------------

namespace Enyim.Caching._Memcached.Configuration {
    using System;
    using System.Data;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using Enyim.Caching._Configuration;

    /// <summary>
    /// 提供本地化配置
    /// </summary>
    public class ConfigurationProvider {

        private MemcachedConfig curConfiguration;
        private MemcachedConfig oldConfiguration;

        private static ConfigurationProvider instance = null;

        private static Object objLock = new Object();

        private ConfigurationProvider() {
            this.IsChanging = false;
        }

        public static ConfigurationProvider Instance {
            get {
                if (instance == null) {
                    lock (objLock) {
                        if (instance == null) {
                            instance = new ConfigurationProvider();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// 注册节点
        /// </summary>
        /// <param name="configuration"></param>
        public void RegisterConfiguration(MemcachedConfiguration configuration) {
            if (configuration == null) {
                throw new Exception();
            }
            this.curConfiguration = new MemcachedConfig(configuration);
            this.oldConfiguration = null;

            this.IsChanging = false;
        }

        /// <summary>
        /// 开始配置变更
        /// </summary>
        /// <param name="newConfiguration"></param>
        public void Configuration_Changing(MemcachedConfiguration newConfiguration) {
            if (newConfiguration == null) {
                throw new Exception();
            }
            this.oldConfiguration = this.curConfiguration;
            this.curConfiguration = new MemcachedConfig(newConfiguration);

            this.IsChanging = true;
        }
        /// <summary>
        /// 配置变更结束
        /// </summary>
        public void Configuration_Changed() {
            this.oldConfiguration = null;
            this.IsChanging = false;
        }

        /// <summary>
        /// 当前配置
        /// </summary>
        public MemcachedConfig CurConfig { get { return this.curConfiguration; } }
        /// <summary>
        /// 老配置
        /// </summary>
        public MemcachedConfig OldConfig { get { return this.oldConfiguration; } }
        /// <summary>
        /// 是否处于配置变更中
        /// </summary>
        public bool IsChanging { get; private set; }
    }
}
