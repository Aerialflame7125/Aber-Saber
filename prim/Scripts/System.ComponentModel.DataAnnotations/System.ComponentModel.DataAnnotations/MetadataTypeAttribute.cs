namespace System.ComponentModel.DataAnnotations;

/// <summary>Specifies the metadata class to associate with a data model class.</summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class MetadataTypeAttribute : Attribute
{
	private Type _metadataClassType;

	/// <summary>Gets the metadata class that is associated with a data-model partial class.</summary>
	/// <returns>The type value that represents the metadata class.</returns>
	public Type MetadataClassType
	{
		get
		{
			if (_metadataClassType == null)
			{
				throw new InvalidOperationException("MetadataClassType cannot be null.");
			}
			return _metadataClassType;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.MetadataTypeAttribute" /> class.</summary>
	/// <param name="metadataClassType">The metadata class to reference.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="metadataClassType" /> is <see langword="null" />.</exception>
	public MetadataTypeAttribute(Type metadataClassType)
	{
		_metadataClassType = metadataClassType;
	}
}
