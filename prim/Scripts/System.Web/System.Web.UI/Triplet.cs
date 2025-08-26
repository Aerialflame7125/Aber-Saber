using System.Security.Permissions;

namespace System.Web.UI;

/// <summary>Provides a basic utility class that is used to store three related objects.</summary>
[Serializable]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class Triplet
{
	/// <summary>Gets or sets the first <see langword="object" /> of the triplet.</summary>
	public object First;

	/// <summary>Gets or sets the second <see langword="object" /> of the triplet.</summary>
	public object Second;

	/// <summary>Gets or sets the third <see langword="object" /> of the triplet.</summary>
	public object Third;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Triplet" /> class. </summary>
	public Triplet()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Triplet" /> class and sets the first two objects. </summary>
	/// <param name="x">Object assigned to <see cref="F:System.Web.UI.Triplet.First" />.</param>
	/// <param name="y">Object assigned to <see cref="F:System.Web.UI.Triplet.Second" />.</param>
	public Triplet(object x, object y)
	{
		First = x;
		Second = y;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Triplet" /> class with the provided three objects. </summary>
	/// <param name="x">Object assigned to <see cref="F:System.Web.UI.Triplet.First" />.</param>
	/// <param name="y">Object assigned to <see cref="F:System.Web.UI.Triplet.Second" />.</param>
	/// <param name="z">Object assigned to <see cref="F:System.Web.UI.Triplet.Third" />.</param>
	public Triplet(object x, object y, object z)
	{
		First = x;
		Second = y;
		Third = z;
	}
}
