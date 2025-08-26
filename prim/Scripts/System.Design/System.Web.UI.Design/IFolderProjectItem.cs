using System.Collections;

namespace System.Web.UI.Design;

/// <summary>Provides an interface for a project folder in a design host, such as Visual Studio 2005.</summary>
public interface IFolderProjectItem
{
	/// <summary>Gets a collection of items in a project folder in a design host, such as Visual Studio 2005.</summary>
	/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the items in the project folder of the design host.</returns>
	ICollection Children { get; }

	/// <summary>Adds a document to a project folder in a design host, such as Visual Studio 2005.</summary>
	/// <param name="name">The name of the document.</param>
	/// <param name="content">An array of type <see cref="T:System.Byte" /> containing the document contents.</param>
	/// <returns>An <see cref="T:System.Web.UI.Design.IDocumentProjectItem" /> representing the added document.</returns>
	IDocumentProjectItem AddDocument(string name, byte[] content);

	/// <summary>Creates a new folder in a project folder of a design host, such as Visual Studio 2005.</summary>
	/// <param name="name">The name for the new folder.</param>
	/// <returns>An <see cref="T:System.Web.UI.Design.IFolderProjectItem" /> representing the new folder.</returns>
	IFolderProjectItem AddFolder(string name);
}
