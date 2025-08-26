namespace System.Web.Caching;

/// <summary>Represents a single HTTP header that is included in a response from the output cache.</summary>
[Serializable]
public sealed class HeaderElement
{
	/// <summary>Gets the name of an HTTP header that is in the output cache.</summary>
	/// <returns>The name of the HTTP header. </returns>
	public string Name { get; private set; }

	/// <summary>Gets the value of an HTTP header that is in the output cache.</summary>
	/// <returns>The value of the HTTP header.</returns>
	public string Value { get; private set; }

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.HeaderElement" /> class. </summary>
	/// <param name="name">The name of the HTTP header.</param>
	/// <param name="value">The value of the HTTP header.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="name" /> is <see langword="null" />. </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="value" /> is <see langword="null" />. </exception>
	public HeaderElement(string name, string value)
	{
		if (name == null)
		{
			throw new ArgumentNullException("name");
		}
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		Name = name;
		Value = value;
	}
}
