using System.CodeDom;

namespace System.ComponentModel.Design.Serialization;

internal class EventCodeDomSerializer : MemberCodeDomSerializer
{
	private CodeThisReferenceExpression _thisReference;

	public EventCodeDomSerializer()
	{
		_thisReference = new CodeThisReferenceExpression();
	}

	public override void Serialize(IDesignerSerializationManager manager, object value, MemberDescriptor descriptor, CodeStatementCollection statements)
	{
		if (statements == null)
		{
			throw new ArgumentNullException("statements");
		}
		if (manager == null)
		{
			throw new ArgumentNullException("manager");
		}
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		if (descriptor == null)
		{
			throw new ArgumentNullException("descriptor");
		}
		if (manager.GetService(typeof(IEventBindingService)) is IEventBindingService eventBindingService)
		{
			EventDescriptor eventDescriptor = (EventDescriptor)descriptor;
			string text = (string)eventBindingService.GetEventProperty(eventDescriptor).GetValue(value);
			if (text != null)
			{
				CodeDelegateCreateExpression listener = new CodeDelegateCreateExpression(new CodeTypeReference(eventDescriptor.EventType), _thisReference, text);
				CodeEventReferenceExpression eventRef = new CodeEventReferenceExpression(SerializeToExpression(manager, value), eventDescriptor.Name);
				statements.Add(new CodeAttachEventStatement(eventRef, listener));
			}
		}
	}

	public override bool ShouldSerialize(IDesignerSerializationManager manager, object value, MemberDescriptor descriptor)
	{
		if (manager.GetService(typeof(IEventBindingService)) is IEventBindingService eventBindingService)
		{
			return eventBindingService.GetEventProperty((EventDescriptor)descriptor).GetValue(value) != null;
		}
		return false;
	}
}
