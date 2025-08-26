using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;

namespace System.Windows.Forms.Design;

internal sealed class DefaultMenuCommands
{
	private IServiceProvider _serviceProvider;

	private const string DT_DATA_FORMAT = "DT_DATA_FORMAT";

	private object _clipboard;

	public DefaultMenuCommands(IServiceProvider serviceProvider)
	{
		if (serviceProvider == null)
		{
			throw new ArgumentNullException("serviceProvider");
		}
		_serviceProvider = serviceProvider;
	}

	public void AddTo(IMenuCommandService commands)
	{
		commands.AddCommand(new MenuCommand(Copy, StandardCommands.Copy));
		commands.AddCommand(new MenuCommand(Cut, StandardCommands.Cut));
		commands.AddCommand(new MenuCommand(Paste, StandardCommands.Paste));
		commands.AddCommand(new MenuCommand(Delete, StandardCommands.Delete));
		commands.AddCommand(new MenuCommand(SelectAll, StandardCommands.SelectAll));
	}

	private void Copy(object sender, EventArgs args)
	{
		IDesignerSerializationService designerSerializationService = GetService(typeof(IDesignerSerializationService)) as IDesignerSerializationService;
		IDesignerHost designerHost = GetService(typeof(IDesignerHost)) as IDesignerHost;
		ISelectionService selectionService = GetService(typeof(ISelectionService)) as ISelectionService;
		if (designerHost == null || designerSerializationService == null || selectionService == null)
		{
			return;
		}
		ICollection selectedComponents = selectionService.GetSelectedComponents();
		ArrayList arrayList = new ArrayList();
		foreach (object item in selectedComponents)
		{
			if (item != designerHost.RootComponent)
			{
				arrayList.Add(item);
				if (designerHost.GetDesigner((IComponent)item) is ComponentDesigner { AssociatedComponents: not null } componentDesigner)
				{
					arrayList.AddRange(componentDesigner.AssociatedComponents);
				}
			}
		}
		object clipboard = designerSerializationService.Serialize(arrayList);
		_clipboard = clipboard;
	}

	private void Paste(object sender, EventArgs args)
	{
		IDesignerSerializationService designerSerializationService = GetService(typeof(IDesignerSerializationService)) as IDesignerSerializationService;
		ISelectionService selectionService = GetService(typeof(ISelectionService)) as ISelectionService;
		IDesignerHost designerHost = GetService(typeof(IDesignerHost)) as IDesignerHost;
		IComponentChangeService componentChangeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
		if (designerHost == null || designerSerializationService == null || _clipboard == null)
		{
			return;
		}
		DesignerTransaction designerTransaction = designerHost.CreateTransaction("Paste");
		foreach (object item in designerSerializationService.Deserialize(_clipboard))
		{
			if (!(item is Control control))
			{
				continue;
			}
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(control)["Parent"];
			if (control.Parent != null)
			{
				if (componentChangeService != null)
				{
					componentChangeService.OnComponentChanging(control, propertyDescriptor);
					componentChangeService.OnComponentChanged(control, propertyDescriptor, null, control.Parent);
				}
				continue;
			}
			ParentControlDesigner parentControlDesigner = null;
			if (selectionService != null && selectionService.PrimarySelection != null)
			{
				parentControlDesigner = designerHost.GetDesigner((IComponent)selectionService.PrimarySelection) as ParentControlDesigner;
			}
			if (parentControlDesigner == null)
			{
				parentControlDesigner = designerHost.GetDesigner(designerHost.RootComponent) as DocumentDesigner;
			}
			if (parentControlDesigner != null && parentControlDesigner.CanParent(control))
			{
				propertyDescriptor.SetValue(control, parentControlDesigner.Control);
			}
		}
		_clipboard = null;
		designerTransaction.Commit();
		((IDisposable)designerTransaction).Dispose();
	}

	private void Cut(object sender, EventArgs args)
	{
		if (!(GetService(typeof(IDesignerHost)) is IDesignerHost designerHost))
		{
			return;
		}
		using DesignerTransaction designerTransaction = designerHost.CreateTransaction("Cut");
		Copy(this, EventArgs.Empty);
		Delete(this, EventArgs.Empty);
		designerTransaction.Commit();
	}

	private void Delete(object sender, EventArgs args)
	{
		IDesignerHost designerHost = GetService(typeof(IDesignerHost)) as IDesignerHost;
		ISelectionService selectionService = GetService(typeof(ISelectionService)) as ISelectionService;
		if (designerHost == null || selectionService == null)
		{
			return;
		}
		ICollection selectedComponents = selectionService.GetSelectedComponents();
		string description = "Delete " + ((selectedComponents.Count > 1) ? (selectedComponents.Count + " controls") : ((IComponent)selectionService.PrimarySelection).Site.Name);
		DesignerTransaction designerTransaction = designerHost.CreateTransaction(description);
		foreach (object item in selectedComponents)
		{
			if (item == designerHost.RootComponent)
			{
				continue;
			}
			if (designerHost.GetDesigner((IComponent)item) is ComponentDesigner { AssociatedComponents: not null } componentDesigner)
			{
				foreach (object associatedComponent in componentDesigner.AssociatedComponents)
				{
					designerHost.DestroyComponent((IComponent)associatedComponent);
				}
			}
			designerHost.DestroyComponent((IComponent)item);
		}
		selectionService.SetSelectedComponents(selectedComponents, SelectionTypes.Remove);
		designerTransaction.Commit();
	}

	private void SelectAll(object sender, EventArgs args)
	{
		IDesignerHost designerHost = GetService(typeof(IDesignerHost)) as IDesignerHost;
		ISelectionService selectionService = GetService(typeof(ISelectionService)) as ISelectionService;
		if (designerHost != null)
		{
			selectionService?.SetSelectedComponents(designerHost.Container.Components, SelectionTypes.Replace);
		}
	}

	private object GetService(Type serviceType)
	{
		if (_serviceProvider != null)
		{
			return _serviceProvider.GetService(serviceType);
		}
		return null;
	}
}
