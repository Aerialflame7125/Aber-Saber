using System.Collections;
using System.ComponentModel.Design;

namespace System.Messaging.Design;

/// <summary>Provides basic design-time functionality for the <see cref="T:System.Messaging.Message" /> class.</summary>
public class MessageDesigner : ComponentDesigner
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Messaging.Design.MessageDesigner" /> class.</summary>
	public MessageDesigner()
	{
	}

	/// <summary>Modifies the set of properties that the designer exposes through the <see cref="T:System.ComponentModel.TypeDescriptor" /> class.</summary>
	/// <param name="properties">A <see cref="T:System.Collections.IDictionary" /> that contains the set of properties to filter for the component.</param>
	[System.MonoTODO]
	protected override void PreFilterProperties(IDictionary properties)
	{
		throw new NotImplementedException();
	}
}
