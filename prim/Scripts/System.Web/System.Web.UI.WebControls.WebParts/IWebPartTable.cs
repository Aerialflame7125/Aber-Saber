using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts;

/// <summary>Defines a provider interface for connecting two server controls using an entire table of data.</summary>
public interface IWebPartTable
{
	/// <summary>Gets the schema information for a data table that is used to share data between two <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> describing the data.</returns>
	PropertyDescriptorCollection Schema { get; }

	/// <summary>Returns the data for the table that is being used by the interface as the basis of a connection between two <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls.</summary>
	/// <param name="callback">A <see cref="T:System.Web.UI.WebControls.WebParts.TableCallback" /> delegate that contains the address of a method that receives the data.</param>
	void GetTableData(TableCallback callback);
}
