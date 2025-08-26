using System.Runtime.InteropServices;

namespace System.Web.Hosting;

/// <summary>Defines a method used to create an <see cref="T:System.AppDomain" /> instance for a Web-application manager and a method used to stop all <see cref="T:System.AppDomain" /> instances for a Web-application manager.</summary>
[ComImport]
[Guid("02998279-7175-4D59-AA5A-FB8E44D4CA9D")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IAppManagerAppDomainFactory
{
	/// <summary>Creates a new application domain for the specified Web application.</summary>
	/// <param name="appId">The unique identifier for the new application.</param>
	/// <param name="appPath">The path to the new application's files.</param>
	/// <returns>A new application domain for the specified Web application.</returns>
	[return: MarshalAs(UnmanagedType.Interface)]
	object Create([In][MarshalAs(UnmanagedType.BStr)] string appId, [In][MarshalAs(UnmanagedType.BStr)] string appPath);

	/// <summary>Stops all application domains associated with this application manager. </summary>
	void Stop();
}
