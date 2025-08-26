using System.Collections;
using System.Text;
using System.Web.Util;

namespace System.Web.Compilation;

internal sealed class TagAttributes
{
	private Hashtable atts_hash;

	private Hashtable tmp_hash;

	private ArrayList keys;

	private ArrayList values;

	private bool got_hashed;

	public ICollection Keys
	{
		get
		{
			if (!got_hashed)
			{
				return keys;
			}
			return atts_hash.Keys;
		}
	}

	public ICollection Values
	{
		get
		{
			if (!got_hashed)
			{
				return values;
			}
			return atts_hash.Values;
		}
	}

	public object this[object key]
	{
		get
		{
			if (got_hashed)
			{
				return atts_hash[key];
			}
			int num = CaseInsensitiveSearch((string)key);
			if (num == -1)
			{
				return null;
			}
			return values[num];
		}
		set
		{
			if (got_hashed)
			{
				CheckServerKey(key);
				atts_hash[key] = value;
			}
			else
			{
				int index = CaseInsensitiveSearch((string)key);
				keys[index] = value;
			}
		}
	}

	public int Count
	{
		get
		{
			if (!got_hashed)
			{
				return keys.Count;
			}
			return atts_hash.Count;
		}
	}

	public TagAttributes()
	{
		got_hashed = false;
		keys = new ArrayList();
		values = new ArrayList();
	}

	private void MakeHash()
	{
		atts_hash = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
		for (int i = 0; i < keys.Count; i++)
		{
			CheckServerKey(keys[i]);
			atts_hash.Add(keys[i], values[i]);
		}
		got_hashed = true;
		keys = null;
		values = null;
	}

	public bool IsRunAtServer()
	{
		return got_hashed;
	}

	public void Add(object key, object value)
	{
		if (key != null && value != null && string.Compare((string)key, "runat", ignoreCase: true, Helpers.InvariantCulture) == 0)
		{
			if (string.Compare((string)value, "server", ignoreCase: true) != 0)
			{
				throw new HttpException("runat attribute must have a 'server' value");
			}
			if (got_hashed)
			{
				return;
			}
			MakeHash();
		}
		if (value != null)
		{
			value = HttpUtility.HtmlDecode(value.ToString());
		}
		if (got_hashed)
		{
			CheckServerKey(key);
			if (atts_hash.ContainsKey(key))
			{
				throw new HttpException(string.Concat("Tag contains duplicated '", key, "' attributes."));
			}
			atts_hash.Add(key, value);
		}
		else
		{
			keys.Add(key);
			values.Add(value);
		}
	}

	private int CaseInsensitiveSearch(string key)
	{
		for (int i = 0; i < keys.Count; i++)
		{
			if (string.Compare((string)keys[i], key, ignoreCase: true, Helpers.InvariantCulture) == 0)
			{
				return i;
			}
		}
		return -1;
	}

	public bool IsDataBound(string att)
	{
		if (att == null || !got_hashed)
		{
			return false;
		}
		if (StrUtils.StartsWith(att, "<%#"))
		{
			return StrUtils.EndsWith(att, "%>");
		}
		return false;
	}

	public IDictionary GetDictionary(string key)
	{
		if (got_hashed)
		{
			return atts_hash;
		}
		if (tmp_hash == null)
		{
			tmp_hash = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
		}
		tmp_hash.Clear();
		for (int num = keys.Count - 1; num >= 0; num--)
		{
			if (key == null || string.Compare(key, (string)keys[num], ignoreCase: true, Helpers.InvariantCulture) == 0)
			{
				tmp_hash[keys[num]] = values[num];
			}
		}
		return tmp_hash;
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("TagAttributes {");
		foreach (string key in Keys)
		{
			stringBuilder.Append('[');
			stringBuilder.Append(key);
			if (this[key] is string arg)
			{
				stringBuilder.AppendFormat("=\"{0}\"", arg);
			}
			stringBuilder.Append("] ");
		}
		if (stringBuilder.Length > 0 && stringBuilder[stringBuilder.Length - 1] == ' ')
		{
			stringBuilder.Length--;
		}
		stringBuilder.Append('}');
		if (IsRunAtServer())
		{
			stringBuilder.Append(" @Server");
		}
		return stringBuilder.ToString();
	}

	private void CheckServerKey(object key)
	{
		if (key == null || ((string)key).Length == 0)
		{
			throw new HttpException("The server tag is not well formed.");
		}
	}
}
