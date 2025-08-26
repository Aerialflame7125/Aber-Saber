using System.ComponentModel;
using System.ComponentModel.Design;

namespace System.Windows.Forms.Design;

/// <summary>Provides a <see cref="T:System.Windows.Forms.Design.PropertyTab" /> that can display events for selection and linking.</summary>
public class EventsTab : PropertyTab
{
	private IServiceProvider serviceProvider;

	/// <summary>Gets the Help keyword for the tab.</summary>
	/// <returns>The Help keyword for the tab.</returns>
	public override string HelpKeyword => TabName;

	/// <summary>Gets the name of the tab.</summary>
	/// <returns>The name of the tab.</returns>
	public override string TabName => Locale.GetText("Events");

	private EventsTab()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.EventsTab" /> class.</summary>
	/// <param name="sp">An <see cref="T:System.IServiceProvider" /> to use. </param>
	public EventsTab(IServiceProvider sp)
	{
		serviceProvider = sp;
	}

	/// <summary>Gets all the properties of the event tab that match the specified attributes and context.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the properties. This will be an empty <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> if the component does not implement an event service.</returns>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain context information. </param>
	/// <param name="component">The component to retrieve the properties of. </param>
	/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that indicates the attributes of the event properties to retrieve. </param>
	public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object component, Attribute[] attributes)
	{
		IEventBindingService eventBindingService = null;
		EventDescriptorCollection eventDescriptorCollection = null;
		if (serviceProvider != null)
		{
			eventBindingService = (IEventBindingService)serviceProvider.GetService(typeof(IEventBindingService));
		}
		if (eventBindingService == null)
		{
			return new PropertyDescriptorCollection(null);
		}
		eventDescriptorCollection = ((attributes == null) ? TypeDescriptor.GetEvents(component) : TypeDescriptor.GetEvents(component, attributes));
		return eventBindingService.GetEventProperties(eventDescriptorCollection);
	}

	/// <summary>Gets all the properties of the event tab that match the specified attributes.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the properties. This will be an empty <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> if the component does not implement an event service.</returns>
	/// <param name="component">The component to retrieve the properties of. </param>
	/// <param name="attributes">An array of <see cref="T:System.Attribute" /> that indicates the attributes of the event properties to retrieve. </param>
	public override PropertyDescriptorCollection GetProperties(object component, Attribute[] attributes)
	{
		return GetProperties(null, component, attributes);
	}

	/// <summary>Gets a value indicating whether the specified object can be extended.</summary>
	/// <returns>true if the specified object can be extended; otherwise, false.</returns>
	/// <param name="extendee">The object to test for extensibility. </param>
	public override bool CanExtend(object extendee)
	{
		return false;
	}

	/// <summary>Gets the default property from the specified object.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> indicating the default property.</returns>
	/// <param name="obj">The object to retrieve the default property of. </param>
	public override PropertyDescriptor GetDefaultProperty(object obj)
	{
		if (serviceProvider == null)
		{
			return null;
		}
		EventDescriptor defaultEvent = TypeDescriptor.GetDefaultEvent(obj);
		IEventBindingService eventBindingService = (IEventBindingService)serviceProvider.GetService(typeof(IEventBindingService));
		if (defaultEvent != null && eventBindingService != null)
		{
			return eventBindingService.GetEventProperty(defaultEvent);
		}
		return null;
	}
}
