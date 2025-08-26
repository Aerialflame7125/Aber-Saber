using System;

public interface IPauseTrigger
{
	void EnableTrigger(bool enable);

	void SetCallback(Action pauseWasTriggeredCallback);
}
