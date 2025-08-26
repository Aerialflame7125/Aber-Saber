using System;
using System.Collections;
using UnityEngine;

public class IntroTutorialController : MonoBehaviour
{
	[SerializeField]
	private BloomFog _bloomFog;

	[SerializeField]
	private IntroTutorialRing _redRing;

	[SerializeField]
	private IntroTutorialRing _blueRing;

	[SerializeField]
	private CanvasGroup _textCanvasGroup;

	[SerializeField]
	private ParticleSystem _shockWavePS;

	private bool _showingFinishAnimation;

	public event Action introTutorialWillFinishEvent;

	public event Action introTutorialDidFinishEvent;

	private void Update()
	{
		if (_redRing.enabled && _redRing.fullyActivated && _blueRing.enabled && _blueRing.fullyActivated)
		{
			ShowFinishAnimation();
		}
	}

	private void ShowFinishAnimation()
	{
		if (!_showingFinishAnimation)
		{
			_showingFinishAnimation = true;
			_redRing.enabled = false;
			_blueRing.enabled = false;
			_shockWavePS.Emit(1);
			StartCoroutine(ShowFinishAnimationCoroutine());
		}
	}

	private IEnumerator ShowFinishAnimationCoroutine()
	{
		float elapsedTime = 0f;
		float duration = 0.2f;
		while (elapsedTime < duration)
		{
			float t = elapsedTime / duration;
			SetFinishAnimationParams(t);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		SetFinishAnimationParams(1f);
		_redRing.gameObject.SetActive(false);
		_blueRing.gameObject.SetActive(false);
		_textCanvasGroup.gameObject.SetActive(false);
		if (this.introTutorialWillFinishEvent != null)
		{
			this.introTutorialWillFinishEvent();
		}
		if (this.introTutorialDidFinishEvent != null)
		{
			this.introTutorialDidFinishEvent();
		}
	}

	private void SetFinishAnimationParams(float progress)
	{
		_bloomFog.transition = progress;
		_textCanvasGroup.alpha = 1f - progress;
		_redRing.alpha = 1f - progress;
		_blueRing.alpha = 1f - progress;
	}
}
