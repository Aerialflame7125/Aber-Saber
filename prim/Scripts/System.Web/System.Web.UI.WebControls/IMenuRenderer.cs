using System.Text;
using System.Web.UI.HtmlControls;

namespace System.Web.UI.WebControls;

internal interface IMenuRenderer
{
	HtmlTextWriterTag Tag { get; }

	void AddAttributesToRender(HtmlTextWriter writer);

	void PreRender(Page page, HtmlHead head, ClientScriptManager csm, string cmenu, StringBuilder script);

	void RenderBeginTag(HtmlTextWriter writer, string skipLinkText);

	void RenderEndTag(HtmlTextWriter writer);

	void RenderContents(HtmlTextWriter writer);

	void RenderItemContent(HtmlTextWriter writer, MenuItem item, bool isDynamicItem);

	void RenderMenuBeginTag(HtmlTextWriter writer, bool dynamic, int menuLevel);

	void RenderMenuBody(HtmlTextWriter writer, MenuItemCollection items, bool vertical, bool dynamic, bool notLast);

	void RenderMenuEndTag(HtmlTextWriter writer, bool dynamic, int menuLevel);

	void RenderMenuItem(HtmlTextWriter writer, MenuItem item, bool notLast, bool isFirst);

	bool IsDynamicItem(MenuItem item);

	bool IsDynamicItem(Menu owner, MenuItem item);
}
