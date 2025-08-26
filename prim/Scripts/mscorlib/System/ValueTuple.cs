using System.Collections;
using System.Collections.Generic;
using System.Numerics.Hashing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System;

/// <summary>Provides static methods for creating value tuples.</summary>
[Serializable]
[StructLayout(LayoutKind.Sequential, Size = 1)]
public struct ValueTuple : IEquatable<ValueTuple>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<ValueTuple>, IValueTupleInternal, ITuple
{
	/// <summary>Gets the length of this <see langword="ValueTuple" /> instance, which is always 0. There are no elements in a <see langword="ValueTuple" />.</summary>
	/// <returns>0, the number of elements in this <see langword="ValueTuple" /> instance.</returns>
	int ITuple.Length => 0;

	/// <summary>Returns an <see cref="T:System.IndexOutOfRangeException" />. There are no elements in a <see langword="ValueTuple" />.</summary>
	/// <param name="index">There is no acceptable value for <paramref name="index" />.</param>
	/// <returns>An <see cref="T:System.IndexOutOfRangeException" />.</returns>
	/// <exception cref="T:System.IndexOutOfRangeException">There is no acceptable value for <paramref name="index" />.</exception>
	object ITuple.this[int index]
	{
		get
		{
			throw new IndexOutOfRangeException();
		}
	}

	/// <summary>Returns a value that indicates whether the current <see cref="T:System.ValueTuple" /> instance is equal to a specified object.</summary>
	/// <param name="obj">The object to compare to the current instance.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.ValueTuple" /> instance; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		return obj is ValueTuple;
	}

	/// <summary>Determines whether two <see cref="T:System.ValueTuple" /> instances are equal. This method always returns <see langword="true" />.</summary>
	/// <param name="other">The value tuple to compare with the current instance.</param>
	/// <returns>This method always returns <see langword="true" />.</returns>
	public bool Equals(ValueTuple other)
	{
		return true;
	}

	/// <summary>Returns a value that indicates whether the current <see cref="T:System.ValueTuple" /> instance is equal to a specified object based on a specified comparison method.</summary>
	/// <param name="other">The object to compare with this instance.</param>
	/// <param name="comparer">An object that defines the method to use to evaluate whether the two objects are equal.</param>
	/// <returns>
	///   <see langword="true" /> if the current instance is equal to the specified object; otherwise, <see langword="false" />.</returns>
	bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
	{
		return other is ValueTuple;
	}

	/// <summary>Compares this <see cref="T:System.ValueTuple" /> instance with a specified object and returns an indication of their relative values.</summary>
	/// <param name="other">The object to compare with the current instance</param>
	/// <returns>0 if <paramref name="other" /> is a <see cref="T:System.ValueTuple" /> instance; otherwise, 1 if <paramref name="other" /> is <see langword="null" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="other" /> is not a <see cref="T:System.ValueTuple" /> instance.</exception>
	int IComparable.CompareTo(object other)
	{
		if (other == null)
		{
			return 1;
		}
		if (!(other is ValueTuple))
		{
			throw new ArgumentException(SR.Format("Argument must be of type {0}.", GetType().ToString()), "other");
		}
		return 0;
	}

	/// <summary>Compares the current <see cref="T:System.ValueTuple" /> instance to a specified <see cref="T:System.ValueTuple" /> instance.</summary>
	/// <param name="other">The object to compare with the current instance.</param>
	/// <returns>This method always returns 0.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="other" /> is not a <see cref="T:System.ValueTuple" /> instance.</exception>
	public int CompareTo(ValueTuple other)
	{
		return 0;
	}

	/// <summary>Compares the current <see cref="T:System.ValueTuple" /> instance to a specified object.</summary>
	/// <param name="other">The object to compare with the current instance.</param>
	/// <param name="comparer">An object that provides custom rules for comparison. This parameter is ignored.</param>
	/// <returns>Returns 0 if <paramref name="other" /> is a <see cref="T:System.ValueTuple" /> instance and 1 if <paramref name="other" /> is <see langword="null" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="other" /> is not a <see cref="T:System.ValueTuple" /> instance.</exception>
	int IStructuralComparable.CompareTo(object other, IComparer comparer)
	{
		if (other == null)
		{
			return 1;
		}
		if (!(other is ValueTuple))
		{
			throw new ArgumentException(SR.Format("Argument must be of type {0}.", GetType().ToString()), "other");
		}
		return 0;
	}

	/// <summary>Returns the hash code for the current <see cref="T:System.ValueTuple" /> instance.</summary>
	/// <returns>This method always return 0.</returns>
	public override int GetHashCode()
	{
		return 0;
	}

	/// <summary>Returns the hash code for this <see cref="T:System.ValueTuple" /> instance.</summary>
	/// <param name="comparer">An object whose <see cref="M:System.Collections.IEqualityComparer.GetHashCode(System.Object)" /> method computes the hash code. This parameter is ignored.</param>
	/// <returns>The hash code for this <see cref="T:System.ValueTuple" /> instance.</returns>
	int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
	{
		return 0;
	}

	int IValueTupleInternal.GetHashCode(IEqualityComparer comparer)
	{
		return 0;
	}

	/// <summary>Returns the string representation of this <see cref="T:System.ValueTuple" /> instance.</summary>
	/// <returns>This method always returns "()".</returns>
	public override string ToString()
	{
		return "()";
	}

	string IValueTupleInternal.ToStringEnd()
	{
		return ")";
	}

	/// <summary>Creates a new value tuple with zero components.</summary>
	/// <returns>A new value tuple with no components.</returns>
	public static ValueTuple Create()
	{
		return default(ValueTuple);
	}

	/// <summary>Creates a new value tuple with 1 component (a singleton).</summary>
	/// <param name="item1">The value of the value tuple's only component.</param>
	/// <typeparam name="T1">The type of the value tuple's only component.</typeparam>
	/// <returns>A value tuple with 1 component.</returns>
	public static ValueTuple<T1> Create<T1>(T1 item1)
	{
		return new ValueTuple<T1>(item1);
	}

	/// <summary>Creates a new value tuple with 2 components (a pair).</summary>
	/// <param name="item1">The value of the value tuple's first component.</param>
	/// <param name="item2">The value of the value tuple's second component.</param>
	/// <typeparam name="T1">The type of the value tuple's first component.</typeparam>
	/// <typeparam name="T2">The type of the value tuple's second component.</typeparam>
	/// <returns>A value tuple with 2 components.</returns>
	public static (T1, T2) Create<T1, T2>(T1 item1, T2 item2)
	{
		return (item1, item2);
	}

	/// <summary>Creates a new value tuple with 3 components (a triple).</summary>
	/// <param name="item1">The value of the value tuple's first component.</param>
	/// <param name="item2">The value of the value tuple's second component.</param>
	/// <param name="item3">The value of the value tuple's third component.</param>
	/// <typeparam name="T1">The type of the value tuple's first component.</typeparam>
	/// <typeparam name="T2">The type of the value tuple's second component.</typeparam>
	/// <typeparam name="T3">The type of the value tuple's third component.</typeparam>
	/// <returns>A value tuple with 3 components.</returns>
	public static (T1, T2, T3) Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
	{
		return (item1, item2, item3);
	}

	/// <summary>Creates a new value tuple with 4 components (a quadruple).</summary>
	/// <param name="item1">The value of the value tuple's first component.</param>
	/// <param name="item2">The value of the value tuple's second component.</param>
	/// <param name="item3">The value of the value tuple's third component.</param>
	/// <param name="item4">The value of the value tuple's fourth component.</param>
	/// <typeparam name="T1">The type of the value tuple's first component.</typeparam>
	/// <typeparam name="T2">The type of the value tuple's second component.</typeparam>
	/// <typeparam name="T3">The type of the value tuple's third component.</typeparam>
	/// <typeparam name="T4">The type of the value tuple's fourth component.</typeparam>
	/// <returns>A value tuple with 4 components.</returns>
	public static (T1, T2, T3, T4) Create<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4)
	{
		return (item1, item2, item3, item4);
	}

	/// <summary>Creates a new value tuple with 5 components (a quintuple).</summary>
	/// <param name="item1">The value of the value tuple's first component.</param>
	/// <param name="item2">The value of the value tuple's second component.</param>
	/// <param name="item3">The value of the value tuple's third component.</param>
	/// <param name="item4">The value of the value tuple's fourth component.</param>
	/// <param name="item5">The value of the value tuple's fifth component.</param>
	/// <typeparam name="T1">The type of the value tuple's first component.</typeparam>
	/// <typeparam name="T2">The type of the value tuple's second component.</typeparam>
	/// <typeparam name="T3">The type of the value tuple's third component.</typeparam>
	/// <typeparam name="T4">The type of the value tuple's fourth component.</typeparam>
	/// <typeparam name="T5">The type of the value tuple's fifth component.</typeparam>
	/// <returns>A value tuple with 5 components.</returns>
	public static (T1, T2, T3, T4, T5) Create<T1, T2, T3, T4, T5>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
	{
		return (item1, item2, item3, item4, item5);
	}

	/// <summary>Creates a new value tuple with 6 components (a sexuple).</summary>
	/// <param name="item1">The value of the value tuple's first component.</param>
	/// <param name="item2">The value of the value tuple's second component.</param>
	/// <param name="item3">The value of the value tuple's third component.</param>
	/// <param name="item4">The value of the value tuple's fourth component.</param>
	/// <param name="item5">The value of the value tuple's fifth component.</param>
	/// <param name="item6">The value of the value tuple's sixth component.</param>
	/// <typeparam name="T1">The type of the value tuple's first component.</typeparam>
	/// <typeparam name="T2">The type of the value tuple's second component.</typeparam>
	/// <typeparam name="T3">The type of the value tuple's third component.</typeparam>
	/// <typeparam name="T4">The type of the value tuple's fourth component.</typeparam>
	/// <typeparam name="T5">The type of the value tuple's fifth component.</typeparam>
	/// <typeparam name="T6">The type of the value tuple's sixth component.</typeparam>
	/// <returns>A value tuple with 6 components.</returns>
	public static (T1, T2, T3, T4, T5, T6) Create<T1, T2, T3, T4, T5, T6>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
	{
		return (item1, item2, item3, item4, item5, item6);
	}

	/// <summary>Creates a new value tuple with 7 components (a septuple).</summary>
	/// <param name="item1">The value of the value tuple's first component.</param>
	/// <param name="item2">The value of the value tuple's second component.</param>
	/// <param name="item3">The value of the value tuple's third component.</param>
	/// <param name="item4">The value of the value tuple's fourth component.</param>
	/// <param name="item5">The value of the value tuple's fifth component.</param>
	/// <param name="item6">The value of the value tuple's sixth component.</param>
	/// <param name="item7">The value of the value tuple's seventh component.</param>
	/// <typeparam name="T1">The type of the value tuple's first component.</typeparam>
	/// <typeparam name="T2">The type of the value tuple's second component.</typeparam>
	/// <typeparam name="T3">The type of the value tuple's third component.</typeparam>
	/// <typeparam name="T4">The type of the value tuple's fourth component.</typeparam>
	/// <typeparam name="T5">The type of the value tuple's fifth component.</typeparam>
	/// <typeparam name="T6">The type of the value tuple's sixth component.</typeparam>
	/// <typeparam name="T7">The type of the value tuple's seventh component.</typeparam>
	/// <returns>A value tuple with 7 components.</returns>
	public static (T1, T2, T3, T4, T5, T6, T7) Create<T1, T2, T3, T4, T5, T6, T7>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
	{
		return (item1, item2, item3, item4, item5, item6, item7);
	}

	/// <summary>Creates a new value tuple with 8 components (an octuple).</summary>
	/// <param name="item1">The value of the value tuple's first component.</param>
	/// <param name="item2">The value of the value tuple's second component.</param>
	/// <param name="item3">The value of the value tuple's third component.</param>
	/// <param name="item4">The value of the value tuple's fourth component.</param>
	/// <param name="item5">The value of the value tuple's fifth component.</param>
	/// <param name="item6">The value of the value tuple's sixth component.</param>
	/// <param name="item7">The value of the value tuple's seventh component.</param>
	/// <param name="item8">The value of the value tuple's eighth component.</param>
	/// <typeparam name="T1">The type of the value tuple's first component.</typeparam>
	/// <typeparam name="T2">The type of the value tuple's second component.</typeparam>
	/// <typeparam name="T3">The type of the value tuple's third component.</typeparam>
	/// <typeparam name="T4">The type of the value tuple's fourth component.</typeparam>
	/// <typeparam name="T5">The type of the value tuple's fifth component.</typeparam>
	/// <typeparam name="T6">The type of the value tuple's sixth component.</typeparam>
	/// <typeparam name="T7">The type of the value tuple's seventh component.</typeparam>
	/// <typeparam name="T8">The type of the value tuple's eighth component.</typeparam>
	/// <returns>A value tuple with 8 components.</returns>
	public static (T1, T2, T3, T4, T5, T6, T7, T8) Create<T1, T2, T3, T4, T5, T6, T7, T8>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
	{
		return new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>(item1, item2, item3, item4, item5, item6, item7, Create(item8));
	}

	internal static int CombineHashCodes(int h1, int h2)
	{
		return System.Numerics.Hashing.HashHelpers.Combine(System.Numerics.Hashing.HashHelpers.Combine(System.Numerics.Hashing.HashHelpers.RandomSeed, h1), h2);
	}

	internal static int CombineHashCodes(int h1, int h2, int h3)
	{
		return System.Numerics.Hashing.HashHelpers.Combine(CombineHashCodes(h1, h2), h3);
	}

	internal static int CombineHashCodes(int h1, int h2, int h3, int h4)
	{
		return System.Numerics.Hashing.HashHelpers.Combine(CombineHashCodes(h1, h2, h3), h4);
	}

	internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5)
	{
		return System.Numerics.Hashing.HashHelpers.Combine(CombineHashCodes(h1, h2, h3, h4), h5);
	}

	internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6)
	{
		return System.Numerics.Hashing.HashHelpers.Combine(CombineHashCodes(h1, h2, h3, h4, h5), h6);
	}

	internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7)
	{
		return System.Numerics.Hashing.HashHelpers.Combine(CombineHashCodes(h1, h2, h3, h4, h5, h6), h7);
	}

	internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8)
	{
		return System.Numerics.Hashing.HashHelpers.Combine(CombineHashCodes(h1, h2, h3, h4, h5, h6, h7), h8);
	}
}
/// <summary>Represents a value tuple with a single component.</summary>
/// <typeparam name="T1">The type of the value tuple's only element.</typeparam>
[Serializable]
public struct ValueTuple<T1> : IEquatable<ValueTuple<T1>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<ValueTuple<T1>>, IValueTupleInternal, ITuple
{
	/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`1" /> instance's first element.</summary>
	public T1 Item1;

	/// <summary>Gets the number of elements in the <see langword="ValueTuple" />.</summary>
	/// <returns>1, the number of elements in a <see cref="T:System.ValueTuple`1" /> object.</returns>
	int ITuple.Length => 1;

	/// <summary>Gets the value of the <see langword="ValueTuple" /> element.</summary>
	/// <param name="index">The index of the <see langword="ValueTuple" /> element. <paramref name="index" /> must be 0.</param>
	/// <returns>The value of the <see langword="ValueTuple" /> element.</returns>
	/// <exception cref="T:System.IndexOutOfRangeException">
	///   <paramref name="index" /> is less than 0 or greater than 0.</exception>
	object ITuple.this[int index]
	{
		get
		{
			if (index != 0)
			{
				throw new IndexOutOfRangeException();
			}
			return Item1;
		}
	}

	/// <summary>Initializes a new <see cref="T:System.ValueTuple`1" /> instance.</summary>
	/// <param name="item1">The value tuple's first element.</param>
	public ValueTuple(T1 item1)
	{
		Item1 = item1;
	}

	/// <summary>Returns a value that indicates whether the current <see cref="T:System.ValueTuple`1" /> instance is equal to a specified object.</summary>
	/// <param name="obj">The object to compare with this instance.</param>
	/// <returns>
	///   <see langword="true" /> if the current instance is equal to the specified object; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		if (obj is ValueTuple<T1>)
		{
			return Equals((ValueTuple<T1>)obj);
		}
		return false;
	}

	/// <summary>Returns a value that indicates whether the current <see cref="T:System.ValueTuple`1" /> instance is equal to a specified <see cref="T:System.ValueTuple`1" /> instance.</summary>
	/// <param name="other">The value tuple to compare with this instance.</param>
	/// <returns>
	///   <see langword="true" /> if the current instance is equal to the specified tuple; otherwise, <see langword="false" />.</returns>
	public bool Equals(ValueTuple<T1> other)
	{
		return EqualityComparer<T1>.Default.Equals(Item1, other.Item1);
	}

	/// <summary>Returns a value that indicates whether the current <see cref="T:System.ValueTuple`1" /> instance is equal to a specified object based on a specified comparison method.</summary>
	/// <param name="other">The object to compare with this instance.</param>
	/// <param name="comparer">An object that defines the method to use to evaluate whether the two objects are equal.</param>
	/// <returns>
	///   <see langword="true" /> if the current instance is equal to the specified object; otherwise, <see langword="false" />.</returns>
	bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
	{
		if (other == null || !(other is ValueTuple<T1> valueTuple))
		{
			return false;
		}
		return comparer.Equals(Item1, valueTuple.Item1);
	}

	/// <summary>Compares the current <see cref="T:System.ValueTuple`1" /> instance to a specified object by using a specified comparer and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
	/// <param name="other">The object to compare with the current instance.</param>
	/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="obj" /> in the sort order, as shown in the following table.  
	///   Value  
	///
	///   Description  
	///
	///   A negative integer  
	///
	///   This instance precedes <paramref name="other" />.  
	///
	///   Zero  
	///
	///   This instance and <paramref name="other" /> have the same position in the sort order.  
	///
	///   A positive integer  
	///
	///   This instance follows <paramref name="other" />.</returns>
	int IComparable.CompareTo(object other)
	{
		if (other == null)
		{
			return 1;
		}
		if (!(other is ValueTuple<T1> valueTuple))
		{
			throw new ArgumentException(SR.Format("Argument must be of type {0}.", GetType().ToString()), "other");
		}
		return Comparer<T1>.Default.Compare(Item1, valueTuple.Item1);
	}

	/// <summary>Compares the current <see cref="T:System.ValueTuple`1" /> instance to a specified <see cref="T:System.ValueTuple`1" /> instance.</summary>
	/// <param name="other">The tuple to compare with this instance.</param>
	/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="other" /> in the sort order, as shown in the following table.  
	///   Value  
	///
	///   Description  
	///
	///   A negative integer  
	///
	///   This instance precedes <paramref name="other" />.  
	///
	///   Zero  
	///
	///   This instance and <paramref name="other" /> have the same position in the sort order.  
	///
	///   A positive integer  
	///
	///   This instance follows <paramref name="other" />.</returns>
	public int CompareTo(ValueTuple<T1> other)
	{
		return Comparer<T1>.Default.Compare(Item1, other.Item1);
	}

	/// <summary>Compares the current <see cref="T:System.ValueTuple`1" /> instance to a specified object by using a specified comparer and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
	/// <param name="other">The object to compare with the current instance.</param>
	/// <param name="comparer">An object that provides custom rules for comparison.</param>
	/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="other" /> in the sort order, as shown in the following table.  
	///   Vaue  
	///
	///   Description  
	///
	///   A negative integer  
	///
	///   This instance precedes <paramref name="other" />.  
	///
	///   Zero  
	///
	///   This instance and <paramref name="other" /> have the same position in the sort order.  
	///
	///   A positive integer  
	///
	///   This instance follows <paramref name="other" />.</returns>
	int IStructuralComparable.CompareTo(object other, IComparer comparer)
	{
		if (other == null)
		{
			return 1;
		}
		if (!(other is ValueTuple<T1> valueTuple))
		{
			throw new ArgumentException(SR.Format("Argument must be of type {0}.", GetType().ToString()), "other");
		}
		return comparer.Compare(Item1, valueTuple.Item1);
	}

	/// <summary>Calculates the hash code for the current <see cref="T:System.ValueTuple`1" /> instance.</summary>
	/// <returns>The hash code for the current <see cref="T:System.ValueTuple`1" /> instance.</returns>
	public override int GetHashCode()
	{
		return Item1?.GetHashCode() ?? 0;
	}

	/// <summary>Calculates the hash code for the current <see cref="T:System.ValueTuple`1" /> instance by using a specified computation method.</summary>
	/// <param name="comparer">An object whose <see cref="M:System.Collections.IEqualityComparer.GetHashCode(System.Object)" /> method calculates the hash code of the current <see cref="T:System.ValueTuple`1" /> instance.</param>
	/// <returns>A 32-bit signed integer hash code.</returns>
	int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
	{
		return comparer.GetHashCode(Item1);
	}

	int IValueTupleInternal.GetHashCode(IEqualityComparer comparer)
	{
		return comparer.GetHashCode(Item1);
	}

	/// <summary>Returns a string that represents the value of this <see cref="T:System.ValueTuple`1" /> instance.</summary>
	/// <returns>The string representation of this <see cref="T:System.ValueTuple`1" /> instance.</returns>
	public override string ToString()
	{
		return "(" + Item1?.ToString() + ")";
	}

	string IValueTupleInternal.ToStringEnd()
	{
		return Item1?.ToString() + ")";
	}
}
/// <summary>Represents a value tuple with 2 components.</summary>
/// <typeparam name="T1">The type of the value tuple's first element.</typeparam>
/// <typeparam name="T2">The type of the value tuple's second element.</typeparam>
[Serializable]
[StructLayout(LayoutKind.Auto)]
public struct ValueTuple<T1, T2> : IEquatable<(T1, T2)>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<(T1, T2)>, IValueTupleInternal, ITuple
{
	/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`2" /> instance's first element.</summary>
	public T1 Item1;

	/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`2" /> instance's second element.</summary>
	public T2 Item2;

	/// <summary>Gets the number of elements in the <see langword="ValueTuple" />.</summary>
	/// <returns>2, the number of elements in a <see cref="T:System.ValueTuple`2" /> object.</returns>
	int ITuple.Length => 2;

	/// <summary>Gets the value of the specified <see langword="ValueTuple" /> element.</summary>
	/// <param name="index">The index of the specified <see langword="ValueTuple" /> element. <paramref name="index" /> can range from 0 to 1.</param>
	/// <returns>The value of the <see langword="ValueTuple" /> element at the specified position.</returns>
	/// <exception cref="T:System.IndexOutOfRangeException">
	///   <paramref name="index" /> is less than 0 or greater than 1.</exception>
	object ITuple.this[int index] => index switch
	{
		0 => Item1, 
		1 => Item2, 
		_ => throw new IndexOutOfRangeException(), 
	};

	/// <summary>Initializes a new <see cref="T:System.ValueTuple`2" /> instance.</summary>
	/// <param name="item1">The value tuple's first element.</param>
	/// <param name="item2">The value tuple's second element.</param>
	public ValueTuple(T1 item1, T2 item2)
	{
		Item1 = item1;
		Item2 = item2;
	}

	/// <summary>Returns a value that indicates whether the current <see cref="T:System.ValueTuple`2" /> instance is equal to a specified object.</summary>
	/// <param name="obj">The object to compare with this instance.</param>
	/// <returns>
	///   <see langword="true" /> if the current instance is equal to the specified object; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		if (obj is ValueTuple<T1, T2>)
		{
			return Equals(((T1, T2))obj);
		}
		return false;
	}

	public bool Equals((T1, T2) other)
	{
		if (EqualityComparer<T1>.Default.Equals(Item1, other.Item1))
		{
			return EqualityComparer<T2>.Default.Equals(Item2, other.Item2);
		}
		return false;
	}

	/// <summary>Returns a value that indicates whether the current <see cref="T:System.ValueTuple`2" /> instance is equal to a specified object based on a specified comparison method.</summary>
	/// <param name="other">The object to compare with this instance.</param>
	/// <param name="comparer">An object that defines the method to use to evaluate whether the two objects are equal.</param>
	/// <returns>
	///   <see langword="true" /> if the current instance is equal to the specified objects; otherwise, <see langword="false" />.</returns>
	bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
	{
		if (other == null || !(other is (T1, T2) tuple))
		{
			return false;
		}
		if (comparer.Equals(Item1, tuple.Item1))
		{
			return comparer.Equals(Item2, tuple.Item2);
		}
		return false;
	}

	/// <summary>Compares the current <see cref="T:System.ValueTuple`2" /> instance to a specified object by using a specified comparer and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
	/// <param name="other">The object to compare with the current instance.</param>
	/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="obj" /> in the sort order, as shown in the following table.  
	///   Value  
	///
	///   Description  
	///
	///   A negative integer  
	///
	///   This instance precedes <paramref name="other" />.  
	///
	///   Zero  
	///
	///   This instance and <paramref name="other" /> have the same position in the sort order.  
	///
	///   A positive integer  
	///
	///   This instance follows <paramref name="other" />.</returns>
	int IComparable.CompareTo(object other)
	{
		if (other == null)
		{
			return 1;
		}
		if (!(other is ValueTuple<T1, T2>))
		{
			throw new ArgumentException(SR.Format("Argument must be of type {0}.", GetType().ToString()), "other");
		}
		return CompareTo(((T1, T2))other);
	}

	public int CompareTo((T1, T2) other)
	{
		int num = Comparer<T1>.Default.Compare(Item1, other.Item1);
		if (num != 0)
		{
			return num;
		}
		return Comparer<T2>.Default.Compare(Item2, other.Item2);
	}

	/// <summary>Compares the current <see cref="T:System.ValueTuple`2" /> instance to a specified object by using a specified comparer and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
	/// <param name="other">The object to compare with the current instance.</param>
	/// <param name="comparer">An object that provides custom rules for comparison.</param>
	/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="other" /> in the sort order, as shown in the following table.  
	///   Value  
	///
	///   Description  
	///
	///   A negative integer  
	///
	///   This instance precedes <paramref name="other" />.  
	///
	///   Zero  
	///
	///   This instance and <paramref name="other" /> have the same position in the sort order.  
	///
	///   A positive integer  
	///
	///   This instance follows <paramref name="other" />.</returns>
	int IStructuralComparable.CompareTo(object other, IComparer comparer)
	{
		if (other == null)
		{
			return 1;
		}
		if (!(other is (T1, T2) tuple))
		{
			throw new ArgumentException(SR.Format("Argument must be of type {0}.", GetType().ToString()), "other");
		}
		int num = comparer.Compare(Item1, tuple.Item1);
		if (num != 0)
		{
			return num;
		}
		return comparer.Compare(Item2, tuple.Item2);
	}

	/// <summary>Calculates the hash code for the current <see cref="T:System.ValueTuple`2" /> instance.</summary>
	/// <returns>The hash code for the current <see cref="T:System.ValueTuple`2" /> instance.</returns>
	public override int GetHashCode()
	{
		return ValueTuple.CombineHashCodes(Item1?.GetHashCode() ?? 0, Item2?.GetHashCode() ?? 0);
	}

	/// <summary>Calculates the hash code for the current <see cref="T:System.ValueTuple`2" /> instance by using a specified computation method.</summary>
	/// <param name="comparer">An object whose <see cref="M:System.Collections.IEqualityComparer.GetHashCode(System.Object)" /> method calculates the hash code of the current <see cref="T:System.ValueTuple`2" /> instance.</param>
	/// <returns>A 32-bit signed integer hash code.</returns>
	int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
	{
		return GetHashCodeCore(comparer);
	}

	private int GetHashCodeCore(IEqualityComparer comparer)
	{
		return ValueTuple.CombineHashCodes(comparer.GetHashCode(Item1), comparer.GetHashCode(Item2));
	}

	int IValueTupleInternal.GetHashCode(IEqualityComparer comparer)
	{
		return GetHashCodeCore(comparer);
	}

	/// <summary>Returns a string that represents the value of this <see cref="T:System.ValueTuple`2" /> instance.</summary>
	/// <returns>The string representation of this <see cref="T:System.ValueTuple`2" /> instance.</returns>
	public override string ToString()
	{
		return "(" + Item1?.ToString() + ", " + Item2?.ToString() + ")";
	}

	string IValueTupleInternal.ToStringEnd()
	{
		return Item1?.ToString() + ", " + Item2?.ToString() + ")";
	}
}
/// <summary>Represents a value tuple with 3 components.</summary>
/// <typeparam name="T1">The type of the value tuple's first element.</typeparam>
/// <typeparam name="T2">The type of the value tuple's second element.</typeparam>
/// <typeparam name="T3">The type of the value tuple's third element.</typeparam>
[Serializable]
[StructLayout(LayoutKind.Auto)]
public struct ValueTuple<T1, T2, T3> : IEquatable<(T1, T2, T3)>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<(T1, T2, T3)>, IValueTupleInternal, ITuple
{
	/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`3" /> instance's first element.</summary>
	public T1 Item1;

	/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`3" /> instance's second element.</summary>
	public T2 Item2;

	/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`3" /> instance's third element.</summary>
	public T3 Item3;

	/// <summary>Gets the number of elements in the <see langword="ValueTuple" />.</summary>
	/// <returns>3, the number of elements in a <see cref="T:System.ValueTuple`3" /> object.</returns>
	int ITuple.Length => 3;

	/// <summary>Gets the value of the specified <see langword="ValueTuple" /> element.</summary>
	/// <param name="index">The index of the specified <see langword="ValueTuple" /> element. <paramref name="index" /> can range from 0 to 2.</param>
	/// <returns>The value of the <see langword="ValueTuple" /> element at the specified position.</returns>
	/// <exception cref="T:System.IndexOutOfRangeException">
	///   <paramref name="index" /> is less than 0 or greater than 2.</exception>
	object ITuple.this[int index] => index switch
	{
		0 => Item1, 
		1 => Item2, 
		2 => Item3, 
		_ => throw new IndexOutOfRangeException(), 
	};

	/// <summary>Initializes a new <see cref="T:System.ValueTuple`3" /> instance.</summary>
	/// <param name="item1">The value tuple's first element.</param>
	/// <param name="item2">The value tuple's second element.</param>
	/// <param name="item3">The value tuple's third element.</param>
	public ValueTuple(T1 item1, T2 item2, T3 item3)
	{
		Item1 = item1;
		Item2 = item2;
		Item3 = item3;
	}

	/// <summary>Returns a value that indicates whether the current <see cref="T:System.ValueTuple`3" /> instance is equal to a specified object.</summary>
	/// <param name="obj">The object to compare with this instance.</param>
	/// <returns>
	///   <see langword="true" /> if the current instance is equal to the specified object; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		if (obj is ValueTuple<T1, T2, T3>)
		{
			return Equals(((T1, T2, T3))obj);
		}
		return false;
	}

	public bool Equals((T1, T2, T3) other)
	{
		if (EqualityComparer<T1>.Default.Equals(Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(Item2, other.Item2))
		{
			return EqualityComparer<T3>.Default.Equals(Item3, other.Item3);
		}
		return false;
	}

	/// <summary>Returns a value that indicates whether the current <see cref="T:System.ValueTuple`3" /> instance is equal to a specified object based on a specified comparison method.</summary>
	/// <param name="other">The object to compare with this instance.</param>
	/// <param name="comparer">An object that defines the method to use to evaluate whether the two objects are equal.</param>
	/// <returns>
	///   <see langword="true" /> if the current instance is equal to the specified objects; otherwise, <see langword="false" />.</returns>
	bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
	{
		if (other == null || !(other is (T1, T2, T3) tuple))
		{
			return false;
		}
		if (comparer.Equals(Item1, tuple.Item1) && comparer.Equals(Item2, tuple.Item2))
		{
			return comparer.Equals(Item3, tuple.Item3);
		}
		return false;
	}

	/// <summary>Compares the current <see cref="T:System.ValueTuple`3" /> instance to a specified object by using a specified comparer and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
	/// <param name="other">The object to compare with the current instance.</param>
	/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="obj" /> in the sort order, as shown in the following table.  
	///   Value  
	///
	///   Description  
	///
	///   A negative integer  
	///
	///   This instance precedes <paramref name="other" />.  
	///
	///   Zero  
	///
	///   This instance and <paramref name="other" /> have the same position in the sort order.  
	///
	///   A positive integer  
	///
	///   This instance follows <paramref name="other" />.</returns>
	int IComparable.CompareTo(object other)
	{
		if (other == null)
		{
			return 1;
		}
		if (!(other is ValueTuple<T1, T2, T3>))
		{
			throw new ArgumentException(SR.Format("Argument must be of type {0}.", GetType().ToString()), "other");
		}
		return CompareTo(((T1, T2, T3))other);
	}

	public int CompareTo((T1, T2, T3) other)
	{
		int num = Comparer<T1>.Default.Compare(Item1, other.Item1);
		if (num != 0)
		{
			return num;
		}
		num = Comparer<T2>.Default.Compare(Item2, other.Item2);
		if (num != 0)
		{
			return num;
		}
		return Comparer<T3>.Default.Compare(Item3, other.Item3);
	}

	/// <summary>Compares the current <see cref="T:System.ValueTuple`3" /> instance to a specified object by using a specified comparer and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
	/// <param name="other">The object to compare with the current instance.</param>
	/// <param name="comparer">An object that provides custom rules for comparison.</param>
	/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="other" /> in the sort order, as shown in the following table.  
	///   Value  
	///
	///   Description  
	///
	///   A negative integer  
	///
	///   This instance precedes <paramref name="other" />.  
	///
	///   Zero  
	///
	///   This instance and <paramref name="other" /> have the same position in the sort order.  
	///
	///   A positive integer  
	///
	///   This instance follows <paramref name="other" />.</returns>
	int IStructuralComparable.CompareTo(object other, IComparer comparer)
	{
		if (other == null)
		{
			return 1;
		}
		if (!(other is (T1, T2, T3) tuple))
		{
			throw new ArgumentException(SR.Format("Argument must be of type {0}.", GetType().ToString()), "other");
		}
		int num = comparer.Compare(Item1, tuple.Item1);
		if (num != 0)
		{
			return num;
		}
		num = comparer.Compare(Item2, tuple.Item2);
		if (num != 0)
		{
			return num;
		}
		return comparer.Compare(Item3, tuple.Item3);
	}

	/// <summary>Calculates the hash code for the current <see cref="T:System.ValueTuple`3" /> instance.</summary>
	/// <returns>The hash code for the current <see cref="T:System.ValueTuple`3" /> instance.</returns>
	public override int GetHashCode()
	{
		return ValueTuple.CombineHashCodes(Item1?.GetHashCode() ?? 0, Item2?.GetHashCode() ?? 0, Item3?.GetHashCode() ?? 0);
	}

	/// <summary>Calculates the hash code for the current <see cref="T:System.ValueTuple`3" /> instance by using a specified computation method.</summary>
	/// <param name="comparer">An object whose <see cref="M:System.Collections.IEqualityComparer.GetHashCode(System.Object)" /> method calculates the hash code of the current <see cref="T:System.ValueTuple`3" /> instance.</param>
	/// <returns>A 32-bit signed integer hash code.</returns>
	int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
	{
		return GetHashCodeCore(comparer);
	}

	private int GetHashCodeCore(IEqualityComparer comparer)
	{
		return ValueTuple.CombineHashCodes(comparer.GetHashCode(Item1), comparer.GetHashCode(Item2), comparer.GetHashCode(Item3));
	}

	int IValueTupleInternal.GetHashCode(IEqualityComparer comparer)
	{
		return GetHashCodeCore(comparer);
	}

	/// <summary>Returns a string that represents the value of this <see cref="T:System.ValueTuple`3" /> instance.</summary>
	/// <returns>The string representation of this <see cref="T:System.ValueTuple`2" /> instance.</returns>
	public override string ToString()
	{
		return "(" + Item1?.ToString() + ", " + Item2?.ToString() + ", " + Item3?.ToString() + ")";
	}

	string IValueTupleInternal.ToStringEnd()
	{
		return Item1?.ToString() + ", " + Item2?.ToString() + ", " + Item3?.ToString() + ")";
	}
}
/// <summary>Represents a value tuple with 4 components.</summary>
/// <typeparam name="T1">The type of the value tuple's first element.</typeparam>
/// <typeparam name="T2">The type of the value tuple's second element.</typeparam>
/// <typeparam name="T3">The type of the value tuple's third element.</typeparam>
/// <typeparam name="T4">The type of the value tuple's fourth element.</typeparam>
[Serializable]
[StructLayout(LayoutKind.Auto)]
public struct ValueTuple<T1, T2, T3, T4> : IEquatable<(T1, T2, T3, T4)>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<(T1, T2, T3, T4)>, IValueTupleInternal, ITuple
{
	/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`4" /> instance's first element.</summary>
	public T1 Item1;

	/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`4" /> instance's second element.</summary>
	public T2 Item2;

	/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`4" /> instance's third element.</summary>
	public T3 Item3;

	/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`4" /> instance's fourth element.</summary>
	public T4 Item4;

	/// <summary>Gets the number of elements in the <see langword="ValueTuple" />.</summary>
	/// <returns>4, the number of elements in a <see cref="T:System.ValueTuple`4" /> object.</returns>
	int ITuple.Length => 4;

	/// <summary>Gets the value of the specified <see langword="ValueTuple" /> element.</summary>
	/// <param name="index">The index of the specified <see langword="ValueTuple" /> element. <paramref name="index" /> can range from 0 to 3.</param>
	/// <returns>The value of the <see langword="ValueTuple" /> element at the specified position.</returns>
	/// <exception cref="T:System.IndexOutOfRangeException">
	///   <paramref name="index" /> is less than 0 or greater than 3.</exception>
	object ITuple.this[int index] => index switch
	{
		0 => Item1, 
		1 => Item2, 
		2 => Item3, 
		3 => Item4, 
		_ => throw new IndexOutOfRangeException(), 
	};

	/// <summary>Initializes a new <see cref="T:System.ValueTuple`4" /> instance.</summary>
	/// <param name="item1">The value tuple's first element.</param>
	/// <param name="item2">The value tuple's second element.</param>
	/// <param name="item3">The value tuple's third element.</param>
	/// <param name="item4">The value tuple's fourth element.</param>
	public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4)
	{
		Item1 = item1;
		Item2 = item2;
		Item3 = item3;
		Item4 = item4;
	}

	/// <summary>Returns a value that indicates whether the current <see cref="T:System.ValueTuple`4" /> instance is equal to a specified object.</summary>
	/// <param name="obj">The object to compare with this instance.</param>
	/// <returns>
	///   <see langword="true" /> if the current instance is equal to the specified object; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		if (obj is ValueTuple<T1, T2, T3, T4>)
		{
			return Equals(((T1, T2, T3, T4))obj);
		}
		return false;
	}

	public bool Equals((T1, T2, T3, T4) other)
	{
		if (EqualityComparer<T1>.Default.Equals(Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(Item2, other.Item2) && EqualityComparer<T3>.Default.Equals(Item3, other.Item3))
		{
			return EqualityComparer<T4>.Default.Equals(Item4, other.Item4);
		}
		return false;
	}

	/// <summary>Returns a value that indicates whether the current <see cref="T:System.ValueTuple`4" /> instance is equal to a specified object based on a specified comparison method.</summary>
	/// <param name="other">The object to compare with this instance.</param>
	/// <param name="comparer">An object that defines the method to use to evaluate whether the two objects are equal.</param>
	/// <returns>
	///   <see langword="true" /> if the current instance is equal to the specified objects; otherwise, <see langword="false" />.</returns>
	bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
	{
		if (other == null || !(other is (T1, T2, T3, T4) tuple))
		{
			return false;
		}
		if (comparer.Equals(Item1, tuple.Item1) && comparer.Equals(Item2, tuple.Item2) && comparer.Equals(Item3, tuple.Item3))
		{
			return comparer.Equals(Item4, tuple.Item4);
		}
		return false;
	}

	/// <summary>Compares the current <see cref="T:System.ValueTuple`4" /> instance to a specified object by using a specified comparer and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
	/// <param name="other">The object to compare with the current instance.</param>
	/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="obj" /> in the sort order, as shown in the following table.  
	///   Value  
	///
	///   Description  
	///
	///   A negative integer  
	///
	///   This instance precedes <paramref name="other" />.  
	///
	///   Zero  
	///
	///   This instance and <paramref name="other" /> have the same position in the sort order.  
	///
	///   A positive integer  
	///
	///   This instance follows <paramref name="other" />.</returns>
	int IComparable.CompareTo(object other)
	{
		if (other == null)
		{
			return 1;
		}
		if (!(other is ValueTuple<T1, T2, T3, T4>))
		{
			throw new ArgumentException(SR.Format("Argument must be of type {0}.", GetType().ToString()), "other");
		}
		return CompareTo(((T1, T2, T3, T4))other);
	}

	public int CompareTo((T1, T2, T3, T4) other)
	{
		int num = Comparer<T1>.Default.Compare(Item1, other.Item1);
		if (num != 0)
		{
			return num;
		}
		num = Comparer<T2>.Default.Compare(Item2, other.Item2);
		if (num != 0)
		{
			return num;
		}
		num = Comparer<T3>.Default.Compare(Item3, other.Item3);
		if (num != 0)
		{
			return num;
		}
		return Comparer<T4>.Default.Compare(Item4, other.Item4);
	}

	/// <summary>Compares the current <see cref="T:System.ValueTuple`4" /> instance to a specified object by using a specified comparer and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
	/// <param name="other">The object to compare with the current instance.</param>
	/// <param name="comparer">An object that provides custom rules for comparison.</param>
	/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="other" /> in the sort order, as shown in the following table.  
	///   Vaue  
	///
	///   Description  
	///
	///   A negative integer  
	///
	///   This instance precedes <paramref name="other" />.  
	///
	///   Zero  
	///
	///   This instance and <paramref name="other" /> have the same position in the sort order.  
	///
	///   A positive integer  
	///
	///   This instance follows <paramref name="other" />.</returns>
	int IStructuralComparable.CompareTo(object other, IComparer comparer)
	{
		if (other == null)
		{
			return 1;
		}
		if (!(other is (T1, T2, T3, T4) tuple))
		{
			throw new ArgumentException(SR.Format("Argument must be of type {0}.", GetType().ToString()), "other");
		}
		int num = comparer.Compare(Item1, tuple.Item1);
		if (num != 0)
		{
			return num;
		}
		num = comparer.Compare(Item2, tuple.Item2);
		if (num != 0)
		{
			return num;
		}
		num = comparer.Compare(Item3, tuple.Item3);
		if (num != 0)
		{
			return num;
		}
		return comparer.Compare(Item4, tuple.Item4);
	}

	/// <summary>Calculates the hash code for the current <see cref="T:System.ValueTuple`4" /> instance.</summary>
	/// <returns>The hash code for the current <see cref="T:System.ValueTuple`4" /> instance.</returns>
	public override int GetHashCode()
	{
		return ValueTuple.CombineHashCodes(Item1?.GetHashCode() ?? 0, Item2?.GetHashCode() ?? 0, Item3?.GetHashCode() ?? 0, Item4?.GetHashCode() ?? 0);
	}

	/// <summary>Calculates the hash code for the current <see cref="T:System.ValueTuple`4" /> instance by using a specified computation method.</summary>
	/// <param name="comparer">An object whose <see cref="M:System.Collections.IEqualityComparer.GetHashCode(System.Object)" /> method calculates the hash code of the current <see cref="T:System.ValueTuple`4" /> instance.</param>
	/// <returns>A 32-bit signed integer hash code.</returns>
	int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
	{
		return GetHashCodeCore(comparer);
	}

	private int GetHashCodeCore(IEqualityComparer comparer)
	{
		return ValueTuple.CombineHashCodes(comparer.GetHashCode(Item1), comparer.GetHashCode(Item2), comparer.GetHashCode(Item3), comparer.GetHashCode(Item4));
	}

	int IValueTupleInternal.GetHashCode(IEqualityComparer comparer)
	{
		return GetHashCodeCore(comparer);
	}

	/// <summary>Returns a string that represents the value of this <see cref="T:System.ValueTuple`4" /> instance.</summary>
	/// <returns>The string representation of this <see cref="T:System.ValueTuple`4" /> instance.</returns>
	public override string ToString()
	{
		return "(" + Item1?.ToString() + ", " + Item2?.ToString() + ", " + Item3?.ToString() + ", " + Item4?.ToString() + ")";
	}

	string IValueTupleInternal.ToStringEnd()
	{
		return Item1?.ToString() + ", " + Item2?.ToString() + ", " + Item3?.ToString() + ", " + Item4?.ToString() + ")";
	}
}
/// <summary>Represents a value tuple with 5 components.</summary>
/// <typeparam name="T1">The type of the value tuple's first element.</typeparam>
/// <typeparam name="T2">The type of the value tuple's second element.</typeparam>
/// <typeparam name="T3">The type of the value tuple's third element.</typeparam>
/// <typeparam name="T4">The type of the value tuple's fourth element.</typeparam>
/// <typeparam name="T5">The type of the value tuple's fifth element.</typeparam>
[Serializable]
[StructLayout(LayoutKind.Auto)]
public struct ValueTuple<T1, T2, T3, T4, T5> : IEquatable<(T1, T2, T3, T4, T5)>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<(T1, T2, T3, T4, T5)>, IValueTupleInternal, ITuple
{
	/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`5" /> instance's first element.</summary>
	public T1 Item1;

	/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`5" /> instance's second element.</summary>
	public T2 Item2;

	/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`5" /> instance's third element.</summary>
	public T3 Item3;

	/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`5" /> instance's fourth element.</summary>
	public T4 Item4;

	/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`5" /> instance's fifth element.</summary>
	public T5 Item5;

	/// <summary>Gets the number of elements in the <see langword="ValueTuple" />.</summary>
	/// <returns>5, the number of elements in a <see cref="T:System.ValueTuple`5" /> object.</returns>
	int ITuple.Length => 5;

	/// <summary>Gets the value of the specified <see langword="ValueTuple" /> element.</summary>
	/// <param name="index">The index of the specified <see langword="ValueTuple" /> element. <paramref name="index" /> can range from 0 to 4.</param>
	/// <returns>The value of the <see langword="ValueTuple" /> element at the specified position.</returns>
	/// <exception cref="T:System.IndexOutOfRangeException">
	///   <paramref name="index" /> is less than 0 or greater than 4.</exception>
	object ITuple.this[int index] => index switch
	{
		0 => Item1, 
		1 => Item2, 
		2 => Item3, 
		3 => Item4, 
		4 => Item5, 
		_ => throw new IndexOutOfRangeException(), 
	};

	/// <summary>Initializes a new <see cref="T:System.ValueTuple`5" /> instance.</summary>
	/// <param name="item1">The value tuple's first element.</param>
	/// <param name="item2">The value tuple's second element.</param>
	/// <param name="item3">The value tuple's third element.</param>
	/// <param name="item4">The value tuple's fourth element.</param>
	/// <param name="item5">The value tuple's fifth element.</param>
	public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
	{
		Item1 = item1;
		Item2 = item2;
		Item3 = item3;
		Item4 = item4;
		Item5 = item5;
	}

	/// <summary>Returns a value that indicates whether the current <see cref="T:System.ValueTuple`5" /> instance is equal to a specified object.</summary>
	/// <param name="obj">The object to compare with this instance.</param>
	/// <returns>
	///   <see langword="true" /> if the current instance is equal to the specified object; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		if (obj is ValueTuple<T1, T2, T3, T4, T5>)
		{
			return Equals(((T1, T2, T3, T4, T5))obj);
		}
		return false;
	}

	public bool Equals((T1, T2, T3, T4, T5) other)
	{
		if (EqualityComparer<T1>.Default.Equals(Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(Item2, other.Item2) && EqualityComparer<T3>.Default.Equals(Item3, other.Item3) && EqualityComparer<T4>.Default.Equals(Item4, other.Item4))
		{
			return EqualityComparer<T5>.Default.Equals(Item5, other.Item5);
		}
		return false;
	}

	/// <summary>Returns a value that indicates whether the current <see cref="T:System.ValueTuple`5" /> instance is equal to a specified object based on a specified comparison method.</summary>
	/// <param name="other">The object to compare with this instance.</param>
	/// <param name="comparer">An object that defines the method to use to evaluate whether the two objects are equal.</param>
	/// <returns>
	///   <see langword="true" /> if the current instance is equal to the specified objects; otherwise, <see langword="false" />.</returns>
	bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
	{
		if (other == null || !(other is (T1, T2, T3, T4, T5) tuple))
		{
			return false;
		}
		if (comparer.Equals(Item1, tuple.Item1) && comparer.Equals(Item2, tuple.Item2) && comparer.Equals(Item3, tuple.Item3) && comparer.Equals(Item4, tuple.Item4))
		{
			return comparer.Equals(Item5, tuple.Item5);
		}
		return false;
	}

	/// <summary>Compares the current <see cref="T:System.ValueTuple`5" /> instance to a specified object by using a specified comparer and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
	/// <param name="other">The object to compare with the current instance.</param>
	/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="obj" /> in the sort order, as shown in the following table.  
	///   Value  
	///
	///   Description  
	///
	///   A negative integer  
	///
	///   This instance precedes <paramref name="other" />.  
	///
	///   Zero  
	///
	///   This instance and <paramref name="other" /> have the same position in the sort order.  
	///
	///   A positive integer  
	///
	///   This instance follows <paramref name="other" />.</returns>
	int IComparable.CompareTo(object other)
	{
		if (other == null)
		{
			return 1;
		}
		if (!(other is ValueTuple<T1, T2, T3, T4, T5>))
		{
			throw new ArgumentException(SR.Format("Argument must be of type {0}.", GetType().ToString()), "other");
		}
		return CompareTo(((T1, T2, T3, T4, T5))other);
	}

	public int CompareTo((T1, T2, T3, T4, T5) other)
	{
		int num = Comparer<T1>.Default.Compare(Item1, other.Item1);
		if (num != 0)
		{
			return num;
		}
		num = Comparer<T2>.Default.Compare(Item2, other.Item2);
		if (num != 0)
		{
			return num;
		}
		num = Comparer<T3>.Default.Compare(Item3, other.Item3);
		if (num != 0)
		{
			return num;
		}
		num = Comparer<T4>.Default.Compare(Item4, other.Item4);
		if (num != 0)
		{
			return num;
		}
		return Comparer<T5>.Default.Compare(Item5, other.Item5);
	}

	/// <summary>Compares the current <see cref="T:System.ValueTuple`5" /> instance to a specified object by using a specified comparer and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
	/// <param name="other">The object to compare with the current instance.</param>
	/// <param name="comparer">An object that provides custom rules for comparison.</param>
	/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="other" /> in the sort order, as shown in the following table.  
	///   Value  
	///
	///   Description  
	///
	///   A negative integer  
	///
	///   This instance precedes <paramref name="other" />.  
	///
	///   Zero  
	///
	///   This instance and <paramref name="other" /> have the same position in the sort order.  
	///
	///   A positive integer  
	///
	///   This instance follows <paramref name="other" />.</returns>
	int IStructuralComparable.CompareTo(object other, IComparer comparer)
	{
		if (other == null)
		{
			return 1;
		}
		if (!(other is (T1, T2, T3, T4, T5) tuple))
		{
			throw new ArgumentException(SR.Format("Argument must be of type {0}.", GetType().ToString()), "other");
		}
		int num = comparer.Compare(Item1, tuple.Item1);
		if (num != 0)
		{
			return num;
		}
		num = comparer.Compare(Item2, tuple.Item2);
		if (num != 0)
		{
			return num;
		}
		num = comparer.Compare(Item3, tuple.Item3);
		if (num != 0)
		{
			return num;
		}
		num = comparer.Compare(Item4, tuple.Item4);
		if (num != 0)
		{
			return num;
		}
		return comparer.Compare(Item5, tuple.Item5);
	}

	/// <summary>Calculates the hash code for the current <see cref="T:System.ValueTuple`5" /> instance.</summary>
	/// <returns>The hash code for the current <see cref="T:System.ValueTuple`5" /> instance.</returns>
	public override int GetHashCode()
	{
		return ValueTuple.CombineHashCodes(Item1?.GetHashCode() ?? 0, Item2?.GetHashCode() ?? 0, Item3?.GetHashCode() ?? 0, Item4?.GetHashCode() ?? 0, Item5?.GetHashCode() ?? 0);
	}

	/// <summary>Calculates the hash code for the current <see cref="T:System.ValueTuple`5" /> instance by using a specified computation method.</summary>
	/// <param name="comparer">An object whose <see cref="M:System.Collections.IEqualityComparer.GetHashCode(System.Object)" /> method calculates the hash code of the current <see cref="T:System.ValueTuple`5" /> instance.</param>
	/// <returns>A 32-bit signed integer hash code.</returns>
	int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
	{
		return GetHashCodeCore(comparer);
	}

	private int GetHashCodeCore(IEqualityComparer comparer)
	{
		return ValueTuple.CombineHashCodes(comparer.GetHashCode(Item1), comparer.GetHashCode(Item2), comparer.GetHashCode(Item3), comparer.GetHashCode(Item4), comparer.GetHashCode(Item5));
	}

	int IValueTupleInternal.GetHashCode(IEqualityComparer comparer)
	{
		return GetHashCodeCore(comparer);
	}

	/// <summary>Returns a string that represents the value of this <see cref="T:System.ValueTuple`5" /> instance.</summary>
	/// <returns>The string representation of this <see cref="T:System.ValueTuple`5" /> instance.</returns>
	public override string ToString()
	{
		return "(" + Item1?.ToString() + ", " + Item2?.ToString() + ", " + Item3?.ToString() + ", " + Item4?.ToString() + ", " + Item5?.ToString() + ")";
	}

	string IValueTupleInternal.ToStringEnd()
	{
		return Item1?.ToString() + ", " + Item2?.ToString() + ", " + Item3?.ToString() + ", " + Item4?.ToString() + ", " + Item5?.ToString() + ")";
	}
}
/// <summary>Represents a value tuple with 6 components.</summary>
/// <typeparam name="T1">The type of the value tuple's first element.</typeparam>
/// <typeparam name="T2">The type of the value tuple's second element.</typeparam>
/// <typeparam name="T3">The type of the value tuple's third element.</typeparam>
/// <typeparam name="T4">The type of the value tuple's fourth element.</typeparam>
/// <typeparam name="T5">The type of the value tuple's fifth element.</typeparam>
/// <typeparam name="T6">The type of the value tuple's sixth element.</typeparam>
[Serializable]
[StructLayout(LayoutKind.Auto)]
public struct ValueTuple<T1, T2, T3, T4, T5, T6> : IEquatable<(T1, T2, T3, T4, T5, T6)>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<(T1, T2, T3, T4, T5, T6)>, IValueTupleInternal, ITuple
{
	/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`6" /> instance's first element.</summary>
	public T1 Item1;

	/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`6" /> instance's second element.</summary>
	public T2 Item2;

	/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`6" /> instance's third element.</summary>
	public T3 Item3;

	/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`6" /> instance's fourth element.</summary>
	public T4 Item4;

	/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`6" /> instance's fifth element.</summary>
	public T5 Item5;

	/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`6" /> instance's sixth element.</summary>
	public T6 Item6;

	/// <summary>Gets the number of elements in the <see langword="ValueTuple" />.</summary>
	/// <returns>6, the number of elements in a <see cref="T:System.ValueTuple`6" /> object.</returns>
	int ITuple.Length => 6;

	/// <summary>Gets the value of the specified <see langword="ValueTuple" /> element.</summary>
	/// <param name="index">The index of the specified <see langword="ValueTuple" /> element. <paramref name="index" /> can range from 0 to 5.</param>
	/// <returns>The value of the <see langword="ValueTuple" /> element at the specified position.</returns>
	/// <exception cref="T:System.IndexOutOfRangeException">
	///   <paramref name="index" /> is less than 0 or greater than 5.</exception>
	object ITuple.this[int index] => index switch
	{
		0 => Item1, 
		1 => Item2, 
		2 => Item3, 
		3 => Item4, 
		4 => Item5, 
		5 => Item6, 
		_ => throw new IndexOutOfRangeException(), 
	};

	/// <summary>Initializes a new <see cref="T:System.ValueTuple`6" /> instance.</summary>
	/// <param name="item1">The value tuple's first element.</param>
	/// <param name="item2">The value tuple's second element.</param>
	/// <param name="item3">The value tuple's third element.</param>
	/// <param name="item4">The value tuple's fourth element.</param>
	/// <param name="item5">The value tuple's fifth element.</param>
	/// <param name="item6">The value tuple's sixth element.</param>
	public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
	{
		Item1 = item1;
		Item2 = item2;
		Item3 = item3;
		Item4 = item4;
		Item5 = item5;
		Item6 = item6;
	}

	/// <summary>Returns a value that indicates whether the current <see cref="T:System.ValueTuple`6" /> instance is equal to a specified object.</summary>
	/// <param name="obj">The object to compare with this instance.</param>
	/// <returns>
	///   <see langword="true" /> if the current instance is equal to the specified object; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		if (obj is ValueTuple<T1, T2, T3, T4, T5, T6>)
		{
			return Equals(((T1, T2, T3, T4, T5, T6))obj);
		}
		return false;
	}

	public bool Equals((T1, T2, T3, T4, T5, T6) other)
	{
		if (EqualityComparer<T1>.Default.Equals(Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(Item2, other.Item2) && EqualityComparer<T3>.Default.Equals(Item3, other.Item3) && EqualityComparer<T4>.Default.Equals(Item4, other.Item4) && EqualityComparer<T5>.Default.Equals(Item5, other.Item5))
		{
			return EqualityComparer<T6>.Default.Equals(Item6, other.Item6);
		}
		return false;
	}

	/// <summary>Returns a value that indicates whether the current <see cref="T:System.ValueTuple`6" /> instance is equal to a specified object based on a specified comparison method.</summary>
	/// <param name="other">The object to compare with this instance.</param>
	/// <param name="comparer">An object that defines the method to use to evaluate whether the two objects are equal.</param>
	/// <returns>
	///   <see langword="true" /> if the current instance is equal to the specified objects; otherwise, <see langword="false" />.</returns>
	bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
	{
		if (other == null || !(other is (T1, T2, T3, T4, T5, T6) tuple))
		{
			return false;
		}
		if (comparer.Equals(Item1, tuple.Item1) && comparer.Equals(Item2, tuple.Item2) && comparer.Equals(Item3, tuple.Item3) && comparer.Equals(Item4, tuple.Item4) && comparer.Equals(Item5, tuple.Item5))
		{
			return comparer.Equals(Item6, tuple.Item6);
		}
		return false;
	}

	/// <summary>Compares the current <see cref="T:System.ValueTuple`6" /> instance to a specified object by using a specified comparer and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
	/// <param name="other">The object to compare with the current instance.</param>
	/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="obj" /> in the sort order, as shown in the following table.  
	///   Value  
	///
	///   Description  
	///
	///   A negative integer  
	///
	///   This instance precedes <paramref name="other" />.  
	///
	///   Zero  
	///
	///   This instance and <paramref name="other" /> have the same position in the sort order.  
	///
	///   A positive integer  
	///
	///   This instance follows <paramref name="other" />.</returns>
	int IComparable.CompareTo(object other)
	{
		if (other == null)
		{
			return 1;
		}
		if (!(other is ValueTuple<T1, T2, T3, T4, T5, T6>))
		{
			throw new ArgumentException(SR.Format("Argument must be of type {0}.", GetType().ToString()), "other");
		}
		return CompareTo(((T1, T2, T3, T4, T5, T6))other);
	}

	public int CompareTo((T1, T2, T3, T4, T5, T6) other)
	{
		int num = Comparer<T1>.Default.Compare(Item1, other.Item1);
		if (num != 0)
		{
			return num;
		}
		num = Comparer<T2>.Default.Compare(Item2, other.Item2);
		if (num != 0)
		{
			return num;
		}
		num = Comparer<T3>.Default.Compare(Item3, other.Item3);
		if (num != 0)
		{
			return num;
		}
		num = Comparer<T4>.Default.Compare(Item4, other.Item4);
		if (num != 0)
		{
			return num;
		}
		num = Comparer<T5>.Default.Compare(Item5, other.Item5);
		if (num != 0)
		{
			return num;
		}
		return Comparer<T6>.Default.Compare(Item6, other.Item6);
	}

	/// <summary>Compares the current <see cref="T:System.ValueTuple`6" /> instance to a specified object by using a specified comparer and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
	/// <param name="other">The object to compare with the current instance.</param>
	/// <param name="comparer">An object that provides custom rules for comparison.</param>
	/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="other" /> in the sort order, as shown in the following table.  
	///   Value  
	///
	///   Description  
	///
	///   A negative integer  
	///
	///   This instance precedes <paramref name="other" />.  
	///
	///   Zero  
	///
	///   This instance and <paramref name="other" /> have the same position in the sort order.  
	///
	///   A positive integer  
	///
	///   This instance follows <paramref name="other" />.</returns>
	int IStructuralComparable.CompareTo(object other, IComparer comparer)
	{
		if (other == null)
		{
			return 1;
		}
		if (!(other is (T1, T2, T3, T4, T5, T6) tuple))
		{
			throw new ArgumentException(SR.Format("Argument must be of type {0}.", GetType().ToString()), "other");
		}
		int num = comparer.Compare(Item1, tuple.Item1);
		if (num != 0)
		{
			return num;
		}
		num = comparer.Compare(Item2, tuple.Item2);
		if (num != 0)
		{
			return num;
		}
		num = comparer.Compare(Item3, tuple.Item3);
		if (num != 0)
		{
			return num;
		}
		num = comparer.Compare(Item4, tuple.Item4);
		if (num != 0)
		{
			return num;
		}
		num = comparer.Compare(Item5, tuple.Item5);
		if (num != 0)
		{
			return num;
		}
		return comparer.Compare(Item6, tuple.Item6);
	}

	/// <summary>Calculates the hash code for the current <see cref="T:System.ValueTuple`6" /> instance.</summary>
	/// <returns>The hash code for the current <see cref="T:System.ValueTuple`6" /> instance.</returns>
	public override int GetHashCode()
	{
		return ValueTuple.CombineHashCodes(Item1?.GetHashCode() ?? 0, Item2?.GetHashCode() ?? 0, Item3?.GetHashCode() ?? 0, Item4?.GetHashCode() ?? 0, Item5?.GetHashCode() ?? 0, Item6?.GetHashCode() ?? 0);
	}

	/// <summary>Calculates the hash code for the current <see cref="T:System.ValueTuple`6" /> instance by using a specified computation method.</summary>
	/// <param name="comparer">An object whose <see cref="M:System.Collections.IEqualityComparer.GetHashCode(System.Object)" /> method calculates the hash code of the current <see cref="T:System.ValueTuple`6" /> instance.</param>
	/// <returns>A 32-bit signed integer hash code.</returns>
	int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
	{
		return GetHashCodeCore(comparer);
	}

	private int GetHashCodeCore(IEqualityComparer comparer)
	{
		return ValueTuple.CombineHashCodes(comparer.GetHashCode(Item1), comparer.GetHashCode(Item2), comparer.GetHashCode(Item3), comparer.GetHashCode(Item4), comparer.GetHashCode(Item5), comparer.GetHashCode(Item6));
	}

	int IValueTupleInternal.GetHashCode(IEqualityComparer comparer)
	{
		return GetHashCodeCore(comparer);
	}

	/// <summary>Returns a string that represents the value of this <see cref="T:System.ValueTuple`6" /> instance.</summary>
	/// <returns>The string representation of this <see cref="T:System.ValueTuple`6" /> instance.</returns>
	public override string ToString()
	{
		return "(" + Item1?.ToString() + ", " + Item2?.ToString() + ", " + Item3?.ToString() + ", " + Item4?.ToString() + ", " + Item5?.ToString() + ", " + Item6?.ToString() + ")";
	}

	string IValueTupleInternal.ToStringEnd()
	{
		return Item1?.ToString() + ", " + Item2?.ToString() + ", " + Item3?.ToString() + ", " + Item4?.ToString() + ", " + Item5?.ToString() + ", " + Item6?.ToString() + ")";
	}
}
/// <summary>Represents a value tuple with 7 components.</summary>
/// <typeparam name="T1">The type of the value tuple's first element.</typeparam>
/// <typeparam name="T2">The type of the value tuple's second element.</typeparam>
/// <typeparam name="T3">The type of the value tuple's third element.</typeparam>
/// <typeparam name="T4">The type of the value tuple's fourth element.</typeparam>
/// <typeparam name="T5">The type of the value tuple's fifth element.</typeparam>
/// <typeparam name="T6">The type of the value tuple's sixth element.</typeparam>
/// <typeparam name="T7">The type of the value tuple's seventh element.</typeparam>
[Serializable]
[StructLayout(LayoutKind.Auto)]
public struct ValueTuple<T1, T2, T3, T4, T5, T6, T7> : IEquatable<(T1, T2, T3, T4, T5, T6, T7)>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<(T1, T2, T3, T4, T5, T6, T7)>, IValueTupleInternal, ITuple
{
	/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`7" /> instance's first element.</summary>
	public T1 Item1;

	/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`7" /> instance's second element.</summary>
	public T2 Item2;

	/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`7" /> instance's third element.</summary>
	public T3 Item3;

	/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`7" /> instance's fourth element.</summary>
	public T4 Item4;

	/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`7" /> instance's fifth element.</summary>
	public T5 Item5;

	/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`7" /> instance's sixth element.</summary>
	public T6 Item6;

	/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`7" /> instance's seventh element.</summary>
	public T7 Item7;

	/// <summary>Gets the number of elements in the <see langword="ValueTuple" />.</summary>
	/// <returns>7, the number of elements in a <see cref="T:System.ValueTuple`7" /> object.</returns>
	int ITuple.Length => 7;

	/// <summary>Gets the value of the specified <see langword="ValueTuple" /> element.</summary>
	/// <param name="index">The index of the specified <see langword="ValueTuple" /> element. <paramref name="index" /> can range from 0 (the index of <see cref="F:System.ValueTuple`7.Item1" />) to 6 (the index of  <see cref="F:System.ValueTuple`7.Item7" />).</param>
	/// <returns>The value of the <see langword="ValueTuple" /> element at the specified position.</returns>
	/// <exception cref="T:System.IndexOutOfRangeException">
	///   <paramref name="index" /> is less than 0 or greater than 6.</exception>
	object ITuple.this[int index] => index switch
	{
		0 => Item1, 
		1 => Item2, 
		2 => Item3, 
		3 => Item4, 
		4 => Item5, 
		5 => Item6, 
		6 => Item7, 
		_ => throw new IndexOutOfRangeException(), 
	};

	/// <summary>Initializes a new <see cref="M:System.ValueTuple`7.#ctor(`0,`1,`2,`3,`4,`5,`6)" /> instance.</summary>
	/// <param name="item1">The value tuple's first element.</param>
	/// <param name="item2">The value tuple's second element.</param>
	/// <param name="item3">The value tuple's third element.</param>
	/// <param name="item4">The value tuple's fourth element.</param>
	/// <param name="item5">The value tuple's fifth element.</param>
	/// <param name="item6">The value tuple's sixth element.</param>
	/// <param name="item7">The value tuple's seventh element.</param>
	public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
	{
		Item1 = item1;
		Item2 = item2;
		Item3 = item3;
		Item4 = item4;
		Item5 = item5;
		Item6 = item6;
		Item7 = item7;
	}

	/// <summary>Returns a value that indicates whether the current <see cref="T:System.ValueTuple`7" /> instance is equal to a specified object.</summary>
	/// <param name="obj">The object to compare with this instance.</param>
	/// <returns>
	///   <see langword="true" /> if the current instance is equal to the specified object; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		if (obj is ValueTuple<T1, T2, T3, T4, T5, T6, T7>)
		{
			return Equals(((T1, T2, T3, T4, T5, T6, T7))obj);
		}
		return false;
	}

	public bool Equals((T1, T2, T3, T4, T5, T6, T7) other)
	{
		if (EqualityComparer<T1>.Default.Equals(Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(Item2, other.Item2) && EqualityComparer<T3>.Default.Equals(Item3, other.Item3) && EqualityComparer<T4>.Default.Equals(Item4, other.Item4) && EqualityComparer<T5>.Default.Equals(Item5, other.Item5) && EqualityComparer<T6>.Default.Equals(Item6, other.Item6))
		{
			return EqualityComparer<T7>.Default.Equals(Item7, other.Item7);
		}
		return false;
	}

	/// <summary>Returns a value that indicates whether the current <see cref="T:System.ValueTuple`7" /> instance is equal to a specified object based on a specified comparison method.</summary>
	/// <param name="other">The object to compare with this instance.</param>
	/// <param name="comparer">An object that defines the method to use to evaluate whether the two objects are equal.</param>
	/// <returns>
	///   <see langword="true" /> if the current instance is equal to the specified objects; otherwise, <see langword="false" />.</returns>
	bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
	{
		if (other == null || !(other is (T1, T2, T3, T4, T5, T6, T7) tuple))
		{
			return false;
		}
		if (comparer.Equals(Item1, tuple.Item1) && comparer.Equals(Item2, tuple.Item2) && comparer.Equals(Item3, tuple.Item3) && comparer.Equals(Item4, tuple.Item4) && comparer.Equals(Item5, tuple.Item5) && comparer.Equals(Item6, tuple.Item6))
		{
			return comparer.Equals(Item7, tuple.Item7);
		}
		return false;
	}

	/// <summary>Compares the current <see cref="T:System.ValueTuple`7" /> instance to a specified object by using a specified comparer and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
	/// <param name="other">The object to compare with the current instance.</param>
	/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="obj" /> in the sort order, as shown in the following table.  
	///   Value  
	///
	///   Description  
	///
	///   A negative integer  
	///
	///   This instance precedes <paramref name="other" />.  
	///
	///   Zero  
	///
	///   This instance and <paramref name="other" /> have the same position in the sort order.  
	///
	///   A positive integer  
	///
	///   This instance follows <paramref name="other" />.</returns>
	int IComparable.CompareTo(object other)
	{
		if (other == null)
		{
			return 1;
		}
		if (!(other is ValueTuple<T1, T2, T3, T4, T5, T6, T7>))
		{
			throw new ArgumentException(SR.Format("Argument must be of type {0}.", GetType().ToString()), "other");
		}
		return CompareTo(((T1, T2, T3, T4, T5, T6, T7))other);
	}

	public int CompareTo((T1, T2, T3, T4, T5, T6, T7) other)
	{
		int num = Comparer<T1>.Default.Compare(Item1, other.Item1);
		if (num != 0)
		{
			return num;
		}
		num = Comparer<T2>.Default.Compare(Item2, other.Item2);
		if (num != 0)
		{
			return num;
		}
		num = Comparer<T3>.Default.Compare(Item3, other.Item3);
		if (num != 0)
		{
			return num;
		}
		num = Comparer<T4>.Default.Compare(Item4, other.Item4);
		if (num != 0)
		{
			return num;
		}
		num = Comparer<T5>.Default.Compare(Item5, other.Item5);
		if (num != 0)
		{
			return num;
		}
		num = Comparer<T6>.Default.Compare(Item6, other.Item6);
		if (num != 0)
		{
			return num;
		}
		return Comparer<T7>.Default.Compare(Item7, other.Item7);
	}

	/// <summary>Compares the current <see cref="T:System.ValueTuple`7" /> instance to a specified object by using a specified comparer and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
	/// <param name="other">The object to compare with the current instance.</param>
	/// <param name="comparer">An object that provides custom rules for comparison.</param>
	/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="other" /> in the sort order, as shown in the following table.  
	///   Value  
	///
	///   Description  
	///
	///   A negative integer  
	///
	///   This instance precedes <paramref name="other" />.  
	///
	///   Zero  
	///
	///   This instance and <paramref name="other" /> have the same position in the sort order.  
	///
	///   A positive integer  
	///
	///   This instance follows <paramref name="other" />.</returns>
	int IStructuralComparable.CompareTo(object other, IComparer comparer)
	{
		if (other == null)
		{
			return 1;
		}
		if (!(other is (T1, T2, T3, T4, T5, T6, T7) tuple))
		{
			throw new ArgumentException(SR.Format("Argument must be of type {0}.", GetType().ToString()), "other");
		}
		int num = comparer.Compare(Item1, tuple.Item1);
		if (num != 0)
		{
			return num;
		}
		num = comparer.Compare(Item2, tuple.Item2);
		if (num != 0)
		{
			return num;
		}
		num = comparer.Compare(Item3, tuple.Item3);
		if (num != 0)
		{
			return num;
		}
		num = comparer.Compare(Item4, tuple.Item4);
		if (num != 0)
		{
			return num;
		}
		num = comparer.Compare(Item5, tuple.Item5);
		if (num != 0)
		{
			return num;
		}
		num = comparer.Compare(Item6, tuple.Item6);
		if (num != 0)
		{
			return num;
		}
		return comparer.Compare(Item7, tuple.Item7);
	}

	/// <summary>Calculates the hash code for the current <see cref="T:System.ValueTuple`7" /> instance.</summary>
	/// <returns>The hash code for the current <see cref="T:System.ValueTuple`7" /> instance.</returns>
	public override int GetHashCode()
	{
		return ValueTuple.CombineHashCodes(Item1?.GetHashCode() ?? 0, Item2?.GetHashCode() ?? 0, Item3?.GetHashCode() ?? 0, Item4?.GetHashCode() ?? 0, Item5?.GetHashCode() ?? 0, Item6?.GetHashCode() ?? 0, Item7?.GetHashCode() ?? 0);
	}

	/// <summary>Calculates the hash code for the current <see cref="T:System.ValueTuple`7" /> instance by using a specified computation method.</summary>
	/// <param name="comparer">An object whose <see cref="M:System.Collections.IEqualityComparer.GetHashCode(System.Object)" /> method calculates the hash code of the current <see cref="T:System.ValueTuple`7" /> instance.</param>
	/// <returns>A 32-bit signed integer hash code.</returns>
	int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
	{
		return GetHashCodeCore(comparer);
	}

	private int GetHashCodeCore(IEqualityComparer comparer)
	{
		return ValueTuple.CombineHashCodes(comparer.GetHashCode(Item1), comparer.GetHashCode(Item2), comparer.GetHashCode(Item3), comparer.GetHashCode(Item4), comparer.GetHashCode(Item5), comparer.GetHashCode(Item6), comparer.GetHashCode(Item7));
	}

	int IValueTupleInternal.GetHashCode(IEqualityComparer comparer)
	{
		return GetHashCodeCore(comparer);
	}

	/// <summary>Returns a string that represents the value of this <see cref="T:System.ValueTuple`7" /> instance.</summary>
	/// <returns>The string representation of this <see cref="T:System.ValueTuple`7" /> instance.</returns>
	public override string ToString()
	{
		return "(" + Item1?.ToString() + ", " + Item2?.ToString() + ", " + Item3?.ToString() + ", " + Item4?.ToString() + ", " + Item5?.ToString() + ", " + Item6?.ToString() + ", " + Item7?.ToString() + ")";
	}

	string IValueTupleInternal.ToStringEnd()
	{
		return Item1?.ToString() + ", " + Item2?.ToString() + ", " + Item3?.ToString() + ", " + Item4?.ToString() + ", " + Item5?.ToString() + ", " + Item6?.ToString() + ", " + Item7?.ToString() + ")";
	}
}
/// <summary>Represents an n-value tuple, where n is 8 or greater.</summary>
/// <typeparam name="T1">The type of the value tuple's first element.</typeparam>
/// <typeparam name="T2">The type of the value tuple's second element.</typeparam>
/// <typeparam name="T3">The type of the value tuple's third element.</typeparam>
/// <typeparam name="T4">The type of the value tuple's fourth element.</typeparam>
/// <typeparam name="T5">The type of the value tuple's fifth element.</typeparam>
/// <typeparam name="T6">The type of the value tuple's sixth element.</typeparam>
/// <typeparam name="T7">The type of the value tuple's seventh element.</typeparam>
/// <typeparam name="TRest">Any generic value tuple instance that defines the types of the tuple's remaining elements.</typeparam>
[Serializable]
[StructLayout(LayoutKind.Auto)]
public struct ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> : IEquatable<ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>>, IValueTupleInternal, ITuple where TRest : struct
{
	/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`8" /> instance's first element.</summary>
	public T1 Item1;

	/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`8" /> instance's second element.</summary>
	public T2 Item2;

	/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`8" /> instance's third element.</summary>
	public T3 Item3;

	/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`8" /> instance's fourth element.</summary>
	public T4 Item4;

	/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`8" /> instance's fifth element.</summary>
	public T5 Item5;

	/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`8" /> instance's sixth element.</summary>
	public T6 Item6;

	/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`8" /> instance's seventh element.</summary>
	public T7 Item7;

	/// <summary>Gets the current <see cref="T:System.ValueTuple`8" /> instance's remaining elements.</summary>
	public TRest Rest;

	/// <summary>Gets the number of elements in the <see langword="ValueTuple" />.</summary>
	/// <returns>The number of elements in the <see langword="ValueTuple" />.</returns>
	int ITuple.Length
	{
		get
		{
			if ((object)Rest is IValueTupleInternal valueTupleInternal)
			{
				return 7 + valueTupleInternal.Length;
			}
			return 8;
		}
	}

	/// <summary>Gets the value of the specified <see langword="ValueTuple" /> element.</summary>
	/// <param name="index">The value of the specified <see langword="ValueTuple" /> element. <paramref name="index" /> can range from 0 for <see langword="Item1" /> to one less than the number of elements in the <see langword="ValueTuple" />.</param>
	/// <returns>The value of the <see langword="ValueTuple" /> element at the specified position.</returns>
	/// <exception cref="T:System.IndexOutOfRangeException">
	///   <paramref name="index" /> is less than 0.  
	/// -or-  
	/// <paramref name="index" /> is greater than or equal to <see cref="P:System.ValueTuple`8.System#Runtime#CompilerServices#ITuple#Length" />.</exception>
	object ITuple.this[int index]
	{
		get
		{
			switch (index)
			{
			case 0:
				return Item1;
			case 1:
				return Item2;
			case 2:
				return Item3;
			case 3:
				return Item4;
			case 4:
				return Item5;
			case 5:
				return Item6;
			case 6:
				return Item7;
			default:
				if (!((object)Rest is IValueTupleInternal valueTupleInternal))
				{
					if (index == 7)
					{
						return Rest;
					}
					throw new IndexOutOfRangeException();
				}
				return valueTupleInternal[index - 7];
			}
		}
	}

	/// <summary>Initializes a new <see cref="T:System.ValueTuple`8" /> instance.</summary>
	/// <param name="item1">The value tuple's first element.</param>
	/// <param name="item2">The value tuple's second element.</param>
	/// <param name="item3">The value tuple's third element.</param>
	/// <param name="item4">The value tuple's fourth element.</param>
	/// <param name="item5">The value tuple's fifth element.</param>
	/// <param name="item6">The value tuple's sixth element.</param>
	/// <param name="item7">The value tuple's seventh element.</param>
	/// <param name="rest">An instance of any value tuple type that contains the values of the value's tuple's remaining elements.</param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="rest" /> is not a generic value tuple type.</exception>
	public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, TRest rest)
	{
		if (!(rest is IValueTupleInternal))
		{
			throw new ArgumentException("The last element of an eight element ValueTuple must be a ValueTuple.");
		}
		Item1 = item1;
		Item2 = item2;
		Item3 = item3;
		Item4 = item4;
		Item5 = item5;
		Item6 = item6;
		Item7 = item7;
		Rest = rest;
	}

	/// <summary>Returns a value that indicates whether the current <see cref="T:System.ValueTuple`8" /> instance is equal to a specified object.</summary>
	/// <param name="obj">The object to compare with this instance.</param>
	/// <returns>
	///   <see langword="true" /> if the current instance is equal to the specified object; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		if (obj is ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>)
		{
			return Equals((ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>)obj);
		}
		return false;
	}

	/// <summary>Returns a value that indicates whether the current <see cref="T:System.ValueTuple`8" /> instance is equal to a specified <see cref="T:System.ValueTuple`8" /> instance.</summary>
	/// <param name="other">The value tuple to compare with this instance.</param>
	/// <returns>
	///   <see langword="true" /> if the current instance is equal to the specified tuple; otherwise, <see langword="false" />.</returns>
	public bool Equals(ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> other)
	{
		if (EqualityComparer<T1>.Default.Equals(Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(Item2, other.Item2) && EqualityComparer<T3>.Default.Equals(Item3, other.Item3) && EqualityComparer<T4>.Default.Equals(Item4, other.Item4) && EqualityComparer<T5>.Default.Equals(Item5, other.Item5) && EqualityComparer<T6>.Default.Equals(Item6, other.Item6) && EqualityComparer<T7>.Default.Equals(Item7, other.Item7))
		{
			return EqualityComparer<TRest>.Default.Equals(Rest, other.Rest);
		}
		return false;
	}

	/// <summary>Returns a value that indicates whether the current <see cref="T:System.ValueTuple`8" /> instance is equal to a specified object based on a specified comparison method.</summary>
	/// <param name="other">The object to compare with this instance.</param>
	/// <param name="comparer">An object that defines the method to use to evaluate whether the two objects are equal.</param>
	/// <returns>
	///   <see langword="true" /> if the current instance is equal to the specified objects; otherwise, <see langword="false" />.</returns>
	bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
	{
		if (other == null || !(other is ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> valueTuple))
		{
			return false;
		}
		if (comparer.Equals(Item1, valueTuple.Item1) && comparer.Equals(Item2, valueTuple.Item2) && comparer.Equals(Item3, valueTuple.Item3) && comparer.Equals(Item4, valueTuple.Item4) && comparer.Equals(Item5, valueTuple.Item5) && comparer.Equals(Item6, valueTuple.Item6) && comparer.Equals(Item7, valueTuple.Item7))
		{
			return comparer.Equals(Rest, valueTuple.Rest);
		}
		return false;
	}

	/// <summary>Compares the current <see cref="T:System.ValueTuple`8" /> object to a specified object and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
	/// <param name="other">An object to compare with the current instance.</param>
	/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="obj" /> in the sort order, as shown in the following table.  
	///   Value  
	///
	///   Description  
	///
	///   A negative integer  
	///
	///   This instance precedes <paramref name="other" />.  
	///
	///   Zero  
	///
	///   This instance and <paramref name="other" /> have the same position in the sort order.  
	///
	///   A positive integer  
	///
	///   This instance follows <paramref name="other" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="other" /> is not a <see cref="T:System.ValueTuple`8" /> object.</exception>
	int IComparable.CompareTo(object other)
	{
		if (other == null)
		{
			return 1;
		}
		if (!(other is ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>))
		{
			throw new ArgumentException(SR.Format("Argument must be of type {0}.", GetType().ToString()), "other");
		}
		return CompareTo((ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>)other);
	}

	/// <summary>Compares the current <see cref="T:System.ValueTuple`8" /> instance to a specified <see cref="T:System.ValueTuple`8" /> instance</summary>
	/// <param name="other">The tuple to compare with this instance.</param>
	/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="other" /> in the sort order, as shown in the following table.  
	///   Value  
	///
	///   Description  
	///
	///   A negative integer  
	///
	///   This instance precedes <paramref name="other" />.  
	///
	///   Zero  
	///
	///   This instance and <paramref name="other" /> have the same position in the sort order.  
	///
	///   A positive integer  
	///
	///   This instance follows <paramref name="other" />.</returns>
	public int CompareTo(ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> other)
	{
		int num = Comparer<T1>.Default.Compare(Item1, other.Item1);
		if (num != 0)
		{
			return num;
		}
		num = Comparer<T2>.Default.Compare(Item2, other.Item2);
		if (num != 0)
		{
			return num;
		}
		num = Comparer<T3>.Default.Compare(Item3, other.Item3);
		if (num != 0)
		{
			return num;
		}
		num = Comparer<T4>.Default.Compare(Item4, other.Item4);
		if (num != 0)
		{
			return num;
		}
		num = Comparer<T5>.Default.Compare(Item5, other.Item5);
		if (num != 0)
		{
			return num;
		}
		num = Comparer<T6>.Default.Compare(Item6, other.Item6);
		if (num != 0)
		{
			return num;
		}
		num = Comparer<T7>.Default.Compare(Item7, other.Item7);
		if (num != 0)
		{
			return num;
		}
		return Comparer<TRest>.Default.Compare(Rest, other.Rest);
	}

	/// <summary>Compares the current <see cref="T:System.ValueTuple`8" /> instance to a specified object by using a specified comparer and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
	/// <param name="other">The object to compare with the current instance.</param>
	/// <param name="comparer">An object that provides custom rules for comparison.</param>
	/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="other" /> in the sort order, as shown in the following table.  
	///   Value  
	///
	///   Description  
	///
	///   A negative integer  
	///
	///   This instance precedes <paramref name="other" />.  
	///
	///   Zero  
	///
	///   This instance and <paramref name="other" /> have the same position in the sort order.  
	///
	///   A positive integer  
	///
	///   This instance follows <paramref name="other" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="other" /> is not a <see cref="T:System.ValueTuple`8" /> object.</exception>
	int IStructuralComparable.CompareTo(object other, IComparer comparer)
	{
		if (other == null)
		{
			return 1;
		}
		if (!(other is ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> valueTuple))
		{
			throw new ArgumentException(SR.Format("Argument must be of type {0}.", GetType().ToString()), "other");
		}
		int num = comparer.Compare(Item1, valueTuple.Item1);
		if (num != 0)
		{
			return num;
		}
		num = comparer.Compare(Item2, valueTuple.Item2);
		if (num != 0)
		{
			return num;
		}
		num = comparer.Compare(Item3, valueTuple.Item3);
		if (num != 0)
		{
			return num;
		}
		num = comparer.Compare(Item4, valueTuple.Item4);
		if (num != 0)
		{
			return num;
		}
		num = comparer.Compare(Item5, valueTuple.Item5);
		if (num != 0)
		{
			return num;
		}
		num = comparer.Compare(Item6, valueTuple.Item6);
		if (num != 0)
		{
			return num;
		}
		num = comparer.Compare(Item7, valueTuple.Item7);
		if (num != 0)
		{
			return num;
		}
		return comparer.Compare(Rest, valueTuple.Rest);
	}

	/// <summary>Calculates the hash code for the current <see cref="T:System.ValueTuple`8" /> instance.</summary>
	/// <returns>The hash code for the current <see cref="T:System.ValueTuple`8" /> instance.</returns>
	public override int GetHashCode()
	{
		T1 val;
		int h;
		if (!((object)Rest is IValueTupleInternal { Length: var length } valueTupleInternal))
		{
			ref T1 reference = ref Item1;
			val = default(T1);
			if (val == null)
			{
				val = reference;
				reference = ref val;
				if (val == null)
				{
					h = 0;
					goto IL_004c;
				}
			}
			h = reference.GetHashCode();
			goto IL_004c;
		}
		if (length >= 8)
		{
			return valueTupleInternal.GetHashCode();
		}
		T7 val2;
		int h24;
		T6 val7;
		int h12;
		T5 val5;
		int h10;
		T4 val3;
		int h5;
		T3 val6;
		int h26;
		T2 val4;
		int h27;
		int h2;
		ref T7 reference3;
		int h3;
		ref T4 reference4;
		int h4;
		int h6;
		int h7;
		int h8;
		ref T2 reference5;
		int h9;
		ref T5 reference7;
		int h11;
		int h13;
		ref T3 reference8;
		int h14;
		ref T6 reference9;
		int h15;
		ref T6 reference10;
		int h16;
		ref T5 reference11;
		int h17;
		ref T4 reference12;
		int h18;
		ref T6 reference13;
		int h19;
		ref T6 reference14;
		ref T7 reference15;
		ref T5 reference16;
		int h20;
		ref T7 reference18;
		int h21;
		ref T7 reference19;
		int h22;
		ref T6 reference20;
		int h23;
		ref T7 reference22;
		ref T7 reference23;
		int h25;
		int h28;
		int h29;
		ref T4 reference27;
		ref T3 reference28;
		ref T5 reference29;
		switch (8 - length)
		{
		case 1:
		{
			ref T7 reference21 = ref Item7;
			val2 = default(T7);
			if (val2 == null)
			{
				val2 = reference21;
				reference21 = ref val2;
				if (val2 == null)
				{
					h24 = 0;
					goto IL_021d;
				}
			}
			h24 = reference21.GetHashCode();
			goto IL_021d;
		}
		case 2:
		{
			ref T6 reference17 = ref Item6;
			val7 = default(T6);
			if (val7 == null)
			{
				val7 = reference17;
				reference17 = ref val7;
				if (val7 == null)
				{
					h12 = 0;
					goto IL_0261;
				}
			}
			h12 = reference17.GetHashCode();
			goto IL_0261;
		}
		case 3:
		{
			ref T5 reference6 = ref Item5;
			val5 = default(T5);
			if (val5 == null)
			{
				val5 = reference6;
				reference6 = ref val5;
				if (val5 == null)
				{
					h10 = 0;
					goto IL_02dd;
				}
			}
			h10 = reference6.GetHashCode();
			goto IL_02dd;
		}
		case 4:
		{
			ref T4 reference25 = ref Item4;
			val3 = default(T4);
			if (val3 == null)
			{
				val3 = reference25;
				reference25 = ref val3;
				if (val3 == null)
				{
					h5 = 0;
					goto IL_0391;
				}
			}
			h5 = reference25.GetHashCode();
			goto IL_0391;
		}
		case 5:
		{
			ref T3 reference24 = ref Item3;
			val6 = default(T3);
			if (val6 == null)
			{
				val6 = reference24;
				reference24 = ref val6;
				if (val6 == null)
				{
					h26 = 0;
					goto IL_047d;
				}
			}
			h26 = reference24.GetHashCode();
			goto IL_047d;
		}
		case 6:
		{
			ref T2 reference26 = ref Item2;
			val4 = default(T2);
			if (val4 == null)
			{
				val4 = reference26;
				reference26 = ref val4;
				if (val4 == null)
				{
					h27 = 0;
					goto IL_05a1;
				}
			}
			h27 = reference26.GetHashCode();
			goto IL_05a1;
		}
		case 7:
		case 8:
		{
			ref T1 reference2 = ref Item1;
			val = default(T1);
			if (val == null)
			{
				val = reference2;
				reference2 = ref val;
				if (val == null)
				{
					h2 = 0;
					goto IL_06fa;
				}
			}
			h2 = reference2.GetHashCode();
			goto IL_06fa;
		}
		default:
			{
				return -1;
			}
			IL_0315:
			reference3 = ref Item7;
			val2 = default(T7);
			if (val2 == null)
			{
				val2 = reference3;
				reference3 = ref val2;
				if (val2 == null)
				{
					h3 = 0;
					goto IL_034d;
				}
			}
			h3 = reference3.GetHashCode();
			goto IL_034d;
			IL_05d9:
			reference4 = ref Item4;
			val3 = default(T4);
			if (val3 == null)
			{
				val3 = reference4;
				reference4 = ref val3;
				if (val3 == null)
				{
					h4 = 0;
					goto IL_0611;
				}
			}
			h4 = reference4.GetHashCode();
			goto IL_0611;
			IL_0439:
			return ValueTuple.CombineHashCodes(h5, h6, h7, h8, valueTupleInternal.GetHashCode());
			IL_06fa:
			reference5 = ref Item2;
			val4 = default(T2);
			if (val4 == null)
			{
				val4 = reference5;
				reference5 = ref val4;
				if (val4 == null)
				{
					h9 = 0;
					goto IL_0732;
				}
			}
			h9 = reference5.GetHashCode();
			goto IL_0732;
			IL_04b5:
			reference7 = ref Item5;
			val5 = default(T5);
			if (val5 == null)
			{
				val5 = reference7;
				reference7 = ref val5;
				if (val5 == null)
				{
					h11 = 0;
					goto IL_04ed;
				}
			}
			h11 = reference7.GetHashCode();
			goto IL_04ed;
			IL_0299:
			return ValueTuple.CombineHashCodes(h12, h13, valueTupleInternal.GetHashCode());
			IL_0732:
			reference8 = ref Item3;
			val6 = default(T3);
			if (val6 == null)
			{
				val6 = reference8;
				reference8 = ref val6;
				if (val6 == null)
				{
					h14 = 0;
					goto IL_076a;
				}
			}
			h14 = reference8.GetHashCode();
			goto IL_076a;
			IL_04ed:
			reference9 = ref Item6;
			val7 = default(T6);
			if (val7 == null)
			{
				val7 = reference9;
				reference9 = ref val7;
				if (val7 == null)
				{
					h15 = 0;
					goto IL_0525;
				}
			}
			h15 = reference9.GetHashCode();
			goto IL_0525;
			IL_02dd:
			reference10 = ref Item6;
			val7 = default(T6);
			if (val7 == null)
			{
				val7 = reference10;
				reference10 = ref val7;
				if (val7 == null)
				{
					h16 = 0;
					goto IL_0315;
				}
			}
			h16 = reference10.GetHashCode();
			goto IL_0315;
			IL_0611:
			reference11 = ref Item5;
			val5 = default(T5);
			if (val5 == null)
			{
				val5 = reference11;
				reference11 = ref val5;
				if (val5 == null)
				{
					h17 = 0;
					goto IL_0649;
				}
			}
			h17 = reference11.GetHashCode();
			goto IL_0649;
			IL_076a:
			reference12 = ref Item4;
			val3 = default(T4);
			if (val3 == null)
			{
				val3 = reference12;
				reference12 = ref val3;
				if (val3 == null)
				{
					h18 = 0;
					goto IL_07a2;
				}
			}
			h18 = reference12.GetHashCode();
			goto IL_07a2;
			IL_0649:
			reference13 = ref Item6;
			val7 = default(T6);
			if (val7 == null)
			{
				val7 = reference13;
				reference13 = ref val7;
				if (val7 == null)
				{
					h19 = 0;
					goto IL_0681;
				}
			}
			h19 = reference13.GetHashCode();
			goto IL_0681;
			IL_03c9:
			reference14 = ref Item6;
			val7 = default(T6);
			if (val7 == null)
			{
				val7 = reference14;
				reference14 = ref val7;
				if (val7 == null)
				{
					h7 = 0;
					goto IL_0401;
				}
			}
			h7 = reference14.GetHashCode();
			goto IL_0401;
			IL_0261:
			reference15 = ref Item7;
			val2 = default(T7);
			if (val2 == null)
			{
				val2 = reference15;
				reference15 = ref val2;
				if (val2 == null)
				{
					h13 = 0;
					goto IL_0299;
				}
			}
			h13 = reference15.GetHashCode();
			goto IL_0299;
			IL_07a2:
			reference16 = ref Item5;
			val5 = default(T5);
			if (val5 == null)
			{
				val5 = reference16;
				reference16 = ref val5;
				if (val5 == null)
				{
					h20 = 0;
					goto IL_07da;
				}
			}
			h20 = reference16.GetHashCode();
			goto IL_07da;
			IL_0681:
			reference18 = ref Item7;
			val2 = default(T7);
			if (val2 == null)
			{
				val2 = reference18;
				reference18 = ref val2;
				if (val2 == null)
				{
					h21 = 0;
					goto IL_06b9;
				}
			}
			h21 = reference18.GetHashCode();
			goto IL_06b9;
			IL_0525:
			reference19 = ref Item7;
			val2 = default(T7);
			if (val2 == null)
			{
				val2 = reference19;
				reference19 = ref val2;
				if (val2 == null)
				{
					h22 = 0;
					goto IL_055d;
				}
			}
			h22 = reference19.GetHashCode();
			goto IL_055d;
			IL_07da:
			reference20 = ref Item6;
			val7 = default(T6);
			if (val7 == null)
			{
				val7 = reference20;
				reference20 = ref val7;
				if (val7 == null)
				{
					h23 = 0;
					goto IL_0812;
				}
			}
			h23 = reference20.GetHashCode();
			goto IL_0812;
			IL_0401:
			reference22 = ref Item7;
			val2 = default(T7);
			if (val2 == null)
			{
				val2 = reference22;
				reference22 = ref val2;
				if (val2 == null)
				{
					h8 = 0;
					goto IL_0439;
				}
			}
			h8 = reference22.GetHashCode();
			goto IL_0439;
			IL_034d:
			return ValueTuple.CombineHashCodes(h10, h16, h3, valueTupleInternal.GetHashCode());
			IL_0812:
			reference23 = ref Item7;
			val2 = default(T7);
			if (val2 == null)
			{
				val2 = reference23;
				reference23 = ref val2;
				if (val2 == null)
				{
					h25 = 0;
					goto IL_084a;
				}
			}
			h25 = reference23.GetHashCode();
			goto IL_084a;
			IL_06b9:
			return ValueTuple.CombineHashCodes(h27, h28, h4, h17, h19, h21, valueTupleInternal.GetHashCode());
			IL_084a:
			return ValueTuple.CombineHashCodes(h2, h9, h14, h18, h20, h23, h25, valueTupleInternal.GetHashCode());
			IL_055d:
			return ValueTuple.CombineHashCodes(h26, h29, h11, h15, h22, valueTupleInternal.GetHashCode());
			IL_047d:
			reference27 = ref Item4;
			val3 = default(T4);
			if (val3 == null)
			{
				val3 = reference27;
				reference27 = ref val3;
				if (val3 == null)
				{
					h29 = 0;
					goto IL_04b5;
				}
			}
			h29 = reference27.GetHashCode();
			goto IL_04b5;
			IL_021d:
			return ValueTuple.CombineHashCodes(h24, valueTupleInternal.GetHashCode());
			IL_05a1:
			reference28 = ref Item3;
			val6 = default(T3);
			if (val6 == null)
			{
				val6 = reference28;
				reference28 = ref val6;
				if (val6 == null)
				{
					h28 = 0;
					goto IL_05d9;
				}
			}
			h28 = reference28.GetHashCode();
			goto IL_05d9;
			IL_0391:
			reference29 = ref Item5;
			val5 = default(T5);
			if (val5 == null)
			{
				val5 = reference29;
				reference29 = ref val5;
				if (val5 == null)
				{
					h6 = 0;
					goto IL_03c9;
				}
			}
			h6 = reference29.GetHashCode();
			goto IL_03c9;
		}
		IL_00f4:
		ref T5 reference30 = ref Item5;
		val5 = default(T5);
		int h30;
		if (val5 == null)
		{
			val5 = reference30;
			reference30 = ref val5;
			if (val5 == null)
			{
				h30 = 0;
				goto IL_012c;
			}
		}
		h30 = reference30.GetHashCode();
		goto IL_012c;
		IL_019c:
		int h31;
		int h32;
		int h33;
		int h34;
		int h35;
		return ValueTuple.CombineHashCodes(h, h31, h32, h33, h30, h34, h35);
		IL_0164:
		ref T7 reference31 = ref Item7;
		val2 = default(T7);
		if (val2 == null)
		{
			val2 = reference31;
			reference31 = ref val2;
			if (val2 == null)
			{
				h35 = 0;
				goto IL_019c;
			}
		}
		h35 = reference31.GetHashCode();
		goto IL_019c;
		IL_0084:
		ref T3 reference32 = ref Item3;
		val6 = default(T3);
		if (val6 == null)
		{
			val6 = reference32;
			reference32 = ref val6;
			if (val6 == null)
			{
				h32 = 0;
				goto IL_00bc;
			}
		}
		h32 = reference32.GetHashCode();
		goto IL_00bc;
		IL_012c:
		ref T6 reference33 = ref Item6;
		val7 = default(T6);
		if (val7 == null)
		{
			val7 = reference33;
			reference33 = ref val7;
			if (val7 == null)
			{
				h34 = 0;
				goto IL_0164;
			}
		}
		h34 = reference33.GetHashCode();
		goto IL_0164;
		IL_00bc:
		ref T4 reference34 = ref Item4;
		val3 = default(T4);
		if (val3 == null)
		{
			val3 = reference34;
			reference34 = ref val3;
			if (val3 == null)
			{
				h33 = 0;
				goto IL_00f4;
			}
		}
		h33 = reference34.GetHashCode();
		goto IL_00f4;
		IL_004c:
		ref T2 reference35 = ref Item2;
		val4 = default(T2);
		if (val4 == null)
		{
			val4 = reference35;
			reference35 = ref val4;
			if (val4 == null)
			{
				h31 = 0;
				goto IL_0084;
			}
		}
		h31 = reference35.GetHashCode();
		goto IL_0084;
	}

	/// <summary>Calculates the hash code for the current <see cref="T:System.ValueTuple`8" /> instance by using a specified computation method.</summary>
	/// <param name="comparer">An object whose <see cref="M:System.Collections.IEqualityComparer.GetHashCode(System.Object)" /> method calculates the hash code of the current <see cref="T:System.ValueTuple`8" /> instance.</param>
	/// <returns>A 32-bit signed integer hash code.</returns>
	int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
	{
		return GetHashCodeCore(comparer);
	}

	private int GetHashCodeCore(IEqualityComparer comparer)
	{
		if (!((object)Rest is IValueTupleInternal { Length: var length } valueTupleInternal))
		{
			return ValueTuple.CombineHashCodes(comparer.GetHashCode(Item1), comparer.GetHashCode(Item2), comparer.GetHashCode(Item3), comparer.GetHashCode(Item4), comparer.GetHashCode(Item5), comparer.GetHashCode(Item6), comparer.GetHashCode(Item7));
		}
		if (length >= 8)
		{
			return valueTupleInternal.GetHashCode(comparer);
		}
		switch (8 - length)
		{
		case 1:
			return ValueTuple.CombineHashCodes(comparer.GetHashCode(Item7), valueTupleInternal.GetHashCode(comparer));
		case 2:
			return ValueTuple.CombineHashCodes(comparer.GetHashCode(Item6), comparer.GetHashCode(Item7), valueTupleInternal.GetHashCode(comparer));
		case 3:
			return ValueTuple.CombineHashCodes(comparer.GetHashCode(Item5), comparer.GetHashCode(Item6), comparer.GetHashCode(Item7), valueTupleInternal.GetHashCode(comparer));
		case 4:
			return ValueTuple.CombineHashCodes(comparer.GetHashCode(Item4), comparer.GetHashCode(Item5), comparer.GetHashCode(Item6), comparer.GetHashCode(Item7), valueTupleInternal.GetHashCode(comparer));
		case 5:
			return ValueTuple.CombineHashCodes(comparer.GetHashCode(Item3), comparer.GetHashCode(Item4), comparer.GetHashCode(Item5), comparer.GetHashCode(Item6), comparer.GetHashCode(Item7), valueTupleInternal.GetHashCode(comparer));
		case 6:
			return ValueTuple.CombineHashCodes(comparer.GetHashCode(Item2), comparer.GetHashCode(Item3), comparer.GetHashCode(Item4), comparer.GetHashCode(Item5), comparer.GetHashCode(Item6), comparer.GetHashCode(Item7), valueTupleInternal.GetHashCode(comparer));
		case 7:
		case 8:
			return ValueTuple.CombineHashCodes(comparer.GetHashCode(Item1), comparer.GetHashCode(Item2), comparer.GetHashCode(Item3), comparer.GetHashCode(Item4), comparer.GetHashCode(Item5), comparer.GetHashCode(Item6), comparer.GetHashCode(Item7), valueTupleInternal.GetHashCode(comparer));
		default:
			return -1;
		}
	}

	int IValueTupleInternal.GetHashCode(IEqualityComparer comparer)
	{
		return GetHashCodeCore(comparer);
	}

	/// <summary>Returns a string that represents the value of this <see cref="T:System.ValueTuple`8" /> instance.</summary>
	/// <returns>The string representation of this <see cref="T:System.ValueTuple`8" /> instance.</returns>
	public override string ToString()
	{
		string[] obj;
		T1 val;
		object obj2;
		if (!((object)Rest is IValueTupleInternal valueTupleInternal))
		{
			obj = new string[17]
			{
				"(", null, null, null, null, null, null, null, null, null,
				null, null, null, null, null, null, null
			};
			ref T1 reference = ref Item1;
			val = default(T1);
			if (val == null)
			{
				val = reference;
				reference = ref val;
				if (val == null)
				{
					obj2 = null;
					goto IL_005d;
				}
			}
			obj2 = reference.ToString();
			goto IL_005d;
		}
		string[] obj3 = new string[16]
		{
			"(", null, null, null, null, null, null, null, null, null,
			null, null, null, null, null, null
		};
		ref T1 reference2 = ref Item1;
		val = default(T1);
		object obj4;
		if (val == null)
		{
			val = reference2;
			reference2 = ref val;
			if (val == null)
			{
				obj4 = null;
				goto IL_0262;
			}
		}
		obj4 = reference2.ToString();
		goto IL_0262;
		IL_0164:
		object obj5;
		obj[9] = (string)obj5;
		obj[10] = ", ";
		ref T6 reference3 = ref Item6;
		T6 val2 = default(T6);
		object obj6;
		if (val2 == null)
		{
			val2 = reference3;
			reference3 = ref val2;
			if (val2 == null)
			{
				obj6 = null;
				goto IL_01a9;
			}
		}
		obj6 = reference3.ToString();
		goto IL_01a9;
		IL_0325:
		object obj7;
		obj3[7] = (string)obj7;
		obj3[8] = ", ";
		ref T5 reference4 = ref Item5;
		T5 val3 = default(T5);
		object obj8;
		if (val3 == null)
		{
			val3 = reference4;
			reference4 = ref val3;
			if (val3 == null)
			{
				obj8 = null;
				goto IL_0369;
			}
		}
		obj8 = reference4.ToString();
		goto IL_0369;
		IL_009d:
		object obj9;
		obj[3] = (string)obj9;
		obj[4] = ", ";
		ref T3 reference5 = ref Item3;
		T3 val4 = default(T3);
		object obj10;
		if (val4 == null)
		{
			val4 = reference5;
			reference5 = ref val4;
			if (val4 == null)
			{
				obj10 = null;
				goto IL_00dd;
			}
		}
		obj10 = reference5.ToString();
		goto IL_00dd;
		IL_005d:
		obj[1] = (string)obj2;
		obj[2] = ", ";
		ref T2 reference6 = ref Item2;
		T2 val5 = default(T2);
		if (val5 == null)
		{
			val5 = reference6;
			reference6 = ref val5;
			if (val5 == null)
			{
				obj9 = null;
				goto IL_009d;
			}
		}
		obj9 = reference6.ToString();
		goto IL_009d;
		IL_0262:
		obj3[1] = (string)obj4;
		obj3[2] = ", ";
		ref T2 reference7 = ref Item2;
		val5 = default(T2);
		object obj11;
		if (val5 == null)
		{
			val5 = reference7;
			reference7 = ref val5;
			if (val5 == null)
			{
				obj11 = null;
				goto IL_02a2;
			}
		}
		obj11 = reference7.ToString();
		goto IL_02a2;
		IL_00dd:
		obj[5] = (string)obj10;
		obj[6] = ", ";
		ref T4 reference8 = ref Item4;
		T4 val6 = default(T4);
		object obj12;
		if (val6 == null)
		{
			val6 = reference8;
			reference8 = ref val6;
			if (val6 == null)
			{
				obj12 = null;
				goto IL_0120;
			}
		}
		obj12 = reference8.ToString();
		goto IL_0120;
		IL_03ae:
		object obj13;
		obj3[11] = (string)obj13;
		obj3[12] = ", ";
		ref T7 reference9 = ref Item7;
		T7 val7 = default(T7);
		object obj14;
		if (val7 == null)
		{
			val7 = reference9;
			reference9 = ref val7;
			if (val7 == null)
			{
				obj14 = null;
				goto IL_03f3;
			}
		}
		obj14 = reference9.ToString();
		goto IL_03f3;
		IL_0369:
		obj3[9] = (string)obj8;
		obj3[10] = ", ";
		ref T6 reference10 = ref Item6;
		val2 = default(T6);
		if (val2 == null)
		{
			val2 = reference10;
			reference10 = ref val2;
			if (val2 == null)
			{
				obj13 = null;
				goto IL_03ae;
			}
		}
		obj13 = reference10.ToString();
		goto IL_03ae;
		IL_02a2:
		obj3[3] = (string)obj11;
		obj3[4] = ", ";
		ref T3 reference11 = ref Item3;
		val4 = default(T3);
		object obj15;
		if (val4 == null)
		{
			val4 = reference11;
			reference11 = ref val4;
			if (val4 == null)
			{
				obj15 = null;
				goto IL_02e2;
			}
		}
		obj15 = reference11.ToString();
		goto IL_02e2;
		IL_01ee:
		object obj16;
		obj[13] = (string)obj16;
		obj[14] = ", ";
		obj[15] = Rest.ToString();
		obj[16] = ")";
		return string.Concat(obj);
		IL_01a9:
		obj[11] = (string)obj6;
		obj[12] = ", ";
		ref T7 reference12 = ref Item7;
		val7 = default(T7);
		if (val7 == null)
		{
			val7 = reference12;
			reference12 = ref val7;
			if (val7 == null)
			{
				obj16 = null;
				goto IL_01ee;
			}
		}
		obj16 = reference12.ToString();
		goto IL_01ee;
		IL_0120:
		obj[7] = (string)obj12;
		obj[8] = ", ";
		ref T5 reference13 = ref Item5;
		val3 = default(T5);
		if (val3 == null)
		{
			val3 = reference13;
			reference13 = ref val3;
			if (val3 == null)
			{
				obj5 = null;
				goto IL_0164;
			}
		}
		obj5 = reference13.ToString();
		goto IL_0164;
		IL_02e2:
		obj3[5] = (string)obj15;
		obj3[6] = ", ";
		ref T4 reference14 = ref Item4;
		val6 = default(T4);
		if (val6 == null)
		{
			val6 = reference14;
			reference14 = ref val6;
			if (val6 == null)
			{
				obj7 = null;
				goto IL_0325;
			}
		}
		obj7 = reference14.ToString();
		goto IL_0325;
		IL_03f3:
		obj3[13] = (string)obj14;
		obj3[14] = ", ";
		obj3[15] = valueTupleInternal.ToStringEnd();
		return string.Concat(obj3);
	}

	string IValueTupleInternal.ToStringEnd()
	{
		string[] array;
		T1 val;
		object obj;
		if (!((object)Rest is IValueTupleInternal valueTupleInternal))
		{
			array = new string[16];
			ref T1 reference = ref Item1;
			val = default(T1);
			if (val == null)
			{
				val = reference;
				reference = ref val;
				if (val == null)
				{
					obj = null;
					goto IL_0055;
				}
			}
			obj = reference.ToString();
			goto IL_0055;
		}
		string[] array2 = new string[15];
		ref T1 reference2 = ref Item1;
		val = default(T1);
		object obj2;
		if (val == null)
		{
			val = reference2;
			reference2 = ref val;
			if (val == null)
			{
				obj2 = null;
				goto IL_0251;
			}
		}
		obj2 = reference2.ToString();
		goto IL_0251;
		IL_015b:
		object obj3;
		array[8] = (string)obj3;
		array[9] = ", ";
		ref T6 reference3 = ref Item6;
		T6 val2 = default(T6);
		object obj4;
		if (val2 == null)
		{
			val2 = reference3;
			reference3 = ref val2;
			if (val2 == null)
			{
				obj4 = null;
				goto IL_01a0;
			}
		}
		obj4 = reference3.ToString();
		goto IL_01a0;
		IL_0314:
		object obj5;
		array2[6] = (string)obj5;
		array2[7] = ", ";
		ref T5 reference4 = ref Item5;
		T5 val3 = default(T5);
		object obj6;
		if (val3 == null)
		{
			val3 = reference4;
			reference4 = ref val3;
			if (val3 == null)
			{
				obj6 = null;
				goto IL_0357;
			}
		}
		obj6 = reference4.ToString();
		goto IL_0357;
		IL_0095:
		object obj7;
		array[2] = (string)obj7;
		array[3] = ", ";
		ref T3 reference5 = ref Item3;
		T3 val4 = default(T3);
		object obj8;
		if (val4 == null)
		{
			val4 = reference5;
			reference5 = ref val4;
			if (val4 == null)
			{
				obj8 = null;
				goto IL_00d5;
			}
		}
		obj8 = reference5.ToString();
		goto IL_00d5;
		IL_0055:
		array[0] = (string)obj;
		array[1] = ", ";
		ref T2 reference6 = ref Item2;
		T2 val5 = default(T2);
		if (val5 == null)
		{
			val5 = reference6;
			reference6 = ref val5;
			if (val5 == null)
			{
				obj7 = null;
				goto IL_0095;
			}
		}
		obj7 = reference6.ToString();
		goto IL_0095;
		IL_0251:
		array2[0] = (string)obj2;
		array2[1] = ", ";
		ref T2 reference7 = ref Item2;
		val5 = default(T2);
		object obj9;
		if (val5 == null)
		{
			val5 = reference7;
			reference7 = ref val5;
			if (val5 == null)
			{
				obj9 = null;
				goto IL_0291;
			}
		}
		obj9 = reference7.ToString();
		goto IL_0291;
		IL_00d5:
		array[4] = (string)obj8;
		array[5] = ", ";
		ref T4 reference8 = ref Item4;
		T4 val6 = default(T4);
		object obj10;
		if (val6 == null)
		{
			val6 = reference8;
			reference8 = ref val6;
			if (val6 == null)
			{
				obj10 = null;
				goto IL_0118;
			}
		}
		obj10 = reference8.ToString();
		goto IL_0118;
		IL_039c:
		object obj11;
		array2[10] = (string)obj11;
		array2[11] = ", ";
		ref T7 reference9 = ref Item7;
		T7 val7 = default(T7);
		object obj12;
		if (val7 == null)
		{
			val7 = reference9;
			reference9 = ref val7;
			if (val7 == null)
			{
				obj12 = null;
				goto IL_03e1;
			}
		}
		obj12 = reference9.ToString();
		goto IL_03e1;
		IL_0357:
		array2[8] = (string)obj6;
		array2[9] = ", ";
		ref T6 reference10 = ref Item6;
		val2 = default(T6);
		if (val2 == null)
		{
			val2 = reference10;
			reference10 = ref val2;
			if (val2 == null)
			{
				obj11 = null;
				goto IL_039c;
			}
		}
		obj11 = reference10.ToString();
		goto IL_039c;
		IL_0291:
		array2[2] = (string)obj9;
		array2[3] = ", ";
		ref T3 reference11 = ref Item3;
		val4 = default(T3);
		object obj13;
		if (val4 == null)
		{
			val4 = reference11;
			reference11 = ref val4;
			if (val4 == null)
			{
				obj13 = null;
				goto IL_02d1;
			}
		}
		obj13 = reference11.ToString();
		goto IL_02d1;
		IL_01e5:
		object obj14;
		array[12] = (string)obj14;
		array[13] = ", ";
		array[14] = Rest.ToString();
		array[15] = ")";
		return string.Concat(array);
		IL_01a0:
		array[10] = (string)obj4;
		array[11] = ", ";
		ref T7 reference12 = ref Item7;
		val7 = default(T7);
		if (val7 == null)
		{
			val7 = reference12;
			reference12 = ref val7;
			if (val7 == null)
			{
				obj14 = null;
				goto IL_01e5;
			}
		}
		obj14 = reference12.ToString();
		goto IL_01e5;
		IL_0118:
		array[6] = (string)obj10;
		array[7] = ", ";
		ref T5 reference13 = ref Item5;
		val3 = default(T5);
		if (val3 == null)
		{
			val3 = reference13;
			reference13 = ref val3;
			if (val3 == null)
			{
				obj3 = null;
				goto IL_015b;
			}
		}
		obj3 = reference13.ToString();
		goto IL_015b;
		IL_02d1:
		array2[4] = (string)obj13;
		array2[5] = ", ";
		ref T4 reference14 = ref Item4;
		val6 = default(T4);
		if (val6 == null)
		{
			val6 = reference14;
			reference14 = ref val6;
			if (val6 == null)
			{
				obj5 = null;
				goto IL_0314;
			}
		}
		obj5 = reference14.ToString();
		goto IL_0314;
		IL_03e1:
		array2[12] = (string)obj12;
		array2[13] = ", ";
		array2[14] = valueTupleInternal.ToStringEnd();
		return string.Concat(array2);
	}
}
