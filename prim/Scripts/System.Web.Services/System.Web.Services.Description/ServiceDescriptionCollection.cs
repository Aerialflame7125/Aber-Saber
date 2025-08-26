using System.Xml;

namespace System.Web.Services.Description;

/// <summary>Represents a collection of instances of the <see cref="T:System.Web.Services.Description.ServiceDescription" /> class. This class cannot be inherited.</summary>
public sealed class ServiceDescriptionCollection : ServiceDescriptionBaseCollection
{
	/// <summary>Gets or sets the value of a <see cref="T:System.Web.Services.Description.ServiceDescription" /> at the specified zero-based index.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.Web.Services.Description.ServiceDescription" /> whose value is modified or returned.</param>
	/// <returns>The value of a <see cref="T:System.Web.Services.Description.ServiceDescription" /> at the specified index.</returns>
	public ServiceDescription this[int index]
	{
		get
		{
			return (ServiceDescription)base.List[index];
		}
		set
		{
			base.List[index] = value;
		}
	}

	/// <summary>Gets a <see cref="T:System.Web.Services.Description.ServiceDescription" /> specified by its <see cref="P:System.Web.Services.Description.ServiceDescription.TargetNamespace" /> property.</summary>
	/// <param name="ns">The namespace of the <see cref="T:System.Web.Services.Description.ServiceDescription" /> returned.</param>
	/// <returns>A <see cref="T:System.Web.Services.Description.ServiceDescription" /> specified by its namespace property.</returns>
	public ServiceDescription this[string ns] => (ServiceDescription)Table[ns];

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.ServiceDescriptionCollection" /> class.</summary>
	public ServiceDescriptionCollection()
		: base(null)
	{
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.ServiceDescription" /> to the end of the <see cref="T:System.Web.Services.Description.ServiceDescriptionCollection" />.</summary>
	/// <param name="serviceDescription">The <see cref="T:System.Web.Services.Description.ServiceDescription" /> to add to the collection.</param>
	/// <returns>The zero-based index where the <see cref="T:System.Web.Services.Description.ServiceDescription" /> parameter has been added.</returns>
	public int Add(ServiceDescription serviceDescription)
	{
		return base.List.Add(serviceDescription);
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.ServiceDescription" /> instance to the <see cref="T:System.Web.Services.Description.ServiceDescriptionCollection" /> at the specified zero-based index.</summary>
	/// <param name="index">The zero-based index at which to insert the <paramref name="serviceDescription" /> parameter.</param>
	/// <param name="serviceDescription">The <see cref="T:System.Web.Services.Description.ServiceDescription" /> to add to the collection.</param>
	/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="index" /> parameter is less than zero.- or - The <paramref name="index" /> parameter is greater than <see cref="P:System.Collections.CollectionBase.Count" />.</exception>
	public void Insert(int index, ServiceDescription serviceDescription)
	{
		base.List.Insert(index, serviceDescription);
	}

	/// <summary>Searches for the specified <see cref="T:System.Web.Services.Description.ServiceDescription" /> and returns the zero-based index of the first occurrence within the collection.</summary>
	/// <param name="serviceDescription">The <see cref="T:System.Web.Services.Description.ServiceDescription" /> for which to search in the collection.</param>
	/// <returns>The zero-based index of the specified service description, or -1 if the element was not found in the collection.</returns>
	public int IndexOf(ServiceDescription serviceDescription)
	{
		return base.List.IndexOf(serviceDescription);
	}

	/// <summary>Returns a value indicating whether the specified <see cref="T:System.Web.Services.Description.ServiceDescription" /> is a member of the collection.</summary>
	/// <param name="serviceDescription">The <see cref="T:System.Web.Services.Description.ServiceDescription" /> for which to check collection membership.</param>
	/// <returns>
	///     <see langword="true" /> if the <paramref name="serviceDescription" /> parameter is a member of the <see cref="T:System.Web.Services.Description.ServiceDescriptionCollection" />; otherwise, <see langword="false" />.</returns>
	public bool Contains(ServiceDescription serviceDescription)
	{
		return base.List.Contains(serviceDescription);
	}

	/// <summary>Removes the first occurrence of the specified <see cref="T:System.Web.Services.Description.ServiceDescription" /> from the collection.</summary>
	/// <param name="serviceDescription">The <see cref="T:System.Web.Services.Description.ServiceDescription" /> to remove from the collection.</param>
	public void Remove(ServiceDescription serviceDescription)
	{
		base.List.Remove(serviceDescription);
	}

	/// <summary>Copies the entire <see cref="T:System.Web.Services.Description.ServiceDescriptionCollection" /> to a one-dimensional array of type <see cref="T:System.Web.Services.Description.ServiceDescription" />, starting at the specified zero-based index of the target array.</summary>
	/// <param name="array">An array of type <see cref="T:System.Web.Services.Description.ServiceDescription" /> serving as the destination of the copy action.</param>
	/// <param name="index">The zero-based index at which to start placing the copied collection.</param>
	public void CopyTo(ServiceDescription[] array, int index)
	{
		base.List.CopyTo(array, index);
	}

	protected override string GetKey(object value)
	{
		string targetNamespace = ((ServiceDescription)value).TargetNamespace;
		if (targetNamespace == null)
		{
			return string.Empty;
		}
		return targetNamespace;
	}

	private Exception ItemNotFound(XmlQualifiedName name, string type)
	{
		return new Exception(Res.GetString("WebDescriptionMissingItem", type, name.Name, name.Namespace));
	}

	/// <summary>Searches the <see cref="T:System.Web.Services.Description.ServiceDescriptionCollection" /> and returns the <see cref="T:System.Web.Services.Description.Message" /> with the specified name that is a member of one of the <see cref="T:System.Web.Services.Description.ServiceDescription" /> instances contained in the collection.</summary>
	/// <param name="name">The <see cref="T:System.Xml.XmlQualifiedName" />, passed by reference, whose <see cref="P:System.Xml.XmlQualifiedName.Name" /> property is shared by the <see cref="T:System.Web.Services.Description.Message" /> returned.</param>
	/// <returns>The message with the specified name.</returns>
	/// <exception cref="T:System.Exception">The specified <see langword="Message" /> is not a member of any <see cref="T:System.Web.Services.Description.ServiceDescription" /> instances within the collection. </exception>
	public Message GetMessage(XmlQualifiedName name)
	{
		ServiceDescription serviceDescription = GetServiceDescription(name);
		Message message = null;
		while (message == null && serviceDescription != null)
		{
			message = serviceDescription.Messages[name.Name];
			serviceDescription = serviceDescription.Next;
		}
		if (message == null)
		{
			throw ItemNotFound(name, "message");
		}
		return message;
	}

	/// <summary>Searches the <see cref="T:System.Web.Services.Description.ServiceDescriptionCollection" /> and returns the <see cref="T:System.Web.Services.Description.PortType" /> with the specified name that is a member of one of the <see cref="T:System.Web.Services.Description.ServiceDescription" /> instances contained in the collection.</summary>
	/// <param name="name">The <see cref="T:System.Xml.XmlQualifiedName" />, passed by reference, whose <see cref="P:System.Xml.XmlQualifiedName.Name" /> property is shared by the <see cref="T:System.Web.Services.Description.PortType" /> returned.</param>
	/// <returns>The <see langword="PortType" /> with the specified name.</returns>
	/// <exception cref="T:System.Exception">The specified <see langword="PortType" /> is not a member of any <see cref="T:System.Web.Services.Description.ServiceDescription" /> instances within the collection.</exception>
	public PortType GetPortType(XmlQualifiedName name)
	{
		ServiceDescription serviceDescription = GetServiceDescription(name);
		PortType portType = null;
		while (portType == null && serviceDescription != null)
		{
			portType = serviceDescription.PortTypes[name.Name];
			serviceDescription = serviceDescription.Next;
		}
		if (portType == null)
		{
			throw ItemNotFound(name, "message");
		}
		return portType;
	}

	/// <summary>Searches the <see cref="T:System.Web.Services.Description.ServiceDescriptionCollection" /> and returns the <see cref="T:System.Web.Services.Description.Service" /> with the specified name that is a member of one of the <see cref="T:System.Web.Services.Description.ServiceDescription" /> instances contained in the collection.</summary>
	/// <param name="name">The <see cref="T:System.Xml.XmlQualifiedName" />, passed by reference, whose <see cref="P:System.Xml.XmlQualifiedName.Name" /> property is shared by the <see cref="T:System.Web.Services.Description.Service" /> returned.</param>
	/// <returns>The service with the specified name.</returns>
	/// <exception cref="T:System.Exception">The specified <see langword="Service" /> is not a member of any <see cref="T:System.Web.Services.Description.ServiceDescription" /> instances within the collection. </exception>
	public Service GetService(XmlQualifiedName name)
	{
		ServiceDescription serviceDescription = GetServiceDescription(name);
		Service service = null;
		while (service == null && serviceDescription != null)
		{
			service = serviceDescription.Services[name.Name];
			serviceDescription = serviceDescription.Next;
		}
		if (service == null)
		{
			throw ItemNotFound(name, "service");
		}
		return service;
	}

	/// <summary>Searches the <see cref="T:System.Web.Services.Description.ServiceDescriptionCollection" /> and returns the <see cref="T:System.Web.Services.Description.Binding" /> with the specified name that is a member of one of the <see cref="T:System.Web.Services.Description.ServiceDescription" /> instances contained in the collection.</summary>
	/// <param name="name">An <see cref="T:System.Xml.XmlQualifiedName" /> whose <see cref="P:System.Xml.XmlQualifiedName.Name" /> property is used to retrieve a <see cref="T:System.Web.Services.Description.Binding" /> instance.</param>
	/// <returns>The binding with the specified name.</returns>
	/// <exception cref="T:System.Exception">The specified <see langword="Binding" /> is not a member of any <see cref="T:System.Web.Services.Description.ServiceDescription" /> instances within the collection.</exception>
	public Binding GetBinding(XmlQualifiedName name)
	{
		ServiceDescription serviceDescription = GetServiceDescription(name);
		Binding binding = null;
		while (binding == null && serviceDescription != null)
		{
			binding = serviceDescription.Bindings[name.Name];
			serviceDescription = serviceDescription.Next;
		}
		if (binding == null)
		{
			throw ItemNotFound(name, "binding");
		}
		return binding;
	}

	private ServiceDescription GetServiceDescription(XmlQualifiedName name)
	{
		ServiceDescription serviceDescription = this[name.Namespace];
		if (serviceDescription == null)
		{
			throw new ArgumentException(Res.GetString("WebDescriptionMissing", name.ToString(), name.Namespace), "name");
		}
		return serviceDescription;
	}

	protected override void SetParent(object value, object parent)
	{
		((ServiceDescription)value).SetParent((ServiceDescriptionCollection)parent);
	}

	protected override void OnInsertComplete(int index, object value)
	{
		string key = GetKey(value);
		if (key != null)
		{
			_ = (ServiceDescription)Table[key];
			((ServiceDescription)value).Next = (ServiceDescription)Table[key];
			Table[key] = value;
		}
		SetParent(value, this);
	}
}
