using System.Runtime.InteropServices;

namespace System.EnterpriseServices.Internal;

/// <summary>Creates and deletes Internet Information Services (IIS) 6.0 virtual roots.</summary>
[Guid("d8013ef1-730b-45e2-ba24-874b7242c425")]
public class IISVirtualRoot : IComSoapIISVRoot
{
	/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.Internal.IISVirtualRoot" /> class.</summary>
	[System.MonoTODO]
	public IISVirtualRoot()
	{
		throw new NotImplementedException();
	}

	/// <summary>Creates an Internet Information Services (IIS) virtual root.</summary>
	/// <param name="RootWeb">A string with the value <c>"IIS://localhost/W3SVC/1/ROOT"</c> representing the root Web server.</param>
	/// <param name="inPhysicalDirectory">The physical path of the virtual root, which corresponds to <paramref name="PhysicalPath" /> from the <see cref="M:System.EnterpriseServices.Internal.Publish.CreateVirtualRoot(System.String,System.String,System.String@,System.String@,System.String@,System.String@)" /> method.</param>
	/// <param name="VirtualDirectory">The name of the virtual root, which corresponds to <paramref name="VirtualRoot" /> from <see cref="M:System.EnterpriseServices.Internal.Publish.CreateVirtualRoot(System.String,System.String,System.String@,System.String@,System.String@,System.String@)" />.</param>
	/// <param name="Error">A string to which an error message can be written.</param>
	[System.MonoTODO]
	public void Create(string RootWeb, string inPhysicalDirectory, string VirtualDirectory, out string Error)
	{
		throw new NotImplementedException();
	}

	/// <summary>Deletes an Internet Information Services (IIS) virtual root.</summary>
	/// <param name="RootWeb">The root Web server, as specified by <paramref name="RootWebServer" /> from the <see cref="M:System.EnterpriseServices.Internal.IComSoapPublisher.DeleteVirtualRoot(System.String,System.String,System.String@)" /> method.</param>
	/// <param name="PhysicalDirectory">The physical path of the virtual root.</param>
	/// <param name="VirtualDirectory">The name of the virtual root.</param>
	/// <param name="Error">A string to which an error message can be written.</param>
	[System.MonoTODO]
	public void Delete(string RootWeb, string PhysicalDirectory, string VirtualDirectory, out string Error)
	{
		throw new NotImplementedException();
	}
}
