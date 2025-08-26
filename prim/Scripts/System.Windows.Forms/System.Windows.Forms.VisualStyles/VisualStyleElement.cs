namespace System.Windows.Forms.VisualStyles;

/// <summary>Identifies a control or user interface (UI) element that is drawn with visual styles.</summary>
public class VisualStyleElement
{
	private enum DATEPICKERPARTS
	{
		DP_DATEBORDER = 2,
		DP_SHOWCALENDARBUTTONRIGHT
	}

	private enum DATEBORDERSTATES
	{
		DPDB_NORMAL = 1,
		DPDB_HOT,
		DPDB_FOCUSED,
		DPDB_DISABLED
	}

	private enum SHOWCALENDARBUTTONRIGHTSTATES
	{
		DPSCBR_NORMAL = 1,
		DPSCBR_HOT,
		DPSCBR_PRESSED,
		DPSCBR_DISABLED
	}

	/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for button-related controls. This class cannot be inherited. </summary>
	public static class Button
	{
		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the different states of the check box control. This class cannot be inherited. </summary>
		public static class CheckBox
		{
			/// <summary>Gets a visual style element that represents a disabled check box in the checked state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled check box in the checked state.</returns>
			public static VisualStyleElement CheckedDisabled => CreateElement("BUTTON", 3, 8);

			/// <summary>Gets a visual style element that represents a hot check box in the checked state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot check box in the checked state.</returns>
			public static VisualStyleElement CheckedHot => CreateElement("BUTTON", 3, 6);

			/// <summary>Gets a visual style element that represents a normal check box in the checked state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal check box in the checked state.</returns>
			public static VisualStyleElement CheckedNormal => CreateElement("BUTTON", 3, 5);

			/// <summary>Gets a visual style element that represents a pressed check box in the checked state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed check box in the checked state.</returns>
			public static VisualStyleElement CheckedPressed => CreateElement("BUTTON", 3, 7);

			/// <summary>Gets a visual style element that represents a disabled check box in the indeterminate state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled check box in the indeterminate state.</returns>
			public static VisualStyleElement MixedDisabled => CreateElement("BUTTON", 3, 12);

			/// <summary>Gets a visual style element that represents a hot check box in the indeterminate state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot check box in the indeterminate state.</returns>
			public static VisualStyleElement MixedHot => CreateElement("BUTTON", 3, 10);

			/// <summary>Gets a visual style element that represents a normal check box in the indeterminate state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal check box in the indeterminate state.</returns>
			public static VisualStyleElement MixedNormal => CreateElement("BUTTON", 3, 9);

			/// <summary>Gets a visual style element that represents a pressed check box in the indeterminate state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed check box in the indeterminate state.</returns>
			public static VisualStyleElement MixedPressed => CreateElement("BUTTON", 3, 11);

			/// <summary>Gets a visual style element that represents a disabled check box in the unchecked state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled check box in the unchecked state.</returns>
			public static VisualStyleElement UncheckedDisabled => CreateElement("BUTTON", 3, 4);

			/// <summary>Gets a visual style element that represents a hot check box in the unchecked state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot check box in the unchecked state.</returns>
			public static VisualStyleElement UncheckedHot => CreateElement("BUTTON", 3, 2);

			/// <summary>Gets a visual style element that represents a normal check box in the unchecked state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal check box in the unchecked state.</returns>
			public static VisualStyleElement UncheckedNormal => CreateElement("BUTTON", 3, 1);

			/// <summary>Gets a visual style element that represents a pressed check box in the unchecked state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed check box in the unchecked state. </returns>
			public static VisualStyleElement UncheckedPressed => CreateElement("BUTTON", 3, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the different states of the group box control. This class cannot be inherited. </summary>
		public static class GroupBox
		{
			/// <summary>Gets a visual style element that represents a disabled group box.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled group box.</returns>
			public static VisualStyleElement Disabled => CreateElement("BUTTON", 4, 2);

			/// <summary>Gets a visual style element that represents a normal group box.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal group box.</returns>
			public static VisualStyleElement Normal => CreateElement("BUTTON", 4, 1);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the different states of the button control. This class cannot be inherited. </summary>
		public static class PushButton
		{
			/// <summary>Gets a visual style element that represents a default button.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a default button.</returns>
			public static VisualStyleElement Default => CreateElement("BUTTON", 1, 5);

			/// <summary>Gets a visual style element that represents a disabled button.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled button.</returns>
			public static VisualStyleElement Disabled => CreateElement("BUTTON", 1, 4);

			/// <summary>Gets a visual style element that represents a hot button.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot button. </returns>
			public static VisualStyleElement Hot => CreateElement("BUTTON", 1, 2);

			/// <summary>Gets a visual style element that represents a normal button.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal button.</returns>
			public static VisualStyleElement Normal => CreateElement("BUTTON", 1, 1);

			/// <summary>Gets a visual style element that represents a pressed button.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed button.</returns>
			public static VisualStyleElement Pressed => CreateElement("BUTTON", 1, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the different states of the radio button control. This class cannot be inherited. </summary>
		public static class RadioButton
		{
			/// <summary>Gets a visual style element that represents a disabled radio button in the checked state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled radio button in the checked state.</returns>
			public static VisualStyleElement CheckedDisabled => CreateElement("BUTTON", 2, 8);

			/// <summary>Gets a visual style element that represents a hot radio button in the checked state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot radio button in the checked state.</returns>
			public static VisualStyleElement CheckedHot => CreateElement("BUTTON", 2, 6);

			/// <summary>Gets a visual style element that represents a normal radio button in the checked state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal radio button in the checked state.</returns>
			public static VisualStyleElement CheckedNormal => CreateElement("BUTTON", 2, 5);

			/// <summary>Gets a visual style element that represents a pressed radio button in the checked state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed radio button in the checked state.</returns>
			public static VisualStyleElement CheckedPressed => CreateElement("BUTTON", 2, 7);

			/// <summary>Gets a visual style element that represents a disabled radio button in the unchecked state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled radio button in the unchecked state.</returns>
			public static VisualStyleElement UncheckedDisabled => CreateElement("BUTTON", 2, 4);

			/// <summary>Gets a visual style element that represents a hot radio button in the unchecked state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot radio button in the unchecked state.</returns>
			public static VisualStyleElement UncheckedHot => CreateElement("BUTTON", 2, 2);

			/// <summary>Gets a visual style element that represents a normal radio button in the unchecked state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal radio button in the unchecked state.</returns>
			public static VisualStyleElement UncheckedNormal => CreateElement("BUTTON", 2, 1);

			/// <summary>Gets a visual style element that represents a pressed radio button in the unchecked state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed radio button in the unchecked state. </returns>
			public static VisualStyleElement UncheckedPressed => CreateElement("BUTTON", 2, 3);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a user button. This class cannot be inherited.</summary>
		public static class UserButton
		{
			/// <summary>Gets a visual style element that represents a user button.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a user button. </returns>
			public static VisualStyleElement Normal => CreateElement("BUTTON", 5, 0);
		}
	}

	/// <summary>Contains a class that provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the drop-down arrow of the combo box control. This class cannot be inherited.</summary>
	public static class ComboBox
	{
		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the different states of the drop-down arrow of the combo box control. This class cannot be inherited. </summary>
		public static class DropDownButton
		{
			/// <summary>Gets a visual style element that represents a drop-down arrow in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down arrow in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("COMBOBOX", 1, 4);

			/// <summary>Gets a visual style element that represents a drop-down arrow in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down arrow in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("COMBOBOX", 1, 2);

			/// <summary>Gets a visual style element that represents a drop-down arrow in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down arrow in the normal state. </returns>
			public static VisualStyleElement Normal => CreateElement("COMBOBOX", 1, 1);

			/// <summary>Gets a visual style element that represents a drop-down arrow in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down arrow in the pressed state.</returns>
			public static VisualStyleElement Pressed => CreateElement("COMBOBOX", 1, 3);
		}

		internal static class Border
		{
			public static VisualStyleElement Normal => new VisualStyleElement("COMBOBOX", 4, 1);

			public static VisualStyleElement Hot => new VisualStyleElement("COMBOBOX", 4, 2);

			public static VisualStyleElement Focused => new VisualStyleElement("COMBOBOX", 4, 3);

			public static VisualStyleElement Disabled => new VisualStyleElement("COMBOBOX", 4, 4);
		}
	}

	internal static class DatePicker
	{
		public static class DateBorder
		{
			public static VisualStyleElement Normal => new VisualStyleElement("DATEPICKER", 2, 1);

			public static VisualStyleElement Hot => new VisualStyleElement("DATEPICKER", 2, 2);

			public static VisualStyleElement Focused => new VisualStyleElement("DATEPICKER", 2, 3);

			public static VisualStyleElement Disabled => new VisualStyleElement("DATEPICKER", 2, 4);
		}

		public static class ShowCalendarButtonRight
		{
			public static VisualStyleElement Normal => new VisualStyleElement("DATEPICKER", 3, 1);

			public static VisualStyleElement Hot => new VisualStyleElement("DATEPICKER", 3, 2);

			public static VisualStyleElement Pressed => new VisualStyleElement("DATEPICKER", 3, 3);

			public static VisualStyleElement Disabled => new VisualStyleElement("DATEPICKER", 3, 4);
		}
	}

	/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each part of the Explorer Bar. This class cannot be inherited.</summary>
	public static class ExplorerBar
	{
		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of the Explorer Bar. This class cannot be inherited. </summary>
		public static class HeaderBackground
		{
			/// <summary>Gets a visual style element that represents the background of the Explorer Bar.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of the Explorer Bar. </returns>
			public static VisualStyleElement Normal => CreateElement("EXPLORERBAR", 1, 0);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Close button of the Explorer Bar. This class cannot be inherited.</summary>
		public static class HeaderClose
		{
			/// <summary>Gets a visual style element that represents a Close button in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Close button in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("EXPLORERBAR", 2, 1);

			/// <summary>Gets a visual style element that represents a Close button in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Close button in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("EXPLORERBAR", 2, 2);

			/// <summary>Gets a visual style element that represents a Close button in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Close button in the pressed state. </returns>
			public static VisualStyleElement Pressed => CreateElement("EXPLORERBAR", 2, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Auto Hide button (which is displayed as a push pin) of the Explorer Bar. This class cannot be inherited.</summary>
		public static class HeaderPin
		{
			/// <summary>Gets a visual style element that represents an Auto Hide button (which is displayed as a push pin) in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an Auto Hide button in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("EXPLORERBAR", 3, 2);

			/// <summary>Gets a visual style element that represents an Auto Hide button (which is displayed as a push pin) in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an Auto Hide button in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("EXPLORERBAR", 3, 1);

			/// <summary>Gets a visual style element that represents an Auto Hide button (which is displayed as a push pin) in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an Auto Hide button in the pressed state.</returns>
			public static VisualStyleElement Pressed => CreateElement("EXPLORERBAR", 3, 3);

			/// <summary>Gets a visual style element that represents a selected Auto Hide button (which is displayed as a push pin) in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a selected Auto Hide button in the hot state.</returns>
			public static VisualStyleElement SelectedHot => CreateElement("EXPLORERBAR", 3, 5);

			/// <summary>Gets a visual style element that represents a selected Auto Hide button (which is displayed as a push pin) in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a selected Auto Hide button in the normal state.</returns>
			public static VisualStyleElement SelectedNormal => CreateElement("EXPLORERBAR", 3, 4);

			/// <summary>Gets a visual style element that represents a selected Auto Hide button (which is displayed as a push pin) in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a selected Auto Hide button in the pressed state.</returns>
			public static VisualStyleElement SelectedPressed => CreateElement("EXPLORERBAR", 3, 6);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the expanded-menu arrow of the Explorer Bar. This class cannot be inherited.</summary>
		public static class IEBarMenu
		{
			/// <summary>Gets a visual style element that represents a hot menu button.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot menu button.</returns>
			public static VisualStyleElement Hot => CreateElement("EXPLORERBAR", 4, 2);

			/// <summary>Gets a visual style element that represents a normal menu button.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal menu button.</returns>
			public static VisualStyleElement Normal => CreateElement("EXPLORERBAR", 4, 1);

			/// <summary>Gets a visual style element that represents a pressed menu button.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed menu button. </returns>
			public static VisualStyleElement Pressed => CreateElement("EXPLORERBAR", 4, 3);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of a common group of items in the Explorer Bar. This class cannot be inherited.</summary>
		public static class NormalGroupBackground
		{
			/// <summary>Gets a visual style element that represents the background of a common group of items in the Explorer Bar.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of a common group of items in the Explorer Bar. </returns>
			public static VisualStyleElement Normal => CreateElement("EXPLORERBAR", 5, 0);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the collapse button of a common group of items in the Explorer Bar. This class cannot be inherited.</summary>
		public static class NormalGroupCollapse
		{
			/// <summary>Gets a visual style element that represents a hot collapse button of a common group of items in the Explorer Bar.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot collapse button.</returns>
			public static VisualStyleElement Hot => CreateElement("EXPLORERBAR", 6, 2);

			/// <summary>Gets a visual style element that represents a normal collapse button of a common group of items in the Explorer Bar.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal collapse button.</returns>
			public static VisualStyleElement Normal => CreateElement("EXPLORERBAR", 6, 1);

			/// <summary>Gets a visual style element that represents a pressed collapse button of a common group of items in the Explorer Bar.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed collapse button.</returns>
			public static VisualStyleElement Pressed => CreateElement("EXPLORERBAR", 6, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the expand button of a common group of items in the Explorer Bar. This class cannot be inherited.</summary>
		public static class NormalGroupExpand
		{
			/// <summary>Gets a visual style element that represents a hot expand button of a common group of items in the Explorer Bar.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot expand button.</returns>
			public static VisualStyleElement Hot => CreateElement("EXPLORERBAR", 7, 2);

			/// <summary>Gets a visual style element that represents a normal expand button of a common group of items in the Explorer Bar.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal expand button.</returns>
			public static VisualStyleElement Normal => CreateElement("EXPLORERBAR", 7, 1);

			/// <summary>Gets a visual style element that represents a pressed expand button of a common group of items in the Explorer Bar.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed expand button. </returns>
			public static VisualStyleElement Pressed => CreateElement("EXPLORERBAR", 7, 3);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the title bar of a common group of items in the Explorer Bar. This class cannot be inherited.</summary>
		public static class NormalGroupHead
		{
			/// <summary>Gets a visual style element that represents the title bar of a common group of items in the Explorer Bar.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a common group of items in the Explorer Bar. </returns>
			public static VisualStyleElement Normal => CreateElement("EXPLORERBAR", 8, 0);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of a special group of items in the Explorer Bar. This class cannot be inherited.</summary>
		public static class SpecialGroupBackground
		{
			/// <summary>Gets a visual style element that represents the background of a special group of items in the Explorer Bar.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of a special group of items in the Explorer Bar. </returns>
			public static VisualStyleElement Normal => CreateElement("EXPLORERBAR", 9, 0);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the collapse button of a special group of items in the Explorer Bar. This class cannot be inherited.</summary>
		public static class SpecialGroupCollapse
		{
			/// <summary>Gets a visual style element that represents a hot collapse button of a special group of items in the Explorer Bar.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot collapse button.</returns>
			public static VisualStyleElement Hot => CreateElement("EXPLORERBAR", 10, 2);

			/// <summary>Gets a visual style element that represents a normal collapse button of a special group of items in the Explorer Bar.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal collapse button.</returns>
			public static VisualStyleElement Normal => CreateElement("EXPLORERBAR", 10, 1);

			/// <summary>Gets a visual style element that represents a pressed collapse button of a special group of items in the Explorer Bar. </summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed collapse button.</returns>
			public static VisualStyleElement Pressed => CreateElement("EXPLORERBAR", 10, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the expand button of a special group of items in the Explorer Bar. This class cannot be inherited.</summary>
		public static class SpecialGroupExpand
		{
			/// <summary>Gets a visual style element that represents a hot expand button of a special group of items in the Explorer Bar.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot expand button.</returns>
			public static VisualStyleElement Hot => CreateElement("EXPLORERBAR", 11, 2);

			/// <summary>Gets a visual style element that represents a normal expand button of a special group of items in the Explorer Bar.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal expand button.</returns>
			public static VisualStyleElement Normal => CreateElement("EXPLORERBAR", 11, 1);

			/// <summary>Gets a visual style element that represents a pressed expand button of a special group of items in the Explorer Bar.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed expand button. </returns>
			public static VisualStyleElement Pressed => CreateElement("EXPLORERBAR", 11, 3);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the title bar of a special group of items in the Explorer Bar. This class cannot be inherited.</summary>
		public static class SpecialGroupHead
		{
			/// <summary>Gets a visual style element that represents the title bar of a special group of items in the Explorer Bar.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a special group of items in the Explorer Bar. </returns>
			public static VisualStyleElement Normal => CreateElement("EXPLORERBAR", 12, 0);
		}
	}

	/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each part of the header control. This class cannot be inherited.</summary>
	public static class Header
	{
		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of an item of the header control. This class cannot be inherited. </summary>
		public static class Item
		{
			/// <summary>Gets a visual style element that represents a hot header item.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot header item.</returns>
			public static VisualStyleElement Hot => CreateElement("HEADER", 1, 2);

			/// <summary>Gets a visual style element that represents a normal header item.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal header item.</returns>
			public static VisualStyleElement Normal => CreateElement("HEADER", 1, 1);

			/// <summary>Gets a visual style element that represents a pressed header item.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed header item. </returns>
			public static VisualStyleElement Pressed => CreateElement("HEADER", 1, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the leftmost item of the header control. This class cannot be inherited. </summary>
		public static class ItemLeft
		{
			/// <summary>Gets a visual style element that represents the leftmost header item in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the leftmost header item in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("HEADER", 2, 2);

			/// <summary>Gets a visual style element that represents the leftmost header item in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the leftmost header item in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("HEADER", 2, 1);

			/// <summary>Gets a visual style element that represents the leftmost header item in the pressed state. </summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the leftmost header item in the pressed state.</returns>
			public static VisualStyleElement Pressed => CreateElement("HEADER", 2, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the rightmost item of the header control. This class cannot be inherited. </summary>
		public static class ItemRight
		{
			/// <summary>Gets a visual style element that represents the rightmost header item in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the rightmost header item in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("HEADER", 3, 2);

			/// <summary>Gets a visual style element that represents the rightmost header item in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the rightmost header item in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("HEADER", 3, 1);

			/// <summary>Gets a visual style element that represents the rightmost header item in the pressed state. </summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the rightmost header item in the pressed state.</returns>
			public static VisualStyleElement Pressed => CreateElement("HEADER", 3, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the sort arrow of a header item. This class cannot be inherited. </summary>
		public static class SortArrow
		{
			/// <summary>Gets a visual style element that represents a downward-pointing sort arrow.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing sort arrow.</returns>
			public static VisualStyleElement SortedDown => CreateElement("HEADER", 4, 2);

			/// <summary>Gets a visual style element that represents an upward-pointing sort arrow.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing sort arrow. </returns>
			public static VisualStyleElement SortedUp => CreateElement("HEADER", 4, 1);
		}
	}

	/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of the list view control. This class cannot be inherited.</summary>
	public static class ListView
	{
		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a list view in detail view. This class cannot be inherited.</summary>
		public static class Detail
		{
			/// <summary>Gets a visual style element that represents a list view in detail view. </summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a list view in detail view.</returns>
			public static VisualStyleElement Normal => CreateElement("LISTVIEW", 3, 0);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the text area of a list view that contains no items. This class cannot be inherited.</summary>
		public static class EmptyText
		{
			/// <summary>Gets a visual style element that represents the text area of a list view that contains no items. </summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the text area that accompanies an empty list view.</returns>
			public static VisualStyleElement Normal => CreateElement("LISTVIEW", 5, 0);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a list view item group. This class cannot be inherited.</summary>
		public static class Group
		{
			/// <summary>Gets a visual style element that represents a list view item group. </summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a group of list view items.</returns>
			public static VisualStyleElement Normal => CreateElement("LISTVIEW", 2, 0);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of an item of the list view control. This class cannot be inherited. </summary>
		public static class Item
		{
			/// <summary>Gets a visual style element that represents a disabled list view item.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled list view item.</returns>
			public static VisualStyleElement Disabled => CreateElement("LISTVIEW", 1, 4);

			/// <summary>Gets a visual style element that represents a hot list view item.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot list view item.</returns>
			public static VisualStyleElement Hot => CreateElement("LISTVIEW", 1, 2);

			/// <summary>Gets a visual style element that represents a normal list view item.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal list view item.</returns>
			public static VisualStyleElement Normal => CreateElement("LISTVIEW", 1, 1);

			/// <summary>Gets a visual style element that represents a selected list view item that has focus.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a selected list view item that has focus.</returns>
			public static VisualStyleElement Selected => CreateElement("LISTVIEW", 1, 3);

			/// <summary>Gets a visual style element that represents a selected list view item without focus.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a selected list view item without focus. </returns>
			public static VisualStyleElement SelectedNotFocus => CreateElement("LISTVIEW", 1, 5);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a sorted list view control in detail view This class cannot be inherited.</summary>
		public static class SortedDetail
		{
			/// <summary>Gets a visual style element that represents a sorted list view control in detail view. </summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a sorted list view control in detail view.</returns>
			public static VisualStyleElement Normal => CreateElement("LISTVIEW", 4, 0);
		}
	}

	/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of a menu. This class cannot be inherited. </summary>
	public static class Menu
	{
		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the drop-down arrow of a menu bar. This class cannot be inherited. </summary>
		public static class BarDropDown
		{
			/// <summary>Gets a visual style element that represents the drop-down arrow of a menu bar. </summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the drop-down arrow of a menu bar.</returns>
			public static VisualStyleElement Normal => CreateElement("MENU", 4, 0);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a menu bar item. This class cannot be inherited. </summary>
		public static class BarItem
		{
			/// <summary>Gets a visual style element that represents a menu bar item. </summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a menu bar item.</returns>
			public static VisualStyleElement Normal => CreateElement("MENU", 3, 0);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the chevron of a menu. This class cannot be inherited. </summary>
		public static class Chevron
		{
			/// <summary>Gets a visual style element that represents a menu chevron.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a menu chevron. </returns>
			public static VisualStyleElement Normal => CreateElement("MENU", 5, 0);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the drop-down arrow of a menu. This class cannot be inherited. </summary>
		public static class DropDown
		{
			/// <summary>Gets a visual style element that represents the drop-down arrow of a menu. </summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the drop-down arrow of a menu.</returns>
			public static VisualStyleElement Normal => CreateElement("MENU", 2, 0);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a menu item. This class cannot be inherited. </summary>
		public static class Item
		{
			/// <summary>Gets a visual style element that represents a menu item that has been demoted.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a menu item that has been demoted.</returns>
			public static VisualStyleElement Demoted => CreateElement("MENU", 1, 3);

			/// <summary>Gets a visual style element that represents a menu item in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a menu item in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("MENU", 1, 1);

			/// <summary>Gets a visual style element that represents a menu item in the selected state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a menu item in the selected state.</returns>
			public static VisualStyleElement Selected => CreateElement("MENU", 1, 2);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a menu item separator. This class cannot be inherited. </summary>
		public static class Separator
		{
			/// <summary>Gets a visual style element that represents a menu item separator.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a menu item separator. </returns>
			public static VisualStyleElement Normal => CreateElement("MENU", 6, 0);
		}
	}

	/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of a menu band. This class cannot be inherited.</summary>
	public static class MenuBand
	{
		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the new application button of a menu band. This class cannot be inherited. </summary>
		public static class NewApplicationButton
		{
			/// <summary>Gets a visual style element that represents the new application button in the checked state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the new application button in the checked state.</returns>
			public static VisualStyleElement Checked => CreateElement("MENUBAND", 1, 5);

			/// <summary>Gets a visual style element that represents the new application button in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the new application button in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("MENUBAND", 1, 4);

			/// <summary>Gets a visual style element that represents the new application button in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the new application button in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("MENUBAND", 1, 2);

			/// <summary>Gets a visual style element that represents the new application button in the hot and checked states.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the new application button in the hot and checked states.</returns>
			public static VisualStyleElement HotChecked => CreateElement("MENUBAND", 1, 6);

			/// <summary>Gets a visual style element that represents the new application button in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the new application button in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("MENUBAND", 1, 1);

			/// <summary>Gets a visual style element that represents the new application button in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the new application button in the pressed state. </returns>
			public static VisualStyleElement Pressed => CreateElement("MENUBAND", 1, 3);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a menu band separator. This class cannot be inherited. </summary>
		public static class Separator
		{
			/// <summary>Gets a visual style element that represents a separator between items in a menu band.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a separator between items in a menu band.</returns>
			public static VisualStyleElement Normal => CreateElement("MENUBAND", 2, 0);
		}
	}

	/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of a page. This class cannot be inherited.</summary>
	public static class Page
	{
		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a down indicator in an up-down or spin box control. This class cannot be inherited. </summary>
		public static class Down
		{
			/// <summary>Gets a visual style element that represents the disabled state of the down indicator in an up-down or spin box control.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a down indicator of an up-down or spin box control in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("PAGE", 2, 4);

			/// <summary>Gets a visual style element that represents a down indicator of an up-down or spin box control in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the down indicator of an up-down or spin box in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("PAGE", 2, 2);

			/// <summary>Gets a visual style element that represents the down indicator of an up-down or spin box control in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a down indicator up an up-down or spin box control in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("PAGE", 2, 3);

			/// <summary>Gets a visual style element that represents the down indicator of an up-down or spin box in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a down indicator of an up-down or spin box in the pressed state. </returns>
			public static VisualStyleElement Pressed => CreateElement("PAGE", 2, 1);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a page backward indicator in a pager control. This class cannot be inherited. </summary>
		public static class DownHorizontal
		{
			/// <summary>Gets a visual style element that represents a page backward indicator of a pager control in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a page backward indicator of a pager control in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("PAGE", 4, 4);

			/// <summary>Gets a visual style element that represents a page backward indicator of a pager control in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a page backward indicator of a pager control in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("PAGE", 4, 2);

			/// <summary>Gets a visual style element that represents a page backward indicator of a pager control in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a page backward indicator of a pager control in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("PAGE", 4, 1);

			/// <summary>Gets a visual style element that represents a page backward indicator of a pager control in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents page backward indicator of a pager control in the pressed state. </returns>
			public static VisualStyleElement Pressed => CreateElement("PAGE", 4, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a page up indicator of an up-down or spin box control. This class cannot be inherited. </summary>
		public static class Up
		{
			/// <summary>Gets a visual style element that represents a page up indicator of an up-down or spin box control in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a page up indicator of an up-down or spin box control in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("PAGE", 1, 4);

			/// <summary>Gets a visual style element that represents a page up indicator of an up-down or spin box control in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a page up indicator of an up-down or spin box control in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("PAGE", 1, 2);

			/// <summary>Gets a visual style element that represents a page up indicator of an up-down or spin box control in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a page up indicator of an up-down or spin box control in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("PAGE", 1, 1);

			/// <summary>Gets a visual style element that represents a page up indicator of an up-down or spin box control in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a page up indicator of an up-down or spin box control in the pressed state. </returns>
			public static VisualStyleElement Pressed => CreateElement("PAGE", 1, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a page forward indicator of a pager control. This class cannot be inherited. </summary>
		public static class UpHorizontal
		{
			/// <summary>Gets a visual style element that represents a page forward indicator of a pager control in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a page forward indicator of a pager control in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("PAGE", 3, 4);

			/// <summary>Gets a visual style element that represents a page forward indicator of a pager control in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a page forward indicator of a pager control in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("PAGE", 3, 2);

			/// <summary>Gets a visual style element that represents a page forward indicator of a pager control in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a page forward indicator of a pager control in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("PAGE", 3, 1);

			/// <summary>Gets a visual style element that represents a page forward indicator of a pager control in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a page forward indicator of a pager control in the pressed state. </returns>
			public static VisualStyleElement Pressed => CreateElement("PAGE", 3, 3);
		}
	}

	/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of the progress bar control. This class cannot be inherited.</summary>
	public static class ProgressBar
	{
		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the frame of a horizontal progress bar. This class cannot be inherited.</summary>
		public static class Bar
		{
			/// <summary>Gets a visual style element that represents a horizontal progress bar frame.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal progress bar frame. </returns>
			public static VisualStyleElement Normal => CreateElement("PROGRESS", 1, 0);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the frame of a vertical progress bar. This class cannot be inherited.</summary>
		public static class BarVertical
		{
			/// <summary>Gets a visual style element that represents a vertical progress bar frame.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical progress bar frame.</returns>
			public static VisualStyleElement Normal => CreateElement("PROGRESS", 2, 0);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the pieces that fill a horizontal progress bar. This class cannot be inherited.</summary>
		public static class Chunk
		{
			/// <summary>Gets a visual style element that represents the pieces that fill a horizontal progress bar.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the pieces that fill a horizontal progress bar. </returns>
			public static VisualStyleElement Normal => CreateElement("PROGRESS", 3, 0);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the pieces that fill a vertical progress bar. This class cannot be inherited.</summary>
		public static class ChunkVertical
		{
			/// <summary>Gets a visual style element that represents the pieces that fill a vertical progress bar.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the pieces that fill a vertical progress bar. </returns>
			public static VisualStyleElement Normal => CreateElement("PROGRESS", 4, 0);
		}
	}

	/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of the rebar control. This class cannot be inherited.</summary>
	public static class Rebar
	{
		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a rebar band. This class cannot be inherited.</summary>
		public static class Band
		{
			/// <summary>Gets a visual style element that represents a rebar band. </summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a rebar band.</returns>
			public static VisualStyleElement Normal => CreateElement("REBAR", 3, 0);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a horizontal chevron. This class cannot be inherited. </summary>
		public static class Chevron
		{
			/// <summary>Gets a visual style element that represents a hot chevron.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot chevron.</returns>
			public static VisualStyleElement Hot => CreateElement("REBAR", 4, 2);

			/// <summary>Gets a visual style element that represents a normal chevron.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal chevron.</returns>
			public static VisualStyleElement Normal => CreateElement("REBAR", 4, 1);

			/// <summary>Gets a visual style element that represents a pressed chevron.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed chevron.</returns>
			public static VisualStyleElement Pressed => CreateElement("REBAR", 4, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a chevron. This class cannot be inherited. </summary>
		public static class ChevronVertical
		{
			/// <summary>Gets a visual style element that represents a hot chevron.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot chevron.</returns>
			public static VisualStyleElement Hot => CreateElement("REBAR", 5, 2);

			/// <summary>Gets a visual style element that represents a normal chevron.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal chevron.</returns>
			public static VisualStyleElement Normal => CreateElement("REBAR", 5, 1);

			/// <summary>Gets a visual style element that represents a pressed chevron.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed chevron. </returns>
			public static VisualStyleElement Pressed => CreateElement("REBAR", 5, 3);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the gripper bar of a horizontal rebar control. This class cannot be inherited.</summary>
		public static class Gripper
		{
			/// <summary>Gets a visual style element that represents a gripper bar for a horizontal rebar.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a gripper bar for a horizontal rebar. </returns>
			public static VisualStyleElement Normal => CreateElement("REBAR", 1, 0);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the gripper bar of a vertical rebar. This class cannot be inherited.</summary>
		public static class GripperVertical
		{
			/// <summary>Gets a visual style element that represents a gripper bar for a vertical rebar.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a gripper bar for a vertical rebar.</returns>
			public static VisualStyleElement Normal => CreateElement("REBAR", 2, 0);
		}
	}

	/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of the scroll bar control. This class cannot be inherited.</summary>
	public static class ScrollBar
	{
		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state and direction of a scroll arrow. This class cannot be inherited. </summary>
		public static class ArrowButton
		{
			/// <summary>Gets a visual style element that represents a downward-pointing scroll arrow in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing scroll arrow in the disabled state.</returns>
			public static VisualStyleElement DownDisabled => CreateElement("SCROLLBAR", 1, 8);

			/// <summary>Gets a visual style element that represents a downward-pointing scroll arrow in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing scroll arrow in the hot state.</returns>
			public static VisualStyleElement DownHot => CreateElement("SCROLLBAR", 1, 6);

			/// <summary>Gets a visual style element that represents a downward-pointing scroll arrow in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing scroll arrow in the normal state.</returns>
			public static VisualStyleElement DownNormal => CreateElement("SCROLLBAR", 1, 5);

			/// <summary>Gets a visual style element that represents a downward-pointing scroll arrow in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing scroll arrow in the pressed state.</returns>
			public static VisualStyleElement DownPressed => CreateElement("SCROLLBAR", 1, 7);

			/// <summary>Gets a visual style element that represents a left-pointing scroll arrow in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing scroll arrow in the disabled state.</returns>
			public static VisualStyleElement LeftDisabled => CreateElement("SCROLLBAR", 1, 12);

			/// <summary>Gets a visual style element that represents a left-pointing scroll arrow in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing scroll arrow in the hot state.</returns>
			public static VisualStyleElement LeftHot => CreateElement("SCROLLBAR", 1, 10);

			/// <summary>Gets a visual style element that represents a left-pointing scroll arrow in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing scroll arrow in the normal state.</returns>
			public static VisualStyleElement LeftNormal => CreateElement("SCROLLBAR", 1, 9);

			/// <summary>Gets a visual style element that represents a left-pointing scroll arrow in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing scroll arrow in the pressed state.</returns>
			public static VisualStyleElement LeftPressed => CreateElement("SCROLLBAR", 1, 11);

			/// <summary>Gets a visual style element that represents a right-pointing scroll arrow in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing scroll arrow in the disabled state.</returns>
			public static VisualStyleElement RightDisabled => CreateElement("SCROLLBAR", 1, 16);

			/// <summary>Gets a visual style element that represents a right-pointing scroll arrow in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing scroll arrow in the hot state.</returns>
			public static VisualStyleElement RightHot => CreateElement("SCROLLBAR", 1, 14);

			/// <summary>Gets a visual style element that represents a right-pointing scroll arrow in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing scroll arrow in the normal state.</returns>
			public static VisualStyleElement RightNormal => CreateElement("SCROLLBAR", 1, 13);

			/// <summary>Gets a visual style element that represents a right-pointing scroll arrow in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing scroll arrow in the pressed state.</returns>
			public static VisualStyleElement RightPressed => CreateElement("SCROLLBAR", 1, 15);

			/// <summary>Gets a visual style element that represents an upward-pointing scroll arrow in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing scroll arrow in the disabled state.</returns>
			public static VisualStyleElement UpDisabled => CreateElement("SCROLLBAR", 1, 4);

			/// <summary>Gets a visual style element that represents an upward-pointing scroll arrow in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing scroll arrow in the hot state.</returns>
			public static VisualStyleElement UpHot => CreateElement("SCROLLBAR", 1, 2);

			/// <summary>Gets a visual style element that represents an upward-pointing scroll arrow in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing scroll arrow in the normal state.</returns>
			public static VisualStyleElement UpNormal => CreateElement("SCROLLBAR", 1, 1);

			/// <summary>Gets a visual style element that represents an upward-pointing scroll arrow in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing scroll arrow in the pressed state. </returns>
			public static VisualStyleElement UpPressed => CreateElement("SCROLLBAR", 1, 3);

			internal static VisualStyleElement DownHover => new VisualStyleElement("SCROLLBAR", 1, 18);

			internal static VisualStyleElement LeftHover => new VisualStyleElement("SCROLLBAR", 1, 19);

			internal static VisualStyleElement RightHover => new VisualStyleElement("SCROLLBAR", 1, 20);

			internal static VisualStyleElement UpHover => new VisualStyleElement("SCROLLBAR", 1, 17);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the grip of a horizontal scroll box (also known as the thumb). This class cannot be inherited.</summary>
		public static class GripperHorizontal
		{
			/// <summary>Gets a visual style element that represents a grip for a horizontal scroll box.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a grip for a horizontal scroll box. </returns>
			public static VisualStyleElement Normal => CreateElement("SCROLLBAR", 8, 0);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the grip of a vertical scroll box (also known as the thumb). This class cannot be inherited.</summary>
		public static class GripperVertical
		{
			/// <summary>Gets a visual style element that represents a grip for a vertical scroll box.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a grip for a vertical scroll box. </returns>
			public static VisualStyleElement Normal => CreateElement("SCROLLBAR", 9, 0);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the left part of a horizontal scroll bar track. This class cannot be inherited. </summary>
		public static class LeftTrackHorizontal
		{
			/// <summary>Gets a visual style element that represents the left part of a horizontal scroll bar track in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the left part of a horizontal scroll bar track in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("SCROLLBAR", 5, 4);

			/// <summary>Gets a visual style element that represents the left part of a horizontal scroll bar track in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the left part of a horizontal scroll bar track in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("SCROLLBAR", 5, 2);

			/// <summary>Gets a visual style element that represents the left part of a horizontal scroll bar track in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the left part of a horizontal scroll bar track in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("SCROLLBAR", 5, 1);

			/// <summary>Gets a visual style element that represents the left part of a horizontal scroll bar track in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the left part of a horizontal scroll bar track in the pressed state.</returns>
			public static VisualStyleElement Pressed => CreateElement("SCROLLBAR", 5, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the lower part of a vertical scroll bar track. This class cannot be inherited. </summary>
		public static class LowerTrackVertical
		{
			/// <summary>Gets a visual style element that represents the lower part of a vertical scroll bar track in the disabled state. </summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the lower part of a vertical scroll bar track in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("SCROLLBAR", 6, 4);

			/// <summary>Gets a visual style element that represents the lower part of a vertical scroll bar track in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the lower part of a vertical scroll bar track in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("SCROLLBAR", 6, 2);

			/// <summary>Gets a visual style element that represents the lower part of a vertical scroll bar track in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the lower part of a vertical scroll bar track in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("SCROLLBAR", 6, 1);

			/// <summary>Gets a visual style element that represents the lower part of a vertical scroll bar track in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the lower part of a vertical scroll bar track in the pressed state. </returns>
			public static VisualStyleElement Pressed => CreateElement("SCROLLBAR", 6, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the right part of a horizontal scroll bar track. This class cannot be inherited. </summary>
		public static class RightTrackHorizontal
		{
			/// <summary>Gets a visual style element that represents the right part of a horizontal scroll bar track in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the right part of a horizontal scroll bar track in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("SCROLLBAR", 4, 4);

			/// <summary>Gets a visual style element that represents the right part of a horizontal scroll bar track in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the right part of a horizontal scroll bar track in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("SCROLLBAR", 4, 2);

			/// <summary>Gets a visual style element that represents the right part of a horizontal scroll bar track in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the right part of a horizontal scroll bar track in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("SCROLLBAR", 4, 1);

			/// <summary>Gets a visual style element that represents the right part of a horizontal scroll bar track in the pressed state. </summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the right part of a horizontal scroll bar track in the pressed state.</returns>
			public static VisualStyleElement Pressed => CreateElement("SCROLLBAR", 4, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the sizing handle of a scroll bar. This class cannot be inherited. </summary>
		public static class SizeBox
		{
			/// <summary>Gets a visual style element that represents a sizing handle that is aligned to the left.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a sizing handle that is aligned to the left.</returns>
			public static VisualStyleElement LeftAlign => CreateElement("SCROLLBAR", 10, 2);

			/// <summary>Gets a visual style element that represents a sizing handle that is aligned to the right.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a sizing handle that is aligned to the right. </returns>
			public static VisualStyleElement RightAlign => CreateElement("SCROLLBAR", 10, 1);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a horizontal scroll box (also known as the thumb). This class cannot be inherited. </summary>
		public static class ThumbButtonHorizontal
		{
			/// <summary>Gets a visual style element that represents a horizontal scroll box in the disabled state. </summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll box in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("SCROLLBAR", 2, 4);

			/// <summary>Gets a visual style element that represents a horizontal scroll box in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll box in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("SCROLLBAR", 2, 2);

			/// <summary>Gets a visual style element that represents a horizontal scroll box in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll box in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("SCROLLBAR", 2, 1);

			/// <summary>Gets a visual style element that represents a horizontal scroll box in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll box in the pressed state.</returns>
			public static VisualStyleElement Pressed => CreateElement("SCROLLBAR", 2, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a vertical scroll box (also known as the thumb). This class cannot be inherited.</summary>
		public static class ThumbButtonVertical
		{
			/// <summary>Gets a visual style element that represents a vertical scroll box in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll box in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("SCROLLBAR", 3, 4);

			/// <summary>Gets a visual style element that represents a vertical scroll box in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll box in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("SCROLLBAR", 3, 2);

			/// <summary>Gets a visual style element that represents a vertical scroll box in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll box in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("SCROLLBAR", 3, 1);

			/// <summary>Gets a visual style element that represents a vertical scroll box in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll box in the pressed state. </returns>
			public static VisualStyleElement Pressed => CreateElement("SCROLLBAR", 3, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the upper part of a vertical scroll bar track. This class cannot be inherited. </summary>
		public static class UpperTrackVertical
		{
			/// <summary>Gets a visual style element that represents the upper part of a vertical scroll bar track in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the upper part of a vertical scroll bar track in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("SCROLLBAR", 7, 4);

			/// <summary>Gets a visual style element that represents the upper part of a vertical scroll bar track in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the upper part of a vertical scroll bar track in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("SCROLLBAR", 7, 2);

			/// <summary>Gets a visual style element that represents the upper part of a vertical scroll bar track in the normal state. </summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the upper part of a vertical scroll bar track in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("SCROLLBAR", 7, 1);

			/// <summary>Gets a visual style element that represents the upper part of a vertical scroll bar track in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the upper part of a vertical scroll bar track in the pressed state. </returns>
			public static VisualStyleElement Pressed => CreateElement("SCROLLBAR", 7, 3);
		}
	}

	/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the arrows of a spin button control (also known as an up-down control). This class cannot be inherited.</summary>
	public static class Spin
	{
		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the downward-pointing arrow for a spin button control (also known as an up-down control). This class cannot be inherited. </summary>
		public static class Down
		{
			/// <summary>Gets a visual style element that represents a downward-pointing spin button arrow in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing spin button arrow in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("SPIN", 2, 4);

			/// <summary>Gets a visual style element that represents a downward-pointing spin button arrow in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing spin button arrow in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("SPIN", 2, 2);

			/// <summary>Gets a visual style element that represents a downward-pointing spin button arrow in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing spin button arrow in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("SPIN", 2, 1);

			/// <summary>Gets a visual style element that represents a downward-pointing spin button arrow in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing spin button arrow in the pressed state.</returns>
			public static VisualStyleElement Pressed => CreateElement("SPIN", 2, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the left-pointing arrow for a spin button control (also known as an up-down control). This class cannot be inherited. </summary>
		public static class DownHorizontal
		{
			/// <summary>Gets a visual style element that represents a left-pointing spin button arrow in the disabled state. </summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing spin button arrow in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("SPIN", 4, 4);

			/// <summary>Gets a visual style element that represents a left-pointing spin button arrow in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing spin button arrow in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("SPIN", 4, 2);

			/// <summary>Gets a visual style element that represents a left-pointing spin button arrow in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing spin button arrow in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("SPIN", 4, 1);

			/// <summary>Gets a visual style element that represents a left-pointing spin button arrow in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing spin button arrow in the pressed state. </returns>
			public static VisualStyleElement Pressed => CreateElement("SPIN", 4, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the upward-pointing arrow for a spin button control (also known as an up-down control). This class cannot be inherited. </summary>
		public static class Up
		{
			/// <summary>Gets a visual style element that represents an upward-pointing spin button arrow in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing spin button arrow in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("SPIN", 1, 4);

			/// <summary>Gets a visual style element that represents an upward-pointing spin button arrow in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing spin button arrow in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("SPIN", 1, 2);

			/// <summary>Gets a visual style element that represents an upward-pointing spin button arrow in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing spin button arrow in the normal state. </returns>
			public static VisualStyleElement Normal => CreateElement("SPIN", 1, 1);

			/// <summary>Gets a visual style element that represents an upward-pointing spin button arrow in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing spin button arrow in the pressed state. </returns>
			public static VisualStyleElement Pressed => CreateElement("SPIN", 1, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the right-pointing arrow for a spin button control (also known as an up-down control). This class cannot be inherited. </summary>
		public static class UpHorizontal
		{
			/// <summary>Gets a visual style element that represents a right-pointing spin button arrow in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing spin button arrow in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("SPIN", 3, 4);

			/// <summary>Gets a visual style element that represents a right-pointing spin button arrow in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing spin button arrow in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("SPIN", 3, 2);

			/// <summary>Gets a visual style element that represents a right-pointing spin button arrow in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing spin button arrow in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("SPIN", 3, 1);

			/// <summary>Gets a visual style element that represents a right-pointing spin button arrow in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing spin button arrow in the pressed state. </returns>
			public static VisualStyleElement Pressed => CreateElement("SPIN", 3, 3);
		}
	}

	/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of the Start menu. This class cannot be inherited.</summary>
	public static class StartPanel
	{
		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the bottom border of the Start menu. This class cannot be inherited. </summary>
		public static class LogOff
		{
			/// <summary>Gets a visual style element that represents the bottom border of the Start menu.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the bottom border of the Start menu. </returns>
			public static VisualStyleElement Normal => CreateElement("STARTPANEL", 8, 0);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Log Off and Shut Down buttons in the Start menu. This class cannot be inherited. </summary>
		public static class LogOffButtons
		{
			/// <summary>Gets a visual style element that represents the Log Off and Shut Down buttons in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Log Off and Shut Down buttons in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("STARTPANEL", 9, 2);

			/// <summary>Gets a visual style element that represents the Log Off and Shut Down buttons in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Log Off and Shut Down buttons in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("STARTPANEL", 9, 1);

			/// <summary>Gets a visual style element that represents the Log Off and Shut Down buttons in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Log Off and Shut Down buttons in the pressed state. </returns>
			public static VisualStyleElement Pressed => CreateElement("STARTPANEL", 9, 3);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of the All Programs item in the Start menu. This class cannot be inherited. </summary>
		public static class MorePrograms
		{
			/// <summary>Gets a visual style element that represents the background of the All Programs menu item.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of the All Programs menu item. </returns>
			public static VisualStyleElement Normal => CreateElement("STARTPANEL", 2, 0);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the All Programs arrow in the Start menu. This class cannot be inherited.</summary>
		public static class MoreProgramsArrow
		{
			/// <summary>Gets a visual style element that represents the All Programs arrow in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the All Programs arrow in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("STARTPANEL", 3, 2);

			/// <summary>Gets a visual style element that represents the All Programs arrow in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the All Programs arrow in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("STARTPANEL", 3, 1);

			/// <summary>Gets a visual style element that represents the All Programs arrow in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the All Programs arrow in the pressed state.</returns>
			public static VisualStyleElement Pressed => CreateElement("STARTPANEL", 3, 3);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of the right side of the Start menu. This class cannot be inherited. </summary>
		public static class PlaceList
		{
			/// <summary>Gets a visual style element that represents the background of the right side of the Start menu.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of the right side of the Start menu. </returns>
			public static VisualStyleElement Normal => CreateElement("STARTPANEL", 6, 0);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the bar that separates groups of items in the right side of the Start menu. This class cannot be inherited. </summary>
		public static class PlaceListSeparator
		{
			/// <summary>Gets a visual style element that represents the bar that separates groups of items in the right side of the Start menu.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the bar that separates groups of items in the right side of the Start menu. </returns>
			public static VisualStyleElement Normal => CreateElement("STARTPANEL", 7, 0);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the preview area of the Start menu. This class cannot be inherited. </summary>
		public static class Preview
		{
			/// <summary>Gets a visual style element that represents the preview area of the Start menu.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the preview area of the Start menu. </returns>
			public static VisualStyleElement Normal => CreateElement("STARTPANEL", 11, 0);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of the left side of the Start menu. This class cannot be inherited. </summary>
		public static class ProgList
		{
			/// <summary>Gets a visual style element that represents the background of the left side of the Start menu.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of the left side of the Start menu. </returns>
			public static VisualStyleElement Normal => CreateElement("STARTPANEL", 4, 0);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the bar that separates groups of items in the left side of the Start menu. This class cannot be inherited. </summary>
		public static class ProgListSeparator
		{
			/// <summary>Gets a visual style element that represents the bar that separates groups of items in the left side of the Start menu.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the bar that separates groups of items in the left side of the Start menu.</returns>
			public static VisualStyleElement Normal => CreateElement("STARTPANEL", 5, 0);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the top border of the Start menu. This class cannot be inherited.</summary>
		public static class UserPane
		{
			/// <summary>Gets a visual style element that represents the top border of the Start menu.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the top border of the Start menu.</returns>
			public static VisualStyleElement Normal => CreateElement("STARTPANEL", 1, 0);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of the user picture on the Start menu. This class cannot be inherited. </summary>
		public static class UserPicture
		{
			/// <summary>Gets a visual style element that represents the background of the user picture on the Start menu.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of the user picture on the Start menu. </returns>
			public static VisualStyleElement Normal => CreateElement("STARTPANEL", 10, 0);
		}
	}

	/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of the status bar. This class cannot be inherited.</summary>
	public static class Status
	{
		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of the status bar. This class cannot be inherited.</summary>
		public static class Bar
		{
			/// <summary>Gets a visual style element that represents the background of the status bar. </summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of the status bar. </returns>
			public static VisualStyleElement Normal => CreateElement("STATUS", 0, 0);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the grip of the status bar. This class cannot be inherited.</summary>
		public static class Gripper
		{
			/// <summary>Gets a visual style element that represents the status bar grip.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the status bar grip. </returns>
			public static VisualStyleElement Normal => CreateElement("STATUS", 3, 0);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the pane of the grip in the status bar. This class cannot be inherited.</summary>
		public static class GripperPane
		{
			/// <summary>Gets a visual style element that represents a pane for the status bar grip.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pane for the status bar grip. </returns>
			public static VisualStyleElement Normal => CreateElement("STATUS", 2, 0);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a status bar pane. This class cannot be inherited.</summary>
		public static class Pane
		{
			/// <summary>Gets a visual style element that represents a status bar pane.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a status bar pane.</returns>
			public static VisualStyleElement Normal => CreateElement("STATUS", 1, 0);
		}
	}

	/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of a tab control. This class cannot be inherited.</summary>
	public static class Tab
	{
		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the interior of a tab control page. This class cannot be inherited.</summary>
		public static class Body
		{
			/// <summary>Gets a visual style element that represents the interior of a tab control page. </summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the interior of a tab control page. </returns>
			public static VisualStyleElement Normal => CreateElement("TAB", 10, 0);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the border of a tab control page. This class cannot be inherited.</summary>
		public static class Pane
		{
			/// <summary>Gets a visual style element that represents the border of a tab control page.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the border of a tab control page.</returns>
			public static VisualStyleElement Normal => CreateElement("TAB", 9, 0);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a tab control that shares its top, left, and right borders with other tab controls. This class cannot be inherited. </summary>
		public static class TabItem
		{
			/// <summary>Gets a visual style element that represents a disabled tab control that shares its top, left, and right borders with other tab controls.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled tab control that shares its top, left, and right borders with other tab controls.</returns>
			public static VisualStyleElement Disabled => CreateElement("TAB", 1, 4);

			/// <summary>Gets a visual style element that represents a hot tab control that shares its top, left, and right borders with other tab controls.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot tab control that shares its top, left, and right borders with other tab controls.</returns>
			public static VisualStyleElement Hot => CreateElement("TAB", 1, 2);

			/// <summary>Gets a visual style element that represents a normal tab control that shares its top, left, and right borders with other tab controls.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal tab control that shares its top, left, and right borders with other tab controls.</returns>
			public static VisualStyleElement Normal => CreateElement("TAB", 1, 1);

			/// <summary>Gets a visual style element that represents a pressed tab control that shares its top, left, and right borders with other tab controls.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed tab control that shares its top, left, and right borders with other tab controls. </returns>
			public static VisualStyleElement Pressed => CreateElement("TAB", 1, 3);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a tab control that shares its top border with another tab control. This class cannot be inherited. </summary>
		public static class TabItemBothEdges
		{
			/// <summary>Gets a visual style element that represents a tab control that shares its top border with another tab control.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a tab control that shares its top border with another tab control. </returns>
			public static VisualStyleElement Normal => CreateElement("TAB", 4, 0);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a tab control that shares its top and right borders with other tab controls. This class cannot be inherited. </summary>
		public static class TabItemLeftEdge
		{
			/// <summary>Gets a visual style element that represents a disabled tab control that shares its top and right borders with other tab controls.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled tab control that shares its top and right borders with other tab controls.</returns>
			public static VisualStyleElement Disabled => CreateElement("TAB", 2, 4);

			/// <summary>Gets a visual style element that represents a hot tab control that shares its top and right borders with other tab controls.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot tab control that shares its top and right borders with other tab controls.</returns>
			public static VisualStyleElement Hot => CreateElement("TAB", 2, 2);

			/// <summary>Gets a visual style element that represents a normal tab control that shares its top and right borders with other tab controls.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal tab control that shares its top and right borders with other tab controls.</returns>
			public static VisualStyleElement Normal => CreateElement("TAB", 2, 1);

			/// <summary>Gets a visual style element that represents a pressed tab control that shares its top and right borders with other tab controls.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed tab control that shares its top and right borders with other tab controls. </returns>
			public static VisualStyleElement Pressed => CreateElement("TAB", 2, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a tab control that shares its top and left borders with other tab controls. This class cannot be inherited. </summary>
		public static class TabItemRightEdge
		{
			/// <summary>Gets a visual style element that represents a disabled tab control that shares its top and left borders with other tab controls.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled tab control that shares its top and left borders with other tab controls.</returns>
			public static VisualStyleElement Disabled => CreateElement("TAB", 3, 4);

			/// <summary>Gets a visual style element that represents a hot tab control that shares its top and left borders with other tab controls.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot tab control that shares its top and left borders with other tab controls.</returns>
			public static VisualStyleElement Hot => CreateElement("TAB", 3, 2);

			/// <summary>Gets a visual style element that represents a normal tab control that shares its top and left borders with other tab controls.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal tab control that shares its top and left borders with other tab controls.</returns>
			public static VisualStyleElement Normal => CreateElement("TAB", 3, 1);

			/// <summary>Gets a visual style element that represents a pressed tab control that shares its top and left borders with other tab controls.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed tab control that shares its top and left borders with other tab controls. </returns>
			public static VisualStyleElement Pressed => CreateElement("TAB", 3, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a tab control that shares its bottom, left, and right borders with other tab controls. This class cannot be inherited. </summary>
		public static class TopTabItem
		{
			/// <summary>Gets a visual style element that represents a disabled tab control that shares its bottom, left, and right borders with other tab controls.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled tab control that shares its bottom, left, and right borders with other tab controls.</returns>
			public static VisualStyleElement Disabled => CreateElement("TAB", 5, 4);

			/// <summary>Gets a visual style element that represents a hot tab control that shares its bottom, left, and right borders with other tab controls.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot tab control that shares its bottom, left, and right borders with other tab controls.</returns>
			public static VisualStyleElement Hot => CreateElement("TAB", 5, 2);

			/// <summary>Gets a visual style element that represents a normal tab control that shares its bottom, left, and right borders with other tab controls. </summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal tab control that shares its bottom, left, and right borders with other tab controls.</returns>
			public static VisualStyleElement Normal => CreateElement("TAB", 5, 1);

			/// <summary>Gets a visual style element that represents a pressed tab control that shares its bottom, left, and right borders with other tab controls.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed tab control that shares its bottom, left, and right borders with other tab controls.</returns>
			public static VisualStyleElement Pressed => CreateElement("TAB", 5, 3);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a tab control that shares its bottom border with another tab control. This class cannot be inherited. </summary>
		public static class TopTabItemBothEdges
		{
			/// <summary>Gets a visual style element that represents a tab control that shares its bottom border with another tab control.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a tab control that shares its bottom border with another tab control. </returns>
			public static VisualStyleElement Normal => CreateElement("TAB", 8, 0);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a tab control that shares its bottom and right borders with other tab controls. This class cannot be inherited. </summary>
		public static class TopTabItemLeftEdge
		{
			/// <summary>Gets a visual style element that represents a disabled tab control that shares its bottom and right borders with other tab controls.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled tab control that shares its bottom and right borders with other tab controls.</returns>
			public static VisualStyleElement Disabled => CreateElement("TAB", 6, 4);

			/// <summary>Gets a visual style element that represents a hot tab control that shares its bottom and right borders with other tab controls.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot tab control that shares its bottom and right borders with other tab controls.</returns>
			public static VisualStyleElement Hot => CreateElement("TAB", 6, 2);

			/// <summary>Gets a visual style element that represents a normal tab control that shares its bottom and right borders with other tab controls.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal tab control that shares its bottom and right borders with other tab controls.</returns>
			public static VisualStyleElement Normal => CreateElement("TAB", 6, 1);

			/// <summary>Gets a visual style element that represents a pressed tab control that shares its bottom and right borders with other tab controls.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed tab control that shares its bottom and right borders with other tab controls. </returns>
			public static VisualStyleElement Pressed => CreateElement("TAB", 6, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a tab control that shares its bottom and left borders with other tab controls. This class cannot be inherited. </summary>
		public static class TopTabItemRightEdge
		{
			/// <summary>Gets a visual style element that represents a disabled tab control that shares its bottom and left borders with other tab controls.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled tab control that shares its bottom and left borders with other tab controls.</returns>
			public static VisualStyleElement Disabled => CreateElement("TAB", 7, 4);

			/// <summary>Gets a visual style element that represents a hot tab control that shares its bottom and left borders with other tab controls.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot tab control that shares its bottom and left borders with other tab controls.</returns>
			public static VisualStyleElement Hot => CreateElement("TAB", 7, 2);

			/// <summary>Gets a visual style element that represents a normal tab control that shares its bottom and left borders with other tab controls.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal tab control that shares its bottom and left borders with other tab controls.</returns>
			public static VisualStyleElement Normal => CreateElement("TAB", 7, 1);

			/// <summary>Gets a visual style element that represents a pressed tab control that shares its bottom and left borders with other tab controls.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed tab control that shares its bottom and left borders with other tab controls. </returns>
			public static VisualStyleElement Pressed => CreateElement("TAB", 7, 3);
		}
	}

	/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for parts of the taskbar. This class cannot be inherited.</summary>
	public static class TaskBand
	{
		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a flashing window button in the taskbar. This class cannot be inherited. </summary>
		public static class FlashButton
		{
			/// <summary>Gets a visual style element that represents a flashing window button in the taskbar.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a flashing window button in the taskbar. </returns>
			public static VisualStyleElement Normal => CreateElement("TASKBAND", 2, 0);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a flashing menu item of a window button in the taskbar. This class cannot be inherited. </summary>
		public static class FlashButtonGroupMenu
		{
			/// <summary>Gets a visual style element that represents a flashing menu item for a window button in the taskbar.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a flashing menu item for a window button in the taskbar.</returns>
			public static VisualStyleElement Normal => CreateElement("TASKBAND", 3, 0);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a group counter of the taskbar. This class cannot be inherited.  </summary>
		public static class GroupCount
		{
			/// <summary>Gets a visual style element that represents a group counter for the taskbar.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a group counter for the taskbar. </returns>
			public static VisualStyleElement Normal => CreateElement("TASKBAND", 1, 0);
		}
	}

	/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of the taskbar. This class cannot be inherited.</summary>
	public static class Taskbar
	{
		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of a taskbar that is docked on the bottom of the screen. This class cannot be inherited. </summary>
		public static class BackgroundBottom
		{
			/// <summary>Gets a visual style element that represents the background of a taskbar that is docked on the bottom of the screen. </summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of a taskbar that is docked on the bottom of the screen. </returns>
			public static VisualStyleElement Normal => CreateElement("TASKBAR", 1, 0);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of a taskbar that is docked on the left side of the screen. This class cannot be inherited. </summary>
		public static class BackgroundLeft
		{
			/// <summary>Gets a visual style element that represents the background of a taskbar that is docked on the left side of the screen. </summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of a taskbar that is docked on the left side of the screen. </returns>
			public static VisualStyleElement Normal => CreateElement("TASKBAR", 4, 0);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of a taskbar that is docked on the right side of the screen. This class cannot be inherited. </summary>
		public static class BackgroundRight
		{
			/// <summary>Gets a visual style element that represents the background of a taskbar that is docked on the right side of the screen.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of a taskbar that is docked on the right side of the screen.</returns>
			public static VisualStyleElement Normal => CreateElement("TASKBAR", 2, 0);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of a taskbar that is docked on the top of the screen. This class cannot be inherited. </summary>
		public static class BackgroundTop
		{
			/// <summary>Gets a visual style element that represents the background of a taskbar that is docked on the top of the screen. </summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of a taskbar that is docked on the top of the screen. </returns>
			public static VisualStyleElement Normal => CreateElement("TASKBAR", 3, 0);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the sizing bar of a taskbar that is docked on the bottom of the screen. This class cannot be inherited. </summary>
		public static class SizingBarBottom
		{
			/// <summary>Gets a visual style element that represents the sizing bar for a taskbar that is docked on the bottom of the screen.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing bar for a taskbar that is docked on the bottom of the screen.</returns>
			public static VisualStyleElement Normal => CreateElement("TASKBAR", 5, 0);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the sizing bar of a taskbar that is docked on the left side of the screen. This class cannot be inherited. </summary>
		public static class SizingBarLeft
		{
			/// <summary>Gets a visual style element that represents the sizing bar for a taskbar that is docked on the left side of the screen.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing bar for a taskbar that is docked on the left side of the screen.</returns>
			public static VisualStyleElement Normal => CreateElement("TASKBAR", 8, 0);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the sizing bar of a taskbar that is docked on the right side of the screen. This class cannot be inherited. </summary>
		public static class SizingBarRight
		{
			/// <summary>Gets a visual style element that represents the sizing bar for a taskbar that is docked on the right side of the screen.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing bar for a taskbar that is docked on the right side of the screen.</returns>
			public static VisualStyleElement Normal => CreateElement("TASKBAR", 6, 0);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the sizing bar of a taskbar that is docked on the top of the screen. This class cannot be inherited. </summary>
		public static class SizingBarTop
		{
			/// <summary>Gets a visual style element that represents the sizing bar for a taskbar that is docked on the top of the screen.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing bar for a taskbar that is docked on the top of the screen.</returns>
			public static VisualStyleElement Normal => CreateElement("TASKBAR", 7, 0);
		}
	}

	/// <summary>Contains a class that provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of the taskbar clock. This class cannot be inherited. </summary>
	public static class TaskbarClock
	{
		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of the taskbar clock. This class cannot be inherited.  </summary>
		public static class Time
		{
			/// <summary>Gets a visual style element that represents the background of the taskbar clock. </summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of the taskbar clock.</returns>
			public static VisualStyleElement Normal => CreateElement("CLOCK", 1, 1);
		}
	}

	/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of a text box. This class cannot be inherited.</summary>
	public static class TextBox
	{
		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the caret of a text box. This class cannot be inherited. </summary>
		public static class Caret
		{
			/// <summary>Gets a visual style element that represents a text box caret.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the insertion point of a text box. </returns>
			public static VisualStyleElement Normal => CreateElement("EDIT", 2, 0);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a text box. This class cannot be inherited. </summary>
		public static class TextEdit
		{
			/// <summary>Gets a visual style element that represents a text box in assist mode.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a text box in assist mode.</returns>
			public static VisualStyleElement Assist => CreateElement("EDIT", 1, 7);

			/// <summary>Gets a visual style element that represents a disabled text box.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled text box.</returns>
			public static VisualStyleElement Disabled => CreateElement("EDIT", 1, 4);

			/// <summary>Gets a visual style element that represents a text box that has focus.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a text box that has focus.</returns>
			public static VisualStyleElement Focused => CreateElement("EDIT", 1, 5);

			/// <summary>Gets a visual style element that represents a hot text box.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot text box.</returns>
			public static VisualStyleElement Hot => CreateElement("EDIT", 1, 2);

			/// <summary>Gets a visual style element that represents a normal text box.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal text box.</returns>
			public static VisualStyleElement Normal => CreateElement("EDIT", 1, 1);

			/// <summary>Gets a visual style element that represents a read-only text box.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a read-only text box.</returns>
			public static VisualStyleElement ReadOnly => CreateElement("EDIT", 1, 6);

			/// <summary>Gets a visual style element that represents a selected text box.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a selected text box.</returns>
			public static VisualStyleElement Selected => CreateElement("EDIT", 1, 3);
		}
	}

	/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of a toolbar. This class cannot be inherited.</summary>
	public static class ToolBar
	{
		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a toolbar button. This class cannot be inherited. </summary>
		public static class Button
		{
			/// <summary>Gets a visual style element that represents a toolbar button in the checked state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a toolbar button in the checked state.</returns>
			public static VisualStyleElement Checked => CreateElement("TOOLBAR", 1, 5);

			/// <summary>Gets a visual style element that represents a toolbar button in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a toolbar button in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("TOOLBAR", 1, 4);

			/// <summary>Gets a visual style element that represents a toolbar button in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a toolbar button in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("TOOLBAR", 1, 2);

			/// <summary>Gets a visual style element that represents a toolbar button in the hot and checked states.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a toolbar button in the hot and checked states.</returns>
			public static VisualStyleElement HotChecked => CreateElement("TOOLBAR", 1, 6);

			/// <summary>Gets a visual style element that represents a toolbar button in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a toolbar button in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("TOOLBAR", 1, 1);

			/// <summary>Gets a visual style element that represents a toolbar button in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a toolbar button in the pressed state.</returns>
			public static VisualStyleElement Pressed => CreateElement("TOOLBAR", 1, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a drop-down toolbar button. This class cannot be inherited. </summary>
		public static class DropDownButton
		{
			/// <summary>Gets a visual style element that represents a drop-down toolbar button in the checked state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down toolbar button in the checked state.</returns>
			public static VisualStyleElement Checked => CreateElement("TOOLBAR", 2, 5);

			/// <summary>Gets a visual style element that represents a drop-down toolbar button in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down toolbar button in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("TOOLBAR", 2, 4);

			/// <summary>Gets a visual style element that represents a drop-down toolbar button in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down toolbar button in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("TOOLBAR", 2, 2);

			/// <summary>Gets a visual style element that represents a drop-down toolbar button in the hot and checked states.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down toolbar button in the hot and checked states.</returns>
			public static VisualStyleElement HotChecked => CreateElement("TOOLBAR", 2, 6);

			/// <summary>Gets a visual style element that represents a drop-down toolbar button in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down toolbar button in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("TOOLBAR", 2, 1);

			/// <summary>Gets a visual style element that represents a drop-down toolbar button in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down toolbar button in the pressed state.</returns>
			public static VisualStyleElement Pressed => CreateElement("TOOLBAR", 2, 3);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a horizontal separator of the toolbar. This class cannot be inherited. </summary>
		public static class SeparatorHorizontal
		{
			/// <summary>Gets a visual style element that represents a horizontal separator of the toolbar.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal separator of the toolbar.</returns>
			public static VisualStyleElement Normal => CreateElement("TOOLBAR", 5, 0);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a vertical separator of the toolbar. This class cannot be inherited. </summary>
		public static class SeparatorVertical
		{
			/// <summary>Gets a visual style element that represents a vertical separator of the toolbar.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical separator of the toolbar.</returns>
			public static VisualStyleElement Normal => CreateElement("TOOLBAR", 6, 0);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the regular button portion of a combined regular button and drop-down button. This class cannot be inherited.</summary>
		public static class SplitButton
		{
			/// <summary>Gets a visual style element that represents a split button in the checked state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split button in the checked state.</returns>
			public static VisualStyleElement Checked => CreateElement("TOOLBAR", 3, 5);

			/// <summary>Gets a visual style element that represents a split button in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split button in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("TOOLBAR", 3, 4);

			/// <summary>Gets a visual style element that represents a split button in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split button in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("TOOLBAR", 3, 2);

			/// <summary>Gets a visual style element that represents a split button in the hot and checked states.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split button in the hot and checked states.</returns>
			public static VisualStyleElement HotChecked => CreateElement("TOOLBAR", 3, 6);

			/// <summary>Gets a visual style element that represents a split button in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split button in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("TOOLBAR", 3, 1);

			/// <summary>Gets a visual style element that represents a split button in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split button in the pressed state. </returns>
			public static VisualStyleElement Pressed => CreateElement("TOOLBAR", 3, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the drop-down portion of a combined regular button and drop-down button. This class cannot be inherited. </summary>
		public static class SplitButtonDropDown
		{
			/// <summary>Gets a visual style element that represents a split drop-down button in the checked state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split drop-down button in the checked state.</returns>
			public static VisualStyleElement Checked => CreateElement("TOOLBAR", 4, 5);

			/// <summary>Gets a visual style element that represents a split drop-down button in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split drop-down button in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("TOOLBAR", 4, 4);

			/// <summary>Gets a visual style element that represents a split drop-down button in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split drop-down button in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("TOOLBAR", 4, 2);

			/// <summary>Gets a visual style element that represents a split drop-down button in the hot and checked states.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split drop-down button in the hot and checked states.</returns>
			public static VisualStyleElement HotChecked => CreateElement("TOOLBAR", 4, 6);

			/// <summary>Gets a visual style element that represents a split drop-down button in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split drop-down button in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("TOOLBAR", 4, 1);

			/// <summary>Gets a visual style element that represents a split drop-down button in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split drop-down button in the pressed state.</returns>
			public static VisualStyleElement Pressed => CreateElement("TOOLBAR", 4, 3);
		}
	}

	/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of a ToolTip. This class cannot be inherited.</summary>
	public static class ToolTip
	{
		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for a balloon ToolTip. This class cannot be inherited. </summary>
		public static class Balloon
		{
			/// <summary>Gets a visual style element that represents a balloon ToolTip that contains a link.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a balloon ToolTip that contains a link.</returns>
			public static VisualStyleElement Link => CreateElement("TOOLTIP", 3, 2);

			/// <summary>Gets a visual style element that represents a balloon ToolTip that contains text.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a balloon ToolTip that contains text.</returns>
			public static VisualStyleElement Normal => CreateElement("TOOLTIP", 3, 1);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the title area of a balloon ToolTip. This class cannot be inherited. </summary>
		public static class BalloonTitle
		{
			/// <summary>Gets a visual style element that represents the title area of a balloon ToolTip. </summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title area of a balloon ToolTip. </returns>
			public static VisualStyleElement Normal => CreateElement("TOOLTIP", 4, 0);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Close button of a ToolTip. This class cannot be inherited. </summary>
		public static class Close
		{
			/// <summary>Gets a visual style element that represents the ToolTip Close button in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the ToolTip Close button in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("TOOLTIP", 5, 2);

			/// <summary>Gets a visual style element that represents the ToolTip Close button in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the ToolTip Close button in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("TOOLTIP", 5, 1);

			/// <summary>Gets a visual style element that represents the ToolTip Close button in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the ToolTip Close button in the pressed state. </returns>
			public static VisualStyleElement Pressed => CreateElement("TOOLTIP", 5, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for a standard ToolTip. This class cannot be inherited. </summary>
		public static class Standard
		{
			/// <summary>Gets a visual style element that represents a standard ToolTip that contains a link.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a standard ToolTip that contains a link.</returns>
			public static VisualStyleElement Link => CreateElement("TOOLTIP", 1, 2);

			/// <summary>Gets a visual style element that represents a standard ToolTip that contains text.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a standard ToolTip that contains text.</returns>
			public static VisualStyleElement Normal => CreateElement("TOOLTIP", 1, 1);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the title area of a standard ToolTip. This class cannot be inherited. </summary>
		public static class StandardTitle
		{
			/// <summary>Gets a visual style element that represents the title area of a standard ToolTip. </summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title area of a standard ToolTip. </returns>
			public static VisualStyleElement Normal => CreateElement("TOOLTIP", 2, 0);
		}
	}

	/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of the track bar control. This class cannot be inherited.</summary>
	public static class TrackBar
	{
		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the slider (also known as the thumb) of a horizontal track bar. This class cannot be inherited. </summary>
		public static class Thumb
		{
			/// <summary>Gets a visual style element that represents the slider of a horizontal track bar in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the slider of a horizontal track bar in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("TRACKBAR", 3, 5);

			/// <summary>Gets a visual style element that represents the slider of a horizontal track bar that has focus.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the slider of a horizontal track bar that has focus.</returns>
			public static VisualStyleElement Focused => CreateElement("TRACKBAR", 3, 4);

			/// <summary>Gets a visual style element that represents the slider of a horizontal track bar in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the slider of a horizontal track bar in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("TRACKBAR", 3, 2);

			/// <summary>Gets a visual style element that represents the slider of a horizontal track bar in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the slider of a horizontal track bar in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("TRACKBAR", 3, 1);

			/// <summary>Gets a visual style element that represents the slider of a horizontal track bar in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the slider of a horizontal track bar in the pressed state.</returns>
			public static VisualStyleElement Pressed => CreateElement("TRACKBAR", 3, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the downward-pointing track bar slider (also known as the thumb). This class cannot be inherited. </summary>
		public static class ThumbBottom
		{
			/// <summary>Gets a visual style element that represents a downward-pointing track bar slider in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing track bar slider in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("TRACKBAR", 4, 5);

			/// <summary>Gets a visual style element that represents a downward-pointing track bar slider that has focus.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing track bar slider that has focus.</returns>
			public static VisualStyleElement Focused => CreateElement("TRACKBAR", 4, 4);

			/// <summary>Gets a visual style element that represents a downward-pointing track bar slider in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing track bar slider in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("TRACKBAR", 4, 2);

			/// <summary>Gets a visual style element that represents a downward-pointing track bar slider in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing track bar slider in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("TRACKBAR", 4, 1);

			/// <summary>Gets a visual style element that represents a downward-pointing track bar slider in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing track bar slider in the pressed state.</returns>
			public static VisualStyleElement Pressed => CreateElement("TRACKBAR", 4, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the left-pointing track bar slider (also known as the thumb). This class cannot be inherited. </summary>
		public static class ThumbLeft
		{
			/// <summary>Gets a visual style element that represents a left-pointing track bar slider in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing track bar slider in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("TRACKBAR", 7, 5);

			/// <summary>Gets a visual style element that represents a left-pointing track bar slider that has focus.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing track bar slider that has focus.</returns>
			public static VisualStyleElement Focused => CreateElement("TRACKBAR", 7, 4);

			/// <summary>Gets a visual style element that represents a left-pointing track bar slider in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing track bar slider in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("TRACKBAR", 7, 2);

			/// <summary>Gets a visual style element that represents a left-pointing track bar slider in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing track bar slider in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("TRACKBAR", 7, 1);

			/// <summary>Gets a visual style element that represents a left-pointing track bar slider in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing track bar slider in the pressed state. </returns>
			public static VisualStyleElement Pressed => CreateElement("TRACKBAR", 7, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the right-pointing track bar slider (also known as the thumb). This class cannot be inherited. </summary>
		public static class ThumbRight
		{
			/// <summary>Gets a visual style element that represents a right-pointing track bar slider in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing track bar slider in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("TRACKBAR", 8, 5);

			/// <summary>Gets a visual style element that represents a right-pointing track bar slider that has focus.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing track bar slider that has focus.</returns>
			public static VisualStyleElement Focused => CreateElement("TRACKBAR", 8, 4);

			/// <summary>Gets a visual style element that represents a right-pointing track bar slider in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing track bar slider in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("TRACKBAR", 8, 2);

			/// <summary>Gets a visual style element that represents a right-pointing track bar slider in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing track bar slider in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("TRACKBAR", 8, 1);

			/// <summary>Gets a visual style element that represents a right-pointing track bar slider in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing track bar slider in the pressed state.</returns>
			public static VisualStyleElement Pressed => CreateElement("TRACKBAR", 8, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the upward-pointing track bar slider (also known as the thumb). This class cannot be inherited. </summary>
		public static class ThumbTop
		{
			/// <summary>Gets a visual style element that represents an upward-pointing track bar slider in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing track bar slider in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("TRACKBAR", 5, 5);

			/// <summary>Gets a visual style element that represents an upward-pointing track bar slider that has focus.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing track bar slider that has focus.</returns>
			public static VisualStyleElement Focused => CreateElement("TRACKBAR", 5, 4);

			/// <summary>Gets a visual style element that represents an upward-pointing track bar slider in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing track bar slider in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("TRACKBAR", 5, 2);

			/// <summary>Gets a visual style element that represents an upward-pointing track bar slider in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing track bar slider in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("TRACKBAR", 5, 1);

			/// <summary>Gets a visual style element that represents an upward-pointing track bar slider in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing track bar slider in the pressed state.</returns>
			public static VisualStyleElement Pressed => CreateElement("TRACKBAR", 5, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the slider (also known as the thumb) of a vertical track bar. This class cannot be inherited. </summary>
		public static class ThumbVertical
		{
			/// <summary>Gets a visual style element that represents the slider of a vertical track bar in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the slider of a vertical track bar in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("TRACKBAR", 6, 5);

			/// <summary>Gets a visual style element that represents the slider of a vertical track bar that has focus. </summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the slider of a vertical track bar that has focus.</returns>
			public static VisualStyleElement Focused => CreateElement("TRACKBAR", 6, 4);

			/// <summary>Gets a visual style element that represents the slider of a vertical track bar in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the slider of a vertical track bar in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("TRACKBAR", 6, 2);

			/// <summary>Gets a visual style element that represents the slider of a vertical track bar in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the slider of a vertical track bar in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("TRACKBAR", 6, 1);

			/// <summary>Gets a visual style element that represents the slider of a vertical track bar in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the slider of a vertical track bar in the pressed state. </returns>
			public static VisualStyleElement Pressed => CreateElement("TRACKBAR", 6, 3);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a single tick of a horizontal track bar. This class cannot be inherited. </summary>
		public static class Ticks
		{
			/// <summary>Gets a visual style element that represents a single tick of a horizontal track bar.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a single tick of a horizontal track bar.</returns>
			public static VisualStyleElement Normal => CreateElement("TRACKBAR", 9, 1);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a single tick of a vertical track bar. This class cannot be inherited. </summary>
		public static class TicksVertical
		{
			/// <summary>Gets a visual style element that represents a single tick of a vertical track bar.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a single tick of a vertical track bar.</returns>
			public static VisualStyleElement Normal => CreateElement("TRACKBAR", 10, 1);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the track for a horizontal track bar. This class cannot be inherited. </summary>
		public static class Track
		{
			/// <summary>Gets a visual style element that represents the track for a horizontal track bar. </summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the track for a horizontal track bar. </returns>
			public static VisualStyleElement Normal => CreateElement("TRACKBAR", 1, 1);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the track for a vertical track bar. This class cannot be inherited. </summary>
		public static class TrackVertical
		{
			/// <summary>Gets a visual style element that represents the track for a vertical track bar.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the track for a vertical track bar.</returns>
			public static VisualStyleElement Normal => CreateElement("TRACKBAR", 2, 1);
		}
	}

	/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the background of the notification area, which is located at the far right of the taskbar. This class cannot be inherited.</summary>
	public static class TrayNotify
	{
		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for an animated background of the notification area. This class cannot be inherited. </summary>
		public static class AnimateBackground
		{
			/// <summary>Gets a visual style element that represents an animated background of the notification area.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an animated background of the notification area. </returns>
			public static VisualStyleElement Normal => CreateElement("TRAYNOTIFY", 2, 0);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of the notification area. This class cannot be inherited. </summary>
		public static class Background
		{
			/// <summary>Gets a visual style element that represents the background of the notification area.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of the notification area. </returns>
			public static VisualStyleElement Normal => CreateElement("TRAYNOTIFY", 1, 0);
		}
	}

	/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of the tree view control. This class cannot be inherited.  </summary>
	public static class TreeView
	{
		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a tree view branch. This class cannot be inherited. </summary>
		public static class Branch
		{
			/// <summary>Gets a visual style element that represents a tree view branch. </summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a tree view branch.</returns>
			public static VisualStyleElement Normal => CreateElement("TREEVIEW", 3, 0);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the plus sign (+) and minus sign (-) buttons of a tree view control. This class cannot be inherited. </summary>
		public static class Glyph
		{
			/// <summary>Gets a visual style element that represents a minus sign (-) button of a tree view node.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a minus sign button of a tree view node.</returns>
			public static VisualStyleElement Closed => CreateElement("TREEVIEW", 2, 1);

			/// <summary>Gets a visual style element that represents a plus sign (+) button of a tree view node.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a plus sign button of a tree view node.</returns>
			public static VisualStyleElement Opened => CreateElement("TREEVIEW", 2, 2);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a tree view item. This class cannot be inherited. </summary>
		public static class Item
		{
			/// <summary>Gets a visual style element that represents a tree view item in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a tree view item in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("TREEVIEW", 1, 4);

			/// <summary>Gets a visual style element that represents a tree view item in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a tree view item in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("TREEVIEW", 1, 2);

			/// <summary>Gets a visual style element that represents a tree view item in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a tree view item in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("TREEVIEW", 1, 1);

			/// <summary>Gets a visual style element that represents a tree view item that is in the selected state and has focus.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a tree view item that is in the selected state and has focus.</returns>
			public static VisualStyleElement Selected => CreateElement("TREEVIEW", 1, 3);

			/// <summary>Gets a visual style element that represents a tree view item that is in the selected state but does not have focus.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a tree view item that is in the selected state but does not have focus.</returns>
			public static VisualStyleElement SelectedNotFocus => CreateElement("TREEVIEW", 1, 5);
		}
	}

	/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of a window. This class cannot be inherited.</summary>
	public static class Window
	{
		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the title bar of a window. This class cannot be inherited. </summary>
		public static class Caption
		{
			/// <summary>Gets a visual style element that represents the title bar of an active window.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of an active window.</returns>
			public static VisualStyleElement Active => CreateElement("WINDOW", 1, 1);

			/// <summary>Gets a visual style element that represents the title bar of a disabled window.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a disabled window.</returns>
			public static VisualStyleElement Disabled => CreateElement("WINDOW", 1, 3);

			/// <summary>Gets a visual style element that represents the title bar of an inactive window.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of an inactive window.</returns>
			public static VisualStyleElement Inactive => CreateElement("WINDOW", 1, 2);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the title bar of a window. This class cannot be inherited. </summary>
		public static class CaptionSizingTemplate
		{
			/// <summary>Gets a visual style element that represents the sizing template of the title bar of a window.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the title bar of a window. </returns>
			public static VisualStyleElement Normal => CreateElement("WINDOW", 30, 0);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Close button of a window. This class cannot be inherited. </summary>
		public static class CloseButton
		{
			/// <summary>Gets a visual style element that represents a Close button in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Close button in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("WINDOW", 18, 4);

			/// <summary>Gets a visual style element that represents a Close button in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Close button in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("WINDOW", 18, 2);

			/// <summary>Gets a visual style element that represents a Close button in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Close button in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("WINDOW", 18, 1);

			/// <summary>Gets a visual style element that represents a Close button in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Close button in the pressed state.</returns>
			public static VisualStyleElement Pressed => CreateElement("WINDOW", 18, 3);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of a dialog box. This class cannot be inherited. </summary>
		public static class Dialog
		{
			/// <summary>Gets a visual style element that represents the background of a dialog box.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of a dialog box.</returns>
			public static VisualStyleElement Normal => CreateElement("WINDOW", 29, 0);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the bottom border of a window. This class cannot be inherited. </summary>
		public static class FrameBottom
		{
			/// <summary>Gets a visual style element that represents the bottom border of an active window.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the bottom border of an active window.</returns>
			public static VisualStyleElement Active => CreateElement("WINDOW", 9, 1);

			/// <summary>Gets a visual style element that represents the bottom border of an inactive window.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the bottom border of an inactive window.</returns>
			public static VisualStyleElement Inactive => CreateElement("WINDOW", 9, 2);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the bottom border of a window. This class cannot be inherited. </summary>
		public static class FrameBottomSizingTemplate
		{
			/// <summary>Gets a visual style element that represents the sizing template of the bottom border of a window.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the bottom border of a window.</returns>
			public static VisualStyleElement Normal => CreateElement("WINDOW", 36, 0);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the left border of a window. This class cannot be inherited. </summary>
		public static class FrameLeft
		{
			/// <summary>Gets a visual style element that represents the left border of an active window.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the left border of an active window.</returns>
			public static VisualStyleElement Active => CreateElement("WINDOW", 7, 1);

			/// <summary>Gets a visual style element that represents the left border of an inactive window.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the left border of an inactive window.</returns>
			public static VisualStyleElement Inactive => CreateElement("WINDOW", 7, 2);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the left border of a window. This class cannot be inherited. </summary>
		public static class FrameLeftSizingTemplate
		{
			/// <summary>Gets a visual style element that represents the sizing template of the left border of a window.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the left border of a window.</returns>
			public static VisualStyleElement Normal => CreateElement("WINDOW", 32, 0);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the right border of a window. This class cannot be inherited. </summary>
		public static class FrameRight
		{
			/// <summary>Gets a visual style element that represents the right border of an active window.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the right border of an active window.</returns>
			public static VisualStyleElement Active => CreateElement("WINDOW", 8, 1);

			/// <summary>Gets a visual style element that represents the right border of an inactive window.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the right border of an inactive window.</returns>
			public static VisualStyleElement Inactive => CreateElement("WINDOW", 8, 2);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the right border of a window. This class cannot be inherited. </summary>
		public static class FrameRightSizingTemplate
		{
			/// <summary>Gets a visual style element that represents the sizing template of the right border of a window. </summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the right border of a window. </returns>
			public static VisualStyleElement Normal => CreateElement("WINDOW", 34, 0);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Help button of a window or dialog box. This class cannot be inherited. </summary>
		public static class HelpButton
		{
			/// <summary>Gets a visual style element that represents a Help button in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Help button in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("WINDOW", 23, 4);

			/// <summary>Gets a visual style element that represents a Help button in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Help button in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("WINDOW", 23, 2);

			/// <summary>Gets a visual style element that represents a Help button in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Help button in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("WINDOW", 23, 1);

			/// <summary>Gets a visual style element that represents a Help button in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Help button in the pressed state.</returns>
			public static VisualStyleElement Pressed => CreateElement("WINDOW", 23, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the horizontal scroll bar of a window. This class cannot be inherited. </summary>
		public static class HorizontalScroll
		{
			/// <summary>Gets a visual style element that represents a horizontal scroll bar in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll bar in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("WINDOW", 25, 4);

			/// <summary>Gets a visual style element that represents a horizontal scroll bar in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll bar in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("WINDOW", 25, 2);

			/// <summary>Gets a visual style element that represents a horizontal scroll bar in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll bar in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("WINDOW", 25, 1);

			/// <summary>Gets a visual style element that represents a horizontal scroll bar in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll bar in the pressed state.</returns>
			public static VisualStyleElement Pressed => CreateElement("WINDOW", 25, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the horizontal scroll box (also known as the thumb) of a window. This class cannot be inherited. </summary>
		public static class HorizontalThumb
		{
			/// <summary>Gets a visual style element that represents a horizontal scroll box in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll box in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("WINDOW", 26, 4);

			/// <summary>Gets a visual style element that represents a horizontal scroll box in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll box in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("WINDOW", 26, 2);

			/// <summary>Gets a visual style element that represents a horizontal scroll box in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll box in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("WINDOW", 26, 1);

			/// <summary>Gets a visual style element that represents a horizontal scroll box in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll box in the pressed state.</returns>
			public static VisualStyleElement Pressed => CreateElement("WINDOW", 26, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Maximize button of a window. This class cannot be inherited. </summary>
		public static class MaxButton
		{
			/// <summary>Gets a visual style element that represents a Maximize button in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Maximize button in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("WINDOW", 17, 4);

			/// <summary>Gets a visual style element that represents a Maximize button in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Maximize button in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("WINDOW", 17, 2);

			/// <summary>Gets a visual style element that represents a Maximize button in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Maximize button in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("WINDOW", 17, 1);

			/// <summary>Gets a visual style element that represents a Maximize button in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Maximize button in the pressed state.</returns>
			public static VisualStyleElement Pressed => CreateElement("WINDOW", 17, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the title bar of a maximized window. This class cannot be inherited. </summary>
		public static class MaxCaption
		{
			/// <summary>Gets a visual style element that represents the title bar of a maximized active window.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a maximized active window.</returns>
			public static VisualStyleElement Active => CreateElement("WINDOW", 5, 1);

			/// <summary>Gets a visual style element that represents the title bar of a maximized disabled window.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a maximized disabled window.</returns>
			public static VisualStyleElement Disabled => CreateElement("WINDOW", 5, 3);

			/// <summary>Gets a visual style element that represents the title bar of a maximized inactive window.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a maximized inactive window. </returns>
			public static VisualStyleElement Inactive => CreateElement("WINDOW", 5, 2);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Close button of a multiple-document interface (MDI) child window. This class cannot be inherited. </summary>
		public static class MdiCloseButton
		{
			/// <summary>Gets a visual style element that represents the Close button of a multiple-document interface (MDI) child window in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Close button of an MDI child window in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("WINDOW", 20, 4);

			/// <summary>Gets a visual style element that represents the Close button of a multiple-document interface (MDI) child window in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Close button of an MDI child window in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("WINDOW", 20, 2);

			/// <summary>Gets a visual style element that represents the Close button of a multiple-document interface (MDI) child window in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Close button of an MDI child window in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("WINDOW", 20, 1);

			/// <summary>Gets a visual style element that represents the Close button of a multiple-document interface (MDI) child window in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Close button of an MDI child window in the pressed state.</returns>
			public static VisualStyleElement Pressed => CreateElement("WINDOW", 20, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Help button of a multiple-document interface (MDI) child window. This class cannot be inherited. </summary>
		public static class MdiHelpButton
		{
			/// <summary>Gets a visual style element that represents the Help button of a multiple-document interface (MDI) child window in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Help button of an MDI child window in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("WINDOW", 24, 4);

			/// <summary>Gets a visual style element that represents the Help button of a multiple-document interface (MDI) child window in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Help button of an MDI child window in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("WINDOW", 24, 2);

			/// <summary>Gets a visual style element that represents the Help button of a multiple-document interface (MDI) child window in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Help button of an MDI child window in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("WINDOW", 24, 1);

			/// <summary>Gets a visual style element that represents the Help button of a multiple-document interface (MDI) child window in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Help button of an MDI child window in the pressed state.</returns>
			public static VisualStyleElement Pressed => CreateElement("WINDOW", 24, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Minimize button of a multiple-document interface (MDI) child window. This class cannot be inherited. </summary>
		public static class MdiMinButton
		{
			/// <summary>Gets a visual style element that represents the Minimize button of a multiple-document interface (MDI) child window in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Minimize button of an MDI child window in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("WINDOW", 16, 4);

			/// <summary>Gets a visual style element that represents the Minimize button of a multiple-document interface (MDI) child window in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Minimize button of an MDI child window in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("WINDOW", 16, 2);

			/// <summary>Gets a visual style element that represents the Minimize button of a multiple-document interface (MDI) child window in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Minimize button of an MDI child window in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("WINDOW", 16, 1);

			/// <summary>Gets a visual style element that represents the Minimize button of a multiple-document interface (MDI) child window in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Minimize button of an MDI child window in the pressed state.</returns>
			public static VisualStyleElement Pressed => CreateElement("WINDOW", 16, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Restore button of a multiple-document interface (MDI) child window. This class cannot be inherited. </summary>
		public static class MdiRestoreButton
		{
			/// <summary>Gets a visual style element that represents the Restore button of a multiple-document interface (MDI) child window in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Restore button of an MDI child window in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("WINDOW", 22, 4);

			/// <summary>Gets a visual style element that represents the Restore button of a multiple-document interface (MDI) child window in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Restore button of an MDI child window in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("WINDOW", 22, 2);

			/// <summary>Gets a visual style element that represents the Restore button of a multiple-document interface (MDI) child window in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Restore button of an MDI child window in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("WINDOW", 22, 1);

			/// <summary>Gets a visual style element that represents the Restore button of a multiple-document interface (MDI) child window in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Restore button of an MDI child window in the pressed state.</returns>
			public static VisualStyleElement Pressed => CreateElement("WINDOW", 22, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the System button of a multiple-document interface (MDI) child window with visual styles. This class cannot be inherited. </summary>
		public static class MdiSysButton
		{
			/// <summary>Gets a visual style element that represents the System button of a multiple-document interface (MDI) child window in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the System button of an MDI child window in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("WINDOW", 14, 4);

			/// <summary>Gets a visual style element that represents the System button of a multiple-document interface (MDI) child window in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the System button of an MDI child window in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("WINDOW", 14, 2);

			/// <summary>Gets a visual style element that represents the System button of a multiple-document interface (MDI) child window in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the System button of an MDI child window in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("WINDOW", 14, 1);

			/// <summary>Gets a visual style element that represents the System button of a multiple-document interface (MDI) child window in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the System button of an MDI child window in the pressed state.</returns>
			public static VisualStyleElement Pressed => CreateElement("WINDOW", 14, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Minimize button of a window. This class cannot be inherited. </summary>
		public static class MinButton
		{
			/// <summary>Gets a visual style element that represents a Minimize button in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Minimize button in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("WINDOW", 15, 4);

			/// <summary>Gets a visual style element that represents a Minimize button in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Minimize button in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("WINDOW", 15, 2);

			/// <summary>Gets a visual style element that represents a Minimize button in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Minimize button in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("WINDOW", 15, 1);

			/// <summary>Gets a visual style element that represents a Minimize button in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Minimize button in the pressed state.</returns>
			public static VisualStyleElement Pressed => CreateElement("WINDOW", 15, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the title bar of a minimized window. This class cannot be inherited. </summary>
		public static class MinCaption
		{
			/// <summary>Gets a visual style element that represents the title bar of a minimized active window.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a minimized active window.</returns>
			public static VisualStyleElement Active => CreateElement("WINDOW", 3, 1);

			/// <summary>Gets a visual style element that represents the title bar of a minimized disabled window.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a minimized disabled window.</returns>
			public static VisualStyleElement Disabled => CreateElement("WINDOW", 3, 3);

			/// <summary>Gets a visual style element that represents the title bar of a minimized inactive window.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a minimized inactive window.</returns>
			public static VisualStyleElement Inactive => CreateElement("WINDOW", 3, 2);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Restore button of a window. This class cannot be inherited. </summary>
		public static class RestoreButton
		{
			/// <summary>Gets a visual style element that represents a Restore button in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Restore button in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("WINDOW", 21, 4);

			/// <summary>Gets a visual style element that represents a Restore button in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Restore button in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("WINDOW", 21, 2);

			/// <summary>Gets a visual style element that represents a Restore button in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Restore button in the normal state. </returns>
			public static VisualStyleElement Normal => CreateElement("WINDOW", 21, 1);

			/// <summary>Gets a visual style element that represents a Restore button in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Restore button in the pressed state. </returns>
			public static VisualStyleElement Pressed => CreateElement("WINDOW", 21, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the title bar of a small window. This class cannot be inherited. </summary>
		public static class SmallCaption
		{
			/// <summary>Gets a visual style element that represents the title bar of an active small window.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of an active small window.</returns>
			public static VisualStyleElement Active => CreateElement("WINDOW", 2, 1);

			/// <summary>Gets a visual style element that represents the title bar of a disabled small window.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a disabled small window.</returns>
			public static VisualStyleElement Disabled => CreateElement("WINDOW", 2, 3);

			/// <summary>Gets a visual style element that represents the title bar of an inactive small window.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of an inactive small window.</returns>
			public static VisualStyleElement Inactive => CreateElement("WINDOW", 2, 2);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the title bar of a small window. This class cannot be inherited. </summary>
		public static class SmallCaptionSizingTemplate
		{
			/// <summary>Gets a visual style element that represents the sizing template of the title bar of a small window.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the title bar of a small window.</returns>
			public static VisualStyleElement Normal => CreateElement("WINDOW", 31, 0);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Close button of a small window. This class cannot be inherited. </summary>
		public static class SmallCloseButton
		{
			/// <summary>Gets a visual style element that represents the small Close button in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the small Close button in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("WINDOW", 19, 4);

			/// <summary>Gets a visual style element that represents the small Close button in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the small Close button in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("WINDOW", 19, 2);

			/// <summary>Gets a visual style element that represents the small Close button in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the small Close button in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("WINDOW", 19, 1);

			/// <summary>Gets a visual style element that represents the small Close button in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the small Close button in the pressed state.</returns>
			public static VisualStyleElement Pressed => CreateElement("WINDOW", 19, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the bottom border of a small window. This class cannot be inherited. </summary>
		public static class SmallFrameBottom
		{
			/// <summary>Gets a visual style element that represents the bottom border of an active small window. </summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the bottom border of an active small window.</returns>
			public static VisualStyleElement Active => CreateElement("WINDOW", 12, 1);

			/// <summary>Gets a visual style element that represents the bottom border of an inactive small window.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the bottom border of an inactive small window. </returns>
			public static VisualStyleElement Inactive => CreateElement("WINDOW", 12, 2);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the bottom border of a small window. This class cannot be inherited. </summary>
		public static class SmallFrameBottomSizingTemplate
		{
			/// <summary>Gets a visual style element that represents the sizing template of the bottom border of a small window.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the bottom border of a small window.</returns>
			public static VisualStyleElement Normal => CreateElement("WINDOW", 37, 0);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the left border of a small window. This class cannot be inherited. </summary>
		public static class SmallFrameLeft
		{
			/// <summary>Gets a visual style element that represents the left border of an active small window.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the left border of an active small window.</returns>
			public static VisualStyleElement Active => CreateElement("WINDOW", 10, 1);

			/// <summary>Gets a visual style element that represents the left border of an inactive small window. </summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the left border of an inactive small window. </returns>
			public static VisualStyleElement Inactive => CreateElement("WINDOW", 10, 2);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the left border of a small window. This class cannot be inherited. </summary>
		public static class SmallFrameLeftSizingTemplate
		{
			/// <summary>Gets a visual style element that represents the sizing template of the left border of a small window. </summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the left border of a small window. </returns>
			public static VisualStyleElement Normal => CreateElement("WINDOW", 33, 0);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the right border of a small window. This class cannot be inherited. </summary>
		public static class SmallFrameRight
		{
			/// <summary>Gets a visual style element that represents the right border of an active small window.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the right border of an active small window.</returns>
			public static VisualStyleElement Active => CreateElement("WINDOW", 11, 1);

			/// <summary>Gets a visual style element that represents the right border of an inactive small window.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the right border of an inactive small window.</returns>
			public static VisualStyleElement Inactive => CreateElement("WINDOW", 11, 2);
		}

		/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the sizing template of the right border of a small window. This class cannot be inherited. </summary>
		public static class SmallFrameRightSizingTemplate
		{
			/// <summary>Gets a visual style element that represents the sizing template of the right border of a small window.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the right border of a small window.</returns>
			public static VisualStyleElement Normal => CreateElement("WINDOW", 35, 0);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the title bar of a maximized small window. This class cannot be inherited. </summary>
		public static class SmallMaxCaption
		{
			/// <summary>Gets a visual style element that represents the title bar of an active small window that is maximized.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of an active small window that is maximized.</returns>
			public static VisualStyleElement Active => CreateElement("WINDOW", 6, 1);

			/// <summary>Gets a visual style element that represents the title bar of a disabled small window that is maximized.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a disabled small window that is maximized.</returns>
			public static VisualStyleElement Disabled => CreateElement("WINDOW", 6, 3);

			/// <summary>Gets a visual style element that represents the title bar of an inactive small window that is maximized.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of an inactive small window that is maximized.</returns>
			public static VisualStyleElement Inactive => CreateElement("WINDOW", 6, 2);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the title bar of a minimized small window. This class cannot be inherited. </summary>
		public static class SmallMinCaption
		{
			/// <summary>Gets a visual style element that represents the title bar of an active small window that is minimized.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of an active small window that is minimized.</returns>
			public static VisualStyleElement Active => CreateElement("WINDOW", 4, 1);

			/// <summary>Gets a visual style element that represents the title bar of a disabled small window that is minimized.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a disabled small window that is minimized.</returns>
			public static VisualStyleElement Disabled => CreateElement("WINDOW", 4, 3);

			/// <summary>Gets a visual style element that represents the title bar of an inactive small window that is minimized.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of an inactive small window that is minimized.</returns>
			public static VisualStyleElement Inactive => CreateElement("WINDOW", 4, 2);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the System button of a window. This class cannot be inherited. </summary>
		public static class SysButton
		{
			/// <summary>Gets a visual style element that represents a System button in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a System button in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("WINDOW", 13, 4);

			/// <summary>Gets a visual style element that represents a System button in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a System button in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("WINDOW", 13, 2);

			/// <summary>Gets a visual style element that represents a System button in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a System button in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("WINDOW", 13, 1);

			/// <summary>Gets a visual style element that represents a System button in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a System button in the pressed state.</returns>
			public static VisualStyleElement Pressed => CreateElement("WINDOW", 13, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the vertical scroll bar of a window. This class cannot be inherited. </summary>
		public static class VerticalScroll
		{
			/// <summary>Gets a visual style element that represents a vertical scroll bar in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll bar in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("WINDOW", 27, 4);

			/// <summary>Gets a visual style element that represents a vertical scroll bar in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll bar in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("WINDOW", 27, 2);

			/// <summary>Gets a visual style element that represents a vertical scroll bar in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll bar in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("WINDOW", 27, 1);

			/// <summary>Gets a visual style element that represents a vertical scroll bar in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll bar in the pressed state.</returns>
			public static VisualStyleElement Pressed => CreateElement("WINDOW", 27, 3);
		}

		/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the vertical scroll box (also known as the thumb) of a window. This class cannot be inherited. </summary>
		public static class VerticalThumb
		{
			/// <summary>Gets a visual style element that represents a vertical scroll box in the disabled state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll box in the disabled state.</returns>
			public static VisualStyleElement Disabled => CreateElement("WINDOW", 28, 4);

			/// <summary>Gets a visual style element that represents a vertical scroll box in the hot state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll box in the hot state.</returns>
			public static VisualStyleElement Hot => CreateElement("WINDOW", 28, 2);

			/// <summary>Gets a visual style element that represents a vertical scroll box in the normal state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll box in the normal state.</returns>
			public static VisualStyleElement Normal => CreateElement("WINDOW", 28, 1);

			/// <summary>Gets a visual style element that represents a vertical scroll box in the pressed state.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll box in the pressed state.</returns>
			public static VisualStyleElement Pressed => CreateElement("WINDOW", 28, 3);
		}
	}

	private const string BUTTON = "BUTTON";

	private const string CLOCK = "CLOCK";

	private const string COMBOBOX = "COMBOBOX";

	private const string DATEPICKER = "DATEPICKER";

	private const string EDIT = "EDIT";

	private const string EXPLORERBAR = "EXPLORERBAR";

	private const string HEADER = "HEADER";

	private const string LISTVIEW = "LISTVIEW";

	private const string MENU = "MENU";

	private const string MENUBAND = "MENUBAND";

	private const string PAGE = "PAGE";

	private const string PROGRESS = "PROGRESS";

	private const string REBAR = "REBAR";

	private const string SCROLLBAR = "SCROLLBAR";

	private const string SPIN = "SPIN";

	private const string STARTPANEL = "STARTPANEL";

	private const string STATUS = "STATUS";

	private const string TAB = "TAB";

	private const string TASKBAND = "TASKBAND";

	private const string TASKBAR = "TASKBAR";

	private const string TOOLBAR = "TOOLBAR";

	private const string TOOLTIP = "TOOLTIP";

	private const string TRACKBAR = "TRACKBAR";

	private const string TRAYNOTIFY = "TRAYNOTIFY";

	private const string TREEVIEW = "TREEVIEW";

	private const string WINDOW = "WINDOW";

	private string class_name;

	private int part;

	private int state;

	/// <summary>Gets the class name of the visual style element that this <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> represents.</summary>
	/// <returns>A string that represents the class name of a visual style element.</returns>
	public string ClassName => class_name;

	/// <summary>Gets a value indicating the part of the visual style element that this <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> represents.</summary>
	/// <returns>A value that represents the part of a visual style element.</returns>
	public int Part => part;

	/// <summary>Gets a value indicating the state of the visual style element that this <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> represents.</summary>
	/// <returns>A value that represents the state of a visual style element.</returns>
	public int State => state;

	internal VisualStyleElement(string className, int part, int state)
	{
		class_name = className;
		this.part = part;
		this.state = state;
	}

	/// <summary>Creates a new visual style element from the specified class, part, and state values.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> with the <see cref="P:System.Windows.Forms.VisualStyles.VisualStyleElement.ClassName" />, <see cref="P:System.Windows.Forms.VisualStyles.VisualStyleElement.Part" />, and <see cref="P:System.Windows.Forms.VisualStyles.VisualStyleElement.State" /> properties initialized to the <paramref name="className" />, <paramref name="part" />, and <paramref name="state" /> parameters.</returns>
	/// <param name="className">A string that represents the class name of the visual style element to be created.</param>
	/// <param name="part">A value that represents the part of the visual style element to be created.</param>
	/// <param name="state">A value that represents the state of the visual style element to be created.</param>
	public static VisualStyleElement CreateElement(string className, int part, int state)
	{
		return new VisualStyleElement(className, part, state);
	}
}
