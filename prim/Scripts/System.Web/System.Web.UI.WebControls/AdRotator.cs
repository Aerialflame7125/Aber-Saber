using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Web.Util;
using System.Xml;

namespace System.Web.UI.WebControls;

/// <summary>Displays an advertisement banner on a Web page.</summary>
[DefaultEvent("AdCreated")]
[DefaultProperty("AdvertisementFile")]
[Designer("System.Web.UI.Design.WebControls.AdRotatorDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ToolboxData("<{0}:AdRotator runat=\"server\"></{0}:AdRotator>")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class AdRotator : DataBoundControl
{
	private AdCreatedEventArgs createdargs;

	private ArrayList ads = new ArrayList();

	private string ad_file = string.Empty;

	private static readonly object AdCreatedEvent;

	/// <summary>Gets or sets the path to an XML file that contains advertisement information.</summary>
	/// <returns>The location of an XML file containing advertisement information. The default value is an empty string ("").</returns>
	[UrlProperty]
	[Bindable(true)]
	[DefaultValue("")]
	[Editor("System.Web.UI.Design.XmlUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public string AdvertisementFile
	{
		get
		{
			return ad_file;
		}
		set
		{
			ad_file = value;
		}
	}

	/// <summary>Gets or sets a custom data field to use in place of the <see langword="AlternateText" /> attribute for an advertisement.</summary>
	/// <returns>The name that identifies the field where the alternate text for an advertisement is stored. The default value is "AlternateText."</returns>
	[DefaultValue("AlternateText")]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	[MonoTODO("Not implemented")]
	public string AlternateTextField
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the font properties associated with the advertisement banner control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.FontInfo" /> object that represents the font properties of the control.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override FontInfo Font => base.Font;

	/// <summary>Gets or sets a custom data field to use in place of the <see langword="ImageUrl" /> attribute for an advertisement.</summary>
	/// <returns>The name that identifies the field where the URL for the image displayed for an advertisement is stored. The default value is "ImageUrl."</returns>
	[DefaultValue("ImageUrl")]
	[MonoTODO("Not implemented")]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public string ImageUrlField
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets a category keyword to filter for specific types of advertisements in the XML advertisement file.</summary>
	/// <returns>The keyword to filter for specific types of advertisements in the XML advertisement file. The default value is an empty string ("").</returns>
	[Bindable(true)]
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public string KeywordFilter
	{
		get
		{
			return ViewState.GetString("KeywordFilter", string.Empty);
		}
		set
		{
			ViewState["KeywordFilter"] = value;
		}
	}

	/// <summary>Gets or sets a custom data field to use in place of the <see langword="NavigateUrl" /> attribute for an advertisement.</summary>
	/// <returns>The name that identifies the field containing the URL for the page to navigate to when the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control is clicked. The default value is "NavigateUrl."</returns>
	[DefaultValue("NavigateUrl")]
	[MonoTODO("Not implemented")]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public string NavigateUrlField
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets the name of the browser window or frame that displays the contents of the Web page linked to when the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control is clicked.</summary>
	/// <returns>The browser window or frame that displays the contents of the Web page linked to when the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control is clicked. The default value is an empty string (""), which refreshes the window or frame with focus.</returns>
	[Bindable(true)]
	[DefaultValue("_top")]
	[TypeConverter(typeof(TargetConverter))]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public string Target
	{
		get
		{
			return ViewState.GetString("Target", "_top");
		}
		set
		{
			ViewState["Target"] = value;
		}
	}

	/// <summary>Gets the unique, hierarchically qualified identifier for the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control.</summary>
	/// <returns>The fully qualified identifier for the server control.</returns>
	public override string UniqueID => base.UniqueID;

	/// <summary>Gets the HTML tag for the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control. </summary>
	/// <returns>An <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration value representing the HTML tag for an <see cref="T:System.Web.UI.WebControls.AdRotator" /> control. </returns>
	protected override HtmlTextWriterTag TagKey => base.TagKey;

	/// <summary>Occurs once per round trip to the server after the creation of the control, but before the page is rendered.</summary>
	[WebSysDescription("")]
	[WebCategory("Action")]
	public event AdCreatedEventHandler AdCreated
	{
		add
		{
			base.Events.AddHandler(AdCreatedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(AdCreatedEvent, value);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event.</summary>
	/// <param name="e">The event arguments.</param>
	protected internal override void OnInit(EventArgs e)
	{
		base.OnInit(e);
	}

	/// <summary>Gets the advertisement information for rendering by looking up the file data or calling the user event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains event data.</param>
	protected internal override void OnPreRender(EventArgs e)
	{
		Hashtable adProperties = null;
		if (!string.IsNullOrEmpty(ad_file))
		{
			ReadAdsFromFile(GetPhysicalFilePath(ad_file));
			adProperties = ChooseAd();
		}
		AdCreatedEventArgs e2 = new AdCreatedEventArgs(adProperties);
		OnAdCreated(e2);
		createdargs = e2;
	}

	/// <summary>Binds the specified data source to the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control.</summary>
	/// <param name="data">An object that represents the data source; the object must implement the <see cref="T:System.Collections.IEnumerable" /> interface.</param>
	[MonoTODO("Not implemented")]
	protected internal override void PerformDataBinding(IEnumerable data)
	{
		throw new NotImplementedException();
	}

	/// <summary>Retrieves the advertisement data from the associated data source.</summary>
	[MonoTODO("Not implemented")]
	protected override void PerformSelect()
	{
		throw new NotImplementedException();
	}

	/// <summary>Displays the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control on the client.</summary>
	/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client. </param>
	protected internal override void Render(HtmlTextWriter writer)
	{
		AdCreatedEventArgs adCreatedEventArgs = createdargs;
		base.AddAttributesToRender(writer);
		if (adCreatedEventArgs.NavigateUrl != null && adCreatedEventArgs.NavigateUrl.Length > 0)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Href, ResolveAdUrl(adCreatedEventArgs.NavigateUrl));
		}
		if (Target != null && Target.Length > 0)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Target, Target);
		}
		writer.RenderBeginTag(HtmlTextWriterTag.A);
		if (adCreatedEventArgs.ImageUrl != null && adCreatedEventArgs.ImageUrl.Length > 0)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Src, ResolveAdUrl(adCreatedEventArgs.ImageUrl));
		}
		writer.AddAttribute(HtmlTextWriterAttribute.Alt, (adCreatedEventArgs.AlternateText == null) ? string.Empty : adCreatedEventArgs.AlternateText);
		writer.AddAttribute(HtmlTextWriterAttribute.Border, "0", fEncode: false);
		writer.RenderBeginTag(HtmlTextWriterTag.Img);
		writer.RenderEndTag();
		writer.RenderEndTag();
	}

	private string ResolveAdUrl(string url)
	{
		if (AdvertisementFile != null && AdvertisementFile.Length > 0 && url[0] != '/' && url[0] != '~')
		{
			try
			{
				new Uri(url);
			}
			catch
			{
				return UrlUtils.Combine(UrlUtils.GetDirectory(ResolveUrl(AdvertisementFile)), url);
			}
		}
		return ResolveUrl(url);
	}

	private Hashtable ChooseAd()
	{
		string keywordFilter = KeywordFilter;
		int num = 0;
		int num2 = 0;
		bool flag = keywordFilter.Length == 0;
		foreach (Hashtable ad in ads)
		{
			if (flag || keywordFilter == (string)ad["Keyword"])
			{
				num += ((ad["Impressions"] == null) ? 1 : int.Parse((string)ad["Impressions"]));
			}
		}
		int num3 = new Random().Next(num);
		foreach (Hashtable ad2 in ads)
		{
			if (flag || !(keywordFilter != (string)ad2["Keyword"]))
			{
				num2 += ((ad2["Impressions"] == null) ? 1 : int.Parse((string)ad2["Impressions"]));
				if (num2 > num3)
				{
					return ad2;
				}
			}
		}
		if (num != 0)
		{
			throw new Exception("I should only get here if no ads matched");
		}
		return null;
	}

	private void ReadAdsFromFile(string s)
	{
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.Load(s);
		}
		catch (Exception innerException)
		{
			throw new HttpException("AdRotator could not parse the xml file", innerException);
		}
		ads.Clear();
		foreach (XmlNode childNode in xmlDocument.DocumentElement.ChildNodes)
		{
			Hashtable hashtable = new Hashtable();
			foreach (XmlNode childNode2 in childNode.ChildNodes)
			{
				hashtable.Add(childNode2.Name, childNode2.InnerText);
			}
			ads.Add(hashtable);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.AdRotator.AdCreated" /> event for the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control.</summary>
	/// <param name="e">An <see cref="T:System.Web.UI.WebControls.AdCreatedEventArgs" /> that contains event data. </param>
	protected virtual void OnAdCreated(AdCreatedEventArgs e)
	{
		((AdCreatedEventHandler)base.Events[AdCreated])?.Invoke(this, e);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.AdRotator" /> class.</summary>
	public AdRotator()
	{
	}

	static AdRotator()
	{
		AdCreated = new object();
	}
}
