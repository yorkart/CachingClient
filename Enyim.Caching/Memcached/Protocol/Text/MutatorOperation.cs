using System;
using System.Globalization;
using System.Text;
using System.Collections.Generic;

namespace Enyim.Caching.Memcached.Protocol.Text
{
	public class MutatorOperation : SingleItemOperation, IMutatorOperation
	{
		private MutationMode mode;
		private ulong delta;
		private ulong result;

		internal MutatorOperation(MutationMode mode, string key, ulong delta)
			: base(key)
		{
			this.delta = delta;
			this.mode = mode;
		}

		public ulong Result
		{
			get { return this.result; }
		}

		protected internal override IList<ArraySegment<byte>> GetBuffer()
		{
			var command = (this.mode == MutationMode.Increment ? "incr " : "decr ")
							+ this.Key
							+ " "
							+ this.delta.ToString(CultureInfo.InvariantCulture)
							+ TextSocketHelper.CommandTerminator;

			return TextSocketHelper.GetCommandBuffer(command);
		}

		protected internal override bool ReadResponse(PooledSocket socket)
		{
			string response = TextSocketHelper.ReadResponse(socket);

			//maybe we should throw an exception when the item is not found?
			if (String.Compare(response, "NOT_FOUND", StringComparison.Ordinal) == 0)
				return false;

			return UInt64.TryParse(response, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, CultureInfo.InvariantCulture, out this.result);
		}

		MutationMode IMutatorOperation.Mode
		{
			get { return this.mode; }
		}

		ulong IMutatorOperation.Result
		{
			get { return this.result; }
		}

		protected internal override bool ReadResponseAsync(PooledSocket socket, System.Action<bool> next)
		{
			throw new System.NotSupportedException();
		}
	}
}

#region [ License information          ]
/* ************************************************************
 * 
 *    Copyright (c) 2010 Attila Kisk? enyim.com
 *    
 *    Licensed under the Apache License, Version 2.0 (the "License");
 *    you may not use this file except in compliance with the License.
 *    You may obtain a copy of the License at
 *    
 *        http://www.apache.org/licenses/LICENSE-2.0
 *    
 *    Unless required by applicable law or agreed to in writing, software
 *    distributed under the License is distributed on an "AS IS" BASIS,
 *    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *    See the License for the specific language governing permissions and
 *    limitations under the License.
 *    
 * ************************************************************/
#endregion
