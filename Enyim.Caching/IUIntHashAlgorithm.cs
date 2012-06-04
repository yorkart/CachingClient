using System;

namespace Enyim
{
	internal interface IUIntHashAlgorithm
	{
		uint ComputeHash(byte[] data);
	}
}