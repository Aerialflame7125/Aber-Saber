using System.Security.Permissions;
using System.Web.Configuration;
using System.Web.UI;

namespace System.Web;

/// <summary>Enables the server to gather information on the capabilities of the browser that is running on the client.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class HttpBrowserCapabilities : HttpCapabilitiesBase, IFilterResolutionService
{
	/// <summary>Creates a new instance of the <see cref="T:System.Web.HttpBrowserCapabilities" /> class.</summary>
	public HttpBrowserCapabilities()
	{
	}

	bool IFilterResolutionService.EvaluateFilter(string filterName)
	{
		throw new NotImplementedException();
	}

	int IFilterResolutionService.CompareFilters(string filter1, string filter2)
	{
		throw new NotImplementedException();
	}
}
