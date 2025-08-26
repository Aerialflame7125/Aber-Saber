namespace System.Web.UI.WebControls;

/// <summary>Provides a property that is used by the <see cref="T:System.Web.DynamicData.DynamicDataManager" /> control to enable selecting data in a data-bound control through the query string.</summary>
public interface IPersistedSelector
{
	/// <summary>Gets or sets the data-key value for the selected record in a data-bound control.</summary>
	/// <returns>The data-key value for the selected record in a data-bound control.</returns>
	DataKey DataKey { get; set; }
}
