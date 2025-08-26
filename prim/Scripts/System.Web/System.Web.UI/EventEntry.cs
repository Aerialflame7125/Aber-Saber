namespace System.Web.UI;

/// <summary>Acts as the property entry for event handlers.</summary>
public class EventEntry
{
	private Type _handlerType;

	private string _handlerMethodName;

	private string _name;

	/// <summary>Gets or sets the name of the method that handles the event.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the event handler method name.</returns>
	public string HandlerMethodName
	{
		get
		{
			return _handlerMethodName;
		}
		set
		{
			_handlerMethodName = value;
		}
	}

	/// <summary>Gets or sets the type of delegate for the event.</summary>
	/// <returns>A <see cref="T:System.Type" /> that represents the type of delegate for the event.</returns>
	public Type HandlerType
	{
		get
		{
			return _handlerType;
		}
		set
		{
			_handlerType = value;
		}
	}

	/// <summary>Gets or sets the name of the property from the expression.</summary>
	/// <returns>A <see cref="T:System.String" /> that contains the name of the property.</returns>
	public string Name
	{
		get
		{
			return _name;
		}
		set
		{
			_name = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.EventEntry" /> class. </summary>
	public EventEntry()
	{
	}
}
