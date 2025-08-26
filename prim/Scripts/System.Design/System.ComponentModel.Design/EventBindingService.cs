using System.Collections;
using System.Collections.Generic;

namespace System.ComponentModel.Design;

/// <summary>A default implementation of the <see cref="T:System.ComponentModel.Design.IEventBindingService" /> interface.</summary>
public abstract class EventBindingService : IEventBindingService
{
	private IServiceProvider _provider;

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.EventBindingService" /> class.</summary>
	/// <param name="provider">The service provider from which <see cref="T:System.ComponentModel.Design.EventBindingService" /> will query for services.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="provider" /> is <see langword="null" />.</exception>
	protected EventBindingService(IServiceProvider provider)
	{
		if (provider == null)
		{
			throw new ArgumentNullException("provider");
		}
		_provider = provider;
	}

	/// <summary>Displays the user code for the specified method.</summary>
	/// <param name="component">The component to which the method is bound.</param>
	/// <param name="e">The <see cref="T:System.ComponentModel.EventDescriptor" /> for the event handler.</param>
	/// <param name="methodName">The name of the method for which to display code.</param>
	/// <returns>
	///   <see langword="true" /> if it is possible to display the code; otherwise, <see langword="false" />.</returns>
	protected abstract bool ShowCode(IComponent component, EventDescriptor e, string methodName);

	/// <summary>Displays the user code at the given line number.</summary>
	/// <param name="lineNumber">The line number to show.</param>
	/// <returns>
	///   <see langword="true" /> if it is possible to display the code; otherwise, <see langword="false" />.</returns>
	protected abstract bool ShowCode(int lineNumber);

	/// <summary>Displays user code.</summary>
	/// <returns>
	///   <see langword="true" /> if it is possible to display the code; otherwise, <see langword="false" />.</returns>
	protected abstract bool ShowCode();

	/// <summary>Creates a unique method name.</summary>
	/// <param name="component">The component for which the method name will be created.</param>
	/// <param name="e">The event to create a name for.</param>
	/// <returns>The unique method name.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="component" /> or <paramref name="e" /> is <see langword="null" />.</exception>
	protected abstract string CreateUniqueMethodName(IComponent component, EventDescriptor e);

	/// <summary>Returns a collection of names of compatible methods.</summary>
	/// <param name="e">The <see cref="T:System.ComponentModel.EventDescriptor" /> containing the compatible delegate.</param>
	/// <returns>A collection of strings that are names of compatible methods.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="e" /> is <see langword="null" />.</exception>
	protected abstract ICollection GetCompatibleMethods(EventDescriptor e);

	/// <summary>Provides a notification that a particular method is no longer being used by an event handler.</summary>
	/// <param name="component">The component to which the method is bound.</param>
	/// <param name="e">The <see cref="T:System.ComponentModel.EventDescriptor" /> for the event handler.</param>
	/// <param name="methodName">The name of the method to be freed.</param>
	protected virtual void FreeMethod(IComponent component, EventDescriptor e, string methodName)
	{
	}

	/// <summary>Provides a notification that a particular method is being used by an event handler.</summary>
	/// <param name="component">The component to which the method is bound.</param>
	/// <param name="e">The <see cref="T:System.ComponentModel.EventDescriptor" /> for the event handler.</param>
	/// <param name="methodName">The name of the method.</param>
	protected virtual void UseMethod(IComponent component, EventDescriptor e, string methodName)
	{
	}

	/// <summary>Validates that the provided method name is valid for the language or script being used.</summary>
	/// <param name="methodName">The method name to validate.</param>
	protected virtual void ValidateMethodName(string methodName)
	{
	}

	/// <summary>Gets the requested service from the service provider.</summary>
	/// <param name="serviceType">The type of service to retrieve.</param>
	/// <returns>A reference to the service specified by <paramref name="serviceType" />, or <see langword="null" /> if the requested service is not available.</returns>
	protected object GetService(Type serviceType)
	{
		if (_provider != null)
		{
			return _provider.GetService(serviceType);
		}
		return null;
	}

	/// <summary>Creates a unique name for an event-handler method for the specified component and event.</summary>
	/// <param name="component">The component instance the event is connected to.</param>
	/// <param name="e">The event to create a name for.</param>
	/// <returns>The recommended name for the event-handler method for this event.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="component" /> or <paramref name="e" /> is <see langword="null" />.</exception>
	string IEventBindingService.CreateUniqueMethodName(IComponent component, EventDescriptor eventDescriptor)
	{
		if (eventDescriptor == null)
		{
			throw new ArgumentNullException("eventDescriptor");
		}
		if (component == null)
		{
			throw new ArgumentNullException("component");
		}
		return CreateUniqueMethodName(component, eventDescriptor);
	}

	/// <summary>Gets a collection of event-handler methods that have a method signature compatible with the specified event.</summary>
	/// <param name="e">The event to get the compatible event-handler methods for.</param>
	/// <returns>A collection of strings that are names of compatible methods.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="e" /> is <see langword="null" />.</exception>
	ICollection IEventBindingService.GetCompatibleMethods(EventDescriptor eventDescriptor)
	{
		if (eventDescriptor == null)
		{
			throw new ArgumentNullException("eventDescriptor");
		}
		return GetCompatibleMethods(eventDescriptor);
	}

	/// <summary>Gets an <see cref="T:System.ComponentModel.EventDescriptor" /> for the event that the specified property descriptor represents, if it represents an event.</summary>
	/// <param name="property">The property that represents an event.</param>
	/// <returns>An <see cref="T:System.ComponentModel.EventDescriptor" /> for the event that the property represents, or <see langword="null" /> if the property does not represent an event.</returns>
	EventDescriptor IEventBindingService.GetEvent(PropertyDescriptor property)
	{
		if (property == null)
		{
			throw new ArgumentNullException("property");
		}
		if (!(property is EventPropertyDescriptor eventPropertyDescriptor))
		{
			return null;
		}
		return eventPropertyDescriptor.InternalEventDescriptor;
	}

	/// <summary>Converts a set of event descriptors to a set of property descriptors.</summary>
	/// <param name="events">The events to convert to properties.</param>
	/// <returns>An array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects that describe the event set.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="events" /> is <see langword="null" />.</exception>
	PropertyDescriptorCollection IEventBindingService.GetEventProperties(EventDescriptorCollection events)
	{
		if (events == null)
		{
			throw new ArgumentNullException("events");
		}
		List<PropertyDescriptor> list = new List<PropertyDescriptor>();
		foreach (EventDescriptor @event in events)
		{
			list.Add(((IEventBindingService)this).GetEventProperty(@event));
		}
		return new PropertyDescriptorCollection(list.ToArray());
	}

	/// <summary>Converts a single event descriptor to a property descriptor.</summary>
	/// <param name="e">The event to convert.</param>
	/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that describes the event.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="e" /> is <see langword="null" />.</exception>
	PropertyDescriptor IEventBindingService.GetEventProperty(EventDescriptor eventDescriptor)
	{
		if (eventDescriptor == null)
		{
			throw new ArgumentNullException("eventDescriptor");
		}
		return new EventPropertyDescriptor(eventDescriptor);
	}

	/// <summary>Displays the user code for the specified event.</summary>
	/// <param name="component">The component that the event is connected to.</param>
	/// <param name="e">The event to display.</param>
	/// <returns>
	///   <see langword="true" /> if the code is displayed; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="events" /> is <see langword="null" />.</exception>
	bool IEventBindingService.ShowCode(IComponent component, EventDescriptor eventDescriptor)
	{
		if (component == null)
		{
			throw new ArgumentNullException("component");
		}
		if (eventDescriptor == null)
		{
			throw new ArgumentNullException("eventDescriptor");
		}
		return ShowCode(component, eventDescriptor, (string)((IEventBindingService)this).GetEventProperty(eventDescriptor).GetValue(component));
	}

	/// <summary>Displays the user code for the designer at the specified line.</summary>
	/// <param name="lineNumber">The line number to place the caret on.</param>
	/// <returns>
	///   <see langword="true" /> if the code is displayed; otherwise, <see langword="false" />.</returns>
	bool IEventBindingService.ShowCode(int lineNumber)
	{
		return ShowCode(lineNumber);
	}

	/// <summary>Displays the user code for the designer.</summary>
	/// <returns>
	///   <see langword="true" /> if the code is displayed; otherwise, <see langword="false" />.</returns>
	bool IEventBindingService.ShowCode()
	{
		return ShowCode();
	}
}
