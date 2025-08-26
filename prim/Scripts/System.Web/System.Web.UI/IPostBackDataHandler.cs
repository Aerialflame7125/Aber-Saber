using System.Collections.Specialized;

namespace System.Web.UI;

/// <summary>Defines methods that ASP.NET server controls must implement to automatically load postback data.</summary>
public interface IPostBackDataHandler
{
	/// <summary>When implemented by a class, processes postback data for an ASP.NET server control.</summary>
	/// <param name="postDataKey">The key identifier for the control. </param>
	/// <param name="postCollection">The collection of all incoming name values. </param>
	/// <returns>
	///     <see langword="true" /> if the server control's state changes as a result of the postback; otherwise, <see langword="false" />.</returns>
	bool LoadPostData(string postDataKey, NameValueCollection postCollection);

	/// <summary>When implemented by a class, signals the server control to notify the ASP.NET application that the state of the control has changed.</summary>
	void RaisePostDataChangedEvent();
}
