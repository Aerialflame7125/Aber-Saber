using System;
using UnityEngine;
using VRUI;
using SongLoader;

public class MainFlowCoordinator : FlowCoordinator
{
	[SerializeField]
	private MenuSceneSetupData _menuSceneSetupData;

	[Space]
	[SerializeField]
	private VRUIScreenSystem _screenSystem;

	[Space]
	[SerializeField]
	private MainMenuViewController _mainMenuViewController;

	[SerializeField]
	private SoloModeSelectionViewController _soloModeSelectionViewController;

	[SerializeField]
	private SettingsViewController _settingsViewController;

	[Space]
	[SerializeField]
	private StandardLevelSelectionFlowCoordinator _levelSelectionFlowCoordinator;

	[Space]
	[SerializeField]
	private LevelCollectionsForGameplayModes _levelCollectionsForGameplayModes;
	[SerializeField]
	private SongSearchViewController _songSearchViewController;

	[SerializeField]
	private GamblingViewController _gamblingViewController;

	private bool _initialized;

	public void Present()
	{
		if (!_initialized)
		{
			_mainMenuViewController.didFinishEvent += HandleMainMenuViewControllerDidFinish;
			SoloModeSelectionViewController soloModeSelectionViewController = _soloModeSelectionViewController;
			soloModeSelectionViewController.didFinishEvent = (Action<SoloModeSelectionViewController, SoloModeSelectionViewController.SubMenuType>)Delegate.Combine(soloModeSelectionViewController.didFinishEvent, new Action<SoloModeSelectionViewController, SoloModeSelectionViewController.SubMenuType>(HandleSoloModeSelectionViewControllerDidFinish));
			_settingsViewController.didFinishEvent += HandleSettingsViewControllerDidFinish;
			_initialized = true;
		}
		_screenSystem.mainScreen.SetRootViewController(_mainMenuViewController);
	}

	private void HandleMainMenuViewControllerDidFinish(MainMenuViewController viewController, MainMenuViewController.MenuButton subMenuType)
	{
		switch (subMenuType)
		{
			case MainMenuViewController.MenuButton.Solo:
				viewController.PresentModalViewController(_soloModeSelectionViewController, null, viewController.isRebuildingHierarchy);
				break;
			case MainMenuViewController.MenuButton.Party:
				{
					GameplayMode gameplayMode = GameplayMode.PartyStandard;
					_levelSelectionFlowCoordinator.Present(viewController, _levelCollectionsForGameplayModes.GetLevels(gameplayMode), gameplayMode);
					break;
				}
			case MainMenuViewController.MenuButton.Settings:
				viewController.PresentModalViewController(_settingsViewController, null, viewController.isRebuildingHierarchy);
				break;
			case MainMenuViewController.MenuButton.Tutorial:
				if (!viewController.isRebuildingHierarchy)
				{
					_menuSceneSetupData.StartTutorial();
				}
				break;
			case MainMenuViewController.MenuButton.Quit:
				Application.Quit();
				break;
			case MainMenuViewController.MenuButton.Gambling:
				viewController.PresentModalViewController(_gamblingViewController, null, viewController.isRebuildingHierarchy);
				break;
			case MainMenuViewController.MenuButton.SongSearch:
				viewController.PresentModalViewController(_songSearchViewController, null, viewController.isRebuildingHierarchy);
				break;
			case MainMenuViewController.MenuButton.Editor:
				break;
		}
	}

	private void HandleSoloModeSelectionViewControllerDidFinish(SoloModeSelectionViewController viewController, SoloModeSelectionViewController.SubMenuType subMenuType)
	{
		switch (subMenuType)
		{
			case SoloModeSelectionViewController.SubMenuType.Back:
				viewController.DismissModalViewController(null);
				break;
			case SoloModeSelectionViewController.SubMenuType.FreePlayMode:
				{
					GameplayMode gameplayMode3 = GameplayMode.SoloStandard;
					_levelSelectionFlowCoordinator.Present(viewController, _levelCollectionsForGameplayModes.GetLevels(gameplayMode3), gameplayMode3);
					break;
				}
			case SoloModeSelectionViewController.SubMenuType.NoArrowsMode:
				{
					GameplayMode gameplayMode2 = GameplayMode.SoloNoArrows;
					_levelSelectionFlowCoordinator.Present(viewController, _levelCollectionsForGameplayModes.GetLevels(gameplayMode2), gameplayMode2);
					break;
				}
			case SoloModeSelectionViewController.SubMenuType.OneSaberMode:
				{
					GameplayMode gameplayMode = GameplayMode.SoloOneSaber;
					_levelSelectionFlowCoordinator.Present(viewController, _levelCollectionsForGameplayModes.GetLevels(gameplayMode), gameplayMode);
					break;
				}
		}
	}

	private void HandleSettingsViewControllerDidFinish(SettingsViewController viewController, SettingsViewController.FinishAction finishAction)
	{
		if (viewController.isRebuildingHierarchy)
		{
			switch (finishAction)
			{
				case SettingsViewController.FinishAction.Ok:
					viewController.DismissModalViewController(null, true);
					break;
			}
			return;
		}
		switch (finishAction)
		{
			case SettingsViewController.FinishAction.Ok:
				_menuSceneSetupData.Restart();
				break;
			case SettingsViewController.FinishAction.Apply:
				_menuSceneSetupData.Restart();
				break;
			case SettingsViewController.FinishAction.Cancel:
				viewController.DismissModalViewController(null);
				break;
		}
	}

	public void ReturnToMainMenu(MainMenuViewController _mainMenuViewController)
	{
		_mainMenuViewController.DismissModalViewController(null);
	}

}
