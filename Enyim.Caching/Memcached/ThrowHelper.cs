using System;
using System.Net;
using System.Net.Sockets;

namespace Enyim.Caching.Memcached {
    internal static class ThrowHelper {
        public static void ThrowSocketWriteError(IPEndPoint endpoint, SocketError error) {
            // move the string into resource file
            throw new System.IO.IOException(String.Format("Failed to write to the socket '{0}'. Error: {1}", endpoint, error));
        }
    }
}