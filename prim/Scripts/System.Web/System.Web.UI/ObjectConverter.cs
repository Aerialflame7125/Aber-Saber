namespace System.Web.UI;

/// <summary>Converts an object from one object type to another object type. This class is obsolete. Use the <see cref="T:System.Convert" /> class and the <see cref="M:System.String.Format(System.String,System.Object)" /> method instead.</summary>
[Obsolete("The recommended alternative is System.Convert and String.Format. http://go.microsoft.com/fwlink/?linkid=14202")]
public sealed class ObjectConverter
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ObjectConverter" /> class. This class is obsolete. Use the <see cref="T:System.Convert" /> class and the <see cref="M:System.String.Format(System.String,System.Object)" /> method instead.</summary>
	public ObjectConverter()
	{
	}

	/// <summary>Converts an object from one object type to another object type. This class is obsolete. Use the <see cref="T:System.Convert" /> class and the <see cref="M:System.String.Format(System.String,System.Object)" /> method instead.</summary>
	/// <param name="value">The object to convert.</param>
	/// <param name="toType">The <see cref="T:System.Type" /> to convert <paramref name="value" /> to.</param>
	/// <param name="formatString">The format string to apply during conversion.</param>
	/// <returns>The converted object.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="Value" /> cannot be converted to type <paramref name="toType" /> with this method.</exception>
	public static object ConvertValue(object value, Type toType, string formatString)
	{
		throw new NotImplementedException("Not implemented and [Obsolete]");
	}
}
