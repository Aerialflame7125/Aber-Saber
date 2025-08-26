using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.Design;

/// <summary>Provides a user interface for a <see cref="T:System.Windows.Forms.Design.WindowsFormsComponentEditor" />.</summary>
[ToolboxItem(false)]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ComVisible(true)]
public class ComponentEditorForm : Form
{
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new virtual bool AutoSize
	{
		get
		{
			return base.AutoSize;
		}
		set
		{
			base.AutoSize = value;
		}
	}

	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler AutoSizeChanged
	{
		add
		{
			base.AutoSizeChanged += value;
		}
		remove
		{
			base.AutoSizeChanged -= value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.ComponentEditorForm" /> class.</summary>
	/// <param name="component">The component to be edited. </param>
	/// <param name="pageTypes">The set of <see cref="T:System.Windows.Forms.Design.ComponentEditorPage" /> objects to be shown in the form. </param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="component" /> is not an <see cref="T:System.ComponentModel.IComponent" />.</exception>
	[System.MonoTODO]
	public ComponentEditorForm(object component, Type[] pageTypes)
	{
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.Activated" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	[System.MonoTODO]
	protected override void OnActivated(EventArgs e)
	{
	}

	/// <summary>Switches between component editor pages.</summary>
	/// <param name="source">The source of the event.</param>
	/// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewEventArgs" /> that contains the event data.</param>
	/// <exception cref="T:System.ComponentModel.Design.CheckoutException">A designer file is checked into source code control and cannot be changed.</exception>
	[System.MonoTODO]
	protected virtual void OnSelChangeSelector(object source, TreeViewEventArgs e)
	{
	}

	/// <summary>Provides a method to override in order to preprocess input messages before they are dispatched.</summary>
	/// <returns>true if the specified message is for a component editor page; otherwise, false.</returns>
	/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" /> that specifies the message to preprocess. </param>
	[System.MonoTODO]
	public override bool PreProcessMessage(ref Message msg)
	{
		throw new NotImplementedException();
	}

	/// <summary>Shows the form. The form will have no owner window.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values indicating the result code returned from the dialog box.</returns>
	[System.MonoTODO]
	public virtual DialogResult ShowForm()
	{
		throw new NotImplementedException();
	}

	/// <summary>Shows the specified page of the specified form. The form will have no owner window.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values indicating the result code returned from the dialog box.</returns>
	/// <param name="page">The index of the page to show. </param>
	[System.MonoTODO]
	public virtual DialogResult ShowForm(int page)
	{
		throw new NotImplementedException();
	}

	/// <summary>Shows the form with the specified owner.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values indicating the result code returned from the dialog box.</returns>
	/// <param name="owner">The <see cref="T:System.Windows.Forms.IWin32Window" /> to own the dialog. </param>
	[System.MonoTODO]
	public virtual DialogResult ShowForm(IWin32Window owner)
	{
		throw new NotImplementedException();
	}

	/// <summary>Shows the form and the specified page with the specified owner.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values indicating the result code returned from the dialog box.</returns>
	/// <param name="owner">The <see cref="T:System.Windows.Forms.IWin32Window" /> to own the dialog. </param>
	/// <param name="page">The index of the page to show. </param>
	[System.MonoTODO]
	public virtual DialogResult ShowForm(IWin32Window owner, int page)
	{
		throw new NotImplementedException();
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HelpRequested" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.HelpEventArgs" /> that contains the event data.</param>
	[System.MonoTODO]
	protected override void OnHelpRequested(HelpEventArgs e)
	{
	}
}
