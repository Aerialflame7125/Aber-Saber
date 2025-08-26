using System.Security.Permissions;

namespace System.Web.UI;

/// <summary>Defines the tag prefix used in a Web page to identify custom controls. This class cannot be inherited.</summary>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class TagPrefixAttribute : Attribute
{
	private string namespaceName;

	private string tagPrefix;

	/// <summary>Gets the namespace prefix for the specified control.</summary>
	/// <returns>The namespace name.</returns>
	public string NamespaceName => namespaceName;

	/// <summary>Gets the tag prefix for the specified control.</summary>
	/// <returns>The tag prefix.</returns>
	public string TagPrefix => tagPrefix;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.TagPrefixAttribute" /> class.</summary>
	/// <param name="namespaceName">A string that identifies the custom control namespace. </param>
	/// <param name="tagPrefix">A string that identifies the custom control prefix. </param>
	/// <exception cref="T:System.ArgumentException">The <paramref name="namespaceName" /> or the <paramref name="tagPrefix" /> is <see langword="null" /> or an empty string ("").</exception>
	public TagPrefixAttribute(string namespaceName, string tagPrefix)
	{
		if (namespaceName == null || namespaceName.Length == 0)
		{
			throw new ArgumentNullException("namespaceName");
		}
		if (tagPrefix == null || tagPrefix.Length == 0)
		{
			throw new ArgumentNullException("tagPrefix");
		}
		this.namespaceName = namespaceName;
		this.tagPrefix = tagPrefix;
	}
}
