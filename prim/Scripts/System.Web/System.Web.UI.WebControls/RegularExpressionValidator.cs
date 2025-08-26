using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Web.UI.WebControls;

/// <summary>Validates whether the value of an associated input control matches the pattern specified by a regular expression.</summary>
[ToolboxData("<{0}:RegularExpressionValidator runat=\"server\" ErrorMessage=\"RegularExpressionValidator\"></{0}:RegularExpressionValidator>")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class RegularExpressionValidator : BaseValidator
{
	/// <summary>Gets or sets the regular expression that determines the pattern used to validate a field.</summary>
	/// <returns>A string that specifies the regular expression used to validate a field for format. The default is <see cref="F:System.String.Empty" />.</returns>
	/// <exception cref="T:System.Web.HttpException">The regular expression is not properly formed. </exception>
	[Themeable(false)]
	[DefaultValue("")]
	[Editor("System.Web.UI.Design.WebControls.RegexTypeEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public string ValidationExpression
	{
		get
		{
			return ViewState.GetString("ValidationExpression", "");
		}
		set
		{
			ViewState["ValidationExpression"] = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.RegularExpressionValidator" /> class. </summary>
	public RegularExpressionValidator()
	{
	}

	/// <summary>Adds to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object the HTML attributes and styles that need to be rendered for the control. </summary>
	/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
	protected override void AddAttributesToRender(HtmlTextWriter writer)
	{
		if (base.RenderUplevel)
		{
			RegisterExpandoAttribute(ClientID, "evaluationfunction", "RegularExpressionValidatorEvaluateIsValid");
			if (ValidationExpression.Length > 0)
			{
				RegisterExpandoAttribute(ClientID, "validationexpression", ValidationExpression, encode: true);
			}
		}
		base.AddAttributesToRender(writer);
	}

	/// <summary>Indicates whether the value in the input control is valid.</summary>
	/// <returns>
	///     <see langword="true" /> if the value in the input control is valid; otherwise, <see langword="false" />.</returns>
	protected override bool EvaluateIsValid()
	{
		if (GetControlValidationValue(base.ControlToValidate).Trim() == "")
		{
			return true;
		}
		StringBuilder stringBuilder = new StringBuilder(ValidationExpression);
		if (stringBuilder.Length == 0 || stringBuilder[0] != '^')
		{
			stringBuilder.Insert(0, '^');
		}
		if (stringBuilder[stringBuilder.Length - 1] != '$')
		{
			stringBuilder.Append('$');
		}
		return Regex.IsMatch(GetControlValidationValue(base.ControlToValidate), stringBuilder.ToString());
	}
}
