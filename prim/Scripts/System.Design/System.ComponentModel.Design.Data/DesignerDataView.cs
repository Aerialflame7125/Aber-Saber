namespace System.ComponentModel.Design.Data;

/// <summary>Represents a data view in the data store.</summary>
public abstract class DesignerDataView : DesignerDataTableBase
{
	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Data.DesignerDataView" /> class with the specified name.</summary>
	/// <param name="name">The name of the view.</param>
	protected DesignerDataView(string name)
		: base(name)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Data.DesignerDataView" /> class with the specified name and owner.</summary>
	/// <param name="name">The name of the view.</param>
	/// <param name="owner">The data-store owner of the view.</param>
	protected DesignerDataView(string name, string owner)
		: base(name, owner)
	{
	}
}
