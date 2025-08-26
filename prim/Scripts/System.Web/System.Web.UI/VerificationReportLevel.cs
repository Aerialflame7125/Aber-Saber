namespace System.Web.UI;

/// <summary>Specifies reporting levels for an accessibility rule defined by an <see cref="T:System.Web.UI.VerificationAttribute" /> instance.</summary>
public enum VerificationReportLevel
{
	/// <summary>The verification rule represented by the <see cref="T:System.Web.UI.VerificationAttribute" /> instance is an error.</summary>
	Error,
	/// <summary>The verification rule represented by the <see cref="T:System.Web.UI.VerificationAttribute" /> instance is a warning.</summary>
	Warning,
	/// <summary>The verification rule represented by the <see cref="T:System.Web.UI.VerificationAttribute" /> instance is a guideline.</summary>
	Guideline
}
