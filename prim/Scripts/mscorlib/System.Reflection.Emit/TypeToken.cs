using System.Runtime.InteropServices;

namespace System.Reflection.Emit;

/// <summary>Represents the <see langword="Token" /> returned by the metadata to represent a type.</summary>
[Serializable]
[ComVisible(true)]
public struct TypeToken
{
	internal int tokValue;

	/// <summary>The default <see langword="TypeToken" /> with <see cref="P:System.Reflection.Emit.TypeToken.Token" /> value 0.</summary>
	public static readonly TypeToken Empty;

	/// <summary>Retrieves the metadata token for this class.</summary>
	/// <returns>Read-only. Retrieves the metadata token of this type.</returns>
	public int Token => tokValue;

	internal TypeToken(int val)
	{
		tokValue = val;
	}

	/// <summary>Checks if the given object is an instance of <see langword="TypeToken" /> and is equal to this instance.</summary>
	/// <param name="obj">The object to compare with this TypeToken.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="obj" /> is an instance of <see langword="TypeToken" /> and is equal to this object; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		bool flag = obj is TypeToken;
		if (flag)
		{
			TypeToken typeToken = (TypeToken)obj;
			flag = tokValue == typeToken.tokValue;
		}
		return flag;
	}

	/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.TypeToken" />.</summary>
	/// <param name="obj">The <see cref="T:System.Reflection.Emit.TypeToken" /> to compare to the current instance.</param>
	/// <returns>
	///   <see langword="true" /> if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, <see langword="false" />.</returns>
	public bool Equals(TypeToken obj)
	{
		return tokValue == obj.tokValue;
	}

	/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.TypeToken" /> structures are equal.</summary>
	/// <param name="a">The <see cref="T:System.Reflection.Emit.TypeToken" /> to compare to <paramref name="b" />.</param>
	/// <param name="b">The <see cref="T:System.Reflection.Emit.TypeToken" /> to compare to <paramref name="a" />.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, <see langword="false" />.</returns>
	public static bool operator ==(TypeToken a, TypeToken b)
	{
		return object.Equals(a, b);
	}

	/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.TypeToken" /> structures are not equal.</summary>
	/// <param name="a">The <see cref="T:System.Reflection.Emit.TypeToken" /> to compare to <paramref name="b" />.</param>
	/// <param name="b">The <see cref="T:System.Reflection.Emit.TypeToken" /> to compare to <paramref name="a" />.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, <see langword="false" />.</returns>
	public static bool operator !=(TypeToken a, TypeToken b)
	{
		return !object.Equals(a, b);
	}

	/// <summary>Generates the hash code for this type.</summary>
	/// <returns>The hash code for this type.</returns>
	public override int GetHashCode()
	{
		return tokValue;
	}
}
