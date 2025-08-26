using System.ComponentModel.Design;

namespace System.Web.UI.Design.WebControls;

/// <summary>Provides a user interface for editing the collection of cells in a table row.</summary>
public class TableCellsCollectionEditor : CollectionEditor
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.TableCellsCollectionEditor" /> class.</summary>
	/// <param name="type">The type of the collection to edit.</param>
	public TableCellsCollectionEditor(Type type)
		: base(type)
	{
	}

	/// <summary>Indicates whether multiple table cells can be selected at the same time.</summary>
	/// <returns>
	///   <see langword="true" /> if multiple cells can be selected at the same time; otherwise, <see langword="false" />.</returns>
	protected override bool CanSelectMultipleInstances()
	{
		throw new NotImplementedException();
	}

	/// <summary>Creates an instance of the editor for use with the specified type.</summary>
	/// <param name="itemType">The <see cref="T:System.Type" /> of the item to create.</param>
	/// <returns>An object of the specified type.</returns>
	protected override object CreateInstance(Type itemType)
	{
		throw new NotImplementedException();
	}
}
