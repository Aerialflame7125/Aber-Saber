namespace System.Windows.Forms;

/// <summary>Provides operating-system specific feature queries.</summary>
/// <filterpriority>2</filterpriority>
public class OSFeature : FeatureSupport
{
	private static OSFeature feature = new OSFeature();

	/// <summary>Represents the layered, top-level windows feature. This field is read-only. </summary>
	/// <filterpriority>1</filterpriority>
	public static readonly object LayeredWindows;

	/// <summary>Represents the operating system themes feature. This field is read-only.</summary>
	/// <filterpriority>1</filterpriority>
	public static readonly object Themes;

	/// <summary>Gets a static instance of the <see cref="T:System.Windows.Forms.OSFeature" /> class to use for feature queries. This property is read-only. </summary>
	/// <returns>An <see cref="T:System.Windows.Forms.OSFeature" />.</returns>
	/// <filterpriority>1</filterpriority>
	public static OSFeature Feature => feature;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.OSFeature" /> class. </summary>
	protected OSFeature()
	{
	}

	/// <summary>Retrieves a value indicating whether the operating system supports the specified feature or metric. </summary>
	/// <returns>true if the feature is available on the system; otherwise, false.</returns>
	/// <param name="enumVal">A <see cref="T:System.Windows.Forms.SystemParameter" /> representing the feature to search for.</param>
	/// <filterpriority>1</filterpriority>
	public static bool IsPresent(SystemParameter enumVal)
	{
		switch (enumVal)
		{
		case SystemParameter.DropShadow:
			try
			{
				object obj = SystemInformation.IsDropShadowEnabled;
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		case SystemParameter.FlatMenu:
			try
			{
				object obj = SystemInformation.IsFlatMenuEnabled;
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		case SystemParameter.FontSmoothingContrastMetric:
			try
			{
				object obj = SystemInformation.FontSmoothingContrast;
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		case SystemParameter.FontSmoothingTypeMetric:
			try
			{
				object obj = SystemInformation.FontSmoothingType;
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		case SystemParameter.MenuFadeEnabled:
			try
			{
				object obj = SystemInformation.IsMenuFadeEnabled;
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		case SystemParameter.SelectionFade:
			try
			{
				object obj = SystemInformation.IsSelectionFadeEnabled;
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		case SystemParameter.ToolTipAnimationMetric:
			try
			{
				object obj = SystemInformation.IsToolTipAnimationEnabled;
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		case SystemParameter.UIEffects:
			try
			{
				object obj = SystemInformation.UIEffectsEnabled;
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		case SystemParameter.CaretWidthMetric:
			try
			{
				object obj = SystemInformation.CaretWidth;
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		case SystemParameter.VerticalFocusThicknessMetric:
			try
			{
				object obj = SystemInformation.VerticalFocusThickness;
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		case SystemParameter.HorizontalFocusThicknessMetric:
			try
			{
				object obj = SystemInformation.HorizontalFocusThickness;
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		default:
			return false;
		}
	}

	/// <summary>Retrieves the version of the specified feature currently available on the system. </summary>
	/// <returns>A <see cref="T:System.Version" /> representing the version of the specified operating system feature currently available on the system; or null if the feature cannot be found.</returns>
	/// <param name="feature">The feature whose version is requested, either <see cref="F:System.Windows.Forms.OSFeature.LayeredWindows" /> or <see cref="F:System.Windows.Forms.OSFeature.Themes" />.</param>
	/// <filterpriority>1</filterpriority>
	public override Version GetVersionPresent(object feature)
	{
		if (feature == Themes)
		{
			return ThemeEngine.Current.Version;
		}
		return null;
	}
}
