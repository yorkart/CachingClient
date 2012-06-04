using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Net;
using System.Collections.ObjectModel;
using System.Web;

namespace Enyim.Caching.Configuration {
    public interface IVBucketConfiguration {
        HashAlgorithm CreateHashAlgorithm();
        IList<IPEndPoint> Servers { get; }
        IList<VBucket> Buckets { get; }
    }

    public struct VBucket {
        private int master;
        private int[] replicas;

        public VBucket(int master, int[] replicas) {
            this.master = master;
            this.replicas = replicas;
        }

        public int Master { get { return this.master; } }
        public int[] Replicas { get { return this.replicas; } }
    }
}