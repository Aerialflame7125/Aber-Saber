using System;

public static class ArrayHelpers
{
	public static T[] CreateOrEnlargeArray<T>(T[] array, int minimumCapacity)
	{
		if (array == null)
		{
			return new T[minimumCapacity + 1];
		}
		if (array.Length <= minimumCapacity)
		{
			T[] array2 = new T[minimumCapacity + 1];
			Array.Copy(array, array2, array.Length);
			return array2;
		}
		return array;
	}
}
