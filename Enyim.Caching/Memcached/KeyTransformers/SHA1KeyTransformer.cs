using System;
using System.Security.Cryptography;
using System.Text;

namespace Enyim.Caching.Memcached
{
	/// <summary>
	/// A key transformer which converts the item keys into their SHA1 hash.
	/// </summary>
	public class SHA1KeyTransformer : KeyTransformerBase
	{
		public override string Transform(string key)
		{
			SHA1Managed sh = new SHA1Managed();
			byte[] data = sh.ComputeHash(Encoding.Unicode.GetBytes(key));

			return Convert.ToBase64String(data, Base64FormattingOptions.None);
		}
	}
}