//#define DEBUG_IO
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.IO;

namespace Enyim.Caching.Memcached
{
	public class AsyncIOArgs
	{
		public Action<AsyncIOArgs> Next { get; set; }
		public int Count { get; set; }

		public byte[] Result { get; internal set; }
		public bool Fail { get; internal set; }
	}
}