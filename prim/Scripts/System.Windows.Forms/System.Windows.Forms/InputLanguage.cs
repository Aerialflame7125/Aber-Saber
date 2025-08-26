using System.Globalization;

namespace System.Windows.Forms;

/// <summary>Provides methods and fields to manage the input language. This class cannot be inherited.</summary>
/// <filterpriority>2</filterpriority>
public sealed class InputLanguage
{
	private static InputLanguageCollection all;

	private IntPtr handle;

	private CultureInfo culture;

	private string layout_name;

	private static InputLanguage current_input;

	private static InputLanguage default_input;

	/// <summary>Gets or sets the input language for the current thread.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.InputLanguage" /> that represents the input language for the current thread.</returns>
	/// <exception cref="T:System.ArgumentException">The input language is not recognized by the system.</exception>
	/// <filterpriority>1</filterpriority>
	public static InputLanguage CurrentInputLanguage
	{
		get
		{
			if (current_input == null)
			{
				current_input = FromCulture(CultureInfo.CurrentUICulture);
			}
			return current_input;
		}
		set
		{
			if (InstalledInputLanguages.Contains(value))
			{
				current_input = value;
			}
		}
	}

	/// <summary>Gets the default input language for the system.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.InputLanguage" /> representing the default input language for the system.</returns>
	/// <filterpriority>1</filterpriority>
	public static InputLanguage DefaultInputLanguage
	{
		get
		{
			if (default_input == null)
			{
				default_input = FromCulture(CultureInfo.CurrentUICulture);
			}
			return default_input;
		}
	}

	/// <summary>Gets a list of all installed input languages.</summary>
	/// <returns>An array of <see cref="T:System.Windows.Forms.InputLanguage" /> objects that represent the input languages installed on the computer.</returns>
	/// <filterpriority>1</filterpriority>
	public static InputLanguageCollection InstalledInputLanguages
	{
		get
		{
			if (all == null)
			{
				all = new InputLanguageCollection(new InputLanguage[1]
				{
					new InputLanguage(IntPtr.Zero, new CultureInfo(string.Empty), "US")
				});
			}
			return all;
		}
	}

	/// <summary>Gets the culture of the current input language.</summary>
	/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> that represents the culture of the current input language.</returns>
	/// <filterpriority>1</filterpriority>
	public CultureInfo Culture => culture;

	/// <summary>Gets the handle for the input language.</summary>
	/// <returns>An <see cref="T:System.IntPtr" /> that represents the handle of this input language.</returns>
	/// <filterpriority>1</filterpriority>
	public IntPtr Handle => handle;

	/// <summary>Gets the name of the current keyboard layout as it appears in the regional settings of the operating system on the computer.</summary>
	/// <returns>The name of the layout.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	public string LayoutName => layout_name;

	[System.MonoInternalNote("Pull Microsofts InputLanguages and enter them here")]
	internal InputLanguage()
	{
	}

	internal InputLanguage(IntPtr handle, CultureInfo culture, string layout_name)
		: this()
	{
		this.handle = handle;
		this.culture = culture;
		this.layout_name = layout_name;
	}

	/// <summary>Returns the input language associated with the specified culture.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.InputLanguage" /> that represents the previously selected input language.</returns>
	/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> that specifies the culture to convert from. </param>
	/// <filterpriority>1</filterpriority>
	public static InputLanguage FromCulture(CultureInfo culture)
	{
		foreach (InputLanguage installedInputLanguage in InstalledInputLanguages)
		{
			if (culture.EnglishName == installedInputLanguage.culture.EnglishName)
			{
				return new InputLanguage(installedInputLanguage.handle, installedInputLanguage.culture, installedInputLanguage.layout_name);
			}
		}
		return new InputLanguage(InstalledInputLanguages[0].handle, InstalledInputLanguages[0].culture, InstalledInputLanguages[0].layout_name);
	}

	/// <summary>Specifies whether two input languages are equal.</summary>
	/// <returns>true if the two languages are equal; otherwise, false.</returns>
	/// <param name="value">The language to test for equality. </param>
	/// <filterpriority>1</filterpriority>
	public override bool Equals(object value)
	{
		if (value is InputLanguage && ((InputLanguage)value).culture == culture && ((InputLanguage)value).handle == handle && ((InputLanguage)value).layout_name == layout_name)
		{
			return true;
		}
		return false;
	}

	/// <summary>Returns the hash code for this input language.</summary>
	/// <returns>The hash code for this input language.</returns>
	/// <filterpriority>1</filterpriority>
	public override int GetHashCode()
	{
		return base.GetHashCode();
	}
}
