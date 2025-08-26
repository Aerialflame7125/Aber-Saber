namespace System.Web.Management;

/// <summary>Specifies the ASP.Net features to install or remove using the methods provided by the <see cref="T:System.Web.Management.SqlServices" /> class.</summary>
[Flags]
public enum SqlFeatures
{
	/// <summary>No features.</summary>
	None = 0,
	/// <summary>The membership feature.</summary>
	Membership = 1,
	/// <summary>The profile feature.</summary>
	Profile = 2,
	/// <summary>The role manager feature.</summary>
	RoleManager = 4,
	/// <summary>The personalization feature.</summary>
	Personalization = 8,
	/// <summary>The Web event provider feature.</summary>
	SqlWebEventProvider = 0x10,
	/// <summary>All features.</summary>
	All = 0x4000001F
}
