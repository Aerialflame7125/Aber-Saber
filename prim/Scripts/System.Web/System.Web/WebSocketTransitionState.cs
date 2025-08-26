namespace System.Web;

internal enum WebSocketTransitionState : byte
{
	Inactive,
	AcceptWebSocketRequestCalled,
	TransitionStarted,
	TransitionCompleted
}
