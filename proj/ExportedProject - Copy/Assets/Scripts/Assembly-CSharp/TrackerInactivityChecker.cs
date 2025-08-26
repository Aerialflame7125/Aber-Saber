using UnityEngine;

public class TrackerInactivityChecker : MonoBehaviour
{
	[SerializeField]
	private Transform _checkedTransform;

	[SerializeField]
	private float _maxAllowedPosOffset = 0.01f;

	[SerializeField]
	private float _maxAllowedRotOffset = 1f;

	[SerializeField]
	private float _minInactivityTime = 30f;

	[SerializeField]
	private MenuSceneSetupData _menuSceneSetupData;

	private Vector3 _checkedPos;

	private Vector3 _checkedRot;

	private float _inactivityTime;

	private bool _inTransition;

	private void Awake()
	{
		_checkedPos = _checkedTransform.position;
		_checkedRot = _checkedTransform.eulerAngles;
		_inactivityTime = 0f;
		_inTransition = false;
	}

	private void Update()
	{
		Vector3 position = _checkedTransform.position;
		Vector3 eulerAngles = _checkedTransform.eulerAngles;
		if (Mathf.Abs(_checkedPos.x - position.x) > _maxAllowedPosOffset || Mathf.Abs(_checkedPos.y - position.y) > _maxAllowedPosOffset || Mathf.Abs(_checkedPos.z - position.z) > _maxAllowedPosOffset || Mathf.Abs(_checkedRot.x - eulerAngles.x) > _maxAllowedRotOffset || Mathf.Abs(_checkedRot.y - eulerAngles.y) > _maxAllowedRotOffset || Mathf.Abs(_checkedRot.z - eulerAngles.z) > _maxAllowedRotOffset)
		{
			_inactivityTime = 0f;
			_checkedPos = position;
			_checkedRot = eulerAngles;
		}
		_inactivityTime += Time.deltaTime;
		if (_inactivityTime > _minInactivityTime && !_inTransition)
		{
			_inTransition = true;
			_menuSceneSetupData.TransitionToScene(0f);
		}
	}
}
