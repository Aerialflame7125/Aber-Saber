namespace System.Web.Services.Protocols;

/// <summary>Represents the attributes of a match made using text pattern matching. This class cannot be inherited.</summary>
[AttributeUsage(AttributeTargets.All)]
public sealed class MatchAttribute : Attribute
{
	private string pattern;

	private int group = 1;

	private int capture;

	private bool ignoreCase;

	private int repeats = -1;

	/// <summary>Gets or sets a regular expression that represents the pattern to match.</summary>
	/// <returns>A regular expression that represents the pattern to match.</returns>
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

	/// <summary>Gets or sets a value that represents a grouping of related matches.</summary>
	/// <returns>A value that represents a grouping of related matches </returns>
	public int Group
	{
		get
		{
			return group;
		}
		set
		{
			group = value;
		}
	}

	/// <summary>Gets or sets a value that represents the index of a match within a grouping.</summary>
	/// <returns>A value that represents the index of a match within a grouping.</returns>
	public int Capture
	{
		get
		{
			return capture;
		}
		set
		{
			capture = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether the pattern to match is case insensitive.</summary>
	/// <returns>
	///     <see langword="true" /> if matching is case insensitive; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
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

	/// <summary>Gets or sets the maximum number of values to return from the match.</summary>
	/// <returns>The maximum number of values to return from the match. The default value is -1, which refers to returning all values.</returns>
	public int MaxRepeats
	{
		get
		{
			return repeats;
		}
		set
		{
			repeats = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.MatchAttribute" /> class with the specified pattern.</summary>
	/// <param name="pattern">A string that represents the pattern to match. </param>
	public MatchAttribute(string pattern)
	{
		this.pattern = pattern;
	}
}
