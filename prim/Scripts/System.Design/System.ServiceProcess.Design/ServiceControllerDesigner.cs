using System.Collections;
using System.ComponentModel.Design;

namespace System.ServiceProcess.Design;

/// <summary>Provides design-time services for the <see cref="T:System.ServiceProcess.ServiceController" /> class.</summary>
public class ServiceControllerDesigner : ComponentDesigner
{
	/// <summary>Initializes a new instance of the <see cref="T:System.ServiceProcess.Design.ServiceControllerDesigner" /> class.</summary>
	public ServiceControllerDesigner()
	{
	}

	/// <summary>Adjusts the set of properties the <see cref="T:System.ServiceProcess.ServiceController" /> exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
	/// <param name="properties">An <see cref="T:System.Collections.IDictionary" /> containing the properties for the <see cref="T:System.ServiceProcess.ServiceController" /> class.</param>
	[System.MonoTODO]
	protected override void PreFilterProperties(IDictionary properties)
	{
		throw new NotImplementedException();
	}
}
