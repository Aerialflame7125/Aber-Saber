namespace System.Web.UI;

/// <summary>Defines a metadata attribute that is used to specify the number of allowed instances of a template. This class cannot be inherited.</summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class TemplateInstanceAttribute : Attribute
{
	/// <summary>Creates an instance of the <see cref="T:System.Web.UI.TemplateInstanceAttribute" /> class as one representing a template that will be instantiated multiple times. This field is read-only.</summary>
	public static readonly TemplateInstanceAttribute Multiple = new TemplateInstanceAttribute(TemplateInstance.Multiple);

	/// <summary>Creates an instance of the <see cref="T:System.Web.UI.TemplateInstanceAttribute" /> class as one representing a template that will be instantiated a single time. This field is read-only.</summary>
	public static readonly TemplateInstanceAttribute Single = new TemplateInstanceAttribute(TemplateInstance.Single);

	/// <summary>Defines the default value for the <see cref="T:System.Web.UI.TemplateInstanceAttribute" /> class. This field is read-only. </summary>
	public static readonly TemplateInstanceAttribute Default = Multiple;

	private TemplateInstance _instances;

	/// <summary>Gets the <see cref="T:System.Web.UI.TemplateInstance" /> enumeration value that the current template instance represents.</summary>
	/// <returns>A <see cref="T:System.Web.UI.TemplateInstance" /> enumeration value that the current template instance represents.</returns>
	public TemplateInstance Instances => _instances;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.TemplateInstanceAttribute" /> class with the specified <see cref="T:System.Web.UI.TemplateInstance" /> enumeration value.</summary>
	/// <param name="instances">A <see cref="T:System.Web.UI.TemplateInstance" /> enumeration value.</param>
	public TemplateInstanceAttribute(TemplateInstance instances)
	{
		_instances = instances;
	}

	/// <summary>Indicates whether the specified object is a <see cref="T:System.Web.UI.TemplateInstanceAttribute" /> object and is identical to the this <see cref="T:System.Web.UI.TemplateInstanceAttribute" /> object.</summary>
	/// <param name="obj">The <see cref="T:System.Object" /> to test.</param>
	/// <returns>
	///     <see langword="true" /> if value is both a <see cref="T:System.Web.UI.TemplateInstanceAttribute" /> object and is identical to the this <see cref="T:System.Web.UI.TemplateInstanceAttribute" /> object; otherwise <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		if (obj == this)
		{
			return true;
		}
		if (obj is TemplateInstanceAttribute templateInstanceAttribute)
		{
			return templateInstanceAttribute.Instances == Instances;
		}
		return false;
	}

	/// <summary>Gets a hash code for this <see cref="T:System.Web.UI.TemplateInstanceAttribute" /> object.</summary>
	/// <returns>The hash code for this <see cref="T:System.Web.UI.TemplateInstanceAttribute" /> object.</returns>
	public override int GetHashCode()
	{
		return _instances.GetHashCode();
	}

	/// <summary>Returns a value indicating if the current <see cref="T:System.Web.UI.TemplateInstanceAttribute" /> object is the same as the default <see cref="T:System.Web.UI.TemplateInstanceAttribute" /> object.</summary>
	/// <returns>
	///     <see langword="true" /> if the value of the current instance of <see cref="T:System.Web.UI.TemplateInstanceAttribute" /> is the default; otherwise, <see langword="false" />.</returns>
	public override bool IsDefaultAttribute()
	{
		return Equals(Default);
	}
}
