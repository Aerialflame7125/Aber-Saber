using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Ease01", menuName = "HMLib/Others/Ease01")]
public class Ease01 : ScriptableObject
{
	[SerializeField]
	private FloatVariableSetter _variable;

	[SerializeField]
	private AnimationCurve _curve;

	[SerializeField]
	private float _defaultFadeOutDuration = 1.3f;

	[SerializeField]
	private float _defaultFadeInDuration = 1f;

	public void FadeOutInstant()
	{
		_variable.SetValue(0f);
	}

	public void FadeIn()
	{
		FadeIn(_defaultFadeInDuration);
	}

	public void FadeOut()
	{
		FadeOut(_defaultFadeOutDuration);
	}

	public void FadeIn(float duration)
	{
		SharedCoroutineStarter.instance.StartCoroutine(Fade(0f, 1f, duration));
	}

	public void FadeOut(float duration)
	{
		SharedCoroutineStarter.instance.StartCoroutine(Fade(_variable.value, 0f, duration));
	}

	private IEnumerator Fade(float fromValue, float toValue, float duration)
	{
		float startTime = Time.timeSinceLevelLoad;
		while (Time.timeSinceLevelLoad - startTime < duration)
		{
			_variable.SetValue(Mathf.Lerp(fromValue, toValue, _curve.Evaluate((Time.timeSinceLevelLoad - startTime) / duration)));
			yield return null;
		}
		_variable.SetValue(toValue);
	}
}
