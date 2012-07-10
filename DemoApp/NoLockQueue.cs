using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DemoApp {
    public class ConcurrentLinkedQueue<T> {

        private class Node {
            internal T Item;
            internal Node Next;

            public Node(T item, Node next) {
                this.Item = item;
                this.Next = next;
            }
        }

        private Node _head;
        private Node _tail;
        private int length;

        public ConcurrentLinkedQueue() {
            _head = new Node(default(T), null);
            _tail = _head;
            length = 0;
        }


        public bool IsEmpty {
            get { return (_head.Next == null); }
        }



        public void Enqueue(T item) {
            Node newNode = new Node(item, null);

            while (true) {
                Node curTail = _tail;
                Node residue = curTail.Next;

                //判断_tail是否被其他process改变
                if (curTail == _tail) {
                    //A 有其他process执行C成功，_tail应该指向新的节点
                    if (residue == null) {
                        //C 如果其他process改变了tail.next节点，需要重新取新的tail节点
                        if (Interlocked.CompareExchange<Node>(ref curTail.Next, newNode, residue) == residue) {
                            //D 尝试修改tail
                            Interlocked.CompareExchange<Node>(ref _tail, newNode, curTail);

                            Interlocked.Increment(ref length);
                            return;
                        }
                    } else {
                        //B 帮助其他线程完成D操作
                        Interlocked.CompareExchange<Node>(ref _tail, residue, curTail);
                    }
                }
            }
        }



        public bool TryDequeue(out T result) {

            Node curHead;
            Node curTail;
            Node next;

            do {
                curHead = _head;
                curTail = _tail;
                next = curHead.Next;
                if (curHead == _head) {

                    if (next == null) {  //Queue为空
                        result = default(T);
                        return false;
                    }

                    if (curHead == curTail) { //Queue处于Enqueue第一个node的过程中
                        //尝试帮助其他Process完成操作
                        Interlocked.CompareExchange<Node>(ref _tail, next, curTail);
                    } else {
                        //取next.Item必须放到CAS之前
                        result = next.Item;
                        //如果_head没有发生改变，则将_head指向next并退出
                        if (Interlocked.CompareExchange<Node>(ref _head, next, curHead) == curHead) {
                            Interlocked.Decrement(ref length);
                            break;
                        }
                    }
                }
            }while (true);

            return true;
        }
    }
}
