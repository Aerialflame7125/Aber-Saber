using System;

namespace Zenject;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class InjectOptionalAttribute : InjectAttributeBase
{
	public InjectOptionalAttribute()
	{
		base.Optional = true;
	}
}
