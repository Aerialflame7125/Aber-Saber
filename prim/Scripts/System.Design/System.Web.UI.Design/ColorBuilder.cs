using System.ComponentModel;
using System.Windows.Forms;

namespace System.Web.UI.Design;

/// <summary>Provides an HTML color string builder at design time that allows a user to select a color.</summary>
public sealed class ColorBuilder
{
	private ColorBuilder()
	{
	}

	/// <summary>Starts a color editor to build an HTML color property value.</summary>
	/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> whose site is to be used to access design-time services.</param>
	/// <param name="owner">The <see cref="T:System.Web.UI.Control" /> used to parent the picker window.</param>
	/// <param name="initialColor">The initial color to be shown in the picker window, in a valid HTML color format.</param>
	/// <returns>The color value, represented as a string in an HTML color format, or <see langword="null" /> if the builder service could not be retrieved.</returns>
	[System.MonoTODO]
	public static string BuildColor(IComponent component, System.Windows.Forms.Control owner, string initialColor)
	{
		throw new NotImplementedException();
	}
}
