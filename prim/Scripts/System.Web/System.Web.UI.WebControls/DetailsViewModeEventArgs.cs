using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DetailsView.ModeChanging" /> event.</summary>
public class DetailsViewModeEventArgs : CancelEventArgs
{
	private DetailsViewMode _mode;

	private bool _cancelingEdit;

	/// <summary>Gets a value indicating whether the <see cref="E:System.Web.UI.WebControls.DetailsView.ModeChanging" /> event was raised as a result of the user canceling an edit operation.</summary>
	/// <returns>
	///     <see langword="true" /> to indicate the <see cref="E:System.Web.UI.WebControls.DetailsView.ModeChanging" /> event was raised as a result of the user canceling an edit operation; otherwise, <see langword="false" />.</returns>
	public bool CancelingEdit => _cancelingEdit;

	/// <summary>Gets or sets the mode to which the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control is changing.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.DetailsViewMode" /> enumeration values.</returns>
	public DetailsViewMode NewMode
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DetailsViewModeEventArgs" /> class.</summary>
	/// <param name="mode">One of the <see cref="T:System.Web.UI.WebControls.DetailsViewMode" /> enumeration values.</param>
	/// <param name="cancelingEdit">
	///       <see langword="true" /> to indicate the <see cref="E:System.Web.UI.WebControls.DetailsView.ModeChanging" /> event was raised as a result of the user canceling an edit operation; otherwise, <see langword="false" />.</param>
	public DetailsViewModeEventArgs(DetailsViewMode mode, bool cancelingEdit)
		: base(cancel: false)
	{
		_mode = mode;
		_cancelingEdit = cancelingEdit;
	}
}
