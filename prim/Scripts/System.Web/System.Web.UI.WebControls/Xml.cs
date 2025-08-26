using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace System.Web.UI.WebControls;

/// <summary>Displays an XML document without formatting or using Extensible Stylesheet Language Transformations (XSLT).</summary>
[DefaultProperty("DocumentSource")]
[Designer("System.Web.UI.Design.WebControls.XmlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[PersistChildren(true)]
[ControlBuilder(typeof(XmlBuilder))]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class Xml : Control
{
	private XmlDocument xml_document;

	private XPathNavigator xpath_navigator;

	private string xml_content;

	private string xml_file;

	private XslTransform xsl_transform;

	private XsltArgumentList transform_arguments;

	private string transform_file;

	/// <summary>Overrides the <see cref="P:System.Web.UI.Control.ClientID" /> property and returns the base server control identifier.</summary>
	/// <returns>The base server control identifier.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[MonoTODO("Anything else?")]
	public override string ClientID => base.ClientID;

	/// <summary>Overrides the <see cref="P:System.Web.UI.Control.Controls" /> property and returns the base <see cref="T:System.Web.UI.ControlCollection" /> collection.</summary>
	/// <returns>The base <see cref="T:System.Web.UI.ControlCollection" /> collection.</returns>
	[MonoTODO("Anything else?")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override ControlCollection Controls => base.Controls;

	/// <summary>Gets or sets the <see cref="T:System.Xml.XmlDocument" /> to display in the <see cref="T:System.Web.UI.WebControls.Xml" /> control.</summary>
	/// <returns>The <see cref="T:System.Xml.XmlDocument" /> to display in the <see cref="T:System.Web.UI.WebControls.Xml" /> control.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Obsolete("Use the XPathNavigator property instead by creating an XPathDocument and calling CreateNavigator().")]
	public XmlDocument Document
	{
		get
		{
			return xml_document;
		}
		set
		{
			xml_content = null;
			xml_file = null;
			xml_document = value;
		}
	}

	/// <summary>Sets a string that contains the XML document to display in the <see cref="T:System.Web.UI.WebControls.Xml" /> control.</summary>
	/// <returns>A string that contains the XML document to display in the <see cref="T:System.Web.UI.WebControls.Xml" /> control.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public string DocumentContent
	{
		get
		{
			if (xml_content == null)
			{
				return "";
			}
			return xml_content;
		}
		set
		{
			xml_content = value;
			xml_file = null;
			xml_document = null;
		}
	}

	/// <summary>Gets or sets the path to an XML document to display in the <see cref="T:System.Web.UI.WebControls.Xml" /> control.</summary>
	/// <returns>The path to an XML document to display in the <see cref="T:System.Web.UI.WebControls.Xml" /> control.</returns>
	[DefaultValue("")]
	[UrlProperty]
	[Editor("System.Web.UI.Design.XmlUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	[MonoLimitation("Absolute path to the file system is not supported; use a relative URI instead.")]
	public string DocumentSource
	{
		get
		{
			if (xml_file == null)
			{
				return "";
			}
			return xml_file;
		}
		set
		{
			xml_content = null;
			xml_file = value;
			xml_document = null;
		}
	}

	/// <summary>Overrides the <see cref="P:System.Web.UI.Control.EnableTheming" /> property. This property is not supported by the <see cref="T:System.Web.UI.WebControls.Xml" /> class.</summary>
	/// <returns>Always returns <see langword="false" />. This property is not supported.</returns>
	/// <exception cref="T:System.NotSupportedException">An attempt is made to set the value of this property.</exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DefaultValue(false)]
	public override bool EnableTheming
	{
		get
		{
			return false;
		}
		set
		{
			throw new NotSupportedException();
		}
	}

	/// <summary>Overrides the <see cref="P:System.Web.UI.Control.SkinID" /> property. This property is not supported by the <see cref="T:System.Web.UI.WebControls.Xml" /> class.</summary>
	/// <returns>Always returns an empty string (""). This property is not supported.</returns>
	/// <exception cref="T:System.NotSupportedException">An attempt is made to set the value of this property.</exception>
	[DefaultValue("")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public override string SkinID
	{
		get
		{
			return string.Empty;
		}
		set
		{
			throw new NotSupportedException("SkinID is not supported on Xml control");
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Xml.Xsl.XslTransform" /> object that formats the XML document before it is written to the output stream.</summary>
	/// <returns>The <see cref="T:System.Xml.Xsl.XslTransform" /> that formats the XML document before it is written to the output stream.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public XslTransform Transform
	{
		get
		{
			return xsl_transform;
		}
		set
		{
			transform_file = null;
			xsl_transform = value;
		}
	}

	/// <summary>Gets or sets a <see cref="T:System.Xml.Xsl.XsltArgumentList" /> that contains a list of optional arguments passed to the style sheet and used during the Extensible Stylesheet Language Transformation (XSLT).</summary>
	/// <returns>A <see cref="T:System.Xml.Xsl.XsltArgumentList" /> that contains a list of optional arguments passed to the style sheet and used during the XSL Transformation.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public XsltArgumentList TransformArgumentList
	{
		get
		{
			return transform_arguments;
		}
		set
		{
			transform_arguments = value;
		}
	}

	/// <summary>Gets or sets the path to an Extensible Stylesheet Language Transformation (XSLT) style sheet that formats the XML document before it is written to the output stream.</summary>
	/// <returns>The path to an XSL Transformation style sheet that formats the XML document before it is written to the output stream.</returns>
	[DefaultValue("")]
	[Editor("System.Web.UI.Design.XslUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[MonoLimitation("Absolute path to the file system is not supported; use a relative URI instead.")]
	public string TransformSource
	{
		get
		{
			if (transform_file == null)
			{
				return "";
			}
			return transform_file;
		}
		set
		{
			transform_file = value;
			xsl_transform = null;
		}
	}

	/// <summary>Gets or sets a cursor model for navigating and editing the XML data associated with the <see cref="T:System.Web.UI.WebControls.Xml" /> control.</summary>
	/// <returns>An <see cref="T:System.Xml.XPath.XPathNavigator" /> object.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public XPathNavigator XPathNavigator
	{
		get
		{
			return xpath_navigator;
		}
		set
		{
			xpath_navigator = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Xml" /> class.</summary>
	public Xml()
	{
	}

	/// <summary>Searches the page naming container for the specified server control.</summary>
	/// <param name="id">The identifier for the control to be found.</param>
	/// <returns>The specified control; otherwise, <see langword="null" /> if the specified control does not exist.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override Control FindControl(string id)
	{
		return null;
	}

	/// <summary>Overrides the <see cref="M:System.Web.UI.Control.Focus" /> method. This method is not supported by the <see cref="T:System.Web.UI.WebControls.Xml" /> class.</summary>
	/// <exception cref="T:System.NotSupportedException">An attempt is made to invoke this method.</exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override void Focus()
	{
		throw new NotSupportedException();
	}

	/// <summary>Determines whether the server control contains any child controls.</summary>
	/// <returns>
	///     <see langword="true" /> if the control contains other controls; otherwise, <see langword="false" />.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override bool HasControls()
	{
		return false;
	}

	/// <summary>Renders the results to the output stream.</summary>
	/// <param name="output">The result of the output stream.</param>
	protected internal override void Render(HtmlTextWriter output)
	{
		XmlDocument xmlDocument = null;
		if (xpath_navigator == null)
		{
			if (xml_document != null)
			{
				xmlDocument = xml_document;
			}
			else if (xml_content != null)
			{
				xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(xml_content);
			}
			else
			{
				if (xml_file == null)
				{
					return;
				}
				xmlDocument = new XmlDocument();
				xmlDocument.Load(MapPathSecure(xml_file));
			}
		}
		XslTransform xslTransform = xsl_transform;
		if (transform_file != null)
		{
			xslTransform = new XslTransform();
			xslTransform.Load(MapPathSecure(transform_file));
		}
		if (xslTransform != null)
		{
			if (xpath_navigator != null)
			{
				xslTransform.Transform(xpath_navigator, transform_arguments, output);
			}
			else
			{
				xslTransform.Transform(xmlDocument, transform_arguments, output, null);
			}
			return;
		}
		XmlTextWriter xmlTextWriter = new XmlTextWriter(output);
		xmlTextWriter.Formatting = Formatting.None;
		if (xpath_navigator != null)
		{
			xmlTextWriter.WriteStartDocument();
			xpath_navigator.WriteSubtree(xmlTextWriter);
		}
		else
		{
			xmlDocument.Save(xmlTextWriter);
		}
	}

	/// <summary>Notifies the server control that an element, either XML or HTML, was parsed, and adds the element to the server control's <see cref="T:System.Web.UI.ControlCollection" /> object.</summary>
	/// <param name="obj">An <see cref="T:System.Object" /> that represents the parsed element. </param>
	/// <exception cref="T:System.Web.HttpException">
	///         <paramref name="obj" /> is not of type <see cref="T:System.Web.UI.LiteralControl" />.</exception>
	protected override void AddParsedSubObject(object obj)
	{
		if (obj is LiteralControl literalControl)
		{
			xml_document = new XmlDocument();
			xml_document.LoadXml(literalControl.Text);
			return;
		}
		throw new HttpException($"Objects of type {obj.GetType()} are not supported as children of the Xml control");
	}

	/// <summary>Creates a new <see cref="T:System.Web.UI.EmptyControlCollection" /> object.</summary>
	/// <returns>Always returns an <see cref="T:System.Web.UI.EmptyControlCollection" />.</returns>
	protected override ControlCollection CreateControlCollection()
	{
		return new EmptyControlCollection(this);
	}

	/// <summary>Gets design-time data for a control.</summary>
	/// <returns>An <see cref="T:System.Collections.IDictionary" /> containing the design-time data for the <see cref="T:System.Web.UI.WebControls.Xml" /> control.</returns>
	[MonoTODO("Always returns null")]
	protected override IDictionary GetDesignModeState()
	{
		return null;
	}
}
