namespace System.Web.UI;

/// <summary>Defines the attribute that indicates whether a control is treated as a visual or non-visual control during design time. This class cannot be inherited.</summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class NonVisualControlAttribute : Attribute
{
	/// <summary>Returns a <see cref="T:System.Web.UI.NonVisualControlAttribute" /> instance that is applied to a Web control to be treated as a non-visual control during design time. This field is read-only.</summary>
	public static readonly NonVisualControlAttribute NonVisual = new NonVisualControlAttribute(nonVisual: true);

	/// <summary>Gets a <see cref="T:System.Web.UI.NonVisualControlAttribute" /> instance that is applied to a Web control to be treated as a visual control during design time. </summary>
	public static readonly NonVisualControlAttribute Visual = new NonVisualControlAttribute(nonVisual: false);

	/// <summary>Returns a <see cref="T:System.Web.UI.NonVisualControlAttribute" /> instance that represents the application-defined default value of the attribute. This field is read-only.</summary>
	public static readonly NonVisualControlAttribute Default = Visual;

	private bool _nonVisual;

	/// <summary>Gets a value indicating whether the control is non-visual.</summary>
	/// <returns>
	///     <see langword="true" /> if the control has been marked as non-visual; otherwise, <see langword="false" />. </returns>
	public bool IsNonVisual => _nonVisual;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.NonVisualControlAttribute" /> class.</summary>
	public NonVisualControlAttribute()
		: this(nonVisual: true)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.NonVisualControlAttribute" /> class, using the specified Boolean value to determine whether the attribute represents a visual or non-visual control. </summary>
	/// <param name="nonVisual">
	///       <see langword="true" /> to initialize the <see cref="T:System.Web.UI.NonVisualControlAttribute" /> to represent a Web control that is not rendered to the client at run time; otherwise, <see langword="false" />.</param>
	public NonVisualControlAttribute(bool nonVisual)
	{
		_nonVisual = nonVisual;
	}

	/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
	/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance or a null reference (<see langword="Nothing" /> in Visual Basic).</param>
	/// <returns>
	///     <see langword="true" /> if <paramref name="obj" /> equals the type and value of this instance; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		if (obj == this)
		{
			return true;
		}
		if (obj is NonVisualControlAttribute nonVisualControlAttribute)
		{
			return nonVisualControlAttribute.IsNonVisual == IsNonVisual;
		}
		return false;
	}

	/// <summary>Returns the hash code for this instance.</summary>
	/// <returns>A 32-bit signed integer hash code.</returns>
	public override int GetHashCode()
	{
		return _nonVisual.GetHashCode();
	}

	/// <summary>Returns a value indicating whether the current instance is equivalent to a default instance of the <see cref="T:System.Web.UI.NonVisualControlAttribute" /> class.</summary>
	/// <returns>
	///     <see langword="true" /> if the current instance is equivalent to a <see cref="F:System.Web.UI.NonVisualControlAttribute.Default" /> instance of the class; otherwise, <see langword="false" />.</returns>
	public override bool IsDefaultAttribute()
	{
		return Equals(Default);
	}
}
