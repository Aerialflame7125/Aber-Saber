using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Performs user-defined validation on an input control.</summary>
[DefaultEvent("ServerValidate")]
[ToolboxData("<{0}:CustomValidator runat=\"server\" ErrorMessage=\"CustomValidator\"></{0}:CustomValidator>")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class CustomValidator : BaseValidator
{
	private static readonly object serverValidateEvent = new object();

	private EventHandlerList events = new EventHandlerList();

	/// <summary>Gets or sets the name of the custom client-side script function used for validation.</summary>
	/// <returns>The name of the custom client script function used for validation. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.The function name should not include any parentheses or parameters.</returns>
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	[Themeable(false)]
	public string ClientValidationFunction
	{
		get
		{
			return ViewState.GetString("ClientValidationFunction", string.Empty);
		}
		set
		{
			ViewState["ClientValidationFunction"] = value;
		}
	}

	/// <summary>Gets or sets a Boolean value indicating whether empty text should be validated.</summary>
	/// <returns>
	///     <see langword="true" /> if empty text should be validated; otherwise, <see langword="false" />.</returns>
	[Themeable(false)]
	[DefaultValue(false)]
	public bool ValidateEmptyText
	{
		get
		{
			return ViewState.GetBool("ValidateEmptyText", def: false);
		}
		set
		{
			ViewState["ValidateEmptyText"] = value;
		}
	}

	/// <summary>Occurs when validation is performed on the server.</summary>
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public event ServerValidateEventHandler ServerValidate
	{
		add
		{
			events.AddHandler(serverValidateEvent, value);
		}
		remove
		{
			events.RemoveHandler(serverValidateEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.CustomValidator" /> class. </summary>
	public CustomValidator()
	{
	}

	/// <summary>Adds the properties of the <see cref="T:System.Web.UI.WebControls.CustomValidator" /> control to the output stream for rendering on the client.</summary>
	/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream for rendering on the client. </param>
	protected override void AddAttributesToRender(HtmlTextWriter writer)
	{
		base.AddAttributesToRender(writer);
		if (base.RenderUplevel)
		{
			RegisterExpandoAttribute(ClientID, "evaluationfunction", "CustomValidatorEvaluateIsValid");
			if (ValidateEmptyText)
			{
				RegisterExpandoAttribute(ClientID, "validateemptytext", "true");
			}
			string clientValidationFunction = ClientValidationFunction;
			if (!string.IsNullOrEmpty(clientValidationFunction))
			{
				RegisterExpandoAttribute(ClientID, "clientvalidationfunction", clientValidationFunction, encode: true);
			}
		}
	}

	/// <summary>Checks the properties of the control for valid values.</summary>
	/// <returns>
	///     <see langword="true" /> if the control properties are valid; otherwise, <see langword="false" />.</returns>
	protected override bool ControlPropertiesValid()
	{
		if (string.IsNullOrEmpty(base.ControlToValidate))
		{
			return true;
		}
		return base.ControlPropertiesValid();
	}

	/// <summary>Overrides the <see cref="M:System.Web.UI.MobileControls.BaseValidator.EvaluateIsValid" /> method.</summary>
	/// <returns>
	///     <see langword="true" /> if the value in the input control is valid; otherwise, <see langword="false" />.</returns>
	protected override bool EvaluateIsValid()
	{
		string controlToValidate = base.ControlToValidate;
		if (!string.IsNullOrEmpty(controlToValidate))
		{
			string controlValidationValue = GetControlValidationValue(controlToValidate);
			if (string.IsNullOrEmpty(controlValidationValue) && !ValidateEmptyText)
			{
				return true;
			}
			return OnServerValidate(controlValidationValue);
		}
		return OnServerValidate(string.Empty);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.CustomValidator.ServerValidate" /> event for the <see cref="T:System.Web.UI.WebControls.CustomValidator" /> control.</summary>
	/// <param name="value">The value to validate. </param>
	/// <returns>
	///     <see langword="true" /> if the value specified by the <paramref name="value" /> parameter passes validation; otherwise, <see langword="false" />.</returns>
	protected virtual bool OnServerValidate(string value)
	{
		if (events[serverValidateEvent] is ServerValidateEventHandler serverValidateEventHandler)
		{
			ServerValidateEventArgs serverValidateEventArgs = new ServerValidateEventArgs(value, isValid: true);
			serverValidateEventHandler(this, serverValidateEventArgs);
			return serverValidateEventArgs.IsValid;
		}
		return true;
	}
}
