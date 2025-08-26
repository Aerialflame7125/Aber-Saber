namespace System.Web;

internal sealed class UplevelHelper
{
	public static bool IsUplevel(string ua)
	{
		if (ua == null)
		{
			return false;
		}
		int length = ua.Length;
		if (length == 0)
		{
			return false;
		}
		bool hasJavaScript = false;
		if (length > 3 && ua[0] == 'M' && ua[1] == 'o' && ua[2] == 'z' && ua[3] == 'i')
		{
			if (DetermineUplevel_1_1(ua, out hasJavaScript, length))
			{
				return hasJavaScript;
			}
			return false;
		}
		if (length > 3 && ua[0] == 'K' && ua[1] == 'o' && ua[2] == 'n' && ua[3] == 'q')
		{
			return true;
		}
		if (length > 3 && ua[0] == 'O' && ua[1] == 'p' && ua[2] == 'e' && ua[3] == 'r')
		{
			return true;
		}
		return false;
	}

	private static bool DetermineUplevel_1_1(string ua, out bool hasJavaScript, int ualength)
	{
		hasJavaScript = true;
		if (ualength > 10 && ua[7] == '/' && ua[8] == '4' && ua[9] == '.' && ua[10] == '0')
		{
			if (ualength > 28 && ua[13] == 'A' && ua[14] == 'c' && ua[15] == 't' && ua[16] == 'i' && ua[17] == 'v' && ua[18] == 'e' && ua[19] == 'T' && ua[20] == 'o' && ua[21] == 'u' && ua[22] == 'r' && ua[23] == 'i' && ua[24] == 's' && ua[25] == 't' && ua[26] == 'B' && ua[27] == 'o' && ua[28] == 't')
			{
				hasJavaScript = false;
				return true;
			}
			hasJavaScript = true;
			return true;
		}
		if (ualength > 10 && ua[7] == '/' && ua[8] == '5' && ua[9] == '.' && ua[10] == '0')
		{
			if (ualength > 28 && ua[13] == 'A' && ua[14] == 'c' && ua[15] == 't' && ua[16] == 'i' && ua[17] == 'v' && ua[18] == 'e' && ua[19] == 'T' && ua[20] == 'o' && ua[21] == 'u' && ua[22] == 'r' && ua[23] == 'i' && ua[24] == 's' && ua[25] == 't' && ua[26] == 'B' && ua[27] == 'o' && ua[28] == 't')
			{
				hasJavaScript = false;
				return true;
			}
			hasJavaScript = true;
			return true;
		}
		if (ScanForMatch_2_3(ua, out hasJavaScript, ualength))
		{
			return true;
		}
		if (ScanForMatch_2_4(ua, out hasJavaScript, ualength))
		{
			return true;
		}
		if (ualength > 15 && ua[12] == '(' && ua[13] == 'M' && ua[14] == 'a' && ua[15] == 'c')
		{
			hasJavaScript = true;
			return true;
		}
		if (ScanForMatch_2_6(ua, out hasJavaScript, ualength))
		{
			return true;
		}
		if (ualength > 15 && ua[12] == 'G' && ua[13] == 'a' && ua[14] == 'l' && ua[15] == 'e')
		{
			hasJavaScript = true;
			return true;
		}
		if (ualength > 28 && ua[25] == 'K' && ua[26] == 'o' && ua[27] == 'n' && ua[28] == 'q')
		{
			hasJavaScript = true;
			return true;
		}
		if (ualength > 12 && ua[9] == '/' && ua[10] == '4' && ua[11] == '.' && ua[12] == '[')
		{
			hasJavaScript = true;
			return true;
		}
		return false;
	}

	private static bool ScanForMatch_2_3(string ua, out bool hasJavaScript, int ualength)
	{
		hasJavaScript = true;
		if (ualength < 25)
		{
			return false;
		}
		int num = 0;
		int num2 = num + 7;
		for (int num3 = ualength; num3 >= 8; num3--)
		{
			if (ua[num] == ')' && ua[num2] == '/' && ua[num + 1] == ' ' && ua[num2 - 1] == 'o' && ua[num + 2] == 'G' && ua[num2 - 2] == 'k' && ua[num + 4] == 'c' && ua[num2 - 4] == 'e')
			{
				hasJavaScript = true;
				return true;
			}
			num++;
			num2++;
		}
		return false;
	}

	private static bool ScanForMatch_2_4(string ua, out bool hasJavaScript, int ualength)
	{
		hasJavaScript = true;
		if (ualength < 24)
		{
			return false;
		}
		int num = 0;
		int num2 = num + 6;
		for (int num3 = ualength; num3 >= 7; num3--)
		{
			if (ua[num] == ')' && ua[num2] == 'a' && ua[num + 1] == ' ' && ua[num2 - 1] == 'r' && ua[num + 2] == 'O' && ua[num2 - 2] == 'e' && ua[num + 3] == 'p')
			{
				hasJavaScript = true;
				return true;
			}
			num++;
			num2++;
		}
		return false;
	}

	private static bool ScanForMatch_2_6(string ua, out bool hasJavaScript, int ualength)
	{
		hasJavaScript = true;
		if (ualength < 21)
		{
			return false;
		}
		int num = 0;
		int num2 = num + 5;
		for (int num3 = ualength; num3 >= 6; num3--)
		{
			if (ua[num] == '(' && ua[num2] == 'L' && ua[num + 1] == 'K' && ua[num2 - 1] == 'M' && ua[num + 3] == 'T' && ua[num2 - 3] == 'H')
			{
				hasJavaScript = true;
				return true;
			}
			num++;
			num2++;
		}
		return false;
	}
}
