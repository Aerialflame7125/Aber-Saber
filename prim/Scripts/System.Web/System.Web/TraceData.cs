using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace System.Web;

internal sealed class TraceData
{
	private bool is_first_time;

	private DateTime first_time;

	private double prev_time;

	private Queue<InfoTraceData> info;

	private Queue<ControlTraceData> control_data;

	private Queue<NameValueTraceData> cookie_data;

	private Queue<NameValueTraceData> header_data;

	private Queue<NameValueTraceData> servervar_data;

	private Hashtable ctrl_cs;

	private string request_path;

	private string session_id;

	private DateTime request_time;

	private Encoding request_encoding;

	private Encoding response_encoding;

	private string request_type;

	private int status_code;

	private Page page;

	private TraceMode _traceMode = HttpRuntime.TraceManager.TraceMode;

	private Hashtable sizes;

	private Hashtable ctrl_vs;

	public TraceMode TraceMode
	{
		get
		{
			return _traceMode;
		}
		set
		{
			_traceMode = value;
		}
	}

	public string RequestPath
	{
		get
		{
			return request_path;
		}
		set
		{
			request_path = value;
		}
	}

	public string SessionID
	{
		get
		{
			return session_id;
		}
		set
		{
			session_id = value;
		}
	}

	public DateTime RequestTime
	{
		get
		{
			return request_time;
		}
		set
		{
			request_time = value;
		}
	}

	public Encoding RequestEncoding
	{
		get
		{
			return request_encoding;
		}
		set
		{
			request_encoding = value;
		}
	}

	public Encoding ResponseEncoding
	{
		get
		{
			return response_encoding;
		}
		set
		{
			response_encoding = value;
		}
	}

	public string RequestType
	{
		get
		{
			return request_type;
		}
		set
		{
			request_type = value;
		}
	}

	public int StatusCode
	{
		get
		{
			return status_code;
		}
		set
		{
			status_code = value;
		}
	}

	public TraceData()
	{
		info = new Queue<InfoTraceData>();
		control_data = new Queue<ControlTraceData>();
		cookie_data = new Queue<NameValueTraceData>();
		header_data = new Queue<NameValueTraceData>();
		servervar_data = new Queue<NameValueTraceData>();
		is_first_time = true;
	}

	public void Write(string category, string msg, Exception error, bool Warning)
	{
		double num;
		double timeSinceLast;
		if (is_first_time)
		{
			num = 0.0;
			timeSinceLast = 0.0;
			prev_time = 0.0;
			is_first_time = false;
			first_time = DateTime.Now;
		}
		else
		{
			num = (DateTime.Now - first_time).TotalSeconds;
			timeSinceLast = num - prev_time;
			prev_time = num;
		}
		info.Enqueue(new InfoTraceData(category, HtmlEncode(msg), error?.ToString(), num, timeSinceLast, Warning));
	}

	private static string HtmlEncode(string s)
	{
		if (s == null)
		{
			return "";
		}
		return HttpUtility.HtmlEncode(s).Replace("\n", "<br>").Replace(" ", "&nbsp;");
	}

	public void AddControlTree(Page page, Hashtable ctrl_vs, Hashtable ctrl_cs, Hashtable sizes)
	{
		this.page = page;
		this.ctrl_vs = ctrl_vs;
		this.sizes = sizes;
		this.ctrl_cs = ctrl_cs;
		AddControl(page, 0);
	}

	private void AddControl(Control c, int control_pos)
	{
		control_data.Enqueue(new ControlTraceData(c.UniqueID, c.GetType(), GetRenderSize(c), GetViewStateSize(c, (ctrl_vs != null) ? ctrl_vs[c] : null), GetViewStateSize(c, (ctrl_cs != null) ? ctrl_cs[c] : null), control_pos));
		if (!c.HasControls())
		{
			return;
		}
		foreach (Control control in c.Controls)
		{
			AddControl(control, control_pos + 1);
		}
	}

	private int GetRenderSize(Control ctrl)
	{
		if (sizes == null)
		{
			return 0;
		}
		object obj = sizes[ctrl];
		if (obj != null)
		{
			return (int)obj;
		}
		return 0;
	}

	private static int GetViewStateSize(Control ctrl, object vs)
	{
		if (vs == null)
		{
			return 0;
		}
		StringWriter stringWriter = new StringWriter();
		new LosFormatter().Serialize(stringWriter, vs);
		return stringWriter.GetStringBuilder().Length;
	}

	public void AddCookie(string name, string value)
	{
		cookie_data.Enqueue(new NameValueTraceData(name, value));
	}

	public void AddHeader(string name, string value)
	{
		header_data.Enqueue(new NameValueTraceData(name, value));
	}

	public void AddServerVar(string name, string value)
	{
		servervar_data.Enqueue(new NameValueTraceData(name, value));
	}

	public void Render(HtmlTextWriter output)
	{
		output.AddAttribute("id", "__asptrace");
		output.RenderBeginTag(HtmlTextWriterTag.Div);
		RenderStyleSheet(output);
		output.AddAttribute("class", "tracecontent");
		output.RenderBeginTag(HtmlTextWriterTag.Span);
		RenderRequestDetails(output);
		RenderTraceInfo(output);
		RenderControlTree(output);
		RenderCookies(output);
		RenderHeaders(output);
		RenderServerVars(output);
		output.RenderEndTag();
		output.RenderEndTag();
	}

	private void RenderRequestDetails(HtmlTextWriter output)
	{
		Table table = CreateTable();
		table.Rows.Add(AltRow("Request Details:"));
		table.Rows.Add(InfoRow2("Session Id:", session_id, "Request Type", request_type));
		table.Rows.Add(InfoRow2("Time of Request:", request_time.ToString(), "State Code:", status_code.ToString()));
		table.Rows.Add(InfoRow2("Request Encoding:", request_encoding.EncodingName, "Response Encoding:", response_encoding.EncodingName));
		table.RenderControl(output);
	}

	private void RenderTraceInfo(HtmlTextWriter output)
	{
		Table table = CreateTable();
		table.Rows.Add(AltRow("Trace Information"));
		table.Rows.Add(SubHeadRow("Category", "Message", "From First(s)", "From Lasts(s)"));
		int num = 0;
		IEnumerable<InfoTraceData> enumerable = info;
		if (TraceMode == TraceMode.SortByCategory)
		{
			List<InfoTraceData> list = new List<InfoTraceData>(info);
			list.Sort((InfoTraceData x, InfoTraceData y) => string.Compare(x.Category, y.Category, StringComparison.Ordinal));
			enumerable = list;
		}
		foreach (InfoTraceData item in enumerable)
		{
			RenderTraceInfoRow(table, item, num++);
		}
		table.RenderControl(output);
	}

	private void RenderControlTree(HtmlTextWriter output)
	{
		Table table = CreateTable();
		int num = ((page != null) ? GetViewStateSize(page, page.GetSavedViewState()) : 0);
		table.Rows.Add(AltRow("Control Tree"));
		table.Rows.Add(SubHeadRow("Control Id", "Type", "Render Size Bytes (including children)", $"ViewState Size (total: {num} bytes)(excluding children)", "ControlState Size (excluding children)"));
		int num2 = 0;
		foreach (ControlTraceData control_datum in control_data)
		{
			RenderControlTraceDataRow(table, control_datum, num2++);
		}
		table.RenderControl(output);
	}

	private void RenderControlTraceDataRow(Table table, ControlTraceData r, int pos)
	{
		if (r != null)
		{
			int depth = r.Depth;
			string text = string.Empty;
			for (int i = 0; i < depth; i++)
			{
				text += "&nbsp;&nbsp;&nbsp;&nbsp;";
			}
			RenderAltRow(table, pos, text + r.ControlId, r.Type.ToString(), r.RenderSize.ToString(), r.ViewstateSize.ToString(), r.ControlstateSize.ToString());
		}
	}

	private void RenderCookies(HtmlTextWriter output)
	{
		Table table = CreateTable();
		table.Rows.Add(AltRow("Cookies Collection"));
		table.Rows.Add(SubHeadRow("Name", "Value", "Size"));
		int num = 0;
		foreach (NameValueTraceData cookie_datum in cookie_data)
		{
			RenderCookieDataRow(table, cookie_datum, num++);
		}
		table.RenderControl(output);
	}

	private void RenderCookieDataRow(Table table, NameValueTraceData r, int pos)
	{
		if (r != null)
		{
			int num = r.Name.Length + ((r.Value != null) ? r.Value.Length : 0);
			RenderAltRow(table, pos++, r.Name, r.Value, num.ToString());
		}
	}

	private void RenderHeaders(HtmlTextWriter output)
	{
		Table table = CreateTable();
		table.Rows.Add(AltRow("Headers Collection"));
		table.Rows.Add(SubHeadRow("Name", "Value"));
		int num = 0;
		foreach (NameValueTraceData header_datum in header_data)
		{
			RenderAltRow(table, num++, header_datum.Name, header_datum.Value);
		}
		table.RenderControl(output);
	}

	private void RenderServerVars(HtmlTextWriter output)
	{
		Table table = CreateTable();
		table.Rows.Add(AltRow("Server Variables"));
		table.Rows.Add(SubHeadRow("Name", "Value"));
		int num = 0;
		foreach (NameValueTraceData servervar_datum in servervar_data)
		{
			RenderAltRow(table, num++, servervar_datum.Name, servervar_datum.Value);
		}
		table.RenderControl(output);
	}

	internal static TableRow AltRow(string title)
	{
		TableRow tableRow = new TableRow();
		TableHeaderCell tableHeaderCell = new TableHeaderCell();
		tableHeaderCell.CssClass = "alt";
		tableHeaderCell.HorizontalAlign = HorizontalAlign.Left;
		tableHeaderCell.Attributes[" colspan"] = "10";
		tableHeaderCell.Text = "<h3><b>" + title + "</b></h3>";
		tableRow.Cells.Add(tableHeaderCell);
		return tableRow;
	}

	private void RenderTraceInfoRow(Table table, InfoTraceData i, int pos)
	{
		if (i != null)
		{
			string text;
			string text2 = (text = string.Empty);
			if (i.IsWarning)
			{
				text2 = "<span style=\"color:red\">";
				text = "</span>";
			}
			string text4;
			string text3;
			if (i.TimeSinceFirst == 0.0)
			{
				text4 = (text3 = string.Empty);
			}
			else
			{
				text4 = i.TimeSinceFirst.ToString("0.000000");
				text3 = ((!(i.TimeSinceLast >= 0.1)) ? i.TimeSinceLast.ToString("0.000000") : ("<span style=\"color:red;font-weight:bold\">" + i.TimeSinceLast.ToString("0.000000") + "</span>"));
			}
			RenderAltRow(table, pos, text2 + i.Category + text, text2 + i.Message + text, text4, text3);
		}
	}

	internal static TableRow SubHeadRow(params string[] cells)
	{
		TableRow tableRow = new TableRow();
		foreach (string text in cells)
		{
			TableHeaderCell tableHeaderCell = new TableHeaderCell();
			tableHeaderCell.Text = text;
			tableRow.Cells.Add(tableHeaderCell);
		}
		tableRow.CssClass = "subhead";
		tableRow.HorizontalAlign = HorizontalAlign.Left;
		return tableRow;
	}

	internal static TableRow RenderAltRow(Table table, int pos, params string[] cells)
	{
		TableRow tableRow = new TableRow();
		foreach (string text in cells)
		{
			TableCell tableCell = new TableCell();
			tableCell.Text = text;
			tableRow.Cells.Add(tableCell);
		}
		if (pos % 2 != 0)
		{
			tableRow.CssClass = "alt";
		}
		table.Rows.Add(tableRow);
		return tableRow;
	}

	private TableRow InfoRow2(string title1, string info1, string title2, string info2)
	{
		TableRow tableRow = new TableRow();
		TableHeaderCell tableHeaderCell = new TableHeaderCell();
		TableHeaderCell tableHeaderCell2 = new TableHeaderCell();
		TableCell tableCell = new TableCell();
		TableCell tableCell2 = new TableCell();
		tableHeaderCell.Text = title1;
		tableHeaderCell2.Text = title2;
		tableCell.Text = info1;
		tableCell2.Text = info2;
		tableRow.Cells.Add(tableHeaderCell);
		tableRow.Cells.Add(tableCell);
		tableRow.Cells.Add(tableHeaderCell2);
		tableRow.Cells.Add(tableCell2);
		tableRow.HorizontalAlign = HorizontalAlign.Left;
		return tableRow;
	}

	internal static Table CreateTable()
	{
		return new Table
		{
			Width = Unit.Percentage(100.0),
			CellSpacing = 0,
			CellPadding = 0
		};
	}

	internal static void RenderStyleSheet(HtmlTextWriter o)
	{
		o.WriteLine("<style type=\"text/css\">");
		o.Write("span.tracecontent { background-color:white; ");
		o.WriteLine("color:black;font: 10pt verdana, arial; }");
		o.Write("span.tracecontent table { font: 10pt verdana, ");
		o.WriteLine("arial; cellspacing:0; cellpadding:0; margin-bottom:25}");
		o.WriteLine("span.tracecontent tr.subhead { background-color:cccccc;}");
		o.WriteLine("span.tracecontent th { padding:0,3,0,3 }");
		o.WriteLine("span.tracecontent th.alt { background-color:black; color:white; padding:3,3,2,3; }");
		o.WriteLine("span.tracecontent td { padding:0,3,0,3 }");
		o.WriteLine("span.tracecontent tr.alt { background-color:eeeeee }");
		o.WriteLine("span.tracecontent h1 { font: 24pt verdana, arial; margin:0,0,0,0}");
		o.WriteLine("span.tracecontent h2 { font: 18pt verdana, arial; margin:0,0,0,0}");
		o.WriteLine("span.tracecontent h3 { font: 12pt verdana, arial; margin:0,0,0,0}");
		o.WriteLine("span.tracecontent th a { color:darkblue; font: 8pt verdana, arial; }");
		o.WriteLine("span.tracecontent a { color:darkblue;text-decoration:none }");
		o.WriteLine("span.tracecontent a:hover { color:darkblue;text-decoration:underline; }");
		o.WriteLine("span.tracecontent div.outer { width:90%; margin:15,15,15,15}");
		o.Write("span.tracecontent table.viewmenu td { background-color:006699; ");
		o.WriteLine("color:white; padding:0,5,0,5; }");
		o.WriteLine("span.tracecontent table.viewmenu td.end { padding:0,0,0,0; }");
		o.WriteLine("span.tracecontent table.viewmenu a {color:white; font: 8pt verdana, arial; }");
		o.WriteLine("span.tracecontent table.viewmenu a:hover {color:white; font: 8pt verdana, arial; }");
		o.WriteLine("span.tracecontent a.tinylink {color:darkblue; font: 8pt verdana, ");
		o.WriteLine("arial;text-decoration:underline;}");
		o.WriteLine("span.tracecontent a.link {color:darkblue; text-decoration:underline;}");
		o.WriteLine("span.tracecontent div.buffer {padding-top:7; padding-bottom:17;}");
		o.WriteLine("span.tracecontent .small { font: 8pt verdana, arial }");
		o.WriteLine("span.tracecontent table td { padding-right:20 }");
		o.WriteLine("span.tracecontent table td.nopad { padding-right:5 }");
		o.WriteLine("</style>");
	}
}
