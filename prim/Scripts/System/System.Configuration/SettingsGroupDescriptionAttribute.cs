namespace System.Configuration;

/// <summary>Provides a string that describes an application settings property group. This class cannot be inherited.</summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class SettingsGroupDescriptionAttribute : Attribute
{
	private string desc;

	/// <summary>The descriptive text for the application settings properties group.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the descriptive text for the application settings group.</returns>
	public string Description => desc;

	/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsGroupDescriptionAttribute" /> class.</summary>
	/// <param name="description">A <see cref="T:System.String" /> containing the descriptive text for the application settings group.</param>
	public SettingsGroupDescriptionAttribute(string description)
	{
		desc = description;
	}
}
