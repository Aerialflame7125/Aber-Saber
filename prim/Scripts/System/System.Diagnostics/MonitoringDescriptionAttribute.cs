using System.ComponentModel;

namespace System.Diagnostics;

/// <summary>Specifies a description for a property or event.</summary>
[AttributeUsage(AttributeTargets.All)]
public class MonitoringDescriptionAttribute : DescriptionAttribute
{
	/// <summary>Gets description text associated with the item monitored.</summary>
	/// <returns>An application-defined description.</returns>
	public override string Description => base.Description;

	/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.MonitoringDescriptionAttribute" /> class, using the specified description.</summary>
	/// <param name="description">The application-defined description text.</param>
	public MonitoringDescriptionAttribute(string description)
		: base(description)
	{
	}
}
