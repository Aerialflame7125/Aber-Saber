using System.IO;

namespace System.Media;

/// <summary>Represents a system sound type.</summary>
public class SystemSound
{
	private Stream resource;

	internal SystemSound(string tag)
	{
		resource = typeof(SystemSound).Assembly.GetManifestResourceStream(tag + ".wav");
	}

	/// <summary>Plays the system sound type.</summary>
	public void Play()
	{
		new SoundPlayer(resource).Play();
	}
}
