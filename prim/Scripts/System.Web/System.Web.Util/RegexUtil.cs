using System.Text.RegularExpressions;

namespace System.Web.Util;

internal class RegexUtil
{
	private static bool? _isRegexTimeoutSetInAppDomain;

	private static bool IsRegexTimeoutSetInAppDomain
	{
		get
		{
			if (!_isRegexTimeoutSetInAppDomain.HasValue)
			{
				bool value = false;
				try
				{
					value = AppDomain.CurrentDomain.GetData("REGEX_DEFAULT_MATCH_TIMEOUT") != null;
				}
				catch
				{
				}
				_isRegexTimeoutSetInAppDomain = value;
			}
			return _isRegexTimeoutSetInAppDomain.Value;
		}
	}

	public static bool IsMatch(string stringToMatch, string pattern, RegexOptions regOption, int? timeoutInMillsec)
	{
		int regexTimeout = GetRegexTimeout(timeoutInMillsec);
		if (regexTimeout > 0 || timeoutInMillsec.HasValue)
		{
			return Regex.IsMatch(stringToMatch, pattern, regOption, TimeSpan.FromMilliseconds(regexTimeout));
		}
		return Regex.IsMatch(stringToMatch, pattern, regOption);
	}

	public static Match Match(string stringToMatch, string pattern, RegexOptions regOption, int? timeoutInMillsec)
	{
		int regexTimeout = GetRegexTimeout(timeoutInMillsec);
		if (regexTimeout > 0 || timeoutInMillsec.HasValue)
		{
			return Regex.Match(stringToMatch, pattern, regOption, TimeSpan.FromMilliseconds(regexTimeout));
		}
		return Regex.Match(stringToMatch, pattern, regOption);
	}

	public static Regex CreateRegex(string pattern, RegexOptions option, int? timeoutInMillsec)
	{
		int regexTimeout = GetRegexTimeout(timeoutInMillsec);
		if (regexTimeout > 0 || timeoutInMillsec.HasValue)
		{
			return new Regex(pattern, option, TimeSpan.FromMilliseconds(regexTimeout));
		}
		return new Regex(pattern, option);
	}

	internal static Regex CreateRegex(string pattern, RegexOptions option)
	{
		return CreateRegex(pattern, option, null);
	}

	private static int GetRegexTimeout(int? timeoutInMillsec)
	{
		int result = -1;
		if (timeoutInMillsec.HasValue)
		{
			result = timeoutInMillsec.Value;
		}
		else if (!IsRegexTimeoutSetInAppDomain && BinaryCompatibility.Current.TargetsAtLeastFramework461)
		{
			result = 2000;
		}
		return result;
	}
}
