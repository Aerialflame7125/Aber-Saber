using System.Runtime.CompilerServices;

namespace System;

/// <summary>Provides extension methods for tuples to interoperate with language support for tuples in C#.</summary>
public static class TupleExtensions
{
	/// <summary>Deconstructs a tuple with 1 element into a separate variable.</summary>
	/// <param name="value">The 1-element tuple to deconstruct into a separate variable.</param>
	/// <param name="item1">The value of the single element.</param>
	/// <typeparam name="T1">The type of the single element.</typeparam>
	public static void Deconstruct<T1>(this Tuple<T1> value, out T1 item1)
	{
		item1 = value.Item1;
	}

	/// <summary>Deconstructs a tuple with 2 elements into separate variables.</summary>
	/// <param name="value">The 2-element tuple to deconstruct into 2 separate variables.</param>
	/// <param name="item1">The value of the first element.</param>
	/// <param name="item2">The value of the second element.</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	public static void Deconstruct<T1, T2>(this Tuple<T1, T2> value, out T1 item1, out T2 item2)
	{
		item1 = value.Item1;
		item2 = value.Item2;
	}

	/// <summary>Deconstructs a tuple with 3 elements into separate variables.</summary>
	/// <param name="value">The 3-element tuple to deconstruct into 3 separate variables.</param>
	/// <param name="item1">The value of the first element.</param>
	/// <param name="item2">The value of the second element.</param>
	/// <param name="item3">The value of the third element.</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	/// <typeparam name="T3">The type of the third element.</typeparam>
	public static void Deconstruct<T1, T2, T3>(this Tuple<T1, T2, T3> value, out T1 item1, out T2 item2, out T3 item3)
	{
		item1 = value.Item1;
		item2 = value.Item2;
		item3 = value.Item3;
	}

	/// <summary>Deconstructs a tuple with 4 elements into separate variables.</summary>
	/// <param name="value">The 4-element tuple to deconstruct into 4 separate variables.</param>
	/// <param name="item1">The value of the first element.</param>
	/// <param name="item2">The value of the second element.</param>
	/// <param name="item3">The value of the third element.</param>
	/// <param name="item4">The value of the fourth element.</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	/// <typeparam name="T3">The type of the third element.</typeparam>
	/// <typeparam name="T4">The type of the fourth element.</typeparam>
	public static void Deconstruct<T1, T2, T3, T4>(this Tuple<T1, T2, T3, T4> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4)
	{
		item1 = value.Item1;
		item2 = value.Item2;
		item3 = value.Item3;
		item4 = value.Item4;
	}

	/// <summary>Deconstructs a tuple with 5 elements into separate variables.</summary>
	/// <param name="value">The 5-element tuple to deconstruct into 5 separate variables.</param>
	/// <param name="item1">The value of the first element.</param>
	/// <param name="item2">The value of the second element.</param>
	/// <param name="item3">The value of the third element.</param>
	/// <param name="item4">The value of the fourth element.</param>
	/// <param name="item5">The value of the fifth element.</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	/// <typeparam name="T3">The type of the third element.</typeparam>
	/// <typeparam name="T4">The type of the fourth element.</typeparam>
	/// <typeparam name="T5">The type of the fifth element.</typeparam>
	public static void Deconstruct<T1, T2, T3, T4, T5>(this Tuple<T1, T2, T3, T4, T5> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5)
	{
		item1 = value.Item1;
		item2 = value.Item2;
		item3 = value.Item3;
		item4 = value.Item4;
		item5 = value.Item5;
	}

	/// <summary>Deconstructs a tuple with 6 elements into separate variables.</summary>
	/// <param name="value">The 6-element tuple to deconstruct into 6 separate variables.</param>
	/// <param name="item1">The value of the first element.</param>
	/// <param name="item2">The value of the second element.</param>
	/// <param name="item3">The value of the third element.</param>
	/// <param name="item4">The value of the fourth element.</param>
	/// <param name="item5">The value of the fifth element.</param>
	/// <param name="item6">The value of the sixth element.</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	/// <typeparam name="T3">The type of the third element.</typeparam>
	/// <typeparam name="T4">The type of the fourth element.</typeparam>
	/// <typeparam name="T5">The type of the fifth element.</typeparam>
	/// <typeparam name="T6">The type of the sixth element.</typeparam>
	public static void Deconstruct<T1, T2, T3, T4, T5, T6>(this Tuple<T1, T2, T3, T4, T5, T6> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6)
	{
		item1 = value.Item1;
		item2 = value.Item2;
		item3 = value.Item3;
		item4 = value.Item4;
		item5 = value.Item5;
		item6 = value.Item6;
	}

	/// <summary>Deconstructs a tuple with 7 elements into separate variables.</summary>
	/// <param name="value">The 7-element tuple to deconstruct into 7 separate variables.</param>
	/// <param name="item1">The value of the first element.</param>
	/// <param name="item2">The value of the second element.</param>
	/// <param name="item3">The value of the third element.</param>
	/// <param name="item4">The value of the fourth element.</param>
	/// <param name="item5">The value of the fifth element.</param>
	/// <param name="item6">The value of the sixth element.</param>
	/// <param name="item7">The value of the seventh element.</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	/// <typeparam name="T3">The type of the third element.</typeparam>
	/// <typeparam name="T4">The type of the fourth element.</typeparam>
	/// <typeparam name="T5">The type of the fifth element.</typeparam>
	/// <typeparam name="T6">The type of the sixth element.</typeparam>
	/// <typeparam name="T7">The type of the seventh element.</typeparam>
	public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7>(this Tuple<T1, T2, T3, T4, T5, T6, T7> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7)
	{
		item1 = value.Item1;
		item2 = value.Item2;
		item3 = value.Item3;
		item4 = value.Item4;
		item5 = value.Item5;
		item6 = value.Item6;
		item7 = value.Item7;
	}

	/// <summary>Deconstructs a tuple with 8 elements into separate variables.</summary>
	/// <param name="value">The 8-element tuple to deconstruct into 8 separate variables.</param>
	/// <param name="item1">The value of the first element.</param>
	/// <param name="item2">The value of the second element.</param>
	/// <param name="item3">The value of the third element.</param>
	/// <param name="item4">The value of the fourth element.</param>
	/// <param name="item5">The value of the fifth element.</param>
	/// <param name="item6">The value of the sixth element.</param>
	/// <param name="item7">The value of the seventh element.</param>
	/// <param name="item8">The value of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	/// <typeparam name="T3">The type of the third element.</typeparam>
	/// <typeparam name="T4">The type of the fourth element.</typeparam>
	/// <typeparam name="T5">The type of the fifth element.</typeparam>
	/// <typeparam name="T6">The type of the sixth element.</typeparam>
	/// <typeparam name="T7">The type of the seventh element.</typeparam>
	/// <typeparam name="T8">The type of the eighth element.</typeparam>
	public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8)
	{
		item1 = value.Item1;
		item2 = value.Item2;
		item3 = value.Item3;
		item4 = value.Item4;
		item5 = value.Item5;
		item6 = value.Item6;
		item7 = value.Item7;
		item8 = value.Rest.Item1;
	}

	/// <summary>Deconstructs a tuple with 9 elements into separate variables.</summary>
	/// <param name="value">The 9-element tuple to deconstruct into 9 separate variables.</param>
	/// <param name="item1">The value of the first element.</param>
	/// <param name="item2">The value of the second element.</param>
	/// <param name="item3">The value of the third element.</param>
	/// <param name="item4">The value of the fourth element.</param>
	/// <param name="item5">The value of the fifth element.</param>
	/// <param name="item6">The value of the sixth element.</param>
	/// <param name="item7">The value of the seventh element.</param>
	/// <param name="item8">The value of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</param>
	/// <param name="item9">The value of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	/// <typeparam name="T3">The type of the third element.</typeparam>
	/// <typeparam name="T4">The type of the fourth element.</typeparam>
	/// <typeparam name="T5">The type of the fifth element.</typeparam>
	/// <typeparam name="T6">The type of the sixth element.</typeparam>
	/// <typeparam name="T7">The type of the seventh element.</typeparam>
	/// <typeparam name="T8">The type of the eighth element.</typeparam>
	/// <typeparam name="T9">The type of the ninth element.</typeparam>
	public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9)
	{
		item1 = value.Item1;
		item2 = value.Item2;
		item3 = value.Item3;
		item4 = value.Item4;
		item5 = value.Item5;
		item6 = value.Item6;
		item7 = value.Item7;
		item8 = value.Rest.Item1;
		item9 = value.Rest.Item2;
	}

	/// <summary>Deconstructs a tuple with 10 elements into separate variables.</summary>
	/// <param name="value">The 10-element tuple to deconstruct into 10 separate variables.</param>
	/// <param name="item1">The value of the first element.</param>
	/// <param name="item2">The value of the second element.</param>
	/// <param name="item3">The value of the third element.</param>
	/// <param name="item4">The value of the fourth element.</param>
	/// <param name="item5">The value of the fifth element.</param>
	/// <param name="item6">The value of the sixth element.</param>
	/// <param name="item7">The value of the seventh element.</param>
	/// <param name="item8">The value of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</param>
	/// <param name="item9">The value of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</param>
	/// <param name="item10">The value of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	/// <typeparam name="T3">The type of the third element.</typeparam>
	/// <typeparam name="T4">The type of the fourth element.</typeparam>
	/// <typeparam name="T5">The type of the fifth element.</typeparam>
	/// <typeparam name="T6">The type of the sixth element.</typeparam>
	/// <typeparam name="T7">The type of the seventh element.</typeparam>
	/// <typeparam name="T8">The type of the eighth element.</typeparam>
	/// <typeparam name="T9">The type of the ninth element.</typeparam>
	/// <typeparam name="T10">The type of the tenth element.</typeparam>
	public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10)
	{
		item1 = value.Item1;
		item2 = value.Item2;
		item3 = value.Item3;
		item4 = value.Item4;
		item5 = value.Item5;
		item6 = value.Item6;
		item7 = value.Item7;
		item8 = value.Rest.Item1;
		item9 = value.Rest.Item2;
		item10 = value.Rest.Item3;
	}

	/// <summary>Deconstructs a tuple with 11 elements into separate variables.</summary>
	/// <param name="value">The 11-element tuple to deconstruct into 11 separate variables.</param>
	/// <param name="item1">The value of the first element.</param>
	/// <param name="item2">The value of the second element.</param>
	/// <param name="item3">The value of the third element.</param>
	/// <param name="item4">The value of the fourth element.</param>
	/// <param name="item5">The value of the fifth element.</param>
	/// <param name="item6">The value of the sixth element.</param>
	/// <param name="item7">The value of the seventh element.</param>
	/// <param name="item8">The value of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</param>
	/// <param name="item9">The value of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</param>
	/// <param name="item10">The value of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</param>
	/// <param name="item11">The value of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	/// <typeparam name="T3">The type of the third element.</typeparam>
	/// <typeparam name="T4">The type of the fourth element.</typeparam>
	/// <typeparam name="T5">The type of the fifth element.</typeparam>
	/// <typeparam name="T6">The type of the sixth element.</typeparam>
	/// <typeparam name="T7">The type of the seventh element.</typeparam>
	/// <typeparam name="T8">The type of the eighth element.</typeparam>
	/// <typeparam name="T9">The type of the ninth element.</typeparam>
	/// <typeparam name="T10">The type of the tenth element.</typeparam>
	/// <typeparam name="T11">The type of the eleventh element.</typeparam>
	public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11)
	{
		item1 = value.Item1;
		item2 = value.Item2;
		item3 = value.Item3;
		item4 = value.Item4;
		item5 = value.Item5;
		item6 = value.Item6;
		item7 = value.Item7;
		item8 = value.Rest.Item1;
		item9 = value.Rest.Item2;
		item10 = value.Rest.Item3;
		item11 = value.Rest.Item4;
	}

	/// <summary>Deconstructs a tuple with 12 elements into separate variables.</summary>
	/// <param name="value">The 12-element tuple to deconstruct into 12 separate variables.</param>
	/// <param name="item1">The value of the first element.</param>
	/// <param name="item2">The value of the second element.</param>
	/// <param name="item3">The value of the third element.</param>
	/// <param name="item4">The value of the fourth element.</param>
	/// <param name="item5">The value of the fifth element.</param>
	/// <param name="item6">The value of the sixth element.</param>
	/// <param name="item7">The value of the seventh element.</param>
	/// <param name="item8">The value of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</param>
	/// <param name="item9">The value of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</param>
	/// <param name="item10">The value of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</param>
	/// <param name="item11">The value of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</param>
	/// <param name="item12">The value of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	/// <typeparam name="T3">The type of the third element.</typeparam>
	/// <typeparam name="T4">The type of the fourth element.</typeparam>
	/// <typeparam name="T5">The type of the fifth element.</typeparam>
	/// <typeparam name="T6">The type of the sixth element.</typeparam>
	/// <typeparam name="T7">The type of the seventh element.</typeparam>
	/// <typeparam name="T8">The type of the eighth element.</typeparam>
	/// <typeparam name="T9">The type of the ninth element.</typeparam>
	/// <typeparam name="T10">The type of the tenth element.</typeparam>
	/// <typeparam name="T11">The type of the eleventh element.</typeparam>
	/// <typeparam name="T12">The type of the twelfth element.</typeparam>
	public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12)
	{
		item1 = value.Item1;
		item2 = value.Item2;
		item3 = value.Item3;
		item4 = value.Item4;
		item5 = value.Item5;
		item6 = value.Item6;
		item7 = value.Item7;
		item8 = value.Rest.Item1;
		item9 = value.Rest.Item2;
		item10 = value.Rest.Item3;
		item11 = value.Rest.Item4;
		item12 = value.Rest.Item5;
	}

	/// <summary>Deconstructs a tuple with 13 elements into separate variables.</summary>
	/// <param name="value">The 13-element tuple to deconstruct into 13 separate variables.</param>
	/// <param name="item1">The value of the first element.</param>
	/// <param name="item2">The value of the second element.</param>
	/// <param name="item3">The value of the third element.</param>
	/// <param name="item4">The value of the fourth element.</param>
	/// <param name="item5">The value of the fifth element.</param>
	/// <param name="item6">The value of the sixth element.</param>
	/// <param name="item7">The value of the seventh element.</param>
	/// <param name="item8">The value of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</param>
	/// <param name="item9">The value of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</param>
	/// <param name="item10">The value of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</param>
	/// <param name="item11">The value of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</param>
	/// <param name="item12">The value of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</param>
	/// <param name="item13">The value of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	/// <typeparam name="T3">The type of the third element.</typeparam>
	/// <typeparam name="T4">The type of the fourth element.</typeparam>
	/// <typeparam name="T5">The type of the fifth element.</typeparam>
	/// <typeparam name="T6">The type of the sixth element.</typeparam>
	/// <typeparam name="T7">The type of the seventh element.</typeparam>
	/// <typeparam name="T8">The type of the eighth element.</typeparam>
	/// <typeparam name="T9">The type of the ninth element.</typeparam>
	/// <typeparam name="T10">The type of the tenth element.</typeparam>
	/// <typeparam name="T11">The type of the eleventh element.</typeparam>
	/// <typeparam name="T12">The type of the twelfth element.</typeparam>
	/// <typeparam name="T13">The type of the thirteenth element.</typeparam>
	public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13)
	{
		item1 = value.Item1;
		item2 = value.Item2;
		item3 = value.Item3;
		item4 = value.Item4;
		item5 = value.Item5;
		item6 = value.Item6;
		item7 = value.Item7;
		item8 = value.Rest.Item1;
		item9 = value.Rest.Item2;
		item10 = value.Rest.Item3;
		item11 = value.Rest.Item4;
		item12 = value.Rest.Item5;
		item13 = value.Rest.Item6;
	}

	/// <summary>Deconstructs a tuple with 14 elements into separate variables.</summary>
	/// <param name="value">The 14-element tuple to deconstruct into 14 separate variables.</param>
	/// <param name="item1">The value of the first element.</param>
	/// <param name="item2">The value of the second element.</param>
	/// <param name="item3">The value of the third element.</param>
	/// <param name="item4">The value of the fourth element.</param>
	/// <param name="item5">The value of the fifth element.</param>
	/// <param name="item6">The value of the sixth element.</param>
	/// <param name="item7">The value of the seventh element.</param>
	/// <param name="item8">The value of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</param>
	/// <param name="item9">The value of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</param>
	/// <param name="item10">The value of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</param>
	/// <param name="item11">The value of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</param>
	/// <param name="item12">The value of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</param>
	/// <param name="item13">The value of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</param>
	/// <param name="item14">The value of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	/// <typeparam name="T3">The type of the third element.</typeparam>
	/// <typeparam name="T4">The type of the fourth element.</typeparam>
	/// <typeparam name="T5">The type of the fifth element.</typeparam>
	/// <typeparam name="T6">The type of the sixth element.</typeparam>
	/// <typeparam name="T7">The type of the seventh element.</typeparam>
	/// <typeparam name="T8">The type of the eighth element.</typeparam>
	/// <typeparam name="T9">The type of the ninth element.</typeparam>
	/// <typeparam name="T10">The type of the tenth element.</typeparam>
	/// <typeparam name="T11">The type of the eleventh element.</typeparam>
	/// <typeparam name="T12">The type of the twelfth element.</typeparam>
	/// <typeparam name="T13">The type of the thirteenth element.</typeparam>
	/// <typeparam name="T14">The type of the fourteenth element.</typeparam>
	public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14)
	{
		item1 = value.Item1;
		item2 = value.Item2;
		item3 = value.Item3;
		item4 = value.Item4;
		item5 = value.Item5;
		item6 = value.Item6;
		item7 = value.Item7;
		item8 = value.Rest.Item1;
		item9 = value.Rest.Item2;
		item10 = value.Rest.Item3;
		item11 = value.Rest.Item4;
		item12 = value.Rest.Item5;
		item13 = value.Rest.Item6;
		item14 = value.Rest.Item7;
	}

	/// <summary>Deconstructs a tuple with 15 elements into separate variables.</summary>
	/// <param name="value">The 15-element tuple to deconstruct into 15 separate variables.</param>
	/// <param name="item1">The value of the first element.</param>
	/// <param name="item2">The value of the second element.</param>
	/// <param name="item3">The value of the third element.</param>
	/// <param name="item4">The value of the fourth element.</param>
	/// <param name="item5">The value of the fifth element.</param>
	/// <param name="item6">The value of the sixth element.</param>
	/// <param name="item7">The value of the seventh element.</param>
	/// <param name="item8">The value of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</param>
	/// <param name="item9">The value of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</param>
	/// <param name="item10">The value of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</param>
	/// <param name="item11">The value of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</param>
	/// <param name="item12">The value of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</param>
	/// <param name="item13">The value of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</param>
	/// <param name="item14">The value of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</param>
	/// <param name="item15">The value of the fifteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item1" />.</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	/// <typeparam name="T3">The type of the third element.</typeparam>
	/// <typeparam name="T4">The type of the fourth element.</typeparam>
	/// <typeparam name="T5">The type of the fifth element.</typeparam>
	/// <typeparam name="T6">The type of the sixth element.</typeparam>
	/// <typeparam name="T7">The type of the seventh element.</typeparam>
	/// <typeparam name="T8">The type of the eighth element.</typeparam>
	/// <typeparam name="T9">The type of the ninth element.</typeparam>
	/// <typeparam name="T10">The type of the tenth element.</typeparam>
	/// <typeparam name="T11">The type of the eleventh element.</typeparam>
	/// <typeparam name="T12">The type of the twelfth element.</typeparam>
	/// <typeparam name="T13">The type of the thirteenth element.</typeparam>
	/// <typeparam name="T14">The type of the fourteenth element.</typeparam>
	/// <typeparam name="T15">The type of the fifteenth element.</typeparam>
	public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15>>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14, out T15 item15)
	{
		item1 = value.Item1;
		item2 = value.Item2;
		item3 = value.Item3;
		item4 = value.Item4;
		item5 = value.Item5;
		item6 = value.Item6;
		item7 = value.Item7;
		item8 = value.Rest.Item1;
		item9 = value.Rest.Item2;
		item10 = value.Rest.Item3;
		item11 = value.Rest.Item4;
		item12 = value.Rest.Item5;
		item13 = value.Rest.Item6;
		item14 = value.Rest.Item7;
		item15 = value.Rest.Rest.Item1;
	}

	/// <summary>Deconstructs a tuple with 16 elements into separate variables.</summary>
	/// <param name="value">The 16-element tuple to deconstruct into 16 separate variables.</param>
	/// <param name="item1">The value of the first element.</param>
	/// <param name="item2">The value of the second element.</param>
	/// <param name="item3">The value of the third element.</param>
	/// <param name="item4">The value of the fourth element.</param>
	/// <param name="item5">The value of the fifth element.</param>
	/// <param name="item6">The value of the sixth element.</param>
	/// <param name="item7">The value of the seventh element.</param>
	/// <param name="item8">The value of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</param>
	/// <param name="item9">The value of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</param>
	/// <param name="item10">The value of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</param>
	/// <param name="item11">The value of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</param>
	/// <param name="item12">The value of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</param>
	/// <param name="item13">The value of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</param>
	/// <param name="item14">The value of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</param>
	/// <param name="item15">The value of the fifteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item1" />.</param>
	/// <param name="item16">The value of the sixteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item2" />.</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	/// <typeparam name="T3">The type of the third element.</typeparam>
	/// <typeparam name="T4">The type of the fourth element.</typeparam>
	/// <typeparam name="T5">The type of the fifth element.</typeparam>
	/// <typeparam name="T6">The type of the sixth element.</typeparam>
	/// <typeparam name="T7">The type of the seventh element.</typeparam>
	/// <typeparam name="T8">The type of the eighth element.</typeparam>
	/// <typeparam name="T9">The type of the ninth element.</typeparam>
	/// <typeparam name="T10">The type of the tenth element.</typeparam>
	/// <typeparam name="T11">The type of the eleventh element.</typeparam>
	/// <typeparam name="T12">The type of the twelfth element.</typeparam>
	/// <typeparam name="T13">The type of the thirteenth element.</typeparam>
	/// <typeparam name="T14">The type of the fourteenth element.</typeparam>
	/// <typeparam name="T15">The type of the fifteenth element.</typeparam>
	/// <typeparam name="T16">The type of the sixteenth element.</typeparam>
	public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16>>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14, out T15 item15, out T16 item16)
	{
		item1 = value.Item1;
		item2 = value.Item2;
		item3 = value.Item3;
		item4 = value.Item4;
		item5 = value.Item5;
		item6 = value.Item6;
		item7 = value.Item7;
		item8 = value.Rest.Item1;
		item9 = value.Rest.Item2;
		item10 = value.Rest.Item3;
		item11 = value.Rest.Item4;
		item12 = value.Rest.Item5;
		item13 = value.Rest.Item6;
		item14 = value.Rest.Item7;
		item15 = value.Rest.Rest.Item1;
		item16 = value.Rest.Rest.Item2;
	}

	/// <summary>Deconstructs a tuple with 17 elements into separate variables.</summary>
	/// <param name="value">The 17-element tuple to deconstruct into 17 separate variables.</param>
	/// <param name="item1">The value of the first element.</param>
	/// <param name="item2">The value of the second element.</param>
	/// <param name="item3">The value of the third element.</param>
	/// <param name="item4">The value of the fourth element.</param>
	/// <param name="item5">The value of the fifth element.</param>
	/// <param name="item6">The value of the sixth element.</param>
	/// <param name="item7">The value of the seventh element.</param>
	/// <param name="item8">The value of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</param>
	/// <param name="item9">The value of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</param>
	/// <param name="item10">The value of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</param>
	/// <param name="item11">The value of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</param>
	/// <param name="item12">The value of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</param>
	/// <param name="item13">The value of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</param>
	/// <param name="item14">The value of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</param>
	/// <param name="item15">The value of the fifteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item1" />.</param>
	/// <param name="item16">The value of the sixteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item2" />.</param>
	/// <param name="item17">The value of the seventeenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item3" />.</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	/// <typeparam name="T3">The type of the third element.</typeparam>
	/// <typeparam name="T4">The type of the fourth element.</typeparam>
	/// <typeparam name="T5">The type of the fifth element.</typeparam>
	/// <typeparam name="T6">The type of the sixth element.</typeparam>
	/// <typeparam name="T7">The type of the seventh element.</typeparam>
	/// <typeparam name="T8">The type of the eighth element.</typeparam>
	/// <typeparam name="T9">The type of the ninth element.</typeparam>
	/// <typeparam name="T10">The type of the tenth element.</typeparam>
	/// <typeparam name="T11">The type of the eleventh element.</typeparam>
	/// <typeparam name="T12">The type of the twelfth element.</typeparam>
	/// <typeparam name="T13">The type of the thirteenth element.</typeparam>
	/// <typeparam name="T14">The type of the fourteenth element.</typeparam>
	/// <typeparam name="T15">The type of the fifteenth element.</typeparam>
	/// <typeparam name="T16">The type of the sixteenth element.</typeparam>
	/// <typeparam name="T17">The type of the seventeenth element.</typeparam>
	public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17>>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14, out T15 item15, out T16 item16, out T17 item17)
	{
		item1 = value.Item1;
		item2 = value.Item2;
		item3 = value.Item3;
		item4 = value.Item4;
		item5 = value.Item5;
		item6 = value.Item6;
		item7 = value.Item7;
		item8 = value.Rest.Item1;
		item9 = value.Rest.Item2;
		item10 = value.Rest.Item3;
		item11 = value.Rest.Item4;
		item12 = value.Rest.Item5;
		item13 = value.Rest.Item6;
		item14 = value.Rest.Item7;
		item15 = value.Rest.Rest.Item1;
		item16 = value.Rest.Rest.Item2;
		item17 = value.Rest.Rest.Item3;
	}

	/// <summary>Deconstructs a tuple with 18 elements into separate variables.</summary>
	/// <param name="value">The 18-element tuple to deconstruct into 18 separate variables.</param>
	/// <param name="item1">The value of the first element.</param>
	/// <param name="item2">The value of the second element.</param>
	/// <param name="item3">The value of the third element.</param>
	/// <param name="item4">The value of the fourth element.</param>
	/// <param name="item5">The value of the fifth element.</param>
	/// <param name="item6">The value of the sixth element.</param>
	/// <param name="item7">The value of the seventh element.</param>
	/// <param name="item8">The value of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</param>
	/// <param name="item9">The value of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</param>
	/// <param name="item10">The value of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</param>
	/// <param name="item11">The value of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</param>
	/// <param name="item12">The value of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</param>
	/// <param name="item13">The value of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</param>
	/// <param name="item14">The value of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</param>
	/// <param name="item15">The value of the fifteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item1" />.</param>
	/// <param name="item16">The value of the sixteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item2" />.</param>
	/// <param name="item17">The value of the seventeenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item3" />.</param>
	/// <param name="item18">The value of the eighteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item4" />.</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	/// <typeparam name="T3">The type of the third element.</typeparam>
	/// <typeparam name="T4">The type of the fourth element.</typeparam>
	/// <typeparam name="T5">The type of the fifth element.</typeparam>
	/// <typeparam name="T6">The type of the sixth element.</typeparam>
	/// <typeparam name="T7">The type of the seventh element.</typeparam>
	/// <typeparam name="T8">The type of the eighth element.</typeparam>
	/// <typeparam name="T9">The type of the ninth element.</typeparam>
	/// <typeparam name="T10">The type of the tenth element.</typeparam>
	/// <typeparam name="T11">The type of the eleventh element.</typeparam>
	/// <typeparam name="T12">The type of the twelfth element.</typeparam>
	/// <typeparam name="T13">The type of the thirteenth element.</typeparam>
	/// <typeparam name="T14">The type of the fourteenth element.</typeparam>
	/// <typeparam name="T15">The type of the fifteenth element.</typeparam>
	/// <typeparam name="T16">The type of the sixteenth element.</typeparam>
	/// <typeparam name="T17">The type of the seventeenth element.</typeparam>
	/// <typeparam name="T18">The type of the eighteenth element.</typeparam>
	public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18>>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14, out T15 item15, out T16 item16, out T17 item17, out T18 item18)
	{
		item1 = value.Item1;
		item2 = value.Item2;
		item3 = value.Item3;
		item4 = value.Item4;
		item5 = value.Item5;
		item6 = value.Item6;
		item7 = value.Item7;
		item8 = value.Rest.Item1;
		item9 = value.Rest.Item2;
		item10 = value.Rest.Item3;
		item11 = value.Rest.Item4;
		item12 = value.Rest.Item5;
		item13 = value.Rest.Item6;
		item14 = value.Rest.Item7;
		item15 = value.Rest.Rest.Item1;
		item16 = value.Rest.Rest.Item2;
		item17 = value.Rest.Rest.Item3;
		item18 = value.Rest.Rest.Item4;
	}

	/// <summary>Deconstructs a tuple with 19 elements into separate variables.</summary>
	/// <param name="value">The 19-element tuple to deconstruct into 19 separate variables.</param>
	/// <param name="item1">The value of the first element.</param>
	/// <param name="item2">The value of the second element.</param>
	/// <param name="item3">The value of the third element.</param>
	/// <param name="item4">The value of the fourth element.</param>
	/// <param name="item5">The value of the fifth element.</param>
	/// <param name="item6">The value of the sixth element.</param>
	/// <param name="item7">The value of the seventh element.</param>
	/// <param name="item8">The value of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</param>
	/// <param name="item9">The value of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</param>
	/// <param name="item10">The value of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</param>
	/// <param name="item11">The value of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</param>
	/// <param name="item12">The value of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</param>
	/// <param name="item13">The value of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</param>
	/// <param name="item14">The value of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</param>
	/// <param name="item15">The value of the fifteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item1" />.</param>
	/// <param name="item16">The value of the sixteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item2" />.</param>
	/// <param name="item17">The value of the seventeenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item3" />.</param>
	/// <param name="item18">The value of the eighteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item4" />.</param>
	/// <param name="item19">The value of the nineteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item5" />.</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	/// <typeparam name="T3">The type of the third element.</typeparam>
	/// <typeparam name="T4">The type of the fourth element.</typeparam>
	/// <typeparam name="T5">The type of the fifth element.</typeparam>
	/// <typeparam name="T6">The type of the sixth element.</typeparam>
	/// <typeparam name="T7">The type of the seventh element.</typeparam>
	/// <typeparam name="T8">The type of the eighth element.</typeparam>
	/// <typeparam name="T9">The type of the ninth element.</typeparam>
	/// <typeparam name="T10">The type of the tenth element.</typeparam>
	/// <typeparam name="T11">The type of the eleventh element.</typeparam>
	/// <typeparam name="T12">The type of the twelfth element.</typeparam>
	/// <typeparam name="T13">The type of the thirteenth element.</typeparam>
	/// <typeparam name="T14">The type of the fourteenth element.</typeparam>
	/// <typeparam name="T15">The type of the fifteenth element.</typeparam>
	/// <typeparam name="T16">The type of the sixteenth element.</typeparam>
	/// <typeparam name="T17">The type of the seventeenth element.</typeparam>
	/// <typeparam name="T18">The type of the eighteenth element.</typeparam>
	/// <typeparam name="T19">The type of the nineteenth element.</typeparam>
	public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19>>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14, out T15 item15, out T16 item16, out T17 item17, out T18 item18, out T19 item19)
	{
		item1 = value.Item1;
		item2 = value.Item2;
		item3 = value.Item3;
		item4 = value.Item4;
		item5 = value.Item5;
		item6 = value.Item6;
		item7 = value.Item7;
		item8 = value.Rest.Item1;
		item9 = value.Rest.Item2;
		item10 = value.Rest.Item3;
		item11 = value.Rest.Item4;
		item12 = value.Rest.Item5;
		item13 = value.Rest.Item6;
		item14 = value.Rest.Item7;
		item15 = value.Rest.Rest.Item1;
		item16 = value.Rest.Rest.Item2;
		item17 = value.Rest.Rest.Item3;
		item18 = value.Rest.Rest.Item4;
		item19 = value.Rest.Rest.Item5;
	}

	/// <summary>Deconstructs a tuple with 20 elements into separate variables.</summary>
	/// <param name="value">The 20-element tuple to deconstruct into 20 separate variables.</param>
	/// <param name="item1">The value of the first element.</param>
	/// <param name="item2">The value of the second element.</param>
	/// <param name="item3">The value of the third element.</param>
	/// <param name="item4">The value of the fourth element.</param>
	/// <param name="item5">The value of the fifth element.</param>
	/// <param name="item6">The value of the sixth element.</param>
	/// <param name="item7">The value of the seventh element.</param>
	/// <param name="item8">The value of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</param>
	/// <param name="item9">The value of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</param>
	/// <param name="item10">The value of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</param>
	/// <param name="item11">The value of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</param>
	/// <param name="item12">The value of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</param>
	/// <param name="item13">The value of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</param>
	/// <param name="item14">The value of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</param>
	/// <param name="item15">The value of the fifteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item1" />.</param>
	/// <param name="item16">The value of the sixteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item2" />.</param>
	/// <param name="item17">The value of the seventeenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item3" />.</param>
	/// <param name="item18">The value of the eighteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item4" />.</param>
	/// <param name="item19">The value of the nineteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item5" />.</param>
	/// <param name="item20">The value of the twentieth element, or <paramref name="value" /><see langword=".Rest.Rest.Item6" />.</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	/// <typeparam name="T3">The type of the third element.</typeparam>
	/// <typeparam name="T4">The type of the fourth element.</typeparam>
	/// <typeparam name="T5">The type of the fifth element.</typeparam>
	/// <typeparam name="T6">The type of the sixth element.</typeparam>
	/// <typeparam name="T7">The type of the seventh element.</typeparam>
	/// <typeparam name="T8">The type of the eighth element.</typeparam>
	/// <typeparam name="T9">The type of the ninth element.</typeparam>
	/// <typeparam name="T10">The type of the tenth element.</typeparam>
	/// <typeparam name="T11">The type of the eleventh element.</typeparam>
	/// <typeparam name="T12">The type of the twelfth element.</typeparam>
	/// <typeparam name="T13">The type of the thirteenth element.</typeparam>
	/// <typeparam name="T14">The type of the fourteenth element.</typeparam>
	/// <typeparam name="T15">The type of the fifteenth element.</typeparam>
	/// <typeparam name="T16">The type of the sixteenth element.</typeparam>
	/// <typeparam name="T17">The type of the seventeenth element.</typeparam>
	/// <typeparam name="T18">The type of the eighteenth element.</typeparam>
	/// <typeparam name="T19">The type of the nineteenth element.</typeparam>
	/// <typeparam name="T20">The type of the twentieth element.</typeparam>
	public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20>>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14, out T15 item15, out T16 item16, out T17 item17, out T18 item18, out T19 item19, out T20 item20)
	{
		item1 = value.Item1;
		item2 = value.Item2;
		item3 = value.Item3;
		item4 = value.Item4;
		item5 = value.Item5;
		item6 = value.Item6;
		item7 = value.Item7;
		item8 = value.Rest.Item1;
		item9 = value.Rest.Item2;
		item10 = value.Rest.Item3;
		item11 = value.Rest.Item4;
		item12 = value.Rest.Item5;
		item13 = value.Rest.Item6;
		item14 = value.Rest.Item7;
		item15 = value.Rest.Rest.Item1;
		item16 = value.Rest.Rest.Item2;
		item17 = value.Rest.Rest.Item3;
		item18 = value.Rest.Rest.Item4;
		item19 = value.Rest.Rest.Item5;
		item20 = value.Rest.Rest.Item6;
	}

	/// <summary>Deconstructs a tuple with 21 elements into separate variables.</summary>
	/// <param name="value">The 21-element tuple to deconstruct into 21 separate variables.</param>
	/// <param name="item1">The value of the first element.</param>
	/// <param name="item2">The value of the second element.</param>
	/// <param name="item3">The value of the third element.</param>
	/// <param name="item4">The value of the fourth element.</param>
	/// <param name="item5">The value of the fifth element.</param>
	/// <param name="item6">The value of the sixth element.</param>
	/// <param name="item7">The value of the seventh element.</param>
	/// <param name="item8">The value of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</param>
	/// <param name="item9">The value of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</param>
	/// <param name="item10">The value of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</param>
	/// <param name="item11">The value of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</param>
	/// <param name="item12">The value of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</param>
	/// <param name="item13">The value of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</param>
	/// <param name="item14">The value of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</param>
	/// <param name="item15">The value of the fifteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item1" />.</param>
	/// <param name="item16">The value of the sixteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item2" />.</param>
	/// <param name="item17">The value of the seventeenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item3" />.</param>
	/// <param name="item18">The value of the eighteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item4" />.</param>
	/// <param name="item19">The value of the nineteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item5" />.</param>
	/// <param name="item20">The value of the twentieth element, or <paramref name="value" /><see langword=".Rest.Rest.Item6" />.</param>
	/// <param name="item21">The value of the twenty-first element, or <paramref name="value" /><see langword=".Rest.Rest.Item7" />.</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	/// <typeparam name="T3">The type of the third element.</typeparam>
	/// <typeparam name="T4">The type of the fourth element.</typeparam>
	/// <typeparam name="T5">The type of the fifth element.</typeparam>
	/// <typeparam name="T6">The type of the sixth element.</typeparam>
	/// <typeparam name="T7">The type of the seventh element.</typeparam>
	/// <typeparam name="T8">The type of the eighth element.</typeparam>
	/// <typeparam name="T9">The type of the ninth element.</typeparam>
	/// <typeparam name="T10">The type of the tenth element.</typeparam>
	/// <typeparam name="T11">The type of the eleventh element.</typeparam>
	/// <typeparam name="T12">The type of the twelfth element.</typeparam>
	/// <typeparam name="T13">The type of the thirteenth element.</typeparam>
	/// <typeparam name="T14">The type of the fourteenth element.</typeparam>
	/// <typeparam name="T15">The type of the fifteenth element.</typeparam>
	/// <typeparam name="T16">The type of the sixteenth element.</typeparam>
	/// <typeparam name="T17">The type of the seventeenth element.</typeparam>
	/// <typeparam name="T18">The type of the eighteenth element.</typeparam>
	/// <typeparam name="T19">The type of the nineteenth element.</typeparam>
	/// <typeparam name="T20">The type of the twentieth element.</typeparam>
	/// <typeparam name="T21">The type of the twenty-first element.</typeparam>
	public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20, T21>>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14, out T15 item15, out T16 item16, out T17 item17, out T18 item18, out T19 item19, out T20 item20, out T21 item21)
	{
		item1 = value.Item1;
		item2 = value.Item2;
		item3 = value.Item3;
		item4 = value.Item4;
		item5 = value.Item5;
		item6 = value.Item6;
		item7 = value.Item7;
		item8 = value.Rest.Item1;
		item9 = value.Rest.Item2;
		item10 = value.Rest.Item3;
		item11 = value.Rest.Item4;
		item12 = value.Rest.Item5;
		item13 = value.Rest.Item6;
		item14 = value.Rest.Item7;
		item15 = value.Rest.Rest.Item1;
		item16 = value.Rest.Rest.Item2;
		item17 = value.Rest.Rest.Item3;
		item18 = value.Rest.Rest.Item4;
		item19 = value.Rest.Rest.Item5;
		item20 = value.Rest.Rest.Item6;
		item21 = value.Rest.Rest.Item7;
	}

	/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
	/// <param name="value">The tuple object to convert to a value tuple</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <returns>The converted value tuple instance.</returns>
	public static ValueTuple<T1> ToValueTuple<T1>(this Tuple<T1> value)
	{
		return ValueTuple.Create(value.Item1);
	}

	/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
	/// <param name="value">The tuple object to convert to a value tuple</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	/// <returns>The converted value tuple instance.</returns>
	public static (T1, T2) ToValueTuple<T1, T2>(this Tuple<T1, T2> value)
	{
		return ValueTuple.Create(value.Item1, value.Item2);
	}

	/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
	/// <param name="value">The tuple object to convert to a value tuple</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	/// <typeparam name="T3">The type of the third element.</typeparam>
	/// <returns>The converted value tuple instance.</returns>
	public static (T1, T2, T3) ToValueTuple<T1, T2, T3>(this Tuple<T1, T2, T3> value)
	{
		return ValueTuple.Create(value.Item1, value.Item2, value.Item3);
	}

	/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
	/// <param name="value">The tuple object to convert to a value tuple</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	/// <typeparam name="T3">The type of the third element.</typeparam>
	/// <typeparam name="T4">The type of the fourth element.</typeparam>
	/// <returns>The converted value tuple instance.</returns>
	public static (T1, T2, T3, T4) ToValueTuple<T1, T2, T3, T4>(this Tuple<T1, T2, T3, T4> value)
	{
		return ValueTuple.Create(value.Item1, value.Item2, value.Item3, value.Item4);
	}

	/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
	/// <param name="value">The tuple object to convert to a value tuple</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	/// <typeparam name="T3">The type of the third element.</typeparam>
	/// <typeparam name="T4">The type of the fourth element.</typeparam>
	/// <typeparam name="T5">The type of the fifth element.</typeparam>
	/// <returns>The converted value tuple instance.</returns>
	public static (T1, T2, T3, T4, T5) ToValueTuple<T1, T2, T3, T4, T5>(this Tuple<T1, T2, T3, T4, T5> value)
	{
		return ValueTuple.Create(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5);
	}

	/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
	/// <param name="value">The tuple object to convert to a value tuple</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	/// <typeparam name="T3">The type of the third element.</typeparam>
	/// <typeparam name="T4">The type of the fourth element.</typeparam>
	/// <typeparam name="T5">The type of the fifth element.</typeparam>
	/// <typeparam name="T6">The type of the sixth element.</typeparam>
	/// <returns>The converted value tuple instance.</returns>
	public static (T1, T2, T3, T4, T5, T6) ToValueTuple<T1, T2, T3, T4, T5, T6>(this Tuple<T1, T2, T3, T4, T5, T6> value)
	{
		return ValueTuple.Create(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6);
	}

	/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
	/// <param name="value">The tuple object to convert to a value tuple</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	/// <typeparam name="T3">The type of the third element.</typeparam>
	/// <typeparam name="T4">The type of the fourth element.</typeparam>
	/// <typeparam name="T5">The type of the fifth element.</typeparam>
	/// <typeparam name="T6">The type of the sixth element.</typeparam>
	/// <typeparam name="T7">The type of the seventh element.</typeparam>
	/// <returns>The converted value tuple instance.</returns>
	public static (T1, T2, T3, T4, T5, T6, T7) ToValueTuple<T1, T2, T3, T4, T5, T6, T7>(this Tuple<T1, T2, T3, T4, T5, T6, T7> value)
	{
		return ValueTuple.Create(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7);
	}

	/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
	/// <param name="value">The tuple object to convert to a value tuple</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	/// <typeparam name="T3">The type of the third element.</typeparam>
	/// <typeparam name="T4">The type of the fourth element.</typeparam>
	/// <typeparam name="T5">The type of the fifth element.</typeparam>
	/// <typeparam name="T6">The type of the sixth element.</typeparam>
	/// <typeparam name="T7">The type of the seventh element.</typeparam>
	/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
	/// <returns>The converted value tuple instance.</returns>
	public static (T1, T2, T3, T4, T5, T6, T7, T8) ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>> value)
	{
		return CreateLong(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, ValueTuple.Create(value.Rest.Item1));
	}

	/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
	/// <param name="value">The tuple object to convert to a value tuple</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	/// <typeparam name="T3">The type of the third element.</typeparam>
	/// <typeparam name="T4">The type of the fourth element.</typeparam>
	/// <typeparam name="T5">The type of the fifth element.</typeparam>
	/// <typeparam name="T6">The type of the sixth element.</typeparam>
	/// <typeparam name="T7">The type of the seventh element.</typeparam>
	/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
	/// <typeparam name="T9">The type of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</typeparam>
	/// <returns>The converted value tuple instance.</returns>
	public static (T1, T2, T3, T4, T5, T6, T7, T8, T9) ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9>> value)
	{
		return CreateLong<T1, T2, T3, T4, T5, T6, T7, (T8, T9)>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, ValueTuple.Create(value.Rest.Item1, value.Rest.Item2));
	}

	/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
	/// <param name="value">The tuple object to convert to a value tuple</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	/// <typeparam name="T3">The type of the third element.</typeparam>
	/// <typeparam name="T4">The type of the fourth element.</typeparam>
	/// <typeparam name="T5">The type of the fifth element.</typeparam>
	/// <typeparam name="T6">The type of the sixth element.</typeparam>
	/// <typeparam name="T7">The type of the seventh element.</typeparam>
	/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
	/// <typeparam name="T9">The type of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</typeparam>
	/// <typeparam name="T10">The type of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</typeparam>
	/// <returns>The converted value tuple instance.</returns>
	public static (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10) ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10>> value)
	{
		return CreateLong<T1, T2, T3, T4, T5, T6, T7, (T8, T9, T10)>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, ValueTuple.Create(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3));
	}

	/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
	/// <param name="value">The tuple object to convert to a value tuple</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	/// <typeparam name="T3">The type of the third element.</typeparam>
	/// <typeparam name="T4">The type of the fourth element.</typeparam>
	/// <typeparam name="T5">The type of the fifth element.</typeparam>
	/// <typeparam name="T6">The type of the sixth element.</typeparam>
	/// <typeparam name="T7">The type of the seventh element.</typeparam>
	/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
	/// <typeparam name="T9">The type of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</typeparam>
	/// <typeparam name="T10">The type of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</typeparam>
	/// <typeparam name="T11">The type of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</typeparam>
	/// <returns>The converted value tuple instance.</returns>
	public static (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11) ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11>> value)
	{
		return CreateLong<T1, T2, T3, T4, T5, T6, T7, (T8, T9, T10, T11)>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, ValueTuple.Create(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4));
	}

	/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
	/// <param name="value">The tuple object to convert to a value tuple</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	/// <typeparam name="T3">The type of the third element.</typeparam>
	/// <typeparam name="T4">The type of the fourth element.</typeparam>
	/// <typeparam name="T5">The type of the fifth element.</typeparam>
	/// <typeparam name="T6">The type of the sixth element.</typeparam>
	/// <typeparam name="T7">The type of the seventh element.</typeparam>
	/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
	/// <typeparam name="T9">The type of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</typeparam>
	/// <typeparam name="T10">The type of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</typeparam>
	/// <typeparam name="T11">The type of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</typeparam>
	/// <typeparam name="T12">The type of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</typeparam>
	/// <returns>The converted value tuple instance.</returns>
	public static (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12) ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12>> value)
	{
		return CreateLong<T1, T2, T3, T4, T5, T6, T7, (T8, T9, T10, T11, T12)>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, ValueTuple.Create(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5));
	}

	/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
	/// <param name="value">The tuple object to convert to a value tuple</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	/// <typeparam name="T3">The type of the third element.</typeparam>
	/// <typeparam name="T4">The type of the fourth element.</typeparam>
	/// <typeparam name="T5">The type of the fifth element.</typeparam>
	/// <typeparam name="T6">The type of the sixth element.</typeparam>
	/// <typeparam name="T7">The type of the seventh element.</typeparam>
	/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
	/// <typeparam name="T9">The type of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</typeparam>
	/// <typeparam name="T10">The type of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</typeparam>
	/// <typeparam name="T11">The type of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</typeparam>
	/// <typeparam name="T12">The type of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</typeparam>
	/// <typeparam name="T13">The type of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</typeparam>
	/// <returns>The converted value tuple instance.</returns>
	public static (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13) ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13>> value)
	{
		return CreateLong<T1, T2, T3, T4, T5, T6, T7, (T8, T9, T10, T11, T12, T13)>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, ValueTuple.Create(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6));
	}

	/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
	/// <param name="value">The tuple object to convert to a value tuple</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	/// <typeparam name="T3">The type of the third element.</typeparam>
	/// <typeparam name="T4">The type of the fourth element.</typeparam>
	/// <typeparam name="T5">The type of the fifth element.</typeparam>
	/// <typeparam name="T6">The type of the sixth element.</typeparam>
	/// <typeparam name="T7">The type of the seventh element.</typeparam>
	/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
	/// <typeparam name="T9">The type of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</typeparam>
	/// <typeparam name="T10">The type of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</typeparam>
	/// <typeparam name="T11">The type of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</typeparam>
	/// <typeparam name="T12">The type of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</typeparam>
	/// <typeparam name="T13">The type of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</typeparam>
	/// <typeparam name="T14">The type of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</typeparam>
	/// <returns>The converted value tuple instance.</returns>
	public static (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14) ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14>> value)
	{
		return CreateLong<T1, T2, T3, T4, T5, T6, T7, (T8, T9, T10, T11, T12, T13, T14)>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, ValueTuple.Create(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7));
	}

	/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
	/// <param name="value">The tuple object to convert to a value tuple</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	/// <typeparam name="T3">The type of the third element.</typeparam>
	/// <typeparam name="T4">The type of the fourth element.</typeparam>
	/// <typeparam name="T5">The type of the fifth element.</typeparam>
	/// <typeparam name="T6">The type of the sixth element.</typeparam>
	/// <typeparam name="T7">The type of the seventh element.</typeparam>
	/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
	/// <typeparam name="T9">The type of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</typeparam>
	/// <typeparam name="T10">The type of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</typeparam>
	/// <typeparam name="T11">The type of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</typeparam>
	/// <typeparam name="T12">The type of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</typeparam>
	/// <typeparam name="T13">The type of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</typeparam>
	/// <typeparam name="T14">The type of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</typeparam>
	/// <typeparam name="T15">The type of the fifteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item1" />.</typeparam>
	/// <returns>The converted value tuple instance.</returns>
	public static (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15) ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15>>> value)
	{
		return CreateLong(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, CreateLong(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, ValueTuple.Create(value.Rest.Rest.Item1)));
	}

	/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
	/// <param name="value">The tuple object to convert to a value tuple</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	/// <typeparam name="T3">The type of the third element.</typeparam>
	/// <typeparam name="T4">The type of the fourth element.</typeparam>
	/// <typeparam name="T5">The type of the fifth element.</typeparam>
	/// <typeparam name="T6">The type of the sixth element.</typeparam>
	/// <typeparam name="T7">The type of the seventh element.</typeparam>
	/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
	/// <typeparam name="T9">The type of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</typeparam>
	/// <typeparam name="T10">The type of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</typeparam>
	/// <typeparam name="T11">The type of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</typeparam>
	/// <typeparam name="T12">The type of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</typeparam>
	/// <typeparam name="T13">The type of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</typeparam>
	/// <typeparam name="T14">The type of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</typeparam>
	/// <typeparam name="T15">The type of the fifteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item1" />.</typeparam>
	/// <typeparam name="T16">The type of the sixteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item2" />.</typeparam>
	/// <returns>The converted value tuple instance.</returns>
	public static (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16) ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16>>> value)
	{
		return CreateLong(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, CreateLong<T8, T9, T10, T11, T12, T13, T14, (T15, T16)>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, ValueTuple.Create(value.Rest.Rest.Item1, value.Rest.Rest.Item2)));
	}

	/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
	/// <param name="value">The tuple object to convert to a value tuple</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	/// <typeparam name="T3">The type of the third element.</typeparam>
	/// <typeparam name="T4">The type of the fourth element.</typeparam>
	/// <typeparam name="T5">The type of the fifth element.</typeparam>
	/// <typeparam name="T6">The type of the sixth element.</typeparam>
	/// <typeparam name="T7">The type of the seventh element.</typeparam>
	/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
	/// <typeparam name="T9">The type of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</typeparam>
	/// <typeparam name="T10">The type of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</typeparam>
	/// <typeparam name="T11">The type of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</typeparam>
	/// <typeparam name="T12">The type of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</typeparam>
	/// <typeparam name="T13">The type of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</typeparam>
	/// <typeparam name="T14">The type of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</typeparam>
	/// <typeparam name="T15">The type of the fifteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item1" />.</typeparam>
	/// <typeparam name="T16">The type of the sixteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item2" />.</typeparam>
	/// <typeparam name="T17">The type of the seventeenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item3" />.</typeparam>
	/// <returns>The converted value tuple instance.</returns>
	public static (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17) ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17>>> value)
	{
		return CreateLong(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, CreateLong<T8, T9, T10, T11, T12, T13, T14, (T15, T16, T17)>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, ValueTuple.Create(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3)));
	}

	/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
	/// <param name="value">The tuple object to convert to a value tuple</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	/// <typeparam name="T3">The type of the third element.</typeparam>
	/// <typeparam name="T4">The type of the fourth element.</typeparam>
	/// <typeparam name="T5">The type of the fifth element.</typeparam>
	/// <typeparam name="T6">The type of the sixth element.</typeparam>
	/// <typeparam name="T7">The type of the seventh element.</typeparam>
	/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
	/// <typeparam name="T9">The type of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</typeparam>
	/// <typeparam name="T10">The type of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</typeparam>
	/// <typeparam name="T11">The type of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</typeparam>
	/// <typeparam name="T12">The type of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</typeparam>
	/// <typeparam name="T13">The type of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</typeparam>
	/// <typeparam name="T14">The type of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</typeparam>
	/// <typeparam name="T15">The type of the fifteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item1" />.</typeparam>
	/// <typeparam name="T16">The type of the sixteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item2" />.</typeparam>
	/// <typeparam name="T17">The type of the seventeenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item3" />.</typeparam>
	/// <typeparam name="T18">The type of the eighteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item4" />.</typeparam>
	/// <returns>The converted value tuple instance.</returns>
	public static (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18) ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18>>> value)
	{
		return CreateLong(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, CreateLong<T8, T9, T10, T11, T12, T13, T14, (T15, T16, T17, T18)>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, ValueTuple.Create(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4)));
	}

	/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
	/// <param name="value">The tuple object to convert to a value tuple</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	/// <typeparam name="T3">The type of the third element.</typeparam>
	/// <typeparam name="T4">The type of the fourth element.</typeparam>
	/// <typeparam name="T5">The type of the fifth element.</typeparam>
	/// <typeparam name="T6">The type of the sixth element.</typeparam>
	/// <typeparam name="T7">The type of the seventh element.</typeparam>
	/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
	/// <typeparam name="T9">The type of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</typeparam>
	/// <typeparam name="T10">The type of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</typeparam>
	/// <typeparam name="T11">The type of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</typeparam>
	/// <typeparam name="T12">The type of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</typeparam>
	/// <typeparam name="T13">The type of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</typeparam>
	/// <typeparam name="T14">The type of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</typeparam>
	/// <typeparam name="T15">The type of the fifteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item1" />.</typeparam>
	/// <typeparam name="T16">The type of the sixteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item2" />.</typeparam>
	/// <typeparam name="T17">The type of the seventeenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item3" />.</typeparam>
	/// <typeparam name="T18">The type of the eighteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item4" />.</typeparam>
	/// <typeparam name="T19">The type of the nineteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item5" />.</typeparam>
	/// <returns>The converted value tuple instance.</returns>
	public static (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19) ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19>>> value)
	{
		return CreateLong(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, CreateLong<T8, T9, T10, T11, T12, T13, T14, (T15, T16, T17, T18, T19)>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, ValueTuple.Create(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4, value.Rest.Rest.Item5)));
	}

	/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
	/// <param name="value">The tuple object to convert to a value tuple</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	/// <typeparam name="T3">The type of the third element.</typeparam>
	/// <typeparam name="T4">The type of the fourth element.</typeparam>
	/// <typeparam name="T5">The type of the fifth element.</typeparam>
	/// <typeparam name="T6">The type of the sixth element.</typeparam>
	/// <typeparam name="T7">The type of the seventh element.</typeparam>
	/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
	/// <typeparam name="T9">The type of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</typeparam>
	/// <typeparam name="T10">The type of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</typeparam>
	/// <typeparam name="T11">The type of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</typeparam>
	/// <typeparam name="T12">The type of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</typeparam>
	/// <typeparam name="T13">The type of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</typeparam>
	/// <typeparam name="T14">The type of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</typeparam>
	/// <typeparam name="T15">The type of the fifteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item1" />.</typeparam>
	/// <typeparam name="T16">The type of the sixteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item2" />.</typeparam>
	/// <typeparam name="T17">The type of the seventeenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item3" />.</typeparam>
	/// <typeparam name="T18">The type of the eighteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item4" />.</typeparam>
	/// <typeparam name="T19">The type of the nineteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item5" />.</typeparam>
	/// <typeparam name="T20">The type of the twentieth element, or <paramref name="value" /><see langword=".Rest.Rest.Item6" />.</typeparam>
	/// <returns>The converted value tuple instance.</returns>
	public static (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20) ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20>>> value)
	{
		return CreateLong(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, CreateLong<T8, T9, T10, T11, T12, T13, T14, (T15, T16, T17, T18, T19, T20)>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, ValueTuple.Create(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4, value.Rest.Rest.Item5, value.Rest.Rest.Item6)));
	}

	/// <summary>Converts an instance of the <see langword="Tuple" /> class to an instance of the  <see langword="ValueTuple" /> structure.</summary>
	/// <param name="value">The tuple object to convert to a value tuple</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <typeparam name="T2">The type of the second element.</typeparam>
	/// <typeparam name="T3">The type of the third element.</typeparam>
	/// <typeparam name="T4">The type of the fourth element.</typeparam>
	/// <typeparam name="T5">The type of the fifth element.</typeparam>
	/// <typeparam name="T6">The type of the sixth element.</typeparam>
	/// <typeparam name="T7">The type of the seventh element.</typeparam>
	/// <typeparam name="T8">The type of the eighth element, or <paramref name="value" /><see langword=".Rest.Item1" />.</typeparam>
	/// <typeparam name="T9">The type of the ninth element, or <paramref name="value" /><see langword=".Rest.Item2" />.</typeparam>
	/// <typeparam name="T10">The type of the tenth element, or <paramref name="value" /><see langword=".Rest.Item3" />.</typeparam>
	/// <typeparam name="T11">The type of the eleventh element, or <paramref name="value" /><see langword=".Rest.Item4" />.</typeparam>
	/// <typeparam name="T12">The type of the twelfth element, or <paramref name="value" /><see langword=".Rest.Item5" />.</typeparam>
	/// <typeparam name="T13">The type of the thirteenth element, or <paramref name="value" /><see langword=".Rest.Item6" />.</typeparam>
	/// <typeparam name="T14">The type of the fourteenth element, or <paramref name="value" /><see langword=".Rest.Item7" />.</typeparam>
	/// <typeparam name="T15">The type of the fifteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item1" />.</typeparam>
	/// <typeparam name="T16">The type of the sixteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item2" />.</typeparam>
	/// <typeparam name="T17">The type of the seventeenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item3" />.</typeparam>
	/// <typeparam name="T18">The type of the eighteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item4" />.</typeparam>
	/// <typeparam name="T19">The type of the nineteenth element, or <paramref name="value" /><see langword=".Rest.Rest.Item5" />.</typeparam>
	/// <typeparam name="T20">The type of the twentieth element, or <paramref name="value" /><see langword=".Rest.Rest.Item6" />.</typeparam>
	/// <typeparam name="T21">The type of the twenty-first element, or <paramref name="value" /><see langword=".Rest.Rest.Item7" />.</typeparam>
	/// <returns>The converted value tuple instance.</returns>
	public static (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21) ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20, T21>>> value)
	{
		return CreateLong(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, CreateLong<T8, T9, T10, T11, T12, T13, T14, (T15, T16, T17, T18, T19, T20, T21)>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, ValueTuple.Create(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4, value.Rest.Rest.Item5, value.Rest.Rest.Item6, value.Rest.Rest.Item7)));
	}

	/// <summary>Converts an instance of the <see langword="ValueTuple" /> structure to an instance of the  <see langword="Tuple" /> class.</summary>
	/// <param name="value">The value tuple instance to convert to a tuple.</param>
	/// <typeparam name="T1">The type of the first element.</typeparam>
	/// <returns>The converted tuple.</returns>
	public static Tuple<T1> ToTuple<T1>(this ValueTuple<T1> value)
	{
		return Tuple.Create(value.Item1);
	}

	public static Tuple<T1, T2> ToTuple<T1, T2>(this (T1, T2) value)
	{
		return Tuple.Create(value.Item1, value.Item2);
	}

	public static Tuple<T1, T2, T3> ToTuple<T1, T2, T3>(this (T1, T2, T3) value)
	{
		return Tuple.Create(value.Item1, value.Item2, value.Item3);
	}

	public static Tuple<T1, T2, T3, T4> ToTuple<T1, T2, T3, T4>(this (T1, T2, T3, T4) value)
	{
		return Tuple.Create(value.Item1, value.Item2, value.Item3, value.Item4);
	}

	public static Tuple<T1, T2, T3, T4, T5> ToTuple<T1, T2, T3, T4, T5>(this (T1, T2, T3, T4, T5) value)
	{
		return Tuple.Create(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5);
	}

	public static Tuple<T1, T2, T3, T4, T5, T6> ToTuple<T1, T2, T3, T4, T5, T6>(this (T1, T2, T3, T4, T5, T6) value)
	{
		return Tuple.Create(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6);
	}

	public static Tuple<T1, T2, T3, T4, T5, T6, T7> ToTuple<T1, T2, T3, T4, T5, T6, T7>(this (T1, T2, T3, T4, T5, T6, T7) value)
	{
		return Tuple.Create(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7);
	}

	public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8>(this (T1, T2, T3, T4, T5, T6, T7, T8) value)
	{
		return CreateLongRef(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, Tuple.Create(value.Rest.Item1));
	}

	public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this (T1, T2, T3, T4, T5, T6, T7, T8, T9) value)
	{
		return CreateLongRef(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, Tuple.Create(value.Rest.Item1, value.Rest.Item2));
	}

	public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10) value)
	{
		return CreateLongRef(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, Tuple.Create(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3));
	}

	public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11) value)
	{
		return CreateLongRef(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, Tuple.Create(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4));
	}

	public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12) value)
	{
		return CreateLongRef(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, Tuple.Create(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5));
	}

	public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13) value)
	{
		return CreateLongRef(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, Tuple.Create(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6));
	}

	public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14) value)
	{
		return CreateLongRef(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, Tuple.Create(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7));
	}

	public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15>>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15) value)
	{
		return CreateLongRef(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, CreateLongRef(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, Tuple.Create(value.Rest.Rest.Item1)));
	}

	public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16>>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16) value)
	{
		return CreateLongRef(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, CreateLongRef(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, Tuple.Create(value.Rest.Rest.Item1, value.Rest.Rest.Item2)));
	}

	public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17>>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(this (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17) value)
	{
		return CreateLongRef(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, CreateLongRef(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, Tuple.Create(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3)));
	}

	public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18>>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(this (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18) value)
	{
		return CreateLongRef(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, CreateLongRef(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, Tuple.Create(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4)));
	}

	public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19>>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(this (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19) value)
	{
		return CreateLongRef(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, CreateLongRef(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, Tuple.Create(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4, value.Rest.Rest.Item5)));
	}

	public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20>>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(this (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20) value)
	{
		return CreateLongRef(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, CreateLongRef(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, Tuple.Create(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4, value.Rest.Rest.Item5, value.Rest.Rest.Item6)));
	}

	public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20, T21>>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(this (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21) value)
	{
		return CreateLongRef(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, CreateLongRef(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, Tuple.Create(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4, value.Rest.Rest.Item5, value.Rest.Rest.Item6, value.Rest.Rest.Item7)));
	}

	private static ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> CreateLong<T1, T2, T3, T4, T5, T6, T7, TRest>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, TRest rest) where TRest : struct, ITuple
	{
		return new ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>(item1, item2, item3, item4, item5, item6, item7, rest);
	}

	private static Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> CreateLongRef<T1, T2, T3, T4, T5, T6, T7, TRest>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, TRest rest) where TRest : ITuple
	{
		return new Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>(item1, item2, item3, item4, item5, item6, item7, rest);
	}
}
