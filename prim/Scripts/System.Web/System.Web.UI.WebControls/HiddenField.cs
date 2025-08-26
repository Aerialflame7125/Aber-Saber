using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Represents a hidden field used to store a non-displayed value.</summary>
[DefaultEvent("ValueChanged")]
[DefaultProperty("Value")]
[Designer("System.Web.UI.Design.WebControls.HiddenFieldDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ControlValueProperty("Value")]
[NonVisualControl]
[ParseChildren]
[PersistChildren(false)]
[SupportsEventValidation]
public class HiddenField : Control, IPostBackDataHandler
{
	private static readonly object ValueChangedEvent;

	/// <summary>Gets or sets the value of the hidden field.</summary>
	/// <returns>The value of the hidden field. The default is an empty string ("").</returns>
	[DefaultValue("")]
	[Bindable(true)]
	public virtual string Value
	{
		get
		{
			return ViewState.GetString("Value", string.Empty);
		}
		set
		{
			ViewState["Value"] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether themes apply to this control.</summary>
	/// <returns>Always returns <see langword="false" /> to indicate that this control does not support themes.</returns>
	/// <exception cref="T:System.NotSupportedException">An attempt is made to set this property.</exception>
	[DefaultValue(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override bool EnableTheming
	{
		get
		{
			return false;
		}
		set
		{
			throw new NotSupportedException();
		}
	}

	/// <summary>Gets or sets the skin to apply to the control.</summary>
	/// <returns>Always returns an empty string ("") to indicate that themes are not supported.</returns>
	/// <exception cref="T:System.NotSupportedException">An attempt is made to set this property.</exception>
	[DefaultValue("")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override string SkinID
	{
		get
		{
			return string.Empty;
		}
		set
		{
			throw new NotSupportedException();
		}
	}

	/// <summary>Occurs when the value of the <see cref="T:System.Web.UI.WebControls.HiddenField" /> control changes between posts to the server.</summary>
	public event EventHandler ValueChanged
	{
		add
		{
			base.Events.AddHandler(ValueChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ValueChangedEvent, value);
		}
	}

	/// <summary>Sets input focus to this control.</summary>
	/// <exception cref="T:System.NotSupportedException">An attempt is made to call this method.</exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override void Focus()
	{
		throw new NotSupportedException();
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.HiddenField.ValueChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
	protected virtual void OnValueChanged(EventArgs e)
	{
		((EventHandler)base.Events[ValueChanged])?.Invoke(this, e);
	}

	/// <summary>Processes postback data for a <see cref="T:System.Web.UI.WebControls.HiddenField" /> control.</summary>
	/// <param name="postDataKey">The key identifier for the control.</param>
	/// <param name="postCollection">The collection of all incoming name values.</param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.WebControls.HiddenField" /> control's state changes as a result of the postback; otherwise, <see langword="false" />.</returns>
	protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
	{
		if (Value != postCollection[postDataKey])
		{
			Value = postCollection[postDataKey];
			return true;
		}
		return false;
	}

	/// <summary>Notifies the ASP.NET application that the state of the <see cref="T:System.Web.UI.WebControls.HiddenField" /> control has changed.</summary>
	protected virtual void RaisePostDataChangedEvent()
	{
		ValidateEvent(UniqueID, string.Empty);
		OnValueChanged(EventArgs.Empty);
	}

	/// <summary>Creates an <see cref="T:System.Web.UI.EmptyControlCollection" /> object used to indicate that child controls are not allowed.</summary>
	/// <returns>Always returns an <see cref="T:System.Web.UI.EmptyControlCollection" /> object.</returns>
	protected override ControlCollection CreateControlCollection()
	{
		return new EmptyControlCollection(this);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
	protected internal override void OnPreRender(EventArgs e)
	{
		base.OnPreRender(e);
	}

	/// <summary>Renders the Web server control content to the client's browser using the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object used to render the server control content on the client's browser. </param>
	protected internal override void Render(HtmlTextWriter writer)
	{
		Page page = Page;
		string text = UniqueID;
		page?.ClientScript.RegisterForEventValidation(text);
		writer.AddAttribute(HtmlTextWriterAttribute.Type, "hidden", fEncode: false);
		if (!string.IsNullOrEmpty(ClientID))
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID);
		}
		if (!string.IsNullOrEmpty(text))
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Name, text);
		}
		if (!string.IsNullOrEmpty(Value))
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Value, Value);
		}
		writer.RenderBeginTag(HtmlTextWriterTag.Input);
		writer.RenderEndTag();
	}

	/// <summary>For a description of this member, see the <see cref="M:System.Web.UI.IPostBackDataHandler.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" /> method.</summary>
	/// <param name="postDataKey">The key identifier for the control.</param>
	/// <param name="postCollection">The collection of all incoming name values.</param>
	/// <returns>
	///     <see langword="true" /> if the server control's state changes as a result of the postback; otherwise, <see langword="false" />.</returns>
	bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
	{
		return LoadPostData(postDataKey, postCollection);
	}

	/// <summary>For a description of this member, see the <see cref="M:System.Web.UI.IPostBackDataHandler.RaisePostDataChangedEvent" /> method.</summary>
	void IPostBackDataHandler.RaisePostDataChangedEvent()
	{
		RaisePostDataChangedEvent();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.HiddenField" /> class.</summary>
	public HiddenField()
	{
	}

	static HiddenField()
	{
		ValueChanged = new object();
	}
}
