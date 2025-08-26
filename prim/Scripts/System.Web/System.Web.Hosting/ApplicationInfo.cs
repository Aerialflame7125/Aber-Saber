namespace System.Web.Hosting;

/// <summary>Provides information about a running application. This class cannot be inherited.</summary>
[Serializable]
public sealed class ApplicationInfo
{
	private string id;

	private string physical_path;

	private string virtual_path;

	/// <summary>Gets the unique identifier for the application.</summary>
	/// <returns>The unique identifier for the application specified when the application was created by using the <see cref="M:System.Web.Hosting.ApplicationManager.CreateObject(System.String,System.Type,System.String,System.String,System.Boolean)" /> method.</returns>
	public string ID => id;

	/// <summary>Gets the physical path corresponding to the application's root.</summary>
	/// <returns>The physical path corresponding to the application's root.</returns>
	public string PhysicalPath => physical_path;

	/// <summary>Gets the virtual path corresponding to the application's root.</summary>
	/// <returns>The virtual path corresponding to the application's root.</returns>
	public string VirtualPath => virtual_path;

	internal ApplicationInfo(string id, string phys, string virt)
	{
		this.id = id;
		physical_path = phys;
		virtual_path = virt;
	}
}
