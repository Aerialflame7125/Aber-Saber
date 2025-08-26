using System.CodeDom;
using System.Security.Permissions;

namespace System.Web.Services.Description;

/// <summary>Provides a common interface and functionality for classes to generate code attributes that specify SOAP extensions.</summary>
[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
public abstract class SoapExtensionImporter
{
	private SoapProtocolImporter protocolImporter;

	/// <summary>Gets or sets the <see cref="T:System.Web.Services.Description.SoapProtocolImporter" /> instance that invokes the <see cref="M:System.Web.Services.Description.SoapExtensionImporter.ImportMethod(System.CodeDom.CodeAttributeDeclarationCollection)" /> method.</summary>
	/// <returns>The <see cref="T:System.Web.Services.Description.SoapProtocolImporter" /> instance that invokes the <see cref="M:System.Web.Services.Description.SoapExtensionImporter.ImportMethod(System.CodeDom.CodeAttributeDeclarationCollection)" /> method.</returns>
	public SoapProtocolImporter ImportContext
	{
		get
		{
			return protocolImporter;
		}
		set
		{
			protocolImporter = value;
		}
	}

	/// <summary>When overridden in a derived class, adds code attribute declarations to any method that represents an operation in a binding.</summary>
	/// <param name="metadata">A <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> into which the <see cref="M:System.Web.Services.Description.SoapExtensionImporter.ImportMethod(System.CodeDom.CodeAttributeDeclarationCollection)" />  method can place new <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> instances.</param>
	public abstract void ImportMethod(CodeAttributeDeclarationCollection metadata);

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.SoapExtensionImporter" /> class. </summary>
	protected SoapExtensionImporter()
	{
	}
}
