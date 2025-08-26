using System.Collections;
using System.Windows.Forms;

namespace System.ComponentModel.Design;

internal class SelectionService : ISelectionService
{
	private IServiceProvider _serviceProvider;

	private ArrayList _selection;

	private IComponent _primarySelection;

	public object PrimarySelection => _primarySelection;

	public int SelectionCount
	{
		get
		{
			if (_selection != null)
			{
				return _selection.Count;
			}
			return 0;
		}
	}

	private IComponent RootComponent
	{
		get
		{
			if (_serviceProvider != null && _serviceProvider.GetService(typeof(IDesignerHost)) is IDesignerHost designerHost)
			{
				return designerHost.RootComponent;
			}
			return null;
		}
	}

	public event EventHandler SelectionChanging;

	public event EventHandler SelectionChanged;

	public SelectionService(IServiceProvider provider)
	{
		_serviceProvider = provider;
		_selection = new ArrayList();
		if (provider.GetService(typeof(IComponentChangeService)) is IComponentChangeService componentChangeService)
		{
			componentChangeService.ComponentRemoving += OnComponentRemoving;
		}
	}

	private void OnComponentRemoving(object sender, ComponentEventArgs args)
	{
		if (GetComponentSelected(args.Component))
		{
			SetSelectedComponents(new IComponent[1] { args.Component }, SelectionTypes.Remove);
		}
	}

	public ICollection GetSelectedComponents()
	{
		if (_selection != null)
		{
			return _selection.ToArray();
		}
		return new object[0];
	}

	protected virtual void OnSelectionChanging()
	{
		if (this.SelectionChanging != null)
		{
			this.SelectionChanging(this, EventArgs.Empty);
		}
	}

	protected virtual void OnSelectionChanged()
	{
		if (this.SelectionChanged != null)
		{
			this.SelectionChanged(this, EventArgs.Empty);
		}
	}

	public bool GetComponentSelected(object component)
	{
		if (_selection != null)
		{
			return _selection.Contains(component);
		}
		return false;
	}

	public void SetSelectedComponents(ICollection components)
	{
		SetSelectedComponents(components, SelectionTypes.Auto);
	}

	public void SetSelectedComponents(ICollection components, SelectionTypes selectionType)
	{
		bool flag4;
		bool flag3;
		bool flag2;
		bool flag;
		bool flag5 = (flag4 = (flag3 = (flag2 = (flag = false))));
		OnSelectionChanging();
		if (_selection == null)
		{
			throw new InvalidOperationException("_selection == null");
		}
		if (components == null || components.Count == 0)
		{
			components = new ArrayList();
			((ArrayList)components).Add(RootComponent);
			selectionType = SelectionTypes.Replace;
		}
		if (!Enum.IsDefined(typeof(SelectionTypes), selectionType))
		{
			selectionType = SelectionTypes.Auto;
		}
		if ((selectionType & SelectionTypes.Auto) == SelectionTypes.Auto)
		{
			if ((Control.ModifierKeys & Keys.Control) == Keys.Control || (Control.ModifierKeys & Keys.Shift) == Keys.Shift)
			{
				flag = true;
			}
			else if (components.Count == 1)
			{
				object component = null;
				{
					IEnumerator enumerator = components.GetEnumerator();
					try
					{
						if (enumerator.MoveNext())
						{
							component = enumerator.Current;
						}
					}
					finally
					{
						IDisposable disposable = enumerator as IDisposable;
						if (disposable != null)
						{
							disposable.Dispose();
						}
					}
				}
				if (GetComponentSelected(component))
				{
					flag5 = true;
				}
				else
				{
					flag2 = true;
				}
			}
			else
			{
				flag2 = true;
			}
		}
		else
		{
			flag5 = (selectionType & SelectionTypes.Click) == SelectionTypes.Click;
			flag4 = (selectionType & SelectionTypes.Add) == SelectionTypes.Add;
			flag3 = (selectionType & SelectionTypes.Remove) == SelectionTypes.Remove;
			flag = (selectionType & SelectionTypes.Toggle) == SelectionTypes.Toggle;
			flag2 = (selectionType & SelectionTypes.Replace) == SelectionTypes.Replace;
		}
		if (flag2)
		{
			_selection.Clear();
			flag4 = true;
		}
		if (flag4)
		{
			foreach (object component2 in components)
			{
				if (component2 is IComponent && !_selection.Contains(component2))
				{
					_selection.Add(component2);
					_primarySelection = (IComponent)component2;
				}
			}
		}
		if (flag3)
		{
			bool flag6 = false;
			foreach (object component3 in components)
			{
				if (component3 is IComponent && _selection.Contains(component3))
				{
					_selection.Remove(component3);
				}
				if (component3 == RootComponent)
				{
					flag6 = true;
				}
			}
			if (_selection.Count == 0)
			{
				if (flag6)
				{
					_primarySelection = null;
				}
				else
				{
					_primarySelection = RootComponent;
					_selection.Add(RootComponent);
				}
			}
		}
		if (flag)
		{
			foreach (object component4 in components)
			{
				if (!(component4 is IComponent))
				{
					continue;
				}
				if (_selection.Contains(component4))
				{
					_selection.Remove(component4);
					if (component4 == _primarySelection)
					{
						_primarySelection = RootComponent;
					}
				}
				else
				{
					_selection.Add(component4);
					_primarySelection = (IComponent)component4;
				}
			}
		}
		if (flag5)
		{
			object obj = null;
			{
				IEnumerator enumerator = components.GetEnumerator();
				try
				{
					if (enumerator.MoveNext())
					{
						obj = enumerator.Current;
					}
				}
				finally
				{
					IDisposable disposable2 = enumerator as IDisposable;
					if (disposable2 != null)
					{
						disposable2.Dispose();
					}
				}
			}
			if (!GetComponentSelected(obj))
			{
				_selection.Add(obj);
			}
			_primarySelection = (IComponent)obj;
		}
		OnSelectionChanged();
	}
}
