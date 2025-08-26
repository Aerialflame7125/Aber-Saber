using UnityEngine;
using UnityEngine.UI;

public class GameEnergyUIPanel : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(GameEnergyCounter))]
	private ObjectProvider _gameEnergyCounterProvider;

	[SerializeField]
	private Image _energyBar;

	private GameEnergyCounter _gameEnergyCounter;

	private void Start()
	{
		_gameEnergyCounter = _gameEnergyCounterProvider.GetProvidedObject<GameEnergyCounter>();
		_gameEnergyCounter.gameEnergyDidChangeEvent += HandleGameEnergyDidChangeEvent;
		RefreshBarSize(_gameEnergyCounter.energy);
	}

	private void OnDestroy()
	{
		if (_gameEnergyCounter != null)
		{
			_gameEnergyCounter.gameEnergyDidChangeEvent -= HandleGameEnergyDidChangeEvent;
		}
	}

	private void HandleGameEnergyDidChangeEvent(float energy)
	{
		RefreshBarSize(energy);
	}

	private void RefreshBarSize(float energy)
	{
		_energyBar.fillAmount = energy;
	}

	public void EnableEnergyPanel(bool enable)
	{
		base.gameObject.SetActive(enable);
	}
}
