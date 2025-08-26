using System.Collections;
using System.Data;
using System.Security.Permissions;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace System.Web.Handlers;

/// <summary>Provides a synchronous HTTP handler that processes requests for tracing information.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class TraceHandler : IHttpHandler
{
	/// <summary>Gets a value indicating whether another request can use the <see cref="T:System.Web.Handlers.TraceHandler" /> instance.</summary>
	/// <returns>
	///     <see langword="false" /> in all cases.</returns>
	bool IHttpHandler.IsReusable => IsReusable;

	/// <summary>Gets a value indicating whether another request can use the <see cref="T:System.Web.Handlers.TraceHandler" /> instance.</summary>
	/// <returns>
	///     <see langword="false" /> in all cases.</returns>
	protected bool IsReusable => false;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Handlers.TraceHandler" /> class. </summary>
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public TraceHandler()
	{
	}

	/// <summary>Processes an HTTP request.</summary>
	/// <param name="context">An <see cref="T:System.Web.HttpContext" /> object that provides access to the current <see langword="Request" /> and <see langword="Response" /> instances.</param>
	void IHttpHandler.ProcessRequest(HttpContext context)
	{
		ProcessRequest(context);
	}

	/// <summary>Processes an HTTP request.</summary>
	/// <param name="context">An <see cref="T:System.Web.HttpContext" /> object that provides access to the current <see langword="Request" /> and <see langword="Response" /> instances.</param>
	protected void ProcessRequest(HttpContext context)
	{
		TraceManager traceManager = HttpRuntime.TraceManager;
		if (!traceManager.Enabled || (traceManager.LocalOnly && !context.Request.IsLocal))
		{
			throw new TraceNotAvailableException(traceManager.Enabled);
		}
		HtmlTextWriter output = new HtmlTextWriter(context.Response.Output);
		if (context.Request.QueryString["clear"] != null)
		{
			traceManager.Clear();
			context.Response.Redirect(context.Request.FilePath);
		}
		string text = context.Request.QueryString["id"];
		int num = -1;
		if (text != null)
		{
			num = int.Parse(text);
		}
		if (num > 0 && num <= traceManager.ItemCount)
		{
			RenderItem(traceManager, output, num);
			return;
		}
		string dir = context.Server.MapPath(UrlUtils.GetDirectory(context.Request.FilePath));
		RenderMenu(traceManager, output, dir);
	}

	private void RenderMenu(TraceManager manager, HtmlTextWriter output, string dir)
	{
		output.RenderBeginTag(HtmlTextWriterTag.Html);
		output.RenderBeginTag(HtmlTextWriterTag.Head);
		TraceData.RenderStyleSheet(output);
		output.RenderEndTag();
		RenderHeader(output, dir);
		output.RenderBeginTag(HtmlTextWriterTag.Body);
		output.AddAttribute("class", "tracecontent");
		output.RenderBeginTag(HtmlTextWriterTag.Span);
		Table table = TraceData.CreateTable();
		table.Rows.Add(TraceData.AltRow("Requests to the Application"));
		table.Rows.Add(TraceData.SubHeadRow("No", "Time of Request", "File", "Status Code", "Verb", "&nbsp;"));
		if (manager.TraceData != null)
		{
			for (int i = 0; i < manager.ItemCount; i++)
			{
				int num = i + 1;
				TraceData traceData = manager.TraceData[i];
				TraceData.RenderAltRow(table, i, num.ToString(), traceData.RequestTime.ToString(), traceData.RequestPath, traceData.StatusCode.ToString(), traceData.RequestType, "<a href=\"Trace.axd?id=" + num + "\" class=\"tinylink\"><b><nobr>View Details</a>");
			}
			table.RenderControl(output);
		}
		output.RenderEndTag();
		output.RenderEndTag();
		output.RenderEndTag();
	}

	private void RenderHeader(HtmlTextWriter output, string dir)
	{
		Table table = TraceData.CreateTable();
		TableRow tableRow = new TableRow();
		TableRow tableRow2 = new TableRow();
		TableCell tableCell = new TableCell();
		TableCell tableCell2 = new TableCell();
		TableCell tableCell3 = new TableCell();
		TableCell tableCell4 = new TableCell();
		tableCell.Text = "<h1>Application Trace</h1>";
		tableCell2.Text = "[ <a href=\"Trace.axd?clear=1\" class=\"link\">clear current trace</a> ]";
		tableCell2.HorizontalAlign = HorizontalAlign.Right;
		tableCell2.VerticalAlign = VerticalAlign.Bottom;
		tableRow.Cells.Add(tableCell);
		tableRow.Cells.Add(tableCell2);
		tableCell3.Text = "<h2><h2><p>";
		tableCell4.Text = "<b>Physical Directory:</b>" + dir;
		tableRow2.Cells.Add(tableCell3);
		tableRow2.Cells.Add(tableCell4);
		table.Rows.Add(tableRow);
		table.Rows.Add(tableRow2);
		table.RenderControl(output);
	}

	private void RenderItem(TraceManager manager, HtmlTextWriter output, int item)
	{
		manager.TraceData[item - 1].Render(output);
	}

	/// <summary>Writes the details of the current system state and page information to the response stream.</summary>
	/// <param name="data">A <see cref="T:System.Data.DataSet" /> object that contains tracing information.</param>
	[MonoLimitation("Not implemented, does nothing")]
	protected void ShowDetails(DataSet data)
	{
	}

	/// <summary>Writes the details of recent HTTP request traffic to the response stream.</summary>
	/// <param name="data">A set of <see cref="T:System.Data.DataSet" /> objects that represent the recent HTTP requests the server has processed.</param>
	[MonoLimitation("Not implemented, does nothing")]
	protected void ShowRequests(IList data)
	{
	}

	/// <summary>Writes the details of the current Common Language Runtime and ASP.NET build versions that the Web server is using.</summary>
	[MonoLimitation("Not implemented, does nothing")]
	protected void ShowVersionDetails()
	{
	}
}
