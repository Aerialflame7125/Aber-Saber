using ModestTree;

namespace Zenject.Internal;

[NoReflectionBaking]
public class LookupId
{
	public IProvider Provider;

	public BindingId BindingId;

	public LookupId()
	{
	}

	public LookupId(IProvider provider, BindingId bindingId)
	{
		Assert.IsNotNull(provider);
		Assert.IsNotNull(bindingId);
		Provider = provider;
		BindingId = bindingId;
	}

	public override int GetHashCode()
	{
		int num = 17;
		num = num * 23 + Provider.GetHashCode();
		return num * 23 + BindingId.GetHashCode();
	}
}
