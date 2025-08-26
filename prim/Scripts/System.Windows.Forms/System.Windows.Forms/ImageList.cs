using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.Globalization;

namespace System.Windows.Forms;

/// <summary>Provides methods to manage a collection of <see cref="T:System.Drawing.Image" /> objects. This class cannot be inherited.</summary>
/// <filterpriority>2</filterpriority>
[DesignerSerializer("System.Windows.Forms.Design.ImageListCodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[ToolboxItemFilter("System.Windows.Forms")]
[TypeConverter(typeof(ImageListConverter))]
[Designer("System.Windows.Forms.Design.ImageListDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[DefaultProperty("Images")]
public sealed class ImageList : Component
{
	/// <summary>Encapsulates the collection of <see cref="T:System.Drawing.Image" /> objects in an <see cref="T:System.Windows.Forms.ImageList" />.</summary>
	[Editor("System.Windows.Forms.Design.ImageCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public sealed class ImageCollection : ICollection, IEnumerable, IList
	{
		private static class IndexedColorDepths
		{
			internal static readonly ColorPalette Palette4Bit;

			internal static readonly ColorPalette Palette8Bit;

			private static readonly int[] squares;

			static IndexedColorDepths()
			{
				Bitmap bitmap = new Bitmap(1, 1, PixelFormat.Format4bppIndexed);
				Palette4Bit = bitmap.Palette;
				bitmap.Dispose();
				bitmap = new Bitmap(1, 1, PixelFormat.Format8bppIndexed);
				Palette8Bit = bitmap.Palette;
				bitmap.Dispose();
				squares = new int[511];
				for (int i = 0; i < 256; i++)
				{
					squares[255 + i] = (squares[255 - i] = i * i);
				}
			}

			internal static int GetNearestColor(Color[] palette, int color)
			{
				int num = palette.Length;
				for (int i = 0; i < num; i++)
				{
					if (palette[i].ToArgb() == color)
					{
						return color;
					}
				}
				int num2 = (color >>> 16) & 0xFF;
				int num3 = (color >>> 8) & 0xFF;
				int num4 = color & 0xFF;
				int result = -16777216;
				int num5 = int.MaxValue;
				for (int i = 0; i < num; i++)
				{
					int num6;
					if ((num6 = squares[255 + palette[i].R - num2] + squares[255 + palette[i].G - num3] + squares[255 + palette[i].B - num4]) < num5)
					{
						result = palette[i].ToArgb();
						num5 = num6;
					}
				}
				return result;
			}
		}

		[Flags]
		private enum ItemFlags
		{
			None = 0,
			UseTransparentColor = 1,
			ImageStrip = 2
		}

		private sealed class ImageListItem
		{
			internal readonly object Image;

			internal readonly ItemFlags Flags;

			internal readonly Color TransparentColor;

			internal readonly int ImageCount = 1;

			internal ImageListItem(Icon value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				Image = (Icon)value.Clone();
			}

			internal ImageListItem(Image value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!(value is Bitmap))
				{
					throw new ArgumentException("Image must be a Bitmap.");
				}
				Image = value;
			}

			internal ImageListItem(Image value, Color transparentColor)
				: this(value)
			{
				Flags = ItemFlags.UseTransparentColor;
				TransparentColor = transparentColor;
			}

			internal ImageListItem(Image value, int imageCount)
				: this(value)
			{
				Flags = ItemFlags.ImageStrip;
				ImageCount = imageCount;
			}
		}

		private const int AlphaMask = -16777216;

		private ColorDepth colorDepth = ColorDepth.Depth8Bit;

		private Size imageSize = DefaultImageSize;

		private Color transparentColor = DefaultTransparentColor;

		private ArrayList list = new ArrayList();

		private ArrayList keys = new ArrayList();

		private int count;

		private bool handleCreated;

		private int lastKeyIndex = -1;

		private readonly ImageList owner;

		/// <summary>Gets or sets an image in an existing <see cref="T:System.Windows.Forms.ImageList.ImageCollection" />.</summary>
		/// <returns>The image in the list specified by the index.</returns>
		/// <param name="index">The zero-based index of the image to get or set. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index is less than 0 or greater than or equal to <see cref="P:System.Windows.Forms.ImageList.ImageCollection.Count" />.</exception>
		/// <exception cref="T:System.Exception">The attempt to replace the image failed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The image to be assigned is null or not a bitmap.</exception>
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				if (!(value is Image))
				{
					throw new ArgumentException("value");
				}
				this[index] = (Image)value;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ImageList.ImageCollection" /> has a fixed size.</summary>
		/// <returns>false in all cases.</returns>
		bool IList.IsFixedSize => false;

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>false in all cases.</returns>
		bool ICollection.IsSynchronized => false;

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>The object used to synchronize the <see cref="T:System.Windows.Forms.ImageList.ImageCollection" />.</returns>
		object ICollection.SyncRoot => this;

		internal ColorDepth ColorDepth
		{
			get
			{
				return colorDepth;
			}
			set
			{
				if (!Enum.IsDefined(typeof(ColorDepth), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ColorDepth));
				}
				if (colorDepth != value)
				{
					colorDepth = value;
					RecreateHandle();
				}
			}
		}

		internal IntPtr Handle
		{
			get
			{
				CreateHandle();
				return (IntPtr)(-1);
			}
		}

		internal bool HandleCreated => handleCreated;

		internal Size ImageSize
		{
			get
			{
				return imageSize;
			}
			set
			{
				if (value.Width < 1 || value.Width > 256 || value.Height < 1 || value.Height > 256)
				{
					throw new ArgumentException("ImageSize.Width and Height must be between 1 and 256", "value");
				}
				if (imageSize != value)
				{
					imageSize = value;
					RecreateHandle();
				}
			}
		}

		internal ImageListStreamer ImageStream
		{
			get
			{
				return (!Empty) ? new ImageListStreamer(this) : null;
			}
			set
			{
				Image[] images;
				if (value == null)
				{
					if (handleCreated)
					{
						DestroyHandle();
					}
					else
					{
						Clear();
					}
				}
				else if ((images = value.Images) != null)
				{
					list = new ArrayList(images.Length);
					count = 0;
					handleCreated = true;
					keys = new ArrayList(images.Length);
					for (int i = 0; i < images.Length; i++)
					{
						list.Add((Image)images[i].Clone());
						keys.Add(null);
					}
					if (Enum.IsDefined(typeof(ColorDepth), value.ColorDepth))
					{
						colorDepth = value.ColorDepth;
					}
					imageSize = value.ImageSize;
					owner.OnRecreateHandle();
				}
			}
		}

		internal Color TransparentColor
		{
			get
			{
				return transparentColor;
			}
			set
			{
				transparentColor = value;
			}
		}

		/// <summary>Gets the number of images currently in the list.</summary>
		/// <returns>The number of images in the list. The default is 0.</returns>
		[Browsable(false)]
		public int Count => (!handleCreated) ? count : list.Count;

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ImageList" /> has any images.</summary>
		/// <returns>true if there are no images in the list; otherwise, false. The default is false.</returns>
		public bool Empty => Count == 0;

		/// <summary>Gets a value indicating whether the list is read-only.</summary>
		/// <returns>Always false.</returns>
		public bool IsReadOnly => false;

		/// <summary>Gets or sets an <see cref="T:System.Drawing.Image" /> at the specified index within the collection.</summary>
		/// <returns>The image in the list specified by <paramref name="index" />. </returns>
		/// <param name="index">The index of the image to get or set. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index is less than 0 or greater than or equal to <see cref="P:System.Windows.Forms.ImageList.ImageCollection.Count" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="image" /> is not a <see cref="T:System.Drawing.Bitmap" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The image to be assigned is null or not a <see cref="T:System.Drawing.Bitmap" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">The image cannot be added to the list.</exception>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Image this[int index]
		{
			get
			{
				return (Image)GetImage(index).Clone();
			}
			set
			{
				if (index < 0 || index >= Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!(value is Bitmap))
				{
					throw new ArgumentException("Image must be a Bitmap.");
				}
				Image value2 = CreateImage(value, transparentColor);
				CreateHandle();
				list[index] = value2;
			}
		}

		/// <summary>Gets an <see cref="T:System.Drawing.Image" /> with the specified key from the collection.</summary>
		/// <returns>The <see cref="T:System.Drawing.Image" /> with the specified key.</returns>
		/// <param name="key">The name of the image to retrieve from the collection.</param>
		public Image this[string key]
		{
			get
			{
				int index;
				return ((index = IndexOfKey(key)) != -1) ? this[index] : null;
			}
		}

		/// <summary>Gets the collection of keys associated with the images in the <see cref="T:System.Windows.Forms.ImageList.ImageCollection" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> containing the names of the images in the <see cref="T:System.Windows.Forms.ImageList.ImageCollection" />.</returns>
		public StringCollection Keys
		{
			get
			{
				StringCollection stringCollection = new StringCollection();
				for (int i = 0; i < keys.Count; i++)
				{
					string text;
					stringCollection.Add(((text = (string)keys[i]) != null && text.Length != 0) ? text : string.Empty);
				}
				return stringCollection;
			}
		}

		internal event EventHandler Changed;

		internal ImageCollection(ImageList owner)
		{
			this.owner = owner;
		}

		/// <summary>Adds the specified image to the <see cref="T:System.Windows.Forms.ImageList" />.</summary>
		/// <returns>The index of the newly added image, or -1 if the image could not be added.</returns>
		/// <param name="value">The image to add to the list.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.Drawing.Bitmap" />.</exception>
		int IList.Add(object value)
		{
			if (!(value is Image))
			{
				throw new ArgumentException("value");
			}
			int result = Count;
			Add((Image)value);
			return result;
		}

		/// <summary>Implements the <see cref="M:System.Collections.IList.Contains(System.Object)" /> method. Throws a <see cref="T:System.NotSupportedException" /> in all cases.</summary>
		/// <param name="image">The image to locate in the <see cref="T:System.Windows.Forms.ImageList.ImageCollection" />.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		bool IList.Contains(object image)
		{
			return image is Image && Contains((Image)image);
		}

		/// <summary>Implements the <see cref="M:System.Collections.IList.IndexOf(System.Object)" /> method. Throws a <see cref="T:System.NotSupportedException" /> in all cases.</summary>
		/// <param name="image">The image to find in the list.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		int IList.IndexOf(object image)
		{
			return (!(image is Image)) ? (-1) : IndexOf((Image)image);
		}

		/// <summary>Implements the <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" /> method. Throws a <see cref="T:System.NotSupportedException" /> in all cases.</summary>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		/// <summary>Implements the <see cref="M:System.Collections.IList.Remove(System.Object)" />. Throws a <see cref="T:System.NotSupportedException" /> in all cases.</summary>
		/// <param name="image"></param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		void IList.Remove(object image)
		{
			if (image is Image)
			{
				Remove((Image)image);
			}
		}

		/// <summary>Copies the items in this collection to a compatible one-dimensional array, starting at the specified index of the target array.</summary>
		/// <param name="dest">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the collection. The array must have zero-based indexing.  </param>
		/// <param name="index">The zero-based index in the <see cref="T:System.Array" /> at which copying begins.  </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dest" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dest" /> is multidimensional.-or-The number of elements in the <see cref="T:System.Windows.Forms.ComboBox.ObjectCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination array.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the <see cref="T:System.Windows.Forms.ComboBox.ObjectCollection" /> cannot be cast automatically to the type of the destination array.</exception>
		void ICollection.CopyTo(Array dest, int index)
		{
			for (int i = 0; i < Count; i++)
			{
				dest.SetValue(this[i], index++);
			}
		}

		private static bool CompareKeys(string key1, string key2)
		{
			if (key1 == null || key2 == null || key1.Length != key2.Length)
			{
				return false;
			}
			return string.Compare(key1, key2, ignoreCase: true, CultureInfo.InvariantCulture) == 0;
		}

		private int AddItem(string key, ImageListItem item)
		{
			int result;
			if (handleCreated)
			{
				result = AddItemInternal(item);
			}
			else
			{
				result = list.Add(item);
				count += item.ImageCount;
			}
			if ((item.Flags & ItemFlags.ImageStrip) == 0)
			{
				keys.Add(key);
			}
			else
			{
				for (int i = 0; i < item.ImageCount; i++)
				{
					keys.Add(null);
				}
			}
			return result;
		}

		private int AddItemInternal(ImageListItem item)
		{
			if (this.Changed != null)
			{
				this.Changed(this, EventArgs.Empty);
			}
			if (item.Image is Icon)
			{
				int width;
				int height;
				Bitmap bitmap = new Bitmap(width = imageSize.Width, height = imageSize.Height, PixelFormat.Format32bppArgb);
				Graphics graphics = Graphics.FromImage(bitmap);
				graphics.DrawIcon((Icon)item.Image, new Rectangle(0, 0, width, height));
				graphics.Dispose();
				ReduceColorDepth(bitmap);
				return list.Add(bitmap);
			}
			if ((item.Flags & ItemFlags.ImageStrip) == 0)
			{
				return list.Add(CreateImage((Image)item.Image, ((item.Flags & ItemFlags.UseTransparentColor) != 0) ? item.TransparentColor : transparentColor));
			}
			int width2;
			Image image;
			int width3;
			if ((width2 = (image = (Image)item.Image).Width) == 0 || width2 % (width3 = imageSize.Width) != 0)
			{
				throw new ArgumentException("Width of image strip must be a positive multiple of ImageSize.Width.", "value");
			}
			int height2;
			if (image.Height != (height2 = imageSize.Height))
			{
				throw new ArgumentException("Height of image strip must be equal to ImageSize.Height.", "value");
			}
			Rectangle destRect = new Rectangle(0, 0, width3, height2);
			ImageAttributes imageAttributes;
			if (transparentColor.A == 0)
			{
				imageAttributes = null;
			}
			else
			{
				imageAttributes = new ImageAttributes();
				imageAttributes.SetColorKey(transparentColor, transparentColor);
			}
			int result = list.Count;
			for (int i = 0; i < width2; i += width3)
			{
				Bitmap bitmap2 = new Bitmap(width3, height2, PixelFormat.Format32bppArgb);
				Graphics graphics2 = Graphics.FromImage(bitmap2);
				graphics2.DrawImage(image, destRect, i, 0, width3, height2, GraphicsUnit.Pixel, imageAttributes);
				graphics2.Dispose();
				ReduceColorDepth(bitmap2);
				list.Add(bitmap2);
			}
			imageAttributes?.Dispose();
			return result;
		}

		private void CreateHandle()
		{
			if (!handleCreated)
			{
				ArrayList arrayList = list;
				list = new ArrayList(count);
				count = 0;
				handleCreated = true;
				for (int i = 0; i < arrayList.Count; i++)
				{
					AddItemInternal((ImageListItem)arrayList[i]);
				}
			}
		}

		private Image CreateImage(Image value, Color transparentColor)
		{
			ImageAttributes imageAttributes;
			if (transparentColor.A == 0)
			{
				imageAttributes = null;
			}
			else
			{
				imageAttributes = new ImageAttributes();
				imageAttributes.SetColorKey(transparentColor, transparentColor);
			}
			int width;
			int height;
			Bitmap bitmap = new Bitmap(width = imageSize.Width, height = imageSize.Height, PixelFormat.Format32bppArgb);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.DrawImage(value, new Rectangle(0, 0, width, height), 0, 0, value.Width, value.Height, GraphicsUnit.Pixel, imageAttributes);
			graphics.Dispose();
			imageAttributes?.Dispose();
			ReduceColorDepth(bitmap);
			return bitmap;
		}

		private void RecreateHandle()
		{
			if (handleCreated)
			{
				DestroyHandle();
				handleCreated = true;
				owner.OnRecreateHandle();
			}
		}

		private unsafe void ReduceColorDepth(Bitmap bitmap)
		{
			if (colorDepth >= ColorDepth.Depth32Bit)
			{
				return;
			}
			BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
			try
			{
				byte* ptr = (byte*)(void*)bitmapData.Scan0;
				int height = bitmapData.Height;
				int num = bitmapData.Width << 2;
				int stride = bitmapData.Stride;
				if (colorDepth < ColorDepth.Depth16Bit)
				{
					Color[] entries = ((colorDepth >= ColorDepth.Depth8Bit) ? IndexedColorDepths.Palette8Bit : IndexedColorDepths.Palette4Bit).Entries;
					for (int i = 0; i < height; i++)
					{
						byte* ptr2 = ptr + num;
						for (byte* ptr3 = ptr; ptr3 < ptr2; ptr3 += 4)
						{
							int num2;
							*(int*)ptr3 = ((((num2 = *(int*)ptr3) & -16777216) != 0) ? IndexedColorDepths.GetNearestColor(entries, num2 | -16777216) : 0);
						}
						ptr += stride;
					}
					return;
				}
				if (colorDepth < ColorDepth.Depth24Bit)
				{
					for (int i = 0; i < height; i++)
					{
						byte* ptr2 = ptr + num;
						for (byte* ptr3 = ptr; ptr3 < ptr2; ptr3 += 4)
						{
							int num2;
							*(int*)ptr3 = ((((num2 = *(int*)ptr3) & -16777216) != 0) ? ((num2 & 0xF8F8F8) | -16777216) : 0);
						}
						ptr += stride;
					}
					return;
				}
				for (int i = 0; i < height; i++)
				{
					byte* ptr2 = ptr + num;
					for (byte* ptr3 = ptr; ptr3 < ptr2; ptr3 += 4)
					{
						int num2;
						*(int*)ptr3 = ((((num2 = *(int*)ptr3) & -16777216) != 0) ? (num2 | -16777216) : 0);
					}
					ptr += stride;
				}
			}
			finally
			{
				bitmap.UnlockBits(bitmapData);
			}
		}

		internal void DestroyHandle()
		{
			if (handleCreated)
			{
				list = new ArrayList();
				count = 0;
				handleCreated = false;
				keys = new ArrayList();
			}
		}

		internal Image GetImage(int index)
		{
			if (index < 0 || index >= Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			CreateHandle();
			return (Image)list[index];
		}

		internal Image[] ToArray()
		{
			CreateHandle();
			Image[] array = new Image[list.Count];
			list.CopyTo(array);
			return array;
		}

		/// <summary>Adds the specified icon to the <see cref="T:System.Windows.Forms.ImageList" />.</summary>
		/// <param name="value">An <see cref="T:System.Drawing.Icon" /> to add to the list. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null-or-value is not an <see cref="T:System.Drawing.Icon" />. </exception>
		public void Add(Icon value)
		{
			Add(null, value);
		}

		/// <summary>Adds the specified image to the <see cref="T:System.Windows.Forms.ImageList" />.</summary>
		/// <param name="value">A <see cref="T:System.Drawing.Bitmap" /> of the image to add to the list. </param>
		/// <exception cref="T:System.ArgumentNullException">The image being added is null. </exception>
		/// <exception cref="T:System.ArgumentException">The image being added is not a <see cref="T:System.Drawing.Bitmap" />. </exception>
		public void Add(Image value)
		{
			Add(null, value);
		}

		/// <summary>Adds the specified image to the <see cref="T:System.Windows.Forms.ImageList" />, using the specified color to generate the mask.</summary>
		/// <returns>The index of the newly added image, or -1 if the image cannot be added.</returns>
		/// <param name="value">A <see cref="T:System.Drawing.Bitmap" /> of the image to add to the list. </param>
		/// <param name="transparentColor">The <see cref="T:System.Drawing.Color" /> to mask this image. </param>
		/// <exception cref="T:System.ArgumentNullException">The image being added is null. </exception>
		/// <exception cref="T:System.ArgumentException">The image being added is not a <see cref="T:System.Drawing.Bitmap" />. </exception>
		public int Add(Image value, Color transparentColor)
		{
			return AddItem(null, new ImageListItem(value, transparentColor));
		}

		/// <summary>Adds an icon with the specified key to the end of the collection. </summary>
		/// <param name="key">The name of the icon.</param>
		/// <param name="icon">The <see cref="T:System.Drawing.Icon" /> to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="icon" /> is null. </exception>
		public void Add(string key, Icon icon)
		{
			AddItem(key, new ImageListItem(icon));
		}

		/// <summary>Adds an image with the specified key to the end of the collection.</summary>
		/// <param name="key">The name of the image.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null. </exception>
		public void Add(string key, Image image)
		{
			AddItem(key, new ImageListItem(image));
		}

		/// <summary>Adds an array of images to the collection.</summary>
		/// <param name="images">The array of <see cref="T:System.Drawing.Image" /> objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="images" /> is null.</exception>
		public void AddRange(Image[] images)
		{
			if (images == null)
			{
				throw new ArgumentNullException("images");
			}
			for (int i = 0; i < images.Length; i++)
			{
				Add(images[i]);
			}
		}

		/// <summary>Adds an image strip for the specified image to the <see cref="T:System.Windows.Forms.ImageList" />.</summary>
		/// <returns>The index of the newly added image, or -1 if the image cannot be added.</returns>
		/// <param name="value">A <see cref="T:System.Drawing.Bitmap" /> with the images to add. </param>
		/// <exception cref="T:System.ArgumentException">The image being added is null.-or- The image being added is not a <see cref="T:System.Drawing.Bitmap" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">The image cannot be added.-or- The width of image strip being added is 0, or the width is not equal to the existing image width.-or- The image strip height is not equal to existing image height. </exception>
		public int AddStrip(Image value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			int width;
			int width2;
			if ((width = value.Width) == 0 || width % (width2 = imageSize.Width) != 0)
			{
				throw new ArgumentException("Width of image strip must be a positive multiple of ImageSize.Width.", "value");
			}
			if (value.Height != imageSize.Height)
			{
				throw new ArgumentException("Height of image strip must be equal to ImageSize.Height.", "value");
			}
			return AddItem(null, new ImageListItem(value, width / width2));
		}

		/// <summary>Removes all the images and masks from the <see cref="T:System.Windows.Forms.ImageList" />.</summary>
		public void Clear()
		{
			list.Clear();
			if (handleCreated)
			{
				count = 0;
			}
			keys.Clear();
		}

		/// <summary>Not supported. The <see cref="M:System.Collections.IList.Contains(System.Object)" /> method indicates whether a specified object is contained in the list.</summary>
		/// <returns>true if the image is found in the list; otherwise, false.</returns>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to find in the list. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Contains(Image image)
		{
			throw new NotSupportedException();
		}

		/// <summary>Determines if the collection contains an image with the specified key.</summary>
		/// <returns>true to indicate an image with the specified key is contained in the collection; otherwise, false. </returns>
		/// <param name="key">The key of the image to search for.</param>
		public bool ContainsKey(string key)
		{
			return IndexOfKey(key) != -1;
		}

		/// <summary>Returns an enumerator that can be used to iterate through the item collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the item collection.</returns>
		public IEnumerator GetEnumerator()
		{
			Image[] array = new Image[Count];
			if (array.Length != 0)
			{
				CreateHandle();
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = (Image)((Image)list[i]).Clone();
				}
			}
			return array.GetEnumerator();
		}

		/// <summary>Not supported. The <see cref="M:System.Collections.IList.IndexOf(System.Object)" /> method returns the index of a specified object in the list.</summary>
		/// <returns>The index of the image in the list.</returns>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to find in the list. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int IndexOf(Image image)
		{
			throw new NotSupportedException();
		}

		/// <summary>Determines the index of the first occurrence of an image with the specified key in the collection.</summary>
		/// <returns>The zero-based index of the first occurrence of an image with the specified key in the collection, if found; otherwise, -1.</returns>
		/// <param name="key">The key of the image to retrieve the index for.</param>
		public int IndexOfKey(string key)
		{
			if (key != null && key.Length != 0)
			{
				if (lastKeyIndex >= 0 && lastKeyIndex < Count && CompareKeys((string)keys[lastKeyIndex], key))
				{
					return lastKeyIndex;
				}
				for (int i = 0; i < Count; i++)
				{
					if (CompareKeys((string)keys[i], key))
					{
						return lastKeyIndex = i;
					}
				}
			}
			return lastKeyIndex = -1;
		}

		/// <summary>Not supported. The <see cref="M:System.Collections.IList.Remove(System.Object)" /> method removes a specified object from the list.</summary>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to remove from the list. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Remove(Image image)
		{
			throw new NotSupportedException();
		}

		/// <summary>Removes an image from the list.</summary>
		/// <param name="index">The index of the image to remove. </param>
		/// <exception cref="T:System.InvalidOperationException">The image cannot be removed. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index value was less than 0.-or- The index value is greater than or equal to the <see cref="P:System.Windows.Forms.ImageList.ImageCollection.Count" /> of images. </exception>
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			CreateHandle();
			list.RemoveAt(index);
			keys.RemoveAt(index);
			if (this.Changed != null)
			{
				this.Changed(this, EventArgs.Empty);
			}
		}

		/// <summary>Removes the image with the specified key from the collection.</summary>
		/// <param name="key">The key of the image to remove from the collection.</param>
		public void RemoveByKey(string key)
		{
			int index;
			if ((index = IndexOfKey(key)) != -1)
			{
				RemoveAt(index);
			}
		}

		/// <summary>Sets the key for an image in the collection.</summary>
		/// <param name="index">The zero-based index of an image in the collection.</param>
		/// <param name="name">The name of the image to be set as the image key.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The specified index is less than 0 or greater than or equal to <see cref="P:System.Windows.Forms.ImageList.ImageCollection.Count" />.</exception>
		public void SetKeyName(int index, string name)
		{
			if (index < 0 || index >= Count)
			{
				throw new IndexOutOfRangeException();
			}
			keys[index] = name;
		}
	}

	private const ColorDepth DefaultColorDepth = ColorDepth.Depth8Bit;

	private static readonly Size DefaultImageSize = new Size(16, 16);

	private static readonly Color DefaultTransparentColor = Color.Transparent;

	private object tag;

	private readonly ImageCollection images;

	private static object RecreateHandleEvent;

	/// <summary>Gets the color depth of the image list.</summary>
	/// <returns>The number of available colors for the image. In the .NET Framework version 1.0, the default is <see cref="F:System.Windows.Forms.ColorDepth.Depth4Bit" />. In the .NET Framework version 1.1 or later, the default is <see cref="F:System.Windows.Forms.ColorDepth.Depth8Bit" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The color depth is not a valid <see cref="T:System.Windows.Forms.ColorDepth" /> enumeration value. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public ColorDepth ColorDepth
	{
		get
		{
			return images.ColorDepth;
		}
		set
		{
			images.ColorDepth = value;
		}
	}

	/// <summary>Gets the handle of the image list object.</summary>
	/// <returns>The handle for the image list. The default is null.</returns>
	/// <exception cref="T:System.InvalidOperationException">Creating the handle for the <see cref="T:System.Windows.Forms.ImageList" /> failed.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public IntPtr Handle => images.Handle;

	/// <summary>Gets a value indicating whether the underlying Win32 handle has been created.</summary>
	/// <returns>true if the <see cref="P:System.Windows.Forms.ImageList.Handle" /> has been created; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(false)]
	public bool HandleCreated => images.HandleCreated;

	/// <summary>Gets the <see cref="T:System.Windows.Forms.ImageList.ImageCollection" /> for this image list.</summary>
	/// <returns>The collection of images.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[MergableProperty(false)]
	[DefaultValue(null)]
	public ImageCollection Images => images;

	/// <summary>Gets or sets the size of the images in the image list.</summary>
	/// <returns>The <see cref="T:System.Drawing.Size" /> that defines the height and width, in pixels, of the images in the list. The default size is 16 by 16. The maximum size is 256 by 256.</returns>
	/// <exception cref="T:System.ArgumentException">The value assigned is equal to <see cref="P:System.Drawing.Size.IsEmpty" />.-or- The value of the height or width is less than or equal to 0.-or- The value of the height or width is greater than 256. </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The new size has a dimension less than 0 or greater than 256.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	public Size ImageSize
	{
		get
		{
			return images.ImageSize;
		}
		set
		{
			images.ImageSize = value;
		}
	}

	/// <summary>Gets the <see cref="T:System.Windows.Forms.ImageListStreamer" /> associated with this image list.</summary>
	/// <returns>null if the image list is empty; otherwise, a <see cref="T:System.Windows.Forms.ImageListStreamer" /> for this <see cref="T:System.Windows.Forms.ImageList" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(false)]
	[DefaultValue(null)]
	public ImageListStreamer ImageStream
	{
		get
		{
			return images.ImageStream;
		}
		set
		{
			images.ImageStream = value;
		}
	}

	/// <summary>Gets or sets an object that contains additional data about the <see cref="T:System.Windows.Forms.ImageList" />.</summary>
	/// <returns>An <see cref="T:System.Object" /> that contains additional data about the <see cref="T:System.Windows.Forms.ImageList" />.</returns>
	/// <filterpriority>1</filterpriority>
	[Bindable(true)]
	[TypeConverter(typeof(StringConverter))]
	[Localizable(false)]
	[DefaultValue(null)]
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

	/// <summary>Gets or sets the color to treat as transparent.</summary>
	/// <returns>One of the <see cref="T:System.Drawing.Color" /> values. The default is Transparent.</returns>
	/// <filterpriority>1</filterpriority>
	public Color TransparentColor
	{
		get
		{
			return images.TransparentColor;
		}
		set
		{
			images.TransparentColor = value;
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ImageList.Handle" /> is recreated.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(false)]
	public event EventHandler RecreateHandle
	{
		add
		{
			base.Events.AddHandler(RecreateHandleEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(RecreateHandleEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ImageList" /> class with default values for <see cref="P:System.Windows.Forms.ImageList.ColorDepth" />, <see cref="P:System.Windows.Forms.ImageList.ImageSize" />, and <see cref="P:System.Windows.Forms.ImageList.TransparentColor" />.</summary>
	public ImageList()
	{
		images = new ImageCollection(this);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ImageList" /> class, associating it with a container.</summary>
	/// <param name="container">An object implementing <see cref="T:System.ComponentModel.IContainer" /> to associate with this instance of <see cref="T:System.Windows.Forms.ImageList" />. </param>
	public ImageList(IContainer container)
		: this()
	{
		container.Add(this);
	}

	static ImageList()
	{
		RecreateHandle = new object();
	}

	private void OnRecreateHandle()
	{
		((EventHandler)base.Events[RecreateHandle])?.Invoke(this, EventArgs.Empty);
	}

	internal bool ShouldSerializeTransparentColor()
	{
		return TransparentColor != Color.LightGray;
	}

	internal bool ShouldSerializeColorDepth()
	{
		return images.Empty;
	}

	internal bool ShouldSerializeImageSize()
	{
		return images.Empty;
	}

	internal void ResetColorDepth()
	{
		ColorDepth = ColorDepth.Depth8Bit;
	}

	internal void ResetImageSize()
	{
		ImageSize = DefaultImageSize;
	}

	internal void ResetTransparentColor()
	{
		TransparentColor = Color.LightGray;
	}

	/// <summary>Draws the image indicated by the specified index on the specified <see cref="T:System.Drawing.Graphics" /> at the given location.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
	/// <param name="pt">The location defined by a <see cref="T:System.Drawing.Point" /> at which to draw the image. </param>
	/// <param name="index">The index of the image in the <see cref="T:System.Windows.Forms.ImageList" /> to draw. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The index is less than 0.-or- The index is greater than or equal to the count of images in the image list. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Draw(Graphics g, Point pt, int index)
	{
		Draw(g, pt.X, pt.Y, index);
	}

	/// <summary>Draws the image indicated by the given index on the specified <see cref="T:System.Drawing.Graphics" /> at the specified location.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
	/// <param name="x">The horizontal position at which to draw the image. </param>
	/// <param name="y">The vertical position at which to draw the image. </param>
	/// <param name="index">The index of the image in the <see cref="T:System.Windows.Forms.ImageList" /> to draw. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The index is less than 0.-or- The index is greater than or equal to the count of images in the image list. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Draw(Graphics g, int x, int y, int index)
	{
		g.DrawImage(images.GetImage(index), x, y);
	}

	/// <summary>Draws the image indicated by the given index on the specified <see cref="T:System.Drawing.Graphics" /> using the specified location and size.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
	/// <param name="x">The horizontal position at which to draw the image. </param>
	/// <param name="y">The vertical position at which to draw the image. </param>
	/// <param name="width">The width, in pixels, of the destination image. </param>
	/// <param name="height">The height, in pixels, of the destination image. </param>
	/// <param name="index">The index of the image in the <see cref="T:System.Windows.Forms.ImageList" /> to draw. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The index is less than 0.-or- The index is greater than or equal to the count of images in the image list. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Draw(Graphics g, int x, int y, int width, int height, int index)
	{
		g.DrawImage(images.GetImage(index), x, y, width, height);
	}

	/// <summary>Returns a string that represents the current <see cref="T:System.Windows.Forms.ImageList" />.</summary>
	/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.ImageList" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override string ToString()
	{
		return base.ToString() + " Images.Count: " + images.Count + ", ImageSize: " + ImageSize.ToString();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			images.DestroyHandle();
		}
		base.Dispose(disposing);
	}
}
