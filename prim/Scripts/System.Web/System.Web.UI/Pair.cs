namespace System.Web.UI;

/// <summary>Provides a basic utility class that is used to store two related objects. </summary>
[Serializable]
public sealed class Pair
{
	/// <summary>Gets or sets the first object of the object pair.</summary>
	public object First;

	/// <summary>Gets or sets the second object of the object pair.</summary>
	public object Second;

	/// <summary>Creates a new, uninitialized instance of the <see cref="T:System.Web.UI.Pair" /> class.</summary>
	public Pair()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Pair" /> class, using the specified object pair.</summary>
	/// <param name="x">An object. </param>
	/// <param name="y">An object. </param>
	public Pair(object x, object y)
	{
		First = x;
		Second = y;
	}
}
