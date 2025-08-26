namespace System.ComponentModel.DataAnnotations.Schema;

/// <summary>Specifies how the database generates values for a property.</summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class DatabaseGeneratedAttribute : Attribute
{
	/// <summary>Gets or sets the pattern used to generate values for the property in the database.</summary>
	/// <returns>The database generated option.</returns>
	public DatabaseGeneratedOption DatabaseGeneratedOption { get; private set; }

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedAttribute" /> class.</summary>
	/// <param name="databaseGeneratedOption">The database generated option.</param>
	public DatabaseGeneratedAttribute(DatabaseGeneratedOption databaseGeneratedOption)
	{
		if (!Enum.IsDefined(typeof(DatabaseGeneratedOption), databaseGeneratedOption))
		{
			throw new ArgumentOutOfRangeException("databaseGeneratedOption");
		}
		DatabaseGeneratedOption = databaseGeneratedOption;
	}
}
