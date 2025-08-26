using System.Configuration;
using System.Runtime.InteropServices;

namespace System.Web.UI.Design;

/// <summary>Provides an interface for accessing a Web application in a design host, such as Microsoft Visual Studio 2005, at design time.</summary>
[Guid("cff39fa8-5607-4b6d-86f3-cc80b3cfe2dd")]
public interface IWebApplication : IServiceProvider
{
	/// <summary>Gets the root project item from the design host.</summary>
	/// <returns>The root project item from the design host.</returns>
	IProjectItem RootProjectItem { get; }

	/// <summary>Returns a project item from a design host based on its URL.</summary>
	/// <param name="appRelativeUrl">The relative path to the project item to retrieve.</param>
	/// <returns>A project item from a design host based on its URL.</returns>
	IProjectItem GetProjectItemFromUrl(string appRelativeUrl);

	/// <summary>Returns a <see cref="T:System.Configuration.Configuration" /> object representing the current Web application in the design host.</summary>
	/// <param name="isReadOnly">
	///   <see langword="true" /> to indicate the returned <see cref="T:System.Configuration.Configuration" /> is editable; otherwise, <see langword="false" />.</param>
	/// <returns>An object representing the current Web application in the design host.</returns>
	System.Configuration.Configuration OpenWebConfiguration(bool isReadOnly);
}
