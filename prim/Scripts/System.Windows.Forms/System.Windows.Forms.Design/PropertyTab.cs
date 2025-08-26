using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms.Design;

/// <summary>Provides a base class for property tabs.</summary>
public abstract class PropertyTab : IExtenderProvider
{
	private Bitmap bitmap;

	private object[] components;

	/// <summary>Gets the bitmap that is displayed for the <see cref="T:System.Windows.Forms.Design.PropertyTab" />.</summary>
	/// <returns>The <see cref="T:System.Drawing.Bitmap" /> to display for the <see cref="T:System.Windows.Forms.Design.PropertyTab" />.</returns>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual Bitmap Bitmap
	{
		get
		{
			if (bitmap == null)
			{
				Type type = GetType();
				bitmap = new Bitmap(type, type.Name + ".bmp");
			}
			return bitmap;
		}
	}

	/// <summary>Gets or sets the array of components the property tab is associated with.</summary>
	/// <returns>The array of components the property tab is associated with.</returns>
	public virtual object[] Components
	{
		get
		{
			return components;
		}
		set
		{
			components = value;
		}
	}

	/// <summary>Gets the Help keyword that is to be associated with this tab.</summary>
	/// <returns>The Help keyword to be associated with this tab.</returns>
	public virtual string HelpKeyword => TabName;

	/// <summary>Gets the name for the property tab.</summary>
	/// <returns>The name for the property tab.</returns>
	public abstract string TabName { get; }

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.PropertyTab" /> class.</summary>
	protected PropertyTab()
	{
	}

	/// <summary>Allows a <see cref="T:System.Windows.Forms.Design.PropertyTab" /> to attempt to free resources and perform other cleanup operations before the <see cref="T:System.Windows.Forms.Design.PropertyTab" /> is reclaimed by garbage collection.</summary>
	~PropertyTab()
	{
		Dispose(disposing: false);
	}

	/// <summary>Gets a value indicating whether this <see cref="T:System.Windows.Forms.Design.PropertyTab" /> can display properties for the specified component.</summary>
	/// <returns>true if the object can be extended; otherwise, false.</returns>
	/// <param name="extendee">The object to test. </param>
	public virtual bool CanExtend(object extendee)
	{
		return true;
	}

	/// <summary>Releases all the resources used by the <see cref="T:System.Windows.Forms.Design.PropertyTab" />.</summary>
	public virtual void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Design.PropertyTab" /> and optionally releases the managed resources.</summary>
	/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
	protected virtual void Dispose(bool disposing)
	{
		if (disposing && bitmap != null)
		{
			bitmap.Dispose();
			bitmap = null;
		}
	}

	/// <summary>Gets the default property of the specified component.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the default property.</returns>
	/// <param name="component">The component to retrieve the default property of. </param>
	public virtual PropertyDescriptor GetDefaultProperty(object component)
	{
		return TypeDescriptor.GetDefaultProperty(component);
	}

	/// <summary>Gets the properties of the specified component.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the properties of the component.</returns>
	/// <param name="component">The component to retrieve the properties of. </param>
	public virtual PropertyDescriptorCollection GetProperties(object component)
	{
		return GetProperties(component, null);
	}

	/// <summary>Gets the properties of the specified component that match the specified attributes.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the properties.</returns>
	/// <param name="component">The component to retrieve properties from. </param>
	/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that indicates the attributes of the properties to retrieve. </param>
	public abstract PropertyDescriptorCollection GetProperties(object component, Attribute[] attributes);

	/// <summary>Gets the properties of the specified component that match the specified attributes and context.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the properties matching the specified context and attributes.</returns>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that indicates the context to retrieve properties from. </param>
	/// <param name="component">The component to retrieve properties from. </param>
	/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that indicates the attributes of the properties to retrieve. </param>
	public virtual PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object component, Attribute[] attributes)
	{
		return GetProperties(component, attributes);
	}
}
