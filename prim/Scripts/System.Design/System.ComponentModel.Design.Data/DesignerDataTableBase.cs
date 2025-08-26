using System.Collections;

namespace System.ComponentModel.Design.Data;

/// <summary>Defines the properties and methods shared between data-store tables and data-store views.</summary>
public abstract class DesignerDataTableBase
{
	private string name;

	private string owner;

	/// <summary>Gets the name of the data-store table or view.</summary>
	/// <returns>The name of the data-store table or view.</returns>
	public string Name => name;

	/// <summary>Gets the owner of the data-store table or view.</summary>
	/// <returns>The owner of the data-store table or view.</returns>
	public string Owner => owner;

	/// <summary>Gets a collection of columns defined for a table or view.</summary>
	/// <returns>A collection of columns defined for a table or view.</returns>
	[System.MonoTODO]
	public ICollection Columns
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Data.DesignerDataTableBase" /> class.</summary>
	/// <param name="name">The name of the table or view.</param>
	protected DesignerDataTableBase(string name)
		: this(name, null)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Data.DesignerDataTableBase" /> class.</summary>
	/// <param name="name">The name of the table or view.</param>
	/// <param name="owner">The data-store owner of the table or view.</param>
	protected DesignerDataTableBase(string name, string owner)
	{
		this.name = name;
		this.owner = owner;
	}

	/// <summary>When overridden in a derived class, returns a collection of data-store column objects.</summary>
	/// <returns>A collection of <see cref="T:System.ComponentModel.Design.Data.DesignerDataColumn" /> objects.</returns>
	protected abstract ICollection CreateColumns();
}
