using System.Collections;

namespace System.Web.UI.Design;

/// <summary>Provides an interface for access to a master page from a content page at design time, if provided by a design host, such as Visual Studio 2005.</summary>
public interface IContentResolutionService
{
	/// <summary>Gets the <see cref="T:System.Web.UI.Design.ContentDefinition" /> objects for the content placeholders that are identified in the master page.</summary>
	/// <returns>An <see cref="T:System.Collections.IDictionary" /> containing identifiers and <see cref="T:System.Web.UI.Design.ContentDefinition" /> objects.</returns>
	IDictionary ContentDefinitions { get; }

	/// <summary>Retrieves the current state of the identified content place holder.</summary>
	/// <param name="identifier">The identifier for a content place holder.</param>
	/// <returns>The current state of the identified content placeholder.</returns>
	ContentDesignerState GetContentDesignerState(string identifier);

	/// <summary>Sets the current state of the identified content place holder.</summary>
	/// <param name="identifier">The identifier for a content place holder.</param>
	/// <param name="state">A <see cref="T:System.Web.UI.Design.ContentDesignerState" />.</param>
	void SetContentDesignerState(string identifier, ContentDesignerState state);
}
