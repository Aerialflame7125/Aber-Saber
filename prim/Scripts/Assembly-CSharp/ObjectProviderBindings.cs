using System;
using UnityEngine;

public class ObjectProviderBindings : MonoBehaviour
{
	[Serializable]
	public class ObjectProviderBindingData
	{
		public MonoBehaviour obj;

		[CreateSO]
		public ObjectProvider objectProvider;
	}

	[SerializeField]
	private ObjectProviderBindingData[] _data;

	public ObjectProviderBindingData[] data => _data;

	private void Awake()
	{
		for (int i = 0; i < _data.Length; i++)
		{
			_data[i].objectProvider.Setup(_data[i].obj);
		}
	}

	private void OnDestroy()
	{
		for (int i = 0; i < _data.Length; i++)
		{
			_data[i].objectProvider.Reset();
		}
	}
}
