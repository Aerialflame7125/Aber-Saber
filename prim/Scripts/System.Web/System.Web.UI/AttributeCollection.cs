using System.Collections;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI;

/// <summary>Provides object-model access to all attributes declared in the opening tag of an ASP.NET server control element. This class cannot be inherited.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class AttributeCollection
{
	private StateBag bag;

	private CssStyleCollection styleCollection;

	internal const string StyleAttribute = "style";

	/// <summary>Gets the number of attributes in the <see cref="T:System.Web.UI.AttributeCollection" /> object.</summary>
	/// <returns>The number of items in the collection.</returns>
	public int Count => bag.Count;

	/// <summary>Gets a collection of styles for the ASP.NET server control to which the current <see cref="T:System.Web.UI.AttributeCollection" /> object belongs.</summary>
	/// <returns>A collection that contains the styles for the current server control.</returns>
	public CssStyleCollection CssStyle
	{
		get
		{
			if (styleCollection == null)
			{
				styleCollection = new CssStyleCollection(bag);
			}
			return styleCollection;
		}
	}

	/// <summary>Gets or sets a specified attribute value for a server control.</summary>
	/// <param name="key">The location of the attribute in the collection. </param>
	/// <returns>The attribute value.</returns>
	public string this[string key]
	{
		get
		{
			return bag[key] as string;
		}
		set
		{
			Add(key, value);
		}
	}

	/// <summary>Gets a collection of keys to all attributes in the server control's <see cref="T:System.Web.UI.AttributeCollection" /> object.</summary>
	/// <returns>The collection of keys.</returns>
	public ICollection Keys => bag.Keys;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.AttributeCollection" /> class.</summary>
	/// <param name="bag">An object that contains the attribute keys and their values from the opening tag of the server control. </param>
	public AttributeCollection(StateBag bag)
	{
		this.bag = bag;
	}

	/// <summary>Determines whether the current instance of the <see cref="T:System.Web.UI.AttributeCollection" /> object is equal to the specified object.</summary>
	/// <param name="o">The object instance to compare with this instance.</param>
	/// <returns>
	///     <see langword="true" /> if the object that is contained in the <paramref name="o" /> parameter is equal to the current instance of <see cref="T:System.Web.UI.AttributeCollection" />; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object o)
	{
		if (!(o is AttributeCollection attributeCollection))
		{
			return false;
		}
		if (Count != attributeCollection.Count)
		{
			return false;
		}
		foreach (string key2 in Keys)
		{
			if (string.CompareOrdinal(key2, "style") != 0 && string.CompareOrdinal(attributeCollection[key2], this[key2]) == 0)
			{
				return false;
			}
		}
		if ((styleCollection == null && attributeCollection.styleCollection != null) || (styleCollection != null && attributeCollection.styleCollection == null))
		{
			return false;
		}
		if (styleCollection != null)
		{
			if (styleCollection.Count != attributeCollection.styleCollection.Count)
			{
				return false;
			}
			foreach (string key3 in styleCollection.Keys)
			{
				if (string.CompareOrdinal(styleCollection[key3], attributeCollection.styleCollection[key3]) == 0)
				{
					return false;
				}
			}
		}
		return true;
	}

	/// <summary>Returns the hash code for this instance.</summary>
	/// <returns>A 32-bit signed integer hash code.</returns>
	public override int GetHashCode()
	{
		int num = 0;
		foreach (string key2 in Keys)
		{
			if (!(key2 == "style"))
			{
				num ^= key2.GetHashCode();
				string text2 = this[key2];
				if (text2 != null)
				{
					num ^= text2.GetHashCode();
				}
			}
		}
		if (styleCollection != null)
		{
			foreach (string key3 in styleCollection.Keys)
			{
				num ^= styleCollection[key3].GetHashCode();
				string text3 = styleCollection[key3];
				if (text3 != null)
				{
					num ^= text3.GetHashCode();
				}
			}
		}
		return num;
	}

	/// <summary>Adds an attribute to a server control's <see cref="T:System.Web.UI.AttributeCollection" /> object.</summary>
	/// <param name="key">The attribute name. </param>
	/// <param name="value">The attribute value. </param>
	public void Add(string key, string value)
	{
		if (string.Compare(key, "style", ignoreCase: true, Helpers.InvariantCulture) == 0)
		{
			CssStyle.Value = value;
		}
		else
		{
			bag.Add(key, value);
		}
	}

	/// <summary>Adds attributes from the <see cref="T:System.Web.UI.AttributeCollection" /> class to the <see cref="T:System.Web.UI.HtmlTextWriter" /> object that is responsible for rendering the attributes as markup.</summary>
	/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> instance that writes the attribute to the opening tag of an ASP.NET server control. </param>
	public void AddAttributes(HtmlTextWriter writer)
	{
		foreach (string key in bag.Keys)
		{
			string value = bag[key] as string;
			writer.AddAttribute(key, value);
		}
	}

	/// <summary>Removes all attributes from a server control's <see cref="T:System.Web.UI.AttributeCollection" /> object.</summary>
	public void Clear()
	{
		CssStyle.Clear();
		bag.Clear();
	}

	/// <summary>Removes an attribute from a server control's <see cref="T:System.Web.UI.AttributeCollection" /> object.</summary>
	/// <param name="key">The name of the attribute to remove. </param>
	public void Remove(string key)
	{
		if (string.Compare(key, "style", ignoreCase: true, Helpers.InvariantCulture) == 0)
		{
			CssStyle.Clear();
		}
		else
		{
			bag.Remove(key);
		}
	}

	/// <summary>Writes the collection of attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> output stream for the control to which the collection belongs.</summary>
	/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> instance that writes the attribute collection to the current output stream. </param>
	public void Render(HtmlTextWriter writer)
	{
		foreach (string key in bag.Keys)
		{
			if (bag[key] is string value)
			{
				writer.WriteAttribute(key, value, fEncode: true);
			}
		}
	}

	internal void CopyFrom(AttributeCollection attributeCollection)
	{
		if (attributeCollection == null || attributeCollection.Count == 0)
		{
			return;
		}
		foreach (string key in attributeCollection.bag.Keys)
		{
			Add(key, attributeCollection[key]);
		}
	}
}
