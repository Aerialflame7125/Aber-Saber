using System.ComponentModel;

namespace System.Drawing.Printing;

/// <summary>Defines a reusable object that sends output to a printer, when printing from a Windows Forms application.</summary>
[DefaultEvent("PrintPage")]
[DefaultProperty("DocumentName")]
[ToolboxItemFilter("System.Drawing.Printing", ToolboxItemFilterType.Allow)]
public class PrintDocument : Component
{
	private PageSettings defaultpagesettings;

	private PrinterSettings printersettings;

	private PrintController printcontroller;

	private string documentname;

	private bool originAtMargins;

	/// <summary>Gets or sets page settings that are used as defaults for all pages to be printed.</summary>
	/// <returns>A <see cref="T:System.Drawing.Printing.PageSettings" /> that specifies the default page settings for the document.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[SRDescription("The settings for the current page.")]
	public PageSettings DefaultPageSettings
	{
		get
		{
			return defaultpagesettings;
		}
		set
		{
			defaultpagesettings = value;
		}
	}

	/// <summary>Gets or sets the document name to display (for example, in a print status dialog box or printer queue) while printing the document.</summary>
	/// <returns>The document name to display while printing the document. The default is "document".</returns>
	[DefaultValue("document")]
	[SRDescription("The name of the document.")]
	public string DocumentName
	{
		get
		{
			return documentname;
		}
		set
		{
			documentname = value;
		}
	}

	/// <summary>Gets or sets the print controller that guides the printing process.</summary>
	/// <returns>The <see cref="T:System.Drawing.Printing.PrintController" /> that guides the printing process. The default is a new instance of the <see cref="T:System.Windows.Forms.PrintControllerWithStatusDialog" /> class.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[SRDescription("The print controller object.")]
	public PrintController PrintController
	{
		get
		{
			return printcontroller;
		}
		set
		{
			printcontroller = value;
		}
	}

	/// <summary>Gets or sets the printer that prints the document.</summary>
	/// <returns>A <see cref="T:System.Drawing.Printing.PrinterSettings" /> that specifies where and how the document is printed. The default is a <see cref="T:System.Drawing.Printing.PrinterSettings" /> with its properties set to their default values.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[SRDescription("The current settings for the active printer.")]
	public PrinterSettings PrinterSettings
	{
		get
		{
			return printersettings;
		}
		set
		{
			printersettings = ((value == null) ? new PrinterSettings() : value);
		}
	}

	/// <summary>Gets or sets a value indicating whether the position of a graphics object associated with a page is located just inside the user-specified margins or at the top-left corner of the printable area of the page.</summary>
	/// <returns>
	///   <see langword="true" /> if the graphics origin starts at the page margins; <see langword="false" /> if the graphics origin is at the top-left corner of the printable page. The default is <see langword="false" />.</returns>
	[DefaultValue(false)]
	[SRDescription("Determines if the origin is set at the specified margins.")]
	public bool OriginAtMargins
	{
		get
		{
			return originAtMargins;
		}
		set
		{
			originAtMargins = value;
		}
	}

	/// <summary>Occurs when the <see cref="M:System.Drawing.Printing.PrintDocument.Print" /> method is called and before the first page of the document prints.</summary>
	[SRDescription("Raised when printing begins")]
	public event PrintEventHandler BeginPrint;

	/// <summary>Occurs when the last page of the document has printed.</summary>
	[SRDescription("Raised when printing ends")]
	public event PrintEventHandler EndPrint;

	/// <summary>Occurs when the output to print for the current page is needed.</summary>
	[SRDescription("Raised when printing of a new page begins")]
	public event PrintPageEventHandler PrintPage;

	/// <summary>Occurs immediately before each <see cref="E:System.Drawing.Printing.PrintDocument.PrintPage" /> event.</summary>
	[SRDescription("Raised before printing of a new page begins")]
	public event QueryPageSettingsEventHandler QueryPageSettings;

	/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrintDocument" /> class.</summary>
	public PrintDocument()
	{
		documentname = "document";
		printersettings = new PrinterSettings();
		defaultpagesettings = (PageSettings)printersettings.DefaultPageSettings.Clone();
		printcontroller = new StandardPrintController();
	}

	/// <summary>Starts the document's printing process.</summary>
	/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist.</exception>
	public void Print()
	{
		PrintEventArgs printEventArgs = new PrintEventArgs();
		OnBeginPrint(printEventArgs);
		if (printEventArgs.Cancel)
		{
			return;
		}
		PrintController.OnStartPrint(this, printEventArgs);
		if (printEventArgs.Cancel)
		{
			return;
		}
		Graphics graphics = null;
		if (printEventArgs.GraphicsContext != null)
		{
			graphics = Graphics.FromHdc(printEventArgs.GraphicsContext.Hdc);
			printEventArgs.GraphicsContext.Graphics = graphics;
		}
		PrintPageEventArgs printPageEventArgs;
		do
		{
			QueryPageSettingsEventArgs queryPageSettingsEventArgs = new QueryPageSettingsEventArgs(DefaultPageSettings.Clone() as PageSettings);
			OnQueryPageSettings(queryPageSettingsEventArgs);
			PageSettings pageSettings = queryPageSettingsEventArgs.PageSettings;
			printPageEventArgs = new PrintPageEventArgs(graphics, pageSettings.Bounds, new Rectangle(0, 0, pageSettings.PaperSize.Width, pageSettings.PaperSize.Height), pageSettings);
			printPageEventArgs.GraphicsContext = printEventArgs.GraphicsContext;
			Graphics graphics2 = PrintController.OnStartPage(this, printPageEventArgs);
			printPageEventArgs.SetGraphics(graphics2);
			if (!printPageEventArgs.Cancel)
			{
				OnPrintPage(printPageEventArgs);
			}
			PrintController.OnEndPage(this, printPageEventArgs);
		}
		while (!printPageEventArgs.Cancel && printPageEventArgs.HasMorePages);
		OnEndPrint(printEventArgs);
		PrintController.OnEndPrint(this, printEventArgs);
	}

	/// <summary>Provides information about the print document, in string form.</summary>
	/// <returns>A string.</returns>
	public override string ToString()
	{
		return "[PrintDocument " + DocumentName + "]";
	}

	/// <summary>Raises the <see cref="E:System.Drawing.Printing.PrintDocument.BeginPrint" /> event. It is called after the <see cref="M:System.Drawing.Printing.PrintDocument.Print" /> method is called and before the first page of the document prints.</summary>
	/// <param name="e">A <see cref="T:System.Drawing.Printing.PrintEventArgs" /> that contains the event data.</param>
	protected virtual void OnBeginPrint(PrintEventArgs e)
	{
		if (this.BeginPrint != null)
		{
			this.BeginPrint(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Drawing.Printing.PrintDocument.EndPrint" /> event. It is called when the last page of the document has printed.</summary>
	/// <param name="e">A <see cref="T:System.Drawing.Printing.PrintEventArgs" /> that contains the event data.</param>
	protected virtual void OnEndPrint(PrintEventArgs e)
	{
		if (this.EndPrint != null)
		{
			this.EndPrint(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Drawing.Printing.PrintDocument.PrintPage" /> event. It is called before a page prints.</summary>
	/// <param name="e">A <see cref="T:System.Drawing.Printing.PrintPageEventArgs" /> that contains the event data.</param>
	protected virtual void OnPrintPage(PrintPageEventArgs e)
	{
		if (this.PrintPage != null)
		{
			this.PrintPage(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Drawing.Printing.PrintDocument.QueryPageSettings" /> event. It is called immediately before each <see cref="E:System.Drawing.Printing.PrintDocument.PrintPage" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Drawing.Printing.QueryPageSettingsEventArgs" /> that contains the event data.</param>
	protected virtual void OnQueryPageSettings(QueryPageSettingsEventArgs e)
	{
		if (this.QueryPageSettings != null)
		{
			this.QueryPageSettings(this, e);
		}
	}
}
