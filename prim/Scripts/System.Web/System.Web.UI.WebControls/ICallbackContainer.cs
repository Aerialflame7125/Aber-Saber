namespace System.Web.UI.WebControls;

/// <summary>Defines a method that enables controls to obtain a callback script.</summary>
public interface ICallbackContainer
{
	/// <summary>Creates a script for initiating a client callback to a Web server.</summary>
	/// <param name="buttonControl">The control initiating the callback request.</param>
	/// <param name="argument">The arguments used to build the callback script.</param>
	/// <returns>A script that, when run on a client, will initiate a callback to the Web server.</returns>
	string GetCallbackScript(IButtonControl buttonControl, string argument);
}
