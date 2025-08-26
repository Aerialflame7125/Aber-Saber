using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.XR;

public class SimpleVRNodeRecorder : MonoBehaviour
{
	[Serializable]
	private class SavedData
	{
		[Serializable]
		public class NodeKeyframe
		{
			public float posX;

			public float posY;

			public float posZ;

			public float rotX;

			public float rotY;

			public float rotZ;

			public float rotW;

			public float time;

			public Vector3 pos => new Vector3(posX, posY, posZ);

			public Quaternion rot => new Quaternion(rotX, rotY, rotZ, rotW);

			public NodeKeyframe(Vector3 pos, Quaternion rot, float time)
			{
				posX = pos.x;
				posY = pos.y;
				posZ = pos.z;
				rotX = rot.x;
				rotY = rot.y;
				rotZ = rot.z;
				rotW = rot.w;
				this.time = time;
			}
		}

		public NodeKeyframe[] keyframes;
	}

	private enum RecordMode
	{
		Record,
		Playback,
		Off
	}

	[SerializeField]
	private FloatVariable _songTime;

	[SerializeField]
	private string _saveFilename = "VRControllersRecording.dat";

	[SerializeField]
	private RecordMode _mode = RecordMode.Off;

	[SerializeField]
	private XRNode _node;

	[SerializeField]
	private Transform _playbackTransform;

	[SerializeField]
	private float _smooth = 4f;

	[SerializeField]
	private float _forwardOffset;

	private List<SavedData.NodeKeyframe> _keyframes;

	private int _keyframeIndex;

	private Vector3 _prevPos;

	private Quaternion _prevRot;

	private void Awake()
	{
		if (_saveFilename == string.Empty)
		{
			_saveFilename = "VRControllersRecording.dat";
		}
		_keyframes = new List<SavedData.NodeKeyframe>();
		if (_mode == RecordMode.Playback)
		{
			Load();
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

	private void RecordNewKeyFrame()
	{
		SavedData.NodeKeyframe item = new SavedData.NodeKeyframe(InputTracking.GetLocalPosition(_node), InputTracking.GetLocalRotation(_node), _songTime.value);
		_keyframes.Add(item);
	}

	private void Update()
	{
		if (_mode == RecordMode.Record)
		{
			RecordNewKeyFrame();
		}
		if (_mode == RecordMode.Playback && _keyframes.Count >= 2)
		{
			float value = _songTime.value;
			while (_keyframeIndex < _keyframes.Count - 2 && _keyframes[_keyframeIndex + 1].time < value)
			{
				_keyframeIndex++;
			}
			SavedData.NodeKeyframe nodeKeyframe = _keyframes[_keyframeIndex];
			SavedData.NodeKeyframe nodeKeyframe2 = _keyframes[_keyframeIndex + 1];
			float t = (value - nodeKeyframe.time) / Mathf.Max(1E-06f, nodeKeyframe2.time - nodeKeyframe.time);
			Vector3 pos = nodeKeyframe.pos;
			Vector3 pos2 = nodeKeyframe2.pos;
			Quaternion rot = nodeKeyframe.rot;
			Quaternion rot2 = nodeKeyframe2.rot;
			if (_smooth < 0f)
			{
				_playbackTransform.localPosition = Vector3.Lerp(pos, pos2, t);
				_playbackTransform.localRotation = Quaternion.Lerp(rot, rot2, t);
				return;
			}
			Vector3 vector = Vector3.Lerp(_prevPos, Vector3.Lerp(pos, pos2, t), Time.deltaTime * _smooth);
			Quaternion quaternion = Quaternion.Lerp(_prevRot, Quaternion.Lerp(rot, rot2, t), Time.deltaTime * _smooth);
			Vector3 eulerAngles = quaternion.eulerAngles;
			eulerAngles.z = 0f;
			quaternion.eulerAngles = eulerAngles;
			_playbackTransform.localPosition = vector;
			_playbackTransform.localRotation = quaternion;
			_playbackTransform.localPosition += _playbackTransform.forward * _forwardOffset;
			_prevPos = vector;
			_prevRot = quaternion;
		}
	}

	private void Save()
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		FileStream fileStream = File.Open(_saveFilename, FileMode.OpenOrCreate);
		SavedData savedData = new SavedData();
		savedData.keyframes = _keyframes.ToArray();
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
			_keyframes = new List<SavedData.NodeKeyframe>(savedData.keyframes);
			Debug.Log("Performance loaded from file " + _saveFilename);
		}
		else
		{
			Debug.Log("Loading performance file failed (" + _saveFilename + ")");
			base.enabled = false;
		}
	}
}
