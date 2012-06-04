using System;
using System.Configuration;

namespace Enyim.Caching {
    /// <summary>
    /// Creates loggers based on the current configuration.
    /// </summary>
    /// <example>
    /// 
    /// Config file:
    /// 
    /// <configuration>
    ///		<configSections>
    ///			<sectionGroup name="enyim.com">
    ///				<section name="log" type="Enyim.Caching.EnyimLoggerSection, Enyim.Caching" />
    ///			</sectionGroup>
    ///		</configSections>
    ///		<enyim.com>
    ///			<log factory="Enyim.Caching.Log4NetLoggerFactory, Enyim.Caching" />
    ///		</enyim.com>
    /// </configuration>
    /// 
    /// Code:
    /// 
    ///		LogManager.AssignFactory(new Log4NetLogFactory());
    /// 
    /// </example>
    public static class LogManager {
        private static ILogFactory factory;

        static LogManager() {
            LogManager.factory = (ILogFactory)new NullLoggerFactory();
            //            var section = ConfigurationManager.GetSection("enyim.com/log") as Enyim.Caching.Configuration.LoggerSection;
            //            ILogFactory f = null;

            //            if (section != null && section.LogFactory != null)
            //            {
            //                f = Enyim.Reflection.FastActivator.Create(section.LogFactory) as ILogFactory;
            //            }
            //#if !log4net
            //            // use an empty logger if nothing is specified in the app.config
            //            LogManager.factory = f ?? (ILogFactory)new NullLoggerFactory();
            //#else
            //            // use the log4net logger logger if nothing is specified in the app.config
            //            LogManager.factory = f ?? (ILogFactory)new Log4NetLogFactory();
            //#endif
        }

        /// <summary>
        /// Assigns a new logger factory programmatically.
        /// </summary>
        /// <param name="factory"></param>
        public static void AssignFactory(ILogFactory factory) {
            if (factory == null) throw new ArgumentNullException("factory");
            LogManager.factory = factory;
        }

        /// <summary>
        /// Returns a new logger for the specified Type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ILog GetLogger(Type type) {
            return factory.GetLogger(type);
        }

        /// <summary>
        /// Returns a logger with the specified name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ILog GetLogger(string name) {
            return factory.GetLogger(name);
        }
    }
}