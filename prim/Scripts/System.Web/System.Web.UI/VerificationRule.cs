namespace System.Web.UI;

/// <summary>Specifies how conditional expressions defined by an <see cref="T:System.Web.UI.VerificationAttribute" /> instance are used in verification.</summary>
public enum VerificationRule
{
	/// <summary>The conditional expression specified in an <see cref="T:System.Web.UI.VerificationAttribute" /> instance is required.</summary>
	Required,
	/// <summary>The conditional expression specified in an <see cref="T:System.Web.UI.VerificationAttribute" /> instance is prohibited.</summary>
	Prohibited,
	/// <summary>The conditional expression specified in an <see cref="T:System.Web.UI.VerificationAttribute" /> instance must have a left hand side that is not an empty string ("").</summary>
	NotEmptyString
}
