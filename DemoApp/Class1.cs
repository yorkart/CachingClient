using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;

namespace DemoApp {
    public class Class1 {

        public static void Main(String[] args) {
            //TimeSpan d = new TimeSpan(0, 0, 0, 0, 100000);
            //ConcurrentQueue<int> s;
        }


        void thread() {


        }



    }


    public class Semaphore {
        private ManualResetEvent waitEvent = new ManualResetEvent(false);
        private object syncObjWait = new object();
        private int maxCount = 1; //最大资源数
        private int currentCount = 0;  //当前资源数

        public Semaphore() {
        }

        public Semaphore(int maxCount) {
            this.maxCount = maxCount;
        }

        public bool Wait() {
            lock (syncObjWait){  //只能一个线程进入下面代码
　　
                bool waitResult = this.waitEvent.WaitOne(); //在此等待资源数大于零
                if (waitResult) {
                    lock (this) {
                        if (currentCount > 0) {
                            currentCount--;
                            if (currentCount == 0) {
                                this.waitEvent.Reset();
                            }
                        } else {
                            System.Diagnostics.Debug.Assert(false, "Semaphore is not allow current count < 0");
                        }
                    }
                }
                return waitResult;
            }
        }
        /**/
        /// <summary>
        /// 允许超时返回的 Wait 操作
        /// </summary>
        /// <param name="millisecondsTimeout"></param>
        /// <returns></returns>
        public bool Wait(int millisecondsTimeout) {
            lock (syncObjWait) {// Monitor 确保该范围类代码在临界区内

                bool waitResult = this.waitEvent.WaitOne(millisecondsTimeout, false);
                if (waitResult) {
                    lock (this) {
                        if (currentCount > 0) {
                            currentCount--;
                            if (currentCount == 0) {
                                this.waitEvent.Reset();
                            }
                        } else {
                            System.Diagnostics.Debug.Assert(false, "Semaphore is not allow current count < 0");
                        }
                    }
                }
                return waitResult;
            }
        }

        public bool Release() {
            lock (this) { // Monitor 确保该范围类代码在临界区内

                currentCount++;
                if (currentCount > this.maxCount) {
                    currentCount = this.maxCount;
                    return false;
                }
                this.waitEvent.Set();  //允许调用Wait的线程进入
            }
            return true;
        }
    }
}