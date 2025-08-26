using System.ComponentModel.Design;

namespace System.Web.UI.Design.WebControls;

/// <summary>Provides a user interface for editing rows of a table.</summary>
public class TableRowsCollectionEditor : CollectionEditor
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.TableRowsCollectionEditor" /> class.</summary>
	/// <param name="type">The type of the collection to edit.</param>
	public TableRowsCollectionEditor(Type type)
		: base(type)
	{
	}

	/// <summary>Indicates whether multiple instances may be selected.</summary>
	/// <returns>
	///   <see langword="true" /> if multiple items can be selected at once; otherwise, <see langword="false" />. This implementation always returns <see langword="false" />.</returns>
	protected override bool CanSelectMultipleInstances()
	{
		throw new NotImplementedException();
	}

	/// <summary>Creates an instance of the specified type.</summary>
	/// <param name="itemType">The <see cref="T:System.Type" /> of the item to create.</param>
	/// <returns>An object of the specified type.</returns>
	protected override object CreateInstance(Type itemType)
	{
		throw new NotImplementedException();
	}
}
