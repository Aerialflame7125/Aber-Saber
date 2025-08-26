namespace System.Web.UI.Design;

/// <summary>Provides an interface that can look up information about the types used in the current Web Forms project.</summary>
[Obsolete("Use new WebFormsReferenceManager feature")]
public interface IWebFormReferenceManager
{
	/// <summary>Gets the type of the specified object.</summary>
	/// <param name="tagPrefix">The tag prefix for the type.</param>
	/// <param name="typeName">The name of the type.</param>
	/// <returns>The <see cref="T:System.Type" /> of the object, if it could be resolved.</returns>
	Type GetObjectType(string tagPrefix, string typeName);

	/// <summary>Gets the register directives for the current project.</summary>
	/// <returns>The register directives for the current project.</returns>
	string GetRegisterDirectives();

	/// <summary>Gets the tag prefix for the specified type of object.</summary>
	/// <param name="objectType">The type of the object.</param>
	/// <returns>The tag prefix for the specified object type, if it could be located.</returns>
	string GetTagPrefix(Type objectType);
}
