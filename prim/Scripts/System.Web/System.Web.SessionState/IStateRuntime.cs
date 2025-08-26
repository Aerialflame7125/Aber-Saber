using System.Runtime.InteropServices;

namespace System.Web.SessionState;

/// <summary>Defines the interface used by the ASP.NET state service to manage session data.</summary>
[ComImport]
[Guid("7297744b-e188-40bf-b7e9-56698d25cf44")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IStateRuntime
{
	/// <summary>Used by the ASP.NET state server to process session data.</summary>
	/// <param name="tracker">An <see cref="T:System.IntPtr" /> pointer to an object stored in the unmanaged ASP.NET state server.</param>
	/// <param name="verb">The action to take on the object.</param>
	/// <param name="uri">An identifier for the session.</param>
	/// <param name="exclusive">The type of access to objects in the store.</param>
	/// <param name="timeout">The number of minutes the session data is stored.</param>
	/// <param name="lockCookieExists">A value that indicates whether the lock cookie exists in the original request from the ASP.NET Web server to the ASP.NET state server.</param>
	/// <param name="lockCookie">The owner of the lock on the session state.</param>
	/// <param name="contentLength">The length, in bytes, of the data stored for the session.</param>
	/// <param name="content">An <see cref="T:System.IntPtr" /> pointer to the content stored for the session in the unmanaged ASP.NET state server.</param>
	void ProcessRequest([In][MarshalAs(UnmanagedType.SysInt)] IntPtr tracker, [In][MarshalAs(UnmanagedType.I4)] int verb, [In][MarshalAs(UnmanagedType.LPWStr)] string uri, [In][MarshalAs(UnmanagedType.I4)] int exclusive, [In][MarshalAs(UnmanagedType.I4)] int timeout, [In][MarshalAs(UnmanagedType.I4)] int lockCookieExists, [In][MarshalAs(UnmanagedType.I4)] int lockCookie, [In][MarshalAs(UnmanagedType.I4)] int contentLength, [In][MarshalAs(UnmanagedType.SysInt)] IntPtr content);

	/// <summary>Used by the ASP.NET state server to process session data.</summary>
	/// <param name="tracker">An <see cref="T:System.IntPtr" /> pointer to an object stored in the unmanaged ASP.NET state server.</param>
	/// <param name="verb">The action to take on the object.</param>
	/// <param name="uri">An identifier for the session.</param>
	/// <param name="exclusive">The type of access to objects in the store.</param>
	/// <param name="extraFlags">A value that indicates whether the current session is an uninitialized, cookieless session.</param>
	/// <param name="timeout">The number of minutes the session data is stored.</param>
	/// <param name="lockCookieExists">A value that indicates whether the lock cookie exists in the original request from the ASP.NET Web server to the ASP.NET state server.</param>
	/// <param name="lockCookie">The owner of the lock on the session state.</param>
	/// <param name="contentLength">The length, in bytes, of the data stored for the session.</param>
	/// <param name="content">An <see cref="T:System.IntPtr" /> pointer to the content stored for the session in the unmanaged ASP.NET state server.</param>
	void ProcessRequest([In][MarshalAs(UnmanagedType.SysInt)] IntPtr tracker, [In][MarshalAs(UnmanagedType.I4)] int verb, [In][MarshalAs(UnmanagedType.LPWStr)] string uri, [In][MarshalAs(UnmanagedType.I4)] int exclusive, [In][MarshalAs(UnmanagedType.I4)] int extraFlags, [In][MarshalAs(UnmanagedType.I4)] int timeout, [In][MarshalAs(UnmanagedType.I4)] int lockCookieExists, [In][MarshalAs(UnmanagedType.I4)] int lockCookie, [In][MarshalAs(UnmanagedType.I4)] int contentLength, [In][MarshalAs(UnmanagedType.SysInt)] IntPtr content);

	/// <summary>Stops the processing of session data stored in ASP.NET state server.</summary>
	void StopProcessing();
}
