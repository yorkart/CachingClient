using System.Collections.Generic;

namespace Enyim.Caching.Memcached {
    /// <summary>
    /// Provides the base interface for Memcached SASL authentication.
    /// </summary>
    public interface ISaslAuthenticationProvider {
        string Type { get; }
        void Initialize(Dictionary<string, object> parameters);

        byte[] Authenticate();
        byte[] Continue(byte[] data);
    }
}