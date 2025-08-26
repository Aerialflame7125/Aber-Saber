namespace System.Web.UI;

/// <summary>Provides a utility string class that is used by the <see cref="T:System.Web.UI.ObjectStateFormatter" /> class to optimize object graph serialization. This class cannot be inherited.</summary>
[Serializable]
public sealed class IndexedString
{
	private string _value;

	/// <summary>Gets the string associated with the <see cref="T:System.Web.UI.IndexedString" /> object.</summary>
	/// <returns>An initialized string.</returns>
	public string Value => _value;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.IndexedString" /> class. </summary>
	/// <param name="s">The string.</param>
	/// <exception cref="T:System.ArgumentNullException">The string parameter passed to the constructor is <see langword="null" /> or <see cref="F:System.String.Empty" />.</exception>
	public IndexedString(string s)
	{
		if (string.IsNullOrEmpty(s))
		{
			throw new ArgumentNullException("s");
		}
		_value = s;
	}
}
