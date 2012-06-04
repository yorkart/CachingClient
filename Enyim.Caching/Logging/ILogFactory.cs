using System;

namespace Enyim.Caching {
    /// <summary>
    /// Implement this interface to instantiate your custom ILog implementation
    /// </summary>
    public interface ILogFactory {
        ILog GetLogger(string name);
        ILog GetLogger(Type type);
    }
}
