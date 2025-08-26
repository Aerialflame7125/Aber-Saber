using System.Collections;
using System.Collections.Specialized;
using System.Security.Permissions;
using System.Text;

namespace System.Web.UI;

/// <summary>Contains the HTML cascading-style sheets (CSS) inline style attributes for a specified HTML server control. This class cannot be inherited.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class CssStyleCollection
{
	private StateBag bag;

	private ListDictionary style;

	private StringBuilder _value = new StringBuilder();

	/// <summary>Gets the number of items in the <see cref="T:System.Web.UI.CssStyleCollection" /> object.</summary>
	/// <returns>The number of items in the <see cref="T:System.Web.UI.CssStyleCollection" /> object.</returns>
	public int Count => style.Count;

	/// <summary>Gets or sets the specified CSS value for the HTML server control.</summary>
	/// <param name="key">The index to the CSS attribute. </param>
	/// <returns>The value of <paramref name="key" />.</returns>
	public string this[string key]
	{
		get
		{
			return style[key] as string;
		}
		set
		{
			Add(key, value);
		}
	}

	/// <summary>Gets a collection of keys to all the styles in the <see cref="T:System.Web.UI.CssStyleCollection" /> object for a specific HTML server control.</summary>
	/// <returns>A collection of keys contained in the <see cref="T:System.Web.UI.CssStyleCollection" /> for the specified HTML server control.</returns>
	public ICollection Keys => style.Keys;

	/// <summary>Gets or sets the specified <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> value for the HTML server control.</summary>
	/// <param name="key">An <see cref="T:System.Web.UI.HtmlTextWriterStyle" />.</param>
	/// <returns>The value <paramref name="key" />; otherwise, <see langword="null" />, if <paramref name="key" /> is not in the server control's collection.</returns>
	public string this[HtmlTextWriterStyle key]
	{
		get
		{
			return style[HtmlTextWriter.StaticGetStyleName(key)] as string;
		}
		set
		{
			Add(HtmlTextWriter.StaticGetStyleName(key), value);
		}
	}

	/// <summary>Gets or sets the value of the <see langword="style" /> attribute of the HTML server control.</summary>
	/// <returns>The style string literal.</returns>
	public string Value
	{
		get
		{
			return _value.ToString();
		}
		set
		{
			SetValueInternal(value);
			InitFromStyle();
		}
	}

	internal CssStyleCollection()
	{
		style = new ListDictionary(StringComparer.OrdinalIgnoreCase);
	}

	internal CssStyleCollection(StateBag bag)
		: this()
	{
		this.bag = bag;
		if (bag != null && bag["style"] != null)
		{
			_value.Append(bag["style"]);
		}
		InitFromStyle();
	}

	private void InitFromStyle()
	{
		style.Clear();
		if (_value.Length > 0)
		{
			for (int num = 0; num >= 0; num = ParseStyle(num))
			{
			}
		}
	}

	private int ParseStyle(int startIndex)
	{
		int num = -1;
		for (int i = startIndex; i < _value.Length; i++)
		{
			if (_value[i] == ':')
			{
				num = i;
				break;
			}
		}
		if (num == -1 || num + 1 == _value.Length)
		{
			return -1;
		}
		string key = _value.ToString(startIndex, num - startIndex).Trim();
		int num2 = -1;
		for (int j = num + 1; j < _value.Length; j++)
		{
			if (_value[j] == ';')
			{
				num2 = j;
				break;
			}
		}
		string value = ((num2 != -1) ? _value.ToString(num + 1, num2 - num - 1).Trim() : _value.ToString(num + 1, _value.Length - num - 1).Trim());
		style.Add(key, value);
		if (num2 == -1 || num2 + 1 == _value.Length)
		{
			return -1;
		}
		return num2 + 1;
	}

	private void BagToValue()
	{
		_value.Length = 0;
		foreach (string key in style.Keys)
		{
			AppendStyle(_value, key, (string)style[key]);
		}
	}

	private static void AppendStyle(StringBuilder sb, string key, string value)
	{
		if (string.Compare(key, "background-image", StringComparison.OrdinalIgnoreCase) == 0 && value.Length >= 3 && string.Compare("url", 0, value, 0, 3, StringComparison.OrdinalIgnoreCase) != 0)
		{
			sb.AppendFormat("{0}:url({1});", key, HttpUtility.UrlPathEncode(value));
		}
		else
		{
			sb.AppendFormat("{0}:{1};", key, value);
		}
	}

	/// <summary>Adds a style item to the <see cref="T:System.Web.UI.CssStyleCollection" /> of a control using the specified name/value pair.</summary>
	/// <param name="key">The name of the new style attribute to add to the collection. </param>
	/// <param name="value">The value of the style attribute to add to the collection. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="key" /> is <see langword="null" />.</exception>
	public void Add(string key, string value)
	{
		if (key == null)
		{
			throw new ArgumentNullException("key");
		}
		if (value == null)
		{
			Remove(key);
			return;
		}
		string text = (string)style[key];
		if (text == null)
		{
			style[key] = value;
			AppendStyle(_value, key, value);
		}
		else
		{
			if (string.CompareOrdinal(text, value) == 0)
			{
				return;
			}
			style[key] = value;
			BagToValue();
		}
		if (bag != null)
		{
			bag["style"] = _value.ToString();
		}
	}

	/// <summary>Adds a style item to the <see cref="T:System.Web.UI.CssStyleCollection" /> collection of a control using the specified <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> enumeration value and corresponding value.</summary>
	/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> enumeration value to add to the collection.</param>
	/// <param name="value">The value of the style attribute to add to the collection.</param>
	public void Add(HtmlTextWriterStyle key, string value)
	{
		Add(HtmlTextWriter.StaticGetStyleName(key), value);
	}

	/// <summary>Removes all style items from the <see cref="T:System.Web.UI.CssStyleCollection" /> object.</summary>
	public void Clear()
	{
		style.Clear();
		SetValueInternal(null);
	}

	/// <summary>Removes a style item from the <see cref="T:System.Web.UI.CssStyleCollection" /> of a control using the specified style key.</summary>
	/// <param name="key">The string literal of the style item to remove. </param>
	public void Remove(string key)
	{
		if (style[key] != null)
		{
			style.Remove(key);
			if (style.Count == 0)
			{
				SetValueInternal(null);
			}
			else
			{
				BagToValue();
			}
		}
	}

	/// <summary>Removes a style item from the <see cref="T:System.Web.UI.CssStyleCollection" /> collection of a control using the specified <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> enumeration value.</summary>
	/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> enumeration value to remove.</param>
	public void Remove(HtmlTextWriterStyle key)
	{
		Remove(HtmlTextWriter.StaticGetStyleName(key));
	}

	private void SetValueInternal(string value)
	{
		_value.Length = 0;
		if (value != null)
		{
			_value.Append(value);
		}
		if (bag != null)
		{
			if (value == null)
			{
				bag.Remove("style");
			}
			else
			{
				bag["style"] = value;
			}
		}
	}
}
