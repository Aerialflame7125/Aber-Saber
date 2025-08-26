using System.Collections;

namespace System.Web.UI;

/// <summary>Provides an abstract base class for a page parser filter that is used by the ASP.NET parser to determine whether an item is allowed in the page at parse time. </summary>
public abstract class PageParserFilter
{
	private TemplateParser parser;

	/// <summary>Gets a value indicating whether an ASP.NET parser filter permits code on the page. </summary>
	/// <returns>
	///     <see langword="true" /> if a parser filter permits code; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool AllowCode => false;

	/// <summary>Gets the line number that is currently being parsed in the file.</summary>
	/// <returns>The integer value representing the line in the file that the parser filter is currently processing.</returns>
	[MonoTODO("Need to implement support for this in the parser")]
	protected int Line => parser.Location.BeginLine;

	/// <summary>Gets the maximum number of controls that a parser filter can parse for a single page.</summary>
	/// <returns>The maximum number of controls a parser filter can parse for a page. The default value is 0, which indicates that no controls are parsed.</returns>
	public virtual int NumberOfControlsAllowed => 0;

	/// <summary>Gets the maximum number of direct file dependencies that the page parser permits for a single page.</summary>
	/// <returns>The maximum number of direct file dependencies the page parser can parse for a page. The default is 0, which that indicates no dependencies are allowed.</returns>
	public virtual int NumberOfDirectDependenciesAllowed => 0;

	/// <summary>Gets the maximum number of direct and indirect file dependencies that the page parser permits for a single page.</summary>
	/// <returns>The maximum number of direct and indirect file dependencies the page parser can parse for a page. The default is 0, which indicates that no dependencies are allowed.</returns>
	public virtual int TotalNumberOfDependenciesAllowed => 0;

	/// <summary>Gets the virtual path to the page currently being parsed.</summary>
	/// <returns>A virtual path to an ASP.NET page.</returns>
	protected string VirtualPath => parser.VirtualPath.Absolute;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PageParserFilter" /> class. </summary>
	protected PageParserFilter()
	{
	}

	/// <summary>Adds a <see cref="T:System.Web.UI.ControlBuilder" /> object in the page control tree at the current page parser position.</summary>
	/// <param name="type">The control type that the <see cref="T:System.Web.UI.ControlBuilder" /> represents.</param>
	/// <param name="attributes">The <see cref="T:System.Collections.IDictionary" /> object that holds all the specified tag attributes.</param>
	protected void AddControl(Type type, IDictionary attributes)
	{
		if (parser != null)
		{
			parser.AddControl(type, attributes);
		}
	}

	/// <summary>Determines whether the page can be derived from the specified <see cref="T:System.Type" />.</summary>
	/// <param name="baseType">A <see cref="T:System.Type" /> that represents the potential base class of the current page.</param>
	/// <returns>
	///     <see langword="true" /> if the page can inherit from the specified type; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool AllowBaseType(Type baseType)
	{
		return false;
	}

	/// <summary>Gets a value indicating whether the specified control type is allowed for this page.</summary>
	/// <param name="controlType">A <see cref="T:System.Type" /> that represents the type of control to add.</param>
	/// <param name="builder">A <see cref="T:System.Web.UI.ControlBuilder" /> used to build the specified type of control.</param>
	/// <returns>
	///     <see langword="true" /> if the control can be used with the current page; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	public virtual bool AllowControl(Type controlType, ControlBuilder builder)
	{
		return false;
	}

	/// <summary>Determines whether a parser permits a specific server-side include on a page.</summary>
	/// <param name="includeVirtualPath">The virtual path to the included file.</param>
	/// <returns>
	///     <see langword="true" /> if a parser permits the specific server-side include; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool AllowServerSideInclude(string includeVirtualPath)
	{
		return false;
	}

	/// <summary>Determines whether a parser permits a virtual reference to a specific type of resource on a page.</summary>
	/// <param name="referenceVirtualPath">The virtual path to a resource, such as a master page file, ASP.NET page, or user control. </param>
	/// <param name="referenceType">A <see cref="T:System.Web.UI.VirtualReferenceType" /> value that identifies the type of resource.</param>
	/// <returns>
	///     <see langword="true" /> if the parser permits a virtual reference to a specific type of resource; otherwise, <see langword="false" />.</returns>
	public virtual bool AllowVirtualReference(string referenceVirtualPath, VirtualReferenceType referenceType)
	{
		return false;
	}

	/// <summary>Retrieves the current compilation mode for the page.</summary>
	/// <param name="current">The current compilation mode for the page.</param>
	/// <returns>One of the <see cref="T:System.Web.UI.CompilationMode" /> values.</returns>
	public virtual CompilationMode GetCompilationMode(CompilationMode current)
	{
		return current;
	}

	/// <summary>Returns a <see cref="T:System.Type" /> that should be used for pages or controls that are not dynamically compiled.</summary>
	/// <returns>The return <see cref="T:System.Type" /> that should be used for pages or controls that are not dynamically compiled. The default is <see langword="null" />.</returns>
	public virtual Type GetNoCompileUserControlType()
	{
		return null;
	}

	/// <summary>Initializes a filter used for a page.</summary>
	protected virtual void Initialize()
	{
	}

	internal void Initialize(TemplateParser parser)
	{
		this.parser = parser;
		Initialize();
	}

	/// <summary>Called by an ASP.NET page parser to notify a filter when the parsing of a page is complete.</summary>
	/// <param name="rootBuilder">The <see cref="T:System.Web.UI.ControlBuilder" /> associated with the page parsing.</param>
	public virtual void ParseComplete(ControlBuilder rootBuilder)
	{
	}

	/// <summary>Allows the page parser filter to preprocess page directives.</summary>
	/// <param name="directiveName">The page directive.</param>
	/// <param name="attributes">A collection of attributes and values parsed from the page.</param>
	public virtual void PreprocessDirective(string directiveName, IDictionary attributes)
	{
	}

	/// <summary>Returns a value that indicates whether a code block should be processed by subsequent parser filters.</summary>
	/// <param name="codeType">One of the <see cref="T:System.Web.UI.CodeConstructType" /> enumeration values that identifies the type of the code construct.</param>
	/// <param name="code">The string literal that contains the code inside the code construct.</param>
	/// <returns>
	///     <see langword="true" /> if the parser should process a code construct further; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool ProcessCodeConstruct(CodeConstructType codeType, string code)
	{
		return false;
	}

	/// <summary>Returns a value that indicates whether the parser filter processes a data binding expression in an attribute.</summary>
	/// <param name="controlId">The ID of the control that contains the data binding attribute.</param>
	/// <param name="name">The name of the attribute with the data binding expression.</param>
	/// <param name="value">The data binding expression.</param>
	/// <returns>
	///     <see langword="true" /> if the parser filter processes data binding attributes; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool ProcessDataBindingAttribute(string controlId, string name, string value)
	{
		return false;
	}

	/// <summary>Returns a value that indicates whether event handlers should be processed further by the parser filter.</summary>
	/// <param name="controlId">The ID of the control whose event has the event handler to process.</param>
	/// <param name="eventName">The event name of the <paramref name="controlID" /> to filter on.</param>
	/// <param name="handlerName">The handler of the <paramref name="eventName" /> name to filter on.</param>
	/// <returns>
	///     <see langword="true" /> if the parser processes event handlers; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool ProcessEventHookup(string controlId, string eventName, string handlerName)
	{
		return false;
	}

	/// <summary>Sets a property on a control derived from the <see cref="T:System.Web.UI.TemplateControl" /> class, which includes the <see cref="T:System.Web.UI.Page" />, <see cref="T:System.Web.UI.UserControl" />, and <see cref="T:System.Web.UI.MasterPage" /> controls.</summary>
	/// <param name="filter">A string containing the value of the filter on an expression. For an example, see <see cref="T:System.Web.UI.PropertyEntry" />.</param>
	/// <param name="name">The name of the property to set a value for.</param>
	/// <param name="value">The value of the property to set.</param>
	protected void SetPageProperty(string filter, string name, string value)
	{
	}
}
