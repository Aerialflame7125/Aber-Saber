using UnityEngine;

public class LevelFailedTextEffect : MonoBehaviour
{
	[SerializeField]
	private Animator _animator;

	public void ShowEffect()
	{
		_animator.enabled = true;
	}
}
