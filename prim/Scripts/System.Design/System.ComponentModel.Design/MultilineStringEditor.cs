using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.ComponentModel.Design;

/// <summary>Displays a dialog for editing multi-line strings in design mode.</summary>
public sealed class MultilineStringEditor : UITypeEditor
{
	private class EditorControl : TextBox
	{
		public EditorControl()
		{
			Multiline = true;
			base.AcceptsReturn = true;
			base.Height = 135;
			base.Width = 280;
			base.ScrollBars = ScrollBars.Both;
			base.WordWrap = false;
			base.BorderStyle = BorderStyle.FixedSingle;
		}
	}

	private IWindowsFormsEditorService editorService;

	private EditorControl control = new EditorControl();

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.MultilineStringEditor" /> class.</summary>
	public MultilineStringEditor()
	{
	}

	/// <summary>Edits the specified object value using the edit style provided by <see cref="M:System.Drawing.Design.ImageEditor.GetEditStyle(System.ComponentModel.ITypeDescriptorContext)" />.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information.</param>
	/// <param name="provider">A service provider object through which editing services can be obtained.</param>
	/// <param name="value">An instance of the value being edited.</param>
	/// <returns>The new value of the object. If the value of the object has not changed, this method should return the same object passed to it.</returns>
	public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
	{
		if (context != null && provider != null)
		{
			editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
			if (editorService != null)
			{
				if (value == null)
				{
					value = string.Empty;
				}
				else if (!(value is string))
				{
					return value;
				}
				control.Text = (string)value;
				editorService.DropDownControl(control);
				return control.Text;
			}
		}
		return base.EditValue(context, provider, value);
	}

	/// <summary>Gets the editing style of the <see cref="M:System.Drawing.Design.ImageEditor.EditValue(System.ComponentModel.ITypeDescriptorContext,System.IServiceProvider,System.Object)" /> method.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information.</param>
	/// <returns>A <see cref="T:System.Drawing.Design.UITypeEditorEditStyle" /> enumeration value indicating the supported editing style.</returns>
	public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
	{
		return UITypeEditorEditStyle.DropDown;
	}

	/// <summary>Gets a value indicating whether this editor supports painting a representation of an object's value.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information.</param>
	/// <returns>
	///   <see langword="false" />, indicating that this <see cref="T:System.Drawing.Design.UITypeEditor" /> does not display a visual representation in the Properties Window.</returns>
	public override bool GetPaintValueSupported(ITypeDescriptorContext context)
	{
		return false;
	}
}
