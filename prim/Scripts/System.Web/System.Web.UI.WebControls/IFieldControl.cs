namespace System.Web.UI.WebControls;

/// <summary>Represents a contract that exposes properties that automatically generate fields that are based on data in a data-bound control.</summary>
public interface IFieldControl
{
	/// <summary>Gets or sets the <see cref="T:System.Web.UI.IAutoFieldGenerator" /> interface, which is the interface that generates fields in a data-bound control.</summary>
	/// <returns>The interface that generates fields in data-bound controls.</returns>
	IAutoFieldGenerator FieldsGenerator { get; set; }
}
