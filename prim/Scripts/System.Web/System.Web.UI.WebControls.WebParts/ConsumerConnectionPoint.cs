using System.Reflection;

namespace System.Web.UI.WebControls.WebParts;

/// <summary>Defines a connection point object that enables a server control acting as a consumer to form a connection with a provider.</summary>
public class ConsumerConnectionPoint : ConnectionPoint
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint" /> class.</summary>
	/// <param name="callbackMethod">The method in the consumer control that returns an interface instance to consumers to establish a connection.</param>
	/// <param name="interfaceType">The <see cref="T:System.Type" /> of the interface that the consumer receives from a provider. </param>
	/// <param name="controlType">The <see cref="T:System.Type" /> of the consumer control with which the consumer connection point is associated.</param>
	/// <param name="displayName">A friendly display name for the consumer connection point that appears to users in the connection user interface (UI).</param>
	/// <param name="id">A unique identifier for the consumer connection point.</param>
	/// <param name="allowsMultipleConnections">A Boolean value indicating whether the consumer connection point can have multiple simultaneous connections with providers.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="callbackMethod" /> is <see langword="null" />.- or -
	///         <paramref name="interfaceType" /> is <see langword="null" />. - or -
	///         <paramref name="controlType" /> is <see langword="null" />.- or - 
	///         <paramref name="displayName" /> is <see langword="null" /> or an empty string ("").</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="controlType " />is not the same type as the consumer control (or a valid class derived from it).</exception>
	public ConsumerConnectionPoint(MethodInfo callbackMethod, Type interfaceType, Type controlType, string displayName, string id, bool allowsMultipleConnections)
		: base(callbackMethod, interfaceType, controlType, displayName, id, allowsMultipleConnections)
	{
	}

	/// <summary>Invokes the callback method in a consumer control and retrieves the interface instance from a provider control.</summary>
	/// <param name="control">The consumer control associated with a consumer connection point.</param>
	/// <param name="data">The interface instance returned from a provider control.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="control" /> is <see langword="null" />.</exception>
	[MonoTODO("Not implemented")]
	public virtual void SetObject(Control control, object data)
	{
		throw new NotImplementedException();
	}

	[MonoTODO("Not implemented")]
	public virtual bool SupportsConnection(Control control, TypeCollection interfaces)
	{
		throw new NotImplementedException();
	}
}
