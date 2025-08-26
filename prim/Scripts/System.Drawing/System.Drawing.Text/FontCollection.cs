using System.Runtime.InteropServices;

namespace System.Drawing.Text;

/// <summary>Provides a base class for installed and private font collections.</summary>
public abstract class FontCollection : IDisposable
{
	internal IntPtr _nativeFontCollection;

	/// <summary>Gets the array of <see cref="T:System.Drawing.FontFamily" /> objects associated with this <see cref="T:System.Drawing.Text.FontCollection" />.</summary>
	/// <returns>An array of <see cref="T:System.Drawing.FontFamily" /> objects.</returns>
	public FontFamily[] Families
	{
		get
		{
			int found = 0;
			SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipGetFontCollectionFamilyCount(new HandleRef(this, _nativeFontCollection), out found));
			IntPtr[] array = new IntPtr[found];
			int retCount = 0;
			SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipGetFontCollectionFamilyList(new HandleRef(this, _nativeFontCollection), found, array, out retCount));
			FontFamily[] array2 = new FontFamily[retCount];
			for (int i = 0; i < retCount; i++)
			{
				GDIPlus.GdipCloneFontFamily(new HandleRef(null, array[i]), out var clone);
				array2[i] = new FontFamily(clone);
			}
			return array2;
		}
	}

	internal FontCollection()
	{
		_nativeFontCollection = IntPtr.Zero;
	}

	/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Text.FontCollection" />.</summary>
	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Drawing.Text.FontCollection" /> and optionally releases the managed resources.</summary>
	/// <param name="disposing">
	///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
	protected virtual void Dispose(bool disposing)
	{
	}

	/// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
	~FontCollection()
	{
		Dispose(disposing: false);
	}
}
