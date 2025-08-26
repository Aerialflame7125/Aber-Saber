namespace System.Windows.Forms;

/// <summary>Defines constants that indicate how a <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" /> is displayed.</summary>
public enum DataGridViewComboBoxDisplayStyle
{
	/// <summary>When it is not in edit mode, the <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" /> mimics the appearance of a <see cref="T:System.Windows.Forms.ComboBox" /> control.</summary>
	ComboBox,
	/// <summary>When it is not in edit mode, the <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" /> is displayed with a drop-down button but does not otherwise mimic the appearance of a <see cref="T:System.Windows.Forms.ComboBox" /> control.</summary>
	DropDownButton,
	/// <summary>When it is not in edit mode, the <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" /> is displayed without a drop-down button.</summary>
	Nothing
}
