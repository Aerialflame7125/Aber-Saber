namespace System.Web.Util;

internal sealed class DateTimeUtil
{
	private const long FileTimeOffset = 504911232000000000L;

	private static readonly DateTime MinValuePlusOneDay = DateTime.MinValue.AddDays(1.0);

	private static readonly DateTime MaxValueMinusOneDay = DateTime.MaxValue.AddDays(-1.0);

	private DateTimeUtil()
	{
	}

	internal static DateTime FromFileTimeToUtc(long filetime)
	{
		return new DateTime(filetime + 504911232000000000L, DateTimeKind.Utc);
	}

	internal static DateTime ConvertToUniversalTime(DateTime localTime)
	{
		if (localTime < MinValuePlusOneDay)
		{
			return DateTime.MinValue;
		}
		if (localTime > MaxValueMinusOneDay)
		{
			return DateTime.MaxValue;
		}
		return localTime.ToUniversalTime();
	}

	internal static DateTime ConvertToLocalTime(DateTime utcTime)
	{
		if (utcTime < MinValuePlusOneDay)
		{
			return DateTime.MinValue;
		}
		if (utcTime > MaxValueMinusOneDay)
		{
			return DateTime.MaxValue;
		}
		return utcTime.ToLocalTime();
	}

	internal static TimeSpan GetTimeoutFromTimeUnit(int timeoutValue, TimeUnit timeoutUnit)
	{
		return timeoutUnit switch
		{
			TimeUnit.Days => new TimeSpan(timeoutValue, 0, 0, 0), 
			TimeUnit.Hours => new TimeSpan(timeoutValue, 0, 0), 
			TimeUnit.Seconds => new TimeSpan(0, 0, timeoutValue), 
			TimeUnit.Milliseconds => new TimeSpan(0, 0, 0, 0, timeoutValue), 
			TimeUnit.Minutes => new TimeSpan(0, timeoutValue, 0), 
			_ => throw new ArgumentException(global::SR.GetString("Invalid value for '{0}' parameter.", "timeoutUnit")), 
		};
	}
}
