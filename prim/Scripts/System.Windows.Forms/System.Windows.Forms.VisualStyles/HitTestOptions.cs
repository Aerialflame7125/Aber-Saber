namespace System.Windows.Forms.VisualStyles;

/// <summary>Specifies the options that can be used when performing a hit test on the background specified by a visual style.</summary>
[Flags]
public enum HitTestOptions
{
	/// <summary>The hit test option for the background segment.</summary>
	BackgroundSegment = 0,
	/// <summary>The hit test option for the fixed border.</summary>
	FixedBorder = 2,
	/// <summary>The hit test option for the caption.</summary>
	Caption = 4,
	/// <summary>The hit test option for the left resizing border.</summary>
	ResizingBorderLeft = 0x10,
	/// <summary>The hit test option for the top resizing border.</summary>
	ResizingBorderTop = 0x20,
	/// <summary>The hit test option for the right resizing border.</summary>
	ResizingBorderRight = 0x40,
	/// <summary>The hit test option for the bottom resizing border.</summary>
	ResizingBorderBottom = 0x80,
	/// <summary>The hit test option for the resizing border.</summary>
	ResizingBorder = 0xF0,
	/// <summary>The resizing border is specified as a template, not just window edges. This option is mutually exclusive with <see cref="F:System.Windows.Forms.VisualStyles.HitTestOptions.SystemSizingMargins" />; <see cref="F:System.Windows.Forms.VisualStyles.HitTestOptions.SizingTemplate" /> takes precedence.</summary>
	SizingTemplate = 0x100,
	/// <summary>The system resizing border width is used instead of visual style content margins. This option is mutually exclusive with <see cref="F:System.Windows.Forms.VisualStyles.HitTestOptions.SizingTemplate" />; <see cref="F:System.Windows.Forms.VisualStyles.HitTestOptions.SizingTemplate" /> takes precedence.</summary>
	SystemSizingMargins = 0x200
}
