using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;

namespace System.Windows.Forms.Design;

/// <summary>Base designer class for extending the design mode behavior of a root design document that supports nested components.</summary>
public class ComponentDocumentDesigner : ComponentDesigner, IRootDesigner, IDesigner, IDisposable, IToolboxUser, ITypeDescriptorFilterService, IOleDragClient
{
	/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.Design.IRootDesigner.SupportedTechnologies" />.</summary>
	/// <returns>An array of supported <see cref="T:System.ComponentModel.Design.ViewTechnology" /> values.</returns>
	ViewTechnology[] IRootDesigner.SupportedTechnologies => new ViewTechnology[1] { ViewTechnology.WindowsForms };

	bool IOleDragClient.CanModifyComponents => true;

	[System.MonoTODO]
	IComponent IOleDragClient.Component
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the control for the designer.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Control" /> the designer is editing.</returns>
	[System.MonoTODO]
	public Control Control
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets a value indicating whether the component tray for the designer is in auto-arrange mode.</summary>
	/// <returns>
	///   <see langword="true" /> if the component tray for the designer is in auto-arrange mode; otherwise, <see langword="false" />.</returns>
	public bool TrayAutoArrange
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets a value indicating whether the component tray for the designer is in large icon mode.</summary>
	/// <returns>
	///   <see langword="true" /> if the component tray for the designer is in large icon mode; otherwise, <see langword="false" />.</returns>
	public bool TrayLargeIcon
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.ComponentDocumentDesigner" /> class.</summary>
	[System.MonoTODO]
	public ComponentDocumentDesigner()
	{
	}

	/// <summary>For a description of this member, see <see cref="T:System.ComponentModel.Design.ViewTechnology" />.</summary>
	/// <param name="technology">A <see cref="T:System.ComponentModel.Design.ViewTechnology" /> that indicates a particular view technology.</param>
	/// <returns>An object that represents the view for this designer.</returns>
	[System.MonoTODO]
	object IRootDesigner.GetView(ViewTechnology technology)
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.Drawing.Design.IToolboxUser.GetToolSupported(System.Drawing.Design.ToolboxItem)" />.</summary>
	/// <param name="tool">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to be tested for toolbox support.</param>
	/// <returns>
	///   <see langword="true" /> if the tool is supported by the toolbox and can be enabled; <see langword="false" /> if the document designer does not know how to use the tool.</returns>
	bool IToolboxUser.GetToolSupported(ToolboxItem tool)
	{
		return true;
	}

	/// <summary>For a description of this member, see <see cref="M:System.Drawing.Design.IToolboxUser.ToolPicked(System.Drawing.Design.ToolboxItem)" />.</summary>
	/// <param name="tool">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to select.</param>
	[System.MonoTODO]
	void IToolboxUser.ToolPicked(ToolboxItem tool)
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.Design.ITypeDescriptorFilterService.FilterAttributes(System.ComponentModel.IComponent,System.Collections.IDictionary)" />.</summary>
	/// <param name="component">The component to filter the attributes of.</param>
	/// <param name="attributes">A dictionary of attributes that can be modified.</param>
	/// <returns>
	///   <see langword="true" /> if the set of filtered attributes is to be cached; <see langword="false" /> if the filter service must query again.</returns>
	[System.MonoTODO]
	bool ITypeDescriptorFilterService.FilterAttributes(IComponent component, IDictionary attributes)
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.Design.ITypeDescriptorFilterService.FilterEvents(System.ComponentModel.IComponent,System.Collections.IDictionary)" />.</summary>
	/// <param name="component">The component to filter events for.</param>
	/// <param name="events">A dictionary of events that can be modified.</param>
	/// <returns>
	///   <see langword="true" /> if the set of filtered events is to be cached; <see langword="false" /> if the filter service must query again.</returns>
	[System.MonoTODO]
	bool ITypeDescriptorFilterService.FilterEvents(IComponent component, IDictionary events)
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.Design.ITypeDescriptorFilterService.FilterProperties(System.ComponentModel.IComponent,System.Collections.IDictionary)" />.</summary>
	/// <param name="component">The component to filter properties for.</param>
	/// <param name="properties">A dictionary of properties that can be modified.</param>
	/// <returns>
	///   <see langword="true" /> if the set of filtered properties is to be cached; <see langword="false" /> if the filter service must query again.</returns>
	[System.MonoTODO]
	bool ITypeDescriptorFilterService.FilterProperties(IComponent component, IDictionary properties)
	{
		throw new NotImplementedException();
	}

	[System.MonoTODO]
	bool IOleDragClient.AddComponent(IComponent component, string name, bool firstAdd)
	{
		throw new NotImplementedException();
	}

	[System.MonoTODO]
	Control IOleDragClient.GetControlForComponent(object component)
	{
		throw new NotImplementedException();
	}

	[System.MonoTODO]
	Control IOleDragClient.GetDesignerControl()
	{
		throw new NotImplementedException();
	}

	[System.MonoTODO]
	bool IOleDragClient.IsDropOk(IComponent component)
	{
		return true;
	}

	/// <summary>Initializes the designer with the specified component.</summary>
	/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to associate with the designer.</param>
	[System.MonoTODO]
	public override void Initialize(IComponent component)
	{
		throw new NotImplementedException();
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Design.ComponentDocumentDesigner" /> and optionally releases the managed resources.</summary>
	/// <param name="disposing">
	///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
	[System.MonoTODO]
	protected override void Dispose(bool disposing)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets a value indicating whether the specified tool is supported by the designer.</summary>
	/// <param name="tool">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to test for toolbox support.</param>
	/// <returns>
	///   <see langword="true" /> if the tool should be enabled on the toolbox; <see langword="false" /> if the document designer doesn't know how to use the tool.</returns>
	protected virtual bool GetToolSupported(ToolboxItem tool)
	{
		return true;
	}

	/// <summary>Adjusts the set of properties the component will expose through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
	/// <param name="properties">An <see cref="T:System.Collections.IDictionary" /> that contains the properties for the class of the component.</param>
	[System.MonoTODO]
	protected override void PreFilterProperties(IDictionary properties)
	{
		throw new NotImplementedException();
	}
}
