using System.Reflection;

namespace System.Web.UI.WebControls.WebParts;

/// <summary>Defines a connection point object that enables a server control acting as a provider to form a connection with a consumer.</summary>
public class ProviderConnectionPoint : ConnectionPoint
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPoint" /> class.</summary>
	/// <param name="callbackMethod">The method in the provider control that returns an interface instance to consumers to establish a connection.</param>
	/// <param name="interfaceType">The <see cref="T:System.Type" /> of the interface that the provider serves to consumers. </param>
	/// <param name="controlType">The <see cref="T:System.Type" /> of the provider control with which the provider connection point is associated.</param>
	/// <param name="displayName">A friendly display name for the provider connection point that appears to users in the connection user interface (UI).</param>
	/// <param name="id">A unique identifier for the provider connection point.</param>
	/// <param name="allowsMultipleConnections">A Boolean value indicating whether the provider connection point can have multiple simultaneous connections with consumers.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="callbackMethod" /> is <see langword="null" />.- or -
	///         <paramref name="interfaceType" /> is <see langword="null" />. - or -
	///         <paramref name="controlType" /> is <see langword="null" />.- or - 
	///         <paramref name="displayName" /> is <see langword="null" /> or an empty string ("").</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="controlType " />is not the same type as the provider control (or a valid class derived from it).</exception>
	public ProviderConnectionPoint(MethodInfo callbackMethod, Type interfaceType, Type controlType, string displayName, string id, bool allowsMultipleConnections)
		: base(callbackMethod, interfaceType, controlType, displayName, id, allowsMultipleConnections)
	{
	}

	/// <summary>Invokes the callback method in a provider control that gets an interface instance to return to consumers.</summary>
	/// <param name="control">The control acting as the provider in a Web Parts connection.</param>
	/// <returns>An <see cref="T:System.Object" /> that is an instance of the interface a provider returns to consumers to establish a connection. </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="control" /> is <see langword="null" />.</exception>
	[MonoTODO("Not implemented")]
	public virtual object GetObject(Control control)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets an optional collection of secondary interfaces that can be supported by a provider connection point.</summary>
	/// <param name="control">The control acting as the provider in a Web Parts connection.</param>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionInterfaceCollection" /> of additional interfaces provided by the control acting as a provider.</returns>
	[MonoTODO("Not implemented")]
	public virtual ConnectionInterfaceCollection GetSecondaryInterfaces(Control control)
	{
		throw new NotImplementedException();
	}
}
