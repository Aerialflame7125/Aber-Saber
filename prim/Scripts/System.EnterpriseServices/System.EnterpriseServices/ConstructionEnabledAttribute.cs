using System.Runtime.InteropServices;

namespace System.EnterpriseServices;

/// <summary>Enables COM+ object construction support. This class cannot be inherited.</summary>
[AttributeUsage(AttributeTargets.Class)]
[ComVisible(false)]
public sealed class ConstructionEnabledAttribute : Attribute
{
	private string def;

	private bool enabled;

	/// <summary>Gets or sets a default value for the constructor string.</summary>
	/// <returns>The value to be used for the default constructor string. The default is an empty string ("").</returns>
	public string Default
	{
		get
		{
			return def;
		}
		set
		{
			def = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether COM+ object construction support is enabled.</summary>
	/// <returns>
	///   <see langword="true" /> if COM+ object construction support is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public bool Enabled
	{
		get
		{
			return enabled;
		}
		set
		{
			enabled = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ConstructionEnabledAttribute" /> class and initializes the default settings for <see cref="P:System.EnterpriseServices.ConstructionEnabledAttribute.Enabled" /> and <see cref="P:System.EnterpriseServices.ConstructionEnabledAttribute.Default" />.</summary>
	public ConstructionEnabledAttribute()
	{
		def = string.Empty;
		enabled = true;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ConstructionEnabledAttribute" /> class, setting <see cref="P:System.EnterpriseServices.ConstructionEnabledAttribute.Enabled" /> to the specified value.</summary>
	/// <param name="val">
	///   <see langword="true" /> to enable COM+ object construction support; otherwise, <see langword="false" />.</param>
	public ConstructionEnabledAttribute(bool val)
	{
		def = string.Empty;
		enabled = val;
	}
}
