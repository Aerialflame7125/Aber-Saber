using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Web.Compilation;
using System.Web.Util;
using System.Xml;

namespace System.Web.UI;

/// <summary>Provides the <see cref="T:System.Web.UI.Page" /> class and the <see cref="T:System.Web.UI.UserControl" /> class with a base set of functionality.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public abstract class TemplateControl : Control, INamingContainer, IFilterResolutionService
{
	private class EvtInfo
	{
		public MethodInfo method;

		public string methodName;

		public EventInfo evt;

		public bool noParams;
	}

	private class StringResourceData
	{
		public IntPtr Ptr;

		public int Length;

		public int MaxOffset;
	}

	private class SimpleTemplate : ITemplate
	{
		private Type type;

		public SimpleTemplate(Type type)
		{
			this.type = type;
		}

		public void InstantiateIn(Control control)
		{
			Control control2 = Activator.CreateInstance(type) as Control;
			control2.SetBindingContainer(isBC: false);
			control.Controls.Add(control2);
		}
	}

	private static readonly Assembly _System_Web_Assembly = typeof(TemplateControl).Assembly;

	private static object abortTransaction = new object();

	private static object commitTransaction = new object();

	private static object error = new object();

	private static string[] methodNames = new string[15]
	{
		"Page_Init", "Page_PreInit", "Page_PreLoad", "Page_LoadComplete", "Page_PreRenderComplete", "Page_SaveStateComplete", "Page_InitComplete", "Page_Load", "Page_DataBind", "Page_PreRender",
		"Page_Disposed", "Page_Error", "Page_Unload", "Page_AbortTransaction", "Page_CommitTransaction"
	};

	private const BindingFlags bflags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

	private string _appRelativeVirtualPath;

	private StringResourceData resource_data;

	private static SplitOrderedList<Type, ArrayList> auto_event_info = new SplitOrderedList<Type, ArrayList>(EqualityComparer<Type>.Default);

	/// <summary>The <see cref="P:System.Web.UI.TemplateControl.AutoHandlers" /> property has been deprecated in ASP.NET NET 2.0. It is used by generated classes and is not intended for use within your code.</summary>
	/// <returns>Always 0. </returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete]
	protected virtual int AutoHandlers
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.TemplateControl" /> control supports automatic events.</summary>
	/// <returns>Always<see langword=" true" />.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	protected virtual bool SupportAutoEvents => true;

	/// <summary>Gets or sets the application-relative, virtual directory path to the file from which the control is parsed and compiled. </summary>
	/// <returns>A string representing the path.</returns>
	/// <exception cref="T:System.ArgumentNullException">The path that is set is <see langword="null" />. </exception>
	/// <exception cref="T:System.ArgumentException">The path that is set is not rooted. </exception>
	public string AppRelativeVirtualPath
	{
		get
		{
			return _appRelativeVirtualPath;
		}
		set
		{
			_appRelativeVirtualPath = value;
		}
	}

	internal override TemplateControl TemplateControlInternal => this;

	/// <summary>Occurs when a user ends a transaction.</summary>
	[WebSysDescription("Raised when the user aborts a transaction.")]
	public event EventHandler AbortTransaction
	{
		add
		{
			base.Events.AddHandler(abortTransaction, value);
		}
		remove
		{
			base.Events.RemoveHandler(abortTransaction, value);
		}
	}

	/// <summary>Occurs when a transaction completes.</summary>
	[WebSysDescription("Raised when the user initiates a transaction.")]
	public event EventHandler CommitTransaction
	{
		add
		{
			base.Events.AddHandler(commitTransaction, value);
		}
		remove
		{
			base.Events.RemoveHandler(commitTransaction, value);
		}
	}

	/// <summary>Occurs when an unhandled exception is thrown.</summary>
	[WebSysDescription("Raised when an exception occurs that cannot be handled.")]
	public event EventHandler Error
	{
		add
		{
			base.Events.AddHandler(error, value);
		}
		remove
		{
			base.Events.RemoveHandler(error, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.TemplateControl" /> class.</summary>
	protected TemplateControl()
	{
		base.TemplateControl = this;
		Construct();
	}

	/// <summary>Performs design-time logic.</summary>
	protected virtual void Construct()
	{
	}

	/// <summary>Accesses literal strings stored in a resource. The <see cref="M:System.Web.UI.TemplateControl.CreateResourceBasedLiteralControl(System.Int32,System.Int32,System.Boolean)" /> method is not intended for use from within your code.</summary>
	/// <param name="offset">The offset of the start of the string in the resource. </param>
	/// <param name="size">The size of the string in bytes. </param>
	/// <param name="fAsciiOnly">A Boolean value indicating if the string in the resource contains only 7-bit ASCII characters. </param>
	/// <returns>A <see cref="T:System.Web.UI.LiteralControl" /> representing a literal string in a resource.</returns>
	protected LiteralControl CreateResourceBasedLiteralControl(int offset, int size, bool fAsciiOnly)
	{
		if (resource_data == null)
		{
			return null;
		}
		if (offset > resource_data.MaxOffset - size)
		{
			throw new ArgumentOutOfRangeException("size");
		}
		return new ResourceBasedLiteralControl(AddOffset(resource_data.Ptr, offset), size);
	}

	internal void WireupAutomaticEvents()
	{
		if (!SupportAutoEvents || !base.AutoEventWireup)
		{
			return;
		}
		Type type = GetType();
		ArrayList arrayList = auto_event_info.InsertOrGet((uint)type.GetHashCode(), type, null, CollectAutomaticEventInfo);
		for (int i = 0; i < arrayList.Count; i++)
		{
			EvtInfo evtInfo = (EvtInfo)arrayList[i];
			if (evtInfo.noParams)
			{
				NoParamsInvoker noParamsInvoker = new NoParamsInvoker(this, evtInfo.method);
				evtInfo.evt.AddEventHandler(this, noParamsInvoker.FakeDelegate);
			}
			else
			{
				evtInfo.evt.AddEventHandler(this, Delegate.CreateDelegate(typeof(EventHandler), this, evtInfo.method));
			}
		}
	}

	private ArrayList CollectAutomaticEventInfo()
	{
		ArrayList arrayList = new ArrayList();
		string[] array = methodNames;
		foreach (string text in array)
		{
			MethodInfo methodInfo = null;
			Type type = GetType();
			while (type.Assembly != _System_Web_Assembly)
			{
				methodInfo = type.GetMethod(text, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (methodInfo != null)
				{
					break;
				}
				type = type.BaseType;
			}
			if (methodInfo == null || (methodInfo.DeclaringType != type && !methodInfo.IsPublic && !methodInfo.IsFamilyOrAssembly && !methodInfo.IsFamilyAndAssembly && !methodInfo.IsFamily) || methodInfo.ReturnType != typeof(void))
			{
				continue;
			}
			ParameterInfo[] parameters = methodInfo.GetParameters();
			int num = parameters.Length;
			bool flag = num == 0;
			if (flag || (num == 2 && !(parameters[0].ParameterType != typeof(object)) && !(parameters[1].ParameterType != typeof(EventArgs))))
			{
				int num2 = text.IndexOf('_');
				string name = text.Substring(num2 + 1);
				EventInfo @event = type.GetEvent(name);
				if (!(@event == null))
				{
					EvtInfo evtInfo = new EvtInfo();
					evtInfo.method = methodInfo;
					evtInfo.methodName = text;
					evtInfo.evt = @event;
					evtInfo.noParams = flag;
					arrayList.Add(evtInfo);
				}
			}
		}
		return arrayList;
	}

	/// <summary>Initializes the control that is derived from the <see cref="T:System.Web.UI.TemplateControl" /> class.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	protected virtual void FrameworkInitialize()
	{
	}

	private Type GetTypeFromControlPath(string virtualPath)
	{
		if (virtualPath == null)
		{
			throw new ArgumentNullException("virtualPath");
		}
		return BuildManager.GetCompiledType(UrlUtils.Combine(TemplateSourceDirectory, virtualPath));
	}

	/// <summary>Loads a <see cref="T:System.Web.UI.Control" /> object from a file based on a specified virtual path.</summary>
	/// <param name="virtualPath">The virtual path to a control file. </param>
	/// <returns>Returns the specified <see cref="T:System.Web.UI.Control" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">The virtual path is <see langword="null" /> or empty.</exception>
	public Control LoadControl(string virtualPath)
	{
		if (virtualPath == null)
		{
			throw new ArgumentNullException("virtualPath");
		}
		Type typeFromControlPath = GetTypeFromControlPath(virtualPath);
		return LoadControl(typeFromControlPath, null);
	}

	/// <summary>Loads a <see cref="T:System.Web.UI.Control" /> object based on a specified type and constructor parameters.</summary>
	/// <param name="t">The type of the control.</param>
	/// <param name="parameters">An array of arguments that match in number, order, and type the parameters of the constructor to invoke. If <paramref name="parameters" /> is an empty array or <see langword="null" />, the constructor that takes no parameters (the default constructor) is invoked.</param>
	/// <returns>Returns the specified <see cref="T:System.Web.UI.UserControl" />.</returns>
	public Control LoadControl(Type t, object[] parameters)
	{
		object[] array = null;
		if (t != null)
		{
			t.GetCustomAttributes(typeof(PartialCachingAttribute), inherit: true);
		}
		if (array != null && array.Length == 1)
		{
			PartialCachingAttribute partialCachingAttribute = (PartialCachingAttribute)array[0];
			return new PartialCachingControl(t, parameters)
			{
				VaryByParams = partialCachingAttribute.VaryByParams,
				VaryByControls = partialCachingAttribute.VaryByControls,
				VaryByCustom = partialCachingAttribute.VaryByCustom
			};
		}
		object obj = Activator.CreateInstance(t, parameters);
		if (obj is UserControl)
		{
			((UserControl)obj).InitializeAsUserControl(Page);
		}
		return (Control)obj;
	}

	/// <summary>Obtains an instance of the <see cref="T:System.Web.UI.ITemplate" /> interface from an external file.</summary>
	/// <param name="virtualPath">The virtual path to a user control file. </param>
	/// <returns>An instance of the specified template.</returns>
	public ITemplate LoadTemplate(string virtualPath)
	{
		if (virtualPath == null)
		{
			throw new ArgumentNullException("virtualPath");
		}
		return new SimpleTemplate(GetTypeFromControlPath(virtualPath));
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.TemplateControl.AbortTransaction" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnAbortTransaction(EventArgs e)
	{
		if (base.Events[abortTransaction] is EventHandler eventHandler)
		{
			eventHandler(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.TemplateControl.CommitTransaction" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnCommitTransaction(EventArgs e)
	{
		if (base.Events[commitTransaction] is EventHandler eventHandler)
		{
			eventHandler(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.TemplateControl.Error" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnError(EventArgs e)
	{
		if (base.Events[error] is EventHandler eventHandler)
		{
			eventHandler(this, e);
		}
	}

	/// <summary>Parses an input string into a <see cref="T:System.Web.UI.Control" /> object on the Web Forms page or user control.</summary>
	/// <param name="content">A string that contains a user control. </param>
	/// <returns>The parsed <see cref="T:System.Web.UI.Control" />.</returns>
	public Control ParseControl(string content)
	{
		if (content == null)
		{
			throw new ArgumentNullException("content");
		}
		Type compiledType = UserControlParser.GetCompiledType(new StringReader(content), content.GetHashCode(), HttpContext.Current);
		if (compiledType == null)
		{
			return null;
		}
		if (!(Activator.CreateInstance(compiledType, null) is TemplateControl templateControl))
		{
			return null;
		}
		if (this is Page)
		{
			templateControl.Page = (Page)this;
		}
		templateControl.FrameworkInitialize();
		Control control = new Control();
		int count = templateControl.Controls.Count;
		Control[] array = new Control[count];
		templateControl.Controls.CopyTo(array, 0);
		for (int i = 0; i < count; i++)
		{
			control.Controls.Add(array[i]);
		}
		TemplateControl templateControl2 = null;
		return control;
	}

	/// <summary>Parses an input string into a <see cref="T:System.Web.UI.Control" /> object on the ASP.NET Web page or user control.</summary>
	/// <param name="content">A string that contains a user control.</param>
	/// <param name="ignoreParserFilter">A value that specifies whether to ignore the parser filter.</param>
	/// <returns>The parsed control.</returns>
	[MonoTODO("Parser filters not implemented yet. Calls ParseControl (string) for now.")]
	public Control ParseControl(string content, bool ignoreParserFilter)
	{
		return ParseControl(content);
	}

	/// <summary>Reads a string resource. The <see cref="M:System.Web.UI.TemplateControl.ReadStringResource" /> method is not intended for use from within your code.</summary>
	/// <returns>An object representing the resource.</returns>
	/// <exception cref="T:System.NotSupportedException">The <see cref="M:System.Web.UI.TemplateControl.ReadStringResource" /> is no longer supported.</exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public object ReadStringResource()
	{
		return ReadStringResource(GetType());
	}

	/// <summary>Gets an application-level resource object based on the specified <see cref="P:System.Web.Compilation.ResourceExpressionFields.ClassKey" /> and <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" /> properties. </summary>
	/// <param name="className">A string representing a <see cref="P:System.Web.Compilation.ResourceExpressionFields.ClassKey" />.</param>
	/// <param name="resourceKey">A string representing a <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" />.</param>
	/// <returns>An object representing the requested resource object; otherwise, <see langword="null" />.</returns>
	protected object GetGlobalResourceObject(string className, string resourceKey)
	{
		return HttpContext.GetGlobalResourceObject(className, resourceKey);
	}

	/// <summary>Gets an application-level resource object based on the specified <see cref="P:System.Web.Compilation.ResourceExpressionFields.ClassKey" /> and <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" /> properties, object type, and property name of the resource.</summary>
	/// <param name="className">A string representing a <see cref="P:System.Web.Compilation.ResourceExpressionFields.ClassKey" />. </param>
	/// <param name="resourceKey">A string representing a <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" />. </param>
	/// <param name="objType">The type of object in the resource to get. </param>
	/// <param name="propName">The property name of the object to get.</param>
	/// <returns>An object representing the requested resource object; otherwise, <see langword="null" />.</returns>
	protected object GetGlobalResourceObject(string className, string resourceKey, Type objType, string propName)
	{
		if (string.IsNullOrEmpty(resourceKey) || string.IsNullOrEmpty(propName) || string.IsNullOrEmpty(className) || objType == null)
		{
			return null;
		}
		object globalResourceObject = GetGlobalResourceObject(className, resourceKey);
		if (globalResourceObject == null)
		{
			return null;
		}
		TypeConverter converter = TypeDescriptor.GetProperties(objType)[propName].Converter;
		if (converter == null || !converter.CanConvertFrom(globalResourceObject.GetType()))
		{
			return null;
		}
		return converter.ConvertFrom(globalResourceObject);
	}

	/// <summary>Gets a page-level resource object based on the specified <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" /> property.</summary>
	/// <param name="resourceKey">A string representing a <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" />.</param>
	/// <returns>An object representing the requested resource object; otherwise, <see langword="null" />.</returns>
	protected object GetLocalResourceObject(string resourceKey)
	{
		return HttpContext.GetLocalResourceObject(VirtualPathUtility.ToAbsolute(AppRelativeVirtualPath), resourceKey);
	}

	/// <summary>Gets a page-level resource object based on the specified <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" /> property, object type, and property name.</summary>
	/// <param name="resourceKey">A string representing a <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" />.</param>
	/// <param name="objType">The type of the resource object to get.</param>
	/// <param name="propName">The property name of the resource object to get.</param>
	/// <returns>An object representing the requested resource object; otherwise, <see langword="null" />.</returns>
	protected object GetLocalResourceObject(string resourceKey, Type objType, string propName)
	{
		if (string.IsNullOrEmpty(resourceKey) || string.IsNullOrEmpty(propName) || objType == null)
		{
			return null;
		}
		object localResourceObject = GetLocalResourceObject(resourceKey);
		if (localResourceObject == null)
		{
			return null;
		}
		TypeConverter converter = TypeDescriptor.GetProperties(objType)[propName].Converter;
		if (converter == null || !converter.CanConvertFrom(localResourceObject.GetType()))
		{
			return null;
		}
		return converter.ConvertFrom(localResourceObject);
	}

	/// <summary>Reads a string resource. The <see cref="M:System.Web.UI.TemplateControl.ReadStringResource(System.Type)" /> method is not intended for use from within your code.</summary>
	/// <param name="t">The <see cref="T:System.Type" /> of the resource to read.</param>
	/// <returns>An object representing the resource.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static object ReadStringResource(Type t)
	{
		StringResourceData stringResourceData = new StringResourceData();
		if (ICalls.GetUnmanagedResourcesPtr(t.Assembly, out stringResourceData.Ptr, out stringResourceData.Length))
		{
			return stringResourceData;
		}
		throw new HttpException("Unable to load the string resources.");
	}

	/// <summary>Sets a pointer to a string resource. The <see cref="M:System.Web.UI.TemplateControl.SetStringResourcePointer(System.Object,System.Int32)" /> method is used by generated classes and is not intended for use from within your code.</summary>
	/// <param name="stringResourcePointer">An object representing the pointer to the string resource.</param>
	/// <param name="maxResourceOffset">The resource size. </param>
	[EditorBrowsable(EditorBrowsableState.Never)]
	protected void SetStringResourcePointer(object stringResourcePointer, int maxResourceOffset)
	{
		if (stringResourcePointer is StringResourceData stringResourceData)
		{
			if (maxResourceOffset < 0 || maxResourceOffset > stringResourceData.Length)
			{
				throw new ArgumentOutOfRangeException("maxResourceOffset");
			}
			resource_data = new StringResourceData();
			resource_data.Ptr = stringResourceData.Ptr;
			resource_data.Length = stringResourceData.Length;
			resource_data.MaxOffset = ((maxResourceOffset > 0) ? Math.Min(maxResourceOffset, stringResourceData.Length) : stringResourceData.Length);
		}
	}

	private static IntPtr AddOffset(IntPtr ptr, int offset)
	{
		if (offset == 0)
		{
			return ptr;
		}
		if (IntPtr.Size == 4)
		{
			int value = ptr.ToInt32() + offset;
			ptr = new IntPtr(value);
		}
		else
		{
			long value2 = ptr.ToInt64() + offset;
			ptr = new IntPtr(value2);
		}
		return ptr;
	}

	/// <summary>Writes a resource string to an <see cref="T:System.Web.UI.HtmlTextWriter" /> control. The <see cref="M:System.Web.UI.TemplateControl.WriteUTF8ResourceString(System.Web.UI.HtmlTextWriter,System.Int32,System.Int32,System.Boolean)" /> method is used by generated classes and is not intended for use from within your code.</summary>
	/// <param name="output">The control to write to.</param>
	/// <param name="offset">The starting position within <paramref name="value" />.</param>
	/// <param name="size">The number of characters within <paramref name="value" /> to use.</param>
	/// <param name="fAsciiOnly">
	///       <see langword="true" /> to bypass re-encoding; otherwise, <see langword="false" />.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">Data that is not valid is being accessed; <paramref name="offset" /> or <paramref name="size" /> is less than zero.- or -The sum of <paramref name="offset" /> and <paramref name="size" /> is greater than the resource size.</exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	protected void WriteUTF8ResourceString(HtmlTextWriter output, int offset, int size, bool fAsciiOnly)
	{
		if (resource_data != null)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (offset > resource_data.MaxOffset - size)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			IntPtr intPtr = AddOffset(resource_data.Ptr, offset);
			HttpWriter httpWriter = output.GetHttpWriter();
			if (httpWriter == null || httpWriter.Response.ContentEncoding.CodePage != 65001)
			{
				byte[] array = new byte[size];
				Marshal.Copy(intPtr, array, 0, size);
				output.Write(Encoding.UTF8.GetString(array));
				array = null;
			}
			else
			{
				httpWriter.WriteUTF8Ptr(intPtr, size);
			}
		}
	}

	/// <summary>Evaluates a data-binding expression.</summary>
	/// <param name="expression">The navigation path from the container to the public property value to place in the bound control property.</param>
	/// <returns>An object that results from the evaluation of the data-binding expression.</returns>
	/// <exception cref="T:System.InvalidOperationException">The data-binding method can be used only for controls contained on a <see cref="T:System.Web.UI.Page" />. </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="expression" /> is <see langword="null" />. - or -
	///         <paramref name="expression" /> is an empty string ("").</exception>
	protected internal object Eval(string expression)
	{
		return DataBinder.Eval(Page.GetDataItem(), expression);
	}

	/// <summary>Evaluates a data-binding expression using the specified format string to display the result.</summary>
	/// <param name="expression">The navigation path from the container to the public property value to place in the bound control property.</param>
	/// <param name="format">A .NET Framework format string to apply to the result.</param>
	/// <returns>A string that results from the evaluation of the data-binding expression and conversion to a string type.</returns>
	/// <exception cref="T:System.InvalidOperationException">The data-binding method can only be used for controls contained on a <see cref="T:System.Web.UI.Page" />. </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="expression" /> is <see langword="null" />. - or -
	///         <paramref name="expression" /> is an empty string ("").</exception>
	protected internal string Eval(string expression, string format)
	{
		return DataBinder.Eval(Page.GetDataItem(), expression, format);
	}

	/// <summary>Evaluates an XPath data-binding expression.</summary>
	/// <param name="xPathExpression">The XPath expression to evaluate. For more information, see <see cref="T:System.Web.UI.XPathBinder" />.</param>
	/// <returns>An object that results from the evaluation of the data-binding expression.</returns>
	/// <exception cref="T:System.InvalidOperationException">The data-binding method can be used only for controls contained on a <see cref="T:System.Web.UI.Page" />. </exception>
	protected internal object XPath(string xPathExpression)
	{
		return XPathBinder.Eval(Page.GetDataItem(), xPathExpression);
	}

	/// <summary>Evaluates an XPath data-binding expression using the specified prefix and namespace mappings for namespace resolution.</summary>
	/// <param name="xPathExpression">The XPath expression to evaluate. For more information, see <see cref="T:System.Web.UI.XPathBinder" />. </param>
	/// <param name="resolver">A set of prefix and namespace mappings used for namespace resolution.</param>
	/// <returns>An object that results from the evaluation of the data-binding expression. </returns>
	/// <exception cref="T:System.InvalidOperationException">The data-binding method can be used only for controls contained on a <see cref="T:System.Web.UI.Page" />. </exception>
	protected internal object XPath(string xPathExpression, IXmlNamespaceResolver resolver)
	{
		return XPathBinder.Eval(Page.GetDataItem(), xPathExpression, null, resolver);
	}

	/// <summary>Evaluates an XPath data-binding expression using the specified format string to display the result. </summary>
	/// <param name="xPathExpression">The XPath expression to evaluate. For more information, see <see cref="T:System.Web.UI.XPathBinder" />. </param>
	/// <param name="format">A .NET Framework format string to apply to the result. </param>
	/// <returns>A string that results from the evaluation of the data-binding expression and conversion to a string type.</returns>
	/// <exception cref="T:System.InvalidOperationException">The data-binding method can be used only for controls contained on a <see cref="T:System.Web.UI.Page" />. </exception>
	protected internal string XPath(string xPathExpression, string format)
	{
		return XPathBinder.Eval(Page.GetDataItem(), xPathExpression, format);
	}

	/// <summary>Evaluates an XPath data-binding expression using the specified prefix and namespace mappings for namespace resolution and the specified format string to display the result.</summary>
	/// <param name="xPathExpression">The XPath expression to evaluate. For more information, see <see cref="T:System.Web.UI.XPathBinder" />. </param>
	/// <param name="format">A .NET Framework format string to apply to the result. </param>
	/// <param name="resolver">A set of prefix and namespace mappings used for namespace resolution. </param>
	/// <returns>A string that results from the evaluation of the data-binding expression and conversion to a string type.</returns>
	/// <exception cref="T:System.InvalidOperationException">The data-binding method can be used only for controls contained on a <see cref="T:System.Web.UI.Page" />. </exception>
	protected internal string XPath(string xPathExpression, string format, IXmlNamespaceResolver resolver)
	{
		return XPathBinder.Eval(Page.GetDataItem(), xPathExpression, format, resolver);
	}

	/// <summary>Evaluates an XPath data-binding expression and returns a node collection that implements the <see cref="T:System.Collections.IEnumerable" /> interface.</summary>
	/// <param name="xPathExpression">The XPath expression to evaluate. For more information, see <see cref="T:System.Web.UI.XPathBinder" />. </param>
	/// <returns>An <see cref="T:System.Collections.IEnumerable" /> node list. </returns>
	protected internal IEnumerable XPathSelect(string xPathExpression)
	{
		return XPathBinder.Select(Page.GetDataItem(), xPathExpression);
	}

	/// <summary>Evaluates an XPath data-binding expression using the specified prefix and namespace mappings for namespace resolution and returns a node collection that implements the <see cref="T:System.Collections.IEnumerable" /> interface.</summary>
	/// <param name="xPathExpression">The XPath expression to evaluate. For more information, see <see cref="T:System.Web.UI.XPathBinder" />. </param>
	/// <param name="resolver">A set of prefix and namespace mappings used for namespace resolution. </param>
	/// <returns>An <see cref="T:System.Collections.IEnumerable" /> node list. </returns>
	protected internal IEnumerable XPathSelect(string xPathExpression, IXmlNamespaceResolver resolver)
	{
		return XPathBinder.Select(Page.GetDataItem(), xPathExpression, resolver);
	}

	/// <summary>Returns a value that indicates whether a parent/child relationship exists between two specified device filters. </summary>
	/// <param name="filter1">A device filter name. </param>
	/// <param name="filter2">A device filter name. </param>
	/// <returns>1, if <paramref name="filter1" /> is a parent of <paramref name="filter2" />; -1, if <paramref name="filter2" /> is a parent of <paramref name="filter1" />; otherwise, 0, if there is no parent/child relationship between <paramref name="filter1" /> and <paramref name="filter2" />.</returns>
	[MonoTODO("Not implemented")]
	int IFilterResolutionService.CompareFilters(string filter1, string filter2)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns a value that indicates whether the specified filter is a type of the current filter object.</summary>
	/// <param name="filterName">The name of a device filter.</param>
	/// <returns>
	///     <see langword="true" /> if the specified filter is a type applicable to the current filter object; otherwise, <see langword="false" />.</returns>
	[MonoTODO("Not implemented")]
	bool IFilterResolutionService.EvaluateFilter(string filterName)
	{
		throw new NotImplementedException();
	}
}
