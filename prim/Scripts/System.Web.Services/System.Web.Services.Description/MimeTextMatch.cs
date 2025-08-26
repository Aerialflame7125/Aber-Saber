using System.ComponentModel;
using System.Globalization;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Represents a text pattern for which the HTTP transmission is searched. This class cannot be inherited.</summary>
public sealed class MimeTextMatch
{
	private string name;

	private string type;

	private int repeats = 1;

	private string pattern;

	private int group = 1;

	private int capture;

	private bool ignoreCase;

	private MimeTextMatchCollection matches = new MimeTextMatchCollection();

	/// <summary>Gets or sets the name of the <see cref="T:System.Web.Services.Description.MimeTextMatch" />.</summary>
	/// <returns>The name of the <see cref="T:System.Web.Services.Description.MimeTextMatch" />.</returns>
	[XmlAttribute("name")]
	public string Name
	{
		get
		{
			if (name != null)
			{
				return name;
			}
			return string.Empty;
		}
		set
		{
			name = value;
		}
	}

	/// <summary>Gets or sets a value indicating the MIME format of the text to be searched.</summary>
	/// <returns>A string indicating the MIME format of the text to be searched.</returns>
	[XmlAttribute("type")]
	public string Type
	{
		get
		{
			if (type != null)
			{
				return type;
			}
			return string.Empty;
		}
		set
		{
			type = value;
		}
	}

	/// <summary>Gets or sets a value indicating the number of groups in which to place the results of the text search.</summary>
	/// <returns>A 32-bit signed integer. The default value is 1.</returns>
	/// <exception cref="T:System.ArgumentException">The property value is negative. </exception>
	[XmlAttribute("group")]
	[DefaultValue(1)]
	public int Group
	{
		get
		{
			return group;
		}
		set
		{
			if (value < 0)
			{
				throw new ArgumentException(Res.GetString("WebNegativeValue", "group"));
			}
			group = value;
		}
	}

	/// <summary>Gets or sets a value indicating the zero-based index of a <see cref="T:System.Web.Services.Description.MimeTextMatch" /> within a group.</summary>
	/// <returns>A 32-bit signed integer. The default value is 0, indicating that the <see cref="T:System.Web.Services.Description.MimeTextMatch" /> is the first instance within a group.</returns>
	/// <exception cref="T:System.ArgumentException">The property value is negative. </exception>
	[XmlAttribute("capture")]
	[DefaultValue(0)]
	public int Capture
	{
		get
		{
			return capture;
		}
		set
		{
			if (value < 0)
			{
				throw new ArgumentException(Res.GetString("WebNegativeValue", "capture"));
			}
			capture = value;
		}
	}

	/// <summary>Gets or sets a value indicating the number of times the search is to be performed.</summary>
	/// <returns>A 32-bit signed integer. The default value is 1.</returns>
	/// <exception cref="T:System.ArgumentException">The property value is negative. </exception>
	[XmlIgnore]
	public int Repeats
	{
		get
		{
			return repeats;
		}
		set
		{
			if (value < 0)
			{
				throw new ArgumentException(Res.GetString("WebNegativeValue", "repeats"));
			}
			repeats = value;
		}
	}

	/// <summary>Gets or sets a value indicating the number of times the search is to be performed.</summary>
	/// <returns>A string indicating the number of times the search is to be performed. The default value is "1".</returns>
	[XmlAttribute("repeats")]
	[DefaultValue("1")]
	public string RepeatsString
	{
		get
		{
			if (repeats != int.MaxValue)
			{
				return repeats.ToString(CultureInfo.InvariantCulture);
			}
			return "*";
		}
		set
		{
			if (value == "*")
			{
				repeats = int.MaxValue;
			}
			else
			{
				Repeats = int.Parse(value, CultureInfo.InvariantCulture);
			}
		}
	}

	/// <summary>Gets or sets the text pattern for the search.</summary>
	/// <returns>A string representing the text for which to search the HTTP transmission. The default value is an empty string ("").</returns>
	[XmlAttribute("pattern")]
	public string Pattern
	{
		get
		{
			if (pattern != null)
			{
				return pattern;
			}
			return string.Empty;
		}
		set
		{
			pattern = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the search should disregard the case of the text to be searched.</summary>
	/// <returns>
	///     <see langword="true" /> if the search should disregard case; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[XmlAttribute("ignoreCase")]
	public bool IgnoreCase
	{
		get
		{
			return ignoreCase;
		}
		set
		{
			ignoreCase = value;
		}
	}

	/// <summary>Gets the collection of text pattern matches that have been found by the search.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Description.MimeTextMatchCollection" /> representing the members of the <see cref="P:System.Web.Services.Description.MimeTextMatch.Group" /> property.</returns>
	[XmlElement("match")]
	public MimeTextMatchCollection Matches => matches;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.MimeTextMatch" /> class. </summary>
	public MimeTextMatch()
	{
	}
}
