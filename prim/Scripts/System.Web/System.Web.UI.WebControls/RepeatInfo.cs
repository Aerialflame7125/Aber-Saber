using System.Diagnostics;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Encapsulates the information used to render a list control that repeats a list of items. This class cannot be inherited.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class RepeatInfo
{
	private bool outer_table_implied;

	private int repeat_cols;

	private RepeatDirection dir = RepeatDirection.Vertical;

	private RepeatLayout layout;

	private string caption = string.Empty;

	private TableCaptionAlign captionAlign;

	private bool useAccessibleHeader;

	/// <summary>Gets or sets a value indicating whether items should be rendered as if they are contained in a table.</summary>
	/// <returns>
	///     <see langword="true" /> if the items should be rendered as if they are contained in a table; otherwise, <see langword="false" />.</returns>
	public bool OuterTableImplied
	{
		get
		{
			return outer_table_implied;
		}
		set
		{
			outer_table_implied = value;
		}
	}

	/// <summary>Gets or sets the number of columns to render.</summary>
	/// <returns>The number of columns to render.</returns>
	public int RepeatColumns
	{
		get
		{
			return repeat_cols;
		}
		set
		{
			repeat_cols = value;
		}
	}

	/// <summary>Gets or sets a value that specifies whether the items are displayed vertically or horizontally.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.RepeatDirection" /> enumeration values.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified direction is not one of the <see cref="T:System.Web.UI.WebControls.RepeatDirection" /> enumeration values. </exception>
	public RepeatDirection RepeatDirection
	{
		get
		{
			return dir;
		}
		set
		{
			if (value != 0 && value != RepeatDirection.Vertical)
			{
				throw new ArgumentOutOfRangeException();
			}
			dir = value;
		}
	}

	/// <summary>Gets or sets a value specifying whether items are displayed in a table.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.RepeatLayout" /> enumeration values.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified layout is not one of the <see cref="T:System.Web.UI.WebControls.RepeatLayout" /> enumeration values. </exception>
	public RepeatLayout RepeatLayout
	{
		get
		{
			return layout;
		}
		set
		{
			if (value < RepeatLayout.Table || value > RepeatLayout.OrderedList)
			{
				throw new ArgumentOutOfRangeException();
			}
			layout = value;
		}
	}

	/// <summary>Gets or sets the <see cref="P:System.Web.UI.WebControls.Table.Caption" /> property if the control is rendered as a <see cref="T:System.Web.UI.WebControls.Table" />.</summary>
	/// <returns>A string that specifies the <see cref="T:System.Web.UI.WebControls.Table" /> caption.</returns>
	[WebSysDescription("")]
	[WebCategory("Accessibility")]
	public string Caption
	{
		get
		{
			return caption;
		}
		set
		{
			caption = value;
		}
	}

	/// <summary>Gets or sets the alignment of the caption, if the <see cref="T:System.Web.UI.WebControls.RepeatInfo" /> is rendered as a <see cref="T:System.Web.UI.WebControls.Table" />.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableCaptionAlign" /> value for the rendered table. The default value is <see cref="F:System.Web.UI.WebControls.TableCaptionAlign.NotSet" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is not one of the <see cref="T:System.Web.UI.WebControls.TableCaptionAlign" /> values.</exception>
	[WebSysDescription("")]
	[WebCategory("Accessibility")]
	public TableCaptionAlign CaptionAlign
	{
		get
		{
			return captionAlign;
		}
		set
		{
			captionAlign = value;
		}
	}

	/// <summary>Gets or sets a value to indicate whether to add a <see cref="P:System.Web.UI.WebControls.TableHeaderCell.Scope" /> attribute when the control is rendered as a <see cref="T:System.Web.UI.WebControls.Table" />.</summary>
	/// <returns>
	///     <see langword="true" /> if a "scope" attribute is to be added, otherwise, <see langword="false" />.</returns>
	[WebSysDescription("")]
	[WebCategory("Accessibility")]
	public bool UseAccessibleHeader
	{
		get
		{
			return useAccessibleHeader;
		}
		set
		{
			useAccessibleHeader = value;
		}
	}

	/// <summary>Renders a list control that repeats a list of items, using the specified information.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream used to render HTML content on the client.</param>
	/// <param name="user">An <see cref="T:System.Web.UI.WebControls.IRepeatInfoUser" /> implemented object that represents the control to render.</param>
	/// <param name="controlStyle">A <see cref="T:System.Web.UI.WebControls.Style" /> that represents the style in which to display the items.</param>
	/// <param name="baseControl">The control from which to copy base attributes.</param>
	public void RenderRepeater(HtmlTextWriter writer, IRepeatInfoUser user, Style controlStyle, WebControl baseControl)
	{
		RepeatLayout repeatLayout = RepeatLayout;
		bool flag = repeatLayout == RepeatLayout.OrderedList || repeatLayout == RepeatLayout.UnorderedList;
		if (flag)
		{
			if (user != null && (user.HasHeader || user.HasFooter || user.HasSeparators))
			{
				throw new InvalidOperationException("The UnorderedList and OrderedList layouts do not support headers, footers or separators.");
			}
			if (OuterTableImplied)
			{
				throw new InvalidOperationException("The UnorderedList and OrderedList layouts do not support implied outer tables.");
			}
			if (RepeatColumns > 1)
			{
				throw new InvalidOperationException("The UnorderedList and OrderedList layouts do not support multi-column layouts.");
			}
		}
		if (RepeatDirection == RepeatDirection.Vertical)
		{
			if (flag)
			{
				RenderList(writer, user, controlStyle, baseControl);
			}
			else
			{
				RenderVert(writer, user, controlStyle, baseControl);
			}
			return;
		}
		if (flag)
		{
			throw new InvalidOperationException("The UnorderedList and OrderedList layouts only support vertical layout.");
		}
		RenderHoriz(writer, user, controlStyle, baseControl);
	}

	private void RenderBr(HtmlTextWriter w)
	{
		w.Write("<br />");
	}

	private void RenderList(HtmlTextWriter w, IRepeatInfoUser user, Style controlStyle, WebControl baseControl)
	{
		int repeatedItemCount = user.RepeatedItemCount;
		RenderBeginTag(w, controlStyle, baseControl);
		for (int i = 0; i < repeatedItemCount; i++)
		{
			w.RenderBeginTag(HtmlTextWriterTag.Li);
			user.RenderItem(ListItemType.Item, i, this, w);
			w.RenderEndTag();
			w.WriteLine();
		}
		w.RenderEndTag();
	}

	private void RenderVert(HtmlTextWriter w, IRepeatInfoUser user, Style controlStyle, WebControl baseControl)
	{
		int repeatedItemCount = user.RepeatedItemCount;
		int num = ((RepeatColumns == 0) ? 1 : RepeatColumns);
		int num2 = (repeatedItemCount + num - 1) / num;
		bool hasSeparators = user.HasSeparators;
		bool outerTableImplied = OuterTableImplied;
		int num3 = num * ((!hasSeparators || num == 1) ? 1 : 2);
		bool flag = RepeatLayout == RepeatLayout.Table && !outerTableImplied;
		bool flag2 = true;
		bool flag3 = true;
		if (!outerTableImplied)
		{
			RenderBeginTag(w, controlStyle, baseControl);
		}
		if (Caption.Length > 0)
		{
			if (CaptionAlign != 0)
			{
				w.AddAttribute(HtmlTextWriterAttribute.Align, CaptionAlign.ToString());
			}
			w.RenderBeginTag(HtmlTextWriterTag.Caption);
			w.Write(Caption);
			w.RenderEndTag();
		}
		if (user.HasHeader)
		{
			if (outerTableImplied)
			{
				user.RenderItem(ListItemType.Header, -1, this, w);
			}
			else if (flag)
			{
				w.RenderBeginTag(HtmlTextWriterTag.Tr);
				if (num3 != 1)
				{
					w.AddAttribute(HtmlTextWriterAttribute.Colspan, num3.ToString(), fEncode: false);
				}
				if (UseAccessibleHeader)
				{
					w.AddAttribute("scope", "col", fEndode: false);
				}
				user.GetItemStyle(ListItemType.Header, -1)?.AddAttributesToRender(w);
				if (UseAccessibleHeader)
				{
					w.RenderBeginTag(HtmlTextWriterTag.Th);
				}
				else
				{
					w.RenderBeginTag(HtmlTextWriterTag.Td);
				}
				user.RenderItem(ListItemType.Header, -1, this, w);
				w.RenderEndTag();
				w.RenderEndTag();
			}
			else
			{
				user.RenderItem(ListItemType.Header, -1, this, w);
				RenderBr(w);
			}
		}
		for (int i = 0; i < num2; i++)
		{
			if (flag)
			{
				w.RenderBeginTag(HtmlTextWriterTag.Tr);
			}
			for (int j = 0; j < num; j++)
			{
				int num4 = index_vert(num2, num, i, j, repeatedItemCount);
				if (!flag2 && num4 >= repeatedItemCount)
				{
					continue;
				}
				if (flag)
				{
					Style style = null;
					if (num4 < repeatedItemCount)
					{
						style = user.GetItemStyle(ListItemType.Item, num4);
					}
					style?.AddAttributesToRender(w);
					w.RenderBeginTag(HtmlTextWriterTag.Td);
				}
				if (num4 < repeatedItemCount)
				{
					user.RenderItem(ListItemType.Item, num4, this, w);
				}
				if (flag)
				{
					w.RenderEndTag();
				}
				if (!hasSeparators || num == 1)
				{
					continue;
				}
				if (flag)
				{
					if (num4 < repeatedItemCount - 1)
					{
						user.GetItemStyle(ListItemType.Separator, num4)?.AddAttributesToRender(w);
					}
					if (num4 < repeatedItemCount - 1 || flag3)
					{
						w.RenderBeginTag(HtmlTextWriterTag.Td);
					}
				}
				if (num4 < repeatedItemCount - 1)
				{
					user.RenderItem(ListItemType.Separator, num4, this, w);
				}
				if (flag && (num4 < repeatedItemCount - 1 || flag3))
				{
					w.RenderEndTag();
				}
			}
			if (!outerTableImplied)
			{
				if (flag)
				{
					w.RenderEndTag();
				}
				else if (i != num2 - 1)
				{
					RenderBr(w);
				}
			}
			if (hasSeparators && i != num2 - 1 && num == 1)
			{
				if (flag)
				{
					w.RenderBeginTag(HtmlTextWriterTag.Tr);
					user.GetItemStyle(ListItemType.Separator, i)?.AddAttributesToRender(w);
					w.RenderBeginTag(HtmlTextWriterTag.Td);
				}
				user.RenderItem(ListItemType.Separator, i, this, w);
				if (flag)
				{
					w.RenderEndTag();
					w.RenderEndTag();
				}
				else if (!outerTableImplied)
				{
					RenderBr(w);
				}
			}
		}
		if (user.HasFooter)
		{
			if (outerTableImplied)
			{
				user.RenderItem(ListItemType.Footer, -1, this, w);
			}
			else if (flag)
			{
				w.RenderBeginTag(HtmlTextWriterTag.Tr);
				if (num3 != 1)
				{
					w.AddAttribute(HtmlTextWriterAttribute.Colspan, num3.ToString(), fEncode: false);
				}
				user.GetItemStyle(ListItemType.Footer, -1)?.AddAttributesToRender(w);
				w.RenderBeginTag(HtmlTextWriterTag.Td);
				user.RenderItem(ListItemType.Footer, -1, this, w);
				w.RenderEndTag();
				w.RenderEndTag();
			}
			else
			{
				if (repeatedItemCount != 0)
				{
					RenderBr(w);
				}
				user.RenderItem(ListItemType.Footer, -1, this, w);
			}
		}
		if (!outerTableImplied)
		{
			w.RenderEndTag();
		}
	}

	private void RenderHoriz(HtmlTextWriter w, IRepeatInfoUser user, Style controlStyle, WebControl baseControl)
	{
		int repeatedItemCount = user.RepeatedItemCount;
		int num = ((RepeatColumns == 0) ? repeatedItemCount : RepeatColumns);
		int num2 = ((num != 0) ? ((repeatedItemCount + num - 1) / num) : 0);
		bool hasSeparators = user.HasSeparators;
		int num3 = num * ((!hasSeparators) ? 1 : 2);
		bool flag = RepeatLayout == RepeatLayout.Table;
		bool flag2 = true;
		bool flag3 = true;
		RenderBeginTag(w, controlStyle, baseControl);
		if (Caption.Length > 0)
		{
			if (CaptionAlign != 0)
			{
				w.AddAttribute(HtmlTextWriterAttribute.Align, CaptionAlign.ToString());
			}
			w.RenderBeginTag(HtmlTextWriterTag.Caption);
			w.Write(Caption);
			w.RenderEndTag();
		}
		if (user.HasHeader)
		{
			if (flag)
			{
				w.RenderBeginTag(HtmlTextWriterTag.Tr);
				if (num3 != 1)
				{
					w.AddAttribute(HtmlTextWriterAttribute.Colspan, num3.ToString(), fEncode: false);
				}
				if (UseAccessibleHeader)
				{
					w.AddAttribute("scope", "col", fEndode: false);
				}
				user.GetItemStyle(ListItemType.Header, -1)?.AddAttributesToRender(w);
				if (UseAccessibleHeader)
				{
					w.RenderBeginTag(HtmlTextWriterTag.Th);
				}
				else
				{
					w.RenderBeginTag(HtmlTextWriterTag.Td);
				}
				user.RenderItem(ListItemType.Header, -1, this, w);
				w.RenderEndTag();
				w.RenderEndTag();
			}
			else
			{
				user.RenderItem(ListItemType.Header, -1, this, w);
				if (!flag && RepeatColumns != 0 && repeatedItemCount != 0)
				{
					RenderBr(w);
				}
			}
		}
		for (int i = 0; i < num2; i++)
		{
			if (flag)
			{
				w.RenderBeginTag(HtmlTextWriterTag.Tr);
			}
			for (int j = 0; j < num; j++)
			{
				int num4 = i * num + j;
				if (!flag2 && num4 >= repeatedItemCount)
				{
					continue;
				}
				if (flag)
				{
					Style style = null;
					if (num4 < repeatedItemCount)
					{
						style = user.GetItemStyle(ListItemType.Item, num4);
					}
					style?.AddAttributesToRender(w);
					w.RenderBeginTag(HtmlTextWriterTag.Td);
				}
				if (num4 < repeatedItemCount)
				{
					user.RenderItem(ListItemType.Item, num4, this, w);
				}
				if (flag)
				{
					w.RenderEndTag();
				}
				if (!hasSeparators)
				{
					continue;
				}
				if (flag)
				{
					if (num4 < repeatedItemCount - 1)
					{
						user.GetItemStyle(ListItemType.Separator, num4)?.AddAttributesToRender(w);
					}
					if (num4 < repeatedItemCount - 1 || flag3)
					{
						w.RenderBeginTag(HtmlTextWriterTag.Td);
					}
				}
				if (num4 < repeatedItemCount - 1)
				{
					user.RenderItem(ListItemType.Separator, num4, this, w);
				}
				if (flag && (num4 < repeatedItemCount - 1 || flag3))
				{
					w.RenderEndTag();
				}
			}
			if (flag)
			{
				w.RenderEndTag();
			}
			else if (i != num2 - 1 || RepeatColumns != 0)
			{
				RenderBr(w);
			}
		}
		if (user.HasFooter)
		{
			if (flag)
			{
				w.RenderBeginTag(HtmlTextWriterTag.Tr);
				if (num3 != 1)
				{
					w.AddAttribute(HtmlTextWriterAttribute.Colspan, num3.ToString(), fEncode: false);
				}
				user.GetItemStyle(ListItemType.Footer, -1)?.AddAttributesToRender(w);
				w.RenderBeginTag(HtmlTextWriterTag.Td);
				user.RenderItem(ListItemType.Footer, -1, this, w);
				w.RenderEndTag();
				w.RenderEndTag();
			}
			else
			{
				user.RenderItem(ListItemType.Footer, -1, this, w);
			}
		}
		w.RenderEndTag();
	}

	private int index_vert(int rows, int cols, int r, int c, int items)
	{
		int num = items % cols;
		if (num == 0)
		{
			num = cols;
		}
		if (r == rows - 1 && c >= num)
		{
			return items;
		}
		if (c > num)
		{
			return num * rows + (c - num) * (rows - 1) + r;
		}
		return rows * c + r;
	}

	private void RenderBeginTag(HtmlTextWriter w, Style s, WebControl wc)
	{
		WebControl webControl = RepeatLayout switch
		{
			RepeatLayout.Table => new Table(), 
			RepeatLayout.Flow => new Label(), 
			RepeatLayout.OrderedList => new WebControl(HtmlTextWriterTag.Ol), 
			RepeatLayout.UnorderedList => new WebControl(HtmlTextWriterTag.Ul), 
			_ => throw new InvalidOperationException($"Unsupported RepeatLayout value '{RepeatLayout}'."), 
		};
		webControl.ID = wc.ClientID;
		webControl.CopyBaseAttributes(wc);
		webControl.ApplyStyle(s);
		webControl.Enabled = wc.IsEnabled;
		webControl.RenderBeginTag(w);
	}

	[Conditional("DEBUG_REPEAT_INFO")]
	internal void PrintValues(IRepeatInfoUser riu)
	{
		string text = string.Format("Layout {0}; Direction {1}; Cols {2}; OuterTableImplied {3}\nUser: itms {4}, hdr {5}; ftr {6}; sep {7}", RepeatLayout, RepeatDirection, RepeatColumns, OuterTableImplied, riu.RepeatedItemCount, riu.HasSeparators, riu.HasHeader, riu.HasFooter, riu.HasSeparators);
		Console.WriteLine(text);
		if (HttpContext.Current != null)
		{
			HttpContext.Current.Trace.Write(text);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.RepeatInfo" /> class.</summary>
	public RepeatInfo()
	{
	}
}
