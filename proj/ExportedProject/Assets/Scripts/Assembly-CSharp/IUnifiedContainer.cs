using System;
using IUnifiedContainerBase;
using UnityEngine;

[Serializable]
public abstract class IUnifiedContainer<TResult> : global::IUnifiedContainerBase.IUnifiedContainerBase where TResult : class
{
	private TResult _result;

	public TResult Result
	{
		get
		{
			return (_result == null) ? (_result = ObjectField as TResult) : _result;
		}
		set
		{
			_result = value;
			ObjectField = _result as UnityEngine.Object;
		}
	}

	public UnityEngine.Object Object
	{
		get
		{
			return (!(ObjectField != null)) ? (ObjectField = _result as UnityEngine.Object) : ObjectField;
		}
	}
}
