using UnityEngine;

namespace VRUI;

[RequireComponent(typeof(RectTransform))]
public class VRUIScreen : MonoBehaviour
{
	[SerializeField]
	private bool _useHierarchyRebuildSystem;

	[SerializeField]
	private VRUIHierarchyRebuildDataManager _hierarchyRebuildDataManager;

	private VRUIViewController _rootViewController;

	public VRUIScreenSystem screenSystem { get; set; }

	public VRUIViewController rootViewController => _rootViewController;

	public void SetRootViewController(VRUIViewController rootViewController)
	{
		if (rootViewController == _rootViewController)
		{
			return;
		}
		if (_rootViewController != null)
		{
			_rootViewController.Deactivate(VRUIViewController.DeactivationType.RemovedFromHierarchy, deactivateGameObject: true);
			_rootViewController.ResetViewController();
		}
		_rootViewController = rootViewController;
		if (_rootViewController != null)
		{
			Vector3 localScale = _rootViewController.transform.localScale;
			if (_rootViewController.transform.parent != base.transform)
			{
				_rootViewController.transform.SetParent(base.transform, worldPositionStays: false);
			}
			_rootViewController.transform.localScale = localScale;
			_rootViewController.transform.localPosition = Vector3.zero;
			Vector3 position = base.transform.position;
			_rootViewController.transform.rotation = Quaternion.LookRotation(base.transform.forward, Vector3.up);
			if (!_rootViewController.gameObject.activeSelf)
			{
				_rootViewController.gameObject.SetActive(value: true);
			}
			_rootViewController.Init(this, null, null, (!_useHierarchyRebuildSystem) ? null : _hierarchyRebuildDataManager);
			_rootViewController.Activate(VRUIViewController.ActivationType.AddedToHierarchy);
			if (_useHierarchyRebuildSystem)
			{
				_hierarchyRebuildDataManager.DeleteAllData();
			}
		}
	}
}
