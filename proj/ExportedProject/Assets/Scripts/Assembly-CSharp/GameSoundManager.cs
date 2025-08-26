using UnityEngine;

public class GameSoundManager : MonoBehaviour
{
	[SerializeField]
	private AudioSource _audioSource;

	[SerializeField]
	private AudioClip[] _noteJumpAudioClips;

	[SerializeField]
	private AudioClip _noteWasCutAudioClip;

	private RandomObjectPicker<AudioClip> _noteJumpAudioClipPicker;

	private void Awake()
	{
		_noteJumpAudioClipPicker = new RandomObjectPicker<AudioClip>(_noteJumpAudioClips, 0.02f);
	}

	private void PlaySoundFromPicker(RandomObjectPicker<AudioClip> picker, float volume)
	{
		AudioClip audioClip = picker.PickRandomObject();
		if (audioClip != null)
		{
			_audioSource.PlayOneShot(audioClip, volume);
		}
	}

	public void PlayNoteJumpSound()
	{
		PlaySoundFromPicker(_noteJumpAudioClipPicker, 1f);
	}

	public void PlayNoteWasCutSound()
	{
		_audioSource.PlayOneShot(_noteWasCutAudioClip, 1f);
	}
}
