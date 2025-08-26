using System.IO;
using System.Security.Permissions;
using System.Web.Compilation;

namespace System.Web.UI;

/// <summary>Provides a parser for Web service handlers. </summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class WebServiceParser : SimpleWebHandlerParser
{
	/// <summary>Gets the default directive name.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the default directive name.</returns>
	protected override string DefaultDirectiveName => "webservice";

	private WebServiceParser(HttpContext context, string virtualPath, string physicalPath)
		: base(context, virtualPath, physicalPath)
	{
	}

	internal WebServiceParser(HttpContext context, VirtualPath virtualPath, TextReader reader)
		: this(context, virtualPath, null, reader)
	{
	}

	internal WebServiceParser(HttpContext context, VirtualPath virtualPath, string physicalPath, TextReader reader)
		: base(context, virtualPath.Original, physicalPath, reader)
	{
	}

	/// <summary>Returns the compiled type for a given input file.</summary>
	/// <param name="inputFile">The file to be compiled. </param>
	/// <param name="context">The <see cref="T:System.Web.HttpContext" /> object for the current request. </param>
	/// <returns>A <see cref="T:System.Type" /> as specified by the <see cref="T:System.Web.HttpContext" />.</returns>
	public static Type GetCompiledType(string inputFile, HttpContext context)
	{
		return BuildManager.GetCompiledType(inputFile);
	}
}
