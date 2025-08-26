using System.Collections;

namespace System.Web.UI.WebControls;

internal class ContentBuilderInternal : TemplateBuilder
{
	private string placeHolderID;

	public string ContentPlaceHolderID => placeHolderID;

	public override void Init(TemplateParser parser, ControlBuilder parentBuilder, Type type, string tagName, string ID, IDictionary attribs)
	{
		base.Init(parser, parentBuilder, type, tagName, ID, attribs);
		placeHolderID = attribs["ContentPlaceHolderID"] as string;
		if (string.IsNullOrEmpty(placeHolderID))
		{
			throw new HttpException("Missing required 'ContentPlaceHolderID' attribute");
		}
	}
}
