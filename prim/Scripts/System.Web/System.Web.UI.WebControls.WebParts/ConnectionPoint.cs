using System.Reflection;

namespace System.Web.UI.WebControls.WebParts;

/// <summary>Serves as the base class for defining connection point objects that enable the consumer control and the provider control in a Web Parts connection to share data.</summary>
public abstract class ConnectionPoint
{
	private bool allowMultiConn;

	private string name = string.Empty;

	private string id = "default";

	private Type interfaceType;

	private Type controlType;

	private MethodInfo callBackMethod;

	/// <summary>Represents a string used to identify the default connection point within a collection of connection points associated with a server control. </summary>
	public const string DefaultID = "default";

	internal MethodInfo CallbackMethod => callBackMethod;

	/// <summary>Gets a value that indicates whether a connection point supports multiple simultaneous connections.</summary>
	/// <returns>
	///     <see langword="true" /> if the connection point supports multiple connections; otherwise, <see langword="false" />. </returns>
	public bool AllowsMultipleConnections => allowMultiConn;

	/// <summary>Gets the <see cref="T:System.Type" /> of the server control with which a connection point is associated.</summary>
	/// <returns>A <see cref="T:System.Type" /> representing the control type.</returns>
	public Type ControlType => controlType;

	/// <summary>Gets a string that contains the identifier for a connection point.</summary>
	/// <returns>A string that contains the identifier for a connection point.</returns>
	public string ID => id;

	/// <summary>Gets the type of the interface used by a connection point.</summary>
	/// <returns>A <see cref="T:System.Type" /> that corresponds to the interface type provided or consumed by a control.</returns>
	public Type InterfaceType => interfaceType;

	public string Name => name;

	internal ConnectionPoint(MethodInfo callBack, Type interFace, Type control, string name, string id, bool allowsMultiConnections)
	{
		allowMultiConn = allowsMultiConnections;
		interfaceType = interFace;
		controlType = control;
		this.name = name;
		this.id = id;
		callBackMethod = callBack;
	}

	/// <summary>Returns a value that indicates whether a connection point can participate in connections. </summary>
	/// <param name="control">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or other server control that is associated with a connection point.</param>
	/// <returns>
	///     <see langword="true" /> if the control can create a connection point to participate in a connection; otherwise, <see langword="false" />.</returns>
	[MonoTODO("Not implemented")]
	public virtual bool GetEnabled(Control control)
	{
		throw new NotImplementedException();
	}
}
