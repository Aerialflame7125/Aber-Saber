namespace System.Windows.Forms.Design;

internal class StringArrayEditor : StringCollectionEditor
{
	public StringArrayEditor(Type type)
		: base(type)
	{
	}

	protected override object[] GetItems(object editValue)
	{
		if (!(editValue is Array array))
		{
			return new object[0];
		}
		object[] array2 = new object[array.GetLength(0)];
		Array.Copy(array, array2, array2.Length);
		return array2;
	}

	protected override object SetItems(object editValue, object[] value)
	{
		if (!(editValue is Array))
		{
			return editValue;
		}
		Array array = Array.CreateInstance(base.CollectionItemType, value.Length);
		Array.Copy(value, array, value.Length);
		return array;
	}

	protected override Type CreateCollectionItemType()
	{
		return base.CollectionType.GetElementType();
	}
}
