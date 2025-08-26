namespace System.ComponentModel.Design;

/// <summary>Manages the user interface (UI) for a smart tag panel. This class cannot be inherited.</summary>
public sealed class DesignerActionUIService : IDisposable
{
	/// <summary>Occurs when a request is made to show or hide a smart tag panel.</summary>
	public event DesignerActionUIStateChangeEventHandler DesignerActionUIStateChange;

	internal DesignerActionUIService()
	{
	}

	/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Design.DesignerActionUIService" /> class.</summary>
	[System.MonoTODO]
	public void Dispose()
	{
	}

	/// <summary>Displays the smart tag panel for a component.</summary>
	/// <param name="component">The component whose smart tag panel should be displayed.</param>
	[System.MonoTODO]
	public void ShowUI(IComponent component)
	{
		throw new NotImplementedException();
	}

	/// <summary>Hides the smart tag panel for a component.</summary>
	/// <param name="component">The component whose smart tag panel should be hidden.</param>
	[System.MonoTODO]
	public void HideUI(IComponent component)
	{
		throw new NotImplementedException();
	}

	/// <summary>Updates the smart tag panel.</summary>
	/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to refresh.</param>
	[System.MonoTODO]
	public void Refresh(IComponent component)
	{
		throw new NotImplementedException();
	}

	/// <summary>Indicates whether to automatically show the smart tag panel.</summary>
	/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to automatically show.</param>
	/// <returns>
	///   <see langword="true" /> to automatically show the smart tag panel; otherwise, <see langword="false" />.</returns>
	[System.MonoTODO]
	public bool ShouldAutoShow(IComponent component)
	{
		throw new NotImplementedException();
	}
}
