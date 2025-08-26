using System.Collections;

namespace System.Web.UI.Design;

/// <summary>Represents an object in a design host such as Visual Studio 2005. This class must be inherited.</summary>
public abstract class DesignerObject : IServiceProvider
{
	/// <summary>Gets the associated designer component.</summary>
	/// <returns>A <see cref="T:System.Web.UI.Design.ControlDesigner" /> object.</returns>
	[System.MonoNotSupported("")]
	public ControlDesigner Designer
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the name of the object.</summary>
	/// <returns>The name of the object.</returns>
	[System.MonoNotSupported("")]
	public string Name
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the object's properties.</summary>
	/// <returns>An <see cref="T:System.Collections.IDictionary" /> object containing the object's properties and their values.</returns>
	[System.MonoNotSupported("")]
	public IDictionary Properties
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.DesignerObject" /> class.</summary>
	/// <param name="designer">The parent designer.</param>
	/// <param name="name">The name of the object.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="designer" /> is <see langword="null" />.  
	/// -or-  
	/// <paramref name="name" /> is <see langword="null" />.</exception>
	[System.MonoNotSupported("")]
	protected DesignerObject(ControlDesigner designer, string name)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets a service from the design host, as identified by the provided type.</summary>
	/// <param name="serviceType">The type of service being requested.</param>
	/// <returns>The requested service.</returns>
	[System.MonoNotSupported("")]
	protected object GetService(Type serviceType)
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.IServiceProvider.GetService(System.Type)" />.</summary>
	/// <param name="serviceType">The type of service being requested.</param>
	/// <returns>The requested service.</returns>
	[System.MonoNotSupported("")]
	object IServiceProvider.GetService(Type serviceType)
	{
		throw new NotImplementedException();
	}
}
