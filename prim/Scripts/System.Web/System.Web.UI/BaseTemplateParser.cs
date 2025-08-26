using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Hosting;

namespace System.Web.UI;

/// <summary>Implements ASP.NET template parsing for template files.</summary>
public abstract class BaseTemplateParser : TemplateParser
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.BaseTemplateParser" /> class.</summary>
	protected BaseTemplateParser()
	{
	}

	/// <summary>Compiles and returns the type of the <see cref="T:System.Web.UI.Page" /> or <see cref="T:System.Web.UI.UserControl" /> control that is specified by the virtual path.</summary>
	/// <param name="virtualPath">The virtual path of the <see cref="T:System.Web.UI.Page" /> or <see cref="T:System.Web.UI.UserControl" />. </param>
	/// <returns>The type of the page or user control.</returns>
	/// <exception cref="T:System.Web.HttpException">The parser does not permit a virtual reference to the resource specified by <paramref name="virtualPath" />. </exception>
	protected Type GetReferencedType(string virtualPath)
	{
		if (string.IsNullOrEmpty(virtualPath))
		{
			throw new ArgumentNullException("virtualPath");
		}
		PageParserFilter pageParserFilter = base.PageParserFilter;
		if (pageParserFilter != null)
		{
			CompilationSection obj = (WebConfigurationManager.GetSection("system.web/compilation") as CompilationSection) ?? throw new HttpException("Internal error. Missing configuration section.");
			string extension = VirtualPathUtility.GetExtension(virtualPath);
			Type providerTypeForExtension = obj.BuildProviders.GetProviderTypeForExtension(extension);
			VirtualReferenceType referenceType = ((providerTypeForExtension == null) ? VirtualReferenceType.Other : ((!(providerTypeForExtension == typeof(PageBuildProvider))) ? ((providerTypeForExtension == typeof(UserControlBuildProvider)) ? VirtualReferenceType.UserControl : ((!(providerTypeForExtension == typeof(MasterPageBuildProvider))) ? VirtualReferenceType.SourceFile : VirtualReferenceType.Master)) : VirtualReferenceType.Page));
			if (!pageParserFilter.AllowVirtualReference(virtualPath, referenceType))
			{
				throw new HttpException("The parser does not permit a virtual reference to the UserControl.");
			}
		}
		virtualPath = HostingEnvironment.VirtualPathProvider.CombineVirtualPaths(base.VirtualPath.Absolute, virtualPath);
		return BuildManager.GetCompiledType(virtualPath);
	}

	/// <summary>Compiles and returns the type of the <see cref="T:System.Web.UI.UserControl" /> object that is specified by the virtual path.</summary>
	/// <param name="virtualPath">The virtual path of the <see cref="T:System.Web.UI.UserControl" />. </param>
	/// <returns>The type of the user control. </returns>
	/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Web.UI.UserControl" /> specified by <paramref name="virtualPath" /> is marked as no compile.- or -The parser does not permit a virtual reference to the <see cref="T:System.Web.UI.UserControl" />. </exception>
	[MonoTODO("We don't do anything here with the no-compile controls.")]
	protected internal Type GetUserControlType(string virtualPath)
	{
		return GetReferencedType(virtualPath);
	}
}
