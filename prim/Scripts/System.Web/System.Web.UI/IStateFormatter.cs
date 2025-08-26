namespace System.Web.UI;

/// <summary>Defines methods that a type implements to serialize and deserialize an object graph. </summary>
public interface IStateFormatter
{
	/// <summary>Deserializes an object state graph from its serialized string form.</summary>
	/// <param name="serializedState">A string that the <see cref="T:System.Web.UI.IStateFormatter" /> deserializes into an initialized object.</param>
	/// <returns>An object that represents the state of an ASP.NET server control.</returns>
	object Deserialize(string serializedState);

	/// <summary>Serializes ASP.NET Web server control state to string form.</summary>
	/// <param name="state">The object that represents the view state of the Web server control to serialize to string form.</param>
	/// <returns>A string that represents a Web server control's view state. </returns>
	string Serialize(object state);
}
