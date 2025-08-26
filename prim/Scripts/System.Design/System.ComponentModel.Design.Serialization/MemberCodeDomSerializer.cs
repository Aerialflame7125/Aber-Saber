using System.CodeDom;

namespace System.ComponentModel.Design.Serialization;

/// <summary>Provides the base class for serializing a reflection primitive within the object graph.</summary>
public abstract class MemberCodeDomSerializer : CodeDomSerializerBase
{
	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.MemberCodeDomSerializer" /> class.</summary>
	protected MemberCodeDomSerializer()
	{
	}

	/// <summary>Serializes the given member descriptor on the given value to a statement collection.</summary>
	/// <param name="manager">The serialization manager to use for serialization.</param>
	/// <param name="value">The object to which the member is bound.</param>
	/// <param name="descriptor">The descriptor of the member to serialize.</param>
	/// <param name="statements">The <see cref="T:System.CodeDom.CodeStatementCollection" /> into which <paramref name="descriptor" /> is serialized.</param>
	public abstract void Serialize(IDesignerSerializationManager manager, object value, MemberDescriptor descriptor, CodeStatementCollection statements);

	/// <summary>Determines if the given member should be serialized.</summary>
	/// <param name="manager">The serialization manager to use for serialization.</param>
	/// <param name="value">The object to which the member is bound.</param>
	/// <param name="descriptor">The descriptor of the member to serialize.</param>
	/// <returns>
	///   <see langword="true" />, if the member described by <paramref name="descriptor" /> should be serialized; otherwise, <see langword="false" />.</returns>
	public abstract bool ShouldSerialize(IDesignerSerializationManager manager, object value, MemberDescriptor descriptor);
}
