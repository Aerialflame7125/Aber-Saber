namespace System.Windows.Forms.VisualStyles;

/// <summary>Provides basic information about the font specified by a visual style for a particular element.</summary>
public struct TextMetrics
{
	private int ascent;

	private int average_char_width;

	private char break_char;

	private TextMetricsCharacterSet char_set;

	private char default_char;

	private int descent;

	private int digitized_aspect_x;

	private int digitized_aspect_y;

	private int external_leading;

	private char first_char;

	private int height;

	private int internal_leading;

	private bool italic;

	private char last_char;

	private int max_char_width;

	private int overhang;

	private TextMetricsPitchAndFamilyValues pitch_and_family;

	private bool struck_out;

	private bool underlined;

	private int weight;

	/// <summary>Gets or sets the ascent of characters in the font.</summary>
	/// <returns>The ascent of characters in the font.</returns>
	public int Ascent
	{
		get
		{
			return ascent;
		}
		set
		{
			ascent = value;
		}
	}

	/// <summary>Gets or sets the average width of characters in the font.</summary>
	/// <returns>The average width of characters in the font.</returns>
	public int AverageCharWidth
	{
		get
		{
			return average_char_width;
		}
		set
		{
			average_char_width = value;
		}
	}

	/// <summary>Gets or sets the character used to define word breaks for text justification.</summary>
	/// <returns>The character used to define word breaks for text justification.</returns>
	public char BreakChar
	{
		get
		{
			return break_char;
		}
		set
		{
			break_char = value;
		}
	}

	/// <summary>Gets or sets the character set of the font.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.VisualStyles.TextMetricsCharacterSet" /> values that specifies the character set of the font.</returns>
	public TextMetricsCharacterSet CharSet
	{
		get
		{
			return char_set;
		}
		set
		{
			char_set = value;
		}
	}

	/// <summary>Gets or sets the character to be substituted for characters not in the font.</summary>
	/// <returns>The character to be substituted for characters not in the font.</returns>
	public char DefaultChar
	{
		get
		{
			return default_char;
		}
		set
		{
			default_char = value;
		}
	}

	/// <summary>Gets or sets the descent of characters in the font.</summary>
	/// <returns>The descent of characters in the font.</returns>
	public int Descent
	{
		get
		{
			return descent;
		}
		set
		{
			descent = value;
		}
	}

	/// <summary>Gets or sets the horizontal aspect of the device for which the font was designed.</summary>
	/// <returns>The horizontal aspect of the device for which the font was designed.</returns>
	public int DigitizedAspectX
	{
		get
		{
			return digitized_aspect_x;
		}
		set
		{
			digitized_aspect_x = value;
		}
	}

	/// <summary>Gets or sets the vertical aspect of the device for which the font was designed.</summary>
	/// <returns>The vertical aspect of the device for which the font was designed.</returns>
	public int DigitizedAspectY
	{
		get
		{
			return digitized_aspect_y;
		}
		set
		{
			digitized_aspect_y = value;
		}
	}

	/// <summary>Gets or sets the amount of extra leading that the application adds between rows.</summary>
	/// <returns>The amount of extra leading (space) required between rows. </returns>
	public int ExternalLeading
	{
		get
		{
			return external_leading;
		}
		set
		{
			external_leading = value;
		}
	}

	/// <summary>Gets or sets the first character defined in the font.</summary>
	/// <returns>The first character defined in the font.</returns>
	public char FirstChar
	{
		get
		{
			return first_char;
		}
		set
		{
			first_char = value;
		}
	}

	/// <summary>Gets or sets the height of characters in the font.</summary>
	/// <returns>The height of characters in the font.</returns>
	public int Height
	{
		get
		{
			return height;
		}
		set
		{
			height = value;
		}
	}

	/// <summary>Gets or sets the amount of leading inside the bounds set by the <see cref="P:System.Windows.Forms.VisualStyles.TextMetrics.Height" /> property. </summary>
	/// <returns>The amount of leading inside the bounds set by the <see cref="P:System.Windows.Forms.VisualStyles.TextMetrics.Height" /> property.</returns>
	public int InternalLeading
	{
		get
		{
			return internal_leading;
		}
		set
		{
			internal_leading = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the font is italic.</summary>
	/// <returns>true if the font is italic; otherwise, false.</returns>
	public bool Italic
	{
		get
		{
			return italic;
		}
		set
		{
			italic = value;
		}
	}

	/// <summary>Gets or sets the last character defined in the font.</summary>
	/// <returns>The last character defined in the font.</returns>
	public char LastChar
	{
		get
		{
			return last_char;
		}
		set
		{
			last_char = value;
		}
	}

	/// <summary>Gets or sets the width of the widest character in the font.</summary>
	/// <returns>The width of the widest character in the font.</returns>
	public int MaxCharWidth
	{
		get
		{
			return max_char_width;
		}
		set
		{
			max_char_width = value;
		}
	}

	/// <summary>Gets or sets the extra width per string that may be added to some synthesized fonts.</summary>
	/// <returns>The extra width per string that may be added to some synthesized fonts.</returns>
	public int Overhang
	{
		get
		{
			return overhang;
		}
		set
		{
			overhang = value;
		}
	}

	/// <summary>Gets or sets information about the pitch, technology, and family of a physical font.</summary>
	/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.VisualStyles.TextMetricsPitchAndFamilyValues" /> values that specifies the pitch, technology, and family of a physical font.</returns>
	public TextMetricsPitchAndFamilyValues PitchAndFamily
	{
		get
		{
			return pitch_and_family;
		}
		set
		{
			pitch_and_family = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the font specifies a horizontal line through the characters.</summary>
	/// <returns>true if the font has a horizontal line through the characters; otherwise, false.</returns>
	public bool StruckOut
	{
		get
		{
			return struck_out;
		}
		set
		{
			struck_out = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the font is underlined.</summary>
	/// <returns>true if the font is underlined; otherwise, false.</returns>
	public bool Underlined
	{
		get
		{
			return underlined;
		}
		set
		{
			underlined = value;
		}
	}

	/// <summary>Gets or sets the weight of the font.</summary>
	/// <returns>The weight of the font.</returns>
	public int Weight
	{
		get
		{
			return weight;
		}
		set
		{
			weight = value;
		}
	}
}
