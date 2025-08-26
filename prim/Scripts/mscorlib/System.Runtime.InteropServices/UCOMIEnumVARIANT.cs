namespace System.Runtime.InteropServices;

/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.IEnumVARIANT" /> instead.</summary>
[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[Guid("00020404-0000-0000-c000-000000000046")]
[Obsolete]
public interface UCOMIEnumVARIANT
{
	/// <summary>Retrieves a specified number of items in the enumeration sequence.</summary>
	/// <param name="celt">The number of elements to return in rgelt.</param>
	/// <param name="rgvar">On successful return, a reference to the enumerated elements.</param>
	/// <param name="pceltFetched">On successful return, a reference to the actual number of elements enumerated in rgelt.</param>
	/// <returns>
	///   <see langword="S_OK" /> if the <paramref name="pceltFetched" /> parameter equals the <paramref name="celt" /> parameter; otherwise, <see langword="S_FALSE" />.</returns>
	[PreserveSig]
	int Next(int celt, int rgvar, int pceltFetched);

	/// <summary>Skips over a specified number of items in the enumeration sequence.</summary>
	/// <param name="celt">The number of elements to skip in the enumeration.</param>
	/// <returns>
	///   <see langword="S_OK" /> if the number of elements skipped equals <paramref name="celt" /> parameter; otherwise, <see langword="S_FALSE" />.</returns>
	[PreserveSig]
	int Skip(int celt);

	/// <summary>Resets the enumeration sequence to the beginning.</summary>
	/// <returns>An HRESULT with the value <see langword="S_OK" />.</returns>
	[PreserveSig]
	int Reset();

	/// <summary>Creates another enumerator that contains the same enumeration state as the current one.</summary>
	/// <param name="ppenum">On successful return, a reference to the newly created enumerator.</param>
	void Clone(int ppenum);
}
