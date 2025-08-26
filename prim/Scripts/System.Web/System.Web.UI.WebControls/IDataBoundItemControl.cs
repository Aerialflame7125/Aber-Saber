namespace System.Web.UI.WebControls;

/// <summary>Exposes the properties that are used to display a single item in a data-bound control.</summary>
public interface IDataBoundItemControl : IDataBoundControl
{
	/// <summary>Gets the object that represents the data-key value of the row in a data-bound control.</summary>
	/// <returns>The object that represents the data-key value of the row in the data-bound control.</returns>
	DataKey DataKey { get; }

	/// <summary>Gets the current mode of a data-bound control.</summary>
	/// <returns>The current mode of the data-bound control.</returns>
	DataBoundControlMode Mode { get; }
}
