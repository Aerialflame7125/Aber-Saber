using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace VRUI
{
	[RequireComponent(typeof(RectTransform))]
	public class VRUIViewController : MonoBehaviour
	{
		public enum ActivationType
		{
			AddedToHierarchy = 0,
			NotAddedToHierarchy = 1
		}

		public enum DeactivationType
		{
			RemovedFromHierarchy = 0,
			NotRemovedFromHierarchy = 1
		}

		private VRUINavigationController _navigationController;

		private VRUIViewController _parentViewController;

		private VRUIViewController _childViewController;

		private VRUIScreen _screen;

		private BaseRaycaster _raycaster;

		private RectTransform _rectTransform;

		protected VRUIHierarchyRebuildDataManager _hierarchyRebuildDataManager;

		private bool _wasActivatedBefore;

		public VRUINavigationController navigationController
		{
			get
			{
				return _navigationController;
			}
		}

		public VRUIScreen screen
		{
			get
			{
				return _screen;
			}
		}

		public VRUIViewController parentViewController
		{
			get
			{
				return _parentViewController;
			}
		}

		public VRUIViewController childViewController
		{
			get
			{
				return _childViewController;
			}
		}

		public RectTransform rectTransform
		{
			get
			{
				if (_rectTransform == null)
				{
					_rectTransform = GetComponent<RectTransform>();
				}
				return _rectTransform;
			}
		}

		public bool isInViewControllerHierarchy
		{
			get
			{
				return _screen != null;
			}
		}

		public string hierarchyPathName { get; private set; }

		public bool isRebuildingHierarchy
		{
			get
			{
				return _hierarchyRebuildDataManager.GetData<object>(this) != null;
			}
		}

		private void OnDestroy()
		{
			if (_hierarchyRebuildDataManager != null)
			{
				_hierarchyRebuildDataManager.SetData(this, GetHierarchyRebuildData());
			}
		}

		private string GetHierarchyPathName()
		{
			if ((bool)_navigationController)
			{
				if (_navigationController.parentViewController != null)
				{
					return _navigationController.parentViewController.hierarchyPathName + "/" + _navigationController.GetHierarchyPathNameForChildController(this);
				}
				return _navigationController.GetHierarchyPathNameForChildController(this);
			}
			if (_parentViewController != null)
			{
				return string.Format("{0}/{1}", _parentViewController.hierarchyPathName, base.name);
			}
			return base.name;
		}

		protected virtual void DidActivate(bool firstActivation, ActivationType activationType)
		{
		}

		protected virtual void RebuildHierarchy(object hierarchyRebuildData)
		{
		}

		protected virtual void DidDeactivate(DeactivationType deactivationType)
		{
		}

		protected virtual object GetHierarchyRebuildData()
		{
			return new object();
		}

		protected virtual void LeftAndRightScreenViewControllers(out VRUIViewController leftScreenViewController, out VRUIViewController rightScreenViewController)
		{
			leftScreenViewController = null;
			rightScreenViewController = null;
		}

		internal virtual void SetUserInteraction(bool enabled)
		{
			if (_raycaster == null)
			{
				_raycaster = GetComponent<BaseRaycaster>();
			}
			if (_raycaster != null)
			{
				_raycaster.enabled = enabled;
			}
		}

		internal virtual void Init(VRUIScreen screen, VRUIViewController parentViewController, VRUINavigationController navigationController, VRUIHierarchyRebuildDataManager hierarchyRebuildDataManager)
		{
			_screen = screen;
			_parentViewController = parentViewController;
			_navigationController = navigationController;
			if (_navigationController != null)
			{
				base.transform.SetParent(navigationController.transform, false);
				base.transform.SetSiblingIndex(0);
			}
			else
			{
				base.transform.SetParent(screen.transform, false);
			}
			_hierarchyRebuildDataManager = hierarchyRebuildDataManager;
			hierarchyPathName = GetHierarchyPathName();
		}

		internal virtual void ResetViewController()
		{
			base.transform.SetParent(_screen.transform, false);
			_screen = null;
			_parentViewController = null;
			_navigationController = null;
			_childViewController = null;
			if (_hierarchyRebuildDataManager != null)
			{
				_hierarchyRebuildDataManager.DeleteData(this);
			}
			_hierarchyRebuildDataManager = null;
			hierarchyPathName = null;
		}

		public void PresentModalViewController(VRUIViewController viewController, Action finishedCallback, bool immediately = false)
		{
			if (_childViewController != null)
			{
				throw new InvalidOperationException("Can not present new view controller. This view controller is already presenting one.");
			}
			if (viewController == null)
			{
				throw new InvalidOperationException("Can not present null view controller.");
			}
			StartCoroutine(PresentModalViewControllerCoroutine(viewController, finishedCallback, immediately));
		}

		private IEnumerator PresentModalViewControllerCoroutine(VRUIViewController newViewController, Action finishedCallback, bool immediately)
		{
			EventSystem eventSystem = EventSystem.current;
			eventSystem.SetSelectedGameObject(null);
			SetUserInteraction(false);
			float transitionDuration = 0.4f;
			float elapsedTime = 0f;
			Deactivate(DeactivationType.NotRemovedFromHierarchy, false);
			float moveOffset = 20f / base.transform.lossyScale.x;
			if (!newViewController.gameObject.activeSelf)
			{
				newViewController.gameObject.SetActive(true);
			}
			_childViewController = newViewController;
			newViewController.Init(_screen, this, null, _hierarchyRebuildDataManager);
			if (immediately)
			{
				base.gameObject.SetActive(false);
				newViewController.SetUserInteraction(true);
				newViewController.transform.localPosition = Vector3.zero;
				base.transform.localPosition = new Vector3(0f - moveOffset, 0f, 0f);
			}
			newViewController.Activate(ActivationType.AddedToHierarchy);
			if (!immediately)
			{
				newViewController.SetUserInteraction(false);
				while (elapsedTime < transitionDuration)
				{
					float t = elapsedTime / transitionDuration;
					newViewController.transform.localPosition = Vector3.Lerp(new Vector3(moveOffset, 0f, 0f), Vector3.zero, 1f - Mathf.Pow(1f - t, 3f));
					base.transform.localPosition = Vector3.Lerp(Vector3.zero, new Vector3(0f - moveOffset, 0f, 0f), Mathf.Pow(t, 3f));
					elapsedTime += Time.deltaTime;
					yield return null;
				}
				base.gameObject.SetActive(false);
				newViewController.SetUserInteraction(true);
				newViewController.transform.localPosition = Vector3.zero;
				base.transform.localPosition = new Vector3(0f - moveOffset, 0f, 0f);
			}
			if (finishedCallback != null)
			{
				finishedCallback();
			}
		}

		public void ReplaceViewController(VRUIViewController viewController, Action finishedCallback, bool immediately = false)
		{
			if (_parentViewController == null)
			{
				throw new InvalidOperationException("Can not replace view controller. Only view controller with parent view controllers can be replaced");
			}
			if (_childViewController != null)
			{
				throw new InvalidOperationException("Can not replace view controller. This view controller is already presenting another view controller.");
			}
			if (viewController == null)
			{
				throw new InvalidOperationException("Can not present null view controller.");
			}
			StartCoroutine(ReplaceViewControllerCoroutine(viewController, finishedCallback, immediately));
		}

		private IEnumerator ReplaceViewControllerCoroutine(VRUIViewController newViewController, Action finishedCallback, bool immediately)
		{
			EventSystem eventSystem = EventSystem.current;
			eventSystem.SetSelectedGameObject(null);
			SetUserInteraction(false);
			float transitionDuration = 0.4f;
			float elapsedTime = 0f;
			Deactivate(DeactivationType.RemovedFromHierarchy, false);
			if (!newViewController.gameObject.activeSelf)
			{
				newViewController.gameObject.SetActive(true);
			}
			parentViewController._childViewController = newViewController;
			newViewController.Init(_screen, parentViewController, null, _hierarchyRebuildDataManager);
			newViewController.Activate(ActivationType.AddedToHierarchy);
			newViewController.SetUserInteraction(false);
			float moveOffset = 10f / base.transform.lossyScale.y;
			if (!immediately)
			{
				while (elapsedTime < transitionDuration)
				{
					float t = elapsedTime / transitionDuration;
					newViewController.transform.localPosition = Vector3.Lerp(new Vector3(0f, moveOffset, 0f), Vector3.zero, 1f - Mathf.Pow(1f - t, 3f));
					base.transform.localPosition = Vector3.Lerp(Vector3.zero, new Vector3(0f, 0f - moveOffset, 0f), Mathf.Pow(t, 3f));
					elapsedTime += Time.deltaTime;
					yield return null;
				}
			}
			newViewController.transform.localPosition = Vector3.zero;
			base.transform.localPosition = new Vector3(0f - moveOffset, 0f, 0f);
			base.gameObject.SetActive(false);
			newViewController.SetUserInteraction(true);
			if (finishedCallback != null)
			{
				finishedCallback();
			}
		}

		public void DismissModalViewController(Action finishedCallback, bool immediately = false)
		{
			if (_parentViewController == null)
			{
				throw new InvalidOperationException("This view controller can not be dismissed, because it does not have any parent.");
			}
			StartCoroutine(DismissModalViewControllerCoroutine(finishedCallback, immediately));
		}

		private IEnumerator DismissModalViewControllerCoroutine(Action finishedCallback, bool immediately)
		{
			EventSystem.current.SetSelectedGameObject(null);
			VRUIViewController movingInViewController = _parentViewController;
			movingInViewController.SetUserInteraction(false);
			Transform movingOutObjectTransform = base.transform;
			SetUserInteraction(false);
			Deactivate(DeactivationType.RemovedFromHierarchy, false);
			movingInViewController._childViewController = null;
			movingInViewController.Activate(ActivationType.NotAddedToHierarchy);
			Transform movingInObjectTransform = movingInViewController.transform;
			float moveOffset = 20f / base.transform.lossyScale.x;
			if (!immediately)
			{
				float transitionDuration = 0.4f;
				float elapsedTime = 0f;
				while (elapsedTime < transitionDuration)
				{
					float t = elapsedTime / transitionDuration;
					movingInObjectTransform.localPosition = Vector3.Lerp(new Vector3(0f - moveOffset, 0f, 0f), Vector3.zero, 1f - Mathf.Pow(1f - t, 3f));
					movingOutObjectTransform.localPosition = Vector3.Lerp(Vector3.zero, new Vector3(moveOffset, 0f, 0f), Mathf.Pow(t, 3f));
					elapsedTime += Time.deltaTime;
					yield return null;
				}
			}
			movingInObjectTransform.localPosition = Vector3.zero;
			movingOutObjectTransform.localPosition = new Vector3(moveOffset, 0f, 0f);
			DeactivateGameObject();
			ResetViewController();
			movingInViewController.SetUserInteraction(true);
			if (finishedCallback != null)
			{
				finishedCallback();
			}
		}

		internal virtual void Activate(ActivationType activationType)
		{
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
			}
			object obj = null;
			if (activationType == ActivationType.AddedToHierarchy && _hierarchyRebuildDataManager != null)
			{
				obj = _hierarchyRebuildDataManager.GetData<object>(this);
			}
			if ((bool)_hierarchyRebuildDataManager)
			{
				VRUIViewController leftScreenViewController = null;
				VRUIViewController rightScreenViewController = null;
				LeftAndRightScreenViewControllers(out leftScreenViewController, out rightScreenViewController);
				_screen.screenSystem.leftScreen.SetRootViewController(leftScreenViewController);
				_screen.screenSystem.rightScreen.SetRootViewController(rightScreenViewController);
			}
			DidActivate(!_wasActivatedBefore, activationType);
			if (!_wasActivatedBefore)
			{
				_wasActivatedBefore = true;
			}
			if (obj != null)
			{
				RebuildHierarchy(obj);
			}
		}

		internal virtual void Deactivate(DeactivationType deactivationType, bool deactivateGameObject)
		{
			if (base.gameObject.activeSelf && deactivateGameObject)
			{
				base.gameObject.SetActive(false);
			}
			DidDeactivate(deactivationType);
		}

		internal virtual void DeactivateGameObject()
		{
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
		}
	}
}
