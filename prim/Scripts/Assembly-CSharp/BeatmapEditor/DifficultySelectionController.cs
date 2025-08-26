using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor;

public class DifficultySelectionController : MonoBehaviour
{
	[SerializeField]
	private ActiveDifficultySO _activeDifficulty;

	[SerializeField]
	private Dropdown _difficultyDropdown;

	private LevelDifficulty[] _difficulties;

	private void Start()
	{
		_difficultyDropdown.onValueChanged.AddListener(HandleDifficultyDropdownValueChanged);
		_activeDifficulty.didChangeEvent += HandleActiveDifficultyDidChange;
		List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();
		_difficulties = (LevelDifficulty[])Enum.GetValues(typeof(LevelDifficulty));
		LevelDifficulty[] difficulties = _difficulties;
		foreach (LevelDifficulty difficulty in difficulties)
		{
			list.Add(new Dropdown.OptionData(difficulty.Name()));
		}
		_difficultyDropdown.AddOptions(list);
		RefreshUI();
	}

	private void OnDestroy()
	{
		if (_difficultyDropdown != null)
		{
			_difficultyDropdown.onValueChanged.RemoveListener(HandleDifficultyDropdownValueChanged);
		}
		if (_activeDifficulty != null)
		{
			_activeDifficulty.didChangeEvent += HandleActiveDifficultyDidChange;
		}
	}

	private void RefreshUI()
	{
		for (int i = 0; i < _difficulties.Length; i++)
		{
			if (_difficulties[i] == _activeDifficulty.difficulty)
			{
				_difficultyDropdown.value = i;
			}
		}
	}

	private void HandleActiveDifficultyDidChange(LevelDifficulty prevDifficulty, LevelDifficulty currentDifficulty)
	{
		RefreshUI();
	}

	private void HandleDifficultyDropdownValueChanged(int value)
	{
		_activeDifficulty.difficulty = _difficulties[value];
	}
}
