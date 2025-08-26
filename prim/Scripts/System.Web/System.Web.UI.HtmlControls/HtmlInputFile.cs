using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls;

/// <summary>Allows programmatic access to the HTML <see langword="&lt;input type= file&gt;" /> element on the server.</summary>
[ValidationProperty("Value")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class HtmlInputFile : HtmlInputControl, IPostBackDataHandler
{
	private HttpPostedFile posted_file;

	/// <summary>Gets or sets a comma-separated list of MIME encodings used to constrain the file types the user can select.</summary>
	/// <returns>The comma-separated list of MIME encodings.</returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	public string Accept
	{
		get
		{
			string text = base.Attributes["accept"];
			if (text == null)
			{
				return string.Empty;
			}
			return text;
		}
		set
		{
			if (value == null)
			{
				base.Attributes.Remove("accept");
			}
			else
			{
				base.Attributes["accept"] = value;
			}
		}
	}

	/// <summary>Gets or sets the maximum length of the file path for the file to upload from the client computer.</summary>
	/// <returns>The maximum length of the file path. The default value is -1, which indicates that the property has not been set.</returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	public int MaxLength
	{
		get
		{
			string text = base.Attributes["maxlength"];
			if (text == null)
			{
				return -1;
			}
			return Convert.ToInt32(text);
		}
		set
		{
			if (value == -1)
			{
				base.Attributes.Remove("maxlength");
			}
			else
			{
				base.Attributes["maxlength"] = value.ToString();
			}
		}
	}

	/// <summary>Gets access to the uploaded file specified by a client.</summary>
	/// <returns>A <see cref="T:System.Web.HttpPostedFile" /> that accesses the file to be uploaded.</returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Misc")]
	public HttpPostedFile PostedFile => posted_file;

	/// <summary>Gets or sets the width of the text box in which the file path is entered.</summary>
	/// <returns>The width of the file-path text box. The default value is -1, which indicates that the property has not been set.</returns>
	[DefaultValue("-1")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public int Size
	{
		get
		{
			string text = base.Attributes["size"];
			if (text == null)
			{
				return -1;
			}
			return Convert.ToInt32(text);
		}
		set
		{
			if (value == -1)
			{
				base.Attributes.Remove("size");
			}
			else
			{
				base.Attributes["size"] = value.ToString();
			}
		}
	}

	/// <summary>Gets the full path of the file on the client's computer.</summary>
	/// <returns>The full path of the client's file.</returns>
	/// <exception cref="T:System.NotSupportedException">An attempt was made to assign a value to this property. </exception>
	[Browsable(false)]
	public override string Value
	{
		get
		{
			HttpPostedFile postedFile = PostedFile;
			if (postedFile == null)
			{
				return string.Empty;
			}
			return postedFile.FileName;
		}
		set
		{
			throw new NotSupportedException("The value property on HtmlInputFile is not settable.");
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputFile" /> class.</summary>
	public HtmlInputFile()
		: base("file")
	{
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event for the <see cref="T:System.Web.UI.HtmlControls.HtmlInputFile" /> control. </summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
	protected internal override void OnPreRender(EventArgs e)
	{
		base.OnPreRender(e);
		Page page = Page;
		if (page != null && !base.Disabled)
		{
			page.RegisterRequiresPostBack(this);
			page.RegisterEnabledControl(this);
		}
		HtmlForm htmlForm = (HtmlForm)SearchParentByType(typeof(HtmlForm));
		if (htmlForm != null && htmlForm.Enctype == string.Empty)
		{
			htmlForm.Enctype = "multipart/form-data";
		}
	}

	private Control SearchParentByType(Type type)
	{
		for (Control parent = Parent; parent != null; parent = parent.Parent)
		{
			if (type.IsAssignableFrom(parent.GetType()))
			{
				return parent;
			}
		}
		return null;
	}

	private bool LoadPostDataInternal(string postDataKey, NameValueCollection postCollection)
	{
		Page page = Page;
		if (page != null)
		{
			posted_file = page.Request.Files[postDataKey];
		}
		return false;
	}

	private void RaisePostDataChangedEventInternal()
	{
	}

	/// <summary>Processes the postback data for the <see cref="T:System.Web.UI.HtmlControls.HtmlInputFile" /> control.</summary>
	/// <param name="postDataKey">The key identifier for the control.</param>
	/// <param name="postCollection">The collection of all incoming name values.</param>
	/// <returns>This method always returns <see langword="false" />.</returns>
	protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
	{
		return LoadPostDataInternal(postDataKey, postCollection);
	}

	/// <summary>Notifies the <see cref="T:System.Web.UI.HtmlControls.HtmlInputFile" /> control that the state of the control has changed.</summary>
	protected virtual void RaisePostDataChangedEvent()
	{
		RaisePostDataChangedEventInternal();
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IPostBackDataHandler.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" />. </summary>
	/// <param name="postDataKey">The key identifier for the control.</param>
	/// <param name="postCollection">The collection of all incoming name values.</param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.HtmlControls.HtmlInputFile" /> control's state has changed as a result of the postback; otherwise, <see langword="false" />.</returns>
	bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
	{
		return LoadPostData(postDataKey, postCollection);
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IPostBackDataHandler.RaisePostDataChangedEvent" />.</summary>
	void IPostBackDataHandler.RaisePostDataChangedEvent()
	{
		RaisePostDataChangedEvent();
	}
}
