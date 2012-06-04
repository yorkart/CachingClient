using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enyim.Caching._Configuration;
using System.Net;

namespace Enyim.Caching._MemcachedNode {

    public interface IMemcachedNode {
        ServerNodeAdapter NodeAdapter { get; }
    }
}
