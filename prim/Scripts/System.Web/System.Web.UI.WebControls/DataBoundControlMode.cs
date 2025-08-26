namespace System.Web.UI.WebControls;

/// <summary>Represents the different data-entry modes for a data-bound control or a particular field in ASP.NET Dynamic Data.</summary>
public enum DataBoundControlMode
{
	/// <summary>Represents the display mode, which prevents the user from modifying the values of a record or a data field.</summary>
	ReadOnly,
	/// <summary>Represents the edit mode, which enables users to update the values of an existing record or data field. </summary>
	Edit,
	/// <summary>Represents the insert mode, which enables users to enter values for a new record or data field.</summary>
	Insert
}
