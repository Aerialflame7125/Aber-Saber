using System;
using UnityEngine;

public abstract class SongController : MonoBehaviour
{
	public event Action songDidFinishEvent;

	public void SendSongDidFinishEvent()
	{
		if (this.songDidFinishEvent != null)
		{
			this.songDidFinishEvent();
		}
	}

	public abstract void StartSong();

	public abstract void StopSong();

	public abstract void PauseSong();

	public abstract void ResumeSong();
}
