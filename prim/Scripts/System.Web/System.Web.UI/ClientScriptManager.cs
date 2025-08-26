using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Web.Handlers;
using System.Web.Util;

namespace System.Web.UI;

/// <summary>Defines methods for managing client scripts in Web applications.</summary>
public sealed class ClientScriptManager
{
	private sealed class ScriptEntry
	{
		public readonly Type Type;

		public readonly string Key;

		public readonly string Script;

		public readonly ScriptEntryFormat Format;

		public ScriptEntry Next;

		public ScriptEntry(Type type, string key, string script, ScriptEntryFormat format)
		{
			Key = key;
			Type = type;
			Script = script;
			Format = format;
		}
	}

	private enum ScriptEntryFormat
	{
		None,
		AddScriptTag,
		Include
	}

	internal const string EventStateFieldName = "__EVENTVALIDATION";

	private Hashtable registeredArrayDeclares;

	private ScriptEntry clientScriptBlocks;

	private ScriptEntry startupScriptBlocks;

	internal Hashtable hiddenFields;

	private ScriptEntry submitStatements;

	private Page ownerPage;

	private int[] eventValidationValues;

	private int eventValidationPos;

	private Hashtable expandoAttributes;

	private bool _hasRegisteredForEventValidationOnCallback;

	private bool _pageInRender;

	private bool _initCallBackRegistered;

	private bool _webFormClientScriptRendered;

	private bool _webFormClientScriptRequired;

	private bool _scriptTagOpened;

	internal const string SCRIPT_BLOCK_START = "//<![CDATA[";

	internal const string SCRIPT_BLOCK_END = "//]]>";

	internal const string SCRIPT_ELEMENT_START = "<script type=\"text/javascript\">//<![CDATA[";

	internal const string SCRIPT_ELEMENT_END = "//]]></script>";

	internal bool ScriptsPresent
	{
		get
		{
			if (!_webFormClientScriptRequired && !_initCallBackRegistered && !_hasRegisteredForEventValidationOnCallback && clientScriptBlocks == null && startupScriptBlocks == null && submitStatements == null && registeredArrayDeclares == null)
			{
				return expandoAttributes != null;
			}
			return true;
		}
	}

	private Page OwnerPage
	{
		get
		{
			if (ownerPage == null)
			{
				throw new InvalidOperationException("Associated Page instance is required to complete this operation.");
			}
			return ownerPage;
		}
	}

	internal ClientScriptManager(Page page)
	{
		ownerPage = page;
	}

	/// <summary>Gets a reference, with <see langword="javascript:" /> appended to the beginning of it, that can be used in a client event to post back to the server for the specified control and with the specified event arguments.</summary>
	/// <param name="control">The server control to process the postback.</param>
	/// <param name="argument">The parameter passed to the server control. </param>
	/// <returns>A string representing a JavaScript call to the postback function that includes the target control's ID and event arguments.</returns>
	public string GetPostBackClientHyperlink(Control control, string argument)
	{
		return "javascript:" + GetPostBackEventReference(control, argument);
	}

	/// <summary>Gets a reference, with <see langword="javascript:" /> appended to the beginning of it, that can be used in a client event to post back to the server for the specified control with the specified event arguments and Boolean indication whether to register the post back for event validation.</summary>
	/// <param name="control">The server control to process the postback.</param>
	/// <param name="argument">The parameter passed to the server control.</param>
	/// <param name="registerForEventValidation">
	///       <see langword="true" /> to register the postback event for validation; <see langword="false" /> to not register the post back event for validation.</param>
	/// <returns>A string representing a JavaScript call to the postback function that includes the target control's ID and event arguments.</returns>
	public string GetPostBackClientHyperlink(Control control, string argument, bool registerForEventValidation)
	{
		if (registerForEventValidation)
		{
			RegisterForEventValidation(control.UniqueID, argument);
		}
		return "javascript:" + GetPostBackEventReference(control, argument);
	}

	/// <summary>Returns a string that can be used in a client event to cause postback to the server. The reference string is defined by the specified control that handles the postback and a string argument of additional event information.</summary>
	/// <param name="control">The server <see cref="T:System.Web.UI.Control" /> that processes the postback on the server.</param>
	/// <param name="argument">A string of optional arguments to pass to the control that processes the postback.</param>
	/// <returns>A string that, when treated as script on the client, initiates the postback.</returns>
	/// <exception cref="T:System.ArgumentNullException">The specified <see cref="T:System.Web.UI.Control" /> is <see langword="null" />.</exception>
	public string GetPostBackEventReference(Control control, string argument)
	{
		if (control == null)
		{
			throw new ArgumentNullException("control");
		}
		Page page = OwnerPage;
		page.RequiresPostBackScript();
		if (page.IsMultiForm)
		{
			return page.theForm + ".__doPostBack('" + control.UniqueID + "','" + argument + "')";
		}
		return "__doPostBack('" + control.UniqueID + "','" + argument + "')";
	}

	/// <summary>Returns a string to use in a client event to cause postback to the server. The reference string is defined by the specified control that handles the postback and a string argument of additional event information. Optionally, registers the event reference for validation.</summary>
	/// <param name="control">The server <see cref="T:System.Web.UI.Control" /> that processes the postback on the server.</param>
	/// <param name="argument">A string of optional arguments to pass to <paramref name="control" />.</param>
	/// <param name="registerForEventValidation">
	///       <see langword="true" /> to register the event reference for validation; otherwise, <see langword="false" />.</param>
	/// <returns>A string that, when treated as script on the client, initiates the postback.</returns>
	/// <exception cref="T:System.ArgumentNullException">The specified <see cref="T:System.Web.UI.Control" /> is <see langword="null" />.</exception>
	public string GetPostBackEventReference(Control control, string argument, bool registerForEventValidation)
	{
		if (control == null)
		{
			throw new ArgumentNullException("control");
		}
		if (registerForEventValidation)
		{
			RegisterForEventValidation(control.UniqueID, argument);
		}
		return GetPostBackEventReference(control, argument);
	}

	/// <summary>Returns a string that can be used in a client event to cause postback to the server. The reference string is defined by the specified <see cref="T:System.Web.UI.PostBackOptions" /> object. Optionally, registers the event reference for validation.</summary>
	/// <param name="options">A <see cref="T:System.Web.UI.PostBackOptions" /> that defines the postback.</param>
	/// <param name="registerForEventValidation">
	///       <see langword="true" /> to register the event reference for validation; otherwise, <see langword="false" />.</param>
	/// <returns>A string that, when treated as script on the client, initiates the client postback.</returns>
	/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.PostBackOptions" /> is <see langword="null" />.</exception>
	public string GetPostBackEventReference(PostBackOptions options, bool registerForEventValidation)
	{
		if (options == null)
		{
			throw new ArgumentNullException("options");
		}
		if (registerForEventValidation)
		{
			RegisterForEventValidation(options);
		}
		return GetPostBackEventReference(options);
	}

	/// <summary>Returns a string that can be used in a client event to cause postback to the server. The reference string is defined by the specified <see cref="T:System.Web.UI.PostBackOptions" /> instance.</summary>
	/// <param name="options">A <see cref="T:System.Web.UI.PostBackOptions" /> that defines the postback.</param>
	/// <returns>A string that, when treated as script on the client, initiates the client postback.</returns>
	/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.PostBackOptions" /> parameter is <see langword="null" /></exception>
	public string GetPostBackEventReference(PostBackOptions options)
	{
		if (options == null)
		{
			throw new ArgumentNullException("options");
		}
		string actionUrl = options.ActionUrl;
		if (actionUrl == null && options.ValidationGroup == null && !options.TrackFocus && !options.AutoPostBack && !options.PerformValidation)
		{
			if (!options.ClientSubmit)
			{
				return null;
			}
			if (options.RequiresJavaScriptProtocol)
			{
				return GetPostBackClientHyperlink(options.TargetControl, options.Argument);
			}
			return GetPostBackEventReference(options.TargetControl, options.Argument);
		}
		RegisterWebFormClientScript();
		Page page = OwnerPage;
		Uri uri = page.RequestInternal?.Url;
		if (uri != null)
		{
			RegisterHiddenField("__PREVIOUSPAGE", uri.AbsolutePath);
		}
		if (options.TrackFocus)
		{
			RegisterHiddenField("__LASTFOCUS", string.Empty);
		}
		string text = (options.RequiresJavaScriptProtocol ? "javascript:" : string.Empty);
		if (page.IsMultiForm)
		{
			text = text + page.theForm + ".";
		}
		return text + "WebForm_DoPostback(" + GetScriptLiteral(options.TargetControl.UniqueID) + "," + GetScriptLiteral(options.Argument) + "," + GetScriptLiteral(actionUrl) + "," + GetScriptLiteral(options.AutoPostBack) + "," + GetScriptLiteral(options.PerformValidation) + "," + GetScriptLiteral(options.TrackFocus) + "," + GetScriptLiteral(options.ClientSubmit) + "," + GetScriptLiteral(options.ValidationGroup) + ")";
	}

	internal void RegisterWebFormClientScript()
	{
		if (!_webFormClientScriptRequired)
		{
			OwnerPage.RequiresPostBackScript();
			_webFormClientScriptRequired = true;
		}
	}

	internal void WriteWebFormClientScript(HtmlTextWriter writer)
	{
		if (!_webFormClientScriptRendered && _webFormClientScriptRequired)
		{
			Page page = OwnerPage;
			writer.WriteLine();
			WriteClientScriptInclude(writer, GetWebResourceUrl(typeof(Page), "webform.js"), typeof(Page), "webform.js");
			WriteBeginScriptBlock(writer);
			writer.WriteLine("WebForm_Initialize({0});", page.IsMultiForm ? page.theForm : "window");
			WriteEndScriptBlock(writer);
			_webFormClientScriptRendered = true;
		}
	}

	/// <summary>Obtains a reference to a client function that, when invoked, initiates a client call back to a server event. The client function for this overloaded method includes a specified control, argument, client script, and context.</summary>
	/// <param name="control">The server <see cref="T:System.Web.UI.Control" /> that handles the client callback. The control must implement the <see cref="T:System.Web.UI.ICallbackEventHandler" /> interface and provide a <see cref="M:System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent(System.String)" /> method. </param>
	/// <param name="argument">An argument passed from the client script to the server 
	///       <see cref="M:System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent(System.String)" />  method. </param>
	/// <param name="clientCallback">The name of the client event handler that receives the result of the successful server event. </param>
	/// <param name="context">The client script that is evaluated on the client prior to initiating the callback. The result of the script is passed back to the client event handler. </param>
	/// <returns>The name of a client function that invokes the client callback. </returns>
	/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.Control" /> specified is <see langword="null" />. </exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.Control" /> specified does not implement the <see cref="T:System.Web.UI.ICallbackEventHandler" /> interface.</exception>
	public string GetCallbackEventReference(Control control, string argument, string clientCallback, string context)
	{
		return GetCallbackEventReference(control, argument, clientCallback, context, null, useAsync: false);
	}

	/// <summary>Obtains a reference to a client function that, when invoked, initiates a client call back to server events. The client function for this overloaded method includes a specified control, argument, client script, context, and Boolean value.</summary>
	/// <param name="control">The server <see cref="T:System.Web.UI.Control" /> that handles the client callback. The control must implement the <see cref="T:System.Web.UI.ICallbackEventHandler" /> interface and provide a <see cref="M:System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent(System.String)" /> method. </param>
	/// <param name="argument">An argument passed from the client script to the server 
	///       <see cref="M:System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent(System.String)" />  method. </param>
	/// <param name="clientCallback">The name of the client event handler that receives the result of the successful server event. </param>
	/// <param name="context">The client script that is evaluated on the client prior to initiating the callback. The result of the script is passed back to the client event handler. </param>
	/// <param name="useAsync">
	///       <see langword="true" /> to perform the callback asynchronously; <see langword="false" /> to perform the callback synchronously.</param>
	/// <returns>The name of a client function that invokes the client callback. </returns>
	/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.Control" /> specified is <see langword="null" />. </exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.Control" /> specified does not implement the <see cref="T:System.Web.UI.ICallbackEventHandler" /> interface.</exception>
	public string GetCallbackEventReference(Control control, string argument, string clientCallback, string context, bool useAsync)
	{
		return GetCallbackEventReference(control, argument, clientCallback, context, null, useAsync);
	}

	/// <summary>Obtains a reference to a client function that, when invoked, initiates a client call back to server events. The client function for this overloaded method includes a specified control, argument, client script, context, error handler, and Boolean value.</summary>
	/// <param name="control">The server <see cref="T:System.Web.UI.Control" /> that handles the client callback. The control must implement the <see cref="T:System.Web.UI.ICallbackEventHandler" /> interface and provide a <see cref="M:System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent(System.String)" /> method. </param>
	/// <param name="argument">An argument passed from the client script to the server <see cref="M:System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent(System.String)" />  method. </param>
	/// <param name="clientCallback">The name of the client event handler that receives the result of the successful server event. </param>
	/// <param name="context">The client script that is evaluated on the client prior to initiating the callback. The result of the script is passed back to the client event handler. </param>
	/// <param name="clientErrorCallback">The name of the client event handler that receives the result when an error occurs in the server event handler. </param>
	/// <param name="useAsync">
	///       <see langword="true " />to perform the callback asynchronously; <see langword="false" /> to perform the callback synchronously. </param>
	/// <returns>The name of a client function that invokes the client callback. </returns>
	/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.Control" /> specified is <see langword="null" />. </exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.Control" /> specified does not implement the <see cref="T:System.Web.UI.ICallbackEventHandler" /> interface.</exception>
	public string GetCallbackEventReference(Control control, string argument, string clientCallback, string context, string clientErrorCallback, bool useAsync)
	{
		if (control == null)
		{
			throw new ArgumentNullException("control");
		}
		if (!(control is ICallbackEventHandler))
		{
			throw new InvalidOperationException("The control must implement the ICallbackEventHandler interface and provide a RaiseCallbackEvent method.");
		}
		return GetCallbackEventReference("'" + control.UniqueID + "'", argument, clientCallback, context, clientErrorCallback, useAsync);
	}

	/// <summary>Obtains a reference to a client function that, when invoked, initiates a client call back to server events. The client function for this overloaded method includes a specified target, argument, client script, context, error handler, and Boolean value.</summary>
	/// <param name="target">The name of a server <see cref="T:System.Web.UI.Control" /> that handles the client callback. The control must implement the <see cref="T:System.Web.UI.ICallbackEventHandler" /> interface and provide a <see cref="M:System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent(System.String)" /> method.</param>
	/// <param name="argument">An argument passed from the client script to the server 
	///       <see cref="M:System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent(System.String)" />  method. </param>
	/// <param name="clientCallback">The name of the client event handler that receives the result of the successful server event. </param>
	/// <param name="context">The client script that is evaluated on the client prior to initiating the callback. The result of the script is passed back to the client event handler.</param>
	/// <param name="clientErrorCallback">The name of the client event handler that receives the result when an error occurs in the server event handler. </param>
	/// <param name="useAsync">
	///       <see langword="true " /> to perform the callback asynchronously; <see langword="false" /> to perform the callback synchronously.</param>
	/// <returns>The name of a client function that invokes the client callback. </returns>
	public string GetCallbackEventReference(string target, string argument, string clientCallback, string context, string clientErrorCallback, bool useAsync)
	{
		RegisterWebFormClientScript();
		Page page = OwnerPage;
		if (!_initCallBackRegistered)
		{
			_initCallBackRegistered = true;
			RegisterStartupScript(typeof(Page), "WebForm_InitCallback", page.WebFormScriptReference + ".WebForm_InitCallback();", addScriptTags: true);
		}
		return page.WebFormScriptReference + ".WebForm_DoCallback(" + target + "," + (argument ?? "null") + "," + clientCallback + "," + (context ?? "null") + "," + (clientErrorCallback ?? "null") + "," + (useAsync ? "true" : "false") + ")";
	}

	/// <summary>Gets a URL reference to a resource in an assembly.</summary>
	/// <param name="type">The type of the resource. </param>
	/// <param name="resourceName">The fully qualified name of the resource in the assembly. </param>
	/// <returns>The URL reference to the resource.</returns>
	/// <exception cref="T:System.ArgumentNullException">The web resource type is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentNullException">The web resource name is <see langword="null" />.- or -The web resource name has a length of zero.</exception>
	public string GetWebResourceUrl(Type type, string resourceName)
	{
		if (type == null)
		{
			throw new ArgumentNullException("type");
		}
		if (resourceName == null || resourceName.Length == 0)
		{
			throw new ArgumentNullException("type");
		}
		return AssemblyResourceLoader.GetResourceUrl(type, resourceName);
	}

	/// <summary>Determines whether the client script block is registered with the <see cref="T:System.Web.UI.Page" /> object using the specified key. </summary>
	/// <param name="key">The key of the client script block to search for.</param>
	/// <returns>
	///     <see langword="true" /> if the client script block is registered; otherwise, <see langword="false" />.</returns>
	public bool IsClientScriptBlockRegistered(string key)
	{
		return IsScriptRegistered(clientScriptBlocks, GetType(), key);
	}

	/// <summary>Determines whether the client script block is registered with the <see cref="T:System.Web.UI.Page" /> object using a key and type.</summary>
	/// <param name="type">The type of the client script block to search for.  </param>
	/// <param name="key">The key of the client script block to search for. </param>
	/// <returns>
	///     <see langword="true" /> if the client script block is registered; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">The client script type is <see langword="null" />.</exception>
	public bool IsClientScriptBlockRegistered(Type type, string key)
	{
		return IsScriptRegistered(clientScriptBlocks, type, key);
	}

	/// <summary>Determines whether the startup script is registered with the <see cref="T:System.Web.UI.Page" /> object using the specified key.</summary>
	/// <param name="key">The key of the startup script to search for.</param>
	/// <returns>
	///     <see langword="true" /> if the startup script is registered; otherwise, <see langword="false" />.</returns>
	public bool IsStartupScriptRegistered(string key)
	{
		return IsScriptRegistered(startupScriptBlocks, GetType(), key);
	}

	/// <summary>Determines whether the startup script is registered with the <see cref="T:System.Web.UI.Page" /> object using the specified key and type.</summary>
	/// <param name="type">The type of the startup script to search for. </param>
	/// <param name="key">The key of the startup script to search for.</param>
	/// <returns>
	///     <see langword="true" /> if the startup script is registered; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">The startup script type is <see langword="null" />.</exception>
	public bool IsStartupScriptRegistered(Type type, string key)
	{
		return IsScriptRegistered(startupScriptBlocks, type, key);
	}

	/// <summary>Determines whether the OnSubmit statement is registered with the <see cref="T:System.Web.UI.Page" /> object using the specified key. </summary>
	/// <param name="key">The key of the OnSubmit statement to search for.</param>
	/// <returns>
	///     <see langword="true" /> if the OnSubmit statement is registered; otherwise, <see langword="false" />.</returns>
	public bool IsOnSubmitStatementRegistered(string key)
	{
		return IsScriptRegistered(submitStatements, GetType(), key);
	}

	/// <summary>Determines whether the OnSubmit statement is registered with the <see cref="T:System.Web.UI.Page" /> object using the specified key and type.</summary>
	/// <param name="type">The type of the OnSubmit statement to search for. </param>
	/// <param name="key">The key of the OnSubmit statement to search for. </param>
	/// <returns>
	///     <see langword="true" /> if the OnSubmit statement is registered; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">The OnSubmit statement type is <see langword="null" />.</exception>
	public bool IsOnSubmitStatementRegistered(Type type, string key)
	{
		return IsScriptRegistered(submitStatements, type, key);
	}

	/// <summary>Determines whether the client script include is registered with the <see cref="T:System.Web.UI.Page" /> object using the specified key. </summary>
	/// <param name="key">The key of the client script include to search for. </param>
	/// <returns>
	///     <see langword="true" /> if the client script include is registered; otherwise, <see langword="false" />.</returns>
	public bool IsClientScriptIncludeRegistered(string key)
	{
		return IsClientScriptIncludeRegistered(GetType(), key);
	}

	/// <summary>Determines whether the client script include is registered with the <see cref="T:System.Web.UI.Page" /> object using a key and type.</summary>
	/// <param name="type">The type of the client script include to search for. </param>
	/// <param name="key">The key of the client script include to search for. </param>
	/// <returns>
	///     <see langword="true" /> if the client script include is registered; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">The client script include type is <see langword="null" />.</exception>
	public bool IsClientScriptIncludeRegistered(Type type, string key)
	{
		return IsScriptRegistered(clientScriptBlocks, type, "include-" + key);
	}

	private bool IsScriptRegistered(ScriptEntry scriptList, Type type, string key)
	{
		while (scriptList != null)
		{
			if (scriptList.Type == type && scriptList.Key == key)
			{
				return true;
			}
			scriptList = scriptList.Next;
		}
		return false;
	}

	/// <summary>Registers a JavaScript array declaration with the <see cref="T:System.Web.UI.Page" /> object using an array name and array value.</summary>
	/// <param name="arrayName">The array name to register.</param>
	/// <param name="arrayValue">The array value or values to register.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="arrayName" /> is <see langword="null" />.</exception>
	public void RegisterArrayDeclaration(string arrayName, string arrayValue)
	{
		if (registeredArrayDeclares == null)
		{
			registeredArrayDeclares = new Hashtable();
		}
		if (!registeredArrayDeclares.ContainsKey(arrayName))
		{
			registeredArrayDeclares.Add(arrayName, new ArrayList());
		}
		((ArrayList)registeredArrayDeclares[arrayName]).Add(arrayValue);
		OwnerPage.RequiresFormScriptDeclaration();
	}

	private void RegisterScript(ref ScriptEntry scriptList, Type type, string key, string script, bool addScriptTags)
	{
		RegisterScript(ref scriptList, type, key, script, addScriptTags ? ScriptEntryFormat.AddScriptTag : ScriptEntryFormat.None);
	}

	private void RegisterScript(ref ScriptEntry scriptList, Type type, string key, string script, ScriptEntryFormat format)
	{
		ScriptEntry scriptEntry = null;
		ScriptEntry scriptEntry2;
		for (scriptEntry2 = scriptList; scriptEntry2 != null; scriptEntry2 = scriptEntry2.Next)
		{
			if (scriptEntry2.Type == type && scriptEntry2.Key == key)
			{
				return;
			}
			scriptEntry = scriptEntry2;
		}
		scriptEntry2 = new ScriptEntry(type, key, script, format);
		if (scriptEntry != null)
		{
			scriptEntry.Next = scriptEntry2;
		}
		else
		{
			scriptList = scriptEntry2;
		}
	}

	internal void RegisterClientScriptBlock(string key, string script)
	{
		RegisterScript(ref clientScriptBlocks, GetType(), key, script, addScriptTags: false);
	}

	/// <summary>Registers the client script with the <see cref="T:System.Web.UI.Page" /> object using a type, key, and script literal.</summary>
	/// <param name="type">The type of the client script to register. </param>
	/// <param name="key">The key of the client script to register. </param>
	/// <param name="script">The client script literal to register. </param>
	public void RegisterClientScriptBlock(Type type, string key, string script)
	{
		RegisterClientScriptBlock(type, key, script, addScriptTags: false);
	}

	/// <summary>Registers the client script with the <see cref="T:System.Web.UI.Page" /> object using a type, key, script literal, and Boolean value indicating whether to add script tags.</summary>
	/// <param name="type">The type of the client script to register. </param>
	/// <param name="key">The key of the client script to register. </param>
	/// <param name="script">The client script literal to register.  </param>
	/// <param name="addScriptTags">A Boolean value indicating whether to add script tags.</param>
	/// <exception cref="T:System.ArgumentNullException">The client script block type is <see langword="null" />.</exception>
	public void RegisterClientScriptBlock(Type type, string key, string script, bool addScriptTags)
	{
		if (type == null)
		{
			throw new ArgumentNullException("type");
		}
		RegisterScript(ref clientScriptBlocks, type, key, script, addScriptTags);
	}

	/// <summary>Registers a hidden value with the <see cref="T:System.Web.UI.Page" /> object.</summary>
	/// <param name="hiddenFieldName">The name of the hidden field to register.</param>
	/// <param name="hiddenFieldInitialValue">The initial value of the field to register.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="hiddenFieldName" /> is <see langword="null" />.</exception>
	public void RegisterHiddenField(string hiddenFieldName, string hiddenFieldInitialValue)
	{
		if (hiddenFields == null)
		{
			hiddenFields = new Hashtable();
		}
		if (!hiddenFields.ContainsKey(hiddenFieldName))
		{
			hiddenFields.Add(hiddenFieldName, hiddenFieldInitialValue);
		}
	}

	internal void RegisterOnSubmitStatement(string key, string script)
	{
		RegisterScript(ref submitStatements, GetType(), key, script, addScriptTags: false);
	}

	/// <summary>Registers an OnSubmit statement with the <see cref="T:System.Web.UI.Page" /> object using a type, a key, and a script literal. The statement executes when the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> is submitted.</summary>
	/// <param name="type">The type of the OnSubmit statement to register. </param>
	/// <param name="key">The key of the OnSubmit statement to register. </param>
	/// <param name="script">The script literal of the OnSubmit statement to register. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="type" /> is <see langword="null" />.</exception>
	public void RegisterOnSubmitStatement(Type type, string key, string script)
	{
		if (type == null)
		{
			throw new ArgumentNullException("type");
		}
		RegisterScript(ref submitStatements, type, key, script, addScriptTags: false);
	}

	internal void RegisterStartupScript(string key, string script)
	{
		RegisterScript(ref startupScriptBlocks, GetType(), key, script, addScriptTags: false);
	}

	/// <summary>Registers the startup script with the <see cref="T:System.Web.UI.Page" /> object using a type, a key, and a script literal.</summary>
	/// <param name="type">The type of the startup script to register. </param>
	/// <param name="key">The key of the startup script to register. </param>
	/// <param name="script">The startup script literal to register. </param>
	public void RegisterStartupScript(Type type, string key, string script)
	{
		RegisterStartupScript(type, key, script, addScriptTags: false);
	}

	/// <summary>Registers the startup script with the <see cref="T:System.Web.UI.Page" /> object using a type, a key, a script literal, and a Boolean value indicating whether to add script tags.</summary>
	/// <param name="type">The type of the startup script to register. </param>
	/// <param name="key">The key of the startup script to register. </param>
	/// <param name="script">The startup script literal to register. </param>
	/// <param name="addScriptTags">A Boolean value indicating whether to add script tags. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="type" /> is <see langword="null" />.</exception>
	public void RegisterStartupScript(Type type, string key, string script, bool addScriptTags)
	{
		if (type == null)
		{
			throw new ArgumentNullException("type");
		}
		RegisterScript(ref startupScriptBlocks, type, key, script, addScriptTags);
	}

	/// <summary>Registers the client script with the <see cref="T:System.Web.UI.Page" /> object using a key and a URL, which enables the script to be called from the client.</summary>
	/// <param name="key">The key of the client script include to register. </param>
	/// <param name="url">The URL of the client script include to register. </param>
	public void RegisterClientScriptInclude(string key, string url)
	{
		RegisterClientScriptInclude(GetType(), key, url);
	}

	/// <summary>Registers the client script include with the <see cref="T:System.Web.UI.Page" /> object using a type, a key, and a URL.</summary>
	/// <param name="type">The type of the client script include to register. </param>
	/// <param name="key">The key of the client script include to register. </param>
	/// <param name="url">The URL of the client script include to register. </param>
	/// <exception cref="T:System.ArgumentNullException">The client script include type is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">The URL is <see langword="null" />. - or -The URL is empty.</exception>
	public void RegisterClientScriptInclude(Type type, string key, string url)
	{
		if (type == null)
		{
			throw new ArgumentNullException("type");
		}
		if (url == null || url.Length == 0)
		{
			throw new ArgumentException("url");
		}
		RegisterScript(ref clientScriptBlocks, type, "include-" + key, url, ScriptEntryFormat.Include);
	}

	/// <summary>Registers the client script resource with the <see cref="T:System.Web.UI.Page" /> object using a type and a resource name.</summary>
	/// <param name="type">The type of the client script resource to register. </param>
	/// <param name="resourceName">The name of the client script resource to register. </param>
	/// <exception cref="T:System.ArgumentNullException">The client resource type is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentNullException">The client resource name is <see langword="null" />.- or -The client resource name has a length of zero.</exception>
	public void RegisterClientScriptResource(Type type, string resourceName)
	{
		RegisterScript(ref clientScriptBlocks, type, "resource-" + resourceName, GetWebResourceUrl(type, resourceName), ScriptEntryFormat.Include);
	}

	/// <summary>Registers a name/value pair as a custom (expando) attribute of the specified control given a control ID, attribute name, and attribute value.</summary>
	/// <param name="controlId">The <see cref="T:System.Web.UI.Control" /> on the page that contains the custom attribute. </param>
	/// <param name="attributeName">The name of the custom attribute to register. </param>
	/// <param name="attributeValue">The value of the custom attribute. </param>
	public void RegisterExpandoAttribute(string controlId, string attributeName, string attributeValue)
	{
		RegisterExpandoAttribute(controlId, attributeName, attributeValue, encode: true);
	}

	/// <summary>Registers a name/value pair as a custom (expando) attribute of the specified control given a control ID, an attribute name, an attribute value, and a Boolean value indicating whether to encode the attribute value.</summary>
	/// <param name="controlId">The <see cref="T:System.Web.UI.Control" /> on the page that contains the custom attribute.</param>
	/// <param name="attributeName">The name of the custom attribute to register.</param>
	/// <param name="attributeValue">The value of the custom attribute.</param>
	/// <param name="encode">A Boolean value indicating whether to encode the custom attribute to register.</param>
	public void RegisterExpandoAttribute(string controlId, string attributeName, string attributeValue, bool encode)
	{
		if (controlId == null)
		{
			throw new ArgumentNullException("controlId");
		}
		if (attributeName == null)
		{
			throw new ArgumentNullException("attributeName");
		}
		if (expandoAttributes == null)
		{
			expandoAttributes = new Hashtable();
		}
		ListDictionary listDictionary = (ListDictionary)expandoAttributes[controlId];
		if (listDictionary == null)
		{
			listDictionary = new ListDictionary();
			expandoAttributes[controlId] = listDictionary;
		}
		listDictionary.Add(attributeName, encode ? StrUtils.EscapeQuotesAndBackslashes(attributeValue) : attributeValue);
	}

	private void EnsureEventValidationArray()
	{
		if (eventValidationValues == null || eventValidationValues.Length == 0)
		{
			eventValidationValues = new int[64];
		}
		int num = eventValidationValues.Length;
		if (eventValidationPos >= num)
		{
			int[] destinationArray = new int[num * 2];
			Array.Copy(eventValidationValues, destinationArray, num);
			eventValidationValues = destinationArray;
		}
	}

	internal void ResetEventValidationState()
	{
		_pageInRender = true;
		eventValidationPos = 0;
	}

	private int CalculateEventHash(string uniqueId, string argument)
	{
		int hashCode = uniqueId.GetHashCode();
		int num = ((!string.IsNullOrEmpty(argument)) ? argument.GetHashCode() : 0);
		return hashCode ^ num;
	}

	/// <summary>Registers an event reference for validation with <see cref="T:System.Web.UI.PostBackOptions" />.</summary>
	/// <param name="options">A <see cref="T:System.Web.UI.PostBackOptions" /> object that specifies how client JavaScript is generated to initiate a postback event.</param>
	public void RegisterForEventValidation(PostBackOptions options)
	{
		RegisterForEventValidation(options.TargetControl.UniqueID, options.Argument);
	}

	/// <summary>Registers an event reference for validation with a unique control ID representing the client control generating the event.</summary>
	/// <param name="uniqueId">A unique ID representing the client control generating the event.</param>
	public void RegisterForEventValidation(string uniqueId)
	{
		RegisterForEventValidation(uniqueId, null);
	}

	/// <summary>Registers an event reference for validation with a unique control ID and event arguments representing the client control generating the event.</summary>
	/// <param name="uniqueId">A unique ID representing the client control generating the event.</param>
	/// <param name="argument">Event arguments passed with the client event.</param>
	/// <exception cref="T:System.InvalidOperationException">The method is called prior to the <see cref="M:System.Web.UI.Page.Render(System.Web.UI.HtmlTextWriter)" /> method.</exception>
	public void RegisterForEventValidation(string uniqueId, string argument)
	{
		Page page = OwnerPage;
		if (!page.EnableEventValidation || uniqueId == null || uniqueId.Length == 0)
		{
			return;
		}
		if (page.IsCallback)
		{
			_hasRegisteredForEventValidationOnCallback = true;
		}
		else if (!_pageInRender)
		{
			throw new InvalidOperationException("RegisterForEventValidation may only be called from the Render method");
		}
		EnsureEventValidationArray();
		int num = CalculateEventHash(uniqueId, argument);
		for (int i = 0; i < eventValidationPos; i++)
		{
			if (eventValidationValues[i] == num)
			{
				return;
			}
		}
		eventValidationValues[eventValidationPos++] = num;
	}

	/// <summary>Validates a client event that was registered for event validation using the <see cref="M:System.Web.UI.ClientScriptManager.RegisterForEventValidation(System.String)" /> method.</summary>
	/// <param name="uniqueId">A unique ID representing the client control generating the event.</param>
	public void ValidateEvent(string uniqueId)
	{
		ValidateEvent(uniqueId, null);
	}

	private ArgumentException InvalidPostBackException()
	{
		return new ArgumentException("Invalid postback or callback argument. Event validation is enabled using <pages enableEventValidation=\"true\"/> in configuration or <%@ Page EnableEventValidation=\"true\" %> in a page. For security purposes, this feature verifies that arguments to postback or callback events originate from the server control that originally rendered them. If the data is valid and expected, use the ClientScriptManager.RegisterForEventValidation method in order to register the postback or callback data for validation.");
	}

	/// <summary>Validates a client event that was registered for event validation using the <see cref="M:System.Web.UI.ClientScriptManager.RegisterForEventValidation(System.String,System.String)" /> method.</summary>
	/// <param name="uniqueId">A unique ID representing the client control generating the event.</param>
	/// <param name="argument">The event arguments passed with the client event.</param>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="uniqueId" /> is <see langword="null" /> or an empty string ("").</exception>
	public void ValidateEvent(string uniqueId, string argument)
	{
		if (uniqueId == null || uniqueId.Length == 0)
		{
			throw new ArgumentException("must not be null or empty", "uniqueId");
		}
		if (!OwnerPage.EnableEventValidation)
		{
			return;
		}
		if (eventValidationValues == null)
		{
			throw InvalidPostBackException();
		}
		int num = CalculateEventHash(uniqueId, argument);
		for (int i = 0; i < eventValidationValues.Length; i++)
		{
			if (eventValidationValues[i] == num)
			{
				return;
			}
		}
		throw InvalidPostBackException();
	}

	private void WriteScripts(HtmlTextWriter writer, ScriptEntry scriptList)
	{
		if (scriptList == null)
		{
			return;
		}
		writer.WriteLine();
		while (scriptList != null)
		{
			switch (scriptList.Format)
			{
			case ScriptEntryFormat.AddScriptTag:
				EnsureBeginScriptBlock(writer);
				writer.Write(scriptList.Script);
				break;
			case ScriptEntryFormat.Include:
				EnsureEndScriptBlock(writer);
				WriteClientScriptInclude(writer, scriptList.Script, scriptList.Type, scriptList.Key);
				break;
			default:
				EnsureEndScriptBlock(writer);
				writer.WriteLine(scriptList.Script);
				break;
			}
			scriptList = scriptList.Next;
		}
		EnsureEndScriptBlock(writer);
	}

	private void EnsureBeginScriptBlock(HtmlTextWriter writer)
	{
		if (!_scriptTagOpened)
		{
			WriteBeginScriptBlock(writer);
			_scriptTagOpened = true;
		}
	}

	private void EnsureEndScriptBlock(HtmlTextWriter writer)
	{
		if (_scriptTagOpened)
		{
			WriteEndScriptBlock(writer);
			_scriptTagOpened = false;
		}
	}

	internal void RestoreEventValidationState(string fieldValue)
	{
		Page page = OwnerPage;
		if (page.EnableEventValidation && fieldValue != null && fieldValue.Length != 0)
		{
			IStateFormatter formatter = page.GetFormatter();
			eventValidationValues = (int[])formatter.Deserialize(fieldValue);
			eventValidationPos = eventValidationValues.Length;
		}
	}

	internal void SaveEventValidationState()
	{
		if (OwnerPage.EnableEventValidation)
		{
			string eventValidationStateFormatted = GetEventValidationStateFormatted();
			if (eventValidationStateFormatted != null)
			{
				RegisterHiddenField("__EVENTVALIDATION", eventValidationStateFormatted);
			}
		}
	}

	internal string GetEventValidationStateFormatted()
	{
		if (eventValidationValues == null || eventValidationValues.Length == 0)
		{
			return null;
		}
		Page page = OwnerPage;
		if (page.IsCallback && !_hasRegisteredForEventValidationOnCallback)
		{
			return null;
		}
		IStateFormatter formatter = page.GetFormatter();
		int[] array = new int[eventValidationPos];
		Array.Copy(eventValidationValues, array, eventValidationPos);
		return formatter.Serialize(array);
	}

	internal void WriteExpandoAttributes(HtmlTextWriter writer)
	{
		if (expandoAttributes == null)
		{
			return;
		}
		writer.WriteLine();
		WriteBeginScriptBlock(writer);
		foreach (string key in expandoAttributes.Keys)
		{
			writer.WriteLine("var {0} = document.all ? document.all [\"{0}\"] : document.getElementById (\"{0}\");", key);
			ListDictionary listDictionary = (ListDictionary)expandoAttributes[key];
			foreach (string key2 in listDictionary.Keys)
			{
				writer.WriteLine("{0}.{1} = \"{2}\";", key, key2, listDictionary[key2]);
			}
		}
		WriteEndScriptBlock(writer);
		writer.WriteLine();
	}

	internal static void WriteBeginScriptBlock(HtmlTextWriter writer)
	{
		writer.WriteLine("<script type=\"text/javascript\">//<![CDATA[");
	}

	internal static void WriteEndScriptBlock(HtmlTextWriter writer)
	{
		writer.WriteLine("//]]></script>");
	}

	internal void WriteHiddenFields(HtmlTextWriter writer)
	{
		if (hiddenFields == null)
		{
			return;
		}
		writer.WriteLine();
		writer.AddAttribute(HtmlTextWriterAttribute.Class, "aspNetHidden");
		writer.RenderBeginTag(HtmlTextWriterTag.Div);
		int indent = writer.Indent;
		writer.Indent = 0;
		bool flag = true;
		StringBuilder stringBuilder = new StringBuilder();
		foreach (string key in hiddenFields.Keys)
		{
			string s = hiddenFields[key] as string;
			if (flag)
			{
				flag = false;
			}
			else
			{
				writer.WriteLine();
			}
			stringBuilder.Append("<input type=\"hidden\" name=\"");
			stringBuilder.Append(key);
			stringBuilder.Append("\" id=\"");
			stringBuilder.Append(key);
			stringBuilder.Append("\" value=\"");
			stringBuilder.Append(HttpUtility.HtmlAttributeEncode(s));
			stringBuilder.Append("\" />");
		}
		writer.Write(stringBuilder.ToString());
		writer.Indent = indent;
		writer.RenderEndTag();
		writer.WriteLine();
		hiddenFields = null;
	}

	internal void WriteClientScriptInclude(HtmlTextWriter writer, string path, Type type, string key)
	{
		if (!OwnerPage.IsMultiForm)
		{
			writer.WriteLine("<script src=\"{0}\" type=\"text/javascript\"></script>", path);
			return;
		}
		string arg = "inc_" + (type.FullName + key).GetHashCode().ToString("X");
		writer.WriteLine("<script type=\"text/javascript\">");
		writer.WriteLine("//<![CDATA[");
		writer.WriteLine("if (!window.{0}) {{", arg);
		writer.WriteLine("\twindow.{0} = true", arg);
		writer.WriteLine("\tdocument.write('<script src=\"{0}\" type=\"text/javascript\"><\\/script>'); }}", path);
		writer.WriteLine("//]]>");
		writer.WriteLine("</script>");
	}

	internal void WriteClientScriptBlocks(HtmlTextWriter writer)
	{
		WriteScripts(writer, clientScriptBlocks);
	}

	internal void WriteStartupScriptBlocks(HtmlTextWriter writer)
	{
		WriteScripts(writer, startupScriptBlocks);
	}

	internal void WriteArrayDeclares(HtmlTextWriter writer)
	{
		if (registeredArrayDeclares == null)
		{
			return;
		}
		writer.WriteLine();
		WriteBeginScriptBlock(writer);
		IDictionaryEnumerator enumerator = registeredArrayDeclares.GetEnumerator();
		Page page = OwnerPage;
		while (enumerator.MoveNext())
		{
			if (page.IsMultiForm)
			{
				writer.Write("\t" + page.theForm + ".");
			}
			else
			{
				writer.Write("\tvar ");
			}
			writer.Write(enumerator.Key);
			writer.Write(" =  new Array(");
			IEnumerator enumerator2 = ((ArrayList)enumerator.Value).GetEnumerator();
			bool flag = true;
			while (enumerator2.MoveNext())
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					writer.Write(", ");
				}
				writer.Write(enumerator2.Current);
			}
			writer.WriteLine(");");
		}
		WriteEndScriptBlock(writer);
		writer.WriteLine();
	}

	internal string GetClientValidationEvent(string validationGroup)
	{
		Page page = OwnerPage;
		if (page.IsMultiForm)
		{
			return "if (typeof(" + page.theForm + ".Page_ClientValidate) == 'function') " + page.theForm + ".Page_ClientValidate('" + validationGroup + "');";
		}
		return "if (typeof(Page_ClientValidate) == 'function') Page_ClientValidate('" + validationGroup + "');";
	}

	internal string GetClientValidationEvent()
	{
		Page page = OwnerPage;
		if (page.IsMultiForm)
		{
			return "if (typeof(" + page.theForm + ".Page_ClientValidate) == 'function') " + page.theForm + ".Page_ClientValidate();";
		}
		return "if (typeof(Page_ClientValidate) == 'function') Page_ClientValidate();";
	}

	internal string WriteSubmitStatements()
	{
		if (submitStatements == null)
		{
			return null;
		}
		StringBuilder stringBuilder = new StringBuilder();
		for (ScriptEntry next = submitStatements; next != null; next = next.Next)
		{
			stringBuilder.Append(EnsureEndsWithSemicolon(next.Script));
		}
		Page page = OwnerPage;
		RegisterClientScriptBlock(GetType(), "HtmlForm-OnSubmitStatemen", "\n" + page.WebFormScriptReference + ".WebForm_OnSubmit = function () {\n" + stringBuilder.ToString() + "\nreturn true;\n}\n", addScriptTags: true);
		return "javascript:return " + page.WebFormScriptReference + ".WebForm_OnSubmit();";
	}

	internal static string GetScriptLiteral(object ob)
	{
		if (ob == null)
		{
			return "null";
		}
		if (ob is string)
		{
			string text = (string)ob;
			bool flag = false;
			int length = text.Length;
			for (int i = 0; i < length; i++)
			{
				if (text[i] == '\\' || text[i] == '"')
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return "\"" + text + "\"";
			}
			StringBuilder stringBuilder = new StringBuilder(length + 10);
			stringBuilder.Append('"');
			for (int j = 0; j < length; j++)
			{
				if (text[j] == '"')
				{
					stringBuilder.Append("\\\"");
				}
				else if (text[j] == '\\')
				{
					stringBuilder.Append("\\\\");
				}
				else
				{
					stringBuilder.Append(text[j]);
				}
			}
			stringBuilder.Append('"');
			return stringBuilder.ToString();
		}
		if (ob is bool)
		{
			return ob.ToString().ToLower(Helpers.InvariantCulture);
		}
		return ob.ToString();
	}

	internal static string EnsureEndsWithSemicolon(string value)
	{
		if (value != null && value.Length > 0 && value[value.Length - 1] != ';')
		{
			return value += ";";
		}
		return value;
	}
}
