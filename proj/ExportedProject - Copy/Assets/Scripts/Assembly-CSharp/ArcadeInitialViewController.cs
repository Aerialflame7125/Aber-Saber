using System;
using VRUI;

public class ArcadeInitialViewController : VRUIViewController
{
	public event Action didActivateEvent;

	protected override void DidActivate(bool firstActivation, ActivationType activationType)
	{
		if (firstActivation && activationType == ActivationType.AddedToHierarchy && this.didActivateEvent != null)
		{
			this.didActivateEvent();
		}
	}
}
