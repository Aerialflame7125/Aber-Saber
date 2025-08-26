namespace System.ComponentModel.Design;

internal class DesignModeNestedContainer : NestedContainer
{
	private class Site : DesignModeSite, INestedSite, ISite, IServiceProvider
	{
		public string FullName
		{
			get
			{
				if (base.Name == null)
				{
					return null;
				}
				string ownerName = ((DesignModeNestedContainer)base.Container).OwnerName;
				if (ownerName == null)
				{
					return base.Name;
				}
				return ownerName + "." + base.Name;
			}
		}

		public Site(IComponent component, string name, IContainer container, IServiceProvider serviceProvider)
			: base(component, name, container, serviceProvider)
		{
		}
	}

	private string _containerName;

	protected override string OwnerName
	{
		get
		{
			if (_containerName != null)
			{
				return base.OwnerName + "." + _containerName;
			}
			return base.OwnerName;
		}
	}

	public DesignModeNestedContainer(IComponent owner, string containerName)
		: base(owner)
	{
		_containerName = containerName;
	}

	public override void Add(IComponent component, string name)
	{
		if (base.Owner.Site != null && base.Owner.Site.GetService(typeof(IDesignerHost)) is DesignerHost designerHost)
		{
			designerHost.AddPreProcess(component, name);
			base.Add(component, name);
			designerHost.AddPostProcess(component, name);
		}
	}

	public override void Remove(IComponent component)
	{
		if (base.Owner.Site != null && base.Owner.Site.GetService(typeof(IDesignerHost)) is DesignerHost designerHost)
		{
			designerHost.RemovePreProcess(component);
			base.Remove(component);
			designerHost.RemovePostProcess(component);
		}
	}

	protected override ISite CreateSite(IComponent component, string name)
	{
		if (component == null)
		{
			throw new ArgumentNullException("component");
		}
		if (base.Owner.Site == null)
		{
			throw new InvalidOperationException("Owner not sited.");
		}
		return new Site(component, name, this, base.Owner.Site);
	}

	protected override object GetService(Type service)
	{
		if (service == typeof(INestedContainer))
		{
			return this;
		}
		object obj = null;
		if (base.Owner.Site != null)
		{
			obj = base.Owner.Site.GetService(service);
		}
		if (obj == null)
		{
			return base.GetService(service);
		}
		return null;
	}
}
