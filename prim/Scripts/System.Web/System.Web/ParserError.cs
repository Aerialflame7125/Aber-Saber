namespace System.Web;

/// <summary>Represents a parser error or warning. This class cannot be inherited. </summary>
[Serializable]
public sealed class ParserError
{
	private string _errorText;

	private string _virtualPath;

	private int _line;

	/// <summary>Gets or sets a string that represents the error for the <see cref="T:System.Web.ParserError" /> object.</summary>
	/// <returns>A string containing the error message.</returns>
	public string ErrorText
	{
		get
		{
			return _errorText;
		}
		set
		{
			_errorText = value;
		}
	}

	/// <summary>Gets or set the virtual path of the file that was being parsed when the error occurred.</summary>
	/// <returns>A string that specifies the virtual path of the file that contains the parser error.</returns>
	public string VirtualPath
	{
		get
		{
			return _virtualPath;
		}
		set
		{
			_virtualPath = value;
		}
	}

	/// <summary>Gets or sets the line number of the source at which the error occurs.</summary>
	/// <returns>The source line number where the parser encountered the error.</returns>
	public int Line
	{
		get
		{
			return _line;
		}
		set
		{
			_line = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.ParserError" /> class.</summary>
	public ParserError()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.ParserError" /> class by using the specified error text, virtual path, and source line number.</summary>
	/// <param name="errorText">The error message text.</param>
	/// <param name="virtualPath">The virtual path of the file being parsed when the error occurred.</param>
	/// <param name="line">The line number of the error source.</param>
	public ParserError(string errorText, string virtualPath, int line)
	{
		_errorText = errorText;
		_virtualPath = virtualPath;
		_line = line;
	}
}
