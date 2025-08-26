using System.Runtime.InteropServices;

namespace System.Web.Hosting;

/// <summary>Provides functionality to respond to a ping request.</summary>
[ComImport]
[Guid("f11dc4c9-ddd1-4566-ad53-cf6f3a28fefe")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IProcessPingCallback
{
	/// <summary>Provides a callback routine that responds to a ping request.</summary>
	void Respond();
}
