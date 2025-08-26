namespace System.Web;

internal interface IRequestCompletedNotifier
{
	bool IsRequestCompleted { get; }
}
