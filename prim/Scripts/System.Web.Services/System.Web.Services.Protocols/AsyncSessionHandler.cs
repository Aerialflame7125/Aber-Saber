using System.Web.SessionState;

namespace System.Web.Services.Protocols;

internal class AsyncSessionHandler : AsyncSessionlessHandler, IRequiresSessionState
{
	internal AsyncSessionHandler(ServerProtocol protocol)
		: base(protocol)
	{
	}
}
