namespace System.Runtime.InteropServices;

/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.IEnumConnectionPoints" /> instead.</summary>
[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[Guid("b196b285-bab4-101a-b69c-00aa00341d07")]
[Obsolete]
public interface UCOMIEnumConnectionPoints
{
	/// <summary>Retrieves a specified number of items in the enumeration sequence.</summary>
	/// <param name="celt">The number of <see langword="IConnectionPoint" /> references to return in <paramref name="rgelt" />.</param>
	/// <param name="rgelt">On successful return, a reference to the enumerated connections.</param>
	/// <param name="pceltFetched">On successful return, a reference to the actual number of connections enumerated in <paramref name="rgelt" />.</param>
	/// <returns>
	///   <see langword="S_OK" /> if the <paramref name="pceltFetched" /> parameter equals the <paramref name="celt" /> parameter; otherwise, <see langword="S_FALSE" />.</returns>
	[PreserveSig]
	int Next(int celt, [Out][MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] UCOMIConnectionPoint[] rgelt, out int pceltFetched);

	/// <summary>Skips over a specified number of items in the enumeration sequence.</summary>
	/// <param name="celt">The number of elements to skip in the enumeration.</param>
	/// <returns>
	///   <see langword="S_OK" /> if the number of elements skipped equals the <paramref name="celt" /> parameter; otherwise, <see langword="S_FALSE" />.</returns>
	[PreserveSig]
	int Skip(int celt);

	/// <summary>Resets the enumeration sequence to the beginning.</summary>
	/// <returns>An HRESULT with the value <see langword="S_OK" />.</returns>
	[PreserveSig]
	int Reset();

	/// <summary>Creates another enumerator that contains the same enumeration state as the current one.</summary>
	/// <param name="ppenum">On successful return, a reference to the newly created enumerator.</param>
	void Clone(out UCOMIEnumConnectionPoints ppenum);
}
