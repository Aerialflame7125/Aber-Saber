using UnityEngine;

public class LoadingIndicator : MonoBehaviour
{
	[SerializeField]
	private GameObject _loadingIndicator;

	public void ShowLoading()
	{
		_loadingIndicator.SetActive(true);
	}

	public void HideLoading()
	{
		_loadingIndicator.SetActive(false);
	}
}
