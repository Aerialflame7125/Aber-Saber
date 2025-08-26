namespace System.Windows.Forms;

/// <summary>Represents methods that process editing requests.</summary>
/// <filterpriority>2</filterpriority>
public interface IDataGridEditingService
{
	/// <summary>Begins an edit operation.</summary>
	/// <returns>true if the operation can be performed; otherwise false.</returns>
	/// <param name="gridColumn">The <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> to edit. </param>
	/// <param name="rowNumber">The index of the row to edit </param>
	/// <filterpriority>1</filterpriority>
	bool BeginEdit(DataGridColumnStyle gridColumn, int rowNumber);

	/// <summary>Ends the edit operation.</summary>
	/// <returns>true if value is commited; otherwise false.</returns>
	/// <param name="gridColumn">The <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> to edit. </param>
	/// <param name="rowNumber">The number of the row to edit </param>
	/// <param name="shouldAbort">True if an abort operation is requested </param>
	/// <filterpriority>1</filterpriority>
	bool EndEdit(DataGridColumnStyle gridColumn, int rowNumber, bool shouldAbort);
}
