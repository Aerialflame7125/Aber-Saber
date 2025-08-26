namespace System.Windows.Forms.VisualStyles;

/// <summary>Specifies the visual effects that can be applied to the edges of a visual style element.</summary>
[Flags]
public enum EdgeEffects
{
	/// <summary>The border is drawn without any effects.</summary>
	None = 0,
	/// <summary>The area within the element borders is filled.</summary>
	FillInterior = 0x800,
	/// <summary>The border is flat.</summary>
	Flat = 0x1000,
	/// <summary>The border is soft.</summary>
	Soft = 0x4000,
	/// <summary>The border is one-dimensional.</summary>
	Mono = 0x8000
}
