using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Web.Util;

internal static class StringUtil
{
	internal static string CheckAndTrimString(string paramValue, string paramName)
	{
		return CheckAndTrimString(paramValue, paramName, throwIfNull: true);
	}

	internal static string CheckAndTrimString(string paramValue, string paramName, bool throwIfNull)
	{
		return CheckAndTrimString(paramValue, paramName, throwIfNull, -1);
	}

	internal static string CheckAndTrimString(string paramValue, string paramName, bool throwIfNull, int lengthToCheck)
	{
		if (paramValue == null)
		{
			if (throwIfNull)
			{
				throw new ArgumentNullException(paramName);
			}
			return null;
		}
		string text = paramValue.Trim();
		if (text.Length == 0)
		{
			throw new ArgumentException(global::SR.GetString("Input parameter '{0}' cannot be an empty string.", paramName));
		}
		if (lengthToCheck > -1 && text.Length > lengthToCheck)
		{
			throw new ArgumentException(global::SR.GetString("Trimmed string value '{0}' of input parameter '{1}' cannot exceed character length {2}.", paramValue, paramName, lengthToCheck.ToString(CultureInfo.InvariantCulture)));
		}
		return text;
	}

	internal static bool Equals(string s1, string s2)
	{
		if (s1 == s2)
		{
			return true;
		}
		if (string.IsNullOrEmpty(s1) && string.IsNullOrEmpty(s2))
		{
			return true;
		}
		return false;
	}

	internal unsafe static bool Equals(string s1, int offset1, string s2, int offset2, int length)
	{
		if (offset1 < 0)
		{
			throw new ArgumentOutOfRangeException("offset1");
		}
		if (offset2 < 0)
		{
			throw new ArgumentOutOfRangeException("offset2");
		}
		if (length < 0)
		{
			throw new ArgumentOutOfRangeException("length");
		}
		if ((s1?.Length ?? 0) - offset1 < length)
		{
			throw new ArgumentOutOfRangeException(global::SR.GetString("The sum of {0} and {1} is greater than the length of the buffer.", "offset1", "length"));
		}
		if ((s2?.Length ?? 0) - offset2 < length)
		{
			throw new ArgumentOutOfRangeException(global::SR.GetString("The sum of {0} and {1} is greater than the length of the buffer.", "offset2", "length"));
		}
		if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2))
		{
			return true;
		}
		fixed (char* ptr = s1)
		{
			fixed (char* ptr3 = s2)
			{
				char* ptr2 = ptr + offset1;
				char* ptr4 = ptr3 + offset2;
				int num = length;
				while (num-- > 0)
				{
					if (*(ptr2++) != *(ptr4++))
					{
						return false;
					}
				}
			}
		}
		return true;
	}

	internal static bool EqualsIgnoreCase(string s1, string s2)
	{
		if (string.IsNullOrEmpty(s1) && string.IsNullOrEmpty(s2))
		{
			return true;
		}
		if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2))
		{
			return false;
		}
		if (s2.Length != s1.Length)
		{
			return false;
		}
		return string.Compare(s1, 0, s2, 0, s2.Length, StringComparison.OrdinalIgnoreCase) == 0;
	}

	internal static bool EqualsIgnoreCase(string s1, int index1, string s2, int index2, int length)
	{
		return string.Compare(s1, index1, s2, index2, length, StringComparison.OrdinalIgnoreCase) == 0;
	}

	internal unsafe static string StringFromWCharPtr(IntPtr ip, int length)
	{
		return new string((char*)(void*)ip, 0, length);
	}

	internal static string StringFromCharPtr(IntPtr ip, int length)
	{
		return Marshal.PtrToStringAnsi(ip, length);
	}

	internal static bool StringEndsWith(string s, char c)
	{
		int length = s.Length;
		if (length != 0)
		{
			return s[length - 1] == c;
		}
		return false;
	}

	internal unsafe static bool StringEndsWith(string s1, string s2)
	{
		int num = s1.Length - s2.Length;
		if (num < 0)
		{
			return false;
		}
		fixed (char* ptr = s1)
		{
			fixed (char* ptr3 = s2)
			{
				char* ptr2 = ptr + num;
				char* ptr4 = ptr3;
				int length = s2.Length;
				while (length-- > 0)
				{
					if (*(ptr2++) != *(ptr4++))
					{
						return false;
					}
				}
			}
		}
		return true;
	}

	internal static bool StringEndsWithIgnoreCase(string s1, string s2)
	{
		int num = s1.Length - s2.Length;
		if (num < 0)
		{
			return false;
		}
		return string.Compare(s1, num, s2, 0, s2.Length, StringComparison.OrdinalIgnoreCase) == 0;
	}

	internal static bool StringStartsWith(string s, char c)
	{
		if (s.Length != 0)
		{
			return s[0] == c;
		}
		return false;
	}

	internal unsafe static bool StringStartsWith(string s1, string s2)
	{
		if (s2.Length > s1.Length)
		{
			return false;
		}
		fixed (char* ptr = s1)
		{
			fixed (char* ptr3 = s2)
			{
				char* ptr2 = ptr;
				char* ptr4 = ptr3;
				int length = s2.Length;
				while (length-- > 0)
				{
					if (*(ptr2++) != *(ptr4++))
					{
						return false;
					}
				}
			}
		}
		return true;
	}

	internal static bool StringStartsWithIgnoreCase(string s1, string s2)
	{
		if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2))
		{
			return false;
		}
		if (s2.Length > s1.Length)
		{
			return false;
		}
		return string.Compare(s1, 0, s2, 0, s2.Length, StringComparison.OrdinalIgnoreCase) == 0;
	}

	internal static bool StringArrayEquals(string[] a, string[] b)
	{
		if (a == null != (b == null))
		{
			return false;
		}
		if (a == null)
		{
			return true;
		}
		int num = a.Length;
		if (num != b.Length)
		{
			return false;
		}
		for (int i = 0; i < num; i++)
		{
			if (a[i] != b[i])
			{
				return false;
			}
		}
		return true;
	}

	internal unsafe static int GetStringHashCode(string s)
	{
		fixed (char* ptr = s)
		{
			int num = 352654597;
			int num2 = num;
			int* ptr2 = (int*)ptr;
			for (int num3 = s.Length; num3 > 0; num3 -= 4)
			{
				num = ((num << 5) + num + (num >> 27)) ^ *ptr2;
				if (num3 <= 2)
				{
					break;
				}
				num2 = ((num2 << 5) + num2 + (num2 >> 27)) ^ ptr2[1];
				ptr2 += 2;
			}
			return num + num2 * 1566083941;
		}
	}

	internal static int GetNullTerminatedByteArray(Encoding enc, string s, out byte[] bytes)
	{
		bytes = null;
		if (s == null)
		{
			return 0;
		}
		bytes = new byte[enc.GetMaxByteCount(s.Length) + 1];
		return enc.GetBytes(s, 0, s.Length, bytes, 0);
	}

	internal unsafe static void memcpyimpl(byte* src, byte* dest, int len)
	{
		if (len >= 16)
		{
			do
			{
				*(int*)dest = *(int*)src;
				*(int*)(dest + 4) = *(int*)(src + 4);
				*(int*)(dest + (nint)2 * (nint)4) = *(int*)(src + (nint)2 * (nint)4);
				*(int*)(dest + (nint)3 * (nint)4) = *(int*)(src + (nint)3 * (nint)4);
				dest += 16;
				src += 16;
			}
			while ((len -= 16) >= 16);
		}
		if (len > 0)
		{
			if ((len & 8) != 0)
			{
				*(int*)dest = *(int*)src;
				*(int*)(dest + 4) = *(int*)(src + 4);
				dest += 8;
				src += 8;
			}
			if ((len & 4) != 0)
			{
				*(int*)dest = *(int*)src;
				dest += 4;
				src += 4;
			}
			if ((len & 2) != 0)
			{
				*(short*)dest = *(short*)src;
				dest += 2;
				src += 2;
			}
			if ((len & 1) != 0)
			{
				*(dest++) = *(src++);
			}
		}
	}

	internal static string[] ObjectArrayToStringArray(object[] objectArray)
	{
		string[] array = new string[objectArray.Length];
		objectArray.CopyTo(array, 0);
		return array;
	}
}
