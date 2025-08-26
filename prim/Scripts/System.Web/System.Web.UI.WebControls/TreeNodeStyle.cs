using System.ComponentModel;
using System.Drawing.Design;

namespace System.Web.UI.WebControls;

/// <summary>Represents the style of a node in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
public sealed class TreeNodeStyle : Style
{
	[Flags]
	private enum TreeNodeStyles
	{
		ChildNodesPadding = 0x10000,
		HorizontalPadding = 0x20000,
		ImageUrl = 0x40000,
		NodeSpacing = 0x80000,
		VerticalPadding = 0x100000
	}

	private const string CHILD_PADD = "ChildNodesPadding";

	private const string HORZ_PADD = "HorizontalPadding";

	private const string IMG_URL = "ImageUrl";

	private const string SPACING = "NodeSpacing";

	private const string VERT_PADD = "VerticalPadding";

	/// <summary>Gets or sets the URL to an image that is displayed next to the node.</summary>
	/// <returns>The URL to a custom image that is displayed next to the node. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.TreeNodeStyle.ImageUrl" /> property is not set.</returns>
	/// <exception cref="T:System.ArgumentNullException">The selected value is <see langword="null" />.</exception>
	[DefaultValue("")]
	[UrlProperty]
	[NotifyParentProperty(true)]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public string ImageUrl
	{
		get
		{
			if (!CheckBit(262144))
			{
				return string.Empty;
			}
			return base.ViewState.GetString("ImageUrl", string.Empty);
		}
		set
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			base.ViewState["ImageUrl"] = value;
			SetBit(262144);
		}
	}

	/// <summary>Gets or sets the amount of space between a parent node and a child node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> class is applied.</summary>
	/// <returns>The amount of space, in pixels, that is above and below the child nodes section of a parent node. The default is 0 pixels.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is of type <see cref="F:System.Web.UI.WebControls.UnitType.Percentage" /> or is less than <see langword="0" />.</exception>
	[DefaultValue(0)]
	[NotifyParentProperty(true)]
	public Unit ChildNodesPadding
	{
		get
		{
			if (!CheckBit(65536))
			{
				return 0;
			}
			if (base.ViewState["ChildNodesPadding"] != null)
			{
				return (Unit)base.ViewState["ChildNodesPadding"];
			}
			return 0;
		}
		set
		{
			base.ViewState["ChildNodesPadding"] = value;
			SetBit(65536);
		}
	}

	/// <summary>Gets or sets the amount of space to the left and right of the text in the node.</summary>
	/// <returns>The amount of space, in pixels, that is to the left and right of the node's text. The default is 0 pixels.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is of type <see cref="F:System.Web.UI.WebControls.UnitType.Percentage" /> or is less than <see langword="0" />.</exception>
	[DefaultValue(0)]
	[NotifyParentProperty(true)]
	public Unit HorizontalPadding
	{
		get
		{
			if (!CheckBit(131072))
			{
				return 0;
			}
			if (base.ViewState["HorizontalPadding"] != null)
			{
				return (Unit)base.ViewState["HorizontalPadding"];
			}
			return 0;
		}
		set
		{
			base.ViewState["HorizontalPadding"] = value;
			SetBit(131072);
		}
	}

	/// <summary>Gets or sets the amount of space above and below the text for a node.</summary>
	/// <returns>The amount of space, in pixels, above and below a node's text. The default is 0 pixels.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is of type <see cref="F:System.Web.UI.WebControls.UnitType.Percentage" /> or is less than <see langword="0" />.</exception>
	[DefaultValue(0)]
	[NotifyParentProperty(true)]
	public Unit VerticalPadding
	{
		get
		{
			if (!CheckBit(1048576))
			{
				return 0;
			}
			if (base.ViewState["VerticalPadding"] != null)
			{
				return (Unit)base.ViewState["VerticalPadding"];
			}
			return 0;
		}
		set
		{
			base.ViewState["VerticalPadding"] = value;
			SetBit(1048576);
		}
	}

	/// <summary>Gets or sets the amount of vertical spacing between the node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object is applied and its adjacent nodes.</summary>
	/// <returns>The amount of vertical space, in pixels, between the node to which the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> is applied and its adjacent nodes at the same level. The default is 0 pixels.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is of type <see cref="F:System.Web.UI.WebControls.UnitType.Percentage" /> or is less than <see langword="0" />.</exception>
	[DefaultValue(0)]
	[NotifyParentProperty(true)]
	public Unit NodeSpacing
	{
		get
		{
			if (!CheckBit(524288))
			{
				return 0;
			}
			if (base.ViewState["NodeSpacing"] != null)
			{
				return (Unit)base.ViewState["NodeSpacing"];
			}
			return 0;
		}
		set
		{
			base.ViewState["NodeSpacing"] = value;
			SetBit(524288);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> class.</summary>
	public TreeNodeStyle()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> class with the specified <see cref="T:System.Web.UI.StateBag" /> object information.</summary>
	/// <param name="bag">A <see cref="T:System.Web.UI.StateBag" /> that stores the style information.</param>
	public TreeNodeStyle(StateBag bag)
		: base(bag)
	{
	}

	/// <summary>Copies the style properties of the specified <see cref="T:System.Web.UI.WebControls.Style" /> object into the current <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object.</summary>
	/// <param name="s">The <see cref="T:System.Web.UI.WebControls.Style" /> to copy. </param>
	public override void CopyFrom(Style s)
	{
		if (s == null || s.IsEmpty)
		{
			return;
		}
		base.CopyFrom(s);
		if (s is TreeNodeStyle treeNodeStyle)
		{
			if (treeNodeStyle.CheckBit(65536))
			{
				ChildNodesPadding = treeNodeStyle.ChildNodesPadding;
			}
			if (treeNodeStyle.CheckBit(131072))
			{
				HorizontalPadding = treeNodeStyle.HorizontalPadding;
			}
			if (treeNodeStyle.CheckBit(262144))
			{
				ImageUrl = treeNodeStyle.ImageUrl;
			}
			if (treeNodeStyle.CheckBit(524288))
			{
				NodeSpacing = treeNodeStyle.NodeSpacing;
			}
			if (treeNodeStyle.CheckBit(1048576))
			{
				VerticalPadding = treeNodeStyle.VerticalPadding;
			}
		}
	}

	/// <summary>Combines the style properties of the specified <see cref="T:System.Web.UI.WebControls.Style" /> object with the style properties of the current <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object.</summary>
	/// <param name="s">The <see cref="T:System.Web.UI.WebControls.Style" /> that will merge with the current node's settings. </param>
	public override void MergeWith(Style s)
	{
		if (s == null || s.IsEmpty)
		{
			return;
		}
		if (IsEmpty)
		{
			CopyFrom(s);
			return;
		}
		base.MergeWith(s);
		if (s is TreeNodeStyle treeNodeStyle)
		{
			if (treeNodeStyle.CheckBit(65536) && !CheckBit(65536))
			{
				ChildNodesPadding = treeNodeStyle.ChildNodesPadding;
			}
			if (treeNodeStyle.CheckBit(131072) && !CheckBit(131072))
			{
				HorizontalPadding = treeNodeStyle.HorizontalPadding;
			}
			if (treeNodeStyle.CheckBit(262144) && !CheckBit(262144))
			{
				ImageUrl = treeNodeStyle.ImageUrl;
			}
			if (treeNodeStyle.CheckBit(524288) && !CheckBit(524288))
			{
				NodeSpacing = treeNodeStyle.NodeSpacing;
			}
			if (treeNodeStyle.CheckBit(1048576) && !CheckBit(1048576))
			{
				VerticalPadding = treeNodeStyle.VerticalPadding;
			}
		}
	}

	/// <summary>Returns the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object to its original state.</summary>
	public override void Reset()
	{
		if (CheckBit(65536))
		{
			base.ViewState.Remove("ChildNodesPadding");
		}
		if (CheckBit(131072))
		{
			base.ViewState.Remove("HorizontalPadding");
		}
		if (CheckBit(262144))
		{
			base.ViewState.Remove("ImageUrl");
		}
		if (CheckBit(524288))
		{
			base.ViewState.Remove("NodeSpacing");
		}
		if (CheckBit(1048576))
		{
			base.ViewState.Remove("VerticalPadding");
		}
		base.Reset();
	}

	protected override void FillStyleAttributes(CssStyleCollection attributes, IUrlResolutionService urlResolver)
	{
		base.FillStyleAttributes(attributes, urlResolver);
		if (CheckBit(131072))
		{
			attributes.Add(HtmlTextWriterStyle.PaddingLeft, HorizontalPadding.ToString());
			attributes.Add(HtmlTextWriterStyle.PaddingRight, HorizontalPadding.ToString());
		}
		if (CheckBit(1048576))
		{
			attributes.Add(HtmlTextWriterStyle.PaddingTop, VerticalPadding.ToString());
			attributes.Add(HtmlTextWriterStyle.PaddingBottom, VerticalPadding.ToString());
		}
	}
}
