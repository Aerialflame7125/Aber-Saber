using System.Security.Permissions;
using System.Xml;

namespace System.Web.UI.Design;

/// <summary>Represents the structure, or schema, of an <see cref="T:System.Xml.XmlDocument" />. This class cannot be inherited.</summary>
[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
public sealed class XmlDocumentSchema : IDataSourceSchema
{
	/// <summary>Creates an instance of the <see cref="T:System.Web.UI.Design.XmlDocumentSchema" /> class using a specified <see cref="T:System.Xml.XmlDocument" /> and an XPath string.</summary>
	/// <param name="xmlDocument">An instance of an <see cref="T:System.Xml.XmlDocument" /> object.</param>
	/// <param name="xPath">An XPath string identifying the child nodes of the document root.</param>
	[System.MonoTODO]
	public XmlDocumentSchema(XmlDocument xmlDocument, string xPath)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets an array containing information about each child node starting at the node identified by the <paramref name="xPath" /> parameter in the constructor, or each child node starting at the document root if the <paramref name="xPath" /> parameter is empty.</summary>
	/// <returns>An array of <see cref="T:System.Web.UI.Design.IDataSourceViewSchema" /> objects.</returns>
	[System.MonoTODO]
	public IDataSourceViewSchema[] GetViews()
	{
		throw new NotImplementedException();
	}
}
