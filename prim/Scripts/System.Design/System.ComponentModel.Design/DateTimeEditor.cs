using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.ComponentModel.Design;

/// <summary>This date time editor is a <see cref="T:System.Drawing.Design.UITypeEditor" /> suitable for visually editing <see cref="T:System.DateTime" /> objects.</summary>
public class DateTimeEditor : UITypeEditor
{
	private class EditorControl : MonthCalendar
	{
		public EditorControl()
		{
			base.MaxSelectionCount = 1;
		}
	}

	private IWindowsFormsEditorService editorService;

	private EditorControl control = new EditorControl();

	private DateTime editContent;

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DateTimeEditor" /> class.</summary>
	public DateTimeEditor()
	{
		control.DateSelected += control_DateSelected;
	}

	/// <summary>Edits the specified object value using the editor style provided by GetEditorStyle. A service provider is provided so that any required editing services can be obtained.</summary>
	/// <param name="context">A type descriptor context that can be used to provide additional context information.</param>
	/// <param name="provider">A service provider object through which editing services may be obtained.</param>
	/// <param name="value">An instance of the value being edited.</param>
	/// <returns>The new value of the object. If the value of the object hasn't changed, this should return the same object it was passed.</returns>
	public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
	{
		if (context != null && provider != null)
		{
			editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
			if (editorService != null)
			{
				if (!(value is DateTime))
				{
					return value;
				}
				editContent = (DateTime)value;
				if (editContent > control.MaxDate || editContent < control.MinDate)
				{
					control.SelectionStart = DateTime.Today;
				}
				else
				{
					control.SelectionStart = editContent;
				}
				editorService.DropDownControl(control);
				return editContent;
			}
		}
		return base.EditValue(context, provider, value);
	}

	private void control_DateSelected(object sender, DateRangeEventArgs e)
	{
		editContent = e.Start;
		editorService.CloseDropDown();
	}

	/// <summary>Retrieves the editing style of the <see cref="Overload:System.ComponentModel.Design.DateTimeEditor.EditValue" /> method. If the method is not supported, this will return None.</summary>
	/// <param name="context">A type descriptor context that can be used to provide additional context information.</param>
	/// <returns>An <see langword="enum" /> value indicating the provided editing style.</returns>
	public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
	{
		return UITypeEditorEditStyle.DropDown;
	}
}
