using System.Runtime.InteropServices;

namespace System;

/// <summary>Provides a mechanism for releasing unmanaged resources.</summary>
[ComVisible(true)]
public interface IDisposable
{
	/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
	void Dispose();
}
