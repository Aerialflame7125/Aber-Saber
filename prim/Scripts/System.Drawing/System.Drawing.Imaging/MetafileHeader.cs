using System.Runtime.InteropServices;

namespace System.Drawing.Imaging;

/// <summary>Contains attributes of an associated <see cref="T:System.Drawing.Imaging.Metafile" />. Not inheritable.</summary>
[StructLayout(LayoutKind.Sequential)]
[System.MonoTODO("Metafiles, both WMF and EMF formats, aren't supported.")]
public sealed class MetafileHeader
{
	private MonoMetafileHeader header;

	/// <summary>Gets a <see cref="T:System.Drawing.Rectangle" /> that bounds the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that bounds the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
	public Rectangle Bounds => new Rectangle(header.x, header.y, header.width, header.height);

	/// <summary>Gets the horizontal resolution, in dots per inch, of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
	/// <returns>The horizontal resolution, in dots per inch, of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
	public float DpiX => header.dpi_x;

	/// <summary>Gets the vertical resolution, in dots per inch, of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
	/// <returns>The vertical resolution, in dots per inch, of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
	public float DpiY => header.dpi_y;

	/// <summary>Gets the size, in bytes, of the enhanced metafile plus header file.</summary>
	/// <returns>The size, in bytes, of the enhanced metafile plus header file.</returns>
	public int EmfPlusHeaderSize => header.emfplus_header_size;

	/// <summary>Gets the logical horizontal resolution, in dots per inch, of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
	/// <returns>The logical horizontal resolution, in dots per inch, of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
	public int LogicalDpiX => header.logical_dpi_x;

	/// <summary>Gets the logical vertical resolution, in dots per inch, of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
	/// <returns>The logical vertical resolution, in dots per inch, of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
	public int LogicalDpiY => header.logical_dpi_y;

	/// <summary>Gets the size, in bytes, of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
	/// <returns>The size, in bytes, of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
	public int MetafileSize => header.size;

	/// <summary>Gets the type of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Imaging.MetafileType" /> enumeration that represents the type of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
	public MetafileType Type => header.type;

	/// <summary>Gets the version number of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
	/// <returns>The version number of the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
	public int Version => header.version;

	/// <summary>Gets the Windows metafile (WMF) header file for the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Imaging.MetaHeader" /> that contains the WMF header file for the associated <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
	public MetaHeader WmfHeader
	{
		get
		{
			if (IsWmf())
			{
				return new MetaHeader(header.wmf_header);
			}
			throw new ArgumentException("WmfHeader only available on WMF files.");
		}
	}

	internal MetafileHeader(IntPtr henhmetafile)
	{
		Marshal.PtrToStructure(henhmetafile, this);
	}

	/// <summary>Returns a value that indicates whether the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is device dependent.</summary>
	/// <returns>
	///   <see langword="true" /> if the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is device dependent; otherwise, <see langword="false" />.</returns>
	[System.MonoTODO("always returns false")]
	public bool IsDisplay()
	{
		return false;
	}

	/// <summary>Returns a value that indicates whether the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is in the Windows enhanced metafile format.</summary>
	/// <returns>
	///   <see langword="true" /> if the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is in the Windows enhanced metafile format; otherwise, <see langword="false" />.</returns>
	public bool IsEmf()
	{
		return Type == MetafileType.Emf;
	}

	/// <summary>Returns a value that indicates whether the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is in the Windows enhanced metafile format or the Windows enhanced metafile plus format.</summary>
	/// <returns>
	///   <see langword="true" /> if the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is in the Windows enhanced metafile format or the Windows enhanced metafile plus format; otherwise, <see langword="false" />.</returns>
	public bool IsEmfOrEmfPlus()
	{
		return Type >= MetafileType.Emf;
	}

	/// <summary>Returns a value that indicates whether the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is in the Windows enhanced metafile plus format.</summary>
	/// <returns>
	///   <see langword="true" /> if the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is in the Windows enhanced metafile plus format; otherwise, <see langword="false" />.</returns>
	public bool IsEmfPlus()
	{
		return Type >= MetafileType.EmfPlusOnly;
	}

	/// <summary>Returns a value that indicates whether the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is in the Dual enhanced metafile format. This format supports both the enhanced and the enhanced plus format.</summary>
	/// <returns>
	///   <see langword="true" /> if the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is in the Dual enhanced metafile format; otherwise, <see langword="false" />.</returns>
	public bool IsEmfPlusDual()
	{
		return Type == MetafileType.EmfPlusDual;
	}

	/// <summary>Returns a value that indicates whether the associated <see cref="T:System.Drawing.Imaging.Metafile" /> supports only the Windows enhanced metafile plus format.</summary>
	/// <returns>
	///   <see langword="true" /> if the associated <see cref="T:System.Drawing.Imaging.Metafile" /> supports only the Windows enhanced metafile plus format; otherwise, <see langword="false" />.</returns>
	public bool IsEmfPlusOnly()
	{
		return Type == MetafileType.EmfPlusOnly;
	}

	/// <summary>Returns a value that indicates whether the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is in the Windows metafile format.</summary>
	/// <returns>
	///   <see langword="true" /> if the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is in the Windows metafile format; otherwise, <see langword="false" />.</returns>
	public bool IsWmf()
	{
		return Type <= MetafileType.WmfPlaceable;
	}

	/// <summary>Returns a value that indicates whether the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is in the Windows placeable metafile format.</summary>
	/// <returns>
	///   <see langword="true" /> if the associated <see cref="T:System.Drawing.Imaging.Metafile" /> is in the Windows placeable metafile format; otherwise, <see langword="false" />.</returns>
	public bool IsWmfPlaceable()
	{
		return Type == MetafileType.WmfPlaceable;
	}
}
