namespace System.Configuration;

/// <summary>Specifies a name for application settings property group. This class cannot be inherited.</summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class SettingsGroupNameAttribute : Attribute
{
	private string group_name;

	/// <summary>Gets the name of the application settings property group.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the name of the application settings property group.</returns>
	public string GroupName => group_name;

	/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsGroupNameAttribute" /> class.</summary>
	/// <param name="groupName">A <see cref="T:System.String" /> containing the name of the application settings property group.</param>
	public SettingsGroupNameAttribute(string groupName)
	{
		group_name = groupName;
	}
}
