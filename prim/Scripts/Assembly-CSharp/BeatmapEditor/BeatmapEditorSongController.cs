using UnityEngine;

namespace BeatmapEditor;

public class BeatmapEditorSongController : MonoBehaviour
{
	[SerializeField]
	private ObservableFloatSO _songTimeOffset;

	[SerializeField]
	private EditorAudioSO _editorAudio;

	[SerializeField]
	private AudioSource _audioSource;

	private float _songTime;

	private float _startTime;

	private float[] _sampleData;

	public float songDuration => (!(_audioSource.clip != null)) ? 0f : _audioSource.clip.length;

	public bool isPlaying => _audioSource.isPlaying;

	public AudioClip audioClip => _audioSource.clip;

	public float songTime
	{
		get
		{
			return _songTime;
		}
		set
		{
			if (_audioSource.clip == null)
			{
				_songTime = 0f;
			}
			else
			{
				_songTime = Mathf.Clamp(value, 0f, _audioSource.clip.length - 0.001f);
			}
		}
	}

	private void Awake()
	{
		_sampleData = new float[2];
		_sampleData[0] = 0f;
		_sampleData[1] = 0f;
	}

	private void Start()
	{
		_audioSource.clip = _editorAudio.audioClip;
		_editorAudio.didChangeAudioEvent += HandleEditorAudioDidChangeAudio;
	}

	private void OnDestroy()
	{
		if ((bool)_editorAudio)
		{
			_editorAudio.didChangeAudioEvent -= HandleEditorAudioDidChangeAudio;
		}
	}

	private void Update()
	{
		_songTime = Mathf.Max(0f, _audioSource.time - (float)_songTimeOffset);
	}

	public void PlaySong(float startTime)
	{
		if (!(_audioSource.clip == null))
		{
			startTime = Mathf.Clamp(startTime, 0f, _audioSource.clip.length - 0.01f);
			_startTime = startTime;
			_audioSource.time = startTime;
			_songTime = startTime;
			_audioSource.Play();
		}
	}

	public void PauseSong()
	{
		if (!(_audioSource.clip == null))
		{
			_audioSource.Pause();
		}
	}

	public void StopSong()
	{
		if (!(_audioSource.clip == null))
		{
			_audioSource.Stop();
			_audioSource.time = _startTime;
			_songTime = _startTime;
		}
	}

	public void RewindSong()
	{
		if (!(_audioSource.clip == null))
		{
			_audioSource.Stop();
			_audioSource.time = 0f;
			_songTime = 0f;
			_startTime = 0f;
		}
	}

	private void HandleEditorAudioDidChangeAudio(AudioClip audioClip)
	{
		_audioSource.Stop();
		_audioSource.clip = audioClip;
	}
}
