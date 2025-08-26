using UnityEngine;

public class RandomAnimationStartTime : MonoBehaviour
{
	[SerializeField]
	private Animator _animator;

	[SerializeField]
	private string _stateName = "Idle";

	private void Start()
	{
		_animator.Play(_stateName, 0, Random.Range(0f, 1f));
	}
}
