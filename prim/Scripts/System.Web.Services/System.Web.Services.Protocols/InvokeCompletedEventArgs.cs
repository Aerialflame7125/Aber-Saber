using System.ComponentModel;

namespace System.Web.Services.Protocols;

/// <summary>Represents the result of an asynchronously invoked web method.</summary>
public class InvokeCompletedEventArgs : AsyncCompletedEventArgs
{
	private object[] results;

	/// <summary>Gets the results returned by the Web method.</summary>
	/// <returns>An array of objects returned by the Web method.</returns>
	public object[] Results => results;

	internal InvokeCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
		: base(exception, cancelled, userState)
	{
		this.results = results;
	}
}
