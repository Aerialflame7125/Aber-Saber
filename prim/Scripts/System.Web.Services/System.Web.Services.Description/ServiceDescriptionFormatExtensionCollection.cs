using System.Collections;
using System.Xml;

namespace System.Web.Services.Description;

/// <summary>Represents the collection of extensibility elements used by the XML Web service. This class cannot be inherited.</summary>
public sealed class ServiceDescriptionFormatExtensionCollection : ServiceDescriptionBaseCollection
{
	private ArrayList handledElements;

	/// <summary>Gets or sets the value of a member of the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" />.</summary>
	/// <param name="index">The zero-based index of the member whose value is modified or returned.</param>
	/// <returns>The value of the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" />.</returns>
	public object this[int index]
	{
		get
		{
			return base.List[index];
		}
		set
		{
			base.List[index] = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> class.</summary>
	/// <param name="parent">The object of which this collection is a member.</param>
	public ServiceDescriptionFormatExtensionCollection(object parent)
		: base(parent)
	{
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" /> to the end of the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" />.</summary>
	/// <param name="extension">The <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" />, passed by reference, to add to the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" />.</param>
	/// <returns>The zero-based index where the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" /> has been added.</returns>
	public int Add(object extension)
	{
		return base.List.Add(extension);
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" /> to the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> at the specified zero-based index.</summary>
	/// <param name="index">The zero-based index at which to insert the <paramref name="extension" /> parameter.</param>
	/// <param name="extension">The <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" /> to add to the collection.</param>
	/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="index" /> parameter is less than zero.- or - The <paramref name="index" /> parameter is greater than <see cref="P:System.Collections.CollectionBase.Count" />. </exception>
	public void Insert(int index, object extension)
	{
		base.List.Insert(index, extension);
	}

	/// <summary>Searches for the specified <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" /> and returns the zero-based index of the first instance with the collection.</summary>
	/// <param name="extension">The <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" /> for which to search in the collection.</param>
	/// <returns>The zero-based index of the specified <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" />, or -1 if the element was not found in the collection.</returns>
	public int IndexOf(object extension)
	{
		return base.List.IndexOf(extension);
	}

	/// <summary>Returns a value indicating whether the specified <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" /> is a member of the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" />.</summary>
	/// <param name="extension">The <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" /> for which to check collection membership.</param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" /> is a member of the collection; otherwise, <see langword="false" />.</returns>
	public bool Contains(object extension)
	{
		return base.List.Contains(extension);
	}

	/// <summary>Removes the first occurrence of the specified <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" /> from the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" />.</summary>
	/// <param name="extension">The <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" /> to remove from the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" />.</param>
	public void Remove(object extension)
	{
		base.List.Remove(extension);
	}

	/// <summary>Copies the entire <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> into a one-dimensional array of type <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" />, starting at the specified zero-based index of the target array.</summary>
	/// <param name="array">An array of type <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" /> serving as the destination of the copy action.</param>
	/// <param name="index">The zero-based index at which to start placing the copied collection.</param>
	public void CopyTo(object[] array, int index)
	{
		base.List.CopyTo(array, index);
	}

	/// <summary>Searches the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> and returns the first element of the specified derived <see cref="T:System.Type" />.</summary>
	/// <param name="type">A <see cref="T:System.Type" /> for which to search the collection.</param>
	/// <returns>If the search is successful, an object of the specified <see cref="T:System.Type" />; otherwise, <see langword="null" />.</returns>
	public object Find(Type type)
	{
		for (int i = 0; i < base.List.Count; i++)
		{
			object obj = base.List[i];
			if (type.IsAssignableFrom(obj.GetType()))
			{
				((ServiceDescriptionFormatExtension)obj).Handled = true;
				return obj;
			}
		}
		return null;
	}

	/// <summary>Searches the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> and returns an array of all elements of the specified <see cref="T:System.Type" />.</summary>
	/// <param name="type">A <see cref="T:System.Type" /> for which to search the collection.</param>
	/// <returns>An array of <see cref="T:System.Object" /> instances representing all collection members of the specified type.</returns>
	public object[] FindAll(Type type)
	{
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < base.List.Count; i++)
		{
			object obj = base.List[i];
			if (type.IsAssignableFrom(obj.GetType()))
			{
				((ServiceDescriptionFormatExtension)obj).Handled = true;
				arrayList.Add(obj);
			}
		}
		return (object[])arrayList.ToArray(type);
	}

	/// <summary>Searches the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> for a member with the specified name and namespace URI.</summary>
	/// <param name="name">The name of the <see cref="T:System.Xml.XmlElement" /> to be found.</param>
	/// <param name="ns">The XML namespace URI of the <see cref="T:System.Xml.XmlElement" /> to be found.</param>
	/// <returns>If the search is successful, an <see cref="T:System.Xml.XmlElement" />; otherwise, <see langword="null" />.</returns>
	public XmlElement Find(string name, string ns)
	{
		for (int i = 0; i < base.List.Count; i++)
		{
			if (base.List[i] is XmlElement xmlElement && xmlElement.LocalName == name && xmlElement.NamespaceURI == ns)
			{
				SetHandled(xmlElement);
				return xmlElement;
			}
		}
		return null;
	}

	/// <summary>Searches the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> and returns an array of all members with the specified name and namespace URI.</summary>
	/// <param name="name">The XML name attribute of the <see cref="T:System.Xml.XmlElement" /> objects to be found.</param>
	/// <param name="ns">The XML namespace URI attribute of the <see cref="T:System.Xml.XmlElement" /> objects to be found.</param>
	/// <returns>An array of <see cref="T:System.Xml.XmlElement" /> instances.</returns>
	public XmlElement[] FindAll(string name, string ns)
	{
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < base.List.Count; i++)
		{
			if (base.List[i] is XmlElement xmlElement && xmlElement.LocalName == name && xmlElement.NamespaceURI == ns)
			{
				SetHandled(xmlElement);
				arrayList.Add(xmlElement);
			}
		}
		return (XmlElement[])arrayList.ToArray(typeof(XmlElement));
	}

	private void SetHandled(XmlElement element)
	{
		if (handledElements == null)
		{
			handledElements = new ArrayList();
		}
		if (!handledElements.Contains(element))
		{
			handledElements.Add(element);
		}
	}

	/// <summary>Returns a value indicating whether the specified object is used by the import process when the extensibility element is imported into the XML Web service.</summary>
	/// <param name="item">An object, either of type <see cref="T:System.Xml.XmlElement" /> or <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" /> to check for use by the import process.</param>
	/// <returns>
	///     <see langword="true" /> if the <paramref name="item" /> parameter is used; otherwise, <see langword="false" />.</returns>
	public bool IsHandled(object item)
	{
		if (item is XmlElement)
		{
			return IsHandled((XmlElement)item);
		}
		return ((ServiceDescriptionFormatExtension)item).Handled;
	}

	/// <summary>Returns a value indicating whether the specified object is necessary for the operation of the XML Web service.</summary>
	/// <param name="item">An object, either of type <see cref="T:System.Xml.XmlElement" /> or <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" />, to check whether it is necessary.</param>
	/// <returns>
	///     <see langword="true" /> if the <paramref name="item" /> parameter is required; otherwise, <see langword="false" />.</returns>
	public bool IsRequired(object item)
	{
		if (item is XmlElement)
		{
			return IsRequired((XmlElement)item);
		}
		return ((ServiceDescriptionFormatExtension)item).Required;
	}

	private bool IsHandled(XmlElement element)
	{
		if (handledElements == null)
		{
			return false;
		}
		return handledElements.Contains(element);
	}

	private bool IsRequired(XmlElement element)
	{
		XmlAttribute xmlAttribute = element.Attributes["required", "http://schemas.xmlsoap.org/wsdl/"];
		if (xmlAttribute == null || xmlAttribute.Value == null)
		{
			xmlAttribute = element.Attributes["required"];
			if (xmlAttribute == null || xmlAttribute.Value == null)
			{
				return false;
			}
		}
		return XmlConvert.ToBoolean(xmlAttribute.Value);
	}

	protected override void SetParent(object value, object parent)
	{
		if (value is ServiceDescriptionFormatExtension)
		{
			((ServiceDescriptionFormatExtension)value).SetParent(parent);
		}
	}

	protected override void OnValidate(object value)
	{
		if (!(value is XmlElement) && !(value is ServiceDescriptionFormatExtension))
		{
			throw new ArgumentException(Res.GetString("OnlyXmlElementsOrTypesDerivingFromServiceDescriptionFormatExtension0"), "value");
		}
		base.OnValidate(value);
	}
}
