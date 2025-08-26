using System.IO;

namespace System.Web.UI.Design;

/// <summary>Provides an interface for accessing a document item retrieved from a design host at design time.</summary>
public interface IDocumentProjectItem
{
	/// <summary>Provides access to the contents of a document item that is retrieved from the design host.</summary>
	/// <returns>A <see cref="T:System.IO.Stream" /> object.</returns>
	Stream GetContents();

	/// <summary>Opens a document item that is retrieved from the design host.</summary>
	void Open();
}
