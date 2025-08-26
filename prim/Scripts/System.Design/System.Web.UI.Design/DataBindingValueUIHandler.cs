using System.Collections;
using System.ComponentModel;

namespace System.Web.UI.Design;

/// <summary>Provides a UI handler for data binding values.</summary>
public class DataBindingValueUIHandler
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.DataBindingValueUIHandler" /> class.</summary>
	public DataBindingValueUIHandler()
	{
	}

	/// <summary>Adds a data binding for the specified property and the specified value item list, if the current control has data bindings and the current object does not already have a binding.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object that can provide additional context information.</param>
	/// <param name="propDesc">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the property to add a data binding for.</param>
	/// <param name="valueUIItemList">An <see cref="T:System.Collections.ArrayList" /> of items that have data bindings.</param>
	[System.MonoTODO]
	public void OnGetUIValueItem(ITypeDescriptorContext context, PropertyDescriptor propDesc, ArrayList valueUIItemList)
	{
		throw new NotImplementedException();
	}
}
