using TMPro;
using UnityEngine;

public class BuildInfoOverlay : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI _text;

	[SerializeField]
	private GameObject _canvas;

	private void Start()
	{
		base.gameObject.SetActive(false);
	}
}
