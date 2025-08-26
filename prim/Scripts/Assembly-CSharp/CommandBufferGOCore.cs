using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class CommandBufferGOCore : MonoBehaviour
{
	private Dictionary<Camera, CommandBufferOwners> _cameras;

	private static Material _material;

	private void OnEnable()
	{
		MeshRenderer component = GetComponent<MeshRenderer>();
		if (component == null)
		{
			component = base.gameObject.AddComponent<MeshRenderer>();
			if (!_material)
			{
				_material = new Material(Shader.Find("Diffuse"));
				_material.renderQueue = 0;
			}
			component.material = _material;
		}
		MeshFilter meshFilter = GetComponent<MeshFilter>();
		if (meshFilter == null)
		{
			meshFilter = base.gameObject.AddComponent<MeshFilter>();
		}
		if (meshFilter.sharedMesh == null)
		{
			Mesh mesh = new Mesh();
			mesh.hideFlags = HideFlags.HideAndDontSave;
			mesh.bounds = new Bounds(Vector3.zero, new Vector3(9999999f, 9999999f, 9999999f));
			meshFilter.sharedMesh = mesh;
		}
		else
		{
			meshFilter.sharedMesh.bounds = new Bounds(Vector3.zero, new Vector3(9999999f, 9999999f, 9999999f));
		}
		_cameras = CamerasDict();
	}

	private void OnDisable()
	{
		List<Camera> list = new List<Camera>();
		foreach (KeyValuePair<Camera, CommandBufferOwners> camera in _cameras)
		{
			if ((bool)camera.Key)
			{
				CommandBufferOwners value = camera.Value;
				value.RemoveOwner(this);
				if (value.NumberOfOwners == 0)
				{
					camera.Key.RemoveCommandBuffer(CameraEvent.BeforeForwardAlpha, value.commandBuffer);
					list.Add(camera.Key);
				}
			}
		}
		foreach (Camera item in list)
		{
			_cameras.Remove(item);
		}
		EssentialHelpers.SafeDestroy(_material);
	}

	protected virtual void OnWillRenderObject()
	{
		if (!base.gameObject.activeInHierarchy || !base.enabled)
		{
			return;
		}
		Camera current = Camera.current;
		if (!current)
		{
			return;
		}
		if (_cameras.TryGetValue(current, out var value))
		{
			if (!value.ContainsOwner(this))
			{
				value.AddOwner(this);
			}
			return;
		}
		value = new CommandBufferOwners();
		CommandBuffer commandBuffer = CreateCommandBuffer();
		current.AddCommandBuffer(CommandBufferCameraEvent(), commandBuffer);
		value.commandBuffer = commandBuffer;
		value.AddOwner(this);
		_cameras[current] = value;
	}

	protected abstract CameraEvent CommandBufferCameraEvent();

	protected abstract CommandBuffer CreateCommandBuffer();

	protected abstract Dictionary<Camera, CommandBufferOwners> CamerasDict();
}
