using System;

namespace Zenject;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class InjectLocalAttribute : InjectAttributeBase
{
	public InjectLocalAttribute()
	{
		base.Source = InjectSources.Local;
	}
}
