using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Checks whether the value of an input control is within a specified range of values.</summary>
[ToolboxData("<{0}:RangeValidator runat=\"server\" ErrorMessage=\"RangeValidator\"></{0}:RangeValidator>")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class RangeValidator : BaseCompareValidator
{
	/// <summary>Gets or sets the maximum value of the validation range.</summary>
	/// <returns>The maximum value of the validation range. The default value is <see cref="F:System.String.Empty" />.</returns>
	[Themeable(false)]
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public string MaximumValue
	{
		get
		{
			return ViewState.GetString("MaximumValue", string.Empty);
		}
		set
		{
			ViewState["MaximumValue"] = value;
		}
	}

	/// <summary>Gets or sets the minimum value of the validation range.</summary>
	/// <returns>The minimum value of the validation range. The default value is <see cref="F:System.String.Empty" />.</returns>
	[Themeable(false)]
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public string MinimumValue
	{
		get
		{
			return ViewState.GetString("MinimumValue", string.Empty);
		}
		set
		{
			ViewState["MinimumValue"] = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.RangeValidator" /> class. </summary>
	public RangeValidator()
	{
	}

	/// <summary>Adds the HTML attributes and styles for the control that need to be rendered to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object. </summary>
	/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
	protected override void AddAttributesToRender(HtmlTextWriter writer)
	{
		base.AddAttributesToRender(writer);
		if (base.RenderUplevel)
		{
			RegisterExpandoAttribute(ClientID, "evaluationfunction", "RangeValidatorEvaluateIsValid");
			RegisterExpandoAttribute(ClientID, "minimumvalue", MinimumValue, encode: true);
			RegisterExpandoAttribute(ClientID, "maximumvalue", MaximumValue, encode: true);
		}
	}

	/// <summary>This is a check of properties to determine any errors made by the developer. </summary>
	/// <returns>
	///     <see langword="true" /> if the control properties are valid; otherwise, <see langword="false" />.</returns>
	protected override bool ControlPropertiesValid()
	{
		if (!BaseCompareValidator.CanConvert(MinimumValue, base.Type))
		{
			throw new HttpException("Minimum value cannot be converterd to type " + base.Type);
		}
		if (!BaseCompareValidator.CanConvert(MaximumValue, base.Type))
		{
			throw new HttpException("Maximum value cannot be converterd to type " + base.Type);
		}
		if (base.Type != 0 && BaseCompareValidator.Compare(MinimumValue, MaximumValue, ValidationCompareOperator.GreaterThan, base.Type))
		{
			throw new HttpException("Maximum value must be equal or bigger than Minimum value");
		}
		return base.ControlPropertiesValid();
	}

	/// <summary>Determines whether the content in the input control is valid.</summary>
	/// <returns>
	///     <see langword="true" /> if the control is valid; otherwise, <see langword="false" />.</returns>
	protected override bool EvaluateIsValid()
	{
		string controlValidationValue = GetControlValidationValue(base.ControlToValidate);
		if (controlValidationValue == null)
		{
			return true;
		}
		controlValidationValue = controlValidationValue.Trim();
		if (controlValidationValue.Length == 0)
		{
			return true;
		}
		if (BaseCompareValidator.Compare(controlValidationValue, MinimumValue, ValidationCompareOperator.GreaterThanEqual, base.Type) && BaseCompareValidator.Compare(controlValidationValue, MaximumValue, ValidationCompareOperator.LessThanEqual, base.Type))
		{
			return true;
		}
		return false;
	}
}
