using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Provides <see cref="T:System.Drawing.Color" /> structures that are colors of a Windows display element. This class cannot be inherited. </summary>
public sealed class ProfessionalColors
{
	private static ProfessionalColorTable color_table = new ProfessionalColorTable();

	/// <summary>Gets the starting color of the gradient used when the button is checked.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the gradient used when the button is checked.</returns>
	public static Color ButtonCheckedGradientBegin => color_table.ButtonCheckedGradientBegin;

	/// <summary>Gets the end color of the gradient used when the button is checked.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used when the button is checked.</returns>
	public static Color ButtonCheckedGradientEnd => color_table.ButtonCheckedGradientEnd;

	/// <summary>Gets the middle color of the gradient used when the button is checked.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used when the button is checked.</returns>
	public static Color ButtonCheckedGradientMiddle => color_table.ButtonCheckedGradientMiddle;

	/// <summary>Gets the solid color used when the button is checked.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid color used when the button is checked.</returns>
	public static Color ButtonCheckedHighlight => color_table.ButtonCheckedHighlight;

	/// <summary>Gets the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonCheckedHighlight" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonCheckedHighlight" />.</returns>
	public static Color ButtonCheckedHighlightBorder => color_table.ButtonCheckedHighlightBorder;

	/// <summary>Gets the border color to use with the <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonPressedGradientBegin" />, <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonPressedGradientMiddle" />, and <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonPressedGradientEnd" /> colors.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use with the <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonPressedGradientBegin" />, <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonPressedGradientMiddle" />, and <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonPressedGradientEnd" /> colors.</returns>
	public static Color ButtonPressedBorder => color_table.ButtonPressedBorder;

	/// <summary>Gets the starting color of the gradient used when the button is pressed down.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used when the button is pressed down.</returns>
	public static Color ButtonPressedGradientBegin => color_table.ButtonPressedGradientBegin;

	/// <summary>Gets the end color of the gradient used when the button is pressed down.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used when the button is pressed down.</returns>
	public static Color ButtonPressedGradientEnd => color_table.ButtonPressedGradientEnd;

	/// <summary>Gets the middle color of the gradient used when the button is pressed down.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used when the button is pressed.</returns>
	public static Color ButtonPressedGradientMiddle => color_table.ButtonPressedGradientMiddle;

	/// <summary>Gets the solid color used when the button is pressed down.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid color used when the button is pressed down.</returns>
	public static Color ButtonPressedHighlight => color_table.ButtonPressedHighlight;

	/// <summary>Gets the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonPressedHighlight" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonPressedHighlight" />.</returns>
	public static Color ButtonPressedHighlightBorder => color_table.ButtonPressedHighlightBorder;

	/// <summary>Gets the border color to use with the <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonSelectedGradientBegin" />, <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonSelectedGradientMiddle" />, and <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonSelectedGradientEnd" /> colors.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use with the <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonSelectedGradientBegin" />, <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonSelectedGradientMiddle" />, and <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonSelectedGradientEnd" /> colors.</returns>
	public static Color ButtonSelectedBorder => color_table.ButtonSelectedBorder;

	/// <summary>Gets the starting color of the gradient used when the button is selected.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used when the button is selected.</returns>
	public static Color ButtonSelectedGradientBegin => color_table.ButtonSelectedGradientBegin;

	/// <summary>Gets the end color of the gradient used when the button is selected.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used when the button is selected.</returns>
	public static Color ButtonSelectedGradientEnd => color_table.ButtonSelectedGradientEnd;

	/// <summary>Gets the middle color of the gradient used when the button is selected.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used when the button is selected.</returns>
	public static Color ButtonSelectedGradientMiddle => color_table.ButtonSelectedGradientMiddle;

	/// <summary>Gets the solid color used when the button is selected.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid color used when the button is selected.</returns>
	public static Color ButtonSelectedHighlight => color_table.ButtonSelectedHighlight;

	/// <summary>Gets the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonSelectedHighlight" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonSelectedHighlight" />.</returns>
	public static Color ButtonSelectedHighlightBorder => color_table.ButtonSelectedHighlightBorder;

	/// <summary>Gets the solid color to use when the check box is selected and gradients are being used.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid color to use when the check box is selected and gradients are being used.</returns>
	public static Color CheckBackground => color_table.CheckBackground;

	/// <summary>Gets the solid color to use when the check box is selected and gradients are being used.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid color to use when the check box is selected and gradients are being used.</returns>
	public static Color CheckPressedBackground => color_table.CheckPressedBackground;

	/// <summary>Gets the solid color to use when the check box is selected and gradients are being used.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid color to use when the check box is selected and gradients are being used.</returns>
	public static Color CheckSelectedBackground => color_table.CheckSelectedBackground;

	/// <summary>Gets the color to use for shadow effects on the grip or move handle.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color to use for shadow effects on the grip or move handle.</returns>
	public static Color GripDark => color_table.GripDark;

	/// <summary>Gets the color to use for highlight effects on the grip or move handle.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color to use for highlight effects on the grip or move handle.</returns>
	public static Color GripLight => color_table.GripLight;

	/// <summary>Gets the starting color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</returns>
	public static Color ImageMarginGradientBegin => color_table.ImageMarginGradientBegin;

	/// <summary>Gets the end color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</returns>
	public static Color ImageMarginGradientEnd => color_table.ImageMarginGradientEnd;

	/// <summary>Gets the middle color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</returns>
	public static Color ImageMarginGradientMiddle => color_table.ImageMarginGradientMiddle;

	/// <summary>Gets the starting color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> when an item is revealed.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> when an item is revealed.</returns>
	public static Color ImageMarginRevealedGradientBegin => color_table.ImageMarginRevealedGradientBegin;

	/// <summary>Gets the end color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> when an item is revealed.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> when an item is revealed.</returns>
	public static Color ImageMarginRevealedGradientEnd => color_table.ImageMarginRevealedGradientEnd;

	/// <summary>Gets the middle color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> when an item is revealed.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> when an item is revealed.</returns>
	public static Color ImageMarginRevealedGradientMiddle => color_table.ImageMarginRevealedGradientMiddle;

	/// <summary>Gets the border color or a <see cref="T:System.Windows.Forms.MenuStrip" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color or a <see cref="T:System.Windows.Forms.MenuStrip" />.</returns>
	public static Color MenuBorder => color_table.MenuBorder;

	/// <summary>Gets the border color to use with a <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use with a <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</returns>
	public static Color MenuItemBorder => color_table.MenuItemBorder;

	/// <summary>Gets the starting color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is pressed down.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is pressed down.</returns>
	public static Color MenuItemPressedGradientBegin => color_table.MenuItemPressedGradientBegin;

	/// <summary>Gets the end color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is pressed down.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is pressed down.</returns>
	public static Color MenuItemPressedGradientEnd => color_table.MenuItemPressedGradientEnd;

	/// <summary>Gets the middle color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is pressed down.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is pressed down.</returns>
	public static Color MenuItemPressedGradientMiddle => color_table.MenuItemPressedGradientMiddle;

	/// <summary>Gets the solid color to use when a <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> other than the top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is selected.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid color to use when a <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> other than the top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is selected.</returns>
	public static Color MenuItemSelected => color_table.MenuItemSelected;

	/// <summary>Gets the starting color of the gradient used when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is selected.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is selected.</returns>
	public static Color MenuItemSelectedGradientBegin => color_table.MenuItemSelectedGradientBegin;

	/// <summary>Gets the end color of the gradient used when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is selected.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is selected.</returns>
	public static Color MenuItemSelectedGradientEnd => color_table.MenuItemSelectedGradientEnd;

	/// <summary>Gets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.MenuStrip" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.MenuStrip" />.</returns>
	public static Color MenuStripGradientBegin => color_table.MenuStripGradientBegin;

	/// <summary>Gets the end color of the gradient used in the <see cref="T:System.Windows.Forms.MenuStrip" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.MenuStrip" />.</returns>
	public static Color MenuStripGradientEnd => color_table.MenuStripGradientEnd;

	/// <summary>Gets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</returns>
	public static Color OverflowButtonGradientBegin => color_table.OverflowButtonGradientBegin;

	/// <summary>Gets the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</returns>
	public static Color OverflowButtonGradientEnd => color_table.OverflowButtonGradientEnd;

	/// <summary>Gets the middle color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</returns>
	public static Color OverflowButtonGradientMiddle => color_table.OverflowButtonGradientMiddle;

	/// <summary>Gets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</returns>
	public static Color RaftingContainerGradientBegin => color_table.RaftingContainerGradientBegin;

	/// <summary>Gets the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</returns>
	public static Color RaftingContainerGradientEnd => color_table.RaftingContainerGradientEnd;

	/// <summary>Gets the color to use to for shadow effects on the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color to use to for shadow effects on the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</returns>
	public static Color SeparatorDark => color_table.SeparatorDark;

	/// <summary>Gets the color to use to for highlight effects on the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color to use to create highlight effects on the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</returns>
	public static Color SeparatorLight => color_table.SeparatorLight;

	/// <summary>Gets the starting color of the gradient used on the <see cref="T:System.Windows.Forms.StatusStrip" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used on the <see cref="T:System.Windows.Forms.StatusStrip" />.</returns>
	public static Color StatusStripGradientBegin => color_table.StatusStripGradientBegin;

	/// <summary>Gets the end color of the gradient used on the <see cref="T:System.Windows.Forms.StatusStrip" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used on the <see cref="T:System.Windows.Forms.StatusStrip" />.</returns>
	public static Color StatusStripGradientEnd => color_table.StatusStripGradientEnd;

	/// <summary>Gets the border color to use on the bottom edge of the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use on the bottom edge of the <see cref="T:System.Windows.Forms.ToolStrip" />.</returns>
	public static Color ToolStripBorder => color_table.ToolStripBorder;

	/// <summary>Gets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</returns>
	public static Color ToolStripContentPanelGradientBegin => color_table.ToolStripContentPanelGradientBegin;

	/// <summary>Gets the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</returns>
	public static Color ToolStripContentPanelGradientEnd => color_table.ToolStripContentPanelGradientEnd;

	/// <summary>Gets the solid background color of the <see cref="T:System.Windows.Forms.ToolStripDropDown" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid background color of the <see cref="T:System.Windows.Forms.ToolStripDropDown" />.</returns>
	public static Color ToolStripDropDownBackground => color_table.ToolStripDropDownBackground;

	/// <summary>Gets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip" /> background.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip" /> background.</returns>
	public static Color ToolStripGradientBegin => color_table.ToolStripGradientBegin;

	/// <summary>Gets the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip" /> background.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip" /> background.</returns>
	public static Color ToolStripGradientEnd => color_table.ToolStripGradientEnd;

	/// <summary>Gets the middle color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip" /> background.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip" /> background.</returns>
	public static Color ToolStripGradientMiddle => color_table.ToolStripGradientMiddle;

	/// <summary>Gets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</returns>
	public static Color ToolStripPanelGradientBegin => color_table.ToolStripPanelGradientBegin;

	/// <summary>Gets the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</returns>
	public static Color ToolStripPanelGradientEnd => color_table.ToolStripPanelGradientEnd;

	private ProfessionalColors()
	{
	}
}
