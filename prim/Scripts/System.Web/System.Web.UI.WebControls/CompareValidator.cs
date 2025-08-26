using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Compares the value entered by the user in an input control with the value entered in another input control, or with a constant value.</summary>
[ToolboxData("<{0}:CompareValidator runat=\"server\" ErrorMessage=\"CompareValidator\"></{0}:CompareValidator>")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class CompareValidator : BaseCompareValidator
{
	/// <summary>Gets or sets the input control to compare with the input control being validated.</summary>
	/// <returns>The input control to compare with the input control being validated. The default value is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[TypeConverter(typeof(ValidatedControlConverter))]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	[Themeable(false)]
	public string ControlToCompare
	{
		get
		{
			return ViewState.GetString("ControlToCompare", string.Empty);
		}
		set
		{
			ViewState["ControlToCompare"] = value;
		}
	}

	/// <summary>Gets or sets the comparison operation to perform.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ValidationCompareOperator" /> values. The default value is <see langword="Equal" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified comparison operator is not one of the <see cref="T:System.Web.UI.WebControls.ValidationCompareOperator" /> values. </exception>
	[DefaultValue(ValidationCompareOperator.Equal)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	[Themeable(false)]
	public ValidationCompareOperator Operator
	{
		get
		{
			return (ValidationCompareOperator)ViewState.GetInt("Operator", 0);
		}
		set
		{
			ViewState["Operator"] = (int)value;
		}
	}

	/// <summary>Gets or sets a constant value to compare with the value entered by the user in the input control being validated.</summary>
	/// <returns>The constant value to compare with the value entered by the user in the input control being validated. The default value is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	[Themeable(false)]
	public string ValueToCompare
	{
		get
		{
			return ViewState.GetString("ValueToCompare", string.Empty);
		}
		set
		{
			ViewState["ValueToCompare"] = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.CompareValidator" /> class. </summary>
	public CompareValidator()
	{
	}

	/// <summary>Adds the attributes of this control to the output stream for rendering on the client.</summary>
	/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream for rendering on the client. </param>
	protected override void AddAttributesToRender(HtmlTextWriter writer)
	{
		if (base.RenderUplevel)
		{
			RegisterExpandoAttribute(ClientID, "evaluationfunction", "CompareValidatorEvaluateIsValid");
			if (ControlToCompare.Length > 0)
			{
				RegisterExpandoAttribute(ClientID, "controltocompare", GetControlRenderID(ControlToCompare));
			}
			if (ValueToCompare.Length > 0)
			{
				RegisterExpandoAttribute(ClientID, "valuetocompare", ValueToCompare, encode: true);
			}
			RegisterExpandoAttribute(ClientID, "operator", Operator.ToString());
		}
		base.AddAttributesToRender(writer);
	}

	/// <summary>Checks the properties of the control for valid values.</summary>
	/// <returns>
	///     <see langword="true" /> if the control properties are valid; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.Web.HttpException">
	///         <see cref="P:System.Web.UI.WebControls.BaseValidator.ControlToValidate" /> and <see cref="P:System.Web.UI.WebControls.CompareValidator.ControlToCompare" /> have the same <see cref="P:System.Web.UI.Control.ID" />.</exception>
	/// <exception cref="T:System.Web.HttpException">The value of a target property cannot be converted to the expected <see cref="T:System.Type" />.</exception>
	protected override bool ControlPropertiesValid()
	{
		if (Operator != ValidationCompareOperator.DataTypeCheck && ControlToCompare.Length == 0 && !BaseCompareValidator.CanConvert(ValueToCompare, base.Type, base.CultureInvariantValues))
		{
			throw new HttpException($"Unable to convert the value: {ValueToCompare} as a {Enum.GetName(typeof(ValidationDataType), base.Type)}");
		}
		if (ControlToCompare.Length > 0)
		{
			if (string.CompareOrdinal(ControlToCompare, base.ControlToValidate) == 0)
			{
				throw new HttpException($"Control '{ID}' cannot have the same value '{ControlToCompare}' for both ControlToValidate and ControlToCompare.");
			}
			CheckControlValidationProperty(ControlToCompare, string.Empty);
		}
		return base.ControlPropertiesValid();
	}

	/// <summary>When overridden in a derived class, this method contains the code to determine whether the value in the input control is valid.</summary>
	/// <returns>
	///     <see langword="true" /> if the value in the input control is valid; otherwise, <see langword="false" />.</returns>
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
		string controlToCompare = ControlToCompare;
		string rightText = ((!string.IsNullOrEmpty(controlToCompare)) ? GetControlValidationValue(controlToCompare) : ValueToCompare);
		return BaseCompareValidator.Compare(GetControlValidationValue(base.ControlToValidate), cultureInvariantLeftText: false, rightText, base.CultureInvariantValues, Operator, base.Type);
	}
}
