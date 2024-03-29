//#define DEBUG_IO
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Enyim.Caching.Memcached
{
	public partial class PooledSocket
	{
		#region [ BasicNetworkStream           ]

		private class BasicNetworkStream : Stream
		{
			private Socket socket;

			public BasicNetworkStream(Socket socket)
			{
				this.socket = socket;
			}

			public override bool CanRead
			{
				get { return true; }
			}

			public override bool CanSeek
			{
				get { return false; }
			}

			public override bool CanWrite
			{
				get { return false; }
			}

			public override void Flush()
			{
			}

			public override long Length
			{
				get { throw new NotSupportedException(); }
			}

			public override long Position
			{
				get { throw new NotSupportedException(); }
				set { throw new NotSupportedException(); }
			}

			public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				SocketError errorCode;

				var retval = this.socket.BeginReceive(buffer, offset, count, SocketFlags.None, out errorCode, callback, state);

				if (errorCode == SocketError.Success)
					return retval;

				throw new System.IO.IOException(String.Format("Failed to read from the socket '{0}'. Error: {1}", this.socket.RemoteEndPoint, errorCode));
			}

			public override int EndRead(IAsyncResult asyncResult)
			{
				SocketError errorCode;

				var retval = this.socket.EndReceive(asyncResult, out errorCode);

				// actually "0 bytes read" could mean an error as well
				if (errorCode == SocketError.Success && retval > 0)
					return retval;

				throw new System.IO.IOException(String.Format("Failed to read from the socket '{0}'. Error: {1}", this.socket.RemoteEndPoint, errorCode));
			}

			public override int Read(byte[] buffer, int offset, int count)
			{
				SocketError errorCode;

				int retval = this.socket.Receive(buffer, offset, count, SocketFlags.None, out errorCode);

				// actually "0 bytes read" could mean an error as well
				if (errorCode == SocketError.Success && retval > 0)
					return retval;

				throw new System.IO.IOException(String.Format("Failed to read from the socket '{0}'. Error: {1}", this.socket.RemoteEndPoint, errorCode == SocketError.Success ? "?" : errorCode.ToString()));
			}

			public override long Seek(long offset, SeekOrigin origin)
			{
				throw new NotSupportedException();
			}

			public override void SetLength(long value)
			{
				throw new NotSupportedException();
			}

			public override void Write(byte[] buffer, int offset, int count)
			{
				throw new NotSupportedException();
			}
		}

		#endregion
	}
}