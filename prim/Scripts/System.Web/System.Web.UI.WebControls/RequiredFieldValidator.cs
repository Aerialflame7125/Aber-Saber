using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Makes the associated input control a required field.</summary>
[ToolboxData("<{0}:RequiredFieldValidator runat=\"server\" ErrorMessage=\"RequiredFieldValidator\"></{0}:RequiredFieldValidator>")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class RequiredFieldValidator : BaseValidator
{
	/// <summary>Gets or sets the initial value of the associated input control.</summary>
	/// <returns>A string that specifies the initial value of the associated input control. The default is <see cref="F:System.String.Empty" />.</returns>
	[Themeable(false)]
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public string InitialValue
	{
		get
		{
			return ViewState.GetString("InitialValue", "");
		}
		set
		{
			ViewState["InitialValue"] = value;
		}
	}

	/// <summary>Adds the HTML attributes and styles that need to be rendered for the control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
	/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
	protected override void AddAttributesToRender(HtmlTextWriter writer)
	{
		if (base.RenderUplevel)
		{
			RegisterExpandoAttribute(ClientID, "evaluationfunction", "RequiredFieldValidatorEvaluateIsValid");
			RegisterExpandoAttribute(ClientID, "initialvalue", InitialValue, encode: true);
		}
		base.AddAttributesToRender(writer);
	}

	/// <summary>Called during the validation stage when ASP.NET processes a Web Form.</summary>
	/// <returns>
	///     <see langword="true" /> if the value in the input control is valid; otherwise, <see langword="false" />.</returns>
	protected override bool EvaluateIsValid()
	{
		return GetControlValidationValue(base.ControlToValidate) != InitialValue;
	}

	/// <summary>Initializes a new instance of <see cref="T:System.Web.UI.WebControls.RequiredFieldValidator" /> class.</summary>
	public RequiredFieldValidator()
	{
	}
}
