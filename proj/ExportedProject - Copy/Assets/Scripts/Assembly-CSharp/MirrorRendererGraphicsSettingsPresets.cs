using System;
using UnityEngine;

public class MirrorRendererGraphicsSettingsPresets : ScriptableObject
{
	[Serializable]
	public class Preset
	{
		public string presetName;

		public LayerMask reflectLayers = -1;

		public int stereoTextureWidth = 2048;

		public int stereoTextureHeight = 1024;

		public int monoTextureWidth = 256;

		public int monoTextureHeight = 256;

		public int maxAntiAliasing = 1;
	}

	public Preset[] presets;
}
