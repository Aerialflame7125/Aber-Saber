using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor;

public class BasicLevelInfoController : MonoBehaviour
{
	[SerializeField]
	private ObservableStringSO _songName;

	[SerializeField]
	private ObservableStringSO _songSubName;

	[SerializeField]
	private ObservableStringSO _songAuthor;

	[SerializeField]
	private ObservableStringSO _beatmapAuthor;

	[SerializeField]
	private InputField _songNameInputField;

	[SerializeField]
	private InputField _songSubNameInputField;

	[SerializeField]
	private InputField _songAuthorInputField;

	[SerializeField]
	private InputField _beatmapAuthorInputField;

	private InputFieldDataBinder _binder;

	private void Awake()
	{
		_binder = new InputFieldDataBinder();
		_binder.AddStringBindings(new List<Tuple<InputField, ObservableStringSO>>
		{
			{ _songNameInputField, _songName },
			{ _songSubNameInputField, _songSubName },
			{ _songAuthorInputField, _songAuthor },
			{ _beatmapAuthorInputField, _beatmapAuthor }
		});
	}

	private void OnDestroy()
	{
		if (_binder != null)
		{
			_binder.ClearBindings();
		}
	}
}
