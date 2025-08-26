using System.Collections;

namespace System.Web.UI.WebControls.WebParts;

public class TypeCollection : ReadOnlyCollectionBase
{
	public static readonly TypeCollection Empty = new TypeCollection();

	public Type this[int index] => (Type)base.InnerList[index];

	public TypeCollection()
	{
	}

	public TypeCollection(ICollection types)
	{
		base.InnerList.AddRange(types);
	}

	public TypeCollection(TypeCollection existingTypes, ICollection types)
	{
		base.InnerList.AddRange(existingTypes.InnerList);
		base.InnerList.AddRange(types);
	}

	public bool Contains(Type value)
	{
		return base.InnerList.Contains(value);
	}

	public void CopyTo(Type[] array, int index)
	{
		base.InnerList.CopyTo(0, array, index, Count);
	}

	public int IndexOf(Type value)
	{
		return base.InnerList.IndexOf(value);
	}
}
