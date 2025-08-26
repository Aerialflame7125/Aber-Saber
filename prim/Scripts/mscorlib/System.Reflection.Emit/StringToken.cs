using System.Runtime.InteropServices;

namespace System.Reflection.Emit;

/// <summary>Represents a token that represents a string.</summary>
[Serializable]
[ComVisible(true)]
public struct StringToken
{
	internal int tokValue;

	/// <summary>Retrieves the metadata token for this string.</summary>
	/// <returns>Read-only. Retrieves the metadata token of this string.</returns>
	public int Token => tokValue;

	internal StringToken(int val)
	{
		tokValue = val;
	}

	/// <summary>Checks if the given object is an instance of <see langword="StringToken" /> and is equal to this instance.</summary>
	/// <param name="obj">The object to compare with this <see langword="StringToken" />.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="obj" /> is an instance of <see langword="StringToken" /> and is equal to this object; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		bool flag = obj is StringToken;
		if (flag)
		{
			StringToken stringToken = (StringToken)obj;
			flag = tokValue == stringToken.tokValue;
		}
		return flag;
	}

	/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.StringToken" />.</summary>
	/// <param name="obj">The <see cref="T:System.Reflection.Emit.StringToken" /> to compare to the current instance.</param>
	/// <returns>
	///   <see langword="true" /> if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, <see langword="false" />.</returns>
	public bool Equals(StringToken obj)
	{
		return tokValue == obj.tokValue;
	}

	/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.StringToken" /> structures are equal.</summary>
	/// <param name="a">The <see cref="T:System.Reflection.Emit.StringToken" /> to compare to <paramref name="b" />.</param>
	/// <param name="b">The <see cref="T:System.Reflection.Emit.StringToken" /> to compare to <paramref name="a" />.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, <see langword="false" />.</returns>
	public static bool operator ==(StringToken a, StringToken b)
	{
		return object.Equals(a, b);
	}

	/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.StringToken" /> structures are not equal.</summary>
	/// <param name="a">The <see cref="T:System.Reflection.Emit.StringToken" /> to compare to <paramref name="b" />.</param>
	/// <param name="b">The <see cref="T:System.Reflection.Emit.StringToken" /> to compare to <paramref name="a" />.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, <see langword="false" />.</returns>
	public static bool operator !=(StringToken a, StringToken b)
	{
		return !object.Equals(a, b);
	}

	/// <summary>Returns the hash code for this string.</summary>
	/// <returns>The underlying string token.</returns>
	public override int GetHashCode()
	{
		return tokValue;
	}
}
