using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI.HtmlControls;

/// <summary>Allows programmatic access to the HTML <see langword="&lt;input type= button&gt;" />, <see langword="&lt;input type= submit&gt;" />, and <see langword="&lt;input type= reset&gt;" /> elements on the server.</summary>
[DefaultEvent("ServerClick")]
[SupportsEventValidation]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class HtmlInputButton : HtmlInputControl, IPostBackEventHandler
{
	private static readonly object ServerClickEvent;

	/// <summary>Gets or sets a value indicating whether validation is performed when the <see cref="T:System.Web.UI.HtmlControls.HtmlInputButton" /> control is clicked.</summary>
	/// <returns>
	///     <see langword="true" /> if validation is performed when the <see cref="T:System.Web.UI.HtmlControls.HtmlInputButton" /> control is clicked; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
	[DefaultValue(true)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual bool CausesValidation
	{
		get
		{
			string text = base.Attributes["CausesValidation"];
			if (text == null)
			{
				return true;
			}
			return bool.Parse(text);
		}
		set
		{
			base.Attributes["CausesValidation"] = value.ToString();
		}
	}

	/// <summary>Gets or sets the group of controls for which the <see cref="T:System.Web.UI.HtmlControls.HtmlInputButton" /> causes validation when it posts back to the server.</summary>
	/// <returns>The group of controls for which the <see cref="T:System.Web.UI.HtmlControls.HtmlInputButton" /> control causes validation when it posts back to the server. The default value is an empty string (""), indicating that this property is not set. </returns>
	[DefaultValue("")]
	public virtual string ValidationGroup
	{
		get
		{
			string text = base.Attributes["ValidationGroup"];
			if (text == null)
			{
				return "";
			}
			return text;
		}
		set
		{
			if (value == null)
			{
				base.Attributes.Remove("ValidationGroup");
			}
			else
			{
				base.Attributes["ValidationGroup"] = value;
			}
		}
	}

	/// <summary>Occurs when an <see cref="T:System.Web.UI.HtmlControls.HtmlInputButton" /> control is clicked on the Web page.</summary>
	[WebSysDescription("")]
	[WebCategory("Action")]
	public event EventHandler ServerClick
	{
		add
		{
			base.Events.AddHandler(ServerClickEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ServerClickEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputButton" /> class using default values.</summary>
	public HtmlInputButton()
		: this("button")
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputButton" /> class using the specified button type.</summary>
	/// <param name="type">The input button type. </param>
	public HtmlInputButton(string type)
		: base(type)
	{
	}

	private void RaisePostBackEventInternal(string eventArgument)
	{
		ValidateEvent(UniqueID, eventArgument);
		if (CausesValidation)
		{
			Page.Validate(ValidationGroup);
		}
		if (string.Compare(base.Type, "reset", ignoreCase: true, Helpers.InvariantCulture) != 0)
		{
			OnServerClick(EventArgs.Empty);
		}
		else
		{
			ResetForm(FindForm());
		}
	}

	private HtmlForm FindForm()
	{
		return Page?.Form;
	}

	private void ResetForm(HtmlForm form)
	{
		if (form != null && form.HasControls())
		{
			ResetChildrenValues(form.Controls);
		}
	}

	private void ResetChildrenValues(ControlCollection children)
	{
		foreach (Control child in children)
		{
			if (child != null)
			{
				if (child.HasControls())
				{
					ResetChildrenValues(child.Controls);
				}
				ResetChildValue(child);
			}
		}
	}

	private void ResetChildValue(Control child)
	{
		Type type = child.GetType();
		object[] customAttributes = type.GetCustomAttributes(inherit: false);
		if (customAttributes == null || customAttributes.Length == 0)
		{
			return;
		}
		string text = null;
		object[] array = customAttributes;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] is DefaultPropertyAttribute defaultPropertyAttribute)
			{
				text = defaultPropertyAttribute.Name;
				break;
			}
		}
		if (text == null || text.Length == 0)
		{
			return;
		}
		PropertyInfo propertyInfo = null;
		try
		{
			propertyInfo = type.GetProperty(text, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}
		catch (Exception)
		{
		}
		if (propertyInfo == null || !propertyInfo.CanWrite)
		{
			return;
		}
		customAttributes = propertyInfo.GetCustomAttributes(inherit: false);
		if (customAttributes == null || customAttributes.Length == 0)
		{
			return;
		}
		DefaultValueAttribute defaultValueAttribute = null;
		object obj = null;
		array = customAttributes;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] is DefaultValueAttribute defaultValueAttribute2)
			{
				obj = defaultValueAttribute2.Value;
				break;
			}
		}
		if (obj == null || propertyInfo.PropertyType != obj.GetType())
		{
			return;
		}
		try
		{
			propertyInfo.SetValue(child, obj, null);
		}
		catch (Exception)
		{
		}
	}

	/// <summary>Raises events for the <see cref="T:System.Web.UI.HtmlControls.HtmlInputButton" /> control when it posts back to the server.</summary>
	/// <param name="eventArgument">The argument for the event.</param>
	protected virtual void RaisePostBackEvent(string eventArgument)
	{
		RaisePostBackEventInternal(eventArgument);
	}

	/// <summary>Implements the <see cref="M:System.Web.UI.IPostBackEventHandler.RaisePostBackEvent(System.String)" /> method by calling the <see cref="M:System.Web.UI.HtmlControls.HtmlInputButton.RaisePostBackEvent(System.String)" /> method.</summary>
	/// <param name="eventArgument">A <see cref="T:System.String" /> that represents an optional event argument to be passed to the event handler.</param>
	void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
	{
		RaisePostBackEvent(eventArgument);
	}

	/// <summary>Raises the <see cref="M:System.Web.UI.Control.OnPreRender(System.EventArgs)" /> event and registers client script for generating postback.</summary>
	/// <param name="e">An <see cref="P:System.Web.UI.Design.ViewEventArgs.EventArgs" /> that contains the event data. </param>
	protected internal override void OnPreRender(EventArgs e)
	{
		base.OnPreRender(e);
		if ((object)base.Events[ServerClick] != null)
		{
			Page.RequiresPostBackScript();
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.HtmlControls.HtmlInputButton.ServerClick" /> event. This allows you to handle the event directly.</summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnServerClick(EventArgs e)
	{
		((EventHandler)base.Events[ServerClick])?.Invoke(this, e);
	}

	/// <summary>Renders the attributes into the specified writer and then calls the <see cref="M:System.Web.UI.HtmlControls.HtmlControl.RenderAttributes(System.Web.UI.HtmlTextWriter)" /> method.</summary>
	/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content.</param>
	protected override void RenderAttributes(HtmlTextWriter writer)
	{
		CultureInfo invariantCulture = Helpers.InvariantCulture;
		string type = base.Type;
		if (string.Compare(type, "reset", ignoreCase: true, invariantCulture) != 0 && (string.Compare(type, "submit", ignoreCase: true, invariantCulture) == 0 || (string.Compare(type, "button", ignoreCase: true, invariantCulture) == 0 && (object)base.Events[ServerClick] != null)))
		{
			string text = string.Empty;
			if (base.Attributes["onclick"] != null)
			{
				text = ClientScriptManager.EnsureEndsWithSemicolon(base.Attributes["onclick"] + text);
				base.Attributes.Remove("onclick");
			}
			Page page = Page;
			if (page != null)
			{
				PostBackOptions postBackOptions = GetPostBackOptions();
				text += page.ClientScript.GetPostBackEventReference(postBackOptions, registerForEventValidation: true);
			}
			if (text.Length > 0)
			{
				bool fEncode = true;
				if ((object)base.Events[ServerClick] != null)
				{
					fEncode = false;
				}
				writer.WriteAttribute("onclick", text, fEncode);
				writer.WriteAttribute("language", "javascript");
			}
		}
		base.Attributes.Remove("CausesValidation");
		base.RenderAttributes(writer);
	}

	private PostBackOptions GetPostBackOptions()
	{
		Page page = Page;
		PostBackOptions postBackOptions = new PostBackOptions(this);
		postBackOptions.ValidationGroup = null;
		postBackOptions.ActionUrl = null;
		postBackOptions.Argument = string.Empty;
		postBackOptions.RequiresJavaScriptProtocol = false;
		postBackOptions.ClientSubmit = string.Compare(base.Type, "submit", ignoreCase: true, Helpers.InvariantCulture) != 0;
		postBackOptions.PerformValidation = CausesValidation && page != null && page.Validators.Count > 0;
		if (postBackOptions.PerformValidation)
		{
			postBackOptions.ValidationGroup = ValidationGroup;
		}
		return postBackOptions;
	}

	static HtmlInputButton()
	{
		ServerClick = new object();
	}
}
