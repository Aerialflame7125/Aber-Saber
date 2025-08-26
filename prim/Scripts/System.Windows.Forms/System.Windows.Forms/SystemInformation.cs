using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Provides information about the current system environment.</summary>
/// <filterpriority>2</filterpriority>
public class SystemInformation
{
	/// <summary>Gets the active window tracking delay.</summary>
	/// <returns>The active window tracking delay, in milliseconds.</returns>
	/// <filterpriority>1</filterpriority>
	[System.MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
	public static int ActiveWindowTrackingDelay => XplatUI.ActiveWindowTrackingDelay;

	/// <summary>Gets a value that indicates the direction in which the operating system arranges minimized windows.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ArrangeDirection" /> values that indicates the direction in which the operating system arranges minimized windows.</returns>
	/// <filterpriority>1</filterpriority>
	public static ArrangeDirection ArrangeDirection => ThemeEngine.Current.ArrangeDirection;

	/// <summary>Gets an <see cref="T:System.Windows.Forms.ArrangeStartingPosition" /> value that indicates the starting position from which the operating system arranges minimized windows.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ArrangeStartingPosition" /> values that indicates the starting position from which the operating system arranges minimized windows.</returns>
	/// <filterpriority>1</filterpriority>
	public static ArrangeStartingPosition ArrangeStartingPosition => ThemeEngine.Current.ArrangeStartingPosition;

	/// <summary>Gets a <see cref="T:System.Windows.Forms.BootMode" /> value that indicates the boot mode the system was started in.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.BootMode" /> values that indicates the boot mode the system was started in.</returns>
	/// <filterpriority>1</filterpriority>
	public static BootMode BootMode => BootMode.Normal;

	/// <summary>Gets the thickness, in pixels, of a three-dimensional (3-D) style window or system control border.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the width, in pixels, of a 3-D style vertical border, and the height, in pixels, of a 3-D style horizontal border.</returns>
	/// <filterpriority>1</filterpriority>
	public static Size Border3DSize => ThemeEngine.Current.Border3DSize;

	/// <summary>Gets the border multiplier factor that is used when determining the thickness of a window's sizing border.</summary>
	/// <returns>The multiplier used to determine the thickness of a window's sizing border.</returns>
	/// <filterpriority>1</filterpriority>
	[System.MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
	public static int BorderMultiplierFactor => ThemeEngine.Current.BorderMultiplierFactor;

	/// <summary>Gets the thickness, in pixels, of a flat-style window or system control border.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the width, in pixels, of a vertical border, and the height, in pixels, of a horizontal border.</returns>
	/// <filterpriority>1</filterpriority>
	public static Size BorderSize => ThemeEngine.Current.BorderSize;

	/// <summary>Gets the standard size, in pixels, of a button in a window's title bar.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the standard dimensions, in pixels, of a button in a window's title bar.</returns>
	/// <filterpriority>1</filterpriority>
	public static Size CaptionButtonSize => ThemeEngine.Current.CaptionButtonSize;

	/// <summary>Gets the height, in pixels, of the standard title bar area of a window.</summary>
	/// <returns>The height, in pixels, of the standard title bar area of a window.</returns>
	/// <filterpriority>1</filterpriority>
	public static int CaptionHeight => ThemeEngine.Current.CaptionHeight;

	/// <summary>Gets the caret blink time.</summary>
	/// <returns>The caret blink time.</returns>
	/// <filterpriority>1</filterpriority>
	[System.MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
	public static int CaretBlinkTime => XplatUI.CaretBlinkTime;

	/// <summary>Gets the width, in pixels, of the caret in edit controls.</summary>
	/// <returns>The width, in pixels, of the caret in edit controls.</returns>
	/// <exception cref="T:System.NotSupportedException">The operating system does not support this feature.</exception>
	/// <filterpriority>1</filterpriority>
	[System.MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
	public static int CaretWidth => XplatUI.CaretWidth;

	/// <summary>Gets the NetBIOS computer name of the local computer.</summary>
	/// <returns>The name of this computer.</returns>
	/// <filterpriority>1</filterpriority>
	public static string ComputerName => Environment.MachineName;

	/// <summary>Gets the maximum size, in pixels, that a cursor can occupy.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the maximum dimensions of a cursor in pixels.</returns>
	/// <filterpriority>1</filterpriority>
	public static Size CursorSize => XplatUI.CursorSize;

	/// <summary>Gets a value indicating whether the operating system is capable of handling double-byte character set (DBCS) characters.</summary>
	/// <returns>true if the operating system supports DBCS; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public static bool DbcsEnabled => false;

	/// <summary>Gets a value indicating whether the debug version of USER.EXE is installed.</summary>
	/// <returns>true if the debugging version of USER.EXE is installed; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public static bool DebugOS => false;

	/// <summary>Gets the dimensions, in pixels, of the area within which the user must click twice for the operating system to consider the two clicks a double-click.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the dimensions, in pixels, of the area within which the user must click twice for the operating system to consider the two clicks a double-click.</returns>
	/// <filterpriority>1</filterpriority>
	public static Size DoubleClickSize => ThemeEngine.Current.DoubleClickSize;

	/// <summary>Gets the maximum number of milliseconds that can elapse between a first click and a second click for the OS to consider the mouse action a double-click.</summary>
	/// <returns>The maximum amount of time, in milliseconds, that can elapse between a first click and a second click for the OS to consider the mouse action a double-click.</returns>
	/// <filterpriority>1</filterpriority>
	public static int DoubleClickTime => ThemeEngine.Current.DoubleClickTime;

	/// <summary>Gets a value indicating whether the user has enabled full window drag.</summary>
	/// <returns>true if the user has enabled full window drag; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public static bool DragFullWindows => XplatUI.DragFullWindows;

	/// <summary>Gets the width and height of a rectangle centered on the point the mouse button was pressed, within which a drag operation will not begin.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the area of a rectangle, in pixels, centered on the point the mouse button was pressed, within which a drag operation will not begin.</returns>
	/// <filterpriority>1</filterpriority>
	public static Size DragSize => XplatUI.DragSize;

	/// <summary>Gets the thickness, in pixels, of the frame border of a window that has a caption and is not resizable.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the thickness, in pixels, of a fixed sized window border.</returns>
	/// <filterpriority>1</filterpriority>
	public static Size FixedFrameBorderSize => ThemeEngine.Current.FixedFrameBorderSize;

	/// <summary>Gets the font smoothing contrast value used in ClearType smoothing.</summary>
	/// <returns>The ClearType font smoothing contrast value.</returns>
	/// <exception cref="T:System.NotSupportedException">The operating system does not support this feature.</exception>
	/// <filterpriority>1</filterpriority>
	[System.MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
	public static int FontSmoothingContrast => XplatUI.FontSmoothingContrast;

	/// <summary>Gets the current type of font smoothing.</summary>
	/// <returns>A value that indicates the current type of font smoothing.</returns>
	/// <exception cref="T:System.NotSupportedException">The operating system does not support this feature.</exception>
	/// <filterpriority>1</filterpriority>
	[System.MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
	public static int FontSmoothingType => XplatUI.FontSmoothingType;

	/// <summary>Gets the thickness, in pixels, of the resizing border that is drawn around the perimeter of a window that is being drag resized.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the thickness, in pixels, of the width of a vertical resizing border and the height of a horizontal resizing border.</returns>
	/// <filterpriority>1</filterpriority>
	public static Size FrameBorderSize => ThemeEngine.Current.FrameBorderSize;

	/// <summary>Gets a value indicating whether the user has enabled the high-contrast mode accessibility feature.</summary>
	/// <returns>true if the user has enabled high-contrast mode; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static bool HighContrast => false;

	/// <summary>Gets the thickness of the left and right edges of the system focus rectangle, in pixels.</summary>
	/// <returns>The thickness of the left and right edges of the system focus rectangle, in pixels.</returns>
	/// <exception cref="T:System.NotSupportedException">The operating system does not support this feature.</exception>
	/// <filterpriority>1</filterpriority>
	[System.MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
	public static int HorizontalFocusThickness => ThemeEngine.Current.HorizontalFocusThickness;

	/// <summary>Gets the thickness of the left and right edges of the sizing border around the perimeter of a window being resized, in pixels.</summary>
	/// <returns>The width of the left and right edges of the sizing border around the perimeter of a window being resized, in pixels.</returns>
	/// <filterpriority>1</filterpriority>
	[System.MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
	public static int HorizontalResizeBorderThickness => XplatUI.HorizontalResizeBorderThickness;

	/// <summary>Gets the width, in pixels, of the arrow bitmap on the horizontal scroll bar.</summary>
	/// <returns>The width, in pixels, of the arrow bitmap on the horizontal scroll bar.</returns>
	/// <filterpriority>1</filterpriority>
	public static int HorizontalScrollBarArrowWidth => ThemeEngine.Current.HorizontalScrollBarArrowWidth;

	/// <summary>Gets the default height, in pixels, of the horizontal scroll bar.</summary>
	/// <returns>The default height, in pixels, of the horizontal scroll bar.</returns>
	/// <filterpriority>1</filterpriority>
	public static int HorizontalScrollBarHeight => ThemeEngine.Current.HorizontalScrollBarHeight;

	/// <summary>Gets the width, in pixels, of the scroll box in a horizontal scroll bar.</summary>
	/// <returns>The width, in pixels, of the scroll box in a horizontal scroll bar.</returns>
	/// <filterpriority>1</filterpriority>
	public static int HorizontalScrollBarThumbWidth => ThemeEngine.Current.HorizontalScrollBarThumbWidth;

	/// <summary>Gets the dimensions, in pixels, of the Windows default program icon size.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the default dimensions, in pixels, for a program icon.</returns>
	/// <filterpriority>1</filterpriority>
	public static Size IconSize => XplatUI.IconSize;

	/// <summary>Gets the width, in pixels, of an icon arrangement cell in large icon view.</summary>
	/// <returns>The width, in pixels, of an icon arrangement cell in large icon view.</returns>
	/// <filterpriority>1</filterpriority>
	public static int IconHorizontalSpacing => IconSpacingSize.Width;

	/// <summary>Gets the height, in pixels, of an icon arrangement cell in large icon view.</summary>
	/// <returns>The height, in pixels, of an icon arrangement cell in large icon view.</returns>
	/// <filterpriority>1</filterpriority>
	public static int IconVerticalSpacing => IconSpacingSize.Height;

	/// <summary>Gets the size, in pixels, of the grid square used to arrange icons in a large-icon view.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the dimensions, in pixels, of the grid square used to arrange icons in a large-icon view.</returns>
	/// <filterpriority>1</filterpriority>
	public static Size IconSpacingSize => ThemeEngine.Current.IconSpacingSize;

	/// <summary>Gets a value indicating whether active window tracking is enabled.</summary>
	/// <returns>true if active window tracking is enabled; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[System.MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
	public static bool IsActiveWindowTrackingEnabled => XplatUI.IsActiveWindowTrackingEnabled;

	/// <summary>Gets a value indicating whether the slide-open effect for combo boxes is enabled.</summary>
	/// <returns>true if the slide-open effect for combo boxes is enabled; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[System.MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
	public static bool IsComboBoxAnimationEnabled => XplatUI.IsComboBoxAnimationEnabled;

	/// <summary>Gets a value indicating whether the drop shadow effect is enabled.</summary>
	/// <returns>true if the drop shadow effect is enabled; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[System.MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
	public static bool IsDropShadowEnabled => XplatUI.IsDropShadowEnabled;

	/// <summary>Gets a value indicating whether native user menus have a flat menu appearance. </summary>
	/// <returns>This property is not used and always returns false.</returns>
	/// <filterpriority>1</filterpriority>
	public static bool IsFlatMenuEnabled => false;

	/// <summary>Gets a value indicating whether font smoothing is enabled.</summary>
	/// <returns>true if the font smoothing feature is enabled; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[System.MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
	public static bool IsFontSmoothingEnabled => XplatUI.IsFontSmoothingEnabled;

	/// <summary>Gets a value indicating whether hot tracking of user-interface elements, such as menu names on menu bars, is enabled.</summary>
	/// <returns>true if hot tracking of user-interface elements is enabled; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[System.MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
	public static bool IsHotTrackingEnabled => XplatUI.IsHotTrackingEnabled;

	/// <summary>Gets a value indicating whether icon-title wrapping is enabled.</summary>
	/// <returns>true if the icon-title wrapping feature is enabled; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[System.MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
	public static bool IsIconTitleWrappingEnabled => XplatUI.IsIconTitleWrappingEnabled;

	/// <summary>Gets a value indicating whether the user relies on the keyboard instead of the mouse, and prefers applications to display keyboard interfaces that would otherwise be hidden.</summary>
	/// <returns>true if keyboard preferred mode is enabled; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[System.MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
	public static bool IsKeyboardPreferred => XplatUI.IsKeyboardPreferred;

	/// <summary>Gets a value indicating whether the smooth-scrolling effect for list boxes is enabled.</summary>
	/// <returns>true if smooth-scrolling is enabled; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[System.MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
	public static bool IsListBoxSmoothScrollingEnabled => XplatUI.IsListBoxSmoothScrollingEnabled;

	/// <summary>Gets a value indicating whether menu fade or slide animation features are enabled.</summary>
	/// <returns>true if menu fade or slide animation is enabled; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[System.MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
	public static bool IsMenuAnimationEnabled => XplatUI.IsMenuAnimationEnabled;

	/// <summary>Gets a value indicating whether menu fade animation is enabled.</summary>
	/// <returns>true if fade animation is enabled; false if it is disabled.</returns>
	/// <filterpriority>1</filterpriority>
	[System.MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
	public static bool IsMenuFadeEnabled => XplatUI.IsMenuFadeEnabled;

	/// <summary>Gets a value indicating whether window minimize and restore animation is enabled.</summary>
	/// <returns>true if window minimize and restore animation is enabled; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[System.MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
	public static bool IsMinimizeRestoreAnimationEnabled => XplatUI.IsMinimizeRestoreAnimationEnabled;

	/// <summary>Gets a value indicating whether the selection fade effect is enabled.</summary>
	/// <returns>true if the selection fade effect is enabled; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[System.MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
	public static bool IsSelectionFadeEnabled => XplatUI.IsSelectionFadeEnabled;

	/// <summary>Gets a value indicating whether the snap-to-default-button feature is enabled.</summary>
	/// <returns>true if the snap-to-default-button feature is enabled; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[System.MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
	public static bool IsSnapToDefaultEnabled => XplatUI.IsSnapToDefaultEnabled;

	/// <summary>Gets a value indicating whether the gradient effect for window title bars is enabled.</summary>
	/// <returns>true if the gradient effect for window title bars is enabled; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[System.MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
	public static bool IsTitleBarGradientEnabled => XplatUI.IsTitleBarGradientEnabled;

	/// <summary>Gets a value indicating whether <see cref="T:System.Windows.Forms.ToolTip" /> animation is enabled.</summary>
	/// <returns>true if <see cref="T:System.Windows.Forms.ToolTip" /> animation is enabled; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[System.MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
	public static bool IsToolTipAnimationEnabled => XplatUI.IsToolTipAnimationEnabled;

	/// <summary>Gets the height, in pixels, of the Kanji window at the bottom of the screen for double-byte character set (DBCS) versions of Windows.</summary>
	/// <returns>The height, in pixels, of the Kanji window.</returns>
	/// <filterpriority>1</filterpriority>
	public static int KanjiWindowHeight => 0;

	/// <summary>Gets the keyboard repeat-delay setting.</summary>
	/// <returns>The keyboard repeat-delay setting, from 0 (approximately 250 millisecond delay) through 3 (approximately 1 second delay).</returns>
	/// <filterpriority>1</filterpriority>
	public static int KeyboardDelay => XplatUI.KeyboardDelay;

	/// <summary>Gets the keyboard repeat-speed setting.</summary>
	/// <returns>The keyboard repeat-speed setting, from 0 (approximately 2.5 repetitions per second) through 31 (approximately 30 repetitions per second).</returns>
	/// <filterpriority>1</filterpriority>
	public static int KeyboardSpeed => XplatUI.KeyboardSpeed;

	/// <summary>Gets the default maximum dimensions, in pixels, of a window that has a caption and sizing borders.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the maximum dimensions, in pixels, to which a window can be sized.</returns>
	/// <filterpriority>1</filterpriority>
	public static Size MaxWindowTrackSize => XplatUI.MaxWindowTrackSize;

	/// <summary>Gets a value indicating whether menu access keys are always underlined.</summary>
	/// <returns>true if menu access keys are always underlined; false if they are underlined only when the menu is activated or receives focus.</returns>
	public static bool MenuAccessKeysUnderlined => ThemeEngine.Current.MenuAccessKeysUnderlined;

	/// <summary>Gets the default width, in pixels, for menu-bar buttons and the height, in pixels, of a menu bar.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the default width for menu-bar buttons, in pixels, and the height of a menu bar, in pixels.</returns>
	/// <filterpriority>1</filterpriority>
	[System.MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
	public static Size MenuBarButtonSize => ThemeEngine.Current.MenuBarButtonSize;

	/// <summary>Gets the default dimensions, in pixels, of menu-bar buttons.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the default dimensions, in pixels, of menu-bar buttons.</returns>
	/// <filterpriority>1</filterpriority>
	public static Size MenuButtonSize => ThemeEngine.Current.MenuButtonSize;

	/// <summary>Gets the dimensions, in pixels, of the default size of a menu check mark area.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the default size, in pixels, of a menu check mark area.</returns>
	/// <filterpriority>1</filterpriority>
	public static Size MenuCheckSize => ThemeEngine.Current.MenuCheckSize;

	/// <summary>Gets the font used to display text on menus.</summary>
	/// <returns>The <see cref="T:System.Drawing.Font" /> used to display text on menus.</returns>
	/// <filterpriority>1</filterpriority>
	public static Font MenuFont => (Font)ThemeEngine.Current.MenuFont.Clone();

	/// <summary>Gets the height, in pixels, of one line of a menu.</summary>
	/// <returns>The height, in pixels, of one line of a menu.</returns>
	/// <filterpriority>1</filterpriority>
	public static int MenuHeight => ThemeEngine.Current.MenuHeight;

	/// <summary>Gets the time, in milliseconds, that the system waits before displaying a cascaded shortcut menu when the mouse cursor is over a submenu item.</summary>
	/// <returns>The time, in milliseconds, that the system waits before displaying a cascaded shortcut menu when the mouse cursor is over a submenu item.</returns>
	/// <filterpriority>1</filterpriority>
	[System.MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
	public static int MenuShowDelay => XplatUI.MenuShowDelay;

	/// <summary>Gets a value indicating whether the operating system is enabled for the Hebrew and Arabic languages.</summary>
	/// <returns>true if the operating system is enabled for Hebrew or Arabic; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public static bool MidEastEnabled => false;

	/// <summary>Gets the dimensions, in pixels, of a normal minimized window.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the dimensions, in pixels, of a normal minimized window.</returns>
	/// <filterpriority>1</filterpriority>
	public static Size MinimizedWindowSize => XplatUI.MinimizedWindowSize;

	/// <summary>Gets the dimensions, in pixels, of the area each minimized window is allocated when arranged.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the area each minimized window is allocated when arranged.</returns>
	/// <filterpriority>1</filterpriority>
	public static Size MinimizedWindowSpacingSize => XplatUI.MinimizedWindowSpacingSize;

	/// <summary>Gets the minimum width and height for a window, in pixels.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the minimum allowable dimensions of a window, in pixels.</returns>
	/// <filterpriority>1</filterpriority>
	public static Size MinimumWindowSize => XplatUI.MinimumWindowSize;

	/// <summary>Gets the default minimum dimensions, in pixels, that a window may occupy during a drag resize.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the default minimum width and height of a window during resize, in pixels.</returns>
	/// <filterpriority>1</filterpriority>
	public static Size MinWindowTrackSize => XplatUI.MinWindowTrackSize;

	/// <summary>Gets the number of display monitors on the desktop.</summary>
	/// <returns>The number of monitors that make up the desktop.</returns>
	/// <filterpriority>1</filterpriority>
	public static int MonitorCount => 1;

	/// <summary>Gets a value indicating whether all the display monitors are using the same pixel color format.</summary>
	/// <returns>true if all monitors are using the same pixel color format; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public static bool MonitorsSameDisplayFormat => true;

	/// <summary>Gets the number of buttons on the mouse.</summary>
	/// <returns>The number of buttons on the mouse, or zero if no mouse is installed.</returns>
	/// <filterpriority>1</filterpriority>
	public static int MouseButtons => XplatUI.MouseButtonCount;

	/// <summary>Gets a value indicating whether the functions of the left and right mouse buttons have been swapped.</summary>
	/// <returns>true if the functions of the left and right mouse buttons are swapped; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public static bool MouseButtonsSwapped => XplatUI.MouseButtonsSwapped;

	/// <summary>Gets the dimensions, in pixels, of the rectangle within which the mouse pointer has to stay for the mouse hover time before a mouse hover message is generated.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the dimensions, in pixels, of the rectangle within which the mouse pointer has to stay for the mouse hover time before a mouse hover message is generated.</returns>
	/// <filterpriority>1</filterpriority>
	public static Size MouseHoverSize => XplatUI.MouseHoverSize;

	/// <summary>Gets the time, in milliseconds, that the mouse pointer has to stay in the hover rectangle before a mouse hover message is generated.</summary>
	/// <returns>The time, in milliseconds, that the mouse pointer has to stay in the hover rectangle before a mouse hover message is generated.</returns>
	/// <filterpriority>1</filterpriority>
	public static int MouseHoverTime => XplatUI.MouseHoverTime;

	/// <summary>Gets the current mouse speed.</summary>
	/// <returns>A mouse speed value between 1 (slowest) and 20 (fastest).</returns>
	/// <filterpriority>1</filterpriority>
	[System.MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
	public static int MouseSpeed => XplatUI.MouseSpeed;

	/// <summary>Gets the amount of the delta value of a single mouse wheel rotation increment.</summary>
	/// <returns>The amount of the delta value of a single mouse wheel rotation increment.</returns>
	/// <filterpriority>1</filterpriority>
	public static int MouseWheelScrollDelta => XplatUI.MouseWheelScrollDelta;

	/// <summary>Gets a value indicating whether a pointing device is installed.</summary>
	/// <returns>true if a mouse is installed; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool MousePresent => true;

	/// <summary>Gets a value indicating whether a mouse with a mouse wheel is installed.</summary>
	/// <returns>true if a mouse with a mouse wheel is installed; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public static bool MouseWheelPresent => XplatUI.MouseWheelPresent;

	/// <summary>Gets the number of lines to scroll when the mouse wheel is rotated.</summary>
	/// <returns>The number of lines to scroll on a mouse wheel rotation, or -1 if the "One screen at a time" mouse option is selected.</returns>
	/// <filterpriority>1</filterpriority>
	public static int MouseWheelScrollLines => ThemeEngine.Current.MouseWheelScrollLines;

	/// <summary>Gets a value indicating whether the operating system natively supports a mouse wheel.</summary>
	/// <returns>true if the operating system natively supports a mouse wheel; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public static bool NativeMouseWheelSupport => MouseWheelPresent;

	/// <summary>Gets a value indicating whether a network connection is present.</summary>
	/// <returns>true if a network connection is present; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public static bool Network => true;

	/// <summary>Gets a value indicating whether the Microsoft Windows for Pen Computing extensions are installed.</summary>
	/// <returns>true if the Windows for Pen Computing extensions are installed; false if Windows for Pen Computing extensions are not installed.</returns>
	/// <filterpriority>1</filterpriority>
	public static bool PenWindows => false;

	/// <summary>Gets the side of pop-up menus that are aligned to the corresponding menu-bar item.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.LeftRightAlignment" /> that indicates whether pop-up menus are left-aligned or right-aligned, relative to the corresponding menu-bar item.</returns>
	/// <filterpriority>1</filterpriority>
	[System.MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
	public static LeftRightAlignment PopupMenuAlignment => XplatUI.PopupMenuAlignment;

	/// <summary>Gets the current system power status.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.PowerStatus" /> that indicates the current system power status.</returns>
	/// <filterpriority>1</filterpriority>
	[System.MonoTODO("Only implemented for Win32.")]
	public static PowerStatus PowerStatus => XplatUI.PowerStatus;

	/// <summary>Gets the default dimensions, in pixels, of a maximized window on the primary display.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the dimensions, in pixels, of a maximized window on the primary display.</returns>
	/// <filterpriority>1</filterpriority>
	public static Size PrimaryMonitorMaximizedWindowSize => new Size(WorkingArea.Width, WorkingArea.Height);

	/// <summary>Gets the dimensions, in pixels, of the current video mode of the primary display.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the dimensions, in pixels, of the current video mode of the primary display.</returns>
	/// <filterpriority>1</filterpriority>
	public static Size PrimaryMonitorSize => new Size(WorkingArea.Width, WorkingArea.Height);

	/// <summary>Gets a value indicating whether drop-down menus are right-aligned with the corresponding menu-bar item.</summary>
	/// <returns>true if drop-down menus are right-aligned with the corresponding menu-bar item; false if the menus are left-aligned.</returns>
	/// <filterpriority>1</filterpriority>
	public static bool RightAlignedMenus => ThemeEngine.Current.RightAlignedMenus;

	/// <summary>Gets the orientation of the screen.</summary>
	/// <returns>The orientation of the screen, in degrees.</returns>
	/// <filterpriority>1</filterpriority>
	public static ScreenOrientation ScreenOrientation => ScreenOrientation.Angle0;

	/// <summary>Gets a value indicating whether a Security Manager is present on this operating system.</summary>
	/// <returns>true if a Security Manager is present; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public static bool Secure => true;

	/// <summary>Gets a value indicating whether the user prefers that an application present information in visual form in situations when it would present the information in audible form.</summary>
	/// <returns>true if the application should visually show information about audible output; false if the application does not need to provide extra visual cues for audio events.</returns>
	/// <filterpriority>1</filterpriority>
	public static bool ShowSounds => false;

	/// <summary>Gets the width, in pixels, of the sizing border drawn around the perimeter of a window being resized.</summary>
	/// <returns>The width, in pixels, of the window sizing border drawn around the perimeter of a window being resized.</returns>
	/// <filterpriority>1</filterpriority>
	[System.MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
	public static int SizingBorderWidth => XplatUI.SizingBorderWidth;

	/// <summary>Gets the width, in pixels, of small caption buttons, and the height, in pixels, of small captions.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the width, in pixels, of small caption buttons, and the height, in pixels, of small captions.</returns>
	/// <filterpriority>1</filterpriority>
	[System.MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
	public static Size SmallCaptionButtonSize => XplatUI.SmallCaptionButtonSize;

	/// <summary>Gets the dimensions, in pixels, of a small icon.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the dimensions, in pixels, of a small icon.</returns>
	/// <filterpriority>1</filterpriority>
	public static Size SmallIconSize => XplatUI.SmallIconSize;

	/// <summary>Gets a value indicating whether the calling process is associated with a Terminal Services client session.</summary>
	/// <returns>true if the calling process is associated with a Terminal Services client session; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public static bool TerminalServerSession => false;

	/// <summary>Gets the dimensions, in pixels, of small caption buttons.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the dimensions, in pixels, of small caption buttons.</returns>
	/// <filterpriority>1</filterpriority>
	public static Size ToolWindowCaptionButtonSize => ThemeEngine.Current.ToolWindowCaptionButtonSize;

	/// <summary>Gets the height, in pixels, of a tool window caption.</summary>
	/// <returns>The height, in pixels, of a tool window caption in pixels.</returns>
	/// <filterpriority>1</filterpriority>
	public static int ToolWindowCaptionHeight => ThemeEngine.Current.ToolWindowCaptionHeight;

	/// <summary>Gets a value indicating whether user interface (UI) effects are enabled or disabled.</summary>
	/// <returns>true if UI effects are enabled; otherwise, false.</returns>
	[System.MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
	public static bool UIEffectsEnabled => XplatUI.UIEffectsEnabled;

	/// <summary>Gets the name of the domain the user belongs to.</summary>
	/// <returns>The name of the user domain. If a local user account exists with the same name as the user name, this property gets the computer name.</returns>
	/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system does not support this feature.</exception>
	/// <filterpriority>1</filterpriority>
	public static string UserDomainName => Environment.UserDomainName;

	/// <summary>Gets a value indicating whether the current process is running in user-interactive mode.</summary>
	/// <returns>true if the current process is running in user-interactive mode; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public static bool UserInteractive => Environment.UserInteractive;

	/// <summary>Gets the user name associated with the current thread.</summary>
	/// <returns>The user name of the user associated with the current thread.</returns>
	/// <filterpriority>1</filterpriority>
	public static string UserName => Environment.UserName;

	/// <summary>Gets the thickness, in pixels, of the top and bottom edges of the system focus rectangle.</summary>
	/// <returns>The thickness, in pixels, of the top and bottom edges of the system focus rectangle.</returns>
	/// <exception cref="T:System.NotSupportedException">The operating system does not support this feature.</exception>
	/// <filterpriority>1</filterpriority>
	[System.MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
	public static int VerticalFocusThickness => ThemeEngine.Current.VerticalFocusThickness;

	/// <summary>Gets the thickness, in pixels, of the top and bottom edges of the sizing border around the perimeter of a window being resized.</summary>
	/// <returns>The height, in pixels, of the top and bottom edges of the sizing border around the perimeter of a window being resized, in pixels.</returns>
	/// <filterpriority>1</filterpriority>
	[System.MonoInternalNote("Determine if we need an X11 implementation or if defaults are good.")]
	public static int VerticalResizeBorderThickness => XplatUI.VerticalResizeBorderThickness;

	/// <summary>Gets the height, in pixels, of the arrow bitmap on the vertical scroll bar.</summary>
	/// <returns>The height, in pixels, of the arrow bitmap on the vertical scroll bar.</returns>
	/// <filterpriority>1</filterpriority>
	public static int VerticalScrollBarArrowHeight => ThemeEngine.Current.VerticalScrollBarArrowHeight;

	/// <summary>Gets the height, in pixels, of the scroll box in a vertical scroll bar.</summary>
	/// <returns>The height, in pixels, of the scroll box in a vertical scroll bar.</returns>
	/// <filterpriority>1</filterpriority>
	public static int VerticalScrollBarThumbHeight => ThemeEngine.Current.VerticalScrollBarThumbHeight;

	/// <summary>Gets the default width, in pixels, of the vertical scroll bar.</summary>
	/// <returns>The default width, in pixels, of the vertical scroll bar.</returns>
	/// <filterpriority>1</filterpriority>
	public static int VerticalScrollBarWidth => ThemeEngine.Current.VerticalScrollBarWidth;

	/// <summary>Gets the bounds of the virtual screen.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that specifies the bounding rectangle of the entire virtual screen.</returns>
	/// <filterpriority>1</filterpriority>
	public static Rectangle VirtualScreen => XplatUI.VirtualScreen;

	/// <summary>Gets the size, in pixels, of the working area of the screen.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the size, in pixels, of the working area of the screen.</returns>
	/// <filterpriority>1</filterpriority>
	public static Rectangle WorkingArea => XplatUI.WorkingArea;

	private SystemInformation()
	{
	}
}
