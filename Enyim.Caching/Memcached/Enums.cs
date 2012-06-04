using System;

namespace Enyim.Caching.Memcached
{
	public enum MutationMode : byte { Increment = 0x05, Decrement = 0x06 };
	public enum ConcatenationMode : byte { Append = 0x0E, Prepend = 0x0F };
	public enum MemcachedProtocol { Binary, Text }
}