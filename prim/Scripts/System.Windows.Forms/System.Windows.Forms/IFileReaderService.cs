using System.IO;

namespace System.Windows.Forms;

/// <summary>Defines a method that opens a file from the current directory.</summary>
/// <filterpriority>2</filterpriority>
public interface IFileReaderService
{
	/// <summary>Opens a file from the current directory.</summary>
	/// <param name="relativePath">The file to open.</param>
	/// <filterpriority>1</filterpriority>
	Stream OpenFileFromSource(string relativePath);
}
