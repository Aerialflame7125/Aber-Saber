namespace System.ComponentModel.Design.Serialization;

/// <summary>Specifies that serializers should handle default values. This class cannot be inherited.</summary>
public sealed class SerializeAbsoluteContext
{
	private MemberDescriptor _member;

	/// <summary>Gets the member to which this context is bound.</summary>
	/// <returns>The member to which this context is bound, or <see langword="null" /> if the context is bound to all members of an object.</returns>
	public MemberDescriptor Member => _member;

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.SerializeAbsoluteContext" /> class.</summary>
	public SerializeAbsoluteContext()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.SerializeAbsoluteContext" /> class with the option of binding to a specific member.</summary>
	/// <param name="member">The member to which this context is bound. Can be <see langword="null" />.</param>
	public SerializeAbsoluteContext(MemberDescriptor member)
	{
		_member = member;
	}

	/// <summary>Gets a value indicating whether the given member should be serialized in this context.</summary>
	/// <param name="member">The member to be examined for serialization.</param>
	/// <returns>
	///   <see langword="true" /> if the given member should be serialized in this context; otherwise, <see langword="false" />.</returns>
	public bool ShouldSerialize(MemberDescriptor member)
	{
		return member == _member;
	}
}
