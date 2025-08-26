namespace System.Web.UI;

/// <summary>Determines how ASP.NET data source controls handle data conflicts when updating or deleting data.</summary>
public enum ConflictOptions
{
	/// <summary>A data source control overwrites all values in a data row with its own values for the row.</summary>
	OverwriteChanges,
	/// <summary>A data source control uses the <paramref name="oldValues" /> collection of the <see langword="Update" /> and <see langword="Delete" /> methods to determine whether the data has been changed by another process.</summary>
	CompareAllValues
}
