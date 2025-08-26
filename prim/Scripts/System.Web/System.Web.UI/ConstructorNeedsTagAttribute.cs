namespace System.Web.UI;

/// <summary>Specifies that a server control needs a tag name in its constructor.</summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class ConstructorNeedsTagAttribute : Attribute
{
	private bool needsTag;

	/// <summary>Indicates whether a control needs a tag name in its constructor. This property is read-only.</summary>
	/// <returns>
	///     <see langword="true" /> if the control needs a tag in its constructor. The default is <see langword="false" />.</returns>
	public bool NeedsTag => needsTag;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ConstructorNeedsTagAttribute" /> class.</summary>
	public ConstructorNeedsTagAttribute()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ConstructorNeedsTagAttribute" /> class.</summary>
	/// <param name="needsTag">
	///       <see langword="true" /> to add a tag to a control; otherwise, <see langword="false" />. </param>
	public ConstructorNeedsTagAttribute(bool needsTag)
	{
		this.needsTag = needsTag;
	}
}
