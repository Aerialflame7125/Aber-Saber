using System.Collections.Generic;
using System.Text;

namespace System.Web;

internal abstract class ExceptionPageTemplate
{
	public const string Template_PageTopName = "PageTop";

	public const string Template_PageBottomName = "PageBottom";

	public const string Template_PageStandardName = "PageStandard";

	public const string Template_PageCustomErrorDefaultName = "PageCustomErrorDefault";

	public const string Template_PageHtmlizedExceptionName = "PageHtmlizedException";

	public const string Template_PageTitleName = "Title";

	public const string Template_ExceptionTypeName = "ExceptionType";

	public const string Template_ExceptionMessageName = "ExceptionMessage";

	public const string Template_DescriptionName = "Description";

	public const string Template_DetailsName = "Details";

	public const string Template_RuntimeVersionInformationName = "RuntimeVersionInformation";

	public const string Template_AspNetVersionInformationName = "AspNetVersionInformation";

	public const string Template_StackTraceName = "StackTrace";

	public const string Template_FullStackTraceName = "FullStackTrace";

	public const string Template_HtmlizedExceptionOriginName = "HtmlizedExceptionOrigin";

	public const string Template_HtmlizedExceptionShortSourceName = "HtmlizedExceptionShortSource";

	public const string Template_HtmlizedExceptionLongSourceName = "HtmlizedExceptionLongSource";

	public const string Template_HtmlizedExceptionSourceFileName = "HtmlizedExceptionSourceFile";

	public const string Template_HtmlizedExceptionErrorLinesName = "HtmlizedExceptionErrorLines";

	public const string Template_HtmlizedExceptionCompilerOutputName = "HtmlizedExceptionCompilerOutput";

	private List<ExceptionPageTemplateFragment> fragments;

	public List<ExceptionPageTemplateFragment> Fragments
	{
		get
		{
			if (fragments == null)
			{
				fragments = new List<ExceptionPageTemplateFragment>();
			}
			return fragments;
		}
	}

	public abstract void Init();

	private void InitFragments(ExceptionPageTemplateValues values)
	{
		foreach (ExceptionPageTemplateFragment fragment in fragments)
		{
			fragment?.Init(values);
		}
	}

	public string Render(ExceptionPageTemplateValues values, ExceptionPageTemplateType pageType)
	{
		if (values == null)
		{
			throw new ArgumentNullException("values");
		}
		StringBuilder sb = new StringBuilder();
		Render(values, pageType, delegate(string text)
		{
			sb.Append(text);
		});
		return sb.ToString();
	}

	public void Render(HttpResponse response, ExceptionPageTemplateValues values, ExceptionPageTemplateType pageType)
	{
		if (response != null)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			Render(values, pageType, delegate(string text)
			{
				response.Write(text);
			});
		}
	}

	private void Render(ExceptionPageTemplateValues values, ExceptionPageTemplateType pageType, Action<string> writer)
	{
		if (fragments == null || fragments.Count == 0 || values.Count == 0)
		{
			return;
		}
		InitFragments(values);
		foreach (ExceptionPageTemplateFragment fragment in fragments)
		{
			if (fragment != null && (fragment.ValidForPageType & pageType) != 0)
			{
				string text = values.Get(fragment.Name);
				if (text != null && fragment.Visible(values))
				{
					writer(fragment.ReplaceMacros(text, values));
				}
			}
		}
	}
}
