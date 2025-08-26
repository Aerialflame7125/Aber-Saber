namespace System.Web.UI.WebControls;

/// <summary>Specifies the display behavior of error messages in validation controls.</summary>
public enum ValidatorDisplay
{
	/// <summary>Validator content never displayed inline.</summary>
	None,
	/// <summary>Validator content physically part of the page layout.</summary>
	Static,
	/// <summary>Validator content dynamically added to the page when validation fails.</summary>
	Dynamic
}
