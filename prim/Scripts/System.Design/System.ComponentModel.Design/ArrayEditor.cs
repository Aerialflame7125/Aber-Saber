namespace System.ComponentModel.Design;

/// <summary>Provides a user interface for editing arrays at design time.</summary>
public class ArrayEditor : CollectionEditor
{
	/// <summary>Initializes a new instance of <see cref="T:System.ComponentModel.Design.ArrayEditor" /> using the specified data type for the array.</summary>
	/// <param name="type">The data type of the items in the array.</param>
	public ArrayEditor(Type type)
		: base(type)
	{
	}

	/// <summary>Gets the data type that this collection is designed to contain.</summary>
	/// <returns>A <see cref="T:System.Type" /> that indicates the data type that the collection is designed to contain.</returns>
	protected override Type CreateCollectionItemType()
	{
		return base.CollectionType.GetElementType();
	}

	/// <summary>Gets the items in the array.</summary>
	/// <param name="editValue">The array from which to retrieve the items.</param>
	/// <returns>An array consisting of the items within the specified array. If the object specified in the <paramref name="editValue" /> parameter is not an array, a new empty object is returned.</returns>
	protected override object[] GetItems(object editValue)
	{
		if (editValue == null)
		{
			return null;
		}
		if (!(editValue is Array))
		{
			return new object[0];
		}
		Array obj = (Array)editValue;
		object[] array = new object[obj.Length];
		obj.CopyTo(array, 0);
		return array;
	}

	/// <summary>Sets the items in the array.</summary>
	/// <param name="editValue">The array to set the items to.</param>
	/// <param name="value">The array of objects to set as the items of the array.</param>
	/// <returns>An instance of the new array. If the object specified by the <paramref name="editValue" /> parameter is not an array, the object specified by the <paramref name="editValue" /> parameter is returned.</returns>
	protected override object SetItems(object editValue, object[] value)
	{
		if (editValue == null)
		{
			return null;
		}
		Array array = Array.CreateInstance(base.CollectionItemType, value.Length);
		value.CopyTo(array, 0);
		return array;
	}
}
