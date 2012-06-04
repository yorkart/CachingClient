using System;

namespace Enyim.Caching.Memcached
{
	public interface IAuthenticator
	{
		bool Authenticate(PooledSocket socket);
	}
}