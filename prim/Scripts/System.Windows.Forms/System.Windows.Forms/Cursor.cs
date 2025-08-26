using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Windows.Forms;

/// <summary>Represents the image used to paint the mouse pointer.</summary>
/// <filterpriority>1</filterpriority>
/// <completionlist cref="T:System.Windows.Forms.Cursors" />
[Serializable]
[Editor("System.Drawing.Design.CursorEditor, System.Drawing.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
[TypeConverter(typeof(CursorConverter))]
public sealed class Cursor : IDisposable, ISerializable
{
	private struct CursorDir
	{
		internal ushort idReserved;

		internal ushort idType;

		internal ushort idCount;

		internal CursorEntry[] idEntries;
	}

	private struct CursorEntry
	{
		internal byte width;

		internal byte height;

		internal byte colorCount;

		internal byte reserved;

		internal ushort xHotspot;

		internal ushort yHotspot;

		internal ushort bitCount;

		internal uint sizeInBytes;

		internal uint fileOffset;
	}

	private struct CursorInfoHeader
	{
		internal uint biSize;

		internal int biWidth;

		internal int biHeight;

		internal ushort biPlanes;

		internal ushort biBitCount;

		internal uint biCompression;

		internal uint biSizeImage;

		internal int biXPelsPerMeter;

		internal int biYPelsPerMeter;

		internal uint biClrUsed;

		internal uint biClrImportant;
	}

	private struct CursorImage
	{
		internal CursorInfoHeader cursorHeader;

		internal uint[] cursorColors;

		internal byte[] cursorXOR;

		internal byte[] cursorAND;
	}

	private static Cursor current;

	private CursorDir cursor_dir;

	private CursorImage[] cursor_data;

	private int id;

	internal IntPtr handle;

	private Size size;

	private Bitmap shape;

	private Bitmap mask;

	private Bitmap cursor;

	internal string name;

	private StdCursor std_cursor = (StdCursor)(-1);

	private object tag;

	/// <summary>Gets or sets the bounds that represent the clipping rectangle for the cursor.</summary>
	/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that represents the clipping rectangle for the <see cref="T:System.Windows.Forms.Cursor" />, in screen coordinates.</returns>
	/// <filterpriority>1</filterpriority>
	public static Rectangle Clip
	{
		get
		{
			XplatUI.GrabInfo(out var intPtr, out var _, out var GrabArea);
			if (intPtr != IntPtr.Zero)
			{
				return GrabArea;
			}
			XplatUI.GetDisplaySize(out var size);
			GrabArea.X = 0;
			GrabArea.Y = 0;
			GrabArea.Width = size.Width;
			GrabArea.Height = size.Height;
			return GrabArea;
		}
		[System.MonoInternalNote("First need to add ability to set cursor clip rectangle to XplatUI drivers to implement this property")]
		[System.MonoTODO("Stub, does nothing")]
		set
		{
		}
	}

	/// <summary>Gets or sets a cursor object that represents the mouse cursor.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Cursor" /> that represents the mouse cursor. The default is null if the mouse cursor is not visible.</returns>
	/// <filterpriority>1</filterpriority>
	public static Cursor Current
	{
		get
		{
			if (current != null)
			{
				return current;
			}
			return Cursors.Default;
		}
		set
		{
			if (current != value)
			{
				current = value;
				if (current == null)
				{
					XplatUI.OverrideCursor(IntPtr.Zero);
				}
				else
				{
					XplatUI.OverrideCursor(current.handle);
				}
			}
		}
	}

	/// <summary>Gets or sets the cursor's position.</summary>
	/// <returns>A <see cref="T:System.Drawing.Point" /> that represents the cursor's position in screen coordinates.</returns>
	/// <filterpriority>1</filterpriority>
	public static Point Position
	{
		get
		{
			XplatUI.GetCursorPos(IntPtr.Zero, out var x, out var y);
			return new Point(x, y);
		}
		set
		{
			XplatUI.SetCursorPos(IntPtr.Zero, value.X, value.Y);
		}
	}

	/// <summary>Gets the handle of the cursor.</summary>
	/// <returns>An <see cref="T:System.IntPtr" /> that represents the cursor's handle.</returns>
	/// <exception cref="T:System.Exception">The handle value is <see cref="F:System.IntPtr.Zero" />. </exception>
	/// <filterpriority>1</filterpriority>
	public IntPtr Handle => handle;

	/// <summary>Gets the cursor hot spot.</summary>
	/// <returns>A <see cref="T:System.Drawing.Point" /> representing the cursor hot spot.</returns>
	/// <filterpriority>1</filterpriority>
	[System.MonoTODO("Implemented for Win32, X11 always returns 0,0")]
	public Point HotSpot
	{
		get
		{
			XplatUI.GetCursorInfo(Handle, out var _, out var _, out var hotspot_x, out var hotspot_y);
			return new Point(hotspot_x, hotspot_y);
		}
	}

	/// <summary>Gets the size of the cursor object.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the width and height of the <see cref="T:System.Windows.Forms.Cursor" />.</returns>
	/// <filterpriority>1</filterpriority>
	public Size Size => size;

	/// <summary>Gets or sets the object that contains data about the <see cref="T:System.Windows.Forms.Cursor" />.</summary>
	/// <returns>An <see cref="T:System.Object" /> that contains data about the <see cref="T:System.Windows.Forms.Cursor" />. The default is null.</returns>
	/// <filterpriority>1</filterpriority>
	[TypeConverter(typeof(StringConverter))]
	[MWFCategory("Data")]
	[Localizable(false)]
	[DefaultValue(null)]
	[Bindable(true)]
	public object Tag
	{
		get
		{
			return tag;
		}
		set
		{
			tag = value;
		}
	}

	internal Cursor(StdCursor cursor)
		: this(XplatUI.DefineStdCursor(cursor))
	{
		std_cursor = cursor;
	}

	private Cursor(SerializationInfo info, StreamingContext context)
	{
	}

	private Cursor()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Cursor" /> class from the specified Windows handle.</summary>
	/// <param name="handle">An <see cref="T:System.IntPtr" /> that represents the Windows handle of the cursor to create. </param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="handle" /> is <see cref="F:System.IntPtr.Zero" />. </exception>
	public Cursor(IntPtr handle)
	{
		this.handle = handle;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Cursor" /> class from the specified data stream.</summary>
	/// <param name="stream">The data stream to load the <see cref="T:System.Windows.Forms.Cursor" /> from. </param>
	public Cursor(Stream stream)
	{
		CreateCursor(stream);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Cursor" /> class from the specified file.</summary>
	/// <param name="fileName">The cursor file to load. </param>
	public Cursor(string fileName)
	{
		using FileStream stream = File.OpenRead(fileName);
		CreateCursor(stream);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Cursor" /> class from the specified resource with the specified resource type.</summary>
	/// <param name="type">The resource <see cref="T:System.Type" />. </param>
	/// <param name="resource">The name of the resource. </param>
	public Cursor(Type type, string resource)
	{
		using (Stream stream = type.Assembly.GetManifestResourceStream(type, resource))
		{
			if (stream != null)
			{
				CreateCursor(stream);
				return;
			}
		}
		using (Stream stream2 = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
		{
			if (stream2 != null)
			{
				CreateCursor(stream2);
				return;
			}
		}
		throw new FileNotFoundException("Resource name was not found: `" + resource + "'");
	}

	/// <summary>Serializes the object.</summary>
	/// <param name="si">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> class.</param>
	/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> class.</param>
	void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
	{
		MemoryStream memoryStream = new MemoryStream();
		BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
		CursorImage cursorImage = cursor_data[id];
		binaryWriter.Write((ushort)0);
		binaryWriter.Write((ushort)2);
		binaryWriter.Write((ushort)1);
		binaryWriter.Write(cursor_dir.idEntries[id].width);
		binaryWriter.Write(cursor_dir.idEntries[id].height);
		binaryWriter.Write(cursor_dir.idEntries[id].colorCount);
		binaryWriter.Write(cursor_dir.idEntries[id].reserved);
		binaryWriter.Write(cursor_dir.idEntries[id].xHotspot);
		binaryWriter.Write(cursor_dir.idEntries[id].yHotspot);
		binaryWriter.Write((uint)(40 + cursorImage.cursorColors.Length * 4 + cursorImage.cursorXOR.Length + cursorImage.cursorAND.Length));
		binaryWriter.Write(22u);
		binaryWriter.Write(cursorImage.cursorHeader.biSize);
		binaryWriter.Write(cursorImage.cursorHeader.biWidth);
		binaryWriter.Write(cursorImage.cursorHeader.biHeight);
		binaryWriter.Write(cursorImage.cursorHeader.biPlanes);
		binaryWriter.Write(cursorImage.cursorHeader.biBitCount);
		binaryWriter.Write(cursorImage.cursorHeader.biCompression);
		binaryWriter.Write(cursorImage.cursorHeader.biSizeImage);
		binaryWriter.Write(cursorImage.cursorHeader.biXPelsPerMeter);
		binaryWriter.Write(cursorImage.cursorHeader.biYPelsPerMeter);
		binaryWriter.Write(cursorImage.cursorHeader.biClrUsed);
		binaryWriter.Write(cursorImage.cursorHeader.biClrImportant);
		for (int i = 0; i < cursorImage.cursorColors.Length; i++)
		{
			binaryWriter.Write(cursorImage.cursorColors[i]);
		}
		binaryWriter.Write(cursorImage.cursorXOR);
		binaryWriter.Write(cursorImage.cursorAND);
		binaryWriter.Flush();
		si.AddValue("CursorData", memoryStream.ToArray());
	}

	private void CreateCursor(Stream stream)
	{
		InitFromStream(stream);
		shape = ToBitmap(xor: true, transparent: false);
		mask = ToBitmap(xor: false, transparent: false);
		handle = XplatUI.DefineCursor(shape, mask, Color.FromArgb(255, 255, 255), Color.FromArgb(255, 255, 255), cursor_dir.idEntries[id].xHotspot, cursor_dir.idEntries[id].yHotspot);
		shape.Dispose();
		shape = null;
		mask.Dispose();
		mask = null;
		if (handle != IntPtr.Zero)
		{
			cursor = ToBitmap(xor: true, transparent: true);
		}
	}

	~Cursor()
	{
		Dispose();
	}

	/// <summary>Hides the cursor.</summary>
	/// <filterpriority>1</filterpriority>
	public static void Hide()
	{
		XplatUI.ShowCursor(show: false);
	}

	/// <summary>Displays the cursor.</summary>
	/// <filterpriority>1</filterpriority>
	public static void Show()
	{
		XplatUI.ShowCursor(show: true);
	}

	/// <summary>Copies the handle of this <see cref="T:System.Windows.Forms.Cursor" />.</summary>
	/// <returns>An <see cref="T:System.IntPtr" /> that represents the cursor's handle.</returns>
	/// <filterpriority>1</filterpriority>
	public IntPtr CopyHandle()
	{
		return handle;
	}

	/// <summary>Releases all resources used by the <see cref="T:System.Windows.Forms.Cursor" />.</summary>
	/// <filterpriority>1</filterpriority>
	public void Dispose()
	{
		if (cursor != null)
		{
			cursor.Dispose();
			cursor = null;
		}
		if (shape != null)
		{
			shape.Dispose();
			shape = null;
		}
		if (mask != null)
		{
			mask.Dispose();
			mask = null;
		}
		GC.SuppressFinalize(this);
	}

	/// <summary>Draws the cursor on the specified surface, within the specified bounds.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> surface on which to draw the <see cref="T:System.Windows.Forms.Cursor" />. </param>
	/// <param name="targetRect">The <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the <see cref="T:System.Windows.Forms.Cursor" />. </param>
	/// <filterpriority>1</filterpriority>
	public void Draw(Graphics g, Rectangle targetRect)
	{
		if (cursor == null && std_cursor != (StdCursor)(-1))
		{
			cursor = XplatUI.DefineStdCursorBitmap(std_cursor);
		}
		if (cursor != null)
		{
			g.DrawImage(cursor, targetRect.X, targetRect.Y);
		}
	}

	/// <summary>Draws the cursor in a stretched format on the specified surface, within the specified bounds.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> surface on which to draw the <see cref="T:System.Windows.Forms.Cursor" />. </param>
	/// <param name="targetRect">The <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the <see cref="T:System.Windows.Forms.Cursor" />. </param>
	/// <filterpriority>1</filterpriority>
	public void DrawStretched(Graphics g, Rectangle targetRect)
	{
		if (cursor == null && std_cursor != (StdCursor)(-1))
		{
			cursor = XplatUI.DefineStdCursorBitmap(std_cursor);
		}
		if (cursor != null)
		{
			g.DrawImage(cursor, targetRect, new Rectangle(0, 0, cursor.Width, cursor.Height), GraphicsUnit.Pixel);
		}
	}

	/// <summary>Returns a value indicating whether this cursor is equal to the specified <see cref="T:System.Windows.Forms.Cursor" />.</summary>
	/// <returns>true if this cursor is equal to the specified <see cref="T:System.Windows.Forms.Cursor" />; otherwise, false.</returns>
	/// <param name="obj">The <see cref="T:System.Windows.Forms.Cursor" /> to compare. </param>
	/// <filterpriority>1</filterpriority>
	public override bool Equals(object obj)
	{
		if (!(obj is Cursor))
		{
			return false;
		}
		if (((Cursor)obj).handle == handle)
		{
			return true;
		}
		return false;
	}

	/// <summary>Retrieves the hash code for the current <see cref="T:System.Windows.Forms.Cursor" />.</summary>
	/// <returns>A hash code for the current <see cref="T:System.Windows.Forms.Cursor" />.</returns>
	/// <filterpriority>1</filterpriority>
	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	/// <summary>Retrieves a human readable string representing this <see cref="T:System.Windows.Forms.Cursor" />.</summary>
	/// <returns>A <see cref="T:System.String" /> that represents this <see cref="T:System.Windows.Forms.Cursor" />.</returns>
	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		if (name != null)
		{
			return "[Cursor:" + name + "]";
		}
		throw new FormatException("Cannot convert custom cursors to string.");
	}

	private void InitFromStream(Stream stream)
	{
		if (stream == null || stream.Length == 0L)
		{
			throw new ArgumentException("The argument 'stream' must be a picture that can be used as a cursor", "stream");
		}
		BinaryReader binaryReader = new BinaryReader(stream);
		cursor_dir = default(CursorDir);
		cursor_dir.idReserved = binaryReader.ReadUInt16();
		cursor_dir.idType = binaryReader.ReadUInt16();
		if (cursor_dir.idReserved != 0 || (cursor_dir.idType != 2 && cursor_dir.idType != 1))
		{
			throw new ArgumentException("Invalid Argument, format error", "stream");
		}
		ushort num = binaryReader.ReadUInt16();
		cursor_dir.idCount = num;
		cursor_dir.idEntries = new CursorEntry[num];
		cursor_data = new CursorImage[num];
		for (int i = 0; i < num; i++)
		{
			CursorEntry cursorEntry = default(CursorEntry);
			cursorEntry.width = binaryReader.ReadByte();
			cursorEntry.height = binaryReader.ReadByte();
			cursorEntry.colorCount = binaryReader.ReadByte();
			cursorEntry.reserved = binaryReader.ReadByte();
			cursorEntry.xHotspot = binaryReader.ReadUInt16();
			cursorEntry.yHotspot = binaryReader.ReadUInt16();
			if (cursor_dir.idType == 1)
			{
				cursorEntry.xHotspot = (ushort)(cursorEntry.width / 2);
				cursorEntry.yHotspot = (ushort)(cursorEntry.height / 2);
			}
			cursorEntry.sizeInBytes = binaryReader.ReadUInt32();
			cursorEntry.fileOffset = binaryReader.ReadUInt32();
			cursor_dir.idEntries[i] = cursorEntry;
		}
		uint num2 = 0u;
		for (int j = 0; j < num; j++)
		{
			if (cursor_dir.idEntries[j].sizeInBytes >= num2)
			{
				num2 = cursor_dir.idEntries[j].sizeInBytes;
				id = (ushort)j;
				size.Height = cursor_dir.idEntries[j].height;
				size.Width = cursor_dir.idEntries[j].width;
			}
		}
		for (int k = 0; k < num; k++)
		{
			CursorImage cursorImage = default(CursorImage);
			CursorInfoHeader cursorHeader = default(CursorInfoHeader);
			stream.Seek(cursor_dir.idEntries[k].fileOffset, SeekOrigin.Begin);
			byte[] array = new byte[cursor_dir.idEntries[k].sizeInBytes];
			stream.Read(array, 0, array.Length);
			BinaryReader binaryReader2 = new BinaryReader(new MemoryStream(array));
			cursorHeader.biSize = binaryReader2.ReadUInt32();
			if (cursorHeader.biSize != 40)
			{
				throw new ArgumentException("Invalid cursor file", "stream");
			}
			cursorHeader.biWidth = binaryReader2.ReadInt32();
			cursorHeader.biHeight = binaryReader2.ReadInt32();
			cursorHeader.biPlanes = binaryReader2.ReadUInt16();
			cursorHeader.biBitCount = binaryReader2.ReadUInt16();
			cursorHeader.biCompression = binaryReader2.ReadUInt32();
			cursorHeader.biSizeImage = binaryReader2.ReadUInt32();
			cursorHeader.biXPelsPerMeter = binaryReader2.ReadInt32();
			cursorHeader.biYPelsPerMeter = binaryReader2.ReadInt32();
			cursorHeader.biClrUsed = binaryReader2.ReadUInt32();
			cursorHeader.biClrImportant = binaryReader2.ReadUInt32();
			cursorImage.cursorHeader = cursorHeader;
			int num3 = cursorHeader.biBitCount switch
			{
				1 => 2, 
				4 => 16, 
				8 => 256, 
				_ => 0, 
			};
			cursorImage.cursorColors = new uint[num3];
			for (int l = 0; l < num3; l++)
			{
				cursorImage.cursorColors[l] = binaryReader2.ReadUInt32();
			}
			int num4 = cursorHeader.biHeight / 2;
			int num5 = cursorHeader.biWidth * cursorHeader.biPlanes * cursorHeader.biBitCount + 31 >> 5 << 2;
			int num6 = num5 * num4;
			cursorImage.cursorXOR = new byte[num6];
			for (int m = 0; m < num6; m++)
			{
				cursorImage.cursorXOR[m] = binaryReader2.ReadByte();
			}
			int num7 = (int)(binaryReader2.BaseStream.Length - binaryReader2.BaseStream.Position);
			cursorImage.cursorAND = new byte[num7];
			for (int n = 0; n < num7; n++)
			{
				cursorImage.cursorAND[n] = binaryReader2.ReadByte();
			}
			cursor_data[k] = cursorImage;
			binaryReader2.Close();
		}
		binaryReader.Close();
	}

	private Bitmap ToBitmap(bool xor, bool transparent)
	{
		if (cursor_data == null)
		{
			return new Bitmap(32, 32);
		}
		CursorImage cursorImage = cursor_data[id];
		CursorInfoHeader cursorHeader = cursorImage.cursorHeader;
		int num = cursorHeader.biHeight / 2;
		Bitmap bitmap;
		if (!xor)
		{
			bitmap = new Bitmap(cursorHeader.biWidth, num, PixelFormat.Format1bppIndexed);
			ColorPalette palette = bitmap.Palette;
			ref Color reference = ref palette.Entries[0];
			reference = Color.FromArgb(0, 0, 0);
			ref Color reference2 = ref palette.Entries[1];
			reference2 = Color.FromArgb(-1);
			BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, bitmap.PixelFormat);
			for (int i = 0; i < num; i++)
			{
				Marshal.Copy(cursorImage.cursorAND, bitmapData.Stride * i, (IntPtr)(bitmapData.Scan0.ToInt64() + bitmapData.Stride * (num - 1 - i)), bitmapData.Stride);
			}
			bitmap.UnlockBits(bitmapData);
		}
		else
		{
			if (cursorHeader.biClrUsed == 0 && cursorHeader.biBitCount < 24)
			{
				int num2 = 1 << (int)cursorHeader.biBitCount;
			}
			switch (cursorHeader.biBitCount)
			{
			case 1:
				bitmap = new Bitmap(cursorHeader.biWidth, num, PixelFormat.Format1bppIndexed);
				break;
			case 4:
				bitmap = new Bitmap(cursorHeader.biWidth, num, PixelFormat.Format4bppIndexed);
				break;
			case 8:
				bitmap = new Bitmap(cursorHeader.biWidth, num, PixelFormat.Format8bppIndexed);
				break;
			case 24:
			case 32:
				bitmap = new Bitmap(cursorHeader.biWidth, num, PixelFormat.Format32bppArgb);
				break;
			default:
				throw new Exception("Unexpected number of bits:" + cursorHeader.biBitCount);
			}
			if (cursorHeader.biBitCount < 24)
			{
				ColorPalette palette = bitmap.Palette;
				for (int j = 0; j < cursorImage.cursorColors.Length; j++)
				{
					ref Color reference3 = ref palette.Entries[j];
					reference3 = Color.FromArgb((int)cursorImage.cursorColors[j] | -16777216);
				}
				bitmap.Palette = palette;
			}
			int num3 = ((cursorHeader.biWidth * cursorHeader.biBitCount + 31) & -32) >> 3;
			BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, bitmap.PixelFormat);
			for (int k = 0; k < num; k++)
			{
				Marshal.Copy(cursorImage.cursorXOR, num3 * k, (IntPtr)(bitmapData.Scan0.ToInt64() + bitmapData.Stride * (num - 1 - k)), num3);
			}
			bitmap.UnlockBits(bitmapData);
		}
		if (transparent)
		{
			bitmap = new Bitmap(bitmap);
			for (int l = 0; l < num; l++)
			{
				for (int m = 0; m < cursorHeader.biWidth / 8; m++)
				{
					for (int num4 = 7; num4 >= 0; num4--)
					{
						if (((cursorImage.cursorAND[l * cursorHeader.biWidth / 8 + m] >> num4) & 1) != 0)
						{
							bitmap.SetPixel(m * 8 + 7 - num4, num - l - 1, Color.Transparent);
						}
					}
				}
			}
		}
		return bitmap;
	}

	/// <summary>Returns a value indicating whether two instances of the <see cref="T:System.Windows.Forms.Cursor" /> class are not equal.</summary>
	/// <returns>true if two instances of the <see cref="T:System.Windows.Forms.Cursor" /> class are not equal; otherwise, false.</returns>
	/// <param name="left">A <see cref="T:System.Windows.Forms.Cursor" /> to compare. </param>
	/// <param name="right">A <see cref="T:System.Windows.Forms.Cursor" /> to compare. </param>
	/// <filterpriority>3</filterpriority>
	public static bool operator !=(Cursor left, Cursor right)
	{
		if ((object)left == right)
		{
			return false;
		}
		if ((object)left == null || (object)right == null)
		{
			return true;
		}
		if (left.handle == right.handle)
		{
			return false;
		}
		return true;
	}

	/// <summary>Returns a value indicating whether two instances of the <see cref="T:System.Windows.Forms.Cursor" /> class are equal.</summary>
	/// <returns>true if two instances of the <see cref="T:System.Windows.Forms.Cursor" /> class are equal; otherwise, false.</returns>
	/// <param name="left">A <see cref="T:System.Windows.Forms.Cursor" /> to compare. </param>
	/// <param name="right">A <see cref="T:System.Windows.Forms.Cursor" /> to compare. </param>
	/// <filterpriority>3</filterpriority>
	public static bool operator ==(Cursor left, Cursor right)
	{
		if ((object)left == right)
		{
			return true;
		}
		if ((object)left == null || (object)right == null)
		{
			return false;
		}
		if (left.handle == right.handle)
		{
			return true;
		}
		return false;
	}
}
