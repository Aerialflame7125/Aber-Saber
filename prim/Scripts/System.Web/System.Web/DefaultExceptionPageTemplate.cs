using System.Collections.Generic;

namespace System.Web;

internal sealed class DefaultExceptionPageTemplate : ExceptionPageTemplate
{
	public override void Init()
	{
		List<ExceptionPageTemplateFragment> list = base.Fragments;
		list.Add(new ExceptionPageTemplateFragment
		{
			Name = "PageTop",
			ResourceName = "ErrorTemplateCommon_Top.html",
			MacroNames = new List<string> { "Title", "ExceptionType", "ExceptionMessage", "Description", "Details" }
		});
		list.Add(new ExceptionPageTemplateFragment
		{
			Name = "PageCustomErrorDefault",
			ResourceName = "DefaultErrorTemplate_CustomErrorDefault.html",
			ValidForPageType = ExceptionPageTemplateType.CustomErrorDefault
		});
		list.Add(new ExceptionPageTemplateFragment
		{
			Name = "PageStandard",
			ResourceName = "DefaultErrorTemplate_StandardPage.html",
			ValidForPageType = ExceptionPageTemplateType.Standard,
			MacroNames = new List<string> { "StackTrace" }
		});
		list.Add(new ExceptionPageTemplateFragment
		{
			Name = "PageHtmlizedException",
			ResourceName = "HtmlizedExceptionPage_Top.html",
			ValidForPageType = ExceptionPageTemplateType.Htmlized,
			MacroNames = new List<string> { "StackTrace", "HtmlizedExceptionOrigin", "HtmlizedExceptionSourceFile" }
		});
		list.Add(new ExceptionPageTemplateFragment
		{
			Name = "File Short Source",
			ResourceName = "HtmlizedExceptionPage_FileShortSource.html",
			ValidForPageType = ExceptionPageTemplateType.SourceError,
			MacroNames = new List<string> { "HtmlizedExceptionShortSource", "HtmlizedExceptionSourceFile", "HtmlizedExceptionErrorLines" }
		});
		list.Add(new ExceptionPageTemplateFragment
		{
			Name = "File Long Source",
			ResourceName = "HtmlizedExceptionPage_FileLongSource.html",
			ValidForPageType = ExceptionPageTemplateType.SourceError,
			MacroNames = new List<string> { "HtmlizedExceptionLongSource", "HtmlizedExceptionSourceFile", "HtmlizedExceptionErrorLines" },
			RequiredMacros = new List<string> { "HtmlizedExceptionLongSource" }
		});
		list.Add(new ExceptionPageTemplateFragment
		{
			Name = "Compiler Output",
			ResourceName = "HtmlizedExceptionPage_CompilerOutput.html",
			ValidForPageType = ExceptionPageTemplateType.SourceError,
			MacroNames = new List<string> { "HtmlizedExceptionCompilerOutput", "HtmlizedExceptionSourceFile", "HtmlizedExceptionErrorLines" },
			RequiredMacros = new List<string> { "HtmlizedExceptionCompilerOutput" }
		});
		list.Add(new ExceptionPageTemplateFragment
		{
			Name = "PageBottom",
			ResourceName = "ErrorTemplateCommon_Bottom.html",
			MacroNames = new List<string> { "RuntimeVersionInformation", "AspNetVersionInformation", "FullStackTrace" }
		});
	}
}
