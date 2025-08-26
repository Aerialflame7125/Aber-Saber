using System.Collections;

namespace System.ComponentModel.Design.Data;

/// <summary>Represents a table in the data store.</summary>
public abstract class DesignerDataTable : DesignerDataTableBase
{
	/// <summary>Gets a collection of relationships defined for a table.</summary>
	/// <returns>A collection of <see cref="T:System.ComponentModel.Design.Data.DesignerDataRelationship" /> objects.</returns>
	[System.MonoTODO]
	public ICollection Relationships
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Data.DesignerDataTable" /> class with the specified name.</summary>
	/// <param name="name">The name of the table.</param>
	protected DesignerDataTable(string name)
		: base(name)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Data.DesignerDataTable" /> class with the specified name and owner.</summary>
	/// <param name="name">The name of the table.</param>
	/// <param name="owner">The owner of the table.</param>
	protected DesignerDataTable(string name, string owner)
		: base(name, owner)
	{
	}

	/// <summary>When overridden in a derived class, returns a collection of relationship objects.</summary>
	/// <returns>A collection of <see cref="T:System.ComponentModel.Design.Data.DesignerDataRelationship" /> objects.</returns>
	protected abstract ICollection CreateRelationships();
}
