using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Represents the style for a <see cref="T:System.Web.UI.WebControls.Panel" /> control.</summary>
public class PanelStyle : Style
{
	[Flags]
	private enum PanelStyles
	{
		BackImageUrl = 0x10000,
		Direction = 0x20000,
		HorizontalAlign = 0x40000,
		ScrollBars = 0x80000,
		Wrap = 0x100000
	}

	/// <summary>Gets or sets the URL of the background image for the panel control.</summary>
	/// <returns>The URL of the background image for the panel control. The default value is an empty string ("").</returns>
	/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Web.UI.WebControls.PanelStyle.BackImageUrl" /> property is <see langword="null" />.</exception>
	[DefaultValue("")]
	[UrlProperty]
	public virtual string BackImageUrl
	{
		get
		{
			if (!CheckBit(65536))
			{
				return string.Empty;
			}
			return base.ViewState.GetString("BackImageUrl", string.Empty);
		}
		set
		{
			base.ViewState["BackImageUrl"] = value;
			SetBit(65536);
		}
	}

	/// <summary>Gets or sets the direction in which to display controls that include text in a panel control.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ContentDirection" /> values. The default is <see cref="F:System.Web.UI.WebControls.ContentDirection.NotSet" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The direction is not one of the <see cref="T:System.Web.UI.WebControls.ContentDirection" /> values.</exception>
	[DefaultValue(ContentDirection.NotSet)]
	public virtual ContentDirection Direction
	{
		get
		{
			if (!CheckBit(131072))
			{
				return ContentDirection.NotSet;
			}
			return (ContentDirection)base.ViewState["Direction"];
		}
		set
		{
			base.ViewState["Direction"] = value;
			SetBit(131072);
		}
	}

	/// <summary>Gets or sets the horizontal alignment of the contents within a panel control.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.HorizontalAlign" /> values. The default is <see cref="F:System.Web.UI.WebControls.HorizontalAlign.NotSet" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The horizontal alignment is not one of the <see cref="T:System.Web.UI.WebControls.HorizontalAlign" /> values.</exception>
	[DefaultValue(HorizontalAlign.NotSet)]
	public virtual HorizontalAlign HorizontalAlign
	{
		get
		{
			if (!CheckBit(262144))
			{
				return HorizontalAlign.NotSet;
			}
			return (HorizontalAlign)base.ViewState["HorizontalAlign"];
		}
		set
		{
			base.ViewState["HorizontalAlign"] = value;
			SetBit(262144);
		}
	}

	/// <summary>Gets or sets the visibility and position of scroll bars in a panel control.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ScrollBars" /> values. The default is <see cref="F:System.Web.UI.WebControls.ScrollBars.None" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.PanelStyle.ScrollBars" /> property is not one of the <see cref="T:System.Web.UI.WebControls.ScrollBars" /> values.</exception>
	[DefaultValue(ScrollBars.None)]
	public virtual ScrollBars ScrollBars
	{
		get
		{
			if (!CheckBit(524288))
			{
				return ScrollBars.None;
			}
			return (ScrollBars)base.ViewState["ScrollBars"];
		}
		set
		{
			base.ViewState["ScrollBars"] = value;
			SetBit(524288);
		}
	}

	/// <summary>Gets or sets a value indicating whether the content wraps within the panel.</summary>
	/// <returns>
	///     <see langword="true" /> if the content wraps within the panel; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[DefaultValue(true)]
	public virtual bool Wrap
	{
		get
		{
			if (!CheckBit(1048576))
			{
				return true;
			}
			return (bool)base.ViewState["Wrap"];
		}
		set
		{
			base.ViewState["Wrap"] = value;
			SetBit(1048576);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.PanelStyle" /> class.</summary>
	/// <param name="bag">A <see cref="T:System.Web.UI.StateBag" /> object that represents the state bag in which to store style information.</param>
	public PanelStyle(StateBag bag)
		: base(bag)
	{
	}

	/// <summary>Duplicates the style properties of the specified <see cref="T:System.Web.UI.WebControls.Style" /> object for the current instance of the <see cref="T:System.Web.UI.WebControls.PanelStyle" /> class.</summary>
	/// <param name="s">A <see cref="T:System.Web.UI.WebControls.Style" /> object that represents the style settings to copy.</param>
	public override void CopyFrom(Style s)
	{
		if (s == null || s.IsEmpty)
		{
			return;
		}
		base.CopyFrom(s);
		if (s is PanelStyle panelStyle)
		{
			if (s.CheckBit(65536))
			{
				BackImageUrl = panelStyle.BackImageUrl;
			}
			if (s.CheckBit(131072))
			{
				Direction = panelStyle.Direction;
			}
			if (s.CheckBit(262144))
			{
				HorizontalAlign = panelStyle.HorizontalAlign;
			}
			if (s.CheckBit(524288))
			{
				ScrollBars = panelStyle.ScrollBars;
			}
			if (s.CheckBit(1048576))
			{
				Wrap = panelStyle.Wrap;
			}
		}
	}

	/// <summary>Combines the style settings of the specified <see cref="T:System.Web.UI.WebControls.Style" /> object with the current instance of the <see cref="T:System.Web.UI.WebControls.PanelStyle" /> class.</summary>
	/// <param name="s">A <see cref="T:System.Web.UI.WebControls.Style" /> object that represents the style settings to combine with the <see cref="T:System.Web.UI.WebControls.PanelStyle" /> object.</param>
	public override void MergeWith(Style s)
	{
		if (s == null || s.IsEmpty)
		{
			return;
		}
		base.MergeWith(s);
		if (s is PanelStyle panelStyle)
		{
			if (!CheckBit(65536) && s.CheckBit(65536))
			{
				BackImageUrl = panelStyle.BackImageUrl;
			}
			if (!CheckBit(131072) && s.CheckBit(131072))
			{
				Direction = panelStyle.Direction;
			}
			if (!CheckBit(262144) && s.CheckBit(262144))
			{
				HorizontalAlign = panelStyle.HorizontalAlign;
			}
			if (!CheckBit(524288) && s.CheckBit(524288))
			{
				ScrollBars = panelStyle.ScrollBars;
			}
			if (!CheckBit(1048576) && s.CheckBit(1048576))
			{
				Wrap = panelStyle.Wrap;
			}
		}
	}

	/// <summary>Removes any defined style settings from the <see cref="T:System.Web.UI.WebControls.PanelStyle" /> class.</summary>
	public override void Reset()
	{
		base.Reset();
		base.ViewState.Remove("BackImageUrl");
		base.ViewState.Remove("Direction");
		base.ViewState.Remove("HorizontalAlign");
		base.ViewState.Remove("ScrollBars");
		base.ViewState.Remove("Wrap");
	}
}
