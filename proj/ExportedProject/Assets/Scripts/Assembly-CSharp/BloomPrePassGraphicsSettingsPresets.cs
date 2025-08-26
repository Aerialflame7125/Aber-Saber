using System;
using UnityEngine;

public class BloomPrePassGraphicsSettingsPresets : ScriptableObject
{
	[Serializable]
	public class Preset
	{
		public string presetName;

		public KawaseBlurRenderer.KernelSize bloom1KernelSize = KawaseBlurRenderer.KernelSize.Kernel135;

		public float bloom1Boost;

		[Space]
		public KawaseBlurRenderer.KernelSize bloom2KernelSize = KawaseBlurRenderer.KernelSize.Kernel23;

		public float bloom2Boost;

		public float bloom2Alpha = 0.5f;

		[Space]
		public int downsample;

		public int textureWidth = 512;

		public int textureHeight = 384;
	}

	public Preset[] presets;
}
