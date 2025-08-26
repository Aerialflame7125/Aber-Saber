namespace System.Windows.Forms.VisualStyles;

/// <summary>Specifies the visual state of a check box that is drawn with visual styles.</summary>
public enum CheckBoxState
{
	/// <summary>The check box is unchecked.</summary>
	UncheckedNormal = 1,
	/// <summary>The check box is unchecked and hot.</summary>
	UncheckedHot,
	/// <summary>The check box is unchecked and pressed.</summary>
	UncheckedPressed,
	/// <summary>The check box is unchecked and disabled.</summary>
	UncheckedDisabled,
	/// <summary>The check box is checked.</summary>
	CheckedNormal,
	/// <summary>The check box is checked and hot.</summary>
	CheckedHot,
	/// <summary>The check box is checked and pressed.</summary>
	CheckedPressed,
	/// <summary>The check box is checked and disabled.</summary>
	CheckedDisabled,
	/// <summary>The check box is three-state.</summary>
	MixedNormal,
	/// <summary>The check box is three-state and hot.</summary>
	MixedHot,
	/// <summary>The check box is three-state and pressed.</summary>
	MixedPressed,
	/// <summary>The check box is three-state and disabled.</summary>
	MixedDisabled
}
