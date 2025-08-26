using System.Web.SessionState;

namespace System.Web.Services.Protocols;

internal class SyncSessionHandler : SyncSessionlessHandler, IRequiresSessionState
{
	internal SyncSessionHandler(ServerProtocol protocol)
		: base(protocol)
	{
	}
}
