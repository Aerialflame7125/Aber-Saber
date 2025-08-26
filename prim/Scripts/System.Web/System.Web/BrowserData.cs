using System.Collections;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Web.Util;

namespace System.Web;

internal sealed class BrowserData
{
	private static char[] wildchars = new char[2] { '*', '?' };

	private object this_lock = new object();

	private BrowserData parent;

	private string text;

	private string pattern;

	private Regex regex;

	private ListDictionary data;

	public BrowserData Parent
	{
		get
		{
			return parent;
		}
		set
		{
			parent = value;
		}
	}

	public BrowserData(string pattern)
	{
		int num = pattern.IndexOfAny(wildchars);
		if (num == -1)
		{
			text = pattern;
			return;
		}
		this.pattern = pattern.Substring(num);
		text = pattern.Substring(0, num);
		if (text.Length == 0)
		{
			text = null;
		}
		this.pattern = this.pattern.Replace(".", "\\.");
		this.pattern = this.pattern.Replace("(", "\\(");
		this.pattern = this.pattern.Replace(")", "\\)");
		this.pattern = this.pattern.Replace("[", "\\[");
		this.pattern = this.pattern.Replace("]", "\\]");
		this.pattern = this.pattern.Replace('?', '.');
		this.pattern = this.pattern.Replace("*", ".*");
	}

	public void Add(string key, string value)
	{
		if (data == null)
		{
			data = new ListDictionary();
		}
		data.Add(key, value);
	}

	public Hashtable GetProperties(Hashtable tbl)
	{
		if (parent != null)
		{
			parent.GetProperties(tbl);
		}
		if (data["browser"] != null)
		{
			tbl["browser"] = data["browser"];
		}
		else if (tbl["browser"] == null)
		{
			tbl["browser"] = "*";
		}
		if (!tbl.ContainsKey("browsers"))
		{
			tbl["browsers"] = new ArrayList();
		}
		((ArrayList)tbl["browsers"]).Add(tbl["browser"]);
		foreach (string key in data.Keys)
		{
			tbl[key.ToLower(Helpers.InvariantCulture).Trim()] = data[key];
		}
		return tbl;
	}

	public string GetParentName()
	{
		return (string)(data.Contains("parent") ? data["parent"] : null);
	}

	public string GetAlternateBrowser()
	{
		if (pattern != null)
		{
			return null;
		}
		return text;
	}

	public string GetBrowser()
	{
		if (pattern == null)
		{
			return text;
		}
		return (string)data["browser"];
	}

	public bool IsMatch(string expression)
	{
		if (expression == null || expression.Length == 0)
		{
			return false;
		}
		if (text != null)
		{
			if (text[0] != expression[0] || string.Compare(text, 1, expression, 1, text.Length - 1, ignoreCase: false, Helpers.InvariantCulture) != 0)
			{
				return false;
			}
			expression = expression.Substring(text.Length);
		}
		if (pattern == null)
		{
			return expression.Length == 0;
		}
		lock (this_lock)
		{
			if (regex == null)
			{
				regex = new Regex(pattern);
			}
		}
		return regex.Match(expression).Success;
	}
}
