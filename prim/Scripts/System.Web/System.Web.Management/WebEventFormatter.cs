namespace System.Web.Management;

/// <summary>Formats ASP.NET health monitoring event information.</summary>
public class WebEventFormatter
{
	private int indentation_level;

	private int tab_size;

	/// <summary>Gets or sets the indentation level.</summary>
	/// <returns>The number of tabs used for the indentation level. </returns>
	public int IndentationLevel
	{
		get
		{
			return indentation_level;
		}
		set
		{
			indentation_level = value;
		}
	}

	/// <summary>Gets or sets the tab size.</summary>
	/// <returns>The number of spaces in a tab.</returns>
	public int TabSize
	{
		get
		{
			return tab_size;
		}
		set
		{
			tab_size = value;
		}
	}

	internal WebEventFormatter()
	{
	}

	/// <summary>Appends the specified string and a carriage return to the event information.</summary>
	/// <param name="s">The string to add to the event information.</param>
	public void AppendLine(string s)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns the event information in string format.</summary>
	/// <returns>The event information.</returns>
	public new string ToString()
	{
		throw new NotImplementedException();
	}
}
