using UnityEngine;

public class BeatEffect : MonoBehaviour
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

	private float _elapsedTime;

	private void Awake()
	{
	}

	private void Update()
	{
		_elapsedTime += Time.deltaTime;
		_animationClip.SampleAnimation(base.gameObject, Mathf.Clamp01(_elapsedTime / _animationDuration * _animationClip.length));
		if (_elapsedTime > _animationDuration)
		{
			base.gameObject.Recycle();
		}
	}

	public void Init(Color color, float animationDuration)
	{
		_elapsedTime = 0f;
		_animationDuration = animationDuration;
		_animationClip.SampleAnimation(base.gameObject, 0f);
		Color color2 = color.ColorWithAlpha(1f);
		SpriteRenderer[] spriteRenderers = _spriteRenderers;
		foreach (SpriteRenderer spriteRenderer in spriteRenderers)
		{
			spriteRenderer.color = color2;
		}
	}
}
