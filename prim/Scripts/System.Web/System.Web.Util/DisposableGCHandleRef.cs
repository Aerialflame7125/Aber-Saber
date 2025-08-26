using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Web.Util;

internal class DisposableGCHandleRef<T> : IDisposable where T : class, IDisposable
{
	private GCHandle _handle;

	public T Target
	{
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		get
		{
			return (T)_handle.Target;
		}
	}

	[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
	public DisposableGCHandleRef(T t)
	{
		_handle = GCHandle.Alloc(t);
	}

	[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
	public void Dispose()
	{
		Target.Dispose();
		if (_handle.IsAllocated)
		{
			_handle.Free();
		}
	}
}
