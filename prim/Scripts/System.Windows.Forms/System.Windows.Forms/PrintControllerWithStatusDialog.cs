using System.Drawing;
using System.Drawing.Printing;

namespace System.Windows.Forms;

/// <summary>Controls how a document is printed from a Windows Forms application.</summary>
/// <filterpriority>2</filterpriority>
public class PrintControllerWithStatusDialog : PrintController
{
	private class PrintingDialog : Form
	{
		private Button buttonCancel;

		private Label label;

		public string LabelText
		{
			get
			{
				return label.Text;
			}
			set
			{
				label.Text = value;
			}
		}

		public PrintingDialog()
		{
			buttonCancel = new Button();
			label = new Label();
			SuspendLayout();
			buttonCancel.Location = new Point(88, 88);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.TabIndex = 0;
			buttonCancel.Text = "Cancel";
			label.Location = new Point(0, 40);
			label.Name = "label";
			label.Size = new Size(257, 23);
			label.TabIndex = 1;
			label.Text = "Page 1 of document";
			label.TextAlign = ContentAlignment.MiddleCenter;
			AutoScaleBaseSize = new Size(5, 13);
			base.CancelButton = buttonCancel;
			base.ClientSize = new Size(258, 124);
			base.ControlBox = false;
			base.Controls.Add(label);
			base.Controls.Add(buttonCancel);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.Name = "PrintingDialog";
			base.ShowInTaskbar = false;
			Text = "Printing";
			ResumeLayout(performLayout: false);
		}
	}

	private PrintController underlyingController;

	private PrintingDialog dialog;

	private int currentPage;

	/// <summary>Gets a value indicating this <see cref="T:System.Windows.Forms.PrintControllerWithStatusDialog" /> is used for print preview.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.PrintControllerWithStatusDialog" /> is used for print preview, otherwise, false.</returns>
	public override bool IsPreview => underlyingController.IsPreview;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PrintControllerWithStatusDialog" /> class, wrapping the supplied <see cref="T:System.Drawing.Printing.PrintController" />.</summary>
	/// <param name="underlyingController">A <see cref="T:System.Drawing.Printing.PrintController" /> to encapsulate. </param>
	public PrintControllerWithStatusDialog(PrintController underlyingController)
	{
		this.underlyingController = underlyingController;
		dialog = new PrintingDialog();
		dialog.Text = "Printing";
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PrintControllerWithStatusDialog" /> class, wrapping the supplied <see cref="T:System.Drawing.Printing.PrintController" /> and specifying a title for the dialog box.</summary>
	/// <param name="underlyingController">A <see cref="T:System.Drawing.Printing.PrintController" /> to encapsulate. </param>
	/// <param name="dialogTitle">A <see cref="T:System.String" /> containing a title for the status dialog box. </param>
	public PrintControllerWithStatusDialog(PrintController underlyingController, string dialogTitle)
		: this(underlyingController)
	{
		dialog.Text = dialogTitle;
	}

	/// <summary>Completes the control sequence that determines when and how to print a page of a document.</summary>
	/// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document currently being printed.</param>
	/// <param name="e">A <see cref="T:System.Drawing.Printing.PrintPageEventArgs" /> that contains the event data.</param>
	/// <filterpriority>1</filterpriority>
	public override void OnEndPage(PrintDocument document, PrintPageEventArgs e)
	{
		if (dialog.DialogResult == DialogResult.Cancel)
		{
			e.Cancel = true;
			dialog.Hide();
		}
		else
		{
			underlyingController.OnEndPage(document, e);
		}
	}

	/// <summary>Completes the control sequence that determines when and how to print a document.</summary>
	/// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document currently being printed.</param>
	/// <param name="e">A <see cref="T:System.Drawing.Printing.PrintPageEventArgs" /> that contains the event data.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override void OnEndPrint(PrintDocument document, PrintEventArgs e)
	{
		dialog.Hide();
		underlyingController.OnEndPrint(document, e);
	}

	/// <summary>Begins the control sequence that determines when and how to print a page of a document.</summary>
	/// <returns>A <see cref="T:System.Drawing.Graphics" /> object that represents a page from a <see cref="T:System.Drawing.Printing.PrintDocument" />.</returns>
	/// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document currently being printed.</param>
	/// <param name="e">A <see cref="T:System.Drawing.Printing.PrintPageEventArgs" /> that contains the event data.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override Graphics OnStartPage(PrintDocument document, PrintPageEventArgs e)
	{
		if (dialog.DialogResult == DialogResult.Cancel)
		{
			e.Cancel = true;
			dialog.Hide();
			return null;
		}
		dialog.LabelText = $"Page {++currentPage} of document";
		return underlyingController.OnStartPage(document, e);
	}

	private void Set_PrinterSettings_PrintFileName(PrinterSettings settings, string filename)
	{
		settings.PrintFileName = filename;
	}

	/// <summary>Begins the control sequence that determines when and how to print a document.</summary>
	/// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document currently being printed.</param>
	/// <param name="e">A <see cref="T:System.Drawing.Printing.PrintEventArgs" /> that contains the event data.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override void OnStartPrint(PrintDocument document, PrintEventArgs e)
	{
		try
		{
			currentPage = 0;
			dialog.Show();
			if (document.PrinterSettings.PrintToFile)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				if (saveFileDialog.ShowDialog() != DialogResult.OK)
				{
					throw new Exception("The operation was canceled by the user");
				}
				Set_PrinterSettings_PrintFileName(document.PrinterSettings, saveFileDialog.FileName);
			}
			underlyingController.OnStartPrint(document, e);
		}
		catch
		{
			dialog.Hide();
			throw;
		}
	}
}
