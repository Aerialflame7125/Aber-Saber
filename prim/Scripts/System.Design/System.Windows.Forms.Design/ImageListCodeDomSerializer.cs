using System.ComponentModel.Design.Serialization;

namespace System.Windows.Forms.Design;

/// <summary>Serializes string dictionaries.</summary>
public class ImageListCodeDomSerializer : CodeDomSerializer
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.ImageListCodeDomSerializer" /> class.</summary>
	public ImageListCodeDomSerializer()
	{
	}

	/// <summary>Deserializes the specified serialized Code Document Object Model (CodeDOM) object into an object.</summary>
	/// <param name="manager">A serialization manager interface that is used during the deserialization process.</param>
	/// <param name="codeObject">A serialized CodeDOM object to deserialize.</param>
	/// <returns>The deserialized CodeDOM object.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="manager" /> or <paramref name="codeObject" /> is <see langword="null" />.</exception>
	[System.MonoTODO]
	public override object Deserialize(IDesignerSerializationManager manager, object codeObject)
	{
		throw new NotImplementedException();
	}

	/// <summary>Serializes the specified object into a Code Document Object Model (CodeDOM) object.</summary>
	/// <param name="manager">The serialization manager to use during serialization.</param>
	/// <param name="value">The object to serialize.</param>
	/// <returns>A CodeDOM object representing the object that has been serialized.</returns>
	[System.MonoTODO]
	public override object Serialize(IDesignerSerializationManager manager, object value)
	{
		throw new NotImplementedException();
	}
}
