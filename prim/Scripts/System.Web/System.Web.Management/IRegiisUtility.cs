using System.Runtime.InteropServices;

namespace System.Web.Management;

/// <summary>Provides authorization utilities to support specific Web-application configuration, assembly registration, and assembly-key container manipulation.</summary>
[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[Guid("C84F668A-CC3F-11D7-B79E-505054503030")]
public interface IRegiisUtility
{
	/// <summary>Allows specific manipulation of configuration sections and assembly-key containers based on the supplied parameters.</summary>
	/// <param name="actionToPerform">The type of action to perform.</param>
	/// <param name="firstArgument">A configuration section or assembly-key container name.</param>
	/// <param name="secondArgument">The configuration file name or account name.</param>
	/// <param name="providerName">The provider name.</param>
	/// <param name="appPath">The application path.</param>
	/// <param name="site">The site reference.</param>
	/// <param name="cspOrLocation">The configuration location.</param>
	/// <param name="keySize">The size of the key.</param>
	/// <param name="exception">The exception to display.</param>
	void ProtectedConfigAction(long actionToPerform, [In][MarshalAs(UnmanagedType.LPWStr)] string firstArgument, [In][MarshalAs(UnmanagedType.LPWStr)] string secondArgument, [In][MarshalAs(UnmanagedType.LPWStr)] string providerName, [In][MarshalAs(UnmanagedType.LPWStr)] string appPath, [In][MarshalAs(UnmanagedType.LPWStr)] string site, [In][MarshalAs(UnmanagedType.LPWStr)] string cspOrLocation, int keySize, out IntPtr exception);

	/// <summary>Allows the executing Microsoft Management Console (MMC) assembly to be registered or unregistered.</summary>
	/// <param name="doReg">A value of 0 indicates that the assembly should be unregistered. A value other than 0 indicates that the assembly should be registered.</param>
	/// <param name="assemblyName">The type of the assembly.</param>
	/// <param name="binaryDirectory">The path of the binary directory.</param>
	/// <param name="exception">The exception to display.</param>
	void RegisterAsnetMmcAssembly(int doReg, [In][MarshalAs(UnmanagedType.LPWStr)] string assemblyName, [In][MarshalAs(UnmanagedType.LPWStr)] string binaryDirectory, out IntPtr exception);

	/// <summary>Allows the executing Web assembly to be registered or unregistered.</summary>
	/// <param name="doReg">A value of 0 indicates that the assembly should be unregistered. A value other than 0 indicates that the assembly should be registered.</param>
	/// <param name="exception">An <see cref="T:System.IntPtr" /> that points to the exception thrown by the method.  If no exception is thrown, the value is <see cref="F:System.IntPtr.Zero" />.</param>
	void RegisterSystemWebAssembly(int doReg, out IntPtr exception);

	/// <summary>Allows the browser-capabilities code generator to be uninstalled.</summary>
	/// <param name="exception">An <see cref="T:System.IntPtr" /> that points to the exception thrown by the method.  If no exception is thrown, the value is <see cref="F:System.IntPtr.Zero" />.</param>
	/// <exception cref="T:System.Exception">The attempt to uninstall the browser-capabilities code generator fails.</exception>
	void RemoveBrowserCaps(out IntPtr exception);
}
