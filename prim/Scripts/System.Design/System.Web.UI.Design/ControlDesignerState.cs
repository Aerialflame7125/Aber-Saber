namespace System.Web.UI.Design;

/// <summary>Provides access to the state of the control designer in the design host through the <see cref="T:System.ComponentModel.Design.IComponentDesignerStateService" /> interface. This class cannot be inherited.</summary>
public sealed class ControlDesignerState
{
	/// <summary>Represents one element, identified by the given key, in the state collection for a control designer.</summary>
	/// <param name="key">The name of the item to set or retrieve from the state collection.</param>
	/// <returns>The object identified by <paramref name="key" />.</returns>
	[System.MonoNotSupported("")]
	public object this[string key]
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
		[System.MonoNotSupported("")]
		set
		{
			throw new NotImplementedException();
		}
	}

	internal ControlDesignerState()
	{
	}
}
