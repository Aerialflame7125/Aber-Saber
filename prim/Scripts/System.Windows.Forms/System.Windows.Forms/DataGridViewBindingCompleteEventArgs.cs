using System.ComponentModel;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.DataBindingComplete" /> event.</summary>
/// <filterpriority>2</filterpriority>
public class DataGridViewBindingCompleteEventArgs : EventArgs
{
	private ListChangedType listChangedType;

	/// <summary>Gets a value specifying how the list changed.</summary>
	/// <returns>One of the <see cref="T:System.ComponentModel.ListChangedType" /> values.</returns>
	/// <filterpriority>1</filterpriority>
	public ListChangedType ListChangedType => listChangedType;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewBindingCompleteEventArgs" /> class.</summary>
	/// <param name="listChangedType">One of the <see cref="T:System.ComponentModel.ListChangedType" /> values.</param>
	public DataGridViewBindingCompleteEventArgs(ListChangedType listChangedType)
	{
		this.listChangedType = listChangedType;
	}
}
