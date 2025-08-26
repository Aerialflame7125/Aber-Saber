using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.HotSpot" /> objects inside an <see cref="T:System.Web.UI.WebControls.ImageMap" /> control. This class cannot be inherited.</summary>
[Editor("System.Web.UI.Design.WebControls.HotSpotCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class HotSpotCollection : StateManagedCollection
{
	private static Type[] _knownTypes = new Type[3]
	{
		typeof(CircleHotSpot),
		typeof(PolygonHotSpot),
		typeof(RectangleHotSpot)
	};

	/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object at the specified index in the <see cref="T:System.Web.UI.WebControls.HotSpotCollection" /> collection.</summary>
	/// <param name="index">The ordinal index value that specifies the location of the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object in the collection. </param>
	/// <returns>The <see cref="T:System.Web.UI.WebControls.HotSpot" /> object at the specified index in the <see cref="T:System.Web.UI.WebControls.HotSpotCollection" /> collection.</returns>
	public HotSpot this[int index] => (HotSpot)((IList)this)[index];

	/// <summary>Appends a specified <see cref="T:System.Web.UI.WebControls.HotSpot" /> object to the end of the <see cref="T:System.Web.UI.WebControls.HotSpotCollection" /> collection.</summary>
	/// <param name="spot">The <see cref="T:System.Web.UI.WebControls.HotSpot" /> object to append to the collection. </param>
	/// <returns>The index at which the object was added to the collection.</returns>
	public int Add(HotSpot spot)
	{
		return ((IList)this).Add((object)spot);
	}

	protected override object CreateKnownType(int index)
	{
		return index switch
		{
			0 => new CircleHotSpot(), 
			1 => new PolygonHotSpot(), 
			2 => new RectangleHotSpot(), 
			_ => throw new ArgumentOutOfRangeException("index"), 
		};
	}

	protected override Type[] GetKnownTypes()
	{
		return _knownTypes;
	}

	/// <summary>Inserts a specified <see cref="T:System.Web.UI.WebControls.HotSpot" /> object into the <see cref="T:System.Web.UI.WebControls.HotSpotCollection" /> collection at the specified index location.</summary>
	/// <param name="index">The array index at which to add the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object. </param>
	/// <param name="spot">The <see cref="T:System.Web.UI.WebControls.HotSpot" /> object to add to the collection. </param>
	public void Insert(int index, HotSpot spot)
	{
		((IList)this).Insert(index, (object)spot);
	}

	protected override void OnValidate(object o)
	{
		base.OnValidate(o);
		if (!(o is HotSpot))
		{
			throw new ArgumentException("o is not a HotSpot");
		}
	}

	/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.HotSpot" /> object from the <see cref="T:System.Web.UI.WebControls.HotSpotCollection" /> collection.</summary>
	/// <param name="spot">The <see cref="T:System.Web.UI.WebControls.HotSpot" /> object to remove from the collection. </param>
	public void Remove(HotSpot spot)
	{
		((IList)this).Remove((object)spot);
	}

	/// <summary>Removes the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object at the specified index location from the collection.</summary>
	/// <param name="index">The array index from which to remove the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object. </param>
	public void RemoveAt(int index)
	{
		((IList)this).RemoveAt(index);
	}

	protected override void SetDirtyObject(object o)
	{
		((HotSpot)o).SetDirty();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.HotSpotCollection" /> class.</summary>
	public HotSpotCollection()
	{
	}
}
