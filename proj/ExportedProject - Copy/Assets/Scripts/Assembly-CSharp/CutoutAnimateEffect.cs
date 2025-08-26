using System.Collections;
using UnityEngine;

public class CutoutAnimateEffect : MonoBehaviour
{
	[SerializeField]
	private MaterialPropertyBlockController _materialPropertyBlockController;

	[Space]
	[SerializeField]
	private AnimationCurve _transitionCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	private Vector3 _randomNoiseTexOffset;

	private int _cutoutPropertyID;

	private int _cutoutTexOffsetPropertyID;

	public bool animating { get; private set; }

	private void Start()
	{
		_randomNoiseTexOffset = Random.onUnitSphere * 10f;
		_cutoutPropertyID = Shader.PropertyToID("_Cutout");
		_cutoutTexOffsetPropertyID = Shader.PropertyToID("_CutoutTexOffset");
		SetShaderData(0f);
	}

	private IEnumerator AnimateToCutoutCoroutine(float cutoutStart, float cutoutEnd, float duration)
	{
		animating = true;
		float elapsedTime = 0f;
		while (elapsedTime < duration)
		{
			float t = elapsedTime / duration;
			SetShaderData(Mathf.Lerp(cutoutStart, cutoutEnd, _transitionCurve.Evaluate(t)));
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		SetShaderData(cutoutEnd);
		animating = false;
	}

	private void SetShaderData(float cutout)
	{
		MaterialPropertyBlock materialPropertyBlock = _materialPropertyBlockController.materialPropertyBlock;
		materialPropertyBlock.SetVector(_cutoutTexOffsetPropertyID, _randomNoiseTexOffset);
		materialPropertyBlock.SetFloat(_cutoutPropertyID, cutout);
		_materialPropertyBlockController.ApplyChanges();
	}

	public void ResetEffect()
	{
		animating = false;
		StopAllCoroutines();
		SetShaderData(0f);
	}

	public void AnimateCutout(float cutoutStart, float cutoutEnd, float duration)
	{
		StopAllCoroutines();
		StartCoroutine(AnimateToCutoutCoroutine(cutoutStart, cutoutEnd, duration));
	}
}
