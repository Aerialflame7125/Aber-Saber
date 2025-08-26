#define UNITY_ASSERTIONS
namespace UnityEngine.Experimental.UIElements;

public static class MouseCaptureController
{
	internal static IEventHandler mouseCapture { get; private set; }

	public static bool IsMouseCaptureTaken()
	{
		return mouseCapture != null;
	}

	public static bool HasMouseCapture(this IEventHandler handler)
	{
		return mouseCapture == handler;
	}

	public static void TakeMouseCapture(this IEventHandler handler)
	{
		if (mouseCapture == handler)
		{
			return;
		}
		if (GUIUtility.hotControl != 0)
		{
			Debug.Log("Should not be capturing when there is a hotcontrol");
			return;
		}
		ReleaseMouseCapture();
		mouseCapture = handler;
		using MouseCaptureEvent evt = MouseCaptureEventBase<MouseCaptureEvent>.GetPooled(mouseCapture);
		UIElementsUtility.eventDispatcher.DispatchEvent(evt, null);
	}

	public static void ReleaseMouseCapture(this IEventHandler handler)
	{
		Debug.Assert(handler == mouseCapture, "Element releasing capture does not have capture");
		if (handler == mouseCapture)
		{
			ReleaseMouseCapture();
		}
	}

	public static void ReleaseMouseCapture()
	{
		if (mouseCapture != null)
		{
			using MouseCaptureOutEvent evt = MouseCaptureEventBase<MouseCaptureOutEvent>.GetPooled(mouseCapture);
			UIElementsUtility.eventDispatcher.DispatchEvent(evt, null);
		}
		mouseCapture = null;
	}
}
