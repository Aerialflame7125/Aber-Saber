using System.Text.RegularExpressions;

namespace System.Web.Configuration;

internal class DelayedRegex
{
	private string _regstring;

	private Regex _regex;

	internal DelayedRegex(string s)
	{
		_regex = null;
		_regstring = s;
	}

	internal Match Match(string s)
	{
		EnsureRegex();
		return _regex.Match(s);
	}

	internal int GroupNumberFromName(string name)
	{
		EnsureRegex();
		return _regex.GroupNumberFromName(name);
	}

	internal void EnsureRegex()
	{
		string regstring = _regstring;
		if (_regex == null)
		{
			_regex = new Regex(regstring);
			_regstring = null;
		}
	}
}
