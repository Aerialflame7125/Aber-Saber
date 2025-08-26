using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Specifies the style for the pager of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. This class cannot be inherited.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class DataGridPagerStyle : TableItemStyle
{
	[Flags]
	private enum DataGridPagerStyles
	{
		Mode = 0x100000,
		NextPageText = 0x200000,
		PageButtonCount = 0x400000,
		Position = 0x800000,
		PrevPageText = 0x1000000,
		Visible = 0x2000000
	}

	/// <summary>Gets or sets a value that specifies whether the pager element displays buttons that link to the next and previous page, or numeric buttons that link directly to a page.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.PagerMode" /> values. The default value is <see langword="NextPrev" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is not one of the <see cref="T:System.Web.UI.WebControls.PagerMode" /> values. </exception>
	[DefaultValue(PagerMode.NextPrev)]
	[NotifyParentProperty(true)]
	[WebSysDescription("")]
	[WebCategory("Misc")]
	public PagerMode Mode
	{
		get
		{
			if (!CheckBit(1048576))
			{
				return PagerMode.NextPrev;
			}
			return (PagerMode)base.ViewState["Mode"];
		}
		set
		{
			base.ViewState["Mode"] = value;
			SetBit(1048576);
		}
	}

	/// <summary>Gets or sets the text displayed for the next page button.</summary>
	/// <returns>The text to display for the next page button. The default value is <see langword="&quot;&amp;gt;&quot;" />, which is rendered as the greater than sign (&gt;).</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is less than <see langword="1" />.</exception>
	[Localizable(true)]
	[DefaultValue("&gt;")]
	[NotifyParentProperty(true)]
	[WebSysDescription("")]
	[WebCategory("Misc")]
	public string NextPageText
	{
		get
		{
			if (!CheckBit(2097152))
			{
				return "&gt;";
			}
			return base.ViewState.GetString("NextPageText", "&gt;");
		}
		set
		{
			base.ViewState["NextPageText"] = value;
			SetBit(2097152);
		}
	}

	/// <summary>Gets or sets the number of numeric buttons to display concurrently in the pager element of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	/// <returns>The number of numeric buttons to display concurrently in the pager element of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. The default value is <see langword="10" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is less than <see langword="1" />.</exception>
	[DefaultValue(10)]
	[NotifyParentProperty(true)]
	[WebSysDescription("")]
	[WebCategory("Misc")]
	public int PageButtonCount
	{
		get
		{
			if (!CheckBit(4194304))
			{
				return 10;
			}
			return base.ViewState.GetInt("PageButtonCount", 10);
		}
		set
		{
			if (value < 1)
			{
				throw new ArgumentOutOfRangeException("value", "PageButtonCount must be greater than 0");
			}
			base.ViewState["PageButtonCount"] = value;
			SetBit(4194304);
		}
	}

	/// <summary>Gets or sets the position of the pager element in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.PagerPosition" /> values. The default value is <see langword="Bottom" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is not one of the <see cref="T:System.Web.UI.WebControls.PagerPosition" /> values. </exception>
	[DefaultValue(PagerPosition.Bottom)]
	[NotifyParentProperty(true)]
	[WebSysDescription("")]
	[WebCategory("Misc")]
	public PagerPosition Position
	{
		get
		{
			if (!CheckBit(8388608))
			{
				return PagerPosition.Bottom;
			}
			return (PagerPosition)base.ViewState["Position"];
		}
		set
		{
			base.ViewState["Position"] = value;
			SetBit(8388608);
		}
	}

	/// <summary>Gets or sets the text displayed for the previous page button.</summary>
	/// <returns>The text to display for the previous page button. The default value is <see langword="&quot;&amp;lt;&quot;" />, which is rendered as the less than sign (&lt;).</returns>
	[Localizable(true)]
	[DefaultValue("&lt;")]
	[NotifyParentProperty(true)]
	[WebSysDescription("")]
	[WebCategory("Misc")]
	public string PrevPageText
	{
		get
		{
			if (!CheckBit(16777216))
			{
				return "&lt;";
			}
			return base.ViewState.GetString("PrevPageText", "&lt;");
		}
		set
		{
			base.ViewState["PrevPageText"] = value;
			SetBit(16777216);
		}
	}

	/// <summary>Gets or sets a value indicating whether the pager is displayed in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	/// <returns>
	///     <see langword="true" /> to display the pager; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
	[DefaultValue(true)]
	[NotifyParentProperty(true)]
	[WebSysDescription("")]
	[WebCategory("Misc")]
	public bool Visible
	{
		get
		{
			if (!CheckBit(33554432))
			{
				return true;
			}
			return base.ViewState.GetBool("Visible", def: true);
		}
		set
		{
			base.ViewState["Visible"] = value;
			SetBit(33554432);
		}
	}

	internal DataGridPagerStyle()
	{
	}

	/// <summary>Copies the style of the specified <see cref="T:System.Web.UI.WebControls.Style" /> object into this instance of the <see cref="T:System.Web.UI.WebControls.DataGridPagerStyle" /> class.</summary>
	/// <param name="s">The <see cref="T:System.Web.UI.WebControls.Style" /> to copy from. </param>
	public override void CopyFrom(Style s)
	{
		base.CopyFrom(s);
		if (s != null && !s.IsEmpty)
		{
			if (s.CheckBit(1048576) && ((DataGridPagerStyle)s).Mode != 0)
			{
				Mode = ((DataGridPagerStyle)s).Mode;
			}
			if (s.CheckBit(2097152) && ((DataGridPagerStyle)s).NextPageText != "&gt;")
			{
				NextPageText = ((DataGridPagerStyle)s).NextPageText;
			}
			if (s.CheckBit(4194304) && ((DataGridPagerStyle)s).PageButtonCount != 10)
			{
				PageButtonCount = ((DataGridPagerStyle)s).PageButtonCount;
			}
			if (s.CheckBit(8388608) && ((DataGridPagerStyle)s).Position != 0)
			{
				Position = ((DataGridPagerStyle)s).Position;
			}
			if (s.CheckBit(16777216) && ((DataGridPagerStyle)s).PrevPageText != "&lt;")
			{
				PrevPageText = ((DataGridPagerStyle)s).PrevPageText;
			}
			if (s.CheckBit(33554432) && !((DataGridPagerStyle)s).Visible)
			{
				Visible = ((DataGridPagerStyle)s).Visible;
			}
		}
	}

	/// <summary>Merges the style of the specified <see cref="T:System.Web.UI.WebControls.Style" /> object with this instance of the <see cref="T:System.Web.UI.WebControls.DataGridPagerStyle" /> class.</summary>
	/// <param name="s">The <see cref="T:System.Web.UI.WebControls.Style" /> to merge with. </param>
	public override void MergeWith(Style s)
	{
		base.MergeWith(s);
		if (s != null && !s.IsEmpty)
		{
			if (!CheckBit(1048576) && s.CheckBit(1048576) && ((DataGridPagerStyle)s).Mode != 0)
			{
				Mode = ((DataGridPagerStyle)s).Mode;
			}
			if (!CheckBit(2097152) && s.CheckBit(2097152) && ((DataGridPagerStyle)s).NextPageText != "&gt;")
			{
				NextPageText = ((DataGridPagerStyle)s).NextPageText;
			}
			if (!CheckBit(4194304) && s.CheckBit(4194304) && ((DataGridPagerStyle)s).PageButtonCount != 10)
			{
				PageButtonCount = ((DataGridPagerStyle)s).PageButtonCount;
			}
			if (!CheckBit(8388608) && s.CheckBit(8388608) && ((DataGridPagerStyle)s).Position != 0)
			{
				Position = ((DataGridPagerStyle)s).Position;
			}
			if (!CheckBit(16777216) && s.CheckBit(16777216) && ((DataGridPagerStyle)s).PrevPageText != "&lt;")
			{
				PrevPageText = ((DataGridPagerStyle)s).PrevPageText;
			}
			if (!CheckBit(33554432) && s.CheckBit(33554432) && !((DataGridPagerStyle)s).Visible)
			{
				Visible = ((DataGridPagerStyle)s).Visible;
			}
		}
	}

	/// <summary>Restores the <see cref="T:System.Web.UI.WebControls.DataGridPagerStyle" /> object to its default values.</summary>
	public override void Reset()
	{
		base.ViewState.Remove("Mode");
		base.ViewState.Remove("NextPageText");
		base.ViewState.Remove("PageButtonCount");
		base.ViewState.Remove("Position");
		base.ViewState.Remove("PrevPageText");
		base.ViewState.Remove("Visible");
		base.Reset();
	}
}
