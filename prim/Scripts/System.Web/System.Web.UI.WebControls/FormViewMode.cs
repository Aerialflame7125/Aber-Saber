namespace System.Web.UI.WebControls;

/// <summary>Represents the different data-entry modes of a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
public enum FormViewMode
{
	/// <summary>A display mode that prevents the user from modifying the values of a record.</summary>
	ReadOnly,
	/// <summary>An editing mode that allows the user to update the values of an existing record.</summary>
	Edit,
	/// <summary>An inserting mode that allows the user to enter the values for a new record.</summary>
	Insert
}
