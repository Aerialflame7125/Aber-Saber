using System.Security.Permissions;

namespace System.Web.Security;

/// <summary>Verifies that the user has permission to access the file requested. This class cannot be inherited.</summary>
[MonoTODO("that's only a stub")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class FileAuthorizationModule : IHttpModule
{
	/// <summary>Creates an instance of the <see cref="T:System.Web.Security.FileAuthorizationModule" /> class.</summary>
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public FileAuthorizationModule()
	{
	}

	/// <summary>Releases all resources, other than memory, used by the <see cref="T:System.Web.Security.FileAuthorizationModule" />.</summary>
	public void Dispose()
	{
	}

	/// <summary>Initializes the <see cref="T:System.Web.Security.FileAuthorizationModule" /> object.</summary>
	/// <param name="app">The current <see cref="T:System.Web.HttpApplication" /> instance. </param>
	[MonoTODO("Not implemented")]
	public void Init(HttpApplication app)
	{
		throw new NotImplementedException();
	}

	/// <summary>Determines whether the user has access to the requested file.</summary>
	/// <param name="virtualPath">The virtual path to the file.</param>
	/// <param name="token">A Windows access token representing the user.</param>
	/// <param name="verb">The HTTP verb used to make the request.</param>
	/// <returns>
	///     <see langword="true" /> if the current Windows user represented by <paramref name="token" /> has access to the file using the specified HTTP verb or if the <see cref="T:System.Web.Security.FileAuthorizationModule" /> module is not defined in the application's configuration file; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="virtualPath" /> is <see langword="null" />.-or-
	///         <paramref name="token" /> is <see cref="F:System.IntPtr.Zero" />.-or-
	///         <paramref name="verb" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="virtualPath" /> is not in the application directory structure of the Web application.</exception>
	/// <exception cref="T:System.IO.FileNotFoundException">The file specified by <paramref name="virtualPath" /> does not exist.</exception>
	[MonoTODO("Not implemented")]
	public static bool CheckFileAccessForUser(string virtualPath, IntPtr token, string verb)
	{
		throw new NotImplementedException();
	}
}
