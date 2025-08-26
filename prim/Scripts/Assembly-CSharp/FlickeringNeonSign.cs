using System.Collections;
using TMPro;
using UnityEngine;

public class FlickeringNeonSign : MonoBehaviour
{
	[SerializeField]
	private TextMeshPro _text;

	[SerializeField]
	private TubeBloomPrePassLight _light;

	[SerializeField]
	private float _minOnDelay = 0.05f;

	[SerializeField]
	private float _maxOnDelay = 0.4f;

	[SerializeField]
	private float _minOffDelay = 0.05f;

	[SerializeField]
	private float _maxOffDelay = 0.4f;

	[SerializeField]
	private Color _onColor;

	[SerializeField]
	private Color _offColor;

	[SerializeField]
	private TMP_FontAsset _onFont;

	[SerializeField]
	private TMP_FontAsset _offFont;

	private Color _currentColor;

	private IEnumerator Start()
	{
		while (true)
		{
			yield return new WaitForSeconds(Random.Range(_minOnDelay, _maxOnDelay));
			SetOn(on: false);
			yield return new WaitForSeconds(Random.Range(_minOffDelay, _maxOffDelay));
			SetOn(on: true);
		}
	}

	private void SetOn(bool on)
	{
		Color color = ((!on) ? _offColor : _onColor);
		TMP_FontAsset font = ((!on) ? _offFont : _onFont);
		_text.color = color;
		_text.font = font;
		_light.color = color;
	}
}
