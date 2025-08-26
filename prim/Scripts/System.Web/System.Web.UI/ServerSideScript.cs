using System.Web.Compilation;

namespace System.Web.UI;

internal class ServerSideScript
{
	public readonly string Script;

	public readonly ILocation Location;

	public ServerSideScript(string script, ILocation location)
	{
		Script = script;
		Location = location;
	}
}
