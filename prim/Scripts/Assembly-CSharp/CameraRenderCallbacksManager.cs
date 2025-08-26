using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraRenderCallbacksManager : MonoBehaviour
{
	public interface ICameraRenderCallbacks
	{
		void OnCameraPostRender(Camera camera);

		void OnCameraPreRender(Camera camera);
	}

	private static Dictionary<Camera, CameraRenderCallbacksManager> _callbackManagers = new Dictionary<Camera, CameraRenderCallbacksManager>();

	protected HashSet<ICameraRenderCallbacks> _observers = new HashSet<ICameraRenderCallbacks>();

	private Camera _camera;

	public static void RegisterForCameraCallbacks(Camera camera, ICameraRenderCallbacks observer)
	{
		CameraRenderCallbacksManager value = null;
		if (!_callbackManagers.TryGetValue(camera, out value))
		{
			value = camera.gameObject.GetComponent<CameraRenderCallbacksManager>();
			if (value == null)
			{
				value = camera.gameObject.AddComponent<CameraRenderCallbacksManager>();
				value._camera = camera;
				_callbackManagers.Add(camera, value);
			}
		}
		value.RegisterForCameraCallbacks(observer);
	}

	public static void UnregisterFromCameraCallbacks(ICameraRenderCallbacks observer)
	{
		List<Camera> list = new List<Camera>(_callbackManagers.Count);
		foreach (KeyValuePair<Camera, CameraRenderCallbacksManager> callbackManager in _callbackManagers)
		{
			CameraRenderCallbacksManager value = callbackManager.Value;
			value.UnregisterFromCameraCallbacksInternal(observer);
			if (value._observers.Count == 0)
			{
				list.Add(callbackManager.Key);
			}
		}
		foreach (Camera item in list)
		{
			_callbackManagers.Remove(item);
		}
	}

	private void RegisterForCameraCallbacks(ICameraRenderCallbacks observer)
	{
		_observers.Add(observer);
	}

	private void UnregisterFromCameraCallbacksInternal(ICameraRenderCallbacks observer)
	{
		_observers.Remove(observer);
		if (_observers.Count == 0)
		{
			Object.Destroy(this);
		}
	}

	private void Awake()
	{
		base.hideFlags = HideFlags.DontSaveInEditor;
		_observers = new HashSet<ICameraRenderCallbacks>();
	}

	private void OnPreRender()
	{
		foreach (ICameraRenderCallbacks observer in _observers)
		{
			observer.OnCameraPreRender(_camera);
		}
	}

	private void OnPostRender()
	{
		foreach (ICameraRenderCallbacks observer in _observers)
		{
			observer.OnCameraPostRender(_camera);
		}
	}
}
