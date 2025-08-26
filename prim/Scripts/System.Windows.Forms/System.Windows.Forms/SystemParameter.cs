using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Specifies the system parameter type.</summary>
/// <filterpriority>2</filterpriority>
[ComVisible(true)]
public enum SystemParameter
{
	/// <summary>Identifies the drop shadow effect. Use the <see cref="P:System.Windows.Forms.SystemInformation.IsDropShadowEnabled" /> property to determine if this effect is enabled. The corresponding Platform SDK system-wide parameters are SPI_GETDROPSHADOW and SPI_SETDROPSHADOW.</summary>
	DropShadow,
	/// <summary>Identifies the flat menu appearance feature. Use the <see cref="P:System.Windows.Forms.SystemInformation.IsFlatMenuEnabled" /> property to determine if this feature is enabled. The corresponding Platform SDK system-wide parameters are SPI_GETFLATMENU and SPI_SETFLATMENU.</summary>
	FlatMenu,
	/// <summary>Identifies the contrast value used in ClearType font smoothing. Use the <see cref="P:System.Windows.Forms.SystemInformation.FontSmoothingContrast" /> property to access this system-wide parameter. The corresponding Platform SDK system-wide parameters are SPI_GETFONTSMOOTHINGCONTRAST and SPI_SETFONTSMOOTHINGCONTRAST.</summary>
	FontSmoothingContrastMetric,
	/// <summary>Identifies the type of font smoothing. Use the <see cref="P:System.Windows.Forms.SystemInformation.FontSmoothingType" /> property to access this system-wide parameter. The corresponding Platform SDK system-wide parameters are SPI_GETFONTSMOOTHINGTYPE and SPI_SETFONTSMOOTHINGTYPE.</summary>
	FontSmoothingTypeMetric,
	/// <summary>Identifies the menu fade animation feature. Use the <see cref="P:System.Windows.Forms.SystemInformation.IsMenuFadeEnabled" /> property to determine if this feature is enabled. The corresponding Platform SDK system-wide parameters are SPI_GETMENUFADE and SPI_SETMENUFADE.</summary>
	MenuFadeEnabled,
	/// <summary>Identifies the selection fade effect. Use the <see cref="P:System.Windows.Forms.SystemInformation.IsSelectionFadeEnabled" /> property to determine if this feature is enabled. The corresponding Platform SDK system-wide parameters are SPI_GETSELECTIONFADE and SPI_SETSELECTIONFADE.</summary>
	SelectionFade,
	/// <summary>Identifies the ToolTip animation feature. Use the <see cref="P:System.Windows.Forms.SystemInformation.IsToolTipAnimationEnabled" /> property to determine if this feature is enabled. The corresponding Platform SDK system-wide parameters are SPI_GETTOOLTIPANIMATION and SPI_SETTOOLTIPANIMATION.</summary>
	ToolTipAnimationMetric,
	/// <summary>Identifies the user interface (UI) effects feature. Use the <see cref="P:System.Windows.Forms.SystemInformation.UIEffectsEnabled" /> property to determine if this feature is enabled. The corresponding Platform SDK system-wide parameters are SPI_GETUIEFFECTS and SPI_SETUIEFFECTS.</summary>
	UIEffects,
	/// <summary>Identifies the caret width, in pixels, for edit controls. Use the <see cref="P:System.Windows.Forms.SystemInformation.CaretWidth" /> property to access this system-wide parameter. The corresponding Platform SDK system-wide parameters are SPI_GETCARETWIDTH and SPI_SETCARETWIDTH.</summary>
	CaretWidthMetric,
	/// <summary>Identifies the thickness of the top and bottom edges of the system focus rectangle. Use the <see cref="P:System.Windows.Forms.SystemInformation.VerticalFocusThickness" /> property to access this system-wide parameter. The corresponding Platform SDK system-wide parameter is SM_CYFOCUSBORDER.</summary>
	VerticalFocusThicknessMetric,
	/// <summary>Identifies the thickness of the left and right edges of the system focus rectangle. Use the <see cref="P:System.Windows.Forms.SystemInformation.HorizontalFocusThickness" /> property to access this system-wide parameter. The corresponding Platform SDK system-wide parameter is SM_CXFOCUSBORDER.</summary>
	HorizontalFocusThicknessMetric
}
