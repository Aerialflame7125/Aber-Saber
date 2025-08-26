namespace System.Web.UI;

/// <summary>Specifies a <see cref="T:System.Web.UI.ControlBuilder" /> class for building a custom control within the ASP.NET parser. This class cannot be inherited.</summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class ControlBuilderAttribute : Attribute
{
	/// <summary>Specifies the new <see cref="T:System.Web.UI.ControlBuilderAttribute" /> object. By default, the new object is set to null. This field is read-only.</summary>
	public static readonly ControlBuilderAttribute Default = new ControlBuilderAttribute(null);

	private Type builderType;

	/// <summary>Gets the <see cref="T:System.Type" /> of the control associated with the attribute. This property is read-only.</summary>
	/// <returns>The <see cref="T:System.Type" /> of the control associated with the attribute.</returns>
	public Type BuilderType => builderType;

	/// <summary>Specifies the control builder for a custom control.</summary>
	/// <param name="builderType">The control builder type </param>
	public ControlBuilderAttribute(Type builderType)
	{
		this.builderType = builderType;
	}

	/// <summary>Returns the hash code of the <see cref="T:System.Web.UI.ControlBuilderAttribute" /> object. </summary>
	/// <returns>A 32-bit signed integer representing the hash code; otherwise, 0, if the <see cref="P:System.Web.UI.ControlBuilderAttribute.BuilderType" /> is <see langword="null" />.</returns>
	public override int GetHashCode()
	{
		if (!(BuilderType != null))
		{
			return 0;
		}
		return BuilderType.GetHashCode();
	}

	/// <summary>Gets a value indicating whether the current <see cref="T:System.Web.UI.ControlBuilderAttribute" /> is identical to the specified object. </summary>
	/// <param name="obj">An object to compare to the current <see cref="T:System.Web.UI.ControlBuilderAttribute" />.</param>
	/// <returns>
	///     <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.Web.UI.ControlBuilderAttribute" /> and is identical to the current <see cref="T:System.Web.UI.ControlBuilderAttribute" />; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		if (obj == this)
		{
			return true;
		}
		if (obj != null && obj is ControlBuilderAttribute)
		{
			return ((ControlBuilderAttribute)obj).BuilderType == builderType;
		}
		return false;
	}

	/// <summary>Determines whether the current control builder is the default.</summary>
	/// <returns>
	///     <see langword="true" /> if the current control builder is the default.</returns>
	public override bool IsDefaultAttribute()
	{
		return Equals(Default);
	}
}
