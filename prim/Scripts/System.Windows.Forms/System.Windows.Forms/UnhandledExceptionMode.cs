namespace System.Windows.Forms;

/// <summary>Defines where a Windows Forms application should send unhandled exceptions.</summary>
public enum UnhandledExceptionMode
{
	/// <summary>Route all exceptions to the <see cref="E:System.Windows.Forms.Application.ThreadException" /> handler, unless the application's configuration file specifies otherwise.</summary>
	Automatic,
	/// <summary>Never route exceptions to the <see cref="E:System.Windows.Forms.Application.ThreadException" /> handler. Ignore the application configuration file.</summary>
	ThrowException,
	/// <summary>Always route exceptions to the <see cref="E:System.Windows.Forms.Application.ThreadException" /> handler. Ignore the application configuration file.</summary>
	CatchException
}
