using System.Collections;

namespace System.Web.UI.Design;

/// <summary>Provides a base class for accessing the types, directives, and controls in the current Web project document. This class must be inherited.</summary>
public abstract class WebFormsReferenceManager
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebFormsReferenceManager" /> class.</summary>
	protected WebFormsReferenceManager()
	{
	}

	/// <summary>Gets the register directives for the current project document.</summary>
	/// <returns>A collection of strings representing the register directives defined in the current document.</returns>
	public abstract ICollection GetRegisterDirectives();

	/// <summary>Gets the tag prefix for the specified object type.</summary>
	/// <param name="objectType">The type of the object.</param>
	/// <returns>The tag prefix for the specified object type, if found; otherwise, <see langword="null" />.</returns>
	public abstract string GetTagPrefix(Type objectType);

	/// <summary>Gets the object type with the specified tag prefix and tag name.</summary>
	/// <param name="tagPrefix">The tag prefix of the type to retrieve.</param>
	/// <param name="tagName">The tag name of the type to retrieve.</param>
	/// <returns>The <see cref="T:System.Type" /> of the object with the specified tag prefix and name, if found; otherwise, <see langword="null" />.</returns>
	public abstract Type GetType(string tagPrefix, string tagName);

	/// <summary>Gets the relative URL path for the user control with the specified tag prefix and tag name.</summary>
	/// <param name="tagPrefix">The tag prefix of the user control to retrieve.</param>
	/// <param name="tagName">The tag name of the user control to retrieve.</param>
	/// <returns>A string representing the relative URL path for the specified user control, if found; otherwise, <see langword="null" />.</returns>
	public abstract string GetUserControlPath(string tagPrefix, string tagName);

	/// <summary>Adds a tag prefix for the specified type.</summary>
	/// <param name="objectType">The type to add a tag prefix for in the current document.</param>
	/// <returns>The tag prefix string.</returns>
	public abstract string RegisterTagPrefix(Type objectType);
}
