using System.ComponentModel;
using System.Windows.Forms.Design;

namespace System.Windows.Forms.PropertyGridInternal;

/// <summary>Represents the Properties tab on a <see cref="T:System.Windows.Forms.PropertyGrid" /> control.</summary>
public class PropertiesTab : PropertyTab
{
	/// <summary>Gets the Help keyword that is to be associated with this tab.</summary>
	/// <returns>The string "vs.properties".</returns>
	public override string HelpKeyword => "vs.properties";

	/// <summary>Gets the name of the Properties tab.</summary>
	/// <returns>The string "Properties".</returns>
	public override string TabName => "Properties";

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PropertyGridInternal.PropertiesTab" /> class.</summary>
	public PropertiesTab()
	{
	}

	/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the properties.</returns>
	/// <param name="component">The component to retrieve properties from. </param>
	/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that indicates the attributes of the properties to retrieve. </param>
	public override PropertyDescriptorCollection GetProperties(object component, Attribute[] attributes)
	{
		return GetProperties(null, component, attributes);
	}

	/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the properties matching the specified context and attributes.</returns>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that indicates the context to retrieve properties from. </param>
	/// <param name="component">The component to retrieve properties from. </param>
	/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that indicates the attributes of the properties to retrieve. </param>
	public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object component, Attribute[] attributes)
	{
		if (component == null)
		{
			return new PropertyDescriptorCollection(null);
		}
		if (attributes == null)
		{
			attributes = new Attribute[1] { BrowsableAttribute.Yes };
		}
		PropertyDescriptorCollection propertyDescriptorCollection = null;
		TypeConverter converter = TypeDescriptor.GetConverter(component);
		if (converter != null && converter.GetPropertiesSupported())
		{
			propertyDescriptorCollection = converter.GetProperties(context, component, attributes);
		}
		if (propertyDescriptorCollection == null)
		{
			propertyDescriptorCollection = TypeDescriptor.GetProperties(component, attributes);
		}
		return propertyDescriptorCollection;
	}

	/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the default property.</returns>
	/// <param name="obj"></param>
	public override PropertyDescriptor GetDefaultProperty(object obj)
	{
		if (obj == null)
		{
			return null;
		}
		return TypeDescriptor.GetDefaultProperty(obj);
	}
}
