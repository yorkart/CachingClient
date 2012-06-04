using System;

namespace Enyim.Caching.Memcached {
    /// <summary>
    /// Represents an object either being retrieved from the cache
    /// or being sent to the cache.
    /// </summary>
    public struct CacheItem {
        private ArraySegment<byte> data;
        private uint flags;

        /// <summary>
        /// Initializes a new instance of <see cref="T:CacheItem"/>.
        /// </summary>
        /// <param name="flags">Custom item data.</param>
        /// <param name="data">The serialized item.</param>
        public CacheItem(uint flags, ArraySegment<byte> data) {
            this.data = data;
            this.flags = flags;
        }

        /// <summary>
        /// The data representing the item being stored/retireved.
        /// </summary>
        public ArraySegment<byte> Data {
            get { return this.data; }
            set { this.data = value; }
        }

        /// <summary>
        /// Flags set for this instance.
        /// </summary>
        public uint Flags {
            get { return this.flags; }
            set { this.flags = value; }
        }
    }
}