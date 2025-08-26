using System;
using UnityEngine;

namespace IUnifiedContainerBase
{
	[Serializable]
	public abstract class IUnifiedContainerBase
	{
		[SerializeField]
		[HideInInspector]
		protected UnityEngine.Object ObjectField;

		[SerializeField]
		[HideInInspector]
		protected string ResultType;
	}
}
