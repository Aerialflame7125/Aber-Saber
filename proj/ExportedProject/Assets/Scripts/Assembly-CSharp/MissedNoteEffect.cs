using UnityEngine;

public class MissedNoteEffect : MonoBehaviour
{
	[SerializeField]
	private FloatVariable _songTime;

	[Space]
	[SerializeField]
	private AnimationClip _animationClip;

	[SerializeField]
	private SpriteRenderer[] _spriteRenderers;

	private SongController _songController;

	private float _animationDuration;

	private float _startAnimationTime;

	private void Awake()
	{
	}

	private void Update()
	{
		_animationClip.SampleAnimation(base.gameObject, Mathf.Clamp01((_songTime.value - _startAnimationTime) / _animationDuration) * _animationClip.length);
		if (_songTime.value - _startAnimationTime > _animationDuration)
		{
			base.gameObject.Recycle();
		}
	}

	public void Init(NoteData noteData, float animationDuration, float startAnimationTime)
	{
		_animationDuration = animationDuration;
		_startAnimationTime = startAnimationTime;
		_animationClip.SampleAnimation(base.gameObject, 0f);
	}
}
