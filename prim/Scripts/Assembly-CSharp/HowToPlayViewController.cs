using System;
using UnityEngine;
using VRUI;

public class HowToPlayViewController : VRUIViewController
{
	[SerializeField]
	private GameObject _tutorialButton;

	public event Action didPressTutorialButtonEvent;

	public void Init(bool showTutorialButton)
	{
		_tutorialButton.SetActive(showTutorialButton);
	}

	protected override void DidActivate(bool firstActivation, ActivationType activationType)
	{
	}

	public void TutorialButtonPressed()
	{
		if (this.didPressTutorialButtonEvent != null)
		{
			this.didPressTutorialButtonEvent();
		}
	}
}
