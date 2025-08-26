using System.ComponentModel;
using System.Drawing;
using System.Security;

namespace System.Windows.Forms.VisualStyles;

/// <summary>Provides methods for drawing and getting information about a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" />. This class cannot be inherited.</summary>
public sealed class VisualStyleRenderer
{
	private class ThemeHandleManager
	{
		public VisualStyleRenderer VisualStyleRenderer;

		~ThemeHandleManager()
		{
			if (!(VisualStyleRenderer.theme == IntPtr.Zero))
			{
				VisualStyles.UxThemeCloseThemeData(VisualStyleRenderer.theme);
			}
		}
	}

	private string class_name;

	private int part;

	private int state;

	private IntPtr theme;

	private int last_hresult;

	private ThemeHandleManager theme_handle_manager = new ThemeHandleManager();

	/// <summary>Gets the class name of the current visual style element.</summary>
	/// <returns>A string that identifies the class of the current visual style element.</returns>
	public string Class => class_name;

	/// <summary>Gets a unique identifier for the current class of visual style elements.</summary>
	/// <returns>An <see cref="T:System.IntPtr" /> that identifies a set of data that defines the class of elements specified by <see cref="P:System.Windows.Forms.VisualStyles.VisualStyleRenderer.Class" />. </returns>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public IntPtr Handle => theme;

	/// <summary>Gets the last error code returned by the native visual styles (UxTheme) API methods encapsulated by the <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleRenderer" /> class.</summary>
	/// <returns>A value specifying the last error code returned by the native visual styles API methods that this class encapsulates.</returns>
	public int LastHResult => last_hresult;

	/// <summary>Gets the part of the current visual style element.</summary>
	/// <returns>A value that specifies the part of the current visual style element.</returns>
	public int Part => part;

	/// <summary>Gets the state of the current visual style element.</summary>
	/// <returns>A value that identifies the state of the current visual style element.</returns>
	public int State => state;

	/// <summary>Gets a value specifying whether the operating system is using visual styles to draw controls.</summary>
	/// <returns>true if the operating system supports visual styles, the user has enabled visual styles in the operating system, and visual styles are applied to the client area of application windows; otherwise, false.</returns>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static bool IsSupported
	{
		get
		{
			if (!VisualStyleInformation.IsEnabledByUser)
			{
				return false;
			}
			if (Application.VisualStyleState == VisualStyleState.ClientAndNonClientAreasEnabled || Application.VisualStyleState == VisualStyleState.ClientAreaEnabled)
			{
				return true;
			}
			return false;
		}
	}

	internal static IVisualStyles VisualStyles => VisualStylesEngine.Instance;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleRenderer" /> class using the given class, part, and state values.</summary>
	/// <param name="className">The class name of the element that this <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleRenderer" /> will represent.</param>
	/// <param name="part">The part of the element that this <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleRenderer" /> will represent.</param>
	/// <param name="state">The state of the element that this <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleRenderer" /> will represent.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	/// <exception cref="T:System.ArgumentException">The combination of <paramref name="className" />, <paramref name="part" />, and <paramref name="state" /> is not defined by the current visual style.</exception>
	public VisualStyleRenderer(string className, int part, int state)
	{
		theme_handle_manager.VisualStyleRenderer = this;
		SetParameters(className, part, state);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleRenderer" /> class using the given <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" />.</summary>
	/// <param name="element">A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that this <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleRenderer" /> will represent.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="element" /> is not defined by the current visual style.</exception>
	public VisualStyleRenderer(VisualStyleElement element)
		: this(element.ClassName, element.Part, element.State)
	{
	}

	/// <summary>Determines whether the specified visual style element is defined by the current visual style.</summary>
	/// <returns>true if the combination of the <see cref="P:System.Windows.Forms.VisualStyles.VisualStyleElement.ClassName" /> and <see cref="P:System.Windows.Forms.VisualStyles.VisualStyleElement.Part" /> properties of <paramref name="element" /> are defined; otherwise, false. </returns>
	/// <param name="element">A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> whose class and part combination will be verified.</param>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	public static bool IsElementDefined(VisualStyleElement element)
	{
		if (!IsSupported)
		{
			throw new InvalidOperationException("Visual Styles are not enabled.");
		}
		if (IsElementKnownToBeSupported(element.ClassName, element.Part, element.State))
		{
			return true;
		}
		IntPtr intPtr = VisualStyles.UxThemeOpenThemeData(IntPtr.Zero, element.ClassName);
		if (intPtr == IntPtr.Zero)
		{
			return false;
		}
		bool result = VisualStyles.UxThemeIsThemePartDefined(intPtr, element.Part);
		VisualStyles.UxThemeCloseThemeData(intPtr);
		return result;
	}

	/// <summary>Draws the background image of the current visual style element within the specified bounding rectangle.</summary>
	/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> used to draw the background image.</param>
	/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> in which the background image is drawn.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dc" /> is null.</exception>
	public void DrawBackground(IDeviceContext dc, Rectangle bounds)
	{
		if (dc == null)
		{
			throw new ArgumentNullException("dc");
		}
		last_hresult = VisualStyles.UxThemeDrawThemeBackground(theme, dc, part, state, bounds);
	}

	/// <summary>Draws the background image of the current visual style element within the specified bounding rectangle and clipped to the specified clipping rectangle.</summary>
	/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> used to draw the background image.</param>
	/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> in which the background image is drawn.</param>
	/// <param name="clipRectangle">A <see cref="T:System.Drawing.Rectangle" /> that defines a clipping rectangle for the drawing operation. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dc" /> is null.</exception>
	public void DrawBackground(IDeviceContext dc, Rectangle bounds, Rectangle clipRectangle)
	{
		if (dc == null)
		{
			throw new ArgumentNullException("dc");
		}
		last_hresult = VisualStyles.UxThemeDrawThemeBackground(theme, dc, part, state, bounds, clipRectangle);
	}

	/// <summary>Draws one or more edges of the specified bounding rectangle.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the interior of the <paramref name="bounds" /> parameter, minus the edges that were drawn.</returns>
	/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> used to draw the edges.</param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> whose bounds define the edges to draw.</param>
	/// <param name="edges">A bitwise combination of the <see cref="T:System.Windows.Forms.VisualStyles.Edges" /> values.</param>
	/// <param name="style">A bitwise combination of the <see cref="T:System.Windows.Forms.VisualStyles.EdgeStyle" /> values.</param>
	/// <param name="effects">A bitwise combination of the <see cref="T:System.Windows.Forms.VisualStyles.EdgeEffects" /> values.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dc" /> is null.</exception>
	public Rectangle DrawEdge(IDeviceContext dc, Rectangle bounds, Edges edges, EdgeStyle style, EdgeEffects effects)
	{
		if (dc == null)
		{
			throw new ArgumentNullException("dc");
		}
		last_hresult = VisualStyles.UxThemeDrawThemeEdge(theme, dc, part, state, bounds, edges, style, effects, out var result);
		return result;
	}

	/// <summary>Draws the image from the specified <see cref="T:System.Windows.Forms.ImageList" /> within the specified bounds.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the image.</param>
	/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> in which the image is drawn.</param>
	/// <param name="imageList">An <see cref="T:System.Windows.Forms.ImageList" /> that contains the <see cref="T:System.Drawing.Image" /> to draw.</param>
	/// <param name="imageIndex">The index of the <see cref="T:System.Drawing.Image" /> within <paramref name="imageList" /> to draw.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="g" /> or <paramref name="image" /> is null.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="imageIndex" /> is less than 0, or greater than or equal to the number of images in<paramref name=" imageList" />.</exception>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void DrawImage(Graphics g, Rectangle bounds, ImageList imageList, int imageIndex)
	{
		if (g == null)
		{
			throw new ArgumentNullException("g");
		}
		if (imageIndex < 0 || imageIndex > imageList.Images.Count - 1)
		{
			throw new ArgumentOutOfRangeException("imageIndex");
		}
		if (imageList.Images[imageIndex] == null)
		{
			throw new ArgumentNullException("imageIndex");
		}
		g.DrawImage(imageList.Images[imageIndex], bounds);
	}

	/// <summary>Draws the specified image within the specified bounds.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the image.</param>
	/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> in which the image is drawn.</param>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="g" /> or <paramref name="image" /> is null.</exception>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void DrawImage(Graphics g, Rectangle bounds, Image image)
	{
		if (g == null)
		{
			throw new ArgumentNullException("g");
		}
		if (image == null)
		{
			throw new ArgumentNullException("image");
		}
		g.DrawImage(image, bounds);
	}

	/// <summary>Draws the background of a control's parent in the specified area.</summary>
	/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> used to draw the background of the parent of <paramref name="childControl" />. This object typically belongs to the child control.</param>
	/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> in which to draw the parent control's background. This rectangle should be inside the child control’s bounds.</param>
	/// <param name="childControl">The control whose parent's background will be drawn.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dc" /> is null.</exception>
	public void DrawParentBackground(IDeviceContext dc, Rectangle bounds, Control childControl)
	{
		if (dc == null)
		{
			throw new ArgumentNullException("dc");
		}
		last_hresult = VisualStyles.UxThemeDrawThemeParentBackground(dc, bounds, childControl);
	}

	/// <summary>Draws text in the specified bounding rectangle with the option of displaying disabled text and applying other text formatting.</summary>
	/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> used to draw the text.</param>
	/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> in which to draw the text.</param>
	/// <param name="textToDraw">The text to draw.</param>
	/// <param name="drawDisabled">true to draw grayed-out text; otherwise, false.</param>
	/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dc" /> is null.</exception>
	public void DrawText(IDeviceContext dc, Rectangle bounds, string textToDraw, bool drawDisabled, TextFormatFlags flags)
	{
		if (dc == null)
		{
			throw new ArgumentNullException("dc");
		}
		last_hresult = VisualStyles.UxThemeDrawThemeText(theme, dc, part, state, textToDraw, flags, bounds);
	}

	/// <summary>Draws text in the specified bounds with the option of displaying disabled text.</summary>
	/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> used to draw the text.</param>
	/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> in which to draw the text.</param>
	/// <param name="textToDraw">The text to draw.</param>
	/// <param name="drawDisabled">true to draw grayed-out text; otherwise, false.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dc" /> is null.</exception>
	public void DrawText(IDeviceContext dc, Rectangle bounds, string textToDraw, bool drawDisabled)
	{
		DrawText(dc, bounds, textToDraw, drawDisabled, TextFormatFlags.Left);
	}

	/// <summary>Draws text in the specified bounds using default formatting.</summary>
	/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> used to draw the text.</param>
	/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> in which to draw the text.</param>
	/// <param name="textToDraw">The text to draw.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dc" /> is null.</exception>
	public void DrawText(IDeviceContext dc, Rectangle bounds, string textToDraw)
	{
		DrawText(dc, bounds, textToDraw, drawDisabled: false, TextFormatFlags.Left);
	}

	/// <summary>Returns the content area for the background of the current visual style element.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that contains the content area for the background of the current visual style element.</returns>
	/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> this operation will use.</param>
	/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the entire background area of the current visual style element.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dc" /> is null.</exception>
	public Rectangle GetBackgroundContentRectangle(IDeviceContext dc, Rectangle bounds)
	{
		if (dc == null)
		{
			throw new ArgumentNullException("dc");
		}
		last_hresult = VisualStyles.UxThemeGetThemeBackgroundContentRect(theme, dc, part, state, bounds, out var result);
		return result;
	}

	/// <summary>Returns the entire background area for the current visual style element.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that contains the entire background area of the current visual style element.</returns>
	/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> this operation will use.</param>
	/// <param name="contentBounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the content area of the current visual style element.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dc" /> is null.</exception>
	public Rectangle GetBackgroundExtent(IDeviceContext dc, Rectangle contentBounds)
	{
		if (dc == null)
		{
			throw new ArgumentNullException("dc");
		}
		last_hresult = VisualStyles.UxThemeGetThemeBackgroundExtent(theme, dc, part, state, contentBounds, out var result);
		return result;
	}

	/// <summary>Returns the region for the background of the current visual style element.</summary>
	/// <returns>The <see cref="T:System.Drawing.Region" /> that contains the background of the current visual style element.</returns>
	/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> this operation will use.</param>
	/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the entire background area of the current visual style element.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dc" /> is null.</exception>
	[SuppressUnmanagedCodeSecurity]
	public Region GetBackgroundRegion(IDeviceContext dc, Rectangle bounds)
	{
		if (dc == null)
		{
			throw new ArgumentNullException("dc");
		}
		last_hresult = VisualStyles.UxThemeGetThemeBackgroundRegion(theme, dc, part, state, bounds, out var result);
		return result;
	}

	/// <summary>Returns the value of the specified Boolean property for the current visual style element.</summary>
	/// <returns>true if the property specified by the <paramref name="prop" /> parameter is true for the current visual style element; otherwise, false.</returns>
	/// <param name="prop">One of the <see cref="T:System.Windows.Forms.VisualStyles.BooleanProperty" /> values that specifies which property value to retrieve for the current visual style element.</param>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="prop" /> is not one of the <see cref="T:System.Windows.Forms.VisualStyles.BooleanProperty" /> values.</exception>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public bool GetBoolean(BooleanProperty prop)
	{
		if (!Enum.IsDefined(typeof(BooleanProperty), prop))
		{
			throw new InvalidEnumArgumentException("prop", (int)prop, typeof(BooleanProperty));
		}
		last_hresult = VisualStyles.UxThemeGetThemeBool(theme, part, state, prop, out var result);
		return result;
	}

	/// <summary>Returns the value of the specified color property for the current visual style element.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that contains the value of the property specified by the <paramref name="prop" /> parameter for the current visual style element.</returns>
	/// <param name="prop">One of the <see cref="T:System.Windows.Forms.VisualStyles.ColorProperty" /> values that specifies which property value to retrieve for the current visual style element.</param>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="prop" /> is not one of the <see cref="T:System.Windows.Forms.VisualStyles.ColorProperty" /> values.</exception>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public Color GetColor(ColorProperty prop)
	{
		if (!Enum.IsDefined(typeof(ColorProperty), prop))
		{
			throw new InvalidEnumArgumentException("prop", (int)prop, typeof(ColorProperty));
		}
		last_hresult = VisualStyles.UxThemeGetThemeColor(theme, part, state, prop, out var result);
		return result;
	}

	/// <summary>Returns the value of the specified enumerated type property for the current visual style element.</summary>
	/// <returns>The integer value of the property specified by the <paramref name="prop" /> parameter for the current visual style element.</returns>
	/// <param name="prop">One of the <see cref="T:System.Windows.Forms.VisualStyles.EnumProperty" /> values that specifies which property value to retrieve for the current visual style element.</param>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="prop" /> is not one of the <see cref="T:System.Windows.Forms.VisualStyles.EnumProperty" /> values.</exception>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public int GetEnumValue(EnumProperty prop)
	{
		if (!Enum.IsDefined(typeof(EnumProperty), prop))
		{
			throw new InvalidEnumArgumentException("prop", (int)prop, typeof(EnumProperty));
		}
		last_hresult = VisualStyles.UxThemeGetThemeEnumValue(theme, part, state, prop, out var result);
		return result;
	}

	/// <summary>Returns the value of the specified file name property for the current visual style element.</summary>
	/// <returns>A <see cref="T:System.String" /> that contains the value of the property specified by the <paramref name="prop" /> parameter for the current visual style element.</returns>
	/// <param name="prop">One of the <see cref="T:System.Windows.Forms.VisualStyles.FilenameProperty" /> values that specifies which property value to retrieve for the current visual style element.</param>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="prop" /> is not one of the <see cref="T:System.Windows.Forms.VisualStyles.FilenameProperty" /> values.</exception>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public string GetFilename(FilenameProperty prop)
	{
		if (!Enum.IsDefined(typeof(FilenameProperty), prop))
		{
			throw new InvalidEnumArgumentException("prop", (int)prop, typeof(FilenameProperty));
		}
		last_hresult = VisualStyles.UxThemeGetThemeFilename(theme, part, state, prop, out var result);
		return result;
	}

	/// <summary>Returns the value of the specified font property for the current visual style element.</summary>
	/// <returns>A <see cref="T:System.Drawing.Font" /> that contains the value of the property specified by the <paramref name="prop" /> parameter for the current visual style element.</returns>
	/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> this operation will use.</param>
	/// <param name="prop">One of the <see cref="T:System.Windows.Forms.VisualStyles.FontProperty" /> values that specifies which property value to retrieve for the current visual style element.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dc" /> is null.</exception>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="prop" /> is not one of the <see cref="T:System.Windows.Forms.VisualStyles.FontProperty" /> values.</exception>
	[System.MonoTODO("I can't get MS's to return anything but null, so I can't really get this one right")]
	public Font GetFont(IDeviceContext dc, FontProperty prop)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns the value of the specified integer property for the current visual style element.</summary>
	/// <returns>The integer value of the property specified by the <paramref name="prop" /> parameter for the current visual style element.</returns>
	/// <param name="prop">One of the <see cref="T:System.Windows.Forms.VisualStyles.IntegerProperty" /> values that specifies which property value to retrieve for the current visual style element.</param>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="prop" /> is not one of the <see cref="T:System.Windows.Forms.VisualStyles.IntegerProperty" /> values.</exception>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public int GetInteger(IntegerProperty prop)
	{
		if (!Enum.IsDefined(typeof(IntegerProperty), prop))
		{
			throw new InvalidEnumArgumentException("prop", (int)prop, typeof(IntegerProperty));
		}
		last_hresult = VisualStyles.UxThemeGetThemeInt(theme, part, state, prop, out var result);
		return result;
	}

	/// <summary>Returns the value of the specified margins property for the current visual style element.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> that contains the value of the property specified by the <paramref name="prop" /> parameter for the current visual style element.</returns>
	/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> this operation will use.</param>
	/// <param name="prop">One of the <see cref="T:System.Windows.Forms.VisualStyles.MarginProperty" /> values that specifies which property value to retrieve for the current visual style element.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dc" /> is null.</exception>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="prop" /> is not one of the <see cref="T:System.Windows.Forms.VisualStyles.MarginProperty" /> values.</exception>
	[System.MonoTODO("MS's causes a PInvokeStackUnbalance on me, so this is not verified against MS.")]
	public Padding GetMargins(IDeviceContext dc, MarginProperty prop)
	{
		if (dc == null)
		{
			throw new ArgumentNullException("dc");
		}
		if (!Enum.IsDefined(typeof(MarginProperty), prop))
		{
			throw new InvalidEnumArgumentException("prop", (int)prop, typeof(MarginProperty));
		}
		last_hresult = VisualStyles.UxThemeGetThemeMargins(theme, dc, part, state, prop, out var result);
		return result;
	}

	/// <summary>Returns the value of the specified size property of the current visual style part using the specified drawing bounds.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that contains the size specified by the <paramref name="type" /> parameter for the current visual style part.</returns>
	/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> this operation will use.</param>
	/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the area in which the part will be drawn.</param>
	/// <param name="type">One of the <see cref="T:System.Windows.Forms.VisualStyles.ThemeSizeType" /> values that specifies which size value to retrieve for the part.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dc" /> is null.</exception>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="prop" /> is not one of the <see cref="T:System.Windows.Forms.VisualStyles.ThemeSizeType" /> values.</exception>
	public Size GetPartSize(IDeviceContext dc, Rectangle bounds, ThemeSizeType type)
	{
		if (dc == null)
		{
			throw new ArgumentNullException("dc");
		}
		if (!Enum.IsDefined(typeof(ThemeSizeType), type))
		{
			throw new InvalidEnumArgumentException("prop", (int)type, typeof(ThemeSizeType));
		}
		last_hresult = VisualStyles.UxThemeGetThemePartSize(theme, dc, part, state, bounds, type, out var result);
		return result;
	}

	/// <summary>Returns the value of the specified size property of the current visual style part.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that contains the size specified by the <paramref name="type" /> parameter for the current visual style part. </returns>
	/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> this operation will use.</param>
	/// <param name="type">One of the <see cref="T:System.Windows.Forms.VisualStyles.ThemeSizeType" /> values that specifies which size value to retrieve for the part.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dc" /> is null.</exception>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="prop" /> is not one of the <see cref="T:System.Windows.Forms.VisualStyles.ThemeSizeType" /> values.</exception>
	public Size GetPartSize(IDeviceContext dc, ThemeSizeType type)
	{
		if (dc == null)
		{
			throw new ArgumentNullException("dc");
		}
		if (!Enum.IsDefined(typeof(ThemeSizeType), type))
		{
			throw new InvalidEnumArgumentException("prop", (int)type, typeof(ThemeSizeType));
		}
		last_hresult = VisualStyles.UxThemeGetThemePartSize(theme, dc, part, state, type, out var result);
		return result;
	}

	/// <summary>Returns the value of the specified point property for the current visual style element.</summary>
	/// <returns>A <see cref="T:System.Drawing.Point" /> that contains the value of the property specified by the <paramref name="prop" /> parameter for the current visual style element.</returns>
	/// <param name="prop">One of the <see cref="T:System.Windows.Forms.VisualStyles.PointProperty" /> values that specifies which property value to retrieve for the current visual style element.</param>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="prop" /> is not one of the <see cref="T:System.Windows.Forms.VisualStyles.PointProperty" /> values.</exception>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public Point GetPoint(PointProperty prop)
	{
		if (!Enum.IsDefined(typeof(PointProperty), prop))
		{
			throw new InvalidEnumArgumentException("prop", (int)prop, typeof(PointProperty));
		}
		last_hresult = VisualStyles.UxThemeGetThemePosition(theme, part, state, prop, out var result);
		return result;
	}

	/// <summary>Returns the value of the specified string property for the current visual style element.</summary>
	/// <returns>A <see cref="T:System.String" /> that contains the value of the property specified by the <paramref name="prop" /> parameter for the current visual style element.</returns>
	/// <param name="prop">One of the <see cref="T:System.Windows.Forms.VisualStyles.StringProperty" /> values that specifies which property value to retrieve for the current visual style element.</param>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="prop" /> is not one of the <see cref="T:System.Windows.Forms.VisualStyles.StringProperty" /> values.</exception>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[System.MonoTODO("Can't find any values that return anything on MS to test against")]
	public string GetString(StringProperty prop)
	{
		if (!Enum.IsDefined(typeof(StringProperty), prop))
		{
			throw new InvalidEnumArgumentException("prop", (int)prop, typeof(StringProperty));
		}
		last_hresult = VisualStyles.UxThemeGetThemeString(theme, part, state, prop, out var result);
		return result;
	}

	/// <summary>Returns the size and location of the specified string when drawn with the font of the current visual style element within the specified initial bounding rectangle.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that contains the area required to fit the rendered text. </returns>
	/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> this operation will use.</param>
	/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> used to control the flow and wrapping of the text.</param>
	/// <param name="textToDraw">The string to measure.</param>
	/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dc" /> is null.</exception>
	public Rectangle GetTextExtent(IDeviceContext dc, Rectangle bounds, string textToDraw, TextFormatFlags flags)
	{
		if (dc == null)
		{
			throw new ArgumentNullException("dc");
		}
		last_hresult = VisualStyles.UxThemeGetThemeTextExtent(theme, dc, part, state, textToDraw, flags, bounds, out var result);
		return result;
	}

	/// <summary>Returns the size and location of the specified string when drawn with the font of the current visual style element.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that contains the area required to fit the rendered text. </returns>
	/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> this operation will use.</param>
	/// <param name="textToDraw">The string to measure.</param>
	/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dc" /> is null.</exception>
	public Rectangle GetTextExtent(IDeviceContext dc, string textToDraw, TextFormatFlags flags)
	{
		if (dc == null)
		{
			throw new ArgumentNullException("dc");
		}
		last_hresult = VisualStyles.UxThemeGetThemeTextExtent(theme, dc, part, state, textToDraw, flags, out var result);
		return result;
	}

	/// <summary>Retrieves information about the font specified by the current visual style element.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.TextMetrics" /> that provides information about the font specified by the current visual style element. </returns>
	/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> this operation will use.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dc" /> is null.</exception>
	public TextMetrics GetTextMetrics(IDeviceContext dc)
	{
		if (dc == null)
		{
			throw new ArgumentNullException("dc", "dc cannot be null.");
		}
		last_hresult = VisualStyles.UxThemeGetThemeTextMetrics(theme, dc, part, state, out var result);
		return result;
	}

	/// <summary>Returns a hit test code indicating whether the point is contained in the background of the current visual style element and within the specified region.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.HitTestCode" /> that describes where <paramref name="pt" /> is located in the background of the current visual style element.</returns>
	/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> this operation will use.</param>
	/// <param name="backgroundRectangle">A <see cref="T:System.Drawing.Rectangle" /> that contains the background of the current visual style element.</param>
	/// <param name="hRgn">A Windows handle to a <see cref="T:System.Drawing.Region" /> that specifies the bounds of the hit test area within the background.</param>
	/// <param name="pt">The <see cref="T:System.Drawing.Point" /> to test.</param>
	/// <param name="options">A bitwise combination of the <see cref="T:System.Windows.Forms.VisualStyles.HitTestOptions" /> values.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dc" /> is null.</exception>
	public HitTestCode HitTestBackground(IDeviceContext dc, Rectangle backgroundRectangle, IntPtr hRgn, Point pt, HitTestOptions options)
	{
		if (dc == null)
		{
			throw new ArgumentNullException("dc");
		}
		last_hresult = VisualStyles.UxThemeHitTestThemeBackground(theme, dc, part, state, options, backgroundRectangle, hRgn, pt, out var result);
		return result;
	}

	/// <summary>Returns a hit test code indicating whether the point is contained in the background of the current visual style element and within the specified bounds.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.HitTestCode" /> that describes where <paramref name="pt" /> is located in the background of the current visual style element, if at all.</returns>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> this operation will use.</param>
	/// <param name="backgroundRectangle">A <see cref="T:System.Drawing.Rectangle" /> that contains the background of the current visual style element.</param>
	/// <param name="region">A <see cref="T:System.Drawing.Region" /> that specifies the bounds of the hit test area within the background.</param>
	/// <param name="pt">The <see cref="T:System.Drawing.Point" /> to test.</param>
	/// <param name="options">A bitwise combination of the <see cref="T:System.Windows.Forms.VisualStyles.HitTestOptions" /> values.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="g" /> is null.</exception>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public HitTestCode HitTestBackground(Graphics g, Rectangle backgroundRectangle, Region region, Point pt, HitTestOptions options)
	{
		if (g == null)
		{
			throw new ArgumentNullException("g");
		}
		IntPtr hrgn = region.GetHrgn(g);
		return HitTestBackground(g, backgroundRectangle, hrgn, pt, options);
	}

	/// <summary>Returns a hit test code indicating whether a point is contained in the background of the current visual style element.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.HitTestCode" /> that describes where <paramref name="pt" /> is located in the background of the current visual style element.</returns>
	/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> this operation will use.</param>
	/// <param name="backgroundRectangle">A <see cref="T:System.Drawing.Rectangle" /> that contains the background of the current visual style element.</param>
	/// <param name="pt">The <see cref="T:System.Drawing.Point" /> to test.</param>
	/// <param name="options">A bitwise combination of the <see cref="T:System.Windows.Forms.VisualStyles.HitTestOptions" /> values.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dc" /> is null.</exception>
	public HitTestCode HitTestBackground(IDeviceContext dc, Rectangle backgroundRectangle, Point pt, HitTestOptions options)
	{
		return HitTestBackground(dc, backgroundRectangle, IntPtr.Zero, pt, options);
	}

	/// <summary>Indicates whether the background of the current visual style element has any semitransparent or alpha-blended pieces.</summary>
	/// <returns>true if the background of the current visual style element has any semitransparent or alpha-blended pieces; otherwise, false.</returns>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public bool IsBackgroundPartiallyTransparent()
	{
		return VisualStyles.UxThemeIsThemeBackgroundPartiallyTransparent(theme, part, state);
	}

	/// <summary>Sets this <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleRenderer" /> to the visual style element represented by the specified class, part, and state values.</summary>
	/// <param name="className">The new value of the <see cref="P:System.Windows.Forms.VisualStyles.VisualStyleRenderer.Class" /> property.</param>
	/// <param name="part">The new value of the <see cref="P:System.Windows.Forms.VisualStyles.VisualStyleRenderer.Part" /> property.</param>
	/// <param name="state">The new value of the <see cref="P:System.Windows.Forms.VisualStyles.VisualStyleRenderer.State" /> property.</param>
	/// <exception cref="T:System.ArgumentException">The combination of <paramref name="className" />, <paramref name="part" />, and <paramref name="state" /> is not defined by the current visual style.</exception>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void SetParameters(string className, int part, int state)
	{
		if (theme != IntPtr.Zero)
		{
			last_hresult = VisualStyles.UxThemeCloseThemeData(theme);
		}
		if (!IsSupported)
		{
			throw new InvalidOperationException("Visual Styles are not enabled.");
		}
		class_name = className;
		this.part = part;
		this.state = state;
		theme = VisualStyles.UxThemeOpenThemeData(IntPtr.Zero, class_name);
		if (IsElementKnownToBeSupported(className, part, state) || (!(theme == IntPtr.Zero) && VisualStyles.UxThemeIsThemePartDefined(theme, this.part)))
		{
			return;
		}
		throw new ArgumentException("This element is not supported by the current visual style.");
	}

	/// <summary>Sets this <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleRenderer" /> to the visual style element represented by the specified <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" />.</summary>
	/// <param name="element">A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that specifies the new values of the <see cref="P:System.Windows.Forms.VisualStyles.VisualStyleRenderer.Class" />, <see cref="P:System.Windows.Forms.VisualStyles.VisualStyleRenderer.Part" />, and <see cref="P:System.Windows.Forms.VisualStyles.VisualStyleRenderer.State" /> properties.</param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="element" /> is not defined by the current visual style.</exception>
	/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void SetParameters(VisualStyleElement element)
	{
		SetParameters(element.ClassName, element.Part, element.State);
	}

	internal void DrawBackgroundExcludingArea(IDeviceContext dc, Rectangle bounds, Rectangle excludedArea)
	{
		VisualStyles.VisualStyleRendererDrawBackgroundExcludingArea(theme, dc, part, state, bounds, excludedArea);
	}

	private static bool IsElementKnownToBeSupported(string className, int part, int state)
	{
		return className == "STATUS" && part == 0 && state == 0;
	}
}
