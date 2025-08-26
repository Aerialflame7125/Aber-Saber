using System;

public class TutorialSceneSetupData : GameSceneSetupData
{
	public event Action<TutorialSceneSetupData, bool> didFinishEvent;

	public void Finish(bool completed)
	{
		if (this.didFinishEvent != null)
		{
			this.didFinishEvent(this, completed);
		}
	}
}
