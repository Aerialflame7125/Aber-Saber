using System.Globalization;

namespace System.Web.Util;

internal static class HttpDate
{
	private static readonly int[] s_tensDigit = new int[10] { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90 };

	private static readonly string[] s_days = new string[7] { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };

	private static readonly string[] s_months = new string[12]
	{
		"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct",
		"Nov", "Dec"
	};

	private static readonly sbyte[] s_monthIndexTable = new sbyte[64]
	{
		-1, 65, 2, 12, -1, -1, -1, 8, -1, -1,
		-1, -1, 7, -1, 78, -1, 9, -1, 82, -1,
		10, -1, 11, -1, -1, 5, -1, -1, -1, -1,
		-1, -1, -1, 65, 2, 12, -1, -1, -1, 8,
		-1, -1, -1, -1, 7, -1, 78, -1, 9, -1,
		82, -1, 10, -1, 11, -1, -1, 5, -1, -1,
		-1, -1, -1, -1
	};

	private static int atoi2(string s, int startIndex)
	{
		try
		{
			int num = s[startIndex] - 48;
			int num2 = s[1 + startIndex] - 48;
			return s_tensDigit[num] + num2;
		}
		catch
		{
			throw new FormatException(global::SR.GetString("Unable to convert two characters in the string '{0}' to a number starting at offset {1}.", s, startIndex));
		}
	}

	private static int make_month(string s, int startIndex)
	{
		int num = (s[2 + startIndex] - 64) & 0x3F;
		sbyte b = s_monthIndexTable[num];
		if (b >= 13)
		{
			b = b switch
			{
				78 => (sbyte)((s_monthIndexTable[(s[1 + startIndex] - 64) & 0x3F] == 65) ? 1 : 6), 
				82 => (sbyte)((s_monthIndexTable[(s[1 + startIndex] - 64) & 0x3F] != 65) ? 4 : 3), 
				_ => throw new FormatException(global::SR.GetString("Unable to convert characters in the string '{0}' to a month starting at offset {1}.", s, startIndex)), 
			};
		}
		string text = s_months[b - 1];
		if (s[startIndex] == text[0] && s[1 + startIndex] == text[1] && s[2 + startIndex] == text[2])
		{
			return b;
		}
		if (char.ToUpper(s[startIndex], CultureInfo.InvariantCulture) == text[0] && char.ToLower(s[1 + startIndex], CultureInfo.InvariantCulture) == text[1] && char.ToLower(s[2 + startIndex], CultureInfo.InvariantCulture) == text[2])
		{
			return b;
		}
		throw new FormatException(global::SR.GetString("Unable to convert characters in the string '{0}' to a month starting at offset {1}.", s, startIndex));
	}

	internal static DateTime UtcParse(string time)
	{
		if (time == null)
		{
			throw new ArgumentNullException("time");
		}
		int day;
		int month;
		int num3;
		int hour;
		int minute;
		int second;
		int num;
		if ((num = time.IndexOf(',')) != -1)
		{
			int num2 = time.Length - num;
			while (--num2 > 0 && time[++num] == ' ')
			{
			}
			if (time[num + 2] == '-')
			{
				if (num2 < 18)
				{
					throw new FormatException(global::SR.GetString("'{0}' was not of the correct format. Expected a string to be of the form 'Thursday, 10-Jun-93 01:29:59 GMT', 'Thu, 10 Jan 1993 01:29:59 GMT', or 'Wed Jun 09 01:29:59 1993 GMT'.", time));
				}
				day = atoi2(time, num);
				month = make_month(time, num + 3);
				num3 = atoi2(time, num + 7);
				num3 = ((num3 >= 50) ? (num3 + 1900) : (num3 + 2000));
				hour = atoi2(time, num + 10);
				minute = atoi2(time, num + 13);
				second = atoi2(time, num + 16);
			}
			else
			{
				if (num2 < 20)
				{
					throw new FormatException(global::SR.GetString("'{0}' was not of the correct format. Expected a string to be of the form 'Thursday, 10-Jun-93 01:29:59 GMT', 'Thu, 10 Jan 1993 01:29:59 GMT', or 'Wed Jun 09 01:29:59 1993 GMT'.", time));
				}
				day = atoi2(time, num);
				month = make_month(time, num + 3);
				num3 = atoi2(time, num + 7) * 100 + atoi2(time, num + 9);
				hour = atoi2(time, num + 12);
				minute = atoi2(time, num + 15);
				second = atoi2(time, num + 18);
			}
		}
		else
		{
			num = -1;
			int num4 = time.Length + 1;
			while (--num4 > 0 && time[++num] == ' ')
			{
			}
			if (num4 < 24)
			{
				throw new FormatException(global::SR.GetString("'{0}' was not of the correct format. Expected a string to be of the form 'Thursday, 10-Jun-93 01:29:59 GMT', 'Thu, 10 Jan 1993 01:29:59 GMT', or 'Wed Jun 09 01:29:59 1993 GMT'.", time));
			}
			day = atoi2(time, num + 8);
			month = make_month(time, num + 4);
			num3 = atoi2(time, num + 20) * 100 + atoi2(time, num + 22);
			hour = atoi2(time, num + 11);
			minute = atoi2(time, num + 14);
			second = atoi2(time, num + 17);
		}
		return new DateTime(num3, month, day, hour, minute, second, DateTimeKind.Utc);
	}
}
