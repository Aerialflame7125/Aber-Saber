using System;
using System.IO;
using UnityEngine;
using VRUI;

public class CustomLevelImportFlowCoordinator : FlowCoordinator
{
	[SerializeField]
	private CustomLevelImportViewController _customLevelImportViewController;

	[SerializeField]
	private FileBrowserViewController _fileBrowserViewController;

	[SerializeField]
	private FileBrowserViewController _fileBrowserBookmarsViewController;

	[SerializeField]
	private CustomLevelImportDetailViewController _customLevelImportDetailViewController;

	[SerializeField]
	private CustomLevelImportNavigationController _customLevelImportNavigationController;

	[SerializeField]
	private MainSettingsModel _mainSettingsModel;

	[SerializeField]
	private BookmarksFoldersModel bookmarksFoldersModel;

	private FileBrowserModel _fileBrowserModel;

	protected bool _initialized;

	public event Action<CustomLevelImportFlowCoordinator, string> didImportEvent;

	public event Action didFinishEvent;

	public void Present(VRUIViewController parentViewController)
	{
		if (!_initialized)
		{
			didFinishEvent += Finish;
			_customLevelImportViewController.didFinishEvent += HandleCustomLevelImportViewControllerDidFinish;
			_customLevelImportNavigationController.didFinishEvent += HandleCustomLevelImportNavigationControllerDidFinish;
			FileBrowserViewController fileBrowserViewController = _fileBrowserViewController;
			fileBrowserViewController.didSelectRowEvent = (Action<FileBrowserViewController, FileBrowserItem>)Delegate.Combine(fileBrowserViewController.didSelectRowEvent, new Action<FileBrowserViewController, FileBrowserItem>(FileBrowserViewControllerDidSelectRow));
			FileBrowserViewController fileBrowserBookmarsViewController = _fileBrowserBookmarsViewController;
			fileBrowserBookmarsViewController.didSelectRowEvent = (Action<FileBrowserViewController, FileBrowserItem>)Delegate.Combine(fileBrowserBookmarsViewController.didSelectRowEvent, new Action<FileBrowserViewController, FileBrowserItem>(FileBrowserBookmarsViewControllerDidSelectRow));
			CustomLevelImportDetailViewController customLevelImportDetailViewController = _customLevelImportDetailViewController;
			customLevelImportDetailViewController.didSelectCustomLevelButtonPressEvent = (Action<CustomLevelImportDetailViewController, CustomLevelFileBrowserModel.CustomLevelFileBrowserItem>)Delegate.Combine(customLevelImportDetailViewController.didSelectCustomLevelButtonPressEvent, new Action<CustomLevelImportDetailViewController, CustomLevelFileBrowserModel.CustomLevelFileBrowserItem>(HandleCustomLevelImportDetailViewControllerDidSelectCustomLevel));
			CustomLevelImportDetailViewController customLevelImportDetailViewController2 = _customLevelImportDetailViewController;
			customLevelImportDetailViewController2.didSelectCustomMusicButtonPressEvent = (Action<CustomLevelImportDetailViewController, CustomMusicFileBrowserModel.MusicFileBrowserItem>)Delegate.Combine(customLevelImportDetailViewController2.didSelectCustomMusicButtonPressEvent, new Action<CustomLevelImportDetailViewController, CustomMusicFileBrowserModel.MusicFileBrowserItem>(HandleCustomLevelImportDetailViewControllerDidSelectCustomMusic));
			parentViewController.PresentModalViewController(_customLevelImportViewController, null);
			_initialized = true;
		}
	}

	private void Finish()
	{
		if (_initialized)
		{
			didFinishEvent -= Finish;
			_customLevelImportViewController.didFinishEvent -= HandleCustomLevelImportViewControllerDidFinish;
			_customLevelImportNavigationController.didFinishEvent -= HandleCustomLevelImportNavigationControllerDidFinish;
			FileBrowserViewController fileBrowserViewController = _fileBrowserViewController;
			fileBrowserViewController.didSelectRowEvent = (Action<FileBrowserViewController, FileBrowserItem>)Delegate.Remove(fileBrowserViewController.didSelectRowEvent, new Action<FileBrowserViewController, FileBrowserItem>(FileBrowserViewControllerDidSelectRow));
			FileBrowserViewController fileBrowserBookmarsViewController = _fileBrowserBookmarsViewController;
			fileBrowserBookmarsViewController.didSelectRowEvent = (Action<FileBrowserViewController, FileBrowserItem>)Delegate.Remove(fileBrowserBookmarsViewController.didSelectRowEvent, new Action<FileBrowserViewController, FileBrowserItem>(FileBrowserBookmarsViewControllerDidSelectRow));
			CustomLevelImportDetailViewController customLevelImportDetailViewController = _customLevelImportDetailViewController;
			customLevelImportDetailViewController.didSelectCustomLevelButtonPressEvent = (Action<CustomLevelImportDetailViewController, CustomLevelFileBrowserModel.CustomLevelFileBrowserItem>)Delegate.Remove(customLevelImportDetailViewController.didSelectCustomLevelButtonPressEvent, new Action<CustomLevelImportDetailViewController, CustomLevelFileBrowserModel.CustomLevelFileBrowserItem>(HandleCustomLevelImportDetailViewControllerDidSelectCustomLevel));
			CustomLevelImportDetailViewController customLevelImportDetailViewController2 = _customLevelImportDetailViewController;
			customLevelImportDetailViewController2.didSelectCustomMusicButtonPressEvent = (Action<CustomLevelImportDetailViewController, CustomMusicFileBrowserModel.MusicFileBrowserItem>)Delegate.Remove(customLevelImportDetailViewController2.didSelectCustomMusicButtonPressEvent, new Action<CustomLevelImportDetailViewController, CustomMusicFileBrowserModel.MusicFileBrowserItem>(HandleCustomLevelImportDetailViewControllerDidSelectCustomMusic));
			_initialized = false;
		}
	}

	private void HandleCustomLevelImportViewControllerDidFinish(CustomLevelImportViewController viewController, CustomLevelImportViewController.SelectedButton selectedButton)
	{
		switch (selectedButton)
		{
		case CustomLevelImportViewController.SelectedButton.Cancel:
			viewController.DismissModalViewController(null);
			if (this.didFinishEvent != null)
			{
				this.didFinishEvent();
			}
			break;
		case CustomLevelImportViewController.SelectedButton.SelectLevelFile:
			PresentSelectLevelFileController(viewController);
			break;
		case CustomLevelImportViewController.SelectedButton.SelectMusicFile:
			PresentSelectMusicFileController(viewController);
			break;
		case CustomLevelImportViewController.SelectedButton.Import:
			viewController.DismissModalViewController(null);
			if (this.didImportEvent != null)
			{
				this.didImportEvent(this, _customLevelImportViewController.selectedLevelID);
			}
			if (this.didFinishEvent != null)
			{
				this.didFinishEvent();
			}
			break;
		}
	}

	private void HandleCustomLevelImportNavigationControllerDidFinish(CustomLevelImportNavigationController viewController)
	{
		viewController.DismissModalViewController(null);
	}

	private void PresentSelectLevelFileController(VRUIViewController presentingController)
	{
		_fileBrowserModel = CustomLevelFileBrowserModel.instance;
		presentingController.PresentModalViewController(_customLevelImportNavigationController, null, presentingController.isRebuildingHierarchy);
		_fileBrowserBookmarsViewController.Init(bookmarksFoldersModel.bookmarksFolders, shouldSelectFirstItem: true);
		_customLevelImportNavigationController.PushViewController(_fileBrowserBookmarsViewController, immediately: true);
	}

	private void PresentSelectMusicFileController(VRUIViewController presentingController)
	{
		_fileBrowserModel = CustomMusicFileBrowserModel.instance;
		presentingController.PresentModalViewController(_customLevelImportNavigationController, null, presentingController.isRebuildingHierarchy);
		_fileBrowserBookmarsViewController.Init(bookmarksFoldersModel.bookmarksFolders, shouldSelectFirstItem: true);
		_customLevelImportNavigationController.PushViewController(_fileBrowserBookmarsViewController, immediately: true);
	}

	public void FileBrowserBookmarsViewControllerDidSelectRow(FileBrowserViewController fileBrowserViewController, FileBrowserItem fileBrowserItem)
	{
		FileBrowserItemWasSelected(fileBrowserItem);
	}

	public void FileBrowserViewControllerDidSelectRow(FileBrowserViewController fileBrowserViewController, FileBrowserItem fileBrowserItem)
	{
		FileBrowserItemWasSelected(fileBrowserItem);
	}

	private void FileBrowserItemWasSelected(FileBrowserItem fileBrowserItem)
	{
		if (DirectoryBrowserItem.IsItemMember(fileBrowserItem))
		{
			ShowDirectory(fileBrowserItem);
		}
		else
		{
			ShowDetail(fileBrowserItem);
		}
	}

	private void ShowDetail(FileBrowserItem browserItem)
	{
		if (!_customLevelImportDetailViewController.isActiveAndEnabled)
		{
			_customLevelImportDetailViewController.Init(browserItem);
			_customLevelImportNavigationController.PushViewController(_customLevelImportDetailViewController, _customLevelImportNavigationController.isRebuildingHierarchy);
			Debug.Log("showing Init");
		}
		else
		{
			Debug.Log("showing SetFileDetail");
			_customLevelImportDetailViewController.SetFileDetail(browserItem);
		}
	}

	public void HandleCustomLevelImportDetailViewControllerDidSelectCustomLevel(CustomLevelImportDetailViewController customLevelImportDetailViewController, CustomLevelFileBrowserModel.CustomLevelFileBrowserItem browserItem)
	{
		_customLevelImportNavigationController.ClearChildControllers();
		_customLevelImportNavigationController.DismissModalViewController(null);
		_customLevelImportViewController.SetCustomLevelFileBrowserItem(browserItem);
		_mainSettingsModel.lastImportedFile = Path.GetFullPath(browserItem.fullPath + "/..");
		_mainSettingsModel.Save();
	}

	public void HandleCustomLevelImportDetailViewControllerDidSelectCustomMusic(CustomLevelImportDetailViewController customLevelImportDetailViewController, CustomMusicFileBrowserModel.MusicFileBrowserItem browserItem)
	{
		_customLevelImportNavigationController.ClearChildControllers();
		_customLevelImportNavigationController.DismissModalViewController(null);
		_customLevelImportViewController.SetMusicBrowserItem(browserItem);
	}

	private void ShowDirectory(FileBrowserItem fileBrowserItem)
	{
		string dirPath = fileBrowserItem.fullPath;
		_customLevelImportNavigationController.loadingIndicator.ShowLoading();
		Action<FileBrowserItem[]> callback = delegate(FileBrowserItem[] items)
		{
			if (!_fileBrowserViewController.isInViewControllerHierarchy)
			{
				_fileBrowserViewController.Init(items);
				_customLevelImportNavigationController.PushViewController(_fileBrowserViewController, _customLevelImportNavigationController.isRebuildingHierarchy);
			}
			else
			{
				_fileBrowserViewController.SetFileBrowserItems(items);
			}
			_customLevelImportNavigationController.SetPath(dirPath);
			Debug.Log("showing detail");
			ShowDetail(fileBrowserItem);
			_customLevelImportNavigationController.loadingIndicator.HideLoading();
		};
		_fileBrowserModel.GetContentOfDirectory(dirPath, callback);
	}
}
