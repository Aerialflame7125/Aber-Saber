using System;
using VRUI;

public class DismissableNavigationController : VRUINavigationController
{
	public event Action<DismissableNavigationController> navigationControllerDidFinishEvent;

	public void DismissButtonWasPressed()
	{
		if (this.navigationControllerDidFinishEvent != null)
		{
			this.navigationControllerDidFinishEvent(this);
		}
	}
}
