namespace System.Runtime.InteropServices.ComTypes;

/// <summary>Defines how to access a function.</summary>
[Serializable]
public enum FUNCKIND
{
	/// <summary>The function is accessed in the same way as <see cref="F:System.Runtime.InteropServices.FUNCKIND.FUNC_PUREVIRTUAL" />, except the function has an implementation.</summary>
	FUNC_VIRTUAL,
	/// <summary>The function is accessed through the virtual function table (VTBL), and takes an implicit <see langword="this" /> pointer.</summary>
	FUNC_PUREVIRTUAL,
	/// <summary>The function is accessed by <see langword="static" /> address and takes an implicit <see langword="this" /> pointer.</summary>
	FUNC_NONVIRTUAL,
	/// <summary>The function is accessed by <see langword="static" /> address and does not take an implicit <see langword="this" /> pointer.</summary>
	FUNC_STATIC,
	/// <summary>The function can be accessed only through <see langword="IDispatch" />.</summary>
	FUNC_DISPATCH
}
