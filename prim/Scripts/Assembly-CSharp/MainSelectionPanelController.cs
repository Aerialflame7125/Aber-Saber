using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainSelectionPanelController : MonoBehaviour
{
	[SerializeField]
	private Toggle _toolsToggle;

	[SerializeField]
	private Toggle _editToggle;

	[SerializeField]
	private Toggle _songParamsToggle;

	[SerializeField]
	private Toggle _projectImageToggle;

	[SerializeField]
	private Toggle _projectParamsToggle;

	[SerializeField]
	private Toggle _filesToggle;

	[Space]
	[SerializeField]
	private GameObject _toolsPanel;

	[SerializeField]
	private GameObject _editPanel;

	[SerializeField]
	private GameObject _songParamsPanel;

	[SerializeField]
	private GameObject _projectImagePanel;

	[SerializeField]
	private GameObject _projectParamsPanel;

	[SerializeField]
	private GameObject _filesPanel;

	private Dictionary<Toggle, GameObject> _togglePanelDict;

	private Dictionary<Toggle, UnityAction<bool>> _toggleOnValueChangedHandlers;

	private void Start()
	{
		_togglePanelDict = new Dictionary<Toggle, GameObject>
		{
			{ _toolsToggle, _toolsPanel },
			{ _editToggle, _editPanel },
			{ _songParamsToggle, _songParamsPanel },
			{ _projectImageToggle, _projectImagePanel },
			{ _projectParamsToggle, _projectParamsPanel },
			{ _filesToggle, _filesPanel }
		};
		_toggleOnValueChangedHandlers = new Dictionary<Toggle, UnityAction<bool>>();
		foreach (Toggle toggle in _togglePanelDict.Keys)
		{
			UnityAction<bool> unityAction = delegate
			{
				ToggleValueChanged(toggle);
			};
			toggle.onValueChanged.AddListener(unityAction);
			_toggleOnValueChangedHandlers[toggle] = unityAction;
		}
		_toolsToggle.isOn = true;
		ActivatePanelWithToggle(_toolsToggle);
	}

	private void OnDestroy()
	{
		foreach (KeyValuePair<Toggle, UnityAction<bool>> toggleOnValueChangedHandler in _toggleOnValueChangedHandlers)
		{
			if (toggleOnValueChangedHandler.Key != null)
			{
				toggleOnValueChangedHandler.Key.onValueChanged.RemoveListener(toggleOnValueChangedHandler.Value);
			}
		}
	}

	private void ActivatePanelWithToggle(Toggle toggle)
	{
		foreach (KeyValuePair<Toggle, GameObject> item in _togglePanelDict)
		{
			if (item.Key == toggle)
			{
				item.Value.SetActive(value: true);
			}
			else
			{
				item.Value.SetActive(value: false);
			}
		}
	}

	public void ToggleValueChanged(Toggle toggle)
	{
		if (toggle.isOn)
		{
			ActivatePanelWithToggle(toggle);
		}
	}
}
