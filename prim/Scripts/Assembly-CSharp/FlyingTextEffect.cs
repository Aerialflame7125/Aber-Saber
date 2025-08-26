using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class FlyingTextEffect : MonoBehaviour
{
	[SerializeField]
	private TextMeshPro[] _textMeshes;

	[SerializeField]
	private AnimationCurve _moveAnimationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	[SerializeField]
	private AnimationCurve _fadeAnimationCurve = AnimationCurve.Linear(0f, 1f, 1f, 0f);

	[SerializeField]
	private float _shakeFrequency = 1f;

	[SerializeField]
	private float _shakeStrength = 20f;

	[SerializeField]
	private AnimationCurve _shakeStrengthAnimationCurve = AnimationCurve.Linear(0f, 1f, 1f, 0f);

	private bool _shake;

	private Quaternion _rotation = default(Quaternion);

	protected Color _color;

	public string text
	{
		set
		{
			for (int i = 0; i < _textMeshes.Length; i++)
			{
				_textMeshes[i].text = value;
			}
		}
	}

	private void Awake()
	{
	}

	public void InitAndPresent(string text, float duration, Vector3 targetPos, Color color, float fontSize, bool shake)
	{
		_shake = shake;
		_color = color;
		for (int i = 0; i < _textMeshes.Length; i++)
		{
			_textMeshes[i].fontSize = fontSize;
			_textMeshes[i].text = text;
		}
		StopAllCoroutines();
		StartCoroutine(FadeOutAndMove(duration, targetPos));
	}

	private IEnumerator FadeOutAndMove(float duration, Vector3 targetPos)
	{
		Vector3 startPos = base.transform.position;
		float elapsedTime = 0f;
		while (elapsedTime < duration)
		{
			float t = elapsedTime / duration;
			float alpha = _fadeAnimationCurve.Evaluate(t);
			Color color = _color;
			color.a *= alpha;
			for (int i = 0; i < _textMeshes.Length; i++)
			{
				_textMeshes[i].color = color;
			}
			base.transform.localPosition = Vector3.Lerp(startPos, targetPos, _moveAnimationCurve.Evaluate(t));
			if (_shake)
			{
				_rotation.eulerAngles = new Vector3(0f, 0f, Mathf.Sin(t * (float)Math.PI * _shakeFrequency) * _shakeStrength * _shakeStrengthAnimationCurve.Evaluate(t));
				base.transform.localRotation = _rotation;
			}
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		base.gameObject.Recycle();
	}
}
