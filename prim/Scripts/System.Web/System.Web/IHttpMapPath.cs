namespace System.Web;

internal interface IHttpMapPath
{
	string MachineConfigPath { get; }

	string MapPath(string path);
}
