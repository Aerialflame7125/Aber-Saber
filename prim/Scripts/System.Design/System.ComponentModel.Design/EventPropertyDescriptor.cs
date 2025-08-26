namespace System.ComponentModel.Design;

internal class EventPropertyDescriptor : PropertyDescriptor
{
	private EventDescriptor _eventDescriptor;

	public override Type ComponentType => _eventDescriptor.ComponentType;

	public override bool IsReadOnly => false;

	public override Type PropertyType => _eventDescriptor.EventType;

	public override TypeConverter Converter => TypeDescriptor.GetConverter(string.Empty);

	internal EventDescriptor InternalEventDescriptor => _eventDescriptor;

	public EventPropertyDescriptor(EventDescriptor eventDescriptor)
		: base(eventDescriptor)
	{
		if (eventDescriptor == null)
		{
			throw new ArgumentNullException("eventDescriptor");
		}
		_eventDescriptor = eventDescriptor;
	}

	public override bool CanResetValue(object component)
	{
		return true;
	}

	public override void ResetValue(object component)
	{
		SetValue(component, null);
	}

	public override object GetValue(object component)
	{
		if (component is IComponent && ((IComponent)component).Site != null && ((IComponent)component).Site.GetService(typeof(IDictionaryService)) is IDictionaryService dictionaryService)
		{
			return dictionaryService.GetValue(base.Name);
		}
		return null;
	}

	public override void SetValue(object component, object value)
	{
		if (component is IComponent && ((IComponent)component).Site != null && ((IComponent)component).Site.GetService(typeof(IDictionaryService)) is IDictionaryService dictionaryService)
		{
			dictionaryService.SetValue(base.Name, value);
		}
	}

	public override bool ShouldSerializeValue(object component)
	{
		if (GetValue(component) != null)
		{
			return true;
		}
		return false;
	}
}
