using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Web;

/// <summary>The exception that is thrown when a parse error occurs.</summary>
[Serializable]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class HttpParseException : HttpException
{
	private int line;

	private string virtualPath;

	private ParserErrorCollection errors = new ParserErrorCollection();

	/// <summary>Gets the name of the file being parsed when the error occurs.</summary>
	/// <returns>The physical path to the source file that is being parsed when the error occurs; otherwise, <see langword="null" />, if the physical path is <see langword="null" />.</returns>
	public string FileName => virtualPath;

	/// <summary>Gets the number of the line being parsed when the error occurs.</summary>
	/// <returns>The number of the line being parsed when the error occurs. This value is 1-based, not 0-based.</returns>
	public int Line => line;

	/// <summary>Gets the virtual path to source file that generated the error.</summary>
	/// <returns>The virtual path to the source file that generated the error.</returns>
	public string VirtualPath => virtualPath;

	/// <summary>Gets the parser errors for the current exception.</summary>
	/// <returns>A collection of the parser errors for the current exception.</returns>
	public ParserErrorCollection ParserErrors => errors;

	internal HttpParseException(string message, string virtualPath, int line)
		: base(message)
	{
		this.virtualPath = virtualPath;
		this.line = line;
	}

	private HttpParseException(SerializationInfo info, StreamingContext context)
		: base(info, context)
	{
		line = info.GetInt32("_line");
		virtualPath = info.GetString("_virtualPath");
		errors = info.GetValue("_parserErrors", typeof(ParserErrorCollection)) as ParserErrorCollection;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpParseException" /> class.</summary>
	public HttpParseException()
		: this("External component has thrown an exception")
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpParseException" /> class with a specified error message. </summary>
	/// <param name="message">The exception message to specify when the error occurs.</param>
	public HttpParseException(string message)
		: base(message)
	{
		errors.Add(new ParserError(message, null, 0));
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpParseException" /> class with a specified error message and a reference to the inner. </summary>
	/// <param name="message">The exception message to specify when the error occurs.</param>
	/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
	public HttpParseException(string message, Exception innerException)
		: base(message, innerException)
	{
		errors.Add(new ParserError(message, null, 0));
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpParseException" /> class with specific information about the source code being compiled and the line number on which the exception occurred. </summary>
	/// <param name="message">The exception message to specify when the error occurs.</param>
	/// <param name="innerException">The exception that is the cause of the current exception. If <paramref name="innerException" /> is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
	/// <param name="virtualPath">The virtual path for the exception.</param>
	/// <param name="sourceCode">The source code being compiled when the exception occurs.</param>
	/// <param name="line">The line number on which the exception occurred.</param>
	public HttpParseException(string message, Exception innerException, string virtualPath, string sourceCode, int line)
		: base(message, innerException)
	{
		this.virtualPath = virtualPath;
		this.line = line;
		errors.Add(new ParserError(message, virtualPath, line));
	}

	/// <summary>When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with information about the exception.</summary>
	/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
	/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is <see langword="null" />.</exception>
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		base.GetObjectData(info, context);
		info.AddValue("_virtualPath", virtualPath);
		info.AddValue("_parserErrors", errors);
		info.AddValue("_line", line);
	}
}
