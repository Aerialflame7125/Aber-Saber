using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.FormView.ModeChanging" /> event.</summary>
public class FormViewModeEventArgs : CancelEventArgs
{
	private FormViewMode _mode;

	private bool _cancelingEdit;

	/// <summary>Gets a value indicating whether the <see cref="E:System.Web.UI.WebControls.FormView.ModeChanging" /> event was raised as a result of the user canceling an edit operation.</summary>
	/// <returns>
	///     <see langword="true" /> to indicate the <see cref="E:System.Web.UI.WebControls.FormView.ModeChanging" /> event was raised as a result of the user canceling an edit operation; otherwise, <see langword="false" />.</returns>
	public bool CancelingEdit => _cancelingEdit;

	/// <summary>Gets or sets the mode to which the <see cref="T:System.Web.UI.WebControls.FormView" /> control is changing.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.FormViewMode" /> enumeration values.</returns>
	public FormViewMode NewMode
	{
		get
		{
			return _mode;
		}
		set
		{
			_mode = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FormViewModeEventArgs" /> class.</summary>
	/// <param name="mode">One of the <see cref="T:System.Web.UI.WebControls.FormViewMode" /> enumeration values.</param>
	/// <param name="cancelingEdit">
	///       <see langword="true" /> to indicate the <see cref="E:System.Web.UI.WebControls.FormView.ModeChanging" /> event was raised as a result of the user canceling an edit operation; otherwise, <see langword="false" />.</param>
	public FormViewModeEventArgs(FormViewMode mode, bool cancelingEdit)
		: base(cancel: false)
	{
		_mode = mode;
		_cancelingEdit = cancelingEdit;
	}
}
