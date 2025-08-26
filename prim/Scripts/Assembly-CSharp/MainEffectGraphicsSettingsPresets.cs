using System;
using UnityEngine;

public class MainEffectGraphicsSettingsPresets : ScriptableObject
{
	[Serializable]
	public class Preset
	{
		public string presetName;

		[Range(0f, 3f)]
		public float baseColorBoost = 0.5f;

		public float baseColorBoostThreshold = 0.1f;

		[Space]
		public int bloomIterations = 4;

		public float bloomAlphaWeightScale = 1f;

		[Range(0f, 5f)]
		public float bloomIntensity = 1f;

		[Space]
		public int textureWidth = 512;

		public int textureHeight = 384;
	}

	public Preset[] presets;
}
