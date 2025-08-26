using System.Collections;
using System.ComponentModel;
using System.Xml;

namespace System.Web.UI;

/// <summary>Represents the base class for a page theme, which is a collection of resources that are used to define a consistent look across pages and controls in a Web site. The page theme can be set through configuration or the page directive.</summary>
[EditorBrowsable(EditorBrowsableState.Advanced)]
public abstract class PageTheme
{
	private Page _page;

	/// <summary>When overridden a derived class, gets the relative URL of the directory for the <see cref="T:System.Web.UI.PageTheme" /> object.</summary>
	/// <returns>A string value containing the relative URL of the <see cref="T:System.Web.UI.PageTheme" /> directory.</returns>
	protected abstract string AppRelativeTemplateSourceDirectory { get; }

	/// <summary>When overridden in a derived class, gets an <see cref="T:System.Collections.IDictionary" /> interface of the names of all default skins that are available for the current page theme, indexed by control type.</summary>
	/// <returns>An <see cref="T:System.Collections.IDictionary" /> that represents the control skins associated with the current page theme.</returns>
	protected abstract IDictionary ControlSkins { get; }

	/// <summary>When overridden in a derived class, gets an array of style sheets that are linked to this page.</summary>
	/// <returns>A string array of style sheets linked to this page.</returns>
	protected abstract string[] LinkedStyleSheets { get; }

	/// <summary>Gets the <see cref="T:System.Web.UI.Page" /> object that is associated with the instance of the <see cref="T:System.Web.UI.PageTheme" /> class.</summary>
	/// <returns>The <see cref="T:System.Web.UI.Page" /> associated with the <see cref="T:System.Web.UI.PageTheme" />.</returns>
	protected Page Page => _page;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PageTheme" /> class.</summary>
	protected PageTheme()
	{
	}

	/// <summary>Creates a lookup key object for a particular control type and skin ID. </summary>
	/// <param name="controlType">The <see cref="T:System.Type" /> of control to which a control skin applies, which is passed typically from the <see cref="P:System.Web.UI.ControlBuilder.ControlType" />.</param>
	/// <param name="skinID">The name of the control skin for which to create a key. </param>
	/// <returns>An object that can be used as a lookup key in a dictionary-style collection, which contains the control type and skin ID information.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="controlType" /> is <see langword="null" />.</exception>
	public static object CreateSkinKey(Type controlType, string skinID)
	{
		return skinID + ":" + controlType;
	}

	/// <summary>Uses the <see cref="M:System.Web.UI.DataBinder.Eval(System.Object,System.String)" /> method of the <see cref="P:System.Web.UI.PageTheme.Page" /> property that the instance of the <see cref="T:System.Web.UI.PageTheme" /> class is associated with to evaluate a data-binding expression.</summary>
	/// <param name="expression">The navigation path from the container to the public property value. For details, see <see cref="T:System.Web.UI.DataBinder" />.</param>
	/// <returns>An object that results from the evaluation of the data-binding expression.</returns>
	protected object Eval(string expression)
	{
		return Page.Eval(expression);
	}

	/// <summary>Uses the <see cref="M:System.Web.UI.DataBinder.Eval(System.Object,System.String,System.String)" /> method of the <see cref="P:System.Web.UI.PageTheme.Page" /> property that the instance of the <see cref="T:System.Web.UI.PageTheme" /> class is associated with to evaluate a data-binding expression.</summary>
	/// <param name="expression">The navigation path from the container to the public property value. For details, see <see cref="T:System.Web.UI.DataBinder" />.</param>
	/// <param name="format">A .NET Framework format string. For details, see <see cref="T:System.Web.UI.DataBinder" />.</param>
	/// <returns>A string that results from the evaluation of the data-binding expression and conversion to a string type.</returns>
	protected string Eval(string expression, string format)
	{
		return Page.Eval(expression, format);
	}

	/// <summary>Tests whether a device filter applies to the <see cref="T:System.Web.UI.Page" /> control that the instance of the <see cref="T:System.Web.UI.PageTheme" /> class is associated with.</summary>
	/// <param name="deviceFilterName">The string name of the device filter to check. </param>
	/// <returns>
	///     <see langword="true" /> if <paramref name="deviceFilterName" /> applies to the page; otherwise; <see langword="false" />.</returns>
	[MonoTODO("Not implemented")]
	public bool TestDeviceFilter(string deviceFilterName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Evaluates an XPath data-binding expression.</summary>
	/// <param name="xPathExpression">The XPath expression to evaluate. For details, see <see cref="T:System.Web.UI.XPathBinder" />.</param>
	/// <returns>An object that results from the evaluation of the data-binding <paramref name="xPathExpression" />.</returns>
	protected object XPath(string xPathExpression)
	{
		return Page.XPath(xPathExpression);
	}

	/// <summary>Evaluates an XPath data-binding expression using the specified prefix and namespace mappings for namespace resolution. </summary>
	/// <param name="xPathExpression">The XPath expression to evaluate. For details, see <see cref="T:System.Web.UI.XPathBinder" />. </param>
	/// <param name="resolver">A set of prefix and namespace mappings used for namespace resolution.</param>
	/// <returns>An object that results from the evaluation of the data-binding <paramref name="xPathExpression" />.</returns>
	protected object XPath(string xPathExpression, IXmlNamespaceResolver resolver)
	{
		return Page.XPath(xPathExpression, resolver);
	}

	/// <summary>Evaluates an XPath data-binding expression using the specified format string to display the result.</summary>
	/// <param name="xPathExpression">The XPath expression to evaluate. For details, see <see cref="T:System.Web.UI.XPathBinder" />.</param>
	/// <param name="format">A .NET Framework format string to apply to the result. </param>
	/// <returns>A string that results from the evaluation of the data-binding expression and conversion to a string type.</returns>
	protected string XPath(string xPathExpression, string format)
	{
		return Page.XPath(xPathExpression, format);
	}

	/// <summary>Uses the <see cref="M:System.Web.UI.TemplateControl.XPath(System.String,System.String,System.Xml.IXmlNamespaceResolver)" /> method of the <see cref="P:System.Web.UI.PageTheme.Page" /> control that the instance of the <see cref="T:System.Web.UI.PageTheme" /> class is associated with to evaluate an XPath data-binding expression.</summary>
	/// <param name="xPathExpression">The XPath expression to evaluate. For details, see <see cref="T:System.Web.UI.XPathBinder" />. </param>
	/// <param name="format">A .NET Framework format string to apply to the result. </param>
	/// <param name="resolver">A set of prefix and namespace mappings used for namespace resolution.</param>
	/// <returns>A string that results from the evaluation of the data-binding expression and conversion to a string type.</returns>
	protected string XPath(string xPathExpression, string format, IXmlNamespaceResolver resolver)
	{
		return Page.XPath(xPathExpression, format, resolver);
	}

	/// <summary>Evaluates an XPath data-binding expression and returns a node collection that implements the <see cref="T:System.Collections.IEnumerable" /> interface.</summary>
	/// <param name="xPathExpression">The XPath expression to evaluate. For details, see <see cref="T:System.Web.UI.XPathBinder" />.</param>
	/// <returns>An <see cref="T:System.Collections.IEnumerable" /> list of nodes.</returns>
	protected IEnumerable XPathSelect(string xPathExpression)
	{
		return Page.XPathSelect(xPathExpression);
	}

	/// <summary>Evaluates an XPath data-binding expression using the specified prefix and namespace mappings for namespace resolution and returns a node collection that implements the <see cref="T:System.Collections.IEnumerable" /> interface.</summary>
	/// <param name="xPathExpression">The XPath expression to evaluate. For details, see <see cref="T:System.Web.UI.XPathBinder" />. </param>
	/// <param name="resolver">A set of prefix and namespace mappings used to for namespace resolution. </param>
	/// <returns>An <see cref="T:System.Collections.IEnumerable" /> list of nodes. </returns>
	protected IEnumerable XPathSelect(string xPathExpression, IXmlNamespaceResolver resolver)
	{
		return Page.XPathSelect(xPathExpression, resolver);
	}

	internal void SetPage(Page page)
	{
		_page = page;
	}

	internal ControlSkin GetControlSkin(Type controlType, string skinID)
	{
		object key = CreateSkinKey(controlType, skinID);
		return ControlSkins[key] as ControlSkin;
	}

	internal string[] GetStyleSheets()
	{
		return LinkedStyleSheets;
	}
}
