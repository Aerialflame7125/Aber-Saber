using System.Web.UI.WebControls;

namespace System.Web.UI;

/// <summary>Defines properties that specify how ASP.NET creates client IDs for a data-bound control.</summary>
public interface IDataKeysControl
{
	/// <summary>Gets the names of the data fields whose values are used to uniquely identify each instance of a data-bound control when ASP.NET generates the <see cref="P:System.Web.UI.Control.ClientID" /> value by using the <see cref="F:System.Web.UI.ClientIDMode.Predictable" /> algorithm.</summary>
	/// <returns>A collection of data-field names.</returns>
	string[] ClientIDRowSuffix { get; }

	/// <summary>Gets a collection of the data values that are used to uniquely identify each instance of a data-bound control when ASP.NET generates the <see cref="P:System.Web.UI.Control.ClientID" /> value.</summary>
	/// <returns>A collection of data-field values.</returns>
	DataKeyArray ClientIDRowSuffixDataKeys { get; }
}
