using System.ComponentModel;

namespace System.Web.UI;

/// <summary>Specifies how client-side JavaScript is generated to initiate a postback event.</summary>
public sealed class PostBackOptions
{
	private Control control;

	private string argument;

	private string actionUrl;

	private bool autoPostBack;

	private bool requiresJavaScriptProtocol;

	private bool trackFocus;

	private bool clientSubmit;

	private bool performValidation;

	private string validationGroup;

	/// <summary>Gets or sets the target URL for the postback of a Web Forms page.</summary>
	/// <returns>The URL for the postback of a Web Forms page. The default value is an empty string ("").</returns>
	[DefaultValue("")]
	public string ActionUrl
	{
		get
		{
			return actionUrl;
		}
		set
		{
			actionUrl = value;
		}
	}

	/// <summary>Gets or sets an optional argument that is transferred in the postback event.</summary>
	/// <returns>The optional argument that is transferred in the postback event. The default value is an empty string ("").</returns>
	[DefaultValue("")]
	public string Argument
	{
		get
		{
			return argument;
		}
		set
		{
			argument = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether the form will automatically post back to the server in response to a user action.</summary>
	/// <returns>
	///     <see langword="true" /> if the form will automatically post back in response to a user action; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	[MonoTODO("Implement support for this in Page")]
	[DefaultValue(false)]
	public bool AutoPostBack
	{
		get
		{
			return autoPostBack;
		}
		set
		{
			autoPostBack = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the postback event should occur from client-side script.</summary>
	/// <returns>
	///     <see langword="true" /> if the postback event should occur from client-side script; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
	[DefaultValue(true)]
	public bool ClientSubmit
	{
		get
		{
			return clientSubmit;
		}
		set
		{
			clientSubmit = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether client-side validation is required before the postback event occurs.</summary>
	/// <returns>
	///     <see langword="true" /> if client-side validation is required before the postback event occurs; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	[DefaultValue(false)]
	public bool PerformValidation
	{
		get
		{
			return performValidation;
		}
		set
		{
			performValidation = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see langword="javascript:" /> prefix is generated for the client-side script. </summary>
	/// <returns>
	///     <see langword="true" /> if the <see langword="javascript:" /> prefix is generated for the client-side script; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
	[DefaultValue(true)]
	public bool RequiresJavaScriptProtocol
	{
		get
		{
			return requiresJavaScriptProtocol;
		}
		set
		{
			requiresJavaScriptProtocol = value;
		}
	}

	/// <summary>Gets the control target that receives the postback event.</summary>
	/// <returns>A <see cref="T:System.Web.UI.Control" /> that represents the control that receives the postback event.</returns>
	[DefaultValue(null)]
	public Control TargetControl => control;

	/// <summary>Gets or sets a value indicating whether the postback event should return the page to the current scroll position and return focus to the current control.</summary>
	/// <returns>
	///     <see langword="true" /> if the postback event should return the page to the current scroll position and return focus to the target control; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	[MonoTODO("Implement support for this in Page")]
	[DefaultValue(false)]
	public bool TrackFocus
	{
		get
		{
			return trackFocus;
		}
		set
		{
			trackFocus = value;
		}
	}

	/// <summary>Gets or sets the group of controls for which the <see cref="T:System.Web.UI.PostBackOptions" /> object causes validation when it posts back to the server. </summary>
	/// <returns>The group of controls for which the <see cref="T:System.Web.UI.PostBackOptions" /> object causes validation when it posts back to the server. The default value is an empty string ("").</returns>
	[MonoTODO("Implement support for this in Page")]
	[DefaultValue("")]
	public string ValidationGroup
	{
		get
		{
			return validationGroup;
		}
		set
		{
			validationGroup = value;
		}
	}

	internal bool RequiresSpecialPostBack
	{
		get
		{
			if (actionUrl == null && validationGroup == null && !trackFocus && !autoPostBack)
			{
				return argument != null;
			}
			return true;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PostBackOptions" /> class with the specified target control data.</summary>
	/// <param name="targetControl">The <see cref="T:System.Web.UI.Control" /> that receives the postback event.</param>
	public PostBackOptions(Control targetControl)
		: this(targetControl, null, null, autoPostBack: false, requiresJavaScriptProtocol: false, trackFocus: false, clientSubmit: true, performValidation: false, null)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PostBackOptions" /> class with the specified target control and argument data.</summary>
	/// <param name="targetControl">The <see cref="T:System.Web.UI.Control" /> that receives the postback event.</param>
	/// <param name="argument">The optional parameter passed during the postback event.</param>
	public PostBackOptions(Control targetControl, string argument)
		: this(targetControl, argument, null, autoPostBack: false, requiresJavaScriptProtocol: false, trackFocus: false, clientSubmit: true, performValidation: false, null)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PostBackOptions" /> class with the specified values for the instance's properties.</summary>
	/// <param name="targetControl">The <see cref="T:System.Web.UI.Control" /> that receives the postback event.</param>
	/// <param name="argument">The optional parameter passed during the postback event.</param>
	/// <param name="actionUrl">The target of the postback.</param>
	/// <param name="autoPostBack">
	///       <see langword="true" /> to automatically post the form back to the server in response to a user action; otherwise, <see langword="false" />.</param>
	/// <param name="requiresJavaScriptProtocol">
	///       <see langword="true" /> if the <see langword="javascript:" /> prefix is required; otherwise, <see langword="false" />.</param>
	/// <param name="trackFocus">
	///       <see langword="true" /> if the postback event should return the page to the current scroll position and return focus to the target control; otherwise, <see langword="false" />.</param>
	/// <param name="clientSubmit">
	///       <see langword="true" /> if the postback event can be raised by client script; otherwise, <see langword="false" />.</param>
	/// <param name="performValidation">
	///       <see langword="true" /> if client-side validation is required before the postback event occurs; otherwise, <see langword="false" />.</param>
	/// <param name="validationGroup">The group of controls for which <see cref="T:System.Web.UI.PostBackOptions" /> causes validation when it posts back to the server.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="targetControl" /> is <see langword="null" />.</exception>
	public PostBackOptions(Control targetControl, string argument, string actionUrl, bool autoPostBack, bool requiresJavaScriptProtocol, bool trackFocus, bool clientSubmit, bool performValidation, string validationGroup)
	{
		if (targetControl == null)
		{
			throw new ArgumentNullException("targetControl");
		}
		control = targetControl;
		this.argument = argument;
		this.actionUrl = actionUrl;
		this.autoPostBack = autoPostBack;
		this.requiresJavaScriptProtocol = requiresJavaScriptProtocol;
		this.trackFocus = trackFocus;
		this.clientSubmit = clientSubmit;
		this.performValidation = performValidation;
		this.validationGroup = validationGroup;
	}
}
