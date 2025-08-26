using System.Threading;

namespace System.Web.Services.Protocols;

internal class UserToken
{
	private SendOrPostCallback callback;

	private object userState;

	internal SendOrPostCallback Callback => callback;

	internal object UserState => userState;

	internal UserToken(SendOrPostCallback callback, object userState)
	{
		this.callback = callback;
		this.userState = userState;
	}
}
