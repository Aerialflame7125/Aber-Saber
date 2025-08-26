using System.Collections;

namespace System.ComponentModel.Design;

internal sealed class ExtenderService : IExtenderProviderService, IExtenderListService, IDisposable
{
	private ArrayList _extenderProviders;

	public ExtenderService()
	{
		_extenderProviders = new ArrayList();
	}

	public void AddExtenderProvider(IExtenderProvider provider)
	{
		if (_extenderProviders != null && !_extenderProviders.Contains(provider))
		{
			_extenderProviders.Add(provider);
		}
	}

	public void RemoveExtenderProvider(IExtenderProvider provider)
	{
		if (_extenderProviders != null && _extenderProviders.Contains(provider))
		{
			_extenderProviders.Remove(provider);
		}
	}

	public IExtenderProvider[] GetExtenderProviders()
	{
		if (_extenderProviders != null)
		{
			IExtenderProvider[] array = new IExtenderProvider[_extenderProviders.Count];
			_extenderProviders.CopyTo(array, 0);
			return array;
		}
		return null;
	}

	public void Dispose()
	{
		if (_extenderProviders != null)
		{
			_extenderProviders.Clear();
			_extenderProviders = null;
		}
	}
}
