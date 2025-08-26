using UnityEngine;
using UnityEngine.UI;

public class SongProgressUIController : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(SongController))]
	private ObjectProvider _songControllerProvider;

	[SerializeField]
	private FloatVariable _songTime;

	[SerializeField]
	private Image _progressBar;

	private GameSongController _gameSongController;

	private void Start()
	{
		_gameSongController = _songControllerProvider.GetProvidedObject<SongController>() as GameSongController;
	}

	private void Update()
	{
		_progressBar.fillAmount = _songTime.value / _gameSongController.songLength;
	}
}
