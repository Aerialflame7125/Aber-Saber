using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Valve.VR;

public class VRTrackersRecorder : MonoBehaviour
{
	[Serializable]
	private class SavedData
	{
		[Serializable]
		public class KeyframeSerializable
		{
			[Serializable]
			public class TransformSerializable
			{
				[SerializeField]
				public float _xPos;

				[SerializeField]
				public float _yPos;

				[SerializeField]
				public float _zPos;

				[SerializeField]
				public float _xRot;

				[SerializeField]
				public float _yRot;

				[SerializeField]
				public float _zRot;

				[SerializeField]
				public float _wRot;

				[SerializeField]
				public bool _valid;
			}

			[SerializeField]
			public TransformSerializable[] _transforms;

			[SerializeField]
			public float _time;
		}

		public KeyframeSerializable[] _keyframes;
	}

	private enum RecordMode
	{
		Record,
		Playback,
		Off
	}

	private class Keyframe
	{
		public class KeyframeTransform
		{
			public Vector3 _pos;

			public Quaternion _rot;

			public bool _valid;
		}

		public KeyframeTransform[] _transforms;

		public float _time;
	}

	[SerializeField]
	private FloatVariable _songTime;

	[SerializeField]
	private string _saveFilename = "VRControllersRecording.dat";

	[SerializeField]
	private RecordMode _mode = RecordMode.Off;

	[SerializeField]
	private Transform _originTransform;

	[SerializeField]
	private Transform[] _playbackTransforms;

	private List<Keyframe> _keyframes;

	private int _keyframeIndex;

	private SteamVR_Events.Action _newPosesAction;

	private Vector3 _loadedOriginPos;

	private Quaternion _loadedOriginRot;

	private void Awake()
	{
		if (_saveFilename == string.Empty)
		{
			_saveFilename = "VRControllersRecording.dat";
		}
		_keyframes = new List<Keyframe>();
		if (_mode == RecordMode.Playback)
		{
			Load();
		}
		else if (_mode == RecordMode.Record)
		{
			_newPosesAction = SteamVR_Events.NewPosesAction(OnNewPoses);
			Debug.LogWarning("Recording performance.");
		}
		if (_playbackTransforms.Length == 0)
		{
			Debug.LogWarning("No playback transforms in VR trackers recorder.");
		}
	}

	private void OnDestroy()
	{
		if (_mode == RecordMode.Record)
		{
			Save();
		}
	}

	private void OnEnable()
	{
		if (_newPosesAction != null && _mode == RecordMode.Record)
		{
			_newPosesAction.enabled = true;
		}
	}

	private void OnDisable()
	{
		if (_newPosesAction != null && _mode == RecordMode.Record)
		{
			_newPosesAction.enabled = false;
		}
	}

	private void OnNewPoses(TrackedDevicePose_t[] poses)
	{
		Keyframe keyframe = new Keyframe();
		keyframe._transforms = new Keyframe.KeyframeTransform[poses.Length];
		for (int i = 0; i < poses.Length; i++)
		{
			Keyframe.KeyframeTransform keyframeTransform = (keyframe._transforms[i] = new Keyframe.KeyframeTransform());
			if (poses[i].bDeviceIsConnected && poses[i].bPoseIsValid)
			{
				SteamVR_Utils.RigidTransform rigidTransform = new SteamVR_Utils.RigidTransform(poses[i].mDeviceToAbsoluteTracking);
				keyframeTransform._pos = rigidTransform.pos;
				keyframeTransform._rot = rigidTransform.rot;
				keyframeTransform._valid = true;
			}
			else
			{
				keyframeTransform._pos = Vector3.zero;
				keyframeTransform._rot = Quaternion.identity;
				keyframeTransform._valid = false;
			}
		}
		keyframe._time = _songTime.value;
		_keyframes.Add(keyframe);
	}

	private void Update()
	{
		if (_mode != RecordMode.Playback || _keyframes.Count < 2)
		{
			return;
		}
		float value = _songTime.value;
		while (_keyframeIndex < _keyframes.Count - 2 && _keyframes[_keyframeIndex + 1]._time < value)
		{
			_keyframeIndex++;
		}
		Vector3 position = _originTransform.position;
		Quaternion rotation = _originTransform.rotation;
		Keyframe keyframe = _keyframes[_keyframeIndex];
		Keyframe keyframe2 = _keyframes[_keyframeIndex + 1];
		float t = (value - keyframe._time) / Mathf.Max(1E-06f, keyframe2._time - keyframe._time);
		for (int i = 0; i < _playbackTransforms.Length; i++)
		{
			if (!(_playbackTransforms[i] == null) && i < keyframe._transforms.Length && i < keyframe2._transforms.Length)
			{
				Vector3 pos = keyframe._transforms[i]._pos;
				Vector3 pos2 = keyframe2._transforms[i]._pos;
				Quaternion rot = keyframe._transforms[i]._rot;
				Quaternion rot2 = keyframe2._transforms[i]._rot;
				_playbackTransforms[i].position = rotation * Vector3.Lerp(pos, pos2, t) + position;
				_playbackTransforms[i].rotation = rotation * Quaternion.Lerp(rot, rot2, t);
			}
		}
	}

	private void Save()
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		FileStream fileStream = File.Open(_saveFilename, FileMode.OpenOrCreate);
		SavedData savedData = new SavedData();
		savedData._keyframes = new SavedData.KeyframeSerializable[_keyframes.Count];
		for (int i = 0; i < _keyframes.Count; i++)
		{
			Keyframe keyframe = _keyframes[i];
			SavedData.KeyframeSerializable keyframeSerializable = new SavedData.KeyframeSerializable();
			keyframeSerializable._transforms = new SavedData.KeyframeSerializable.TransformSerializable[keyframe._transforms.Length];
			for (int j = 0; j < keyframe._transforms.Length; j++)
			{
				SavedData.KeyframeSerializable.TransformSerializable transformSerializable = (keyframeSerializable._transforms[j] = new SavedData.KeyframeSerializable.TransformSerializable());
				transformSerializable._valid = keyframeSerializable._transforms[j]._valid;
				Vector3 pos = keyframe._transforms[j]._pos;
				Quaternion rot = keyframe._transforms[j]._rot;
				transformSerializable._xPos = pos.x;
				transformSerializable._yPos = pos.y;
				transformSerializable._zPos = pos.z;
				transformSerializable._xRot = rot.x;
				transformSerializable._yRot = rot.y;
				transformSerializable._zRot = rot.z;
				transformSerializable._wRot = rot.w;
			}
			keyframeSerializable._time = keyframe._time;
			savedData._keyframes[i] = keyframeSerializable;
		}
		binaryFormatter.Serialize(fileStream, savedData);
		fileStream.Close();
		Debug.Log("Performance saved to file " + _saveFilename);
	}

	private void Load()
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		FileStream fileStream = null;
		SavedData savedData = null;
		try
		{
			fileStream = File.Open(_saveFilename, FileMode.Open);
			savedData = (SavedData)binaryFormatter.Deserialize(fileStream);
		}
		catch
		{
			savedData = null;
		}
		finally
		{
			fileStream?.Close();
		}
		if (savedData != null)
		{
			_keyframes = new List<Keyframe>(savedData._keyframes.Length);
			for (int i = 0; i < savedData._keyframes.Length; i++)
			{
				SavedData.KeyframeSerializable keyframeSerializable = savedData._keyframes[i];
				Keyframe keyframe = new Keyframe();
				keyframe._transforms = new Keyframe.KeyframeTransform[keyframeSerializable._transforms.Length];
				for (int j = 0; j < keyframe._transforms.Length; j++)
				{
					SavedData.KeyframeSerializable.TransformSerializable transformSerializable = keyframeSerializable._transforms[j];
					keyframe._transforms[j] = new Keyframe.KeyframeTransform();
					keyframe._transforms[j]._valid = transformSerializable._valid;
					keyframe._transforms[j]._pos = new Vector3(transformSerializable._xPos, transformSerializable._yPos, transformSerializable._zPos);
					keyframe._transforms[j]._rot = new Quaternion(transformSerializable._xRot, transformSerializable._yRot, transformSerializable._zRot, transformSerializable._wRot);
				}
				keyframe._time = keyframeSerializable._time;
				_keyframes.Add(keyframe);
			}
			Debug.Log("Performance loaded from file " + _saveFilename);
		}
		else
		{
			Debug.Log("Loading performance file failed (" + _saveFilename + ")");
			base.enabled = false;
		}
	}
}
