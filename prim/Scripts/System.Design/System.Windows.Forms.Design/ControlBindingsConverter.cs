using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms.Design;

internal class ControlBindingsConverter : TypeConverter
{
	[System.MonoTODO]
	private class DataBindingPropertyDescriptor : PropertyDescriptor
	{
		private bool _readOnly;

		[System.MonoTODO]
		public override Type PropertyType => typeof(DataBindingPropertyDescriptor);

		[System.MonoTODO]
		public override TypeConverter Converter => null;

		public override Type ComponentType => typeof(ControlBindingsCollection);

		public override bool IsReadOnly => _readOnly;

		[System.MonoTODO]
		public DataBindingPropertyDescriptor(PropertyDescriptor property, Attribute[] attrs, bool readOnly)
			: base(property.Name, attrs)
		{
			_readOnly = readOnly;
		}

		[System.MonoTODO]
		public override object GetValue(object component)
		{
			return null;
		}

		[System.MonoTODO]
		public override void SetValue(object component, object value)
		{
		}

		[System.MonoTODO]
		public override void ResetValue(object component)
		{
			throw new NotImplementedException();
		}

		[System.MonoTODO]
		public override bool CanResetValue(object component)
		{
			return false;
		}

		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}
	}

	[System.MonoTODO]
	public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
	{
		PropertyDescriptorCollection propertyDescriptorCollection = new PropertyDescriptorCollection(new PropertyDescriptor[0]);
		ControlBindingsCollection obj = value as ControlBindingsCollection;
		object bindableComponent = obj.BindableComponent;
		if (obj != null && bindableComponent != null)
		{
			foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(bindableComponent, attributes))
			{
				if (((BindableAttribute)property.Attributes[typeof(BindableAttribute)]).Bindable)
				{
					propertyDescriptorCollection.Add(new DataBindingPropertyDescriptor(property, attributes, readOnly: true));
				}
			}
		}
		return propertyDescriptorCollection;
	}

	public override bool GetPropertiesSupported(ITypeDescriptorContext context)
	{
		return true;
	}

	public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
	{
		if (destinationType == typeof(string))
		{
			return true;
		}
		return base.CanConvertTo(context, destinationType);
	}

	public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
	{
		if (destinationType == typeof(string))
		{
			return string.Empty;
		}
		return base.ConvertTo(context, culture, value, destinationType);
	}
}
