using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Caching;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Routing;
using System.Web.SessionState;
using System.Web.UI.Adapters;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace System.Web.UI;

/// <summary>Represents an .aspx file, also known as a Web Forms page, requested from a server that hosts an ASP.NET Web application.</summary>
[DefaultEvent("Load")]
[DesignerCategory("ASPXCodeBehind")]
[ToolboxItem(false)]
[Designer("Microsoft.VisualStudio.Web.WebForms.WebFormDesigner, Microsoft.VisualStudio.Web, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(IRootDesigner))]
[DesignerSerializer("Microsoft.VisualStudio.Web.WebForms.WebFormCodeDomSerializer, Microsoft.VisualStudio.Web, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.TypeCodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class Page : TemplateControl, IHttpHandler
{
	private delegate void ProcessRequestDelegate(HttpContext context);

	private sealed class DummyAsyncResult : IAsyncResult
	{
		private readonly object state;

		private readonly WaitHandle asyncWaitHandle;

		private readonly bool completedSynchronously;

		private readonly bool isCompleted;

		public object AsyncState => state;

		public WaitHandle AsyncWaitHandle => asyncWaitHandle;

		public bool CompletedSynchronously => completedSynchronously;

		public bool IsCompleted => isCompleted;

		public DummyAsyncResult(bool isCompleted, bool completedSynchronously, object state)
		{
			this.isCompleted = isCompleted;
			this.completedSynchronously = completedSynchronously;
			this.state = state;
			if (isCompleted)
			{
				asyncWaitHandle = new ManualResetEvent(initialState: true);
			}
			else
			{
				asyncWaitHandle = new ManualResetEvent(initialState: false);
			}
		}
	}

	private bool _eventValidation = true;

	private object[] _savedControlState;

	private bool _doLoadPreviousPage;

	private string _focusedControlID;

	private bool _hasEnabledControlArray;

	private bool _viewState;

	private bool _viewStateMac;

	private string _errorPage;

	private bool is_validated;

	private bool _smartNavigation;

	private int _transactionMode;

	private ValidatorCollection _validators;

	private bool renderingForm;

	private string _savedViewState;

	private List<string> _requiresPostBack;

	private List<string> _requiresPostBackCopy;

	private List<IPostBackDataHandler> requiresPostDataChanged;

	private IPostBackEventHandler requiresRaiseEvent;

	private IPostBackEventHandler formPostedRequiresRaiseEvent;

	private NameValueCollection secondPostData;

	private bool requiresPostBackScript;

	private bool postBackScriptRendered;

	private bool requiresFormScriptDeclaration;

	private bool formScriptDeclarationRendered;

	private bool handleViewState;

	private string viewStateUserKey;

	private NameValueCollection _requestValueCollection;

	private string clientTarget;

	private ClientScriptManager scriptManager;

	private bool allow_load;

	private PageStatePersister page_state_persister;

	private CultureInfo _appCulture;

	private CultureInfo _appUICulture;

	private HttpContext _context;

	private HttpApplicationState _application;

	private HttpResponse _response;

	private HttpRequest _request;

	private Cache _cache;

	private HttpSessionState _session;

	/// <summary>A string that defines the EVENTARGUMENT hidden field in the rendered page.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public const string postEventArgumentID = "__EVENTARGUMENT";

	/// <summary>A string that defines the EVENTTARGET hidden field in the rendered page.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public const string postEventSourceID = "__EVENTTARGET";

	private const string ScrollPositionXID = "__SCROLLPOSITIONX";

	private const string ScrollPositionYID = "__SCROLLPOSITIONY";

	private const string EnabledControlArrayID = "__enabledControlArray";

	internal const string LastFocusID = "__LASTFOCUS";

	internal const string CallbackArgumentID = "__CALLBACKPARAM";

	internal const string CallbackSourceID = "__CALLBACKID";

	internal const string PreviousPageID = "__PREVIOUSPAGE";

	private int maxPageStateFieldLength = -1;

	private string uniqueFilePathSuffix;

	private HtmlHead htmlHeader;

	private MasterPage masterPage;

	private string masterPageFile;

	private Page previousPage;

	private bool isCrossPagePostBack;

	private bool isPostBack;

	private bool isCallback;

	private List<Control> requireStateControls;

	private HtmlForm _form;

	private string _title;

	private string _theme;

	private string _styleSheetTheme;

	private string _metaDescription;

	private string _metaKeywords;

	private Control _autoPostBackControl;

	private bool frameworkInitialized;

	private Hashtable items;

	private bool _maintainScrollPositionOnPostBack;

	private bool asyncMode;

	private TimeSpan asyncTimeout;

	private const double DefaultAsyncTimeout = 45.0;

	private List<PageAsyncTask> parallelTasks;

	private List<PageAsyncTask> serialTasks;

	private ViewStateEncryptionMode viewStateEncryptionMode;

	private bool controlRegisteredForViewStateEncryption;

	private string _validationStartupScript;

	private string _validationOnSubmitStatement;

	private string _validationInitializeScript;

	private string _webFormScriptReference;

	internal static readonly object InitCompleteEvent = new object();

	internal static readonly object LoadCompleteEvent = new object();

	internal static readonly object PreInitEvent = new object();

	internal static readonly object PreLoadEvent = new object();

	internal static readonly object PreRenderCompleteEvent = new object();

	internal static readonly object SaveStateCompleteEvent = new object();

	private int event_mask;

	private const int initcomplete_mask = 1;

	private const int loadcomplete_mask = 2;

	private const int preinit_mask = 4;

	private const int preload_mask = 8;

	private const int prerendercomplete_mask = 16;

	private const int savestatecomplete_mask = 32;

	private Hashtable contentTemplates;

	private PageTheme _pageTheme;

	private PageTheme _styleSheetPageTheme;

	private Stack dataItemCtx;

	/// <summary>Gets the <see cref="T:System.Web.HttpApplicationState" /> object for the current Web request.</summary>
	/// <returns>The current data in the <see cref="T:System.Web.HttpApplicationState" /> class.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public HttpApplicationState Application => _application;

	/// <summary>Sets a value indicating whether the page can be executed on a single-threaded apartment (STA) thread.</summary>
	/// <returns>
	///     <see langword="true" /> if the page supports Active Server Pages (ASP) code; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	protected bool AspCompatMode
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	/// <summary>Sets a value indicating whether the page output is buffered.</summary>
	/// <returns>
	///     <see langword="true" /> if page output is buffered; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool Buffer
	{
		get
		{
			return Response.BufferOutput;
		}
		set
		{
			Response.BufferOutput = value;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.Caching.Cache" /> object associated with the application in which the page resides.</summary>
	/// <returns>The <see cref="T:System.Web.Caching.Cache" /> associated with the page's application.</returns>
	/// <exception cref="T:System.Web.HttpException">An instance of <see cref="T:System.Web.Caching.Cache" /> is not created. </exception>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public Cache Cache
	{
		get
		{
			if (_cache == null)
			{
				throw new HttpException("Cache is not available.");
			}
			return _cache;
		}
	}

	/// <summary>Gets or sets a value that allows you to override automatic detection of browser capabilities and to specify how a page is rendered for particular browser clients.</summary>
	/// <returns>A <see cref="T:System.String" /> that specifies the browser capabilities that you want to override.</returns>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[DefaultValue("")]
	[WebSysDescription("Value do override the automatic browser detection and force the page to use the specified browser.")]
	public string ClientTarget
	{
		get
		{
			if (clientTarget != null)
			{
				return clientTarget;
			}
			return string.Empty;
		}
		set
		{
			clientTarget = value;
			if (value == string.Empty)
			{
				clientTarget = null;
			}
		}
	}

	/// <summary>Sets the code page identifier for the current <see cref="T:System.Web.UI.Page" />.</summary>
	/// <returns>An integer that represents the code page identifier for the current <see cref="T:System.Web.UI.Page" />.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public int CodePage
	{
		get
		{
			return Response.ContentEncoding.CodePage;
		}
		set
		{
			Response.ContentEncoding = Encoding.GetEncoding(value);
		}
	}

	/// <summary>Sets the HTTP MIME type for the <see cref="T:System.Web.HttpResponse" /> object associated with the page.</summary>
	/// <returns>The HTTP MIME type associated with the current page.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public string ContentType
	{
		get
		{
			return Response.ContentType;
		}
		set
		{
			Response.ContentType = value;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.HttpContext" /> object associated with the page.</summary>
	/// <returns>An <see cref="T:System.Web.HttpContext" /> object that contains information associated with the current page.</returns>
	protected internal override HttpContext Context
	{
		get
		{
			if (_context == null)
			{
				return HttpContext.Current;
			}
			return _context;
		}
	}

	/// <summary>Sets the culture ID for the <see cref="T:System.Threading.Thread" /> object associated with the page.</summary>
	/// <returns>A valid culture ID.</returns>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public string Culture
	{
		get
		{
			return Thread.CurrentThread.CurrentCulture.Name;
		}
		set
		{
			Thread.CurrentThread.CurrentCulture = GetPageCulture(value, Thread.CurrentThread.CurrentCulture);
		}
	}

	/// <summary>Gets or sets a value indicating whether the page validates postback and callback events.</summary>
	/// <returns>
	///     <see langword="true" /> if the page validates postback and callback events; otherwise, <see langword="false" />.The default is <see langword="true" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.Page.EnableEventValidation" /> property was set after the page was initialized.</exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DefaultValue("true")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public virtual bool EnableEventValidation
	{
		get
		{
			return _eventValidation;
		}
		set
		{
			if (base.IsInited)
			{
				throw new InvalidOperationException("The 'EnableEventValidation' property can be set only in the Page_init, the Page directive or in the <pages> configuration section.");
			}
			_eventValidation = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the page maintains its view state, and the view state of any server controls it contains, when the current page request ends.</summary>
	/// <returns>
	///     <see langword="true" /> if the page maintains its view state; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[Browsable(false)]
	public override bool EnableViewState
	{
		get
		{
			return _viewState;
		}
		set
		{
			_viewState = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether ASP.NET should check message authentication codes (MAC) in the page's view state when the page is posted back from the client.</summary>
	/// <returns>
	///     <see langword="true" /> if the view state should be MAC checked and encoded; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public bool EnableViewStateMac
	{
		get
		{
			return _viewStateMac;
		}
		set
		{
			_viewStateMac = value;
		}
	}

	internal bool EnableViewStateMacInternal
	{
		get
		{
			return _viewStateMac;
		}
		set
		{
			_viewStateMac = value;
		}
	}

	/// <summary>Gets or sets the error page to which the requesting browser is redirected in the event of an unhandled page exception.</summary>
	/// <returns>The error page to which the browser is redirected.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[DefaultValue("")]
	[WebSysDescription("The URL of a page used for error redirection.")]
	public string ErrorPage
	{
		get
		{
			return _errorPage;
		}
		set
		{
			HttpContext context = Context;
			_errorPage = value;
			if (context != null)
			{
				context.ErrorPage = value;
			}
		}
	}

	/// <summary>Sets an array of files that the current <see cref="T:System.Web.HttpResponse" /> object is dependent upon.</summary>
	/// <returns>The array of files that the current <see cref="T:System.Web.HttpResponse" /> object is dependent upon.</returns>
	[Obsolete("The recommended alternative is HttpResponse.AddFileDependencies. http://go.microsoft.com/fwlink/?linkid=14202")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	protected ArrayList FileDependencies
	{
		set
		{
			if (Response != null)
			{
				Response.AddFileDependencies(value);
			}
		}
	}

	/// <summary>Gets or sets an identifier for a particular instance of the <see cref="T:System.Web.UI.Page" /> class.</summary>
	/// <returns>The identifier for the instance of the <see cref="T:System.Web.UI.Page" /> class. The default value is '_Page'.</returns>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override string ID
	{
		get
		{
			return base.ID;
		}
		set
		{
			base.ID = value;
		}
	}

	/// <summary>Gets a value that indicates whether the page is being rendered for the first time or is being loaded in response to a postback.</summary>
	/// <returns>
	///     <see langword="true" /> if the page is being loaded in response to a client postback; otherwise, <see langword="false" />.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool IsPostBack => isPostBack;

	/// <summary>Gets a value that indicates whether the control in the page that performs postbacks has been registered.</summary>
	/// <returns>
	///     <see langword="true" /> if the control has been registered; otherwise, <see langword="false" />.</returns>
	public bool IsPostBackEventControlRegistered => requiresRaiseEvent != null;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.Page" /> object can be reused.</summary>
	/// <returns>
	///     <see langword="false" /> in all cases. </returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public bool IsReusable => false;

	/// <summary>Gets a value indicating whether page validation succeeded.</summary>
	/// <returns>
	///     <see langword="true" /> if page validation succeeded; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.Web.HttpException">The <see cref="P:System.Web.UI.Page.IsValid" /> property is called before validation has occurred.</exception>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool IsValid
	{
		get
		{
			if (!is_validated)
			{
				throw new HttpException(Locale.GetText("Page.IsValid cannot be called before validation has taken place. It should be queried in the event handler for a control that has CausesValidation=True and initiated the postback, or after a call to Page.Validate."));
			}
			foreach (IValidator validator in Validators)
			{
				if (!validator.IsValid)
				{
					return false;
				}
			}
			return true;
		}
	}

	/// <summary>Gets a list of objects stored in the page context.</summary>
	/// <returns>A reference to an <see cref="T:System.Collections.IDictionary" /> containing objects stored in the page context.</returns>
	[Browsable(false)]
	public IDictionary Items
	{
		get
		{
			if (items == null)
			{
				items = new Hashtable();
			}
			return items;
		}
	}

	/// <summary>Sets the locale identifier for the <see cref="T:System.Threading.Thread" /> object associated with the page.</summary>
	/// <returns>The locale identifier to pass to the <see cref="T:System.Threading.Thread" />.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public int LCID
	{
		get
		{
			return Thread.CurrentThread.CurrentCulture.LCID;
		}
		set
		{
			Thread.CurrentThread.CurrentCulture = new CultureInfo(value);
		}
	}

	/// <summary>Gets or sets a value indicating whether to return the user to the same position in the client browser after postback. This property replaces the obsolete <see cref="P:System.Web.UI.Page.SmartNavigation" /> property.</summary>
	/// <returns>
	///     <see langword="true" /> if the client position should be maintained; otherwise, <see langword="false" />.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool MaintainScrollPositionOnPostBack
	{
		get
		{
			return _maintainScrollPositionOnPostBack;
		}
		set
		{
			_maintainScrollPositionOnPostBack = value;
		}
	}

	/// <summary>Gets the adapter that renders the page for the specific requesting browser.</summary>
	/// <returns>The <see cref="T:System.Web.UI.Adapters.PageAdapter" /> that renders the page.</returns>
	public PageAdapter PageAdapter => base.Adapter as PageAdapter;

	internal string WebFormScriptReference
	{
		get
		{
			if (_webFormScriptReference == null)
			{
				_webFormScriptReference = (IsMultiForm ? theForm : "window");
			}
			return _webFormScriptReference;
		}
	}

	internal string ValidationStartupScript
	{
		get
		{
			if (_validationStartupScript == null)
			{
				_validationStartupScript = "\n" + WebFormScriptReference + ".Page_ValidationActive = false;\n" + WebFormScriptReference + ".ValidatorOnLoad();\n" + WebFormScriptReference + ".ValidatorOnSubmit = function () {\n\tif (this.Page_ValidationActive) {\n\t\treturn this.ValidatorCommonOnSubmit();\n\t}\n\treturn true;\n};\n";
			}
			return _validationStartupScript;
		}
	}

	internal string ValidationOnSubmitStatement
	{
		get
		{
			if (_validationOnSubmitStatement == null)
			{
				_validationOnSubmitStatement = "if (!" + WebFormScriptReference + ".ValidatorOnSubmit()) return false;";
			}
			return _validationOnSubmitStatement;
		}
	}

	internal string ValidationInitializeScript
	{
		get
		{
			if (_validationInitializeScript == null)
			{
				_validationInitializeScript = "WebFormValidation_Initialize(" + WebFormScriptReference + ");";
			}
			return _validationInitializeScript;
		}
	}

	internal IScriptManager ScriptManager => (IScriptManager)Items[typeof(IScriptManager)];

	internal string theForm => "theForm";

	internal bool IsMultiForm => false;

	/// <summary>Gets the <see cref="T:System.Web.HttpRequest" /> object for the requested page.</summary>
	/// <returns>The current <see cref="T:System.Web.HttpRequest" /> associated with the page.</returns>
	/// <exception cref="T:System.Web.HttpException">Occurs when the <see cref="T:System.Web.HttpRequest" /> object is not available. </exception>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public HttpRequest Request
	{
		get
		{
			if (_request == null)
			{
				throw new HttpException("Request is not available in this context.");
			}
			return RequestInternal;
		}
	}

	internal HttpRequest RequestInternal => _request;

	/// <summary>Gets the <see cref="T:System.Web.HttpResponse" /> object associated with the <see cref="T:System.Web.UI.Page" /> object. This object allows you to send HTTP response data to a client and contains information about that response.</summary>
	/// <returns>The current <see cref="T:System.Web.HttpResponse" /> associated with the page.</returns>
	/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Web.HttpResponse" /> object is not available. </exception>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public HttpResponse Response
	{
		get
		{
			if (_response == null)
			{
				throw new HttpException("Response is not available in this context.");
			}
			return _response;
		}
	}

	/// <summary>Sets the encoding language for the current <see cref="T:System.Web.HttpResponse" /> object.</summary>
	/// <returns>A string that contains the encoding language for the current <see cref="T:System.Web.HttpResponse" />.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public string ResponseEncoding
	{
		get
		{
			return Response.ContentEncoding.WebName;
		}
		set
		{
			Response.ContentEncoding = Encoding.GetEncoding(value);
		}
	}

	/// <summary>Gets the <see langword="Server" /> object, which is an instance of the <see cref="T:System.Web.HttpServerUtility" /> class.</summary>
	/// <returns>The current <see langword="Server" /> object associated with the page.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public HttpServerUtility Server => Context.Server;

	/// <summary>Gets the current <see langword="Session" /> object provided by ASP.NET.</summary>
	/// <returns>The current session-state data.</returns>
	/// <exception cref="T:System.Web.HttpException">Occurs when the session information is set to <see langword="null" />. </exception>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public virtual HttpSessionState Session
	{
		get
		{
			if (_session != null)
			{
				return _session;
			}
			try
			{
				_session = Context.Session;
			}
			catch
			{
			}
			if (_session == null)
			{
				throw new HttpException("Session state can only be used when enableSessionState is set to true, either in a configuration file or in the Page directive.");
			}
			return _session;
		}
	}

	/// <summary>Gets or sets a value indicating whether smart navigation is enabled. This property is deprecated.</summary>
	/// <returns>
	///     <see langword="true" /> if smart navigation is enabled; otherwise, <see langword="false" />.</returns>
	[Filterable(false)]
	[Obsolete("The recommended alternative is Page.SetFocus and Page.MaintainScrollPositionOnPostBack. http://go.microsoft.com/fwlink/?linkid=14202")]
	[Browsable(false)]
	public bool SmartNavigation
	{
		get
		{
			return _smartNavigation;
		}
		set
		{
			_smartNavigation = value;
		}
	}

	/// <summary>Gets or sets the name of the theme that is applied to the page early in the page life cycle.</summary>
	/// <returns>The name of the theme that is applied to the page early in the page life cycle.</returns>
	/// <exception cref="T:System.InvalidOperationException">An attempt was made to set the <see cref="P:System.Web.UI.Page.StyleSheetTheme" /> property after the <see cref="M:System.Web.UI.Page.FrameworkInitialize" /> method was called.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <see cref="P:System.Web.UI.Page.StyleSheetTheme" /> is set to an invalid theme name. This exception is thrown when the <see cref="M:System.Web.UI.Page.FrameworkInitialize" /> method is called, not by the property setter.</exception>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Filterable(false)]
	[Browsable(false)]
	public virtual string StyleSheetTheme
	{
		get
		{
			return _styleSheetTheme;
		}
		set
		{
			_styleSheetTheme = value;
		}
	}

	/// <summary>Gets or sets the name of the page theme.</summary>
	/// <returns>The name of the page theme.</returns>
	/// <exception cref="T:System.InvalidOperationException">An attempt was made to set <see cref="P:System.Web.UI.Page.Theme" /> after the <see cref="E:System.Web.UI.Page.PreInit" /> event has occurred.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <see cref="P:System.Web.UI.Page.Theme" /> is set to an invalid theme name.</exception>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public virtual string Theme
	{
		get
		{
			return _theme;
		}
		set
		{
			_theme = value;
		}
	}

	/// <summary>Gets or sets the control in the page that is used to perform postbacks.</summary>
	/// <returns>The control that is used to perform postbacks.</returns>
	public Control AutoPostBackControl
	{
		get
		{
			return _autoPostBackControl;
		}
		set
		{
			_autoPostBackControl = value;
		}
	}

	/// <summary>Gets the <see cref="P:System.Web.Routing.RequestContext.RouteData" /> value of the current <see cref="T:System.Web.Routing.RequestContext" /> instance.</summary>
	/// <returns>The <see cref="P:System.Web.Routing.RequestContext.RouteData" /> value of the current <see cref="T:System.Web.Routing.RequestContext" /> instance.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public RouteData RouteData
	{
		get
		{
			if (_request == null)
			{
				return null;
			}
			return _request.RequestContext?.RouteData;
		}
	}

	/// <summary>Gets or sets the content of the "description" <see langword="meta" /> element.</summary>
	/// <returns>The content of the "description" <see langword="meta" /> element.</returns>
	/// <exception cref="T:System.InvalidOperationException">The page does not have a header control (a <see langword="head" /> element with the <see langword="runat" /> attribute set to "server"). </exception>
	[Bindable(true)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Localizable(true)]
	public string MetaDescription
	{
		get
		{
			if (_metaDescription == null)
			{
				if (htmlHeader == null)
				{
					if (frameworkInitialized)
					{
						throw new InvalidOperationException("A server-side head element is required to set this property.");
					}
					return string.Empty;
				}
				return htmlHeader.Description;
			}
			return _metaDescription;
		}
		set
		{
			if (htmlHeader == null)
			{
				if (frameworkInitialized)
				{
					throw new InvalidOperationException("A server-side head element is required to set this property.");
				}
				_metaDescription = value;
			}
			else
			{
				htmlHeader.Description = value;
			}
		}
	}

	/// <summary>Gets or sets the content of the "keywords" <see langword="meta" /> element.</summary>
	/// <returns>The content of the "keywords" <see langword="meta" /> element.</returns>
	/// <exception cref="T:System.InvalidOperationException">The page does not have a header control (a <see langword="head" /> element with the <see langword="runat" /> attribute set to "server"). </exception>
	[Bindable(true)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Localizable(true)]
	public string MetaKeywords
	{
		get
		{
			if (_metaKeywords == null)
			{
				if (htmlHeader == null)
				{
					if (frameworkInitialized)
					{
						throw new InvalidOperationException("A server-side head element is required to set this property.");
					}
					return string.Empty;
				}
				return htmlHeader.Keywords;
			}
			return _metaDescription;
		}
		set
		{
			if (htmlHeader == null)
			{
				if (frameworkInitialized)
				{
					throw new InvalidOperationException("A server-side head element is required to set this property.");
				}
				_metaKeywords = value;
			}
			else
			{
				htmlHeader.Keywords = value;
			}
		}
	}

	/// <summary>Gets or sets the title for the page.</summary>
	/// <returns>The title of the page.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.Page.Title" /> property requires a header control on the page.</exception>
	[Localizable(true)]
	[Bindable(true)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public string Title
	{
		get
		{
			if (_title == null)
			{
				if (htmlHeader != null && htmlHeader.Title != null)
				{
					return htmlHeader.Title;
				}
				return string.Empty;
			}
			return _title;
		}
		set
		{
			if (htmlHeader != null)
			{
				htmlHeader.Title = value;
			}
			else
			{
				_title = value;
			}
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.TraceContext" /> object for the current Web request.</summary>
	/// <returns>Data from the <see cref="T:System.Web.TraceContext" /> object for the current Web request.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public TraceContext Trace => Context.Trace;

	/// <summary>Sets a value indicating whether tracing is enabled for the <see cref="T:System.Web.UI.Page" /> object.</summary>
	/// <returns>
	///     <see langword="true" /> if tracing is enabled for the page; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool TraceEnabled
	{
		get
		{
			return Trace.IsEnabled;
		}
		set
		{
			Trace.IsEnabled = value;
		}
	}

	/// <summary>Sets the mode in which trace statements are displayed on the page.</summary>
	/// <returns>One of the <see cref="T:System.Web.TraceMode" /> enumeration members.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public TraceMode TraceModeValue
	{
		get
		{
			return Trace.TraceMode;
		}
		set
		{
			Trace.TraceMode = value;
		}
	}

	/// <summary>Sets the level of transaction support for the page.</summary>
	/// <returns>An integer that represents one of the <see cref="T:System.EnterpriseServices.TransactionOption" /> enumeration members.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	protected int TransactionMode
	{
		get
		{
			return _transactionMode;
		}
		set
		{
			_transactionMode = value;
		}
	}

	/// <summary>Sets the user interface (UI) ID for the <see cref="T:System.Threading.Thread" /> object associated with the page.</summary>
	/// <returns>The UI ID.</returns>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public string UICulture
	{
		get
		{
			return Thread.CurrentThread.CurrentUICulture.Name;
		}
		set
		{
			Thread.CurrentThread.CurrentUICulture = GetPageCulture(value, Thread.CurrentThread.CurrentUICulture);
		}
	}

	/// <summary>Gets information about the user making the page request.</summary>
	/// <returns>An <see cref="T:System.Security.Principal.IPrincipal" /> that represents the user making the page request.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public IPrincipal User => Context.User;

	/// <summary>Gets a collection of all validation controls contained on the requested page.</summary>
	/// <returns>The collection of validation controls.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public ValidatorCollection Validators
	{
		get
		{
			if (_validators == null)
			{
				_validators = new ValidatorCollection();
			}
			return _validators;
		}
	}

	/// <summary>Assigns an identifier to an individual user in the view-state variable associated with the current page.</summary>
	/// <returns>The identifier for the individual user.</returns>
	/// <exception cref="T:System.Web.HttpException">The <see cref="P:System.Web.UI.Page.ViewStateUserKey" /> property was accessed too late during page processing. </exception>
	[MonoTODO("Use this when encrypting/decrypting ViewState")]
	[Browsable(false)]
	public string ViewStateUserKey
	{
		get
		{
			return viewStateUserKey;
		}
		set
		{
			viewStateUserKey = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.Page" /> object is rendered.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.Page" /> is to be rendered; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[Browsable(false)]
	public override bool Visible
	{
		get
		{
			return base.Visible;
		}
		set
		{
			base.Visible = value;
		}
	}

	internal string RawViewState
	{
		get
		{
			NameValueCollection requestValueCollection = _requestValueCollection;
			string text;
			if (requestValueCollection == null || (text = requestValueCollection["__VIEWSTATE"]) == null)
			{
				return null;
			}
			if (text == string.Empty)
			{
				return null;
			}
			return text;
		}
		set
		{
			_savedViewState = value;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.UI.PageStatePersister" /> object associated with the page.</summary>
	/// <returns>A <see cref="T:System.Web.UI.PageStatePersister" /> associated with the page.</returns>
	protected virtual PageStatePersister PageStatePersister
	{
		get
		{
			if (page_state_persister == null && PageAdapter != null)
			{
				page_state_persister = PageAdapter.GetStatePersister();
			}
			if (page_state_persister == null)
			{
				page_state_persister = new HiddenFieldPageStatePersister(this);
			}
			return page_state_persister;
		}
	}

	/// <summary>Gets a <see cref="T:System.Web.UI.ClientScriptManager" /> object used to manage, register, and add script to the page.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ClientScriptManager" /> object.</returns>
	public ClientScriptManager ClientScript => scriptManager;

	/// <summary>Gets the HTML form for the page.</summary>
	/// <returns>The <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> object associated with the page.</returns>
	public HtmlForm Form => _form;

	/// <summary>Gets the query string portion of the requested URL.</summary>
	/// <returns>The query string portion of the requested URL.</returns>
	public string ClientQueryString => Request.UrlComponents.Query;

	/// <summary>Gets the page that transferred control to the current page.</summary>
	/// <returns>The <see cref="T:System.Web.UI.Page" /> representing the page that transferred control to the current page.</returns>
	/// <exception cref="T:System.InvalidOperationException">The current user is not allowed to access the previous page.-or-ASP.NET routing is in use and the previous page's URL is a routed URL. When ASP.NET checks access permissions, it assumes that the URL is an actual path to a file. Because this is not the case with a routed URL, the check fails.</exception>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Page PreviousPage
	{
		get
		{
			if (_doLoadPreviousPage)
			{
				_doLoadPreviousPage = false;
				LoadPreviousPageReference();
			}
			return previousPage;
		}
	}

	/// <summary>Gets a value that indicates whether the page request is the result of a callback.</summary>
	/// <returns>
	///     <see langword="true" /> if the page request is the result of a callback; otherwise, <see langword="false" />.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool IsCallback => isCallback;

	/// <summary>Gets a value indicating whether the page is involved in a cross-page postback.</summary>
	/// <returns>
	///     <see langword="true" /> if the page is participating in a cross-page request; otherwise, <see langword="false" />.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool IsCrossPagePostBack => isCrossPagePostBack;

	/// <summary>Gets the character used to separate control identifiers when building a unique ID for a control on a page.</summary>
	/// <returns>The character used to separate control identifiers. The default is set by the <see cref="T:System.Web.UI.Adapters.PageAdapter" /> instance that renders the page. The <see cref="P:System.Web.UI.Page.IdSeparator" /> is a server-side field and should not be modified.</returns>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public new virtual char IdSeparator => base.IdSeparator;

	/// <summary>Gets the document header for the page if the <see langword="head" /> element is defined with a <see langword="runat=server" /> in the page declaration.</summary>
	/// <returns>An <see cref="T:System.Web.UI.HtmlControls.HtmlHead" /> containing the page header.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public HtmlHead Header => htmlHeader;

	/// <summary>Sets a value indicating whether the page is processed synchronously or asynchronously.</summary>
	/// <returns>
	///     <see langword="true" /> if the page is processed asynchronously; otherwise, <see langword="false" />.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	protected bool AsyncMode
	{
		get
		{
			return asyncMode;
		}
		set
		{
			asyncMode = value;
		}
	}

	/// <summary>Gets or sets a value indicating the time-out interval used when processing asynchronous tasks.</summary>
	/// <returns>A <see cref="T:System.TimeSpan" /> that contains the allowed time interval for completion of the asynchronous task. The default time interval is 45 seconds.</returns>
	/// <exception cref="T:System.ArgumentException">The property was set to a negative value.</exception>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public TimeSpan AsyncTimeout
	{
		get
		{
			return asyncTimeout;
		}
		set
		{
			asyncTimeout = value;
		}
	}

	/// <summary>Gets a value indicating whether the page is processed asynchronously.</summary>
	/// <returns>
	///     <see langword="true" /> if the page is in asynchronous mode; otherwise, <see langword="false" />;</returns>
	public bool IsAsync => AsyncMode;

	/// <summary>Gets a unique suffix to append to the file path for caching browsers.</summary>
	/// <returns>A unique suffix appended to the file path. The default is "__ufps=" plus a unique 6-digit number.</returns>
	protected internal virtual string UniqueFilePathSuffix
	{
		get
		{
			if (string.IsNullOrEmpty(uniqueFilePathSuffix))
			{
				uniqueFilePathSuffix = "__ufps=" + base.AppRelativeVirtualPath.GetHashCode().ToString("x");
			}
			return uniqueFilePathSuffix;
		}
	}

	/// <summary>Gets or sets the maximum length for the page's state field.</summary>
	/// <returns>The maximum length, in bytes, for the page's state field. The default is -1.</returns>
	/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Web.UI.Page.MaxPageStateFieldLength" /> property is not equal to -1 or a positive number.</exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.Page.MaxPageStateFieldLength" /> property was set after the page was initialized.</exception>
	[MonoTODO("Actually use the value in code.")]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public int MaxPageStateFieldLength
	{
		get
		{
			return maxPageStateFieldLength;
		}
		set
		{
			maxPageStateFieldLength = value;
		}
	}

	private List<PageAsyncTask> ParallelTasks
	{
		get
		{
			if (parallelTasks == null)
			{
				parallelTasks = new List<PageAsyncTask>();
			}
			return parallelTasks;
		}
	}

	private List<PageAsyncTask> SerialTasks
	{
		get
		{
			if (serialTasks == null)
			{
				serialTasks = new List<PageAsyncTask>();
			}
			return serialTasks;
		}
	}

	/// <summary>Gets or sets the encryption mode of the view state.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.ViewStateEncryptionMode" /> values. The default value is <see cref="F:System.Web.UI.ViewStateEncryptionMode.Auto" />. </returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The value set is not a member of the <see cref="T:System.Web.UI.ViewStateEncryptionMode" /> enumeration.</exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.Page.ViewStateEncryptionMode" /> property can be set only in or before the page <see langword="PreRender" />phase in the page life cycle.</exception>
	[Browsable(false)]
	[DefaultValue("0")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public ViewStateEncryptionMode ViewStateEncryptionMode
	{
		get
		{
			return viewStateEncryptionMode;
		}
		set
		{
			viewStateEncryptionMode = value;
		}
	}

	internal bool NeedViewStateEncryption
	{
		get
		{
			if (ViewStateEncryptionMode != ViewStateEncryptionMode.Always)
			{
				if (ViewStateEncryptionMode == ViewStateEncryptionMode.Auto)
				{
					return controlRegisteredForViewStateEncryption;
				}
				return false;
			}
			return true;
		}
	}

	/// <summary>Gets or sets the virtual path of the master page.</summary>
	/// <returns>The virtual path of the master page.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.Page.MasterPageFile" /> property is set after the <see cref="E:System.Web.UI.Page.PreInit" /> event is complete.</exception>
	/// <exception cref="T:System.Web.HttpException">The file specified in the <see cref="P:System.Web.UI.Page.MasterPageFile" /> property does not exist.- or -The page does not have a <see cref="T:System.Web.UI.WebControls.Content" /> control as the top level control.</exception>
	[DefaultValue("")]
	public virtual string MasterPageFile
	{
		get
		{
			return masterPageFile;
		}
		set
		{
			masterPageFile = value;
			masterPage = null;
		}
	}

	/// <summary>Gets the master page that determines the overall look of the page.</summary>
	/// <returns>The <see cref="T:System.Web.UI.MasterPage" /> associated with this page if it exists; otherwise, <see langword="null" />. </returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public MasterPage Master
	{
		get
		{
			if (Context == null || string.IsNullOrEmpty(masterPageFile))
			{
				return null;
			}
			if (masterPage == null)
			{
				masterPage = MasterPage.CreateMasterPage(this, Context, masterPageFile, contentTemplates);
			}
			return masterPage;
		}
	}

	internal PageTheme PageTheme => _pageTheme;

	internal PageTheme StyleSheetPageTheme => _styleSheetPageTheme;

	/// <summary>Occurs when page initialization is complete.</summary>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public event EventHandler InitComplete
	{
		add
		{
			event_mask |= 1;
			base.Events.AddHandler(InitCompleteEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(InitCompleteEvent, value);
		}
	}

	/// <summary>Occurs at the end of the load stage of the page's life cycle.</summary>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public event EventHandler LoadComplete
	{
		add
		{
			event_mask |= 2;
			base.Events.AddHandler(LoadCompleteEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(LoadCompleteEvent, value);
		}
	}

	/// <summary>Occurs before page initialization.</summary>
	public event EventHandler PreInit
	{
		add
		{
			event_mask |= 4;
			base.Events.AddHandler(PreInitEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(PreInitEvent, value);
		}
	}

	/// <summary>Occurs before the page <see cref="E:System.Web.UI.Control.Load" /> event.</summary>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public event EventHandler PreLoad
	{
		add
		{
			event_mask |= 8;
			base.Events.AddHandler(PreLoadEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(PreLoadEvent, value);
		}
	}

	/// <summary>Occurs before the page content is rendered.</summary>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public event EventHandler PreRenderComplete
	{
		add
		{
			event_mask |= 16;
			base.Events.AddHandler(PreRenderCompleteEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(PreRenderCompleteEvent, value);
		}
	}

	/// <summary>Occurs after the page has completed saving all view state and control state information for the page and controls on the page.</summary>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public event EventHandler SaveStateComplete
	{
		add
		{
			event_mask |= 32;
			base.Events.AddHandler(SaveStateCompleteEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(SaveStateCompleteEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Page" /> class.</summary>
	public Page()
	{
		scriptManager = new ClientScriptManager(this);
		Page = this;
		ID = "__Page";
		if (WebConfigurationManager.GetSection("system.web/pages") is PagesSection pagesSection)
		{
			asyncTimeout = pagesSection.AsyncTimeout;
			viewStateEncryptionMode = pagesSection.ViewStateEncryptionMode;
			_viewState = pagesSection.EnableViewState;
			_viewStateMac = pagesSection.EnableViewStateMac;
		}
		else
		{
			asyncTimeout = TimeSpan.FromSeconds(45.0);
			viewStateEncryptionMode = ViewStateEncryptionMode.Auto;
			_viewState = true;
		}
		ViewStateMode = ViewStateMode.Enabled;
	}

	private void InitializeStyleSheet()
	{
		if (_styleSheetTheme == null && WebConfigurationManager.GetSection("system.web/pages") is PagesSection pagesSection)
		{
			_styleSheetTheme = pagesSection.StyleSheetTheme;
		}
		if (!string.IsNullOrEmpty(_styleSheetTheme))
		{
			string virtualPath = "~/App_Themes/" + _styleSheetTheme;
			_styleSheetPageTheme = BuildManager.CreateInstanceFromVirtualPath(virtualPath, typeof(PageTheme)) as PageTheme;
		}
	}

	private void InitializeTheme()
	{
		if (_theme == null && WebConfigurationManager.GetSection("system.web/pages") is PagesSection pagesSection)
		{
			_theme = pagesSection.Theme;
		}
		if (!string.IsNullOrEmpty(_theme))
		{
			string virtualPath = "~/App_Themes/" + _theme;
			_pageTheme = BuildManager.CreateInstanceFromVirtualPath(virtualPath, typeof(PageTheme)) as PageTheme;
			if (_pageTheme != null)
			{
				_pageTheme.SetPage(this);
			}
		}
	}

	private CultureInfo GetPageCulture(string culture, CultureInfo deflt)
	{
		if (culture == null)
		{
			return deflt;
		}
		CultureInfo cultureInfo = null;
		if (culture.StartsWith("auto", StringComparison.InvariantCultureIgnoreCase))
		{
			string[] userLanguages = Request.UserLanguages;
			try
			{
				if (userLanguages != null && userLanguages.Length != 0)
				{
					cultureInfo = CultureInfo.CreateSpecificCulture(userLanguages[0]);
				}
			}
			catch
			{
			}
			if (cultureInfo == null)
			{
				cultureInfo = deflt;
			}
		}
		else
		{
			cultureInfo = CultureInfo.CreateSpecificCulture(culture);
		}
		return cultureInfo;
	}

	/// <summary>Initiates a request for Active Server Page (ASP) resources. This method is provided for compatibility with legacy ASP applications.</summary>
	/// <param name="context">An <see cref="T:System.Web.HttpContext" /> with information about the current request. </param>
	/// <param name="cb">The callback method. </param>
	/// <param name="extraData">Any extra data needed to process the request in the same manner as an ASP request. </param>
	/// <returns>An <see cref="T:System.IAsyncResult" /> object.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	protected IAsyncResult AspCompatBeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
	{
		throw new NotImplementedException();
	}

	/// <summary>Terminates a request for Active Server Page (ASP) resources. This method is provided for compatibility with legacy ASP applications.</summary>
	/// <param name="result">The ASP page generated by the request. </param>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[MonoNotSupported("Mono does not support classic ASP compatibility mode.")]
	protected void AspCompatEndProcessRequest(IAsyncResult result)
	{
		throw new NotImplementedException();
	}

	/// <summary>Creates an <see cref="T:System.Web.UI.HtmlTextWriter" /> object to render the page's content.</summary>
	/// <param name="tw">The <see cref="T:System.IO.TextWriter" /> used to create the <see cref="T:System.Web.UI.HtmlTextWriter" />.</param>
	/// <returns>An <see cref="T:System.Web.UI.HtmlTextWriter" /> or <see cref="T:System.Web.UI.Html32TextWriter" />.</returns>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual HtmlTextWriter CreateHtmlTextWriter(TextWriter tw)
	{
		if (Request.BrowserMightHaveSpecialWriter)
		{
			return Request.Browser.CreateHtmlTextWriter(tw);
		}
		return new HtmlTextWriter(tw);
	}

	/// <summary>Performs any initialization of the instance of the <see cref="T:System.Web.UI.Page" /> class that is required by RAD designers. This method is used only at design time.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public void DesignerInitialize()
	{
		InitRecursive(null);
	}

	/// <summary>Returns a <see cref="T:System.Collections.Specialized.NameValueCollection" /> of data posted back to the page using either a POST or a GET command. </summary>
	/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> object that contains the form data. If the postback used the POST command, the form information is returned from the <see cref="P:System.Web.UI.Page.Context" /> object. If the postback used the GET command, the query string information is returned. If the page is being requested for the first time, <see langword="null" /> is returned.</returns>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected internal virtual NameValueCollection DeterminePostBackMode()
	{
		if (_context.IsProcessingInclude)
		{
			return null;
		}
		HttpRequest request = Request;
		if (request == null)
		{
			return null;
		}
		NameValueCollection nameValueCollection = null;
		if (string.Compare(Request.HttpMethod, "POST", ignoreCase: true, Helpers.InvariantCulture) == 0)
		{
			nameValueCollection = request.Form;
		}
		else
		{
			string queryStringRaw = Request.QueryStringRaw;
			if (queryStringRaw == null || queryStringRaw.Length == 0)
			{
				return null;
			}
			nameValueCollection = request.QueryString;
		}
		WebROCollection webROCollection = (WebROCollection)nameValueCollection;
		allow_load = !webROCollection.GotID;
		if (allow_load)
		{
			webROCollection.ID = GetTypeHashCode();
		}
		else
		{
			allow_load = webROCollection.ID == GetTypeHashCode();
		}
		if (nameValueCollection != null && nameValueCollection["__VIEWSTATE"] == null && nameValueCollection["__EVENTTARGET"] == null)
		{
			return null;
		}
		return nameValueCollection;
	}

	/// <summary>Searches the page naming container for a server control with the specified identifier.</summary>
	/// <param name="id">The identifier for the control to be found. </param>
	/// <returns>The specified control, or <see langword="null" /> if the specified control does not exist.</returns>
	public override Control FindControl(string id)
	{
		if (id == ID)
		{
			return this;
		}
		return base.FindControl(id);
	}

	private Control FindControl(string id, bool decode)
	{
		return FindControl(id);
	}

	/// <summary>Gets a reference that can be used in a client event to post back to the server for the specified control and with the specified event arguments.</summary>
	/// <param name="control">The server control that receives the client event postback. </param>
	/// <param name="argument">A <see cref="T:System.String" /> that is passed to <see cref="M:System.Web.UI.IPostBackEventHandler.RaisePostBackEvent(System.String)" />. </param>
	/// <returns>The string that represents the client event.</returns>
	[Obsolete("The recommended alternative is ClientScript.GetPostBackEventReference. http://go.microsoft.com/fwlink/?linkid=14202")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public string GetPostBackClientEvent(Control control, string argument)
	{
		return scriptManager.GetPostBackEventReference(control, argument);
	}

	/// <summary>Gets a reference, with <see langword="javascript:" /> appended to the beginning of it, that can be used in a client event to post back to the server for the specified control and with the specified event arguments.</summary>
	/// <param name="control">The server control to process the postback. </param>
	/// <param name="argument">The parameter passed to the server control. </param>
	/// <returns>A string representing a JavaScript call to the postback function that includes the target control's ID and event arguments.</returns>
	[Obsolete("The recommended alternative is ClientScript.GetPostBackClientHyperlink. http://go.microsoft.com/fwlink/?linkid=14202")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public string GetPostBackClientHyperlink(Control control, string argument)
	{
		return scriptManager.GetPostBackClientHyperlink(control, argument);
	}

	/// <summary>Returns a string that can be used in a client event to cause postback to the server. The reference string is defined by the specified <see cref="T:System.Web.UI.Control" /> object.</summary>
	/// <param name="control">The server control to process the postback on the server. </param>
	/// <returns>A string that, when treated as script on the client, initiates the postback.</returns>
	[Obsolete("The recommended alternative is ClientScript.GetPostBackEventReference. http://go.microsoft.com/fwlink/?linkid=14202")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public string GetPostBackEventReference(Control control)
	{
		return scriptManager.GetPostBackEventReference(control, string.Empty);
	}

	/// <summary>Returns a string that can be used in a client event to cause postback to the server. The reference string is defined by the specified control that handles the postback and a string argument of additional event information. </summary>
	/// <param name="control">The server control to process the postback. </param>
	/// <param name="argument">The parameter passed to the server control. </param>
	/// <returns>A string that, when treated as script on the client, initiates the postback.</returns>
	[Obsolete("The recommended alternative is ClientScript.GetPostBackEventReference. http://go.microsoft.com/fwlink/?linkid=14202")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public string GetPostBackEventReference(Control control, string argument)
	{
		return scriptManager.GetPostBackEventReference(control, argument);
	}

	internal void RequiresFormScriptDeclaration()
	{
		requiresFormScriptDeclaration = true;
	}

	internal void RequiresPostBackScript()
	{
		if (!requiresPostBackScript)
		{
			ClientScript.RegisterHiddenField("__EVENTTARGET", string.Empty);
			ClientScript.RegisterHiddenField("__EVENTARGUMENT", string.Empty);
			requiresPostBackScript = true;
			RequiresFormScriptDeclaration();
		}
	}

	/// <summary>Retrieves a hash code that is generated by <see cref="T:System.Web.UI.Page" /> objects that are generated at run time. This hash code is unique to the <see cref="T:System.Web.UI.Page" /> object's control hierarchy.</summary>
	/// <returns>The hash code generated at run time. The default is 0.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual int GetTypeHashCode()
	{
		return 0;
	}

	/// <summary>Initializes the output cache for the current page request based on an <see cref="T:System.Web.UI.OutputCacheParameters" /> object.</summary>
	/// <param name="cacheSettings">An <see cref="T:System.Web.UI.OutputCacheParameters" /> that contains the cache settings.</param>
	/// <exception cref="T:System.Web.HttpException">The cache profile was not found.- or -A missing directive or configuration settings profile attribute.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The output cache settings location is invalid. </exception>
	[MonoTODO("The following properties of OutputCacheParameters are silently ignored: CacheProfile, SqlDependency")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	protected internal virtual void InitOutputCache(OutputCacheParameters cacheSettings)
	{
		if (cacheSettings.Enabled)
		{
			InitOutputCache(cacheSettings.Duration, cacheSettings.VaryByContentEncoding, cacheSettings.VaryByHeader, cacheSettings.VaryByCustom, cacheSettings.Location, cacheSettings.VaryByParam);
			HttpCachePolicy httpCachePolicy = Response?.Cache;
			if (httpCachePolicy != null && cacheSettings.NoStore)
			{
				httpCachePolicy.SetNoStore();
			}
		}
	}

	/// <summary>Initializes the output cache for the current page request.</summary>
	/// <param name="duration">The amount of time that objects stored in the output cache are valid.</param>
	/// <param name="varyByContentEncoding">A semicolon-separated list of character-sets (content encodings) that content from the output cache will vary by.</param>
	/// <param name="varyByHeader">A semicolon-separated list of headers that content from the output cache will vary by.</param>
	/// <param name="varyByCustom">The <see langword="Vary" /> HTTP header.</param>
	/// <param name="location">One of the <see cref="T:System.Web.UI.OutputCacheLocation" /> values.</param>
	/// <param name="varyByParam">A semicolon-separated list of parameters received by a GET or POST method that content from the output cache will vary by.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">An invalid value is specified for <paramref name="location" />. </exception>
	[MonoTODO("varyByContentEncoding is not currently used")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	protected virtual void InitOutputCache(int duration, string varyByContentEncoding, string varyByHeader, string varyByCustom, OutputCacheLocation location, string varyByParam)
	{
		if (duration <= 0)
		{
			return;
		}
		HttpResponse response = Response;
		HttpCachePolicy cache = response.Cache;
		bool flag = false;
		DateTime lastModified = Context?.Timestamp ?? DateTime.Now;
		switch (location)
		{
		case OutputCacheLocation.Any:
			cache.SetCacheability(HttpCacheability.Public);
			cache.SetMaxAge(new TimeSpan(0, 0, duration));
			cache.SetLastModified(lastModified);
			flag = true;
			break;
		case OutputCacheLocation.Client:
			cache.SetCacheability(HttpCacheability.Private);
			cache.SetMaxAge(new TimeSpan(0, 0, duration));
			cache.SetLastModified(lastModified);
			break;
		case OutputCacheLocation.Downstream:
			cache.SetCacheability(HttpCacheability.Public);
			cache.SetMaxAge(new TimeSpan(0, 0, duration));
			cache.SetLastModified(lastModified);
			break;
		case OutputCacheLocation.Server:
			cache.SetCacheability(HttpCacheability.Server);
			flag = true;
			break;
		}
		if (flag)
		{
			if (varyByCustom != null)
			{
				cache.SetVaryByCustom(varyByCustom);
			}
			if (varyByParam != null && varyByParam.Length > 0)
			{
				string[] array = varyByParam.Split(';');
				foreach (string text in array)
				{
					cache.VaryByParams[text.Trim()] = true;
				}
				cache.VaryByParams.IgnoreParams = false;
			}
			else
			{
				cache.VaryByParams.IgnoreParams = true;
			}
			if (varyByHeader != null && varyByHeader.Length > 0)
			{
				string[] array = varyByHeader.Split(';');
				foreach (string text2 in array)
				{
					cache.VaryByHeaders[text2.Trim()] = true;
				}
			}
			if (PageAdapter != null)
			{
				if (PageAdapter.CacheVaryByParams != null)
				{
					StringEnumerator enumerator = PageAdapter.CacheVaryByParams.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							string current = enumerator.Current;
							cache.VaryByParams[current] = true;
						}
					}
					finally
					{
						if (enumerator is IDisposable disposable)
						{
							disposable.Dispose();
						}
					}
				}
				if (PageAdapter.CacheVaryByHeaders != null)
				{
					StringEnumerator enumerator = PageAdapter.CacheVaryByHeaders.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							string current2 = enumerator.Current;
							cache.VaryByHeaders[current2] = true;
						}
					}
					finally
					{
						if (enumerator is IDisposable disposable2)
						{
							disposable2.Dispose();
						}
					}
				}
			}
		}
		response.IsCached = true;
		cache.Duration = duration;
		cache.SetExpires(lastModified.AddSeconds(duration));
	}

	/// <summary>Initializes the output cache for the current page request.</summary>
	/// <param name="duration">The amount of time that objects stored in the output cache are valid. </param>
	/// <param name="varyByHeader">A semicolon-separated list of headers that content from the output cache will vary by. </param>
	/// <param name="varyByCustom">The <see langword="Vary" /> HTTP header. </param>
	/// <param name="location">One of the <see cref="T:System.Web.UI.OutputCacheLocation" /> values. </param>
	/// <param name="varyByParam">A semicolon-separated list of parameters received by a GET or POST method that content from the output cache will vary by.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">An invalid value is specified for <paramref name="location" />. </exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	protected virtual void InitOutputCache(int duration, string varyByHeader, string varyByCustom, OutputCacheLocation location, string varyByParam)
	{
		InitOutputCache(duration, null, varyByHeader, varyByCustom, location, varyByParam);
	}

	/// <summary>Determines whether the client script block with the specified key is registered with the page.</summary>
	/// <param name="key">The string key of the client script to search for. </param>
	/// <returns>
	///     <see langword="true" /> if the script block is registered; otherwise, <see langword="false" />.</returns>
	[Obsolete("The recommended alternative is ClientScript.IsClientScriptBlockRegistered(string key). http://go.microsoft.com/fwlink/?linkid=14202")]
	public bool IsClientScriptBlockRegistered(string key)
	{
		return scriptManager.IsClientScriptBlockRegistered(key);
	}

	/// <summary>Determines whether the client startup script is registered with the <see cref="T:System.Web.UI.Page" /> object.</summary>
	/// <param name="key">The string key of the startup script to search for. </param>
	/// <returns>
	///     <see langword="true" /> if the startup script is registered; otherwise, <see langword="false" />.</returns>
	[Obsolete("The recommended alternative is ClientScript.IsStartupScriptRegistered(string key). http://go.microsoft.com/fwlink/?linkid=14202")]
	public bool IsStartupScriptRegistered(string key)
	{
		return scriptManager.IsStartupScriptRegistered(key);
	}

	/// <summary>Retrieves the physical path that a virtual path, either absolute or relative, or an application-relative path maps to.</summary>
	/// <param name="virtualPath">A <see cref="T:System.String" /> that represents the virtual path. </param>
	/// <returns>The physical path associated with the virtual path or application-relative path.</returns>
	public string MapPath(string virtualPath)
	{
		return Request.MapPath(virtualPath);
	}

	/// <summary>Initializes the <see cref="T:System.Web.UI.HtmlTextWriter" /> object and calls on the child controls of the <see cref="T:System.Web.UI.Page" /> to render.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the page content.</param>
	protected internal override void Render(HtmlTextWriter writer)
	{
		if (MaintainScrollPositionOnPostBack)
		{
			ClientScript.RegisterWebFormClientScript();
			ClientScript.RegisterHiddenField("__SCROLLPOSITIONX", Request["__SCROLLPOSITIONX"]);
			ClientScript.RegisterHiddenField("__SCROLLPOSITIONY", Request["__SCROLLPOSITIONY"]);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("<script type=\"text/javascript\">");
			stringBuilder.AppendLine("//<![CDATA[");
			stringBuilder.AppendLine(theForm + ".oldSubmit = " + theForm + ".submit;");
			stringBuilder.AppendLine(theForm + ".submit = function () { " + WebFormScriptReference + ".WebForm_SaveScrollPositionSubmit(); }");
			stringBuilder.AppendLine(theForm + ".oldOnSubmit = " + theForm + ".onsubmit;");
			stringBuilder.AppendLine(theForm + ".onsubmit = function () { " + WebFormScriptReference + ".WebForm_SaveScrollPositionOnSubmit(); }");
			if (IsPostBack)
			{
				stringBuilder.AppendLine(theForm + ".oldOnLoad = window.onload;");
				stringBuilder.AppendLine("window.onload = function () { " + WebFormScriptReference + ".WebForm_RestoreScrollPosition (); };");
			}
			stringBuilder.AppendLine("//]]>");
			stringBuilder.AppendLine("</script>");
			ClientScript.RegisterStartupScript(typeof(Page), "MaintainScrollPositionOnPostBackStartup", stringBuilder.ToString());
		}
		base.Render(writer);
	}

	private void RenderPostBackScript(HtmlTextWriter writer, string formUniqueID)
	{
		writer.WriteLine();
		ClientScriptManager.WriteBeginScriptBlock(writer);
		RenderClientScriptFormDeclaration(writer, formUniqueID);
		writer.WriteLine(WebFormScriptReference + "._form = " + theForm + ";");
		writer.WriteLine(WebFormScriptReference + ".__doPostBack = function (eventTarget, eventArgument) {");
		writer.WriteLine("\tif(" + theForm + ".onsubmit && " + theForm + ".onsubmit() == false) return;");
		writer.WriteLine("\t" + theForm + ".__EVENTTARGET.value = eventTarget;");
		writer.WriteLine("\t" + theForm + ".__EVENTARGUMENT.value = eventArgument;");
		writer.WriteLine("\t" + theForm + ".submit();");
		writer.WriteLine("}");
		ClientScriptManager.WriteEndScriptBlock(writer);
	}

	private void RenderClientScriptFormDeclaration(HtmlTextWriter writer, string formUniqueID)
	{
		if (!formScriptDeclarationRendered)
		{
			if (PageAdapter != null)
			{
				writer.WriteLine("\tvar {0} = {1};\n", theForm, PageAdapter.GetPostBackFormReference(formUniqueID));
			}
			else
			{
				writer.WriteLine("\tvar {0};\n\tif (document.getElementById) {{ {0} = document.getElementById ('{1}'); }}", theForm, formUniqueID);
				writer.WriteLine("\telse {{ {0} = document.{1}; }}", theForm, formUniqueID);
			}
			formScriptDeclarationRendered = true;
		}
	}

	internal void OnFormRender(HtmlTextWriter writer, string formUniqueID)
	{
		if (renderingForm)
		{
			throw new HttpException("Only 1 HtmlForm is allowed per page.");
		}
		renderingForm = true;
		writer.WriteLine();
		if (requiresFormScriptDeclaration || (scriptManager != null && scriptManager.ScriptsPresent) || PageAdapter != null)
		{
			ClientScriptManager.WriteBeginScriptBlock(writer);
			RenderClientScriptFormDeclaration(writer, formUniqueID);
			ClientScriptManager.WriteEndScriptBlock(writer);
		}
		if (handleViewState)
		{
			scriptManager.RegisterHiddenField("__VIEWSTATE", _savedViewState);
		}
		scriptManager.WriteHiddenFields(writer);
		if (requiresPostBackScript)
		{
			RenderPostBackScript(writer, formUniqueID);
			postBackScriptRendered = true;
		}
		scriptManager.WriteWebFormClientScript(writer);
		scriptManager.WriteClientScriptBlocks(writer);
	}

	internal IStateFormatter GetFormatter()
	{
		return new ObjectStateFormatter(this);
	}

	internal string GetSavedViewState()
	{
		return _savedViewState;
	}

	internal void OnFormPostRender(HtmlTextWriter writer, string formUniqueID)
	{
		scriptManager.SaveEventValidationState();
		scriptManager.WriteExpandoAttributes(writer);
		scriptManager.WriteHiddenFields(writer);
		if (!postBackScriptRendered && requiresPostBackScript)
		{
			RenderPostBackScript(writer, formUniqueID);
		}
		scriptManager.WriteWebFormClientScript(writer);
		scriptManager.WriteArrayDeclares(writer);
		scriptManager.WriteStartupScriptBlocks(writer);
		renderingForm = false;
		postBackScriptRendered = false;
	}

	private void ProcessPostData(NameValueCollection data, bool second)
	{
		NameValueCollection postCollection = ((_requestValueCollection == null) ? new NameValueCollection(SecureHashCodeProvider.DefaultInvariant, CaseInsensitiveComparer.DefaultInvariant) : _requestValueCollection);
		if (data != null && data.Count > 0)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.Ordinal);
			string[] allKeys = data.AllKeys;
			foreach (string text in allKeys)
			{
				switch (text)
				{
				case "__VIEWSTATE":
				case "__EVENTTARGET":
				case "__EVENTARGUMENT":
				case "__EVENTVALIDATION":
					continue;
				}
				if (dictionary.ContainsKey(text))
				{
					continue;
				}
				dictionary.Add(text, text);
				Control control = FindControl(text, decode: true);
				if (control != null)
				{
					IPostBackDataHandler postBackDataHandler = control as IPostBackDataHandler;
					IPostBackEventHandler postBackEventHandler = control as IPostBackEventHandler;
					if (postBackDataHandler == null)
					{
						if (postBackEventHandler != null)
						{
							formPostedRequiresRaiseEvent = postBackEventHandler;
						}
						continue;
					}
					if (postBackDataHandler.LoadPostData(text, postCollection))
					{
						if (requiresPostDataChanged == null)
						{
							requiresPostDataChanged = new List<IPostBackDataHandler>();
						}
						requiresPostDataChanged.Add(postBackDataHandler);
					}
					if (_requiresPostBackCopy != null)
					{
						_requiresPostBackCopy.Remove(text);
					}
				}
				else if (!second)
				{
					if (secondPostData == null)
					{
						secondPostData = new NameValueCollection(SecureHashCodeProvider.DefaultInvariant, CaseInsensitiveComparer.DefaultInvariant);
					}
					secondPostData.Add(text, data[text]);
				}
			}
		}
		List<string> list = null;
		if (_requiresPostBackCopy != null && _requiresPostBackCopy.Count > 0)
		{
			string[] allKeys = _requiresPostBackCopy.ToArray();
			foreach (string text2 in allKeys)
			{
				if (FindControl(text2, decode: true) is IPostBackDataHandler postBackDataHandler2)
				{
					_requiresPostBackCopy.Remove(text2);
					if (postBackDataHandler2.LoadPostData(text2, postCollection))
					{
						if (requiresPostDataChanged == null)
						{
							requiresPostDataChanged = new List<IPostBackDataHandler>();
						}
						requiresPostDataChanged.Add(postBackDataHandler2);
					}
				}
				else if (!second)
				{
					if (list == null)
					{
						list = new List<string>();
					}
					list.Add(text2);
				}
			}
		}
		_requiresPostBackCopy = (second ? null : list);
		if (second)
		{
			secondPostData = null;
		}
	}

	/// <summary>Sets the intrinsic server objects of the <see cref="T:System.Web.UI.Page" /> object, such as the <see cref="P:System.Web.UI.Page.Context" />, <see cref="P:System.Web.UI.Page.Request" />, <see cref="P:System.Web.UI.Page.Response" />, and <see cref="P:System.Web.UI.Page.Application" /> properties.</summary>
	/// <param name="context">An <see cref="T:System.Web.HttpContext" /> object that provides references to the intrinsic server objects (for example, <see cref="P:System.Web.HttpContext.Request" />, <see cref="P:System.Web.HttpContext.Response" />, and <see cref="P:System.Web.HttpContext.Session" />) used to service HTTP requests. </param>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual void ProcessRequest(HttpContext context)
	{
		SetContext(context);
		if (clientTarget != null)
		{
			Request.ClientTarget = clientTarget;
		}
		WireupAutomaticEvents();
		_appCulture = Thread.CurrentThread.CurrentCulture;
		_appUICulture = Thread.CurrentThread.CurrentUICulture;
		FrameworkInitialize();
		frameworkInitialized = true;
		context.ErrorPage = _errorPage;
		try
		{
			InternalProcessRequest();
		}
		catch (ThreadAbortException ex)
		{
			if (FlagEnd.Value == ex.ExceptionState)
			{
				Thread.ResetAbort();
				return;
			}
			throw;
		}
		catch (Exception e)
		{
			ProcessException(e);
		}
		finally
		{
			ProcessUnload();
		}
	}

	private void ProcessException(Exception e)
	{
		Trace.Warn("Unhandled Exception", e.ToString(), e);
		_context.AddError(e);
		OnError(EventArgs.Empty);
		if (_context.HasError(e))
		{
			_context.ClearError(e);
			throw new HttpUnhandledException(null, e);
		}
	}

	private void ProcessUnload()
	{
		try
		{
			RenderTrace();
			UnloadRecursive(dispose: true);
		}
		catch
		{
		}
		if (!Thread.CurrentThread.CurrentCulture.Equals(_appCulture))
		{
			Thread.CurrentThread.CurrentCulture = _appCulture;
		}
		if (!Thread.CurrentThread.CurrentUICulture.Equals(_appUICulture))
		{
			Thread.CurrentThread.CurrentUICulture = _appUICulture;
		}
		_appCulture = null;
		_appUICulture = null;
	}

	/// <summary>Begins processing an asynchronous page request.</summary>
	/// <param name="context">The <see cref="T:System.Web.HttpContext" /> for the request.</param>
	/// <param name="callback">The callback method to notify when the process is complete.</param>
	/// <param name="extraData">State data for the asynchronous method.</param>
	/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous request.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	protected IAsyncResult AsyncPageBeginProcessRequest(HttpContext context, AsyncCallback callback, object extraData)
	{
		ProcessRequest(context);
		DummyAsyncResult dummyAsyncResult = new DummyAsyncResult(isCompleted: true, completedSynchronously: true, extraData);
		callback?.Invoke(dummyAsyncResult);
		return dummyAsyncResult;
	}

	/// <summary>Ends processing an asynchronous page request.</summary>
	/// <param name="result">An <see cref="T:System.IAsyncResult" /> referencing a pending asynchronous request.</param>
	[EditorBrowsable(EditorBrowsableState.Never)]
	protected void AsyncPageEndProcessRequest(IAsyncResult result)
	{
	}

	private void InternalProcessRequest()
	{
		if (PageAdapter != null)
		{
			_requestValueCollection = PageAdapter.DeterminePostBackMode();
		}
		else
		{
			_requestValueCollection = DeterminePostBackMode();
		}
		if (_requestValueCollection != null)
		{
			if (!isCrossPagePostBack && _requestValueCollection["__PREVIOUSPAGE"] != null && _requestValueCollection["__PREVIOUSPAGE"] != Request.FilePath)
			{
				_doLoadPreviousPage = true;
			}
			else
			{
				isCallback = _requestValueCollection["__CALLBACKPARAM"] != null;
				isPostBack = true;
			}
			string text = _requestValueCollection["__LASTFOCUS"];
			if (!string.IsNullOrEmpty(text))
			{
				_focusedControlID = UniqueID2ClientID(text);
			}
		}
		if (!isCrossPagePostBack && _context.PreviousHandler is Page)
		{
			previousPage = (Page)_context.PreviousHandler;
		}
		Trace.Write("aspx.page", "Begin PreInit");
		OnPreInit(EventArgs.Empty);
		Trace.Write("aspx.page", "End PreInit");
		InitializeTheme();
		ApplyMasterPage();
		Trace.Write("aspx.page", "Begin Init");
		InitRecursive(null);
		Trace.Write("aspx.page", "End Init");
		Trace.Write("aspx.page", "Begin InitComplete");
		OnInitComplete(EventArgs.Empty);
		Trace.Write("aspx.page", "End InitComplete");
		renderingForm = false;
		RestorePageState();
		ProcessPostData();
		ProcessRaiseEvents();
		if (!ProcessLoadComplete())
		{
			RenderPage();
		}
	}

	private void RestorePageState()
	{
		if (IsPostBack || IsCallback)
		{
			if (_requestValueCollection != null)
			{
				scriptManager.RestoreEventValidationState(_requestValueCollection["__EVENTVALIDATION"]);
			}
			Trace.Write("aspx.page", "Begin LoadViewState");
			LoadPageViewState();
			Trace.Write("aspx.page", "End LoadViewState");
		}
	}

	private void ProcessPostData()
	{
		if (IsPostBack || IsCallback)
		{
			Trace.Write("aspx.page", "Begin ProcessPostData");
			ProcessPostData(_requestValueCollection, second: false);
			Trace.Write("aspx.page", "End ProcessPostData");
		}
		ProcessLoad();
		if (IsPostBack || IsCallback)
		{
			Trace.Write("aspx.page", "Begin ProcessPostData Second Try");
			ProcessPostData(secondPostData, second: true);
			Trace.Write("aspx.page", "End ProcessPostData Second Try");
		}
	}

	private void ProcessLoad()
	{
		Trace.Write("aspx.page", "Begin PreLoad");
		OnPreLoad(EventArgs.Empty);
		Trace.Write("aspx.page", "End PreLoad");
		Trace.Write("aspx.page", "Begin Load");
		LoadRecursive();
		Trace.Write("aspx.page", "End Load");
	}

	private void ProcessRaiseEvents()
	{
		if (IsPostBack || IsCallback)
		{
			Trace.Write("aspx.page", "Begin Raise ChangedEvents");
			RaiseChangedEvents();
			Trace.Write("aspx.page", "End Raise ChangedEvents");
			Trace.Write("aspx.page", "Begin Raise PostBackEvent");
			RaisePostBackEvents();
			Trace.Write("aspx.page", "End Raise PostBackEvent");
		}
	}

	private bool ProcessLoadComplete()
	{
		Trace.Write("aspx.page", "Begin LoadComplete");
		OnLoadComplete(EventArgs.Empty);
		Trace.Write("aspx.page", "End LoadComplete");
		if (IsCrossPagePostBack)
		{
			return true;
		}
		if (IsCallback)
		{
			string value = ProcessCallbackData();
			HtmlTextWriter htmlTextWriter = new HtmlTextWriter(Response.Output);
			htmlTextWriter.Write(value);
			htmlTextWriter.Flush();
			return true;
		}
		Trace.Write("aspx.page", "Begin PreRender");
		PreRenderRecursiveInternal();
		Trace.Write("aspx.page", "End PreRender");
		ExecuteRegisteredAsyncTasks();
		Trace.Write("aspx.page", "Begin PreRenderComplete");
		OnPreRenderComplete(EventArgs.Empty);
		Trace.Write("aspx.page", "End PreRenderComplete");
		Trace.Write("aspx.page", "Begin SaveViewState");
		SavePageViewState();
		Trace.Write("aspx.page", "End SaveViewState");
		Trace.Write("aspx.page", "Begin SaveStateComplete");
		OnSaveStateComplete(EventArgs.Empty);
		Trace.Write("aspx.page", "End SaveStateComplete");
		return false;
	}

	internal void RenderPage()
	{
		scriptManager.ResetEventValidationState();
		Trace.Write("aspx.page", "Begin Render");
		HtmlTextWriter writer = CreateHtmlTextWriter(Response.Output);
		RenderControl(writer);
		Trace.Write("aspx.page", "End Render");
	}

	internal void SetContext(HttpContext context)
	{
		_context = context;
		_application = context.Application;
		_response = context.Response;
		_request = context.Request;
		_cache = context.Cache;
	}

	private void RenderTrace()
	{
		TraceManager traceManager = HttpRuntime.TraceManager;
		if ((!Trace.HaveTrace || Trace.IsEnabled) && (Trace.HaveTrace || traceManager.Enabled))
		{
			Trace.SaveData();
			if ((Trace.HaveTrace || !traceManager.Enabled || traceManager.PageOutput) && (!traceManager.LocalOnly || Context.Request.IsLocal))
			{
				HtmlTextWriter output = new HtmlTextWriter(Response.Output);
				Trace.Render(output);
			}
		}
	}

	private void RaisePostBackEvents()
	{
		if (requiresRaiseEvent != null)
		{
			RaisePostBackEvent(requiresRaiseEvent, null);
			return;
		}
		if (formPostedRequiresRaiseEvent != null)
		{
			RaisePostBackEvent(formPostedRequiresRaiseEvent, null);
			return;
		}
		NameValueCollection requestValueCollection = _requestValueCollection;
		if (requestValueCollection == null)
		{
			return;
		}
		string text = requestValueCollection["__EVENTTARGET"];
		if (string.IsNullOrEmpty(text))
		{
			if (AutoPostBackControl is IPostBackEventHandler sourceControl)
			{
				RaisePostBackEvent(sourceControl, null);
			}
			else if (formPostedRequiresRaiseEvent != null)
			{
				RaisePostBackEvent(formPostedRequiresRaiseEvent, null);
			}
			else
			{
				Validate();
			}
			return;
		}
		IPostBackEventHandler postBackEventHandler = FindControl(text, decode: true) as IPostBackEventHandler;
		if (postBackEventHandler == null)
		{
			postBackEventHandler = AutoPostBackControl as IPostBackEventHandler;
		}
		if (postBackEventHandler != null)
		{
			string eventArgument = requestValueCollection["__EVENTARGUMENT"];
			RaisePostBackEvent(postBackEventHandler, eventArgument);
		}
	}

	internal void RaiseChangedEvents()
	{
		if (requiresPostDataChanged == null)
		{
			return;
		}
		foreach (IPostBackDataHandler item in requiresPostDataChanged)
		{
			item.RaisePostDataChangedEvent();
		}
		requiresPostDataChanged = null;
	}

	/// <summary>Notifies the server control that caused the postback that it should handle an incoming postback event.</summary>
	/// <param name="sourceControl">The ASP.NET server control that caused the postback. This control must implement the <see cref="T:System.Web.UI.IPostBackEventHandler" /> interface. </param>
	/// <param name="eventArgument">The postback argument. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
	{
		sourceControl.RaisePostBackEvent(eventArgument);
	}

	/// <summary>Declares a value that is declared as an ECMAScript array declaration when the page is rendered.</summary>
	/// <param name="arrayName">The name of the array in which to declare the value. </param>
	/// <param name="arrayValue">The value to place in the array. </param>
	[Obsolete("The recommended alternative is ClientScript.RegisterArrayDeclaration(string arrayName, string arrayValue). http://go.microsoft.com/fwlink/?linkid=14202")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public void RegisterArrayDeclaration(string arrayName, string arrayValue)
	{
		scriptManager.RegisterArrayDeclaration(arrayName, arrayValue);
	}

	/// <summary>Emits client-side script blocks to the response.</summary>
	/// <param name="key">Unique key that identifies a script block. </param>
	/// <param name="script">Content of script that is sent to the client. </param>
	[Obsolete("The recommended alternative is ClientScript.RegisterClientScriptBlock(Type type, string key, string script). http://go.microsoft.com/fwlink/?linkid=14202")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public virtual void RegisterClientScriptBlock(string key, string script)
	{
		scriptManager.RegisterClientScriptBlock(key, script);
	}

	/// <summary>Allows server controls to automatically register a hidden field on the form. The field will be sent to the <see cref="T:System.Web.UI.Page" /> object when the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> server control is rendered.</summary>
	/// <param name="hiddenFieldName">The unique name of the hidden field to be rendered. </param>
	/// <param name="hiddenFieldInitialValue">The value to be emitted in the hidden form. </param>
	[Obsolete]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public virtual void RegisterHiddenField(string hiddenFieldName, string hiddenFieldInitialValue)
	{
		scriptManager.RegisterHiddenField(hiddenFieldName, hiddenFieldInitialValue);
	}

	[MonoTODO("Not implemented, Used in HtmlForm")]
	internal void RegisterClientScriptFile(string a, string b, string c)
	{
		throw new NotImplementedException();
	}

	/// <summary>Allows a page to access the client <see langword="OnSubmit" /> event. The script should be a function call to client code registered elsewhere.</summary>
	/// <param name="key">Unique key that identifies a script block. </param>
	/// <param name="script">The client-side script to be sent to the client. </param>
	[Obsolete("The recommended alternative is ClientScript.RegisterOnSubmitStatement(Type type, string key, string script). http://go.microsoft.com/fwlink/?linkid=14202")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public void RegisterOnSubmitStatement(string key, string script)
	{
		scriptManager.RegisterOnSubmitStatement(key, script);
	}

	internal string GetSubmitStatements()
	{
		return scriptManager.WriteSubmitStatements();
	}

	/// <summary>Registers a control as one that requires postback handling when the page is posted back to the server. </summary>
	/// <param name="control">The control to be registered. </param>
	/// <exception cref="T:System.Web.HttpException">The control to register does not implement the <see cref="T:System.Web.UI.IPostBackDataHandler" /> interface. </exception>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public void RegisterRequiresPostBack(Control control)
	{
		if (!(control is IPostBackDataHandler))
		{
			throw new HttpException("The control to register does not implement the IPostBackDataHandler interface.");
		}
		if (_requiresPostBack == null)
		{
			_requiresPostBack = new List<string>();
		}
		string item = control.UniqueID;
		if (!_requiresPostBack.Contains(item))
		{
			_requiresPostBack.Add(item);
		}
	}

	/// <summary>Registers an ASP.NET server control as one requiring an event to be raised when the control is processed on the <see cref="T:System.Web.UI.Page" /> object.</summary>
	/// <param name="control">The control to register. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public virtual void RegisterRequiresRaiseEvent(IPostBackEventHandler control)
	{
		requiresRaiseEvent = control;
	}

	/// <summary>Emits a client-side script block in the page response. </summary>
	/// <param name="key">Unique key that identifies a script block. </param>
	/// <param name="script">Content of script that will be sent to the client. </param>
	[Obsolete("The recommended alternative is ClientScript.RegisterStartupScript(Type type, string key, string script). http://go.microsoft.com/fwlink/?linkid=14202")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public virtual void RegisterStartupScript(string key, string script)
	{
		scriptManager.RegisterStartupScript(key, script);
	}

	/// <summary>Causes page view state to be persisted, if called.</summary>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public void RegisterViewStateHandler()
	{
		handleViewState = true;
	}

	/// <summary>Saves any view-state and control-state information for the page.</summary>
	/// <param name="state">An <see cref="T:System.Object" /> in which to store the view-state information. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void SavePageStateToPersistenceMedium(object state)
	{
		PageStatePersister pageStatePersister = PageStatePersister;
		if (pageStatePersister != null)
		{
			if (state is Pair pair)
			{
				pageStatePersister.ViewState = pair.First;
				pageStatePersister.ControlState = pair.Second;
			}
			else
			{
				pageStatePersister.ViewState = state;
			}
			pageStatePersister.Save();
		}
	}

	/// <summary>Loads any saved view-state information to the <see cref="T:System.Web.UI.Page" /> object. </summary>
	/// <returns>The saved view state.</returns>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual object LoadPageStateFromPersistenceMedium()
	{
		PageStatePersister pageStatePersister = PageStatePersister;
		if (pageStatePersister == null)
		{
			return null;
		}
		pageStatePersister.Load();
		return new Pair(pageStatePersister.ViewState, pageStatePersister.ControlState);
	}

	internal void LoadPageViewState()
	{
		if (LoadPageStateFromPersistenceMedium() is Pair pair && (allow_load || isCrossPagePostBack))
		{
			LoadPageControlState(pair.Second);
			if (pair.First is Pair pair2)
			{
				LoadViewStateRecursive(pair2.First);
				_requiresPostBackCopy = pair2.Second as List<string>;
			}
		}
	}

	internal void SavePageViewState()
	{
		if (handleViewState)
		{
			object second = SavePageControlState();
			Pair first = null;
			object obj = null;
			if (EnableViewState && ViewStateMode == ViewStateMode.Enabled)
			{
				obj = SaveViewStateRecursive();
			}
			object obj2 = ((_requiresPostBack != null && _requiresPostBack.Count > 0) ? _requiresPostBack : null);
			if (obj != null || obj2 != null)
			{
				first = new Pair(obj, obj2);
			}
			Pair pair = new Pair();
			pair.First = first;
			pair.Second = second;
			if (pair.First == null && pair.Second == null)
			{
				SavePageStateToPersistenceMedium(null);
			}
			else
			{
				SavePageStateToPersistenceMedium(pair);
			}
		}
	}

	/// <summary>Instructs any validation controls included on the page to validate their assigned information.</summary>
	public virtual void Validate()
	{
		is_validated = true;
		ValidateCollection(_validators);
	}

	internal bool AreValidatorsUplevel()
	{
		return AreValidatorsUplevel(string.Empty);
	}

	internal bool AreValidatorsUplevel(string valGroup)
	{
		bool result = false;
		foreach (IValidator validator in Validators)
		{
			if (validator is BaseValidator baseValidator && !(valGroup != baseValidator.ValidationGroup) && baseValidator.GetRenderUplevel())
			{
				result = true;
				break;
			}
		}
		return result;
	}

	private bool ValidateCollection(ValidatorCollection validators)
	{
		if (validators == null || validators.Count == 0)
		{
			return true;
		}
		bool result = true;
		foreach (IValidator validator in validators)
		{
			validator.Validate();
			if (!validator.IsValid)
			{
				result = false;
			}
		}
		return result;
	}

	/// <summary>Confirms that an <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control is rendered for the specified ASP.NET server control at run time.</summary>
	/// <param name="control">The ASP.NET server control that is required in the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control. </param>
	/// <exception cref="T:System.Web.HttpException">The specified server control is not contained between the opening and closing tags of the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> server control at run time. </exception>
	/// <exception cref="T:System.ArgumentNullException">The control to verify is <see langword="null" />.</exception>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public virtual void VerifyRenderingInServerForm(Control control)
	{
		if (Context == null || IsCallback || renderingForm)
		{
			return;
		}
		throw new HttpException("Control '" + control.ClientID + "' of type '" + control.GetType().Name + "' must be placed inside a form tag with runat=server.");
	}

	/// <summary>Initializes the control tree during page generation based on the declarative nature of the page. </summary>
	protected override void FrameworkInitialize()
	{
		base.FrameworkInitialize();
		InitializeStyleSheet();
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Page.InitComplete" /> event after page initialization.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnInitComplete(EventArgs e)
	{
		if ((event_mask & 1) != 0)
		{
			((EventHandler)base.Events[InitCompleteEvent])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Page.LoadComplete" /> event at the end of the page load stage.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnLoadComplete(EventArgs e)
	{
		if ((event_mask & 2) != 0)
		{
			((EventHandler)base.Events[LoadCompleteEvent])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Page.PreInit" /> event at the beginning of page initialization.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnPreInit(EventArgs e)
	{
		if ((event_mask & 4) != 0)
		{
			((EventHandler)base.Events[PreInitEvent])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Page.PreLoad" /> event after postback data is loaded into the page server controls but before the <see cref="M:System.Web.UI.Control.OnLoad(System.EventArgs)" /> event. </summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnPreLoad(EventArgs e)
	{
		if ((event_mask & 8) != 0)
		{
			((EventHandler)base.Events[PreLoadEvent])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Page.PreRenderComplete" /> event after the <see cref="M:System.Web.UI.Page.OnPreRenderComplete(System.EventArgs)" /> event and before the page is rendered.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnPreRenderComplete(EventArgs e)
	{
		if ((event_mask & 0x10) != 0)
		{
			((EventHandler)base.Events[PreRenderCompleteEvent])?.Invoke(this, e);
		}
		if (Form == null || !Form.DetermineRenderUplevel())
		{
			return;
		}
		string defaultButton = Form.DefaultButton;
		if (string.IsNullOrEmpty(_focusedControlID))
		{
			_focusedControlID = Form.DefaultFocus;
			if (string.IsNullOrEmpty(_focusedControlID))
			{
				_focusedControlID = defaultButton;
			}
		}
		if (!string.IsNullOrEmpty(_focusedControlID))
		{
			ClientScript.RegisterWebFormClientScript();
			ClientScript.RegisterStartupScript(typeof(Page), "HtmlForm-DefaultButton-StartupScript", "\n" + WebFormScriptReference + ".WebForm_AutoFocus('" + _focusedControlID + "');\n", addScriptTags: true);
		}
		if (Form.SubmitDisabledControls && _hasEnabledControlArray)
		{
			ClientScript.RegisterWebFormClientScript();
			ClientScript.RegisterOnSubmitStatement(typeof(Page), "HtmlForm-SubmitDisabledControls-SubmitStatement", WebFormScriptReference + ".WebForm_ReEnableControls();");
		}
	}

	internal void RegisterEnabledControl(Control control)
	{
		if (Form != null && Page.Form.SubmitDisabledControls && Page.Form.DetermineRenderUplevel())
		{
			_hasEnabledControlArray = true;
			Page.ClientScript.RegisterArrayDeclaration("__enabledControlArray", "'" + control.ClientID + "'");
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Page.SaveStateComplete" /> event after the page state has been saved to the persistence medium.</summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> object containing the event data.</param>
	protected virtual void OnSaveStateComplete(EventArgs e)
	{
		if ((event_mask & 0x20) != 0)
		{
			((EventHandler)base.Events[SaveStateCompleteEvent])?.Invoke(this, e);
		}
	}

	internal void RegisterForm(HtmlForm form)
	{
		_form = form;
	}

	private string ProcessCallbackData()
	{
		ICallbackEventHandler callbackTarget = GetCallbackTarget();
		string callbackEventError = string.Empty;
		ProcessRaiseCallbackEvent(callbackTarget, ref callbackEventError);
		return ProcessGetCallbackResult(callbackTarget, callbackEventError);
	}

	private ICallbackEventHandler GetCallbackTarget()
	{
		string text = _requestValueCollection["__CALLBACKID"];
		if (text == null || text.Length == 0)
		{
			throw new HttpException("Callback target not provided.");
		}
		return (FindControl(text, decode: true) as ICallbackEventHandler) ?? throw new HttpException($"Invalid callback target '{text}'.");
	}

	private void ProcessRaiseCallbackEvent(ICallbackEventHandler target, ref string callbackEventError)
	{
		string eventArgument = _requestValueCollection["__CALLBACKPARAM"];
		try
		{
			target.RaiseCallbackEvent(eventArgument);
		}
		catch (Exception ex)
		{
			callbackEventError = "e" + (RuntimeHelpers.DebuggingEnabled ? ex.ToString() : ex.Message);
		}
	}

	private string ProcessGetCallbackResult(ICallbackEventHandler target, string callbackEventError)
	{
		string callbackResult;
		try
		{
			callbackResult = target.GetCallbackResult();
		}
		catch (Exception ex)
		{
			return "e" + (RuntimeHelpers.DebuggingEnabled ? ex.ToString() : ex.Message);
		}
		string eventValidationStateFormatted = ClientScript.GetEventValidationStateFormatted();
		return callbackEventError + ((eventValidationStateFormatted == null) ? "0" : eventValidationStateFormatted.Length.ToString()) + "|" + eventValidationStateFormatted + callbackResult;
	}

	internal void SetHeader(HtmlHead header)
	{
		htmlHeader = header;
		if (header != null)
		{
			if (_title != null)
			{
				htmlHeader.Title = _title;
				_title = null;
			}
			if (_metaDescription != null)
			{
				htmlHeader.Description = _metaDescription;
				_metaDescription = null;
			}
			if (_metaKeywords != null)
			{
				htmlHeader.Keywords = _metaKeywords;
				_metaKeywords = null;
			}
		}
	}

	/// <summary>Registers beginning and ending event handler delegates that do not require state information for an asynchronous page.</summary>
	/// <param name="beginHandler">The delegate for the <see cref="T:System.Web.BeginEventHandler" /> method.</param>
	/// <param name="endHandler">The delegate for the <see cref="T:System.Web.EndEventHandler" /> method.</param>
	/// <exception cref="T:System.InvalidOperationException">The <see langword="&lt;async&gt;" /> page directive is not set to <see langword="true" />.- or -The <see cref="M:System.Web.UI.Page.AddOnPreRenderCompleteAsync(System.Web.BeginEventHandler,System.Web.EndEventHandler)" /> method is called after the <see cref="E:System.Web.UI.Control.PreRender" /> event.</exception>
	/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Web.UI.PageAsyncTask.BeginHandler" /> or <see cref="P:System.Web.UI.PageAsyncTask.EndHandler" /> is <see langword="null" />. </exception>
	public void AddOnPreRenderCompleteAsync(BeginEventHandler beginHandler, EndEventHandler endHandler)
	{
		AddOnPreRenderCompleteAsync(beginHandler, endHandler, null);
	}

	/// <summary>Registers beginning and ending  event handler delegates for an asynchronous page.</summary>
	/// <param name="beginHandler">The delegate for the <see cref="T:System.Web.BeginEventHandler" /> method.</param>
	/// <param name="endHandler">The delegate for the <see cref="T:System.Web.EndEventHandler" /> method.</param>
	/// <param name="state">An object containing state information for the event handlers.</param>
	/// <exception cref="T:System.InvalidOperationException">The <see langword="&lt;async&gt;" /> page directive is not set to <see langword="true" />.- or -The <see cref="M:System.Web.UI.Page.AddOnPreRenderCompleteAsync(System.Web.BeginEventHandler,System.Web.EndEventHandler)" /> method is called after the <see cref="E:System.Web.UI.Control.PreRender" /> event.</exception>
	/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Web.UI.PageAsyncTask.BeginHandler" /> or <see cref="P:System.Web.UI.PageAsyncTask.EndHandler" /> is <see langword="null" />. </exception>
	public void AddOnPreRenderCompleteAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
	{
		if (!IsAsync)
		{
			throw new InvalidOperationException("AddOnPreRenderCompleteAsync called and Page.IsAsync == false");
		}
		if (base.IsPrerendered)
		{
			throw new InvalidOperationException("AddOnPreRenderCompleteAsync can only be called before and during PreRender.");
		}
		if (beginHandler == null)
		{
			throw new ArgumentNullException("beginHandler");
		}
		if (endHandler == null)
		{
			throw new ArgumentNullException("endHandler");
		}
		RegisterAsyncTask(new PageAsyncTask(beginHandler, endHandler, null, state, executeInParallel: false));
	}

	/// <summary>Registers a new asynchronous task with the page.</summary>
	/// <param name="task">A <see cref="T:System.Web.UI.PageAsyncTask" /> that defines the asynchronous task.</param>
	/// <exception cref="T:System.ArgumentNullException">The asynchronous task is <see langword="null" />. </exception>
	public void RegisterAsyncTask(PageAsyncTask task)
	{
		if (task == null)
		{
			throw new ArgumentNullException("task");
		}
		if (task.ExecuteInParallel)
		{
			ParallelTasks.Add(task);
		}
		else
		{
			SerialTasks.Add(task);
		}
	}

	/// <summary>Starts the execution of an asynchronous task.</summary>
	/// <exception cref="T:System.Web.HttpException">There is an exception in the asynchronous task.</exception>
	public void ExecuteRegisteredAsyncTasks()
	{
		if ((parallelTasks == null || parallelTasks.Count == 0) && (serialTasks == null || serialTasks.Count == 0))
		{
			return;
		}
		if (parallelTasks != null)
		{
			DateTime now = DateTime.Now;
			List<PageAsyncTask> list = parallelTasks;
			parallelTasks = null;
			List<IAsyncResult> list2 = new List<IAsyncResult>();
			foreach (PageAsyncTask item in list)
			{
				IAsyncResult asyncResult = item.BeginHandler(this, EventArgs.Empty, EndAsyncTaskCallback, item);
				if (asyncResult.CompletedSynchronously)
				{
					item.EndHandler(asyncResult);
				}
				else
				{
					list2.Add(asyncResult);
				}
			}
			if (list2.Count > 0)
			{
				WaitHandle[] array = new WaitHandle[list2.Count];
				int num = 0;
				for (num = 0; num < list2.Count; num++)
				{
					array[num] = list2[num].AsyncWaitHandle;
				}
				if (!WaitHandle.WaitAll(array, AsyncTimeout, exitContext: false))
				{
					for (num = 0; num < list2.Count; num++)
					{
						if (!list2[num].IsCompleted)
						{
							list[num].TimeoutHandler(list2[num]);
						}
					}
				}
			}
			TimeSpan timeSpan = DateTime.Now - now;
			if (timeSpan <= AsyncTimeout)
			{
				AsyncTimeout -= timeSpan;
			}
			else
			{
				AsyncTimeout = TimeSpan.FromTicks(0L);
			}
		}
		if (serialTasks != null)
		{
			List<PageAsyncTask> list3 = serialTasks;
			serialTasks = null;
			foreach (PageAsyncTask item2 in list3)
			{
				DateTime now2 = DateTime.Now;
				IAsyncResult asyncResult2 = item2.BeginHandler(this, EventArgs.Empty, EndAsyncTaskCallback, item2);
				if (asyncResult2.CompletedSynchronously)
				{
					item2.EndHandler(asyncResult2);
				}
				else if (!asyncResult2.AsyncWaitHandle.WaitOne(AsyncTimeout, exitContext: false) && !asyncResult2.IsCompleted)
				{
					item2.TimeoutHandler(asyncResult2);
				}
				TimeSpan timeSpan2 = DateTime.Now - now2;
				if (timeSpan2 <= AsyncTimeout)
				{
					AsyncTimeout -= timeSpan2;
				}
				else
				{
					AsyncTimeout = TimeSpan.FromTicks(0L);
				}
			}
		}
		AsyncTimeout = TimeSpan.FromSeconds(45.0);
	}

	private void EndAsyncTaskCallback(IAsyncResult result)
	{
		((PageAsyncTask)result.AsyncState).EndHandler(result);
	}

	/// <summary>Creates a specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object to render the page's content.</summary>
	/// <param name="tw">The <see cref="T:System.IO.TextWriter" /> used to create the <see cref="T:System.Web.UI.HtmlTextWriter" />. </param>
	/// <param name="writerType">The type of text writer to create.</param>
	/// <returns>An <see cref="T:System.Web.UI.HtmlTextWriter" /> that renders the content of the page.</returns>
	/// <exception cref="T:System.Web.HttpException">The <paramref name="writerType" /> parameter is set to an invalid type.</exception>
	public static HtmlTextWriter CreateHtmlTextWriterFromType(TextWriter tw, Type writerType)
	{
		if (!typeof(HtmlTextWriter).IsAssignableFrom(writerType))
		{
			throw new HttpException($"Type '{writerType.FullName}' cannot be assigned to HtmlTextWriter");
		}
		if (writerType.GetConstructor(new Type[1] { typeof(TextWriter) }) == null)
		{
			throw new HttpException($"Type '{writerType.FullName}' does not have a consturctor that takes a TextWriter as parameter");
		}
		return (HtmlTextWriter)Activator.CreateInstance(writerType, tw);
	}

	/// <summary>Registers a control with the page as one requiring view-state encryption. </summary>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Web.UI.Page.RegisterRequiresViewStateEncryption" /> method must be called before or during the page <see langword="PreRender" />phase in the page life cycle. </exception>
	public void RegisterRequiresViewStateEncryption()
	{
		controlRegisteredForViewStateEncryption = true;
	}

	private void ApplyMasterPage()
	{
		if (masterPageFile != null && masterPageFile.Length > 0)
		{
			MasterPage master = Master;
			if (master != null)
			{
				Dictionary<string, bool> appliedMasterPageFiles = new Dictionary<string, bool>(StringComparer.Ordinal);
				MasterPage.ApplyMasterPageRecursive(Request.CurrentExecutionFilePath, HostingEnvironment.VirtualPathProvider, master, appliedMasterPageFiles);
				master.Page = this;
				Controls.Clear();
				Controls.Add(master);
			}
		}
	}

	/// <summary>Sets the browser focus to the control with the specified identifier. </summary>
	/// <param name="clientID">The ID of the control to set focus to.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="clientID" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">
	///         <see cref="M:System.Web.UI.Page.SetFocus(System.String)" /> is called when the control is not part of a Web Forms page.- or -
	///         <see cref="M:System.Web.UI.Page.SetFocus(System.String)" /> is called after the <see cref="E:System.Web.UI.Control.PreRender" /> event.</exception>
	public void SetFocus(string clientID)
	{
		if (string.IsNullOrEmpty(clientID))
		{
			throw new ArgumentNullException("control");
		}
		if (base.IsPrerendered)
		{
			throw new InvalidOperationException("SetFocus can only be called before and during PreRender.");
		}
		if (Form == null)
		{
			throw new InvalidOperationException("A form tag with runat=server must exist on the Page to use SetFocus() or the Focus property.");
		}
		_focusedControlID = clientID;
	}

	/// <summary>Sets the browser focus to the specified control. </summary>
	/// <param name="control">The control to receive focus.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="control" /> is <see langword="null" />. </exception>
	/// <exception cref="T:System.InvalidOperationException">
	///         <see cref="M:System.Web.UI.Page.SetFocus(System.Web.UI.Control)" /> is called when the control is not part of a Web Forms page. - or -
	///         <see cref="M:System.Web.UI.Page.SetFocus(System.Web.UI.Control)" /> is called after the <see cref="E:System.Web.UI.Control.PreRender" /> event. </exception>
	public void SetFocus(Control control)
	{
		if (control == null)
		{
			throw new ArgumentNullException("control");
		}
		SetFocus(control.ClientID);
	}

	/// <summary>Registers a control as one whose control state must be persisted.</summary>
	/// <param name="control">The control to register.</param>
	/// <exception cref="T:System.ArgumentException">The control to register is <see langword="null" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Web.UI.Page.RegisterRequiresControlState(System.Web.UI.Control)" /> method can be called only before or during the <see cref="E:System.Web.UI.Control.PreRender" /> event.</exception>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public void RegisterRequiresControlState(Control control)
	{
		if (control == null)
		{
			throw new ArgumentNullException("control");
		}
		if (RequiresControlState(control))
		{
			return;
		}
		if (requireStateControls == null)
		{
			requireStateControls = new List<Control>();
		}
		requireStateControls.Add(control);
		int num = requireStateControls.Count - 1;
		if (_savedControlState == null || num >= _savedControlState.Length)
		{
			return;
		}
		for (Control parent = control.Parent; parent != null; parent = parent.Parent)
		{
			if (parent.IsChildControlStateCleared)
			{
				return;
			}
		}
		object obj = _savedControlState[num];
		if (obj != null)
		{
			control.LoadControlState(obj);
		}
	}

	/// <summary>Determines whether the specified <see cref="T:System.Web.UI.Control" /> object is registered to participate in control state management.</summary>
	/// <param name="control">The <see cref="T:System.Web.UI.Control" /> to check for a control state requirement.</param>
	/// <returns>
	///     <see langword="true" /> if the specified <see cref="T:System.Web.UI.Control" /> requires control state; otherwise, <see langword="false" /></returns>
	public bool RequiresControlState(Control control)
	{
		if (requireStateControls == null)
		{
			return false;
		}
		return requireStateControls.Contains(control);
	}

	/// <summary>Stops persistence of control state for the specified control.</summary>
	/// <param name="control">The <see cref="T:System.Web.UI.Control" /> for which to stop persistence of control state.</param>
	/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Web.UI.Control" /> is <see langword="null" />.</exception>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public void UnregisterRequiresControlState(Control control)
	{
		if (requireStateControls != null)
		{
			requireStateControls.Remove(control);
		}
	}

	/// <summary>Returns a collection of control validators for a specified validation group.</summary>
	/// <param name="validationGroup">The validation group to return, or <see langword="null" /> to return the default validation group.</param>
	/// <returns>A <see cref="T:System.Web.UI.ValidatorCollection" /> that contains the control validators for the specified validation group.</returns>
	public ValidatorCollection GetValidators(string validationGroup)
	{
		if (validationGroup == string.Empty)
		{
			validationGroup = null;
		}
		ValidatorCollection validatorCollection = new ValidatorCollection();
		if (_validators == null)
		{
			return validatorCollection;
		}
		foreach (IValidator validator in _validators)
		{
			if (BelongsToGroup(validator, validationGroup))
			{
				validatorCollection.Add(validator);
			}
		}
		return validatorCollection;
	}

	private bool BelongsToGroup(IValidator v, string validationGroup)
	{
		BaseValidator baseValidator = v as BaseValidator;
		if (validationGroup == null)
		{
			if (baseValidator != null)
			{
				return string.IsNullOrEmpty(baseValidator.ValidationGroup);
			}
			return true;
		}
		if (baseValidator != null)
		{
			return baseValidator.ValidationGroup == validationGroup;
		}
		return false;
	}

	/// <summary>Instructs the validation controls in the specified validation group to validate their assigned information.</summary>
	/// <param name="validationGroup">The validation group name of the controls to validate.</param>
	public virtual void Validate(string validationGroup)
	{
		is_validated = true;
		ValidateCollection(GetValidators(validationGroup));
	}

	private object SavePageControlState()
	{
		int num = ((requireStateControls != null) ? requireStateControls.Count : 0);
		if (num == 0)
		{
			return null;
		}
		object[] array = new object[num];
		object[] array2 = new object[num];
		bool flag = true;
		TraceContext traceContext = ((Context != null && Context.Trace.IsEnabled) ? Context.Trace : null);
		for (int i = 0; i < num; i++)
		{
			Control control = requireStateControls[i];
			object obj = (array[i] = control.SaveControlState());
			if (obj != null)
			{
				flag = false;
			}
			traceContext?.SaveControlState(control, obj);
			ControlAdapter controlAdapter = control.Adapter;
			if (controlAdapter != null)
			{
				array2[i] = controlAdapter.SaveAdapterControlState();
				if (array2[i] != null)
				{
					flag = false;
				}
			}
		}
		if (flag)
		{
			return null;
		}
		return new Pair(array, array2);
	}

	private void LoadPageControlState(object data)
	{
		_savedControlState = null;
		if (data == null)
		{
			return;
		}
		Pair pair = (Pair)data;
		_savedControlState = (object[])pair.First;
		object[] array = (object[])pair.Second;
		if (requireStateControls == null)
		{
			return;
		}
		int num = Math.Min(requireStateControls.Count, (_savedControlState != null) ? _savedControlState.Length : requireStateControls.Count);
		for (int i = 0; i < num; i++)
		{
			Control control = requireStateControls[i];
			control.LoadControlState((_savedControlState != null) ? _savedControlState[i] : null);
			if (control.Adapter != null)
			{
				control.Adapter.LoadAdapterControlState((array != null) ? array[i] : null);
			}
		}
	}

	private void LoadPreviousPageReference()
	{
		if (_requestValueCollection != null)
		{
			string text = _requestValueCollection["__PREVIOUSPAGE"];
			if (text != null)
			{
				IHttpHandler httpHandler = BuildManager.CreateInstanceFromVirtualPath(text, typeof(IHttpHandler)) as IHttpHandler;
				previousPage = (Page)httpHandler;
				previousPage.isCrossPagePostBack = true;
				Server.Execute(httpHandler, null, preserveForm: true, _context.Request.CurrentExecutionFilePath, null, isTransfer: false, isInclude: false);
			}
		}
	}

	/// <summary>Called during page initialization to create a collection of content (from content controls) that is handed to a master page, if the current page or master page refers to a master page. </summary>
	/// <param name="templateName">The name of the content template to add.</param>
	/// <param name="template">The content template</param>
	/// <exception cref="T:System.Web.HttpException">A content template with the same name already exists.</exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	protected internal void AddContentTemplate(string templateName, ITemplate template)
	{
		if (contentTemplates == null)
		{
			contentTemplates = new Hashtable();
		}
		contentTemplates[templateName] = template;
	}

	internal void PushDataItemContext(object o)
	{
		if (dataItemCtx == null)
		{
			dataItemCtx = new Stack();
		}
		dataItemCtx.Push(o);
	}

	internal void PopDataItemContext()
	{
		if (dataItemCtx == null)
		{
			throw new InvalidOperationException();
		}
		dataItemCtx.Pop();
	}

	/// <summary>Gets the data item at the top of the data-binding context stack.</summary>
	/// <returns>The object at the top of the data binding context stack.</returns>
	/// <exception cref="T:System.InvalidOperationException">There is no data-binding context for the page.</exception>
	public object GetDataItem()
	{
		if (dataItemCtx == null || dataItemCtx.Count == 0)
		{
			throw new InvalidOperationException("No data item");
		}
		return dataItemCtx.Peek();
	}

	private void AddStyleSheets(PageTheme theme, ref List<string> links)
	{
		if (theme == null)
		{
			return;
		}
		string[] array = theme?.GetStyleSheets();
		if (array != null && array.Length != 0)
		{
			if (links == null)
			{
				links = new List<string>();
			}
			links.AddRange(array);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event to initialize the page.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected internal override void OnInit(EventArgs e)
	{
		base.OnInit(e);
		List<string> links = null;
		AddStyleSheets(StyleSheetPageTheme, ref links);
		AddStyleSheets(PageTheme, ref links);
		if (links != null)
		{
			HtmlHead header = Header;
			if (links != null && header == null)
			{
				throw new InvalidOperationException("Using themed css files requires a header control on the page.");
			}
			ControlCollection controls = header.Controls;
			for (int num = links.Count - 1; num >= 0; num--)
			{
				string href = links[num];
				HtmlLink htmlLink = new HtmlLink();
				htmlLink.Href = href;
				htmlLink.Attributes["type"] = "text/css";
				htmlLink.Attributes["rel"] = "stylesheet";
				controls.AddAt(0, htmlLink);
			}
		}
	}

	/// <summary>Returns a list of physical file names that correspond to a list of virtual file locations.</summary>
	/// <param name="virtualFileDependencies">A string array of virtual file locations.</param>
	/// <returns>An object containing a list of physical file locations.</returns>
	[MonoDocumentationNote("Not implemented.  Only used by .net aspx parser")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	protected object GetWrappedFileDependencies(string[] virtualFileDependencies)
	{
		return virtualFileDependencies;
	}

	/// <summary>Sets the <see cref="P:System.Web.UI.Page.Culture" /> and <see cref="P:System.Web.UI.Page.UICulture" /> for the current thread of the page.</summary>
	[MonoDocumentationNote("Does nothing.  Used by .net aspx parser")]
	protected virtual void InitializeCulture()
	{
	}

	/// <summary>Adds a list of dependent files that make up the current page. This method is used internally by the ASP.NET page framework and is not intended to be used directly from your code.</summary>
	/// <param name="virtualFileDependencies">An <see cref="T:System.Object" /> containing the list of file names.</param>
	[MonoDocumentationNote("Does nothing. Used by .net aspx parser")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	protected internal void AddWrappedFileDependencies(object virtualFileDependencies)
	{
	}
}
