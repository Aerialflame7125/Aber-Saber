using System.Globalization;

namespace System.Web.Util;

internal sealed class TimeUtil
{
	private TimeUtil()
	{
	}

	internal static string ToUtcTimeString(DateTime dt)
	{
		return dt.ToUniversalTime().ToString("R", DateTimeFormatInfo.InvariantInfo);
	}
}
