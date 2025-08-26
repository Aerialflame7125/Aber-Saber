using System.Drawing;

namespace System.Windows.Forms.VisualStyles;

/// <summary>Provides information about the current visual style of the operating system.</summary>
public static class VisualStyleInformation
{
	/// <summary>Gets the author of the current visual style.</summary>
	/// <returns>A string that specifies the author of the current visual style if visual styles are enabled; otherwise, an empty string ("").</returns>
	public static string Author
	{
		get
		{
			if (!VisualStyleRenderer.IsSupported)
			{
				return string.Empty;
			}
			return VisualStyles.VisualStyleInformationAuthor;
		}
	}

	/// <summary>Gets the color scheme of the current visual style.</summary>
	/// <returns>A string that specifies the color scheme of the current visual style if visual styles are enabled; otherwise, an empty string ("").</returns>
	public static string ColorScheme
	{
		get
		{
			if (!VisualStyleRenderer.IsSupported)
			{
				return string.Empty;
			}
			return VisualStyles.VisualStyleInformationColorScheme;
		}
	}

	/// <summary>Gets the company that created the current visual style.</summary>
	/// <returns>A string that specifies the company that created the current visual style if visual styles are enabled; otherwise, an empty string ("").</returns>
	public static string Company
	{
		get
		{
			if (!VisualStyleRenderer.IsSupported)
			{
				return string.Empty;
			}
			return VisualStyles.VisualStyleInformationCompany;
		}
	}

	/// <summary>Gets the color that the current visual style uses to indicate the hot state of a control.</summary>
	/// <returns>If visual styles are enabled, the <see cref="T:System.Drawing.Color" /> used to paint a highlight on a control in the hot state; otherwise, <see cref="P:System.Drawing.SystemColors.ButtonHighlight" />.</returns>
	[System.MonoTODO("Cannot get this to return the same as MS's...")]
	public static Color ControlHighlightHot
	{
		get
		{
			if (!VisualStyleRenderer.IsSupported)
			{
				return SystemColors.ButtonHighlight;
			}
			return VisualStyles.VisualStyleInformationControlHighlightHot;
		}
	}

	/// <summary>Gets the copyright of the current visual style.</summary>
	/// <returns>A string that specifies the copyright of the current visual style if visual styles are enabled; otherwise, an empty string ("").</returns>
	public static string Copyright
	{
		get
		{
			if (!VisualStyleRenderer.IsSupported)
			{
				return string.Empty;
			}
			return VisualStyles.VisualStyleInformationCopyright;
		}
	}

	/// <summary>Gets a description of the current visual style.</summary>
	/// <returns>A string that describes the current visual style if visual styles are enabled; otherwise, an empty string ("").</returns>
	public static string Description
	{
		get
		{
			if (!VisualStyleRenderer.IsSupported)
			{
				return string.Empty;
			}
			return VisualStyles.VisualStyleInformationDescription;
		}
	}

	/// <summary>Gets the display name of the current visual style.</summary>
	/// <returns>A string that specifies the display name of the current visual style if visual styles are enabled; otherwise, an empty string ("").</returns>
	public static string DisplayName
	{
		get
		{
			if (!VisualStyleRenderer.IsSupported)
			{
				return string.Empty;
			}
			return VisualStyles.VisualStyleInformationDisplayName;
		}
	}

	/// <summary>Gets a value indicating whether the user has enabled visual styles in the operating system.</summary>
	/// <returns>true if the user has enabled visual styles in an operating system that supports them; otherwise, false.</returns>
	public static bool IsEnabledByUser
	{
		get
		{
			if (!IsSupportedByOS)
			{
				return false;
			}
			return VisualStyles.UxThemeIsAppThemed() && VisualStyles.UxThemeIsThemeActive();
		}
	}

	/// <summary>Gets a value indicating whether the operating system supports visual styles.</summary>
	/// <returns>true if the operating system supports visual styles; otherwise, false.</returns>
	public static bool IsSupportedByOS => VisualStyles.VisualStyleInformationIsSupportedByOS;

	/// <summary>Gets the minimum color depth for the current visual style.</summary>
	/// <returns>The minimum color depth for the current visual style if visual styles are enabled; otherwise, 0.</returns>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static int MinimumColorDepth
	{
		get
		{
			if (!VisualStyleRenderer.IsSupported)
			{
				return 0;
			}
			return VisualStyles.VisualStyleInformationMinimumColorDepth;
		}
	}

	/// <summary>Gets a string that describes the size of the current visual style.</summary>
	/// <returns>A string that describes the size of the current visual style if visual styles are enabled; otherwise, an empty string ("").</returns>
	public static string Size
	{
		get
		{
			if (!VisualStyleRenderer.IsSupported)
			{
				return string.Empty;
			}
			return VisualStyles.VisualStyleInformationSize;
		}
	}

	/// <summary>Gets a value indicating whether the current visual style supports flat menus.</summary>
	/// <returns>true if visual styles are enabled and the current visual style supports flat menus; otherwise, false.</returns>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static bool SupportsFlatMenus
	{
		get
		{
			if (!VisualStyleRenderer.IsSupported)
			{
				return false;
			}
			return VisualStyles.VisualStyleInformationSupportsFlatMenus;
		}
	}

	/// <summary>Gets the color that the current visual style uses to paint the borders of controls that contain text.</summary>
	/// <returns>If visual styles are enabled, the <see cref="T:System.Drawing.Color" /> that the current visual style uses to paint the borders of controls that contain text; otherwise, <see cref="P:System.Drawing.SystemColors.ControlDarkDark" />.</returns>
	[System.MonoTODO("Cannot get this to return the same as MS's...")]
	public static Color TextControlBorder
	{
		get
		{
			if (!VisualStyleRenderer.IsSupported)
			{
				return SystemColors.ControlDarkDark;
			}
			return VisualStyles.VisualStyleInformationTextControlBorder;
		}
	}

	/// <summary>Gets a URL provided by the author of the current visual style.</summary>
	/// <returns>A string that specifies a URL provided by the author of the current visual style if visual styles are enabled; otherwise, an empty string ("").</returns>
	public static string Url
	{
		get
		{
			if (!VisualStyleRenderer.IsSupported)
			{
				return string.Empty;
			}
			return VisualStyles.VisualStyleInformationUrl;
		}
	}

	/// <summary>Gets the version of the current visual style.</summary>
	/// <returns>A string that indicates the version of the current visual style if visual styles are enabled; otherwise, an empty string ("").</returns>
	public static string Version
	{
		get
		{
			if (!VisualStyleRenderer.IsSupported)
			{
				return string.Empty;
			}
			return VisualStyles.VisualStyleInformationVersion;
		}
	}

	private static IVisualStyles VisualStyles => VisualStylesEngine.Instance;
}
