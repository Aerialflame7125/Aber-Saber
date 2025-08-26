using UnityEngine;

public class PauseTriggerSetter : MonoBehaviour
{
	[SerializeField]
	private IPauseTriggerSetableContainer _pauseTriggerSetter;

	[SerializeField]
	private DelayedPauseTrigger _delayedPauseTrigger;

	[SerializeField]
	private MainSettingsModel _mainSettingsModel;

	private void Awake()
	{
		_pauseTriggerSetter.Result.SetPauseTrigger(_delayedPauseTrigger);
		_delayedPauseTrigger.SetLongPressDuration(_mainSettingsModel.pauseButtonPressDuration);
	}
}
