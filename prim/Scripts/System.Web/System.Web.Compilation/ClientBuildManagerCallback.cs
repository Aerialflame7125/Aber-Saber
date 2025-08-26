using System.CodeDom.Compiler;

namespace System.Web.Compilation;

/// <summary>Receives status information about a build from the <see cref="T:System.Web.Compilation.ClientBuildManager" /> object.</summary>
public class ClientBuildManagerCallback : MarshalByRefObject
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.ClientBuildManagerCallback" /> class. </summary>
	public ClientBuildManagerCallback()
	{
	}

	/// <summary>Reports compilation errors and warnings that occur during an application build.</summary>
	/// <param name="error">The error or warning encountered during compilation. </param>
	public virtual void ReportCompilerError(CompilerError error)
	{
	}

	/// <summary>Reports parsing errors and warnings that occur during an application build.</summary>
	/// <param name="error">The error or warning encountered during parsing.</param>
	public virtual void ReportParseError(ParserError error)
	{
	}

	/// <summary>Reports the progress of an application build.</summary>
	/// <param name="message">A <see cref="T:System.String" /> containing the current status of the build.</param>
	public virtual void ReportProgress(string message)
	{
	}
}
