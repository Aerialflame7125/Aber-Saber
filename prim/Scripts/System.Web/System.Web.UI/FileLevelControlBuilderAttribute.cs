namespace System.Web.UI;

/// <summary>Allows a <see cref="T:System.Web.UI.TemplateControl" />-derived class to specify the control builder used at the top level of the builder tree when parsing the file. This class cannot be inherited.</summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class FileLevelControlBuilderAttribute : Attribute
{
	/// <summary>Specifies the new <see cref="T:System.Web.UI.FileLevelControlBuilderAttribute" /> object. By default, the new object is set to <see langword="null" />. This field is read-only.</summary>
	public static readonly FileLevelControlBuilderAttribute Default = new FileLevelControlBuilderAttribute(null);

	private Type builderType;

	/// <summary>Gets the <see cref="T:System.Type" /> of the control builder used when parsing the file. This property is read-only. </summary>
	/// <returns>The <see cref="T:System.Type" /> of the control builder used when parsing the file.</returns>
	public Type BuilderType => builderType;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.FileLevelControlBuilderAttribute" /> class.</summary>
	/// <param name="builderType">The <see cref="T:System.Type" /> of the control builder used when parsing the file.</param>
	public FileLevelControlBuilderAttribute(Type builderType)
	{
		this.builderType = builderType;
	}

	/// <summary>Returns the hash code for this instance.</summary>
	/// <returns>A 32-bit signed integer hash code.</returns>
	public override int GetHashCode()
	{
		return builderType.GetHashCode();
	}

	/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
	/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance. </param>
	/// <returns>
	///     <see langword="true" /> if <paramref name="obj" /> equals the type and value of this instance; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		if (obj == this)
		{
			return true;
		}
		if (obj != null && obj is FileLevelControlBuilderAttribute)
		{
			return ((FileLevelControlBuilderAttribute)obj).BuilderType == builderType;
		}
		return false;
	}

	/// <summary>Determines whether the current <see cref="T:System.Web.UI.FileLevelControlBuilderAttribute" /> object is the default.</summary>
	/// <returns>
	///     <see langword="true" /> if the current <see cref="T:System.Web.UI.FileLevelControlBuilderAttribute" /> is the default; otherwise, <see langword="false" />.</returns>
	public override bool IsDefaultAttribute()
	{
		return Equals(Default);
	}
}
