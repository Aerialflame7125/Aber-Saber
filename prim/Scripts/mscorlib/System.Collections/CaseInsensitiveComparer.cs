using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Collections;

/// <summary>Compares two objects for equivalence, ignoring the case of strings.</summary>
[Serializable]
[ComVisible(true)]
public class CaseInsensitiveComparer : IComparer
{
	private CompareInfo m_compareInfo;

	private static volatile CaseInsensitiveComparer m_InvariantCaseInsensitiveComparer;

	/// <summary>Gets an instance of <see cref="T:System.Collections.CaseInsensitiveComparer" /> that is associated with the <see cref="P:System.Threading.Thread.CurrentCulture" /> of the current thread and that is always available.</summary>
	/// <returns>An instance of <see cref="T:System.Collections.CaseInsensitiveComparer" /> that is associated with the <see cref="P:System.Threading.Thread.CurrentCulture" /> of the current thread.</returns>
	public static CaseInsensitiveComparer Default => new CaseInsensitiveComparer(CultureInfo.CurrentCulture);

	/// <summary>Gets an instance of <see cref="T:System.Collections.CaseInsensitiveComparer" /> that is associated with <see cref="P:System.Globalization.CultureInfo.InvariantCulture" /> and that is always available.</summary>
	/// <returns>An instance of <see cref="T:System.Collections.CaseInsensitiveComparer" /> that is associated with <see cref="P:System.Globalization.CultureInfo.InvariantCulture" />.</returns>
	public static CaseInsensitiveComparer DefaultInvariant
	{
		get
		{
			if (m_InvariantCaseInsensitiveComparer == null)
			{
				m_InvariantCaseInsensitiveComparer = new CaseInsensitiveComparer(CultureInfo.InvariantCulture);
			}
			return m_InvariantCaseInsensitiveComparer;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Collections.CaseInsensitiveComparer" /> class using the <see cref="P:System.Threading.Thread.CurrentCulture" /> of the current thread.</summary>
	public CaseInsensitiveComparer()
	{
		m_compareInfo = CultureInfo.CurrentCulture.CompareInfo;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Collections.CaseInsensitiveComparer" /> class using the specified <see cref="T:System.Globalization.CultureInfo" />.</summary>
	/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use for the new <see cref="T:System.Collections.CaseInsensitiveComparer" />.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="culture" /> is <see langword="null" />.</exception>
	public CaseInsensitiveComparer(CultureInfo culture)
	{
		if (culture == null)
		{
			throw new ArgumentNullException("culture");
		}
		m_compareInfo = culture.CompareInfo;
	}

	/// <summary>Performs a case-insensitive comparison of two objects of the same type and returns a value indicating whether one is less than, equal to, or greater than the other.</summary>
	/// <param name="a">The first object to compare.</param>
	/// <param name="b">The second object to compare.</param>
	/// <returns>A signed integer that indicates the relative values of <paramref name="a" /> and <paramref name="b" />, as shown in the following table.  
	///   Value  
	///
	///   Meaning  
	///
	///   Less than zero  
	///
	///  <paramref name="a" /> is less than <paramref name="b" />, with casing ignored.  
	///
	///   Zero  
	///
	///  <paramref name="a" /> equals <paramref name="b" />, with casing ignored.  
	///
	///   Greater than zero  
	///
	///  <paramref name="a" /> is greater than <paramref name="b" />, with casing ignored.</returns>
	/// <exception cref="T:System.ArgumentException">Neither <paramref name="a" /> nor <paramref name="b" /> implements the <see cref="T:System.IComparable" /> interface.  
	///  -or-  
	///  <paramref name="a" /> and <paramref name="b" /> are of different types.</exception>
	public int Compare(object a, object b)
	{
		string text = a as string;
		string text2 = b as string;
		if (text != null && text2 != null)
		{
			return m_compareInfo.Compare(text, text2, CompareOptions.IgnoreCase);
		}
		return Comparer.Default.Compare(a, b);
	}
}
