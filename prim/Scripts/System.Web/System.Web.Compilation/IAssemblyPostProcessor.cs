using System.Security.Permissions;

namespace System.Web.Compilation;

/// <summary>Defines the method a class implements to process an assembly after the assembly has been built.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.High)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.High)]
public interface IAssemblyPostProcessor : IDisposable
{
	/// <summary>Called before the assembly is loaded to allow the implementing class to modify the assembly.</summary>
	/// <param name="path">The path to the assembly.</param>
	void PostProcessAssembly(string path);
}
