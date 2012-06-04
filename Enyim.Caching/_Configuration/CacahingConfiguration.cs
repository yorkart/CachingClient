using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enyim.Caching._Configuration {

    public class CacahingConfiguration {

        public IList<ServerNode> ServerList { get; set; }

        public ServerNode DefaultServer { get; set; }


    }
}
