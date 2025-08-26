using System.Runtime.InteropServices;
using System.Security;

namespace System.Globalization;

/// <summary>Retrieves information about a Unicode character. This class cannot be inherited.</summary>
public static class CharUnicodeInfo
{
	[StructLayout(LayoutKind.Explicit)]
	internal struct UnicodeDataHeader
	{
		[FieldOffset(0)]
		internal char TableName;

		[FieldOffset(32)]
		internal ushort version;

		[FieldOffset(40)]
		internal uint OffsetToCategoriesIndex;

		[FieldOffset(44)]
		internal uint OffsetToCategoriesValue;

		[FieldOffset(48)]
		internal uint OffsetToNumbericIndex;

		[FieldOffset(52)]
		internal uint OffsetToDigitValue;

		[FieldOffset(56)]
		internal uint OffsetToNumbericValue;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 2)]
	internal struct DigitValues
	{
		internal sbyte decimalDigit;

		internal sbyte digit;
	}

	internal const char HIGH_SURROGATE_START = '\ud800';

	internal const char HIGH_SURROGATE_END = '\udbff';

	internal const char LOW_SURROGATE_START = '\udc00';

	internal const char LOW_SURROGATE_END = '\udfff';

	internal const int UNICODE_CATEGORY_OFFSET = 0;

	internal const int BIDI_CATEGORY_OFFSET = 1;

	private static bool s_initialized = InitTable();

	[SecurityCritical]
	private unsafe static ushort* s_pCategoryLevel1Index;

	[SecurityCritical]
	private unsafe static byte* s_pCategoriesValue;

	[SecurityCritical]
	private unsafe static ushort* s_pNumericLevel1Index;

	[SecurityCritical]
	private unsafe static byte* s_pNumericValues;

	[SecurityCritical]
	private unsafe static DigitValues* s_pDigitValues;

	internal const string UNICODE_INFO_FILE_NAME = "charinfo.nlp";

	internal const int UNICODE_PLANE01_START = 65536;

	private unsafe static int EndianSwap(int value)
	{
		if (!BitConverter.IsLittleEndian)
		{
			byte* ptr = (byte*)(&value);
			int result = default(int);
			byte* ptr2 = (byte*)(&result);
			int num = 3;
			for (int i = 0; i < 4; i++)
			{
				ptr2[num - i] = ptr[i];
			}
			return result;
		}
		return value;
	}

	private unsafe static uint EndianSwap(uint value)
	{
		if (!BitConverter.IsLittleEndian)
		{
			byte* ptr = (byte*)(&value);
			uint result = default(uint);
			byte* ptr2 = (byte*)(&result);
			uint num = 3u;
			for (uint num2 = 0u; num2 < 4; num2++)
			{
				ptr2[num - num2] = ptr[num2];
			}
			return result;
		}
		return value;
	}

	private unsafe static ushort EndianSwap(ushort value)
	{
		if (!BitConverter.IsLittleEndian)
		{
			byte* ptr = (byte*)(&value);
			ushort result = default(ushort);
			byte* ptr2 = (byte*)(&result);
			ushort num = 1;
			for (ushort num2 = 0; num2 < 2; num2++)
			{
				ptr2[num - num2] = ptr[(int)num2];
			}
			return result;
		}
		return value;
	}

	private unsafe static double EndianSwap(double value)
	{
		if (!BitConverter.IsLittleEndian)
		{
			byte* ptr = (byte*)(&value);
			double result = default(double);
			byte* ptr2 = (byte*)(&result);
			ushort num = 7;
			for (ushort num2 = 0; num2 < 8; num2++)
			{
				ptr2[num - num2] = ptr[(int)num2];
			}
			return result;
		}
		return value;
	}

	[SecuritySafeCritical]
	private unsafe static bool InitTable()
	{
		UnicodeDataHeader* globalizationResourceBytePtr;
		UnicodeDataHeader* num = (globalizationResourceBytePtr = (UnicodeDataHeader*)GlobalizationAssembly.GetGlobalizationResourceBytePtr(typeof(CharUnicodeInfo).Assembly, "charinfo.nlp"));
		s_pCategoryLevel1Index = (ushort*)((byte*)num + EndianSwap(globalizationResourceBytePtr->OffsetToCategoriesIndex));
		s_pCategoriesValue = (byte*)num + EndianSwap(globalizationResourceBytePtr->OffsetToCategoriesValue);
		s_pNumericLevel1Index = (ushort*)((byte*)num + EndianSwap(globalizationResourceBytePtr->OffsetToNumbericIndex));
		s_pNumericValues = (byte*)num + EndianSwap(globalizationResourceBytePtr->OffsetToNumbericValue);
		s_pDigitValues = (DigitValues*)((byte*)num + EndianSwap(globalizationResourceBytePtr->OffsetToDigitValue));
		return true;
	}

	internal static int InternalConvertToUtf32(string s, int index)
	{
		if (index < s.Length - 1)
		{
			int num = s[index] - 55296;
			if (num >= 0 && num <= 1023)
			{
				int num2 = s[index + 1] - 56320;
				if (num2 >= 0 && num2 <= 1023)
				{
					return num * 1024 + num2 + 65536;
				}
			}
		}
		return s[index];
	}

	internal static int InternalConvertToUtf32(string s, int index, out int charLength)
	{
		charLength = 1;
		if (index < s.Length - 1)
		{
			int num = s[index] - 55296;
			if (num >= 0 && num <= 1023)
			{
				int num2 = s[index + 1] - 56320;
				if (num2 >= 0 && num2 <= 1023)
				{
					charLength++;
					return num * 1024 + num2 + 65536;
				}
			}
		}
		return s[index];
	}

	internal static bool IsWhiteSpace(string s, int index)
	{
		UnicodeCategory unicodeCategory = GetUnicodeCategory(s, index);
		if ((uint)(unicodeCategory - 11) <= 2u)
		{
			return true;
		}
		return false;
	}

	internal static bool IsWhiteSpace(char c)
	{
		UnicodeCategory unicodeCategory = GetUnicodeCategory(c);
		if ((uint)(unicodeCategory - 11) <= 2u)
		{
			return true;
		}
		return false;
	}

	[SecuritySafeCritical]
	internal unsafe static double InternalGetNumericValue(int ch)
	{
		ushort num = EndianSwap(s_pNumericLevel1Index[ch >> 8]);
		num = EndianSwap(s_pNumericLevel1Index[num + ((ch >> 4) & 0xF)]);
		byte* ptr = (byte*)(s_pNumericLevel1Index + (int)num);
		return EndianSwap(*(double*)(s_pNumericValues + (nint)(int)ptr[ch & 0xF] * (nint)8));
	}

	[SecuritySafeCritical]
	internal unsafe static DigitValues* InternalGetDigitValues(int ch)
	{
		ushort num = s_pNumericLevel1Index[ch >> 8];
		num = s_pNumericLevel1Index[num + ((ch >> 4) & 0xF)];
		byte* ptr = (byte*)(s_pNumericLevel1Index + (int)num);
		return s_pDigitValues + (int)ptr[ch & 0xF];
	}

	[SecuritySafeCritical]
	internal unsafe static sbyte InternalGetDecimalDigitValue(int ch)
	{
		return InternalGetDigitValues(ch)->decimalDigit;
	}

	[SecuritySafeCritical]
	internal unsafe static sbyte InternalGetDigitValue(int ch)
	{
		return InternalGetDigitValues(ch)->digit;
	}

	/// <summary>Gets the numeric value associated with the specified character.</summary>
	/// <param name="ch">The Unicode character for which to get the numeric value.</param>
	/// <returns>The numeric value associated with the specified character.  
	///  -or-  
	///  -1, if the specified character is not a numeric character.</returns>
	public static double GetNumericValue(char ch)
	{
		return InternalGetNumericValue(ch);
	}

	/// <summary>Gets the numeric value associated with the character at the specified index of the specified string.</summary>
	/// <param name="s">The <see cref="T:System.String" /> containing the Unicode character for which to get the numeric value.</param>
	/// <param name="index">The index of the Unicode character for which to get the numeric value.</param>
	/// <returns>The numeric value associated with the character at the specified index of the specified string.  
	///  -or-  
	///  -1, if the character at the specified index of the specified string is not a numeric character.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="s" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is outside the range of valid indexes in <paramref name="s" />.</exception>
	public static double GetNumericValue(string s, int index)
	{
		if (s == null)
		{
			throw new ArgumentNullException("s");
		}
		if (index < 0 || index >= s.Length)
		{
			throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
		}
		return InternalGetNumericValue(InternalConvertToUtf32(s, index));
	}

	/// <summary>Gets the decimal digit value of the specified numeric character.</summary>
	/// <param name="ch">The Unicode character for which to get the decimal digit value.</param>
	/// <returns>The decimal digit value of the specified numeric character.  
	///  -or-  
	///  -1, if the specified character is not a decimal digit.</returns>
	public static int GetDecimalDigitValue(char ch)
	{
		return InternalGetDecimalDigitValue(ch);
	}

	/// <summary>Gets the decimal digit value of the numeric character at the specified index of the specified string.</summary>
	/// <param name="s">The <see cref="T:System.String" /> containing the Unicode character for which to get the decimal digit value.</param>
	/// <param name="index">The index of the Unicode character for which to get the decimal digit value.</param>
	/// <returns>The decimal digit value of the numeric character at the specified index of the specified string.  
	///  -or-  
	///  -1, if the character at the specified index of the specified string is not a decimal digit.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="s" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is outside the range of valid indexes in <paramref name="s" />.</exception>
	public static int GetDecimalDigitValue(string s, int index)
	{
		if (s == null)
		{
			throw new ArgumentNullException("s");
		}
		if (index < 0 || index >= s.Length)
		{
			throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
		}
		return InternalGetDecimalDigitValue(InternalConvertToUtf32(s, index));
	}

	/// <summary>Gets the digit value of the specified numeric character.</summary>
	/// <param name="ch">The Unicode character for which to get the digit value.</param>
	/// <returns>The digit value of the specified numeric character.  
	///  -or-  
	///  -1, if the specified character is not a digit.</returns>
	public static int GetDigitValue(char ch)
	{
		return InternalGetDigitValue(ch);
	}

	/// <summary>Gets the digit value of the numeric character at the specified index of the specified string.</summary>
	/// <param name="s">The <see cref="T:System.String" /> containing the Unicode character for which to get the digit value.</param>
	/// <param name="index">The index of the Unicode character for which to get the digit value.</param>
	/// <returns>The digit value of the numeric character at the specified index of the specified string.  
	///  -or-  
	///  -1, if the character at the specified index of the specified string is not a digit.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="s" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is outside the range of valid indexes in <paramref name="s" />.</exception>
	public static int GetDigitValue(string s, int index)
	{
		if (s == null)
		{
			throw new ArgumentNullException("s");
		}
		if (index < 0 || index >= s.Length)
		{
			throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
		}
		return InternalGetDigitValue(InternalConvertToUtf32(s, index));
	}

	/// <summary>Gets the Unicode category of the specified character.</summary>
	/// <param name="ch">The Unicode character for which to get the Unicode category.</param>
	/// <returns>A <see cref="T:System.Globalization.UnicodeCategory" /> value indicating the category of the specified character.</returns>
	public static UnicodeCategory GetUnicodeCategory(char ch)
	{
		return InternalGetUnicodeCategory(ch);
	}

	/// <summary>Gets the Unicode category of the character at the specified index of the specified string.</summary>
	/// <param name="s">The <see cref="T:System.String" /> containing the Unicode character for which to get the Unicode category.</param>
	/// <param name="index">The index of the Unicode character for which to get the Unicode category.</param>
	/// <returns>A <see cref="T:System.Globalization.UnicodeCategory" /> value indicating the category of the character at the specified index of the specified string.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="s" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is outside the range of valid indexes in <paramref name="s" />.</exception>
	public static UnicodeCategory GetUnicodeCategory(string s, int index)
	{
		if (s == null)
		{
			throw new ArgumentNullException("s");
		}
		if ((uint)index >= (uint)s.Length)
		{
			throw new ArgumentOutOfRangeException("index");
		}
		return InternalGetUnicodeCategory(s, index);
	}

	internal static UnicodeCategory InternalGetUnicodeCategory(int ch)
	{
		return (UnicodeCategory)InternalGetCategoryValue(ch, 0);
	}

	[SecuritySafeCritical]
	internal unsafe static byte InternalGetCategoryValue(int ch, int offset)
	{
		ushort num = EndianSwap(s_pCategoryLevel1Index[ch >> 8]);
		num = EndianSwap(s_pCategoryLevel1Index[num + ((ch >> 4) & 0xF)]);
		byte* ptr = (byte*)(s_pCategoryLevel1Index + (int)num);
		byte b = ptr[ch & 0xF];
		return s_pCategoriesValue[b * 2 + offset];
	}

	internal static BidiCategory GetBidiCategory(string s, int index)
	{
		if (s == null)
		{
			throw new ArgumentNullException("s");
		}
		if ((uint)index >= (uint)s.Length)
		{
			throw new ArgumentOutOfRangeException("index");
		}
		return (BidiCategory)InternalGetCategoryValue(InternalConvertToUtf32(s, index), 1);
	}

	internal static UnicodeCategory InternalGetUnicodeCategory(string value, int index)
	{
		return InternalGetUnicodeCategory(InternalConvertToUtf32(value, index));
	}

	internal static UnicodeCategory InternalGetUnicodeCategory(string str, int index, out int charLength)
	{
		return InternalGetUnicodeCategory(InternalConvertToUtf32(str, index, out charLength));
	}

	internal static bool IsCombiningCategory(UnicodeCategory uc)
	{
		if (uc != UnicodeCategory.NonSpacingMark && uc != UnicodeCategory.SpacingCombiningMark)
		{
			return uc == UnicodeCategory.EnclosingMark;
		}
		return true;
	}
}
