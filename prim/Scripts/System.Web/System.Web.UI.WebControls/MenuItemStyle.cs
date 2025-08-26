using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Represents the style of a menu item in a <see cref="T:System.Web.UI.WebControls.Menu" /> control. This class cannot be inherited.</summary>
public sealed class MenuItemStyle : Style
{
	[Flags]
	private enum MenuItemStyles
	{
		HorizontalPadding = 0x10000,
		VerticalPadding = 0x20000,
		ItemSpacing = 0x40000
	}

	/// <summary>Gets or sets the amount of space to the left and right of the menu item's text.</summary>
	/// <returns>The amount of space (in pixels) to the left and right of the menu item's text. The default is 0.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is of type <see cref="F:System.Web.UI.WebControls.UnitType.Percentage" />.- or -The selected value is less than <see langword="0" />.</exception>
	[DefaultValue(typeof(Unit), "")]
	[NotifyParentProperty(true)]
	public Unit HorizontalPadding
	{
		get
		{
			if (CheckBit(65536))
			{
				return (Unit)base.ViewState["HorizontalPadding"];
			}
			return Unit.Empty;
		}
		set
		{
			base.ViewState["HorizontalPadding"] = value;
			SetBit(65536);
		}
	}

	/// <summary>Gets or sets the amount of space above and below a menu item's text.</summary>
	/// <returns>The amount of space (in pixels) above and below a menu item's text. The default is 0.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is of type <see cref="F:System.Web.UI.WebControls.UnitType.Percentage" />.- or -The selected value is less than <see langword="0" />.</exception>
	[DefaultValue(typeof(Unit), "")]
	[NotifyParentProperty(true)]
	public Unit VerticalPadding
	{
		get
		{
			if (CheckBit(131072))
			{
				return (Unit)base.ViewState["VerticalPadding"];
			}
			return Unit.Empty;
		}
		set
		{
			base.ViewState["VerticalPadding"] = value;
			SetBit(131072);
		}
	}

	/// <summary>Gets or sets the amount of vertical spacing between the menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> object is applied and its adjacent menu items.</summary>
	/// <returns>The amount of vertical spacing (in pixels) between the menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> object is applied and its adjacent menu items. The default is 0.</returns>
	[DefaultValue(typeof(Unit), "")]
	[NotifyParentProperty(true)]
	public Unit ItemSpacing
	{
		get
		{
			if (CheckBit(262144))
			{
				return (Unit)base.ViewState["ItemSpacing"];
			}
			return Unit.Empty;
		}
		set
		{
			base.ViewState["ItemSpacing"] = value;
			SetBit(262144);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> class.</summary>
	public MenuItemStyle()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> class using the specified state information.</summary>
	/// <param name="bag">A <see cref="T:System.Web.UI.StateBag" /> that represents the state bag in which menu item style information is stored.</param>
	public MenuItemStyle(StateBag bag)
		: base(bag)
	{
	}

	/// <summary>Copies the style properties of the specified <see cref="T:System.Web.UI.WebControls.Style" /> object into the current instance of the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> class.</summary>
	/// <param name="s">The <see cref="T:System.Web.UI.WebControls.Style" /> to copy.</param>
	public override void CopyFrom(Style s)
	{
		if (s == null || s.IsEmpty)
		{
			return;
		}
		base.CopyFrom(s);
		if (s is MenuItemStyle menuItemStyle)
		{
			if (menuItemStyle.CheckBit(65536))
			{
				HorizontalPadding = menuItemStyle.HorizontalPadding;
			}
			if (menuItemStyle.CheckBit(262144))
			{
				ItemSpacing = menuItemStyle.ItemSpacing;
			}
			if (menuItemStyle.CheckBit(131072))
			{
				VerticalPadding = menuItemStyle.VerticalPadding;
			}
		}
	}

	/// <summary>Combines the style properties of the specified <see cref="T:System.Web.UI.WebControls.Style" /> object with those of the current instance of the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> class.</summary>
	/// <param name="s">The <see cref="T:System.Web.UI.WebControls.Style" /> to combine settings with.</param>
	public override void MergeWith(Style s)
	{
		if (s == null || s.IsEmpty)
		{
			return;
		}
		base.MergeWith(s);
		if (s is MenuItemStyle menuItemStyle)
		{
			if (!CheckBit(65536) && menuItemStyle.CheckBit(65536))
			{
				HorizontalPadding = menuItemStyle.HorizontalPadding;
			}
			if (!CheckBit(262144) && menuItemStyle.CheckBit(262144))
			{
				ItemSpacing = menuItemStyle.ItemSpacing;
			}
			if (!CheckBit(131072) && menuItemStyle.CheckBit(131072))
			{
				VerticalPadding = menuItemStyle.VerticalPadding;
			}
		}
	}

	/// <summary>Returns the current instance of the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> class to its original state.</summary>
	public override void Reset()
	{
		base.ViewState.Remove("HorizontalPadding");
		base.ViewState.Remove("ItemSpacing");
		base.ViewState.Remove("VerticalPadding");
		base.Reset();
	}

	protected override void FillStyleAttributes(CssStyleCollection attributes, IUrlResolutionService urlResolver)
	{
		base.FillStyleAttributes(attributes, urlResolver);
		if (CheckBit(65536))
		{
			attributes.Add(HtmlTextWriterStyle.PaddingLeft, HorizontalPadding.ToString());
			attributes.Add(HtmlTextWriterStyle.PaddingRight, HorizontalPadding.ToString());
		}
		if (CheckBit(131072))
		{
			attributes.Add(HtmlTextWriterStyle.PaddingTop, VerticalPadding.ToString());
			attributes.Add(HtmlTextWriterStyle.PaddingBottom, VerticalPadding.ToString());
		}
	}
}
