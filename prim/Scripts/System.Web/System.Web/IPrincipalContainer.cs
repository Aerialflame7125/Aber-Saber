using System.Security.Principal;

namespace System.Web;

internal interface IPrincipalContainer
{
	IPrincipal Principal { get; set; }
}
