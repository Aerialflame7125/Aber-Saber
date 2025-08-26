namespace System.Web.UI;

/// <summary>Defines the method ASP.NET server controls must implement to handle postback events.</summary>
public interface IPostBackEventHandler
{
	/// <summary>When implemented by a class, enables a server control to process an event raised when a form is posted to the server.</summary>
	/// <param name="eventArgument">A <see cref="T:System.String" /> that represents an optional event argument to be passed to the event handler. </param>
	void RaisePostBackEvent(string eventArgument);
}
