using System.Collections;

namespace System.ComponentModel.Design.Data;

/// <summary>Represents a stored procedure in the data store.</summary>
public abstract class DesignerDataStoredProcedure
{
	private string name;

	private string owner;

	/// <summary>Gets the name of the stored procedure.</summary>
	/// <returns>The name of the stored procedure.</returns>
	public string Name => name;

	/// <summary>Gets the owner of the stored procedure.</summary>
	/// <returns>The owner of the stored procedure.</returns>
	public string Owner => owner;

	/// <summary>Gets a collection of parameters required for a stored procedure.</summary>
	/// <returns>A collection of parameters for the stored procedure.</returns>
	[System.MonoTODO]
	public ICollection Parameters
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Data.DesignerDataStoredProcedure" /> class with the specified name.</summary>
	/// <param name="name">The name of the stored procedure.</param>
	[System.MonoTODO]
	protected DesignerDataStoredProcedure(string name)
		: this(name, null)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Data.DesignerDataStoredProcedure" /> class with the specified name and owner.</summary>
	/// <param name="name">The name of the stored procedure.</param>
	/// <param name="owner">The data store owner of the stored procedure.</param>
	[System.MonoTODO]
	protected DesignerDataStoredProcedure(string name, string owner)
	{
		this.name = name;
		this.owner = owner;
	}

	/// <summary>When overridden in a derived class, returns a collection of parameters for the stored procedure.</summary>
	/// <returns>A collection of <see cref="T:System.ComponentModel.Design.Data.DesignerDataParameter" /> objects.</returns>
	protected abstract ICollection CreateParameters();
}
