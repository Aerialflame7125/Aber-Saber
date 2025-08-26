using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace VRUIControls
{
	[RequireComponent(typeof(EventSystem))]
	public class VRPointer : MonoBehaviour
	{
		[SerializeField]
		private VRController _leftVRController;

		[SerializeField]
		private VRController _rightVRController;

		[SerializeField]
		private Transform _laserPointerPrefab;

		[SerializeField]
		private Transform _laserHitPrefab;

		[SerializeField]
		private float _defaultLaserPointerLength = 10f;

		[SerializeField]
		private float _laserPointerWidth = 0.01f;

		public const float kScrollMultiplier = 1f;

		private Transform _laserPointerTransform;

		private Transform _laserHitTransform;

		private EventSystem _eventSystem;

		private VRController _vrController;

		private static bool _lastControllerUsedWasRight = true;

		private PointerEventData _pointerData;

		public VRController vrController
		{
			get
			{
				return _vrController;
			}
		}

		private EventSystem eventSystem
		{
			get
			{
				return _eventSystem ?? (_eventSystem = GetComponent<EventSystem>());
			}
		}

		public string state
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (_pointerData != null)
				{
					stringBuilder.AppendFormat("\ndevice id: {0}", _vrController.node);
					stringBuilder.AppendFormat("\nentered: {0}", (!(_pointerData.pointerEnter == null)) ? _pointerData.pointerEnter.name : "none");
					stringBuilder.AppendFormat("\npressed: {0}", (!(_pointerData.pointerPress == null)) ? _pointerData.pointerPress.name : "none");
					stringBuilder.AppendFormat("\ndragged: {0}", (!(_pointerData.pointerDrag == null)) ? _pointerData.pointerDrag.name : "none");
					stringBuilder.AppendFormat("\nselected: {0}", (!(eventSystem.currentSelectedGameObject == null)) ? eventSystem.currentSelectedGameObject.name : "none");
				}
				return stringBuilder.ToString();
			}
		}

		private void Awake()
		{
			if (_lastControllerUsedWasRight || !_leftVRController.active)
			{
				_vrController = _rightVRController;
			}
			else
			{
				_vrController = _leftVRController;
			}
		}

		private void OnEnable()
		{
			CreateLaserPointerAndLaserHit();
		}

		private void OnDisable()
		{
			DestroyLaserAndHit();
		}

		private void Update()
		{
			if (_leftVRController.active && _leftVRController.triggerValue > 0.1f && _vrController != _leftVRController)
			{
				_lastControllerUsedWasRight = false;
				_vrController = _leftVRController;
				DestroyLaserAndHit();
				CreateLaserPointerAndLaserHit();
			}
			else if (_rightVRController.active && _rightVRController.triggerValue > 0.1f && _vrController != _rightVRController)
			{
				_lastControllerUsedWasRight = true;
				_vrController = _rightVRController;
				DestroyLaserAndHit();
				CreateLaserPointerAndLaserHit();
			}
			if (eventSystem.enabled && _laserPointerTransform == null)
			{
				CreateLaserPointerAndLaserHit();
			}
			else if (!eventSystem.enabled && _laserPointerTransform != null)
			{
				DestroyLaserAndHit();
			}
		}

		private void CreateLaserPointerAndLaserHit()
		{
			if (_laserPointerTransform == null && _laserPointerPrefab != null)
			{
				_laserPointerTransform = Object.Instantiate(_laserPointerPrefab, _vrController.transform, false);
				_laserPointerTransform.localPosition = new Vector3(0f, 0f, _defaultLaserPointerLength / 2f);
				_laserPointerTransform.localScale = new Vector3(_laserPointerWidth, _laserPointerWidth, _defaultLaserPointerLength);
			}
			if (_laserHitTransform == null && _laserHitPrefab != null)
			{
				_laserHitTransform = Object.Instantiate(_laserHitPrefab);
				_laserHitTransform.gameObject.SetActive(false);
				_laserHitTransform.SetParent(base.transform, false);
			}
		}

		private void RefreshLaserPointerAndLaserHit(PointerEventData pointerData)
		{
			if (pointerData.pointerCurrentRaycast.gameObject != null)
			{
				if (_laserPointerTransform != null)
				{
					_laserPointerTransform.localPosition = new Vector3(0f, 0f, pointerData.pointerCurrentRaycast.distance / 2f);
					_laserPointerTransform.localScale = new Vector3(_laserPointerWidth, _laserPointerWidth, pointerData.pointerCurrentRaycast.distance);
				}
				if (_laserHitTransform != null)
				{
					_laserHitTransform.gameObject.SetActive(true);
					_laserHitTransform.position = pointerData.pointerCurrentRaycast.worldPosition;
				}
			}
			else
			{
				if (_laserPointerTransform != null)
				{
					_laserPointerTransform.localPosition = new Vector3(0f, 0f, _defaultLaserPointerLength / 2f);
					_laserPointerTransform.localScale = new Vector3(_laserPointerWidth, _laserPointerWidth, _defaultLaserPointerLength);
				}
				if (_laserHitTransform != null)
				{
					_laserHitTransform.gameObject.SetActive(false);
				}
			}
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			if (!hasFocus && _laserHitTransform != null)
			{
				_laserHitTransform.gameObject.SetActive(false);
			}
		}

		private void DestroyLaserAndHit()
		{
			if (_laserPointerTransform != null)
			{
				_laserPointerTransform.gameObject.SetActive(false);
				Object.Destroy(_laserPointerTransform.gameObject);
				_laserPointerTransform = null;
			}
			if (_laserHitTransform != null)
			{
				_laserHitTransform.gameObject.SetActive(false);
				Object.Destroy(_laserHitTransform.gameObject);
				_laserHitTransform = null;
			}
		}

		public void Process(PointerEventData pointerEventData)
		{
			if (!_vrController.active)
			{
				base.enabled = false;
				return;
			}
			base.enabled = true;
			_pointerData = pointerEventData;
			RefreshLaserPointerAndLaserHit(_pointerData);
		}
	}
}
