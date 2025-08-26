using System.Collections.Generic;
using UnityEngine;

namespace VRUI;

public class VRUIHierarchyRebuildDataManager : ScriptableObject
{
	private Dictionary<string, object> _dataDict = new Dictionary<string, object>();

	public void SetData(VRUIViewController viewController, object data)
	{
		_dataDict[viewController.hierarchyPathName] = data;
	}

	public T GetData<T>(VRUIViewController viewController) where T : class
	{
		object value = null;
		if (_dataDict.TryGetValue(viewController.hierarchyPathName, out value))
		{
			return value as T;
		}
		return (T)null;
	}

	public void DeleteData(VRUIViewController viewController)
	{
		_dataDict.Remove(viewController.hierarchyPathName);
	}

	public void DeleteAllData()
	{
		_dataDict.Clear();
	}

	private void OnEnable()
	{
		base.hideFlags |= HideFlags.DontUnloadUnusedAsset;
	}
}
