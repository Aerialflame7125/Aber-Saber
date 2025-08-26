using System.Runtime.InteropServices;

namespace System;

/// <summary>Specifies a return value type for a method that does not return a value.</summary>
[Serializable]
[StructLayout(LayoutKind.Sequential, Size = 1)]
[ComVisible(true)]
public struct Void
{
}
