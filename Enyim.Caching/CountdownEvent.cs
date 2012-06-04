using System;
using System.Linq;
using System.Configuration;
using Enyim.Caching.Memcached;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Diagnostics;

namespace Enyim.Caching {
    public class CountdownEvent : IDisposable {
        private int count;
        private ManualResetEvent mre;

        public CountdownEvent(int count) {
            this.count = count;
            this.mre = new ManualResetEvent(false);
        }

        public void Signal() {
            if (this.count == 0) throw new InvalidOperationException("Counter underflow");

            int tmp = Interlocked.Decrement(ref this.count);

            if (tmp == 0) { if (!this.mre.Set()) throw new InvalidOperationException("couldn't signal"); } else if (tmp < 0)
                throw new InvalidOperationException("Counter underflow");
        }

        public void Wait() {
            if (this.count == 0) return;

            this.mre.WaitOne();
        }

        ~CountdownEvent() {
            this.Dispose();
        }

        void IDisposable.Dispose() {
            this.Dispose();
        }

        public void Dispose() {
            GC.SuppressFinalize(this);

            if (this.mre != null) {
                this.mre.Close();
                this.mre = null;
            }
        }
    }
}