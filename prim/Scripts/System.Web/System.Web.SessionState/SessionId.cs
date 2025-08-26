using System.Security.Cryptography;
using System.Web.Util;

namespace System.Web.SessionState;

internal class SessionId
{
	internal const int IdLength = 24;

	private const int half_len = 12;

	private static RandomNumberGenerator rng = RandomNumberGenerator.Create();

	internal static string Create()
	{
		byte[] array = new byte[12];
		lock (rng)
		{
			rng.GetBytes(array);
		}
		return MachineKeySectionUtils.GetHexString(array);
	}
}
