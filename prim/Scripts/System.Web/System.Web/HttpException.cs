using System.Collections.Specialized;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Web.Compilation;
using System.Web.Util;

namespace System.Web;

/// <summary>Describes an exception that occurred during the processing of HTTP requests.</summary>
[Serializable]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class HttpException : ExternalException
{
	private const string DEFAULT_DESCRIPTION_TEXT = "Error processing request.";

	private const string ERROR_404_DESCRIPTION = "The resource you are looking for (or one of its dependencies) could have been removed, had its name changed, or is temporarily unavailable.  Please review the following URL and make sure that it is spelled correctly.";

	private int webEventCode;

	private int http_code = 500;

	private string resource_name;

	private string description;

	private ExceptionPageTemplate pageTemplate;

	private const string DoubleFaultExceptionMessage = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\n<head>\n<style type=\"text/css\">\nbody { background-color: #FFFFFF; font-size: .75em; font-family: Verdana, Helvetica, Sans-Serif; margin: 0; padding: 0; color: #696969; }\na:link { color: #000000; text-decoration: underline; }\na:visited { color: #000000; }\na:hover { color: #000000; text-decoration: none; }\na:active { color: #12eb87; }\np, ul { margin-bottom: 20px; line-height: 1.6em; }\npre { font-size: 1.2em; margin-left: 20px; margin-top: 0px; }\nh1, h2, h3, h4, h5, h6 { font-size: 1.6em; color: #000; font-family: Arial, Helvetica, sans-serif; }\nh1 { font-weight: bold; margin-bottom: 0; margin-top: 0; padding-bottom: 0; }\nh2 { font-size: 1em; padding: 0 0 0px 0; color: #696969; font-weight: normal; margin-top: 0; margin-bottom: 20px; }\nh3 { font-size: 1.2em; }\nh4 { font-size: 1.1em; }\nh5, h6 { font-size: 1em; }\n#header { position: relative; margin-bottom: 0px; color: #000; padding: 0; background-color: #5c87b2; height: 38px; padding-left: 10px; }\n#header h1 { font-weight: bold; padding: 5px 0; margin: 0; color: #fff; border: none; line-height: 2em; font-family: Arial, Helvetica, sans-serif; font-size: 32px !important; }\n#header-image { float: left; padding: 3px; margin-left: 1px; margin-right: 1px; }\n#header-text { color: #fff; font-size: 1.4em; line-height: 38px; font-weight: bold; }\n#main { padding: 20px 20px 15px 20px; background-color: #fff; _height: 1px; }\n#footer { color: #999; padding: 5px 0; text-align: left; line-height: normal; margin: 20px 0px 0px 0px; font-size: .9em; border-top: solid 1px #5C87B2; }\n#footer-powered-by { float: right; }\n.details { font-family: monospace; border: solid 1px #e8eef4; white-space: pre; font-size: 1.2em; overflow: auto; padding: 6px; margin-top: 6px }\n.details-wrapped { white-space: normal }\n.details-header { margin-top: 1.5em }\n.details-header a { font-weight: bold; text-decoration: none }\np { margin-bottom: 0.3em; margin-top: 0.1em }\n.sourceErrorLine { color: #770000; font-weight: bold; }\n</style>\n\n<title>Double fault in exception reporting code</title>\n</head>\n<body>\n<h1>Double fault in exception reporting code</h1>\n<p>While generating HTML with exception report, a double fault has occurred. Please consult your server's console and/or log file to see the actual exception.</p>\n</body>\n</html>\n";

	private ExceptionPageTemplate PageTemplate
	{
		get
		{
			if (pageTemplate == null)
			{
				pageTemplate = GetPageTemplate();
			}
			return pageTemplate;
		}
	}

	/// <summary>Gets the event codes that are associated with the HTTP exception.</summary>
	/// <returns>An integer representing a Web event code.</returns>
	public int WebEventCode => webEventCode;

	internal virtual string Description
	{
		get
		{
			if (description != null)
			{
				return description;
			}
			return "Error processing request.";
		}
		set
		{
			if (value != null && value.Length > 0)
			{
				description = value;
			}
			else
			{
				description = "Error processing request.";
			}
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpException" /> class and creates an empty <see cref="T:System.Web.HttpException" /> object.</summary>
	public HttpException()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpException" /> class using a supplied error message.</summary>
	/// <param name="message">The error message displayed to the client when the exception is thrown. </param>
	public HttpException(string message)
		: base(message)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpException" /> class using an error message and the <see cref="P:System.Exception.InnerException" /> property.</summary>
	/// <param name="message">The error message displayed to the client when the exception is thrown. </param>
	/// <param name="innerException">The <see cref="P:System.Exception.InnerException" />, if any, that threw the current exception. </param>
	public HttpException(string message, Exception innerException)
		: base(message, innerException)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpException" /> class using an HTTP response status code and an error message.</summary>
	/// <param name="httpCode">The HTTP response status code sent to the client corresponding to this error. </param>
	/// <param name="message">The error message displayed to the client when the exception is thrown. </param>
	public HttpException(int httpCode, string message)
		: base(message)
	{
		http_code = httpCode;
	}

	internal HttpException(int httpCode, string message, string resourceName)
		: this(httpCode, message)
	{
		resource_name = resourceName;
	}

	internal HttpException(int httpCode, string message, string resourceName, string description)
		: this(httpCode, message, resourceName)
	{
		this.description = description;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpException" /> class with serialized data.</summary>
	/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
	/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that holds the contextual information about the source or destination.</param>
	protected HttpException(SerializationInfo info, StreamingContext context)
		: base(info, context)
	{
		http_code = info.GetInt32("_httpCode");
		webEventCode = info.GetInt32("_webEventCode");
	}

	/// <summary>Gets information about the exception and adds it to the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object. </summary>
	/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
	/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that holds the contextual information about the source or destination.</param>
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		base.GetObjectData(info, context);
		info.AddValue("_httpCode", http_code);
		info.AddValue("_webEventCode", webEventCode);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpException" /> class using an HTTP response status code, an error message, and an exception code.</summary>
	/// <param name="httpCode">The HTTP response status code displayed on the client. </param>
	/// <param name="message">The error message displayed to the client when the exception is thrown. </param>
	/// <param name="hr">The exception code that defines the error. </param>
	public HttpException(int httpCode, string message, int hr)
		: base(message, hr)
	{
		http_code = httpCode;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpException" /> class using an error message and an exception code.</summary>
	/// <param name="message">The error message displayed to the client when the exception is thrown. </param>
	/// <param name="hr">The exception code that defines the error. </param>
	public HttpException(string message, int hr)
		: base(message, hr)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpException" /> class using an HTTP response status code, an error message, and the <see cref="P:System.Exception.InnerException" /> property.</summary>
	/// <param name="httpCode">The HTTP response status code displayed on the client. </param>
	/// <param name="message">The error message displayed to the client when the exception is thrown. </param>
	/// <param name="innerException">The <see cref="P:System.Exception.InnerException" />, if any, that threw the current exception. </param>
	public HttpException(int httpCode, string message, Exception innerException)
		: base(message, innerException)
	{
		http_code = httpCode;
	}

	internal HttpException(int httpCode, string message, Exception innerException, string resourceName)
		: this(httpCode, message, innerException)
	{
		resource_name = resourceName;
	}

	[MonoTODO("For now just the default template is created. Means of user-provided templates are to be implemented yet.")]
	private ExceptionPageTemplate GetPageTemplate()
	{
		DefaultExceptionPageTemplate defaultExceptionPageTemplate = new DefaultExceptionPageTemplate();
		defaultExceptionPageTemplate.Init();
		return defaultExceptionPageTemplate;
	}

	/// <summary>Gets the HTML error message to return to the client.</summary>
	/// <returns>The HTML error message.</returns>
	public string GetHtmlErrorMessage()
	{
		ExceptionPageTemplateValues exceptionPageTemplateValues = new ExceptionPageTemplateValues();
		ExceptionPageTemplate exceptionPageTemplate = PageTemplate;
		try
		{
			exceptionPageTemplateValues.Add("RuntimeVersionInformation", RuntimeHelpers.MonoVersion);
			exceptionPageTemplateValues.Add("AspNetVersionInformation", Environment.Version.ToString());
			HttpContext current = HttpContext.Current;
			ExceptionPageTemplateType pageType = ExceptionPageTemplateType.Standard;
			if (current != null && current.IsCustomErrorEnabled)
			{
				if (http_code != 404 && http_code != 403)
				{
					FillDefaultCustomErrorValues(exceptionPageTemplateValues);
					pageType = ExceptionPageTemplateType.CustomErrorDefault;
				}
				else
				{
					FillDefaultErrorValues(showTrace: false, showExceptionType: false, null, exceptionPageTemplateValues);
				}
			}
			else
			{
				Exception ex = GetBaseException();
				if (ex == null)
				{
					ex = this;
				}
				exceptionPageTemplateValues.Add("FullStackTrace", FormatFullStackTrace());
				if (!(ex is HtmlizedException exc))
				{
					FillDefaultErrorValues(showTrace: true, showExceptionType: true, ex, exceptionPageTemplateValues);
				}
				else
				{
					pageType = ExceptionPageTemplateType.Htmlized;
					FillHtmlizedErrorValues(exceptionPageTemplateValues, exc, ref pageType);
				}
			}
			return exceptionPageTemplate.Render(exceptionPageTemplateValues, pageType);
		}
		catch (Exception value)
		{
			Console.Error.WriteLine("An exception has occurred while generating HttpException page:");
			Console.Error.WriteLine(value);
			Console.Error.WriteLine();
			Console.Error.WriteLine("The actual exception which was being reported was:");
			Console.Error.WriteLine(this);
			try
			{
				FillDefaultCustomErrorValues(exceptionPageTemplateValues);
				return exceptionPageTemplate.Render(exceptionPageTemplateValues, ExceptionPageTemplateType.CustomErrorDefault);
			}
			catch
			{
				return "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\n<head>\n<style type=\"text/css\">\nbody { background-color: #FFFFFF; font-size: .75em; font-family: Verdana, Helvetica, Sans-Serif; margin: 0; padding: 0; color: #696969; }\na:link { color: #000000; text-decoration: underline; }\na:visited { color: #000000; }\na:hover { color: #000000; text-decoration: none; }\na:active { color: #12eb87; }\np, ul { margin-bottom: 20px; line-height: 1.6em; }\npre { font-size: 1.2em; margin-left: 20px; margin-top: 0px; }\nh1, h2, h3, h4, h5, h6 { font-size: 1.6em; color: #000; font-family: Arial, Helvetica, sans-serif; }\nh1 { font-weight: bold; margin-bottom: 0; margin-top: 0; padding-bottom: 0; }\nh2 { font-size: 1em; padding: 0 0 0px 0; color: #696969; font-weight: normal; margin-top: 0; margin-bottom: 20px; }\nh3 { font-size: 1.2em; }\nh4 { font-size: 1.1em; }\nh5, h6 { font-size: 1em; }\n#header { position: relative; margin-bottom: 0px; color: #000; padding: 0; background-color: #5c87b2; height: 38px; padding-left: 10px; }\n#header h1 { font-weight: bold; padding: 5px 0; margin: 0; color: #fff; border: none; line-height: 2em; font-family: Arial, Helvetica, sans-serif; font-size: 32px !important; }\n#header-image { float: left; padding: 3px; margin-left: 1px; margin-right: 1px; }\n#header-text { color: #fff; font-size: 1.4em; line-height: 38px; font-weight: bold; }\n#main { padding: 20px 20px 15px 20px; background-color: #fff; _height: 1px; }\n#footer { color: #999; padding: 5px 0; text-align: left; line-height: normal; margin: 20px 0px 0px 0px; font-size: .9em; border-top: solid 1px #5C87B2; }\n#footer-powered-by { float: right; }\n.details { font-family: monospace; border: solid 1px #e8eef4; white-space: pre; font-size: 1.2em; overflow: auto; padding: 6px; margin-top: 6px }\n.details-wrapped { white-space: normal }\n.details-header { margin-top: 1.5em }\n.details-header a { font-weight: bold; text-decoration: none }\np { margin-bottom: 0.3em; margin-top: 0.1em }\n.sourceErrorLine { color: #770000; font-weight: bold; }\n</style>\n\n<title>Double fault in exception reporting code</title>\n</head>\n<body>\n<h1>Double fault in exception reporting code</h1>\n<p>While generating HTML with exception report, a double fault has occurred. Please consult your server's console and/or log file to see the actual exception.</p>\n</body>\n</html>\n";
			}
		}
	}

	internal static HttpException NewWithCode(string message, int webEventCode)
	{
		HttpException ex = new HttpException(message);
		ex.SetWebEventCode(webEventCode);
		return ex;
	}

	internal static HttpException NewWithCode(string message, Exception innerException, int webEventCode)
	{
		HttpException ex = new HttpException(message, innerException);
		ex.SetWebEventCode(webEventCode);
		return ex;
	}

	internal static HttpException NewWithCode(int httpCode, string message, int webEventCode)
	{
		HttpException ex = new HttpException(httpCode, message);
		ex.SetWebEventCode(webEventCode);
		return ex;
	}

	internal static HttpException NewWithCode(int httpCode, string message, Exception innerException, string resourceName, int webEventCode)
	{
		HttpException ex = new HttpException(httpCode, message, innerException, resourceName);
		ex.SetWebEventCode(webEventCode);
		return ex;
	}

	internal static HttpException NewWithCode(int httpCode, string message, string resourceName, int webEventCode)
	{
		HttpException ex = new HttpException(httpCode, message, resourceName);
		ex.SetWebEventCode(webEventCode);
		return ex;
	}

	internal static HttpException NewWithCode(int httpCode, string message, Exception innerException, int webEventCode)
	{
		HttpException ex = new HttpException(httpCode, message, innerException);
		ex.SetWebEventCode(webEventCode);
		return ex;
	}

	internal void SetWebEventCode(int webEventCode)
	{
		this.webEventCode = webEventCode;
	}

	private string FormatFullStackTrace()
	{
		Exception ex = this;
		StringBuilder stringBuilder = new StringBuilder("\r\n<!--");
		bool flag = true;
		while (ex != null)
		{
			string stackTrace = ex.StackTrace;
			string message = ex.Message;
			bool flag2 = !string.IsNullOrEmpty(stackTrace);
			if (!flag2 && string.IsNullOrEmpty(message))
			{
				ex = ex.InnerException;
				continue;
			}
			if (flag)
			{
				flag = false;
			}
			else
			{
				stringBuilder.Append("\r\n");
			}
			stringBuilder.Append(string.Concat("\r\n[", ex.GetType(), "]: ", HtmlEncode(message), "\r\n"));
			if (flag2)
			{
				stringBuilder.Append(ex.StackTrace);
			}
			ex = ex.InnerException;
		}
		stringBuilder.Append("\r\n-->\r\n");
		return stringBuilder.ToString();
	}

	private void FillHtmlizedErrorValues(ExceptionPageTemplateValues values, HtmlizedException exc, ref ExceptionPageTemplateType pageType)
	{
		bool flag = exc is ParseException;
		bool flag2 = !flag && exc is CompilationException;
		values.Add("Title", HtmlEncode(exc.Title));
		values.Add("Description", HtmlEncode(exc.Description));
		values.Add("StackTrace", HtmlEncode(exc.StackTrace));
		values.Add("ExceptionType", exc.GetType().ToString());
		values.Add("ExceptionMessage", HtmlEncode(exc.Message));
		values.Add("Details", HtmlEncode(exc.ErrorMessage));
		string value = (flag ? "Parser" : ((!flag2) ? "Other" : "Compiler"));
		values.Add("HtmlizedExceptionOrigin", value);
		if (exc.FileText != null)
		{
			pageType |= ExceptionPageTemplateType.SourceError;
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = ((!flag2) ? null : new StringBuilder());
			FormatSource(stringBuilder, stringBuilder2, exc);
			values.Add("HtmlizedExceptionShortSource", stringBuilder.ToString());
			values.Add("HtmlizedExceptionLongSource", stringBuilder2?.ToString());
			if (exc.SourceFile != exc.FileName)
			{
				values.Add("HtmlizedExceptionSourceFile", FormatSourceFile(exc.SourceFile));
			}
			else
			{
				values.Add("HtmlizedExceptionSourceFile", FormatSourceFile(exc.FileName));
			}
			if (flag || flag2)
			{
				int[] errorLines = exc.ErrorLines;
				int num = ((errorLines != null) ? errorLines.Length : 0);
				StringBuilder stringBuilder3 = new StringBuilder();
				for (int i = 0; i < num; i++)
				{
					if (i > 0)
					{
						stringBuilder3.Append(", ");
					}
					stringBuilder3.Append(errorLines[i]);
				}
				values.Add("HtmlizedExceptionErrorLines", stringBuilder3.ToString());
			}
		}
		else
		{
			values.Add("HtmlizedExceptionSourceFile", FormatSourceFile(exc.FileName));
		}
		if (!flag2)
		{
			return;
		}
		StringCollection compilerOutput = (exc as CompilationException).CompilerOutput;
		if (compilerOutput == null || compilerOutput.Count <= 0)
		{
			return;
		}
		pageType |= ExceptionPageTemplateType.CompilerOutput;
		StringBuilder stringBuilder4 = new StringBuilder();
		bool flag3 = true;
		StringEnumerator enumerator = compilerOutput.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				string current = enumerator.Current;
				stringBuilder4.Append(HtmlEncode(current));
				if (flag3)
				{
					stringBuilder4.Append("<br/>");
					flag3 = false;
				}
				stringBuilder4.Append("<br/>");
			}
		}
		finally
		{
			if (enumerator is IDisposable disposable)
			{
				disposable.Dispose();
			}
		}
		values.Add("HtmlizedExceptionCompilerOutput", stringBuilder4.ToString());
	}

	private void FillDefaultCustomErrorValues(ExceptionPageTemplateValues values)
	{
		values.Add("Title", "Runtime Error");
		values.Add("ExceptionType", "Runtime Error");
		values.Add("ExceptionMessage", "A runtime error has occurred");
		values.Add("Description", "An application error occurred on the server. The current custom error settings for this application prevent the details of the application error from being viewed (for security reasons).");
		values.Add("Details", "To enable the details of this specific error message to be viewable, please create a &lt;customErrors&gt; tag within a &quot;web.config&quot; configuration file located in the root directory of the current web application. This &lt;customErrors&gt; tag should then have its &quot;mode&quot; attribute set to &quot;Off&quot;.");
	}

	private void FillDefaultErrorValues(bool showTrace, bool showExceptionType, Exception baseEx, ExceptionPageTemplateValues values)
	{
		if (baseEx == null)
		{
			baseEx = this;
		}
		values.Add("Title", string.Format("Error{0}", (http_code != 0) ? (" " + http_code) : string.Empty));
		values.Add("ExceptionType", showExceptionType ? baseEx.GetType().ToString() : "Runtime error");
		values.Add("ExceptionMessage", (http_code == 404) ? "The resource cannot be found." : HtmlEncode(baseEx.Message));
		string text = ((http_code != 0) ? ("HTTP " + http_code + ".") : string.Empty);
		values.Add("Description", text + ((http_code == 404) ? "The resource you are looking for (or one of its dependencies) could have been removed, had its name changed, or is temporarily unavailable.  Please review the following URL and make sure that it is spelled correctly." : HtmlEncode(Description)));
		if (!string.IsNullOrEmpty(resource_name))
		{
			values.Add("Details", "Requested URL: " + HtmlEncode(resource_name));
		}
		else if (http_code == 404)
		{
			values.Add("Details", "No virtual path information available.");
		}
		else if (baseEx is HttpException)
		{
			text = ((HttpException)baseEx).Description;
			values.Add("Details", (!string.IsNullOrEmpty(text)) ? HtmlEncode(text) : "Web exception occurred but no additional error description given.");
		}
		else
		{
			StringBuilder stringBuilder = new StringBuilder("Non-web exception.");
			text = baseEx.Source;
			if (!string.IsNullOrEmpty(text))
			{
				stringBuilder.AppendFormat(" Exception origin (name of application or object): {0}.", HtmlEncode(text));
			}
			text = baseEx.HelpLink;
			if (!string.IsNullOrEmpty(text))
			{
				stringBuilder.AppendFormat(" Additional information is available at {0}", HtmlEncode(text));
			}
			values.Add("Details", stringBuilder.ToString());
		}
		if (showTrace)
		{
			string stackTrace = baseEx.StackTrace;
			if (!string.IsNullOrEmpty(stackTrace))
			{
				values.Add("StackTrace", HtmlEncode(stackTrace));
			}
		}
	}

	private static string HtmlEncode(string s)
	{
		if (string.IsNullOrEmpty(s))
		{
			return s;
		}
		return HttpUtility.HtmlEncode(s).Replace("\r\n", "<br />");
	}

	private string FormatSourceFile(string filename)
	{
		if (filename == null || filename.Length == 0)
		{
			return string.Empty;
		}
		if (filename.StartsWith("@@"))
		{
			return "[internal] <!-- " + HttpUtility.HtmlEncode(filename) + " -->";
		}
		return HttpUtility.HtmlEncode(filename);
	}

	private static void FormatSource(StringBuilder builder, StringBuilder longVersion, HtmlizedException e)
	{
		if (e is CompilationException)
		{
			WriteCompilationSource(builder, longVersion, e);
		}
		else
		{
			WritePageSource(builder, e);
		}
	}

	private static void WriteCompilationSource(StringBuilder builder, StringBuilder longVersion, HtmlizedException e)
	{
		int[] errorLines = e.ErrorLines;
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		if (errorLines != null && errorLines.Length != 0)
		{
			num3 = errorLines[0];
		}
		int num4 = num3 - 2;
		int num5 = num3 + 2;
		if (num4 < 0)
		{
			num4 = 0;
		}
		using TextReader textReader = new StringReader(e.FileText);
		string s;
		while ((s = textReader.ReadLine()) != null)
		{
			num++;
			if (num < num4 || num > num5)
			{
				longVersion?.AppendFormat("{0}: {1}\r\n", num, HtmlEncode(s));
				continue;
			}
			if (num3 == num)
			{
				longVersion?.Append("<span class=\"sourceErrorLine\">");
				builder.Append("<span class=\"sourceErrorLine\">");
			}
			string value = $"{num}: {HtmlEncode(s)}\r\n";
			builder.Append(value);
			longVersion?.Append(value);
			if (num == num3)
			{
				builder.Append("</span>");
				longVersion?.Append("</span>");
				num3 = ((++num2 < errorLines.Length) ? errorLines[num2] : 0);
			}
		}
	}

	private static void WritePageSource(StringBuilder builder, HtmlizedException e)
	{
		int num = 0;
		int num2 = e.ErrorLines[0];
		int num3 = e.ErrorLines[1];
		int num4 = num2 - 2;
		int num5 = num3 + 2;
		if (num4 <= 0)
		{
			num4 = 1;
		}
		TextReader textReader = new StringReader(e.FileText);
		string s;
		while ((s = textReader.ReadLine()) != null)
		{
			num++;
			if (num >= num4)
			{
				if (num > num5)
				{
					break;
				}
				if (num2 == num)
				{
					builder.Append("<span class=\"sourceErrorLine\">");
				}
				builder.AppendFormat("{0}: {1}\r\n", num, HtmlEncode(s));
				if (num3 <= num)
				{
					builder.Append("</span>");
					num3 = num5 + 1;
				}
			}
		}
	}

	/// <summary>Gets the HTTP response status code to return to the client. </summary>
	/// <returns>A non-zero HTTP code representing the exception or the <see cref="P:System.Exception.InnerException" /> code; otherwise, HTTP response status code 500.</returns>
	public int GetHttpCode()
	{
		return http_code;
	}

	/// <summary>Creates a new <see cref="T:System.Web.HttpException" /> exception based on the error code that is returned from the Win32 API <see langword="GetLastError()" /> method.</summary>
	/// <param name="message">The error message displayed to the client when the exception is thrown. </param>
	/// <returns>An <see cref="T:System.Web.HttpException" /> based on the error code that is returned from a call to the Win32 API <see langword="GetLastError()" /> method.</returns>
	public static HttpException CreateFromLastError(string message)
	{
		return new HttpException(message, 0);
	}
}
