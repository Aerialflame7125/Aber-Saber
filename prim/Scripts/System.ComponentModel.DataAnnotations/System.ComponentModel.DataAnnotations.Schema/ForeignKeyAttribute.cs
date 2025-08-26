using System.Globalization;

namespace System.ComponentModel.DataAnnotations.Schema;

/// <summary>Denotes a property used as a foreign key in a relationship.</summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class ForeignKeyAttribute : Attribute
{
	private readonly string _name;

	/// <summary>Gets the name of the associated navigation property or of the associated foreign keys.</summary>
	/// <returns>The name of the associated navigation property or of the associated foreign keys.</returns>
	public string Name => _name;

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.Schema.ForeignKeyAttribute" /> class.</summary>
	/// <param name="name">The name of the associated navigation property, or the name of one or more associated foreign keys.</param>
	public ForeignKeyAttribute(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "The argument '{0}' cannot be null, empty or contain only white space.", "name"));
		}
		_name = name;
	}
}
