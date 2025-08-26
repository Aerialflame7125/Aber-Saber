using System.ComponentModel;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.MaskedTextBox.MaskInputRejected" /> event.</summary>
/// <filterpriority>2</filterpriority>
public class MaskInputRejectedEventArgs : EventArgs
{
	private int position;

	private MaskedTextResultHint rejection_hint;

	/// <summary>Gets the position in the mask corresponding to the invalid input character.</summary>
	/// <returns>An <see cref="T:System.Int32" /> value that contains the zero-based position of the character that failed the mask. The position includes literal characters.</returns>
	/// <filterpriority>1</filterpriority>
	public int Position => position;

	/// <summary>Gets an enumerated value that describes why the input character was rejected.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that generally describes why the character was rejected.</returns>
	public MaskedTextResultHint RejectionHint => rejection_hint;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MaskInputRejectedEventArgs" /> class.</summary>
	/// <param name="position">An <see cref="T:System.Int32" /> value that contains the zero-based position of the character that failed the mask. The position includes literal characters.</param>
	/// <param name="rejectionHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that generally describes why the character was rejected.</param>
	public MaskInputRejectedEventArgs(int position, MaskedTextResultHint rejectionHint)
	{
		this.position = position;
		rejection_hint = rejectionHint;
	}
}
