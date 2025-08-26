using System.Collections;
using System.Collections.Specialized;
using System.Web.UI.WebControls;

namespace System.Web.UI.Adapters;

/// <summary>Adapts a Web page for a specific browser and provides the base class from which all page adapters inherit, directly or indirectly. </summary>
public abstract class PageAdapter : ControlAdapter
{
	private ListDictionary radio_button_group;

	/// <summary>Gets a list of additional HTTP headers by which caching is varied for the Web page to which this derived page adapter is attached.</summary>
	/// <returns>An <see cref="T:System.Collections.IList" /> that contains a list of HTTP headers; otherwise, <see langword="null" />.</returns>
	public virtual StringCollection CacheVaryByHeaders => null;

	/// <summary>Gets a list of additional parameters from HTTP GET and POST requests by which caching is varied for the Web page to which this derived page adapter is attached.</summary>
	/// <returns>An <see cref="T:System.Collections.IList" /> that contains a list of the GET and POST parameters; otherwise, <see langword="null" />.</returns>
	public virtual StringCollection CacheVaryByParams => null;

	/// <summary>Gets an encoded string that contains the view and control states data of the Web page to which this derived page adapter is attached.</summary>
	/// <returns>An encoded <see cref="T:System.String" /> containing the combined view and control states of the controls on the associated <see cref="T:System.Web.UI.Page" />.</returns>
	protected string ClientState => base.Page.GetSavedViewState();

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Adapters.PageAdapter" /> class. </summary>
	protected PageAdapter()
	{
	}

	internal PageAdapter(Page p)
		: base(p)
	{
	}

	/// <summary>Determines whether the Web page is in postback and returns a name/value collection of the postback variables.</summary>
	/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> of the postback variables, if any; otherwise <see langword="null" />. </returns>
	public virtual NameValueCollection DeterminePostBackMode()
	{
		return base.Page.DeterminePostBackMode();
	}

	/// <summary>Retrieves a collection of radio button controls specified by <paramref name="groupName" />.</summary>
	/// <param name="groupName">A <see cref="T:System.String" /> that is the name of the <see cref="T:System.Web.UI.WebControls.RadioButton" /> group to retrieve. </param>
	/// <returns>An <see cref="T:System.Collections.ICollection" /> of <see cref="T:System.Web.UI.WebControls.RadioButton" /> controls that make up <paramref name="groupName" />.</returns>
	public virtual ICollection GetRadioButtonsByGroup(string groupName)
	{
		if (radio_button_group == null)
		{
			return new ArrayList();
		}
		ArrayList arrayList = (ArrayList)radio_button_group[groupName];
		if (arrayList == null)
		{
			return new ArrayList();
		}
		return arrayList;
	}

	/// <summary>Returns an object that is used by the Web page to maintain the control and view states.</summary>
	/// <returns>An object derived from <see cref="T:System.Web.UI.PageStatePersister" /> that supports creating and extracting the combined control and view states for the <see cref="T:System.Web.UI.Page" />.</returns>
	public virtual PageStatePersister GetStatePersister()
	{
		return new HiddenFieldPageStatePersister((Page)base.Control);
	}

	/// <summary>Adds a radio button control to the collection for a specified radio button group.</summary>
	/// <param name="radioButton">The <see cref="T:System.Web.UI.WebControls.RadioButton" /> to add to the collection. </param>
	public virtual void RegisterRadioButton(RadioButton radioButton)
	{
		if (radio_button_group == null)
		{
			radio_button_group = new ListDictionary();
		}
		ArrayList arrayList = (ArrayList)radio_button_group[radioButton.GroupName];
		if (arrayList == null)
		{
			arrayList = (ArrayList)(radio_button_group[radioButton.GroupName] = new ArrayList());
		}
		if (!arrayList.Contains(radioButton))
		{
			arrayList.Add(radioButton);
		}
	}

	/// <summary>Renders an opening hyperlink tag that includes the target URL to the response stream.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> containing methods to render the target-specific output. </param>
	/// <param name="targetUrl">The <see cref="T:System.String" /> value holding the target URL of the link. </param>
	/// <param name="encodeUrl">
	///       <see langword="true" /> to use <see cref="M:System.Web.HttpUtility.HtmlAttributeEncode(System.String)" /> to encode the stream output; otherwise, <see langword="false" />. </param>
	/// <param name="softkeyLabel">The <see cref="T:System.String" /> value to use as a soft key label. </param>
	public virtual void RenderBeginHyperlink(HtmlTextWriter writer, string targetUrl, bool encodeUrl, string softkeyLabel)
	{
		InternalRenderBeginHyperlink(writer, targetUrl, encodeUrl, softkeyLabel, null);
	}

	/// <summary>Renders an opening hyperlink tag that includes the target URL and an access key to the response stream.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> containing methods to render the target-specific output. </param>
	/// <param name="targetUrl">The <see cref="T:System.String" /> value holding the target URL of the link. </param>
	/// <param name="encodeUrl">
	///       <see langword="true" /> to use <see cref="M:System.Web.HttpUtility.HtmlAttributeEncode(System.String)" /> to encode the stream output; otherwise, <see langword="false" />. </param>
	/// <param name="softkeyLabel">The <see cref="T:System.String" /> value to use as a soft key label. </param>
	/// <param name="accessKey">The <see cref="T:System.String" /> value to assign to the <see langword="accessKey" /> attribute of the link to create. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="accessKey" /> is longer than one character.</exception>
	public virtual void RenderBeginHyperlink(HtmlTextWriter writer, string targetUrl, bool encodeUrl, string softkeyLabel, string accessKey)
	{
		if (accessKey != null && accessKey.Length > 1)
		{
			throw new ArgumentOutOfRangeException("accessKey");
		}
		InternalRenderBeginHyperlink(writer, targetUrl, encodeUrl, softkeyLabel, accessKey);
	}

	private void InternalRenderBeginHyperlink(HtmlTextWriter w, string targetUrl, bool encodeUrl, string softKeyLabel, string accessKey)
	{
		w.AddAttribute(HtmlTextWriterAttribute.Href, targetUrl, encodeUrl);
		if (accessKey != null)
		{
			w.AddAttribute(HtmlTextWriterAttribute.Accesskey, accessKey);
		}
		w.RenderBeginTag(HtmlTextWriterTag.A);
	}

	/// <summary>Renders a closing hyperlink tag to the response stream.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains methods to render the target-specific output. </param>
	public virtual void RenderEndHyperlink(HtmlTextWriter writer)
	{
		writer.RenderEndTag();
	}

	/// <summary>Renders a postback event into the response stream as a hyperlink, including the encoded and possibly encrypted view state, and event target and argument.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> containing methods to render the target-specific output. </param>
	/// <param name="target">The <see cref="T:System.String" /> value holding the postback event target name. </param>
	/// <param name="argument">The <see cref="T:System.String" /> value holding the argument to pass to the postback target event. </param>
	/// <param name="softkeyLabel">The <see cref="T:System.String" /> value to use as a soft key label. </param>
	/// <param name="text">The <see cref="T:System.String" /> value of the text to display as the link. </param>
	public virtual void RenderPostBackEvent(HtmlTextWriter writer, string target, string argument, string softkeyLabel, string text)
	{
		RenderPostBackEvent(writer, target, argument, softkeyLabel, text, base.Page.Request.FilePath, null, encode: true);
	}

	/// <summary>Renders a postback event into the response stream as a hyperlink, including the encoded and possibly encrypted view state, an event target and argument, a previous-page parameter, and an access key.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> containing methods to render the target-specific output. </param>
	/// <param name="target">The <see cref="T:System.String" /> value holding the postback event target name. </param>
	/// <param name="argument">The <see cref="T:System.String" /> value holding the argument to pass to the postback target event. </param>
	/// <param name="softkeyLabel">The <see cref="T:System.String" /> value to use as a soft key label. </param>
	/// <param name="text">The <see cref="T:System.String" /> value of the text to display as the link. </param>
	/// <param name="postUrl">The <see cref="T:System.String" /> value holding the URL target page of the postback. </param>
	/// <param name="accessKey">The <see cref="T:System.String" /> value used to assign to the <see langword="accessKey" /> attribute of the created link. </param>
	public virtual void RenderPostBackEvent(HtmlTextWriter writer, string target, string argument, string softkeyLabel, string text, string postUrl, string accessKey)
	{
		RenderPostBackEvent(writer, target, argument, softkeyLabel, text, postUrl, accessKey, encode: true);
	}

	/// <summary>Renders a postback event into the response stream as a hyperlink, including the encoded view state, an event target and argument, a previous-page parameter, and an access key.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> containing methods to render the target-specific output. </param>
	/// <param name="target">The <see cref="T:System.String" /> value holding the postback event target name. </param>
	/// <param name="argument">The <see cref="T:System.String" /> value holding the argument to pass to the postback target event. </param>
	/// <param name="softkeyLabel">The <see cref="T:System.String" /> value to use as a soft key label. </param>
	/// <param name="text">The <see cref="T:System.String" /> value of the text to display as the link. </param>
	/// <param name="postUrl">The <see cref="T:System.String" /> value holding the URL target page of the postback. </param>
	/// <param name="accessKey">The <see cref="T:System.String" /> value to assign to the <see langword="accessKey" /> attribute of the created link. </param>
	/// <param name="encode">
	///       <see langword="true" /> to use &amp;amp; as the URL parameter separator; <see langword="false" /> to use &amp;. </param>
	protected void RenderPostBackEvent(HtmlTextWriter writer, string target, string argument, string softkeyLabel, string text, string postUrl, string accessKey, bool encode)
	{
		string targetUrl = $"{postUrl}?__VIEWSTATE={HttpUtility.UrlEncode(base.Page.GetSavedViewState())}&__EVENTTARGET={target}&__EVENTARGUMENT={argument}&__PREVIOUSPAGE={base.Page.Request.FilePath}";
		RenderBeginHyperlink(writer, targetUrl, encode, softkeyLabel, accessKey);
		writer.Write(text);
		RenderEndHyperlink(writer);
	}

	/// <summary>Transforms text for the target browser.</summary>
	/// <param name="text">A <see cref="T:System.String" /> that is the text to transform.</param>
	/// <returns>A <see cref="T:System.String" /> that contains the transformed text.</returns>
	public virtual string TransformText(string text)
	{
		return text;
	}

	/// <summary>Returns a DHTML code fragment that the client browser can use to reference the form on the page that was posted.</summary>
	/// <param name="formId">A <see cref="T:System.String" /> containing the client ID of the form that was posted. </param>
	/// <returns>A <see cref="T:System.String" /> with a reference to the form on the page that was posted.</returns>
	protected internal virtual string GetPostBackFormReference(string formId)
	{
		return $"document.forms['{formId}']";
	}
}
