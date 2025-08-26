using System.Collections;

namespace System.Web.UI;

internal interface ITagNameToTypeMapper
{
	Type GetControlType(string tagName, IDictionary attribs);
}
