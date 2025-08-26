using System.CodeDom.Compiler;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Web;

/// <summary>The exception that is thrown when a compiler error occurs.</summary>
[Serializable]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class HttpCompileException : HttpException
{
	private CompilerResults results;

	private string sourceCode;

	/// <summary>Gets compiler output and error information for the exception.</summary>
	/// <returns>A <see cref="T:System.CodeDom.Compiler.CompilerResults" /> containing compiler output and error information.</returns>
	public CompilerResults Results
	{
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.High)]
		get
		{
			return results;
		}
	}

	/// <summary>Gets a string containing the path to the file that contains the source code being compiled when the error occurs.</summary>
	/// <returns>The path of the source file being compiled when the error occurs. The default is <see langword="null" />.</returns>
	public string SourceCode
	{
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.High)]
		get
		{
			return sourceCode;
		}
	}

	/// <summary>Gets a message that describes the reason for the current exception.</summary>
	/// <returns>A string that describes the first compilation error listed in the compiler results. If no compiler results were provided, the property returns the error message provided for this exception, or an empty string (""), if no error message was provided.</returns>
	public override string Message => base.Message;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpCompileException" /> class.</summary>
	public HttpCompileException()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpCompileException" /> class.</summary>
	/// <param name="message">The exception message to specify when the error occurs.</param>
	public HttpCompileException(string message)
		: base(message)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpCompileException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	/// <param name="innerException">The exception that is the cause of the current exception. If <paramref name="innerException" /> is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
	public HttpCompileException(string message, Exception innerException)
		: base(message, innerException)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpCompileException" /> class.</summary>
	/// <param name="results">A <see cref="T:System.CodeDom.Compiler.CompilerResults" /> containing compiler output and error information. </param>
	/// <param name="sourceCode">The path to the file that contains the source code being compiled when the error occurs.</param>
	public HttpCompileException(CompilerResults results, string sourceCode)
	{
		this.results = results;
		this.sourceCode = sourceCode;
	}

	/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with information about the exception.</summary>
	/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
	/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		base.GetObjectData(info, context);
		sourceCode = info.GetString("sourcecode");
		results = (CompilerResults)info.GetValue("results", typeof(CompilerResults));
	}
}
