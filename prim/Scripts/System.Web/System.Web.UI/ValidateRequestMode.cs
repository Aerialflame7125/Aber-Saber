namespace System.Web.UI;

/// <summary>Specifies the type of request validation for a control.</summary>
public enum ValidateRequestMode
{
	/// <summary>Request validation uses the same behavior as its parent control.</summary>
	Inherit,
	/// <summary>Request validation is disabled.</summary>
	Disabled,
	/// <summary>Request validation is enabled.</summary>
	Enabled
}
