using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls;

/// <summary>Defines the methods, properties, and events common to all HTML server controls in the ASP.NET page framework.</summary>
[ToolboxItem(false)]
[Designer("System.Web.UI.Design.HtmlIntrinsicControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public abstract class HtmlControl : Control, IAttributeAccessor
{
	internal string _tagName;

	private AttributeCollection _attributes;

	/// <summary>Gets a collection of all attribute name and value pairs expressed on a server control tag within the ASP.NET page.</summary>
	/// <returns>A <see cref="T:System.Web.UI.AttributeCollection" /> object that contains all attribute name and value pairs expressed on a server control tag within the Web page.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public AttributeCollection Attributes
	{
		get
		{
			if (_attributes == null)
			{
				_attributes = new AttributeCollection(ViewState);
			}
			return _attributes;
		}
	}

	/// <summary>Gets or sets a value indicating whether the HTML server control is disabled.</summary>
	/// <returns>
	///     <see langword="true" /> if the control is disabled; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	[DefaultValue(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	[TypeConverter(typeof(MinimizableAttributeTypeConverter))]
	public bool Disabled
	{
		get
		{
			return Attributes["disabled"] != null;
		}
		set
		{
			if (!value)
			{
				Attributes.Remove("disabled");
			}
			else
			{
				Attributes["disabled"] = "disabled";
			}
		}
	}

	/// <summary>Gets a collection of all cascading style sheet (CSS) properties applied to a specified HTML server control in the ASP.NET file.</summary>
	/// <returns>A <see cref="T:System.Web.UI.CssStyleCollection" /> object that contains the style properties for the HTML server control.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public CssStyleCollection Style => Attributes.CssStyle;

	/// <summary>Gets the element name of a tag that contains a <see langword="runat=server" /> attribute and value pair.</summary>
	/// <returns>The element name of the specified tag.</returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual string TagName => _tagName;

	/// <summary>Gets a value that indicates whether the <see cref="T:System.Web.UI.HtmlControls.HtmlControl" /> view state is case-sensitive.</summary>
	/// <returns>
	///     <see langword="true" /> in all cases.</returns>
	protected override bool ViewStateIgnoresCase => true;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlControl" /> class using default values.</summary>
	protected HtmlControl()
		: this("span")
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlControl" /> class using the specified tag.</summary>
	/// <param name="tag">A string that specifies the tag name of the control. </param>
	protected HtmlControl(string tag)
	{
		_tagName = tag;
	}

	/// <summary>Creates a new <see cref="T:System.Web.UI.ControlCollection" /> object to hold the child controls (both literal and server) of the server control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> object to contain the current server control's child server controls.</returns>
	protected override ControlCollection CreateControlCollection()
	{
		return new EmptyControlCollection(this);
	}

	internal static string AttributeToString(int n)
	{
		if (n != -1)
		{
			return n.ToString(NumberFormatInfo.InvariantInfo);
		}
		return null;
	}

	internal static string AttributeToString(string s)
	{
		if (s != null && s.Length != 0)
		{
			return s;
		}
		return null;
	}

	internal void PreProcessRelativeReference(HtmlTextWriter writer, string attribName)
	{
		string text = Attributes[attribName];
		if (text != null && text.Length != 0)
		{
			try
			{
				text = ResolveClientUrl(text);
			}
			catch (Exception)
			{
				throw new HttpException(attribName + " property had malformed url");
			}
			writer.WriteAttribute(attribName, text);
			Attributes.Remove(attribName);
		}
	}

	/// <summary>Gets the value of the named attribute on the <see cref="T:System.Web.UI.HtmlControls.HtmlControl" /> control.</summary>
	/// <param name="name">The name of the attribute. This argument is case-insensitive.</param>
	/// <returns>The value of this attribute on the element, as a <see cref="T:System.String" /> value. If the specified attribute does not exist on this element, returns an empty string ("").</returns>
	protected virtual string GetAttribute(string name)
	{
		return Attributes[name];
	}

	/// <summary>Sets the value of the named attribute on the <see cref="T:System.Web.UI.HtmlControls.HtmlControl" /> control.</summary>
	/// <param name="name">The name of the attribute to set.</param>
	/// <param name="value">The value to set the attribute to.</param>
	protected virtual void SetAttribute(string name, string value)
	{
		Attributes[name] = value;
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IAttributeAccessor.GetAttribute(System.String)" />. </summary>
	/// <param name="name">The attribute name.</param>
	/// <returns>The value of this attribute on the element, as a <see cref="T:System.String" /> value. If the specified attribute does not exist on this element, returns an empty string ("").</returns>
	string IAttributeAccessor.GetAttribute(string name)
	{
		return Attributes[name];
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IAttributeAccessor.SetAttribute(System.String,System.String)" />. </summary>
	/// <param name="name">The name of the attribute to set.</param>
	/// <param name="value">The value to set the attribute to.</param>
	void IAttributeAccessor.SetAttribute(string name, string value)
	{
		Attributes[name] = value;
	}

	/// <summary>Renders the opening HTML tag of the control into the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content.</param>
	protected virtual void RenderBeginTag(HtmlTextWriter writer)
	{
		writer.WriteBeginTag(TagName);
		RenderAttributes(writer);
		writer.Write('>');
	}

	/// <summary>Writes content to render on a client to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
	/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client.</param>
	protected internal override void Render(HtmlTextWriter writer)
	{
		RenderBeginTag(writer);
	}

	/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlControl" /> control's attributes into the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content.</param>
	protected virtual void RenderAttributes(HtmlTextWriter writer)
	{
		if (ID != null)
		{
			writer.WriteAttribute("id", ClientID);
		}
		Attributes.Render(writer);
	}
}
