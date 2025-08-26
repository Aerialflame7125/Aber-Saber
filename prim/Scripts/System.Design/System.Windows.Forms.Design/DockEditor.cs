using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace System.Windows.Forms.Design;

/// <summary>Provides a user interface for specifying a <see cref="P:System.Windows.Forms.Control.Dock" /> property.</summary>
public sealed class DockEditor : UITypeEditor
{
	private class DockEditorControl : UserControl
	{
		private CheckBox buttonNone;

		private Panel panel1;

		private CheckBox buttonBottom;

		private CheckBox buttonTop;

		private Panel panel2;

		private CheckBox buttonLeft;

		private CheckBox buttonRight;

		private CheckBox buttonFill;

		private IWindowsFormsEditorService editorService;

		private DockStyle dockStyle;

		public DockStyle DockStyle
		{
			get
			{
				return dockStyle;
			}
			set
			{
				dockStyle = value;
				buttonNone.Checked = false;
				buttonBottom.Checked = false;
				buttonTop.Checked = false;
				buttonLeft.Checked = false;
				buttonRight.Checked = false;
				buttonFill.Checked = false;
				switch (DockStyle)
				{
				case DockStyle.Fill:
					buttonFill.CheckState = CheckState.Checked;
					break;
				case DockStyle.None:
					buttonNone.CheckState = CheckState.Checked;
					break;
				case DockStyle.Left:
					buttonLeft.CheckState = CheckState.Checked;
					break;
				case DockStyle.Right:
					buttonRight.CheckState = CheckState.Checked;
					break;
				case DockStyle.Top:
					buttonTop.CheckState = CheckState.Checked;
					break;
				case DockStyle.Bottom:
					buttonBottom.CheckState = CheckState.Checked;
					break;
				}
			}
		}

		public DockEditorControl(IWindowsFormsEditorService editorService)
		{
			buttonNone = new CheckBox();
			panel1 = new Panel();
			buttonBottom = new CheckBox();
			buttonTop = new CheckBox();
			panel2 = new Panel();
			buttonLeft = new CheckBox();
			buttonRight = new CheckBox();
			buttonFill = new CheckBox();
			panel1.SuspendLayout();
			panel2.SuspendLayout();
			SuspendLayout();
			buttonNone.Appearance = Appearance.Button;
			buttonNone.Dock = DockStyle.Bottom;
			buttonNone.Location = new Point(0, 92);
			buttonNone.Size = new Size(150, 23);
			buttonNone.TabIndex = 5;
			buttonNone.Text = "None";
			buttonNone.TextAlign = ContentAlignment.MiddleLeft;
			buttonNone.Click += buttonClick;
			panel1.Controls.Add(panel2);
			panel1.Controls.Add(buttonTop);
			panel1.Controls.Add(buttonBottom);
			panel1.Dock = DockStyle.Fill;
			panel1.Location = new Point(0, 0);
			panel1.Name = "panel1";
			panel1.Size = new Size(150, 92);
			panel1.TabStop = false;
			buttonBottom.Appearance = Appearance.Button;
			buttonBottom.Dock = DockStyle.Bottom;
			buttonBottom.Location = new Point(0, 69);
			buttonBottom.Name = "buttonBottom";
			buttonBottom.Size = new Size(150, 23);
			buttonBottom.TabIndex = 5;
			buttonBottom.Click += buttonClick;
			buttonTop.Appearance = Appearance.Button;
			buttonTop.Dock = DockStyle.Top;
			buttonTop.Location = new Point(0, 0);
			buttonTop.Name = "buttonTop";
			buttonTop.Size = new Size(150, 23);
			buttonTop.TabIndex = 1;
			buttonTop.Click += buttonClick;
			panel2.Controls.Add(buttonFill);
			panel2.Controls.Add(buttonRight);
			panel2.Controls.Add(buttonLeft);
			panel2.Dock = DockStyle.Fill;
			panel2.Location = new Point(0, 23);
			panel2.Size = new Size(150, 46);
			panel2.TabIndex = 2;
			panel2.TabStop = false;
			buttonLeft.Appearance = Appearance.Button;
			buttonLeft.Dock = DockStyle.Left;
			buttonLeft.Location = new Point(0, 0);
			buttonLeft.Size = new Size(24, 46);
			buttonLeft.TabIndex = 2;
			buttonLeft.Click += buttonClick;
			buttonRight.Appearance = Appearance.Button;
			buttonRight.Dock = DockStyle.Right;
			buttonRight.Location = new Point(126, 0);
			buttonRight.Size = new Size(24, 46);
			buttonRight.TabIndex = 4;
			buttonRight.Click += buttonClick;
			buttonFill.Appearance = Appearance.Button;
			buttonFill.Dock = DockStyle.Fill;
			buttonFill.Location = new Point(24, 0);
			buttonFill.Size = new Size(102, 46);
			buttonFill.TabIndex = 3;
			buttonFill.Click += buttonClick;
			base.Controls.Add(panel1);
			base.Controls.Add(buttonNone);
			base.Size = new Size(150, 115);
			panel1.ResumeLayout(performLayout: false);
			panel2.ResumeLayout(performLayout: false);
			ResumeLayout(performLayout: false);
			this.editorService = editorService;
			dockStyle = DockStyle.None;
		}

		private void buttonClick(object sender, EventArgs e)
		{
			if (sender == buttonNone)
			{
				dockStyle = DockStyle.None;
			}
			else if (sender == buttonFill)
			{
				dockStyle = DockStyle.Fill;
			}
			else if (sender == buttonLeft)
			{
				dockStyle = DockStyle.Left;
			}
			else if (sender == buttonRight)
			{
				dockStyle = DockStyle.Right;
			}
			else if (sender == buttonTop)
			{
				dockStyle = DockStyle.Top;
			}
			else if (sender == buttonBottom)
			{
				dockStyle = DockStyle.Bottom;
			}
			editorService.CloseDropDown();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.DockEditor" /> class.</summary>
	public DockEditor()
	{
	}

	/// <summary>Edits the specified object value using the editor style provided by GetEditorStyle. A service provider is provided so that any required editing services can be obtained.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information.</param>
	/// <param name="provider">A service provider object through which editing services may be obtained.</param>
	/// <param name="value">An instance of the value being edited.</param>
	/// <returns>The new value of the object. If the value of the object hasn't changed, this should return the same object it was passed.</returns>
	public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
	{
		if (context != null && provider != null)
		{
			IWindowsFormsEditorService windowsFormsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
			if (windowsFormsEditorService != null)
			{
				DockEditorControl dockEditorControl = new DockEditorControl(windowsFormsEditorService);
				dockEditorControl.DockStyle = (DockStyle)value;
				windowsFormsEditorService.DropDownControl(dockEditorControl);
				return dockEditorControl.DockStyle;
			}
		}
		return base.EditValue(context, provider, value);
	}

	/// <summary>Retrieves the editing style of the EditValue method. If the method is not supported, this will return None.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information.</param>
	/// <returns>An enum value indicating the provided editing style.</returns>
	public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
	{
		return UITypeEditorEditStyle.DropDown;
	}
}
