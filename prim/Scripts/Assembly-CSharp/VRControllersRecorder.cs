using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class VRControllersRecorder : MonoBehaviour
{
	[Serializable]
	private class SavedData
	{
		[Serializable]
		public class KeyframeSerializable
		{
			public float _xPos1;

			public float _yPos1;

			public float _zPos1;

			public float _xPos2;

			public float _yPos2;

			public float _zPos2;

			public float _xPos3;

			public float _yPos3;

			public float _zPos3;

			public float _xRot1;

			public float _yRot1;

			public float _zRot1;

			public float _wRot1;

			public float _xRot2;

			public float _yRot2;

			public float _zRot2;

			public float _wRot2;

			public float _xRot3;

			public float _yRot3;

			public float _zRot3;

			public float _wRot3;

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
		public Vector3 _pos1;

		public Vector3 _pos2;

		public Vector3 _pos3;

		public Quaternion _rot1;

		public Quaternion _rot2;

		public Quaternion _rot3;

		public float _time;
	}

	[SerializeField]
	[Provider(typeof(PlayerController))]
	private ObjectProvider _playerControllerProvider;

	[SerializeField]
	private FloatVariable _songTime;

	[SerializeField]
	private VRController _controller0;

	[SerializeField]
	private VRController _controller1;

	[SerializeField]
	private Transform _headTransform;

	[SerializeField]
	private Camera _camera;

	[Space]
	[SerializeField]
	private string _saveFilename = "VRControllersRecording.dat";

	[SerializeField]
	private RecordMode _mode = RecordMode.Off;

	[Space]
	[SerializeField]
	private bool _dontMoveHead;

	[SerializeField]
	private bool _changeToNonVRCamera;

	[SerializeField]
	private Vector3 _headRotationOffset;

	[SerializeField]
	private Vector3 _headPositionOffset;

	[SerializeField]
	private float _headSmooth = 8f;

	[SerializeField]
	private float _cameraFOV = 65f;

	[SerializeField]
	private float _controllersTimeOffset;

	[SerializeField]
	private float _controllersSmooth = 8f;

	private List<Keyframe> _keyframes;

	private int _keyframeIndex;

	private PlayerController _playerController;

	private void Awake()
	{
		_playerController = _playerControllerProvider.GetProvidedObject<PlayerController>();
		if (_saveFilename == string.Empty)
		{
			_saveFilename = "Recordings/VRControllersRecording.rd";
		}
		if (_mode == RecordMode.Off)
		{
			base.enabled = false;
		}
		if (_mode == RecordMode.Playback)
		{
			_controller0.enabled = false;
			_controller1.enabled = false;
			if (_changeToNonVRCamera)
			{
				_camera.stereoTargetEye = StereoTargetEyeMask.None;
				_camera.fieldOfView = _cameraFOV;
			}
		}
	}

	private void Start()
	{
		_keyframes = new List<Keyframe>();
		if (_mode == RecordMode.Playback)
		{
			Load();
			if (_keyframes.Count > 0 && _headTransform != null && !_dontMoveHead)
			{
				_headTransform.transform.SetPositionAndRotation(_keyframes[0]._pos3, _keyframes[0]._rot3);
			}
		}
		else if (_mode == RecordMode.Record)
		{
			Debug.LogWarning("Recording performance.");
		}
	}

	private void OnDestroy()
	{
		if (_mode == RecordMode.Record)
		{
			Save();
		}
	}

	private void PlaybackTick()
	{
		float num = _songTime.value + _controllersTimeOffset;
		while (_keyframeIndex < _keyframes.Count - 2 && _keyframes[_keyframeIndex + 1]._time < num)
		{
			_keyframeIndex++;
		}
		Keyframe keyframe = _keyframes[_keyframeIndex];
		Keyframe keyframe2 = _keyframes[_keyframeIndex + 1];
		float t = (num - keyframe._time) / Mathf.Max(1E-06f, keyframe2._time - keyframe._time);
		if (_controller0.transform != null)
		{
			if (!_controller0.transform.gameObject.activeSelf)
			{
				_controller0.transform.gameObject.SetActive(value: true);
			}
			Vector3 targetPos = Vector3.Lerp(keyframe._pos1, keyframe2._pos1, t);
			Quaternion targetRot = Quaternion.Lerp(keyframe._rot1, keyframe2._rot1, t);
			SetPositionAndRotation(_controller0.transform.transform, targetPos, targetRot, Time.deltaTime * _controllersSmooth);
		}
		if (_controller1.transform != null)
		{
			if (!_controller1.transform.gameObject.activeSelf)
			{
				_controller1.transform.gameObject.SetActive(value: true);
			}
			Vector3 targetPos2 = Vector3.Lerp(keyframe._pos2, keyframe2._pos2, t);
			Quaternion targetRot2 = Quaternion.Lerp(keyframe._rot2, keyframe2._rot2, t);
			SetPositionAndRotation(_controller1.transform.transform, targetPos2, targetRot2, Time.deltaTime * _controllersSmooth);
		}
		if (!(_headTransform != null))
		{
			return;
		}
		Vector3 vector = Vector3.Lerp(keyframe._pos3, keyframe2._pos3, t);
		_playerController.OverrideHeadPos(vector);
		if (!_dontMoveHead)
		{
			Quaternion quaternion = Quaternion.Lerp(keyframe._rot3, keyframe2._rot3, t);
			Vector3 eulerAngles = quaternion.eulerAngles;
			eulerAngles += _headRotationOffset;
			quaternion.eulerAngles = eulerAngles;
			vector += _headPositionOffset;
			if (_headSmooth == 0f)
			{
				_headTransform.SetPositionAndRotation(vector, quaternion);
				return;
			}
			_headTransform.position = Vector3.Lerp(_headTransform.position, vector, _headSmooth * Time.deltaTime);
			_headTransform.rotation = Quaternion.Lerp(_headTransform.rotation, quaternion, _headSmooth * Time.deltaTime);
		}
	}

	private void SetPositionAndRotation(Transform transf, Vector3 targetPos, Quaternion targetRot, float t)
	{
		Vector3 position = transf.position;
		Quaternion rotation = transf.rotation;
		position = Vector3.Lerp(position, targetPos, t);
		rotation = Quaternion.Lerp(rotation, targetRot, t);
		transf.SetPositionAndRotation(position, rotation);
	}

	private void RecordTick()
	{
		Keyframe keyframe = new Keyframe();
		if (_controller0.transform != null)
		{
			keyframe._pos1 = _controller0.transform.position;
			keyframe._rot1 = _controller0.transform.rotation;
		}
		if (_controller1.transform != null)
		{
			keyframe._pos2 = _controller1.transform.position;
			keyframe._rot2 = _controller1.transform.rotation;
		}
		if (_headTransform != null)
		{
			keyframe._pos3 = _headTransform.position;
			keyframe._rot3 = _headTransform.rotation;
		}
		keyframe._time = _songTime.value;
		_keyframes.Add(keyframe);
	}

	private void Update()
	{
		if (_mode == RecordMode.Playback && _keyframes.Count > 1)
		{
			PlaybackTick();
		}
	}

	private void LateUpdate()
	{
		if (_mode == RecordMode.Record)
		{
			RecordTick();
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
			keyframeSerializable._xPos1 = keyframe._pos1.x;
			keyframeSerializable._yPos1 = keyframe._pos1.y;
			keyframeSerializable._zPos1 = keyframe._pos1.z;
			keyframeSerializable._xPos2 = keyframe._pos2.x;
			keyframeSerializable._yPos2 = keyframe._pos2.y;
			keyframeSerializable._zPos2 = keyframe._pos2.z;
			keyframeSerializable._xPos3 = keyframe._pos3.x;
			keyframeSerializable._yPos3 = keyframe._pos3.y;
			keyframeSerializable._zPos3 = keyframe._pos3.z;
			keyframeSerializable._xRot1 = keyframe._rot1.x;
			keyframeSerializable._yRot1 = keyframe._rot1.y;
			keyframeSerializable._zRot1 = keyframe._rot1.z;
			keyframeSerializable._wRot1 = keyframe._rot1.w;
			keyframeSerializable._xRot2 = keyframe._rot2.x;
			keyframeSerializable._yRot2 = keyframe._rot2.y;
			keyframeSerializable._zRot2 = keyframe._rot2.z;
			keyframeSerializable._wRot2 = keyframe._rot2.w;
			keyframeSerializable._xRot3 = keyframe._rot3.x;
			keyframeSerializable._yRot3 = keyframe._rot3.y;
			keyframeSerializable._zRot3 = keyframe._rot3.z;
			keyframeSerializable._wRot3 = keyframe._rot3.w;
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
				keyframe._pos1 = new Vector3(keyframeSerializable._xPos1, keyframeSerializable._yPos1, keyframeSerializable._zPos1);
				keyframe._pos2 = new Vector3(keyframeSerializable._xPos2, keyframeSerializable._yPos2, keyframeSerializable._zPos2);
				keyframe._pos3 = new Vector3(keyframeSerializable._xPos3, keyframeSerializable._yPos3, keyframeSerializable._zPos3);
				keyframe._rot1 = new Quaternion(keyframeSerializable._xRot1, keyframeSerializable._yRot1, keyframeSerializable._zRot1, keyframeSerializable._wRot1);
				keyframe._rot2 = new Quaternion(keyframeSerializable._xRot2, keyframeSerializable._yRot2, keyframeSerializable._zRot2, keyframeSerializable._wRot2);
				keyframe._rot3 = new Quaternion(keyframeSerializable._xRot3, keyframeSerializable._yRot3, keyframeSerializable._zRot3, keyframeSerializable._wRot3);
				keyframe._time = keyframeSerializable._time;
				_keyframes.Add(keyframe);
			}
			Debug.Log("Performance loaded from file " + _saveFilename);
		}
		else
		{
			Debug.LogWarning("Loading performance file failed (" + _saveFilename + ")");
			base.enabled = false;
		}
	}
}
