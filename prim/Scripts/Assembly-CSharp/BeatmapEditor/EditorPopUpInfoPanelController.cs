using System.Collections;
using UnityEngine;

namespace BeatmapEditor;

public class EditorPopUpInfoPanelController : MonoBehaviour
{
	public enum InfoType
	{
		Info,
		Warning
	}

	[SerializeField]
	private EditorPopUpInfoPanel _popupPanelPrefab;

	[SerializeField]
	private Vector2Int _showPos;

	[SerializeField]
	private Vector2 _hidePos;

	[SerializeField]
	private Color _warningColor = Color.red;

	[SerializeField]
	private Color _infoColor = Color.blue;

	public void ShowInfo(string text, InfoType infoType)
	{
		StartCoroutine(ShowInfoCoroutine(text, infoType));
	}

	private IEnumerator ShowInfoCoroutine(string text, InfoType infoType)
	{
		EditorPopUpInfoPanel panel = Object.Instantiate(_popupPanelPrefab, base.transform);
		panel.color = ((infoType != 0) ? _warningColor : _infoColor);
		panel.text = text;
		panel.anchoredPosition = _hidePos;
		float duration = 0.2f;
		float elapsedTime = 0f;
		while (elapsedTime < duration)
		{
			float t = elapsedTime / duration;
			panel.anchoredPosition = Vector2.Lerp(_hidePos, _showPos, (0f - t) * (t - 2f));
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		yield return new WaitForSeconds(3f);
		elapsedTime = 0f;
		while (elapsedTime < duration)
		{
			float t2 = elapsedTime / duration;
			panel.anchoredPosition = Vector2.Lerp(_showPos, _hidePos, t2 * t2);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		Object.Destroy(panel.gameObject);
	}
}
