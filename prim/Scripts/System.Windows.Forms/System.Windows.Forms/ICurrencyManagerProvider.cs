namespace System.Windows.Forms;

/// <summary>Provides custom binding management for components.</summary>
/// <filterpriority>2</filterpriority>
public interface ICurrencyManagerProvider
{
	/// <summary>Gets the <see cref="T:System.Windows.Forms.CurrencyManager" /> associated with this <see cref="T:System.Windows.Forms.ICurrencyManagerProvider" />. </summary>
	/// <returns>The <see cref="T:System.Windows.Forms.CurrencyManager" /> associated with this <see cref="T:System.Windows.Forms.ICurrencyManagerProvider" />.</returns>
	/// <filterpriority>1</filterpriority>
	CurrencyManager CurrencyManager { get; }

	/// <summary>Gets the <see cref="T:System.Windows.Forms.CurrencyManager" /> for this <see cref="T:System.Windows.Forms.ICurrencyManagerProvider" /> and the specified data member.</summary>
	/// <returns>The related <see cref="T:System.Windows.Forms.CurrencyManager" /> obtained from this <see cref="T:System.Windows.Forms.ICurrencyManagerProvider" /> and the specified data member.</returns>
	/// <param name="dataMember">The name of the column or list, within the data source, to obtain the <see cref="T:System.Windows.Forms.CurrencyManager" /> for.</param>
	CurrencyManager GetRelatedCurrencyManager(string dataMember);
}
