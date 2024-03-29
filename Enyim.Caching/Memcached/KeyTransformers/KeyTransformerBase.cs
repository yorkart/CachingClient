
namespace Enyim.Caching.Memcached
{
	public abstract class KeyTransformerBase : IMemcachedKeyTransformer
	{
		public abstract string Transform(string key);

		string IMemcachedKeyTransformer.Transform(string key)
		{
			return this.Transform(key);
		}
	}
}