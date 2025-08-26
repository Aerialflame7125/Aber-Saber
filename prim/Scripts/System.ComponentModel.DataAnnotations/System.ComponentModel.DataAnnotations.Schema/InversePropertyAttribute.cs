using System.Globalization;

namespace System.ComponentModel.DataAnnotations.Schema;

/// <summary>Specifies the inverse of a navigation property that represents the other end of the same relationship.</summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class InversePropertyAttribute : Attribute
{
	private readonly string _property;

	/// <summary>Gets the navigation property representing the other end of the same relationship.</summary>
	/// <returns>The property of the attribute.</returns>
	public string Property => _property;

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.Schema.InversePropertyAttribute" /> class using the specified property.</summary>
	/// <param name="property">The navigation property representing the other end of the same relationship.</param>
	public InversePropertyAttribute(string property)
	{
		if (string.IsNullOrWhiteSpace(property))
		{
			throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "The argument '{0}' cannot be null, empty or contain only white space.", "property"));
		}
		_property = property;
	}
}
