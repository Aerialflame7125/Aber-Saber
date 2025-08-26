using System;
using System.Runtime.InteropServices;
using UnityEngine;

[AddComponentMenu("AVPro Live Camera/Grabber")]
public class AVProLiveCameraGrabber : MonoBehaviour
{
	public AVProLiveCamera _camera;

	private AVProLiveCameraDevice _device;

	private Color32[] _frameData;

	private int _frameWidth;

	private int _frameHeight;

	private GCHandle _frameHandle;

	private IntPtr _framePointer;

	private uint _lastFrame;

	private Texture2D _testTexture;

	private void Update()
	{
		if (_camera != null)
		{
			_device = _camera.Device;
		}
		if (_device == null || !_device.IsActive || _device.IsPaused)
		{
			return;
		}
		if (_device.CurrentWidth > _frameWidth || _device.CurrentHeight > _frameHeight)
		{
			CreateBuffer(_device.CurrentWidth, _device.CurrentHeight);
		}
		uint lastFrame = AVProLiveCameraPlugin.GetLastFrame(_device.DeviceIndex);
		if (lastFrame != _lastFrame)
		{
			_lastFrame = lastFrame;
			if (AVProLiveCameraPlugin.GetFrameAsColor32(_device.DeviceIndex, _framePointer, _frameWidth, _frameHeight))
			{
				_testTexture.SetPixels32(_frameData);
				_testTexture.Apply(false, false);
			}
		}
	}

	private void CreateBuffer(int width, int height)
	{
		if (_frameHandle.IsAllocated && _frameData != null && _frameData.Length < _frameWidth * _frameHeight)
		{
			FreeBuffer();
		}
		if (_frameData == null)
		{
			_frameWidth = width;
			_frameHeight = height;
			_frameData = new Color32[_frameWidth * _frameHeight];
			_frameHandle = GCHandle.Alloc(_frameData, GCHandleType.Pinned);
			_framePointer = _frameHandle.AddrOfPinnedObject();
			_testTexture = new Texture2D(_frameWidth, _frameHeight, TextureFormat.ARGB32, false, false);
			_testTexture.Apply(false, false);
		}
	}

	private void FreeBuffer()
	{
		if (_frameHandle.IsAllocated)
		{
			_framePointer = IntPtr.Zero;
			_frameHandle.Free();
			_frameData = null;
		}
		if ((bool)_testTexture)
		{
			UnityEngine.Object.DestroyImmediate(_testTexture);
			_testTexture = null;
		}
	}

	private void OnDestroy()
	{
		FreeBuffer();
	}

	private void OnGUI()
	{
		if ((bool)_testTexture)
		{
			GUI.depth = 1;
			GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), _testTexture, ScaleMode.ScaleToFit, false);
		}
	}
}
