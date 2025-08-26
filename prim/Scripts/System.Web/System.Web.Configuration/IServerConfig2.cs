namespace System.Web.Configuration;

internal interface IServerConfig2
{
	bool IsWithinApp(string virtualPath);
}
