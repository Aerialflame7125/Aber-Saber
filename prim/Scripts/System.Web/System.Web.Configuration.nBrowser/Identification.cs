using System.Text.RegularExpressions;

namespace System.Web.Configuration.nBrowser;

internal class Identification
{
	private bool MatchType = true;

	private string MatchName = string.Empty;

	private string MatchGroup = string.Empty;

	private string MatchPattern = string.Empty;

	private Regex RegexPattern;

	public string Name => MatchName;

	public string Group => MatchGroup;

	public string Pattern => MatchPattern;

	public Identification(bool matchType, string matchGroup, string matchName, string matchPattern)
	{
		MatchType = matchType;
		MatchGroup = matchGroup;
		MatchName = matchName;
		MatchPattern = matchPattern;
		RegexPattern = new Regex(matchPattern);
	}

	public Match GetMatch(string Header)
	{
		return RegexPattern.Match((Header == null) ? string.Empty : Header);
	}

	public bool IsMatchSuccessful(Match m)
	{
		return MatchType == m.Success;
	}
}
