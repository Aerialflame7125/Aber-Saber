using System.ComponentModel;

namespace System.DirectoryServices;

/// <summary>Specifies how to sort the results of a search.</summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class SortOption
{
	private string propertyName;

	private SortDirection direction;

	/// <summary>Gets or sets the name of the property to sort on.</summary>
	/// <returns>The name of the property to sort on. The default is a null reference (<see langword="Nothing" /> in Visual Basic).</returns>
	/// <exception cref="T:System.ArgumentNullException">The property value is a null reference (<see langword="Nothing" /> in Visual Basic).</exception>
	[DSDescription("Name of propertion to be sorted on")]
	[DefaultValue(null)]
	public string PropertyName
	{
		get
		{
			return propertyName;
		}
		set
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			propertyName = value;
		}
	}

	/// <summary>Gets or sets the direction in which to sort the results of a query.</summary>
	/// <returns>One of the <see cref="T:System.DirectoryServices.SortDirection" /> values. The default is Ascending.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value is not one of the <see cref="T:System.DirectoryServices.SortDirection" /> values.</exception>
	[DSDescription("Whether the sort is ascending or descending")]
	[DefaultValue(SortDirection.Ascending)]
	public SortDirection Direction
	{
		get
		{
			return direction;
		}
		set
		{
			direction = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="M:System.DirectoryServices.SortOption.#ctor" /> class.</summary>
	public SortOption()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.SortOption" /> class, which contains the specified property name and specified sort direction.</summary>
	/// <param name="propertyName">The name of the property to sort by. The <see cref="P:System.DirectoryServices.SortOption.PropertyName" /> property is set to this value.</param>
	/// <param name="direction">One of the <see cref="T:System.DirectoryServices.SortDirection" /> values. The <see cref="P:System.DirectoryServices.SortOption.Direction" /> property is set to this value.</param>
	public SortOption(string propertyName, SortDirection direction)
	{
		this.propertyName = propertyName;
		this.direction = direction;
	}
}
