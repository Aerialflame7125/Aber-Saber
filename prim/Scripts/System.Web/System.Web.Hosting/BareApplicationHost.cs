using System.Collections.Generic;

namespace System.Web.Hosting;

internal sealed class BareApplicationHost : MarshalByRefObject
{
	private string vpath;

	private string phys_path;

	private Dictionary<Type, RegisteredItem> hash;

	internal ApplicationManager Manager;

	internal string AppID;

	public string VirtualPath => vpath;

	public string PhysicalPath => phys_path;

	public AppDomain Domain => AppDomain.CurrentDomain;

	public BareApplicationHost()
	{
		Init();
	}

	private void Init()
	{
		hash = new Dictionary<Type, RegisteredItem>();
		HostingEnvironment.Host = this;
		AppDomain currentDomain = AppDomain.CurrentDomain;
		currentDomain.DomainUnload += OnDomainUnload;
		phys_path = (string)currentDomain.GetData(".appPath");
		vpath = (string)currentDomain.GetData(".appVPath");
	}

	public void Shutdown()
	{
		HostingEnvironment.InitiateShutdown();
	}

	public void StopObject(Type type)
	{
		if (hash.ContainsKey(type))
		{
			hash[type].Item.Stop(immediate: false);
		}
	}

	public IRegisteredObject CreateInstance(Type type)
	{
		return (IRegisteredObject)Activator.CreateInstance(type, null);
	}

	public void RegisterObject(IRegisteredObject obj, bool auto_clean)
	{
		hash[obj.GetType()] = new RegisteredItem(obj, auto_clean);
	}

	public bool UnregisterObject(IRegisteredObject obj)
	{
		return hash.Remove(obj.GetType());
	}

	public IRegisteredObject GetObject(Type type)
	{
		if (hash.ContainsKey(type))
		{
			return hash[type].Item;
		}
		return null;
	}

	public string GetCodeGenDir()
	{
		return AppDomain.CurrentDomain.SetupInformation.DynamicBase;
	}

	private void OnDomainUnload(object sender, EventArgs args)
	{
		Manager.RemoveHost(AppID);
		Dictionary<Type, RegisteredItem>.ValueCollection values = hash.Values;
		RegisteredItem[] array = new RegisteredItem[hash.Count];
		((ICollection<RegisteredItem>)values).CopyTo(array, 0);
		RegisteredItem[] array2 = array;
		foreach (RegisteredItem registeredItem in array2)
		{
			try
			{
				registeredItem.Item.Stop(immediate: true);
			}
			catch
			{
			}
		}
		hash.Clear();
	}
}
