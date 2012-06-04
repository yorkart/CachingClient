using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enyim.Caching._Configuration {

    public class CacahingConfiguration {

        public IList<ServerNodeConfiguration> ServerList { get; set; }

        public ServerNodeConfiguration DefaultServer { get; set; }


    }
}
