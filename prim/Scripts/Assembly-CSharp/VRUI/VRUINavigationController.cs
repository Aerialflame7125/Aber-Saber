using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace VRUI;

public class VRUINavigationController : VRUIViewController
{
	protected List<VRUIViewController> _viewControllers = new List<VRUIViewController>();

	public string GetHierarchyPathNameForChildController(VRUIViewController viewController)
	{
		StringBuilder stringBuilder = new StringBuilder(100);
		stringBuilder.AppendFormat("{0}", base.name);
		for (int i = 0; i < _viewControllers.Count; i++)
		{
			stringBuilder.AppendFormat("/{0}", _viewControllers[i].name);
			if (_viewControllers[i] == viewController)
			{
				return stringBuilder.ToString();
			}
		}
		return null;
	}

	public void ClearChildControllers()
	{
		int count = _viewControllers.Count;
		for (int i = 0; i < count; i++)
		{
			_viewControllers[i].Deactivate(DeactivationType.RemovedFromHierarchy, deactivateGameObject: true);
			_viewControllers[i].ResetViewController();
		}
		_viewControllers.Clear();
	}

	internal override void SetUserInteraction(bool enabled)
	{
		base.SetUserInteraction(enabled);
		int count = _viewControllers.Count;
		for (int i = 0; i < count; i++)
		{
			_viewControllers[i].SetUserInteraction(enabled);
		}
	}

	internal override void Init(VRUIScreen screen, VRUIViewController parentViewController, VRUINavigationController navigationController, VRUIHierarchyRebuildDataManager hierarchyRebuildDataManager)
	{
		base.Init(screen, parentViewController, navigationController, hierarchyRebuildDataManager);
		int count = _viewControllers.Count;
		for (int i = 0; i < count; i++)
		{
			_viewControllers[i].Init(screen, parentViewController, this, hierarchyRebuildDataManager);
		}
	}

	internal override void ResetViewController()
	{
		base.ResetViewController();
		int count = _viewControllers.Count;
		for (int i = 0; i < count; i++)
		{
			_viewControllers[i].ResetViewController();
		}
		_viewControllers.Clear();
	}

	internal override void Activate(ActivationType activationType)
	{
		base.Activate(activationType);
		int count = _viewControllers.Count;
		for (int i = 0; i < count; i++)
		{
			_viewControllers[i].Activate(activationType);
		}
		LayoutViewControllers(_viewControllers);
	}

	internal override void Deactivate(DeactivationType deactivationType, bool deactivateGameObject)
	{
		base.Deactivate(deactivationType, deactivateGameObject);
		int count = _viewControllers.Count;
		for (int i = 0; i < count; i++)
		{
			_viewControllers[i].Deactivate(deactivationType, deactivateGameObject);
		}
	}

	internal override void DeactivateGameObject()
	{
		base.DeactivateGameObject();
		int count = _viewControllers.Count;
		for (int i = 0; i < count; i++)
		{
			if (_viewControllers[i].gameObject.activeSelf)
			{
				_viewControllers[i].gameObject.SetActive(value: false);
			}
		}
	}

	public virtual void PushViewController(VRUIViewController viewController, bool immediately = false)
	{
		if (viewController.GetType().IsSubclassOf(typeof(VRUINavigationController)))
		{
			throw new InvalidOperationException("Can not push navigation controller into navigation controller hierarchy.");
		}
		if (_viewControllers.IndexOf(viewController) >= 0)
		{
			throw new InvalidOperationException("Can not push the same view controller into navigation controller hierarchy more than once.");
		}
		StopAllCoroutines();
		StartCoroutine(PushViewControllerCoroutine(viewController, immediately));
	}

	public virtual void PopViewControllerImmediately()
	{
		VRUIViewController vRUIViewController = _viewControllers[_viewControllers.Count - 1];
		vRUIViewController.ResetViewController();
		vRUIViewController.Deactivate(DeactivationType.RemovedFromHierarchy, deactivateGameObject: true);
		_viewControllers.Remove(vRUIViewController);
	}

	private IEnumerator PushViewControllerCoroutine(VRUIViewController newViewController, bool immediately)
	{
		newViewController.SetUserInteraction(enabled: false);
		_viewControllers.Add(newViewController);
		newViewController.Init(base.screen, base.parentViewController, this, _hierarchyRebuildDataManager);
		float moveOffset = 4f / base.transform.lossyScale.x;
		newViewController.transform.localPosition = new Vector3(moveOffset, 0f, 0f);
		newViewController.Activate(ActivationType.AddedToHierarchy);
		float[] positions = GetNewXPositionsForViewControllers(_viewControllers);
		int numberOfViewControllers = _viewControllers.Count;
		if (!immediately)
		{
			float transitionDuration = 0.4f;
			float elapsedTime = 0f;
			float[] startXPositions = new float[numberOfViewControllers];
			for (int i = 0; i < numberOfViewControllers; i++)
			{
				startXPositions[i] = _viewControllers[i].transform.localPosition.x;
			}
			while (elapsedTime < transitionDuration)
			{
				float t = elapsedTime / transitionDuration;
				for (int j = 0; j < numberOfViewControllers; j++)
				{
					float x = Mathf.Lerp(startXPositions[j], positions[j], 1f - Mathf.Pow(1f - t, 3f));
					_viewControllers[j].transform.localPosition = new Vector3(x, 0f, 0f);
				}
				elapsedTime += Time.deltaTime;
				yield return null;
			}
		}
		for (int k = 0; k < numberOfViewControllers; k++)
		{
			_viewControllers[k].transform.localPosition = new Vector3(positions[k], 0f, 0f);
		}
		newViewController.SetUserInteraction(enabled: true);
	}

	private void LayoutViewControllers(List<VRUIViewController> viewControllers)
	{
		float[] newXPositionsForViewControllers = GetNewXPositionsForViewControllers(viewControllers);
		int count = viewControllers.Count;
		for (int i = 0; i < count; i++)
		{
			viewControllers[i].transform.localPosition = new Vector3(newXPositionsForViewControllers[i], 0f, 0f);
		}
	}

	private float[] GetNewXPositionsForViewControllers(List<VRUIViewController> viewControllers)
	{
		int count = viewControllers.Count;
		float[] array = new float[count];
		float num = 0f;
		for (int i = 0; i < count; i++)
		{
			RectTransform component = viewControllers[i].GetComponent<RectTransform>();
			num += component.rect.width * component.localScale.x;
		}
		float num2 = 0.05f;
		float num3 = (0f - (num + num2 * (float)(count - 1))) * 0.5f;
		for (int j = 0; j < count; j++)
		{
			RectTransform component2 = viewControllers[j].GetComponent<RectTransform>();
			array[j] = num3 + component2.rect.width * component2.localScale.x * 0.5f;
			num3 += component2.rect.width * component2.localScale.x + num2;
		}
		return array;
	}
}
