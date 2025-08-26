using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Encapsulates the font properties of text. This class cannot be inherited.</summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class FontInfo
{
	private static string[] empty_names = new string[0];

	private StateBag bag;

	private Style _owner;

	/// <summary>Gets or sets a value that indicates whether the font is bold.</summary>
	/// <returns>
	///     <see langword="true" /> if the font is bold; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	[DefaultValue(false)]
	[NotifyParentProperty(true)]
	[WebSysDescription("")]
	[WebCategory("Font")]
	public bool Bold
	{
		get
		{
			if (!_owner.CheckBit(2048))
			{
				return false;
			}
			return bag.GetBool("Font_Bold", def: false);
		}
		set
		{
			bag["Font_Bold"] = value;
			_owner.SetBit(2048);
		}
	}

	/// <summary>Gets or sets a value that indicates whether the font is italic.</summary>
	/// <returns>
	///     <see langword="true" /> if the font is italic; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	[DefaultValue(false)]
	[NotifyParentProperty(true)]
	[WebSysDescription("")]
	[WebCategory("Font")]
	public bool Italic
	{
		get
		{
			if (!_owner.CheckBit(4096))
			{
				return false;
			}
			return bag.GetBool("Font_Italic", def: false);
		}
		set
		{
			bag["Font_Italic"] = value;
			_owner.SetBit(4096);
		}
	}

	/// <summary>Gets or sets the primary font name.</summary>
	/// <returns>The primary font name. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
	/// <exception cref="T:System.ArgumentNullException">The specified font name is null. </exception>
	[RefreshProperties(RefreshProperties.Repaint)]
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Editor("System.Drawing.Design.FontNameEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[NotifyParentProperty(true)]
	[TypeConverter(typeof(FontConverter.FontNameConverter))]
	[WebSysDescription("")]
	[WebCategory("Font")]
	public string Name
	{
		get
		{
			string[] names = Names;
			if (names.Length == 0)
			{
				return string.Empty;
			}
			return names[0];
		}
		set
		{
			if (value == string.Empty)
			{
				Names = null;
				return;
			}
			if (value == null)
			{
				throw new ArgumentNullException("value", "Font name cannot be null");
			}
			Names = new string[1] { value };
		}
	}

	/// <summary>Gets or sets an ordered array of font names.</summary>
	/// <returns>An ordered array of font names.</returns>
	[RefreshProperties(RefreshProperties.Repaint)]
	[Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[NotifyParentProperty(true)]
	[TypeConverter(typeof(FontNamesConverter))]
	[WebSysDescription("")]
	[WebCategory("Font")]
	public string[] Names
	{
		get
		{
			if (!_owner.CheckBit(512))
			{
				return empty_names;
			}
			string[] array = (string[])bag["Font_Names"];
			if (array != null)
			{
				return array;
			}
			return empty_names;
		}
		set
		{
			if (value == null)
			{
				bag.Remove("Font_Names");
				_owner.RemoveBit(512);
			}
			else
			{
				bag["Font_Names"] = value;
				_owner.SetBit(512);
			}
		}
	}

	/// <summary>Gets or sets a value that indicates whether the font is overlined.</summary>
	/// <returns>
	///     <see langword="true" /> if the font is overlined; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	[DefaultValue(false)]
	[NotifyParentProperty(true)]
	[WebSysDescription("")]
	[WebCategory("Font")]
	public bool Overline
	{
		get
		{
			if (!_owner.CheckBit(16384))
			{
				return false;
			}
			return bag.GetBool("Font_Overline", def: false);
		}
		set
		{
			bag["Font_Overline"] = value;
			_owner.SetBit(16384);
		}
	}

	/// <summary>Gets or sets the font size.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.FontUnit" /> that represents the font size.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified font size is negative. </exception>
	[RefreshProperties(RefreshProperties.Repaint)]
	[DefaultValue(typeof(FontUnit), "")]
	[NotifyParentProperty(true)]
	[WebSysDescription("")]
	[WebCategory("Font")]
	public FontUnit Size
	{
		get
		{
			if (!_owner.CheckBit(1024))
			{
				return FontUnit.Empty;
			}
			return (FontUnit)bag["Font_Size"];
		}
		set
		{
			if (value.Unit.Value < 0.0)
			{
				throw new ArgumentOutOfRangeException("Value", value.Unit.Value, "Font size cannot be negative");
			}
			bag["Font_Size"] = value;
			_owner.SetBit(1024);
		}
	}

	/// <summary>Gets or sets a value that indicates whether the font is strikethrough.</summary>
	/// <returns>
	///     <see langword="true" /> if the font is struck through; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	[DefaultValue(false)]
	[NotifyParentProperty(true)]
	[WebSysDescription("")]
	[WebCategory("Font")]
	public bool Strikeout
	{
		get
		{
			if (!_owner.CheckBit(32768))
			{
				return false;
			}
			return bag.GetBool("Font_Strikeout", def: false);
		}
		set
		{
			bag["Font_Strikeout"] = value;
			_owner.SetBit(32768);
		}
	}

	/// <summary>Gets or sets a value that indicates whether the font is underlined.</summary>
	/// <returns>
	///     <see langword="true" /> if the font is underlined; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	[DefaultValue(false)]
	[NotifyParentProperty(true)]
	[WebSysDescription("")]
	[WebCategory("Font")]
	public bool Underline
	{
		get
		{
			if (!_owner.CheckBit(8192))
			{
				return false;
			}
			return bag.GetBool("Font_Underline", def: false);
		}
		set
		{
			bag["Font_Underline"] = value;
			_owner.SetBit(8192);
		}
	}

	private bool IsEmpty => !_owner.CheckBit(65024);

	internal FontInfo(Style owner)
	{
		_owner = owner;
		bag = owner.ViewState;
	}

	/// <summary>Duplicates the font properties of the specified <see cref="T:System.Web.UI.WebControls.FontInfo" /> into the instance of the <see cref="T:System.Web.UI.WebControls.FontInfo" /> class that this method is called from.</summary>
	/// <param name="f">A <see cref="T:System.Web.UI.WebControls.FontInfo" /> that contains the font properties to duplicate. </param>
	public void CopyFrom(FontInfo f)
	{
		if (f != null && !f.IsEmpty && f != this)
		{
			if (f._owner.CheckBit(2048))
			{
				Bold = f.Bold;
			}
			if (f._owner.CheckBit(4096))
			{
				Italic = f.Italic;
			}
			Names = f.Names;
			if (f._owner.CheckBit(16384))
			{
				Overline = f.Overline;
			}
			if (f._owner.CheckBit(1024))
			{
				Size = f.Size;
			}
			if (f._owner.CheckBit(32768))
			{
				Strikeout = f.Strikeout;
			}
			if (f._owner.CheckBit(8192))
			{
				Underline = f.Underline;
			}
		}
	}

	/// <summary>Combines the font properties of the specified <see cref="T:System.Web.UI.WebControls.FontInfo" /> with the instance of the <see cref="T:System.Web.UI.WebControls.FontInfo" /> class that this method is called from.</summary>
	/// <param name="f">A <see cref="T:System.Web.UI.WebControls.FontInfo" /> that contains the font properties to combine. </param>
	public void MergeWith(FontInfo f)
	{
		if (!_owner.CheckBit(2048) && f._owner.CheckBit(2048))
		{
			Bold = f.Bold;
		}
		if (!_owner.CheckBit(4096) && f._owner.CheckBit(4096))
		{
			Italic = f.Italic;
		}
		if (!_owner.CheckBit(512) && f._owner.CheckBit(512))
		{
			Names = f.Names;
		}
		if (!_owner.CheckBit(16384) && f._owner.CheckBit(16384))
		{
			Overline = f.Overline;
		}
		if (!_owner.CheckBit(1024) && f._owner.CheckBit(1024))
		{
			Size = f.Size;
		}
		if (!_owner.CheckBit(32768) && f._owner.CheckBit(32768))
		{
			Strikeout = f.Strikeout;
		}
		if (!_owner.CheckBit(8192) && f._owner.CheckBit(8192))
		{
			Underline = f.Underline;
		}
	}

	/// <summary>Determines whether the <see cref="P:System.Web.UI.WebControls.FontInfo.Names" /> property should be persisted.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="P:System.Web.UI.WebControls.FontInfo.Names" /> property has changed from its default value; otherwise, <see langword="false" />.</returns>
	public bool ShouldSerializeNames()
	{
		return Names.Length != 0;
	}

	/// <summary>Returns a string that contains the font name and size for an instance of the <see cref="T:System.Web.UI.WebControls.FontInfo" /> class.</summary>
	/// <returns>A string that contains the font name and size for an instance of the <see cref="T:System.Web.UI.WebControls.FontInfo" /> class.</returns>
	public override string ToString()
	{
		if (Names.Length == 0)
		{
			return Size.ToString();
		}
		return Name + ", " + Size.ToString();
	}

	/// <summary>Resets all <see cref="T:System.Web.UI.WebControls.FontInfo" /> properties to the unset state and clears the view state.</summary>
	public void ClearDefaults()
	{
		Reset();
	}

	internal void Reset()
	{
		bag.Remove("Font_Bold");
		bag.Remove("Font_Italic");
		bag.Remove("Font_Names");
		bag.Remove("Font_Overline");
		bag.Remove("Font_Size");
		bag.Remove("Font_Strikeout");
		bag.Remove("Font_Underline");
		_owner.RemoveBit(65024);
	}

	internal void FillStyleAttributes(CssStyleCollection attributes, bool alwaysRenderTextDecoration)
	{
		if (IsEmpty)
		{
			if (alwaysRenderTextDecoration)
			{
				attributes.Add(HtmlTextWriterStyle.TextDecoration, "none");
			}
			return;
		}
		string text = string.Join(",", Names);
		if (text.Length > 0)
		{
			attributes.Add(HtmlTextWriterStyle.FontFamily, text);
		}
		if (_owner.CheckBit(2048))
		{
			attributes.Add(HtmlTextWriterStyle.FontWeight, Bold ? "bold" : "normal");
		}
		if (_owner.CheckBit(4096))
		{
			attributes.Add(HtmlTextWriterStyle.FontStyle, Italic ? "italic" : "normal");
		}
		if (!Size.IsEmpty)
		{
			attributes.Add(HtmlTextWriterStyle.FontSize, Size.ToString());
		}
		text = string.Empty;
		bool flag = false;
		if (_owner.CheckBit(16384))
		{
			if (Overline)
			{
				text += "overline ";
			}
			flag = true;
		}
		if (_owner.CheckBit(32768))
		{
			if (Strikeout)
			{
				text += "line-through ";
			}
			flag = true;
		}
		if (_owner.CheckBit(8192))
		{
			if (Underline)
			{
				text += "underline ";
			}
			flag = true;
		}
		text = ((text.Length > 0) ? text.Trim() : ((alwaysRenderTextDecoration || flag) ? "none" : string.Empty));
		if (text.Length > 0)
		{
			attributes.Add(HtmlTextWriterStyle.TextDecoration, text);
		}
	}
}
