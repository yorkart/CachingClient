using System;
using System.Configuration;

namespace Enyim.Caching.Configuration {
    internal class ConfigurationElementException : ApplicationException {
        public ConfigurationElementException(string message) : this(message, null) { }
        public ConfigurationElementException(string message, Exception inner) : base(message, inner) { }
    }
}