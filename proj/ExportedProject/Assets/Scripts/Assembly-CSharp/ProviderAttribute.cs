using System;
using UnityEngine;

public class ProviderAttribute : PropertyAttribute
{
	public Type type;

	public ProviderAttribute(Type type)
	{
		this.type = type;
	}
}
