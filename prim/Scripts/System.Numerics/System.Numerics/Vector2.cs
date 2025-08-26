using System.Globalization;
using System.Numerics.Hashing;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Numerics;

/// <summary>Represents a vector with two single-precision floating-point values.</summary>
public struct Vector2 : IEquatable<Vector2>, IFormattable
{
	/// <summary>The X component of the vector.</summary>
	public float X;

	/// <summary>The Y component of the vector.</summary>
	public float Y;

	/// <summary>Returns a vector whose 2 elements are equal to zero.</summary>
	/// <returns>A vector whose two elements are equal to zero (that is, it returns the vector <c>(0,0)</c>.</returns>
	public static Vector2 Zero => default(Vector2);

	/// <summary>Gets a vector whose 2 elements are equal to one.</summary>
	/// <returns>A vector whose two elements are equal to one (that is, it returns the vector <c>(1,1)</c>.</returns>
	public static Vector2 One => new Vector2(1f, 1f);

	/// <summary>Gets the vector (1,0).</summary>
	/// <returns>The vector <c>(1,0)</c>.</returns>
	public static Vector2 UnitX => new Vector2(1f, 0f);

	/// <summary>Gets the vector (0,1).</summary>
	/// <returns>The vector <c>(0,1)</c>.</returns>
	public static Vector2 UnitY => new Vector2(0f, 1f);

	/// <summary>Returns the hash code for this instance.</summary>
	/// <returns>The hash code.</returns>
	public override int GetHashCode()
	{
		return System.Numerics.Hashing.HashHelpers.Combine(X.GetHashCode(), Y.GetHashCode());
	}

	/// <summary>Returns a value that indicates whether this instance and a specified object are equal.</summary>
	/// <param name="obj">The object to compare with the current instance.</param>
	/// <returns>
	///   <see langword="true" /> if the current instance and <paramref name="obj" /> are equal; otherwise, <see langword="false" />. If <paramref name="obj" /> is <see langword="null" />, the method returns <see langword="false" />.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override bool Equals(object obj)
	{
		if (!(obj is Vector2))
		{
			return false;
		}
		return Equals((Vector2)obj);
	}

	/// <summary>Returns the string representation of the current instance using default formatting.</summary>
	/// <returns>The string representation of the current instance.</returns>
	public override string ToString()
	{
		return ToString("G", CultureInfo.CurrentCulture);
	}

	/// <summary>Returns the string representation of the current instance using the specified format string to format individual elements.</summary>
	/// <param name="format">A standard or custom numeric format string that defines the format of individual elements.</param>
	/// <returns>The string representation of the current instance.</returns>
	public string ToString(string format)
	{
		return ToString(format, CultureInfo.CurrentCulture);
	}

	/// <summary>Returns the string representation of the current instance using the specified format string to format individual elements and the specified format provider to define culture-specific formatting.</summary>
	/// <param name="format">A standard or custom numeric format string that defines the format of individual elements.</param>
	/// <param name="formatProvider">A format provider that supplies culture-specific formatting information.</param>
	/// <returns>The string representation of the current instance.</returns>
	public string ToString(string format, IFormatProvider formatProvider)
	{
		StringBuilder stringBuilder = new StringBuilder();
		string numberGroupSeparator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;
		stringBuilder.Append('<');
		stringBuilder.Append(X.ToString(format, formatProvider));
		stringBuilder.Append(numberGroupSeparator);
		stringBuilder.Append(' ');
		stringBuilder.Append(Y.ToString(format, formatProvider));
		stringBuilder.Append('>');
		return stringBuilder.ToString();
	}

	/// <summary>Returns the length of the vector.</summary>
	/// <returns>The vector's length.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public float Length()
	{
		if (Vector.IsHardwareAccelerated)
		{
			return MathF.Sqrt(Dot(this, this));
		}
		return MathF.Sqrt(X * X + Y * Y);
	}

	/// <summary>Returns the length of the vector squared.</summary>
	/// <returns>The vector's length squared.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public float LengthSquared()
	{
		if (Vector.IsHardwareAccelerated)
		{
			return Dot(this, this);
		}
		return X * X + Y * Y;
	}

	/// <summary>Computes the Euclidean distance between the two given points.</summary>
	/// <param name="value1">The first point.</param>
	/// <param name="value2">The second point.</param>
	/// <returns>The distance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Distance(Vector2 value1, Vector2 value2)
	{
		if (Vector.IsHardwareAccelerated)
		{
			Vector2 vector = value1 - value2;
			return MathF.Sqrt(Dot(vector, vector));
		}
		float num = value1.X - value2.X;
		float num2 = value1.Y - value2.Y;
		return MathF.Sqrt(num * num + num2 * num2);
	}

	/// <summary>Returns the Euclidean distance squared between two specified points.</summary>
	/// <param name="value1">The first point.</param>
	/// <param name="value2">The second point.</param>
	/// <returns>The distance squared.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float DistanceSquared(Vector2 value1, Vector2 value2)
	{
		if (Vector.IsHardwareAccelerated)
		{
			Vector2 vector = value1 - value2;
			return Dot(vector, vector);
		}
		float num = value1.X - value2.X;
		float num2 = value1.Y - value2.Y;
		return num * num + num2 * num2;
	}

	/// <summary>Returns a vector with the same direction as the specified vector, but with a length of one.</summary>
	/// <param name="value">The vector to normalize.</param>
	/// <returns>The normalized vector.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 Normalize(Vector2 value)
	{
		if (Vector.IsHardwareAccelerated)
		{
			float num = value.Length();
			return value / num;
		}
		float x = value.X * value.X + value.Y * value.Y;
		float num2 = 1f / MathF.Sqrt(x);
		return new Vector2(value.X * num2, value.Y * num2);
	}

	/// <summary>Returns the reflection of a vector off a surface that has the specified normal.</summary>
	/// <param name="vector">The source vector.</param>
	/// <param name="normal">The normal of the surface being reflected off.</param>
	/// <returns>The reflected vector.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 Reflect(Vector2 vector, Vector2 normal)
	{
		if (Vector.IsHardwareAccelerated)
		{
			float num = Dot(vector, normal);
			return vector - 2f * num * normal;
		}
		float num2 = vector.X * normal.X + vector.Y * normal.Y;
		return new Vector2(vector.X - 2f * num2 * normal.X, vector.Y - 2f * num2 * normal.Y);
	}

	/// <summary>Restricts a vector between a minimum and a maximum value.</summary>
	/// <param name="value1">The vector to restrict.</param>
	/// <param name="min">The minimum value.</param>
	/// <param name="max">The maximum value.</param>
	/// <returns>The restricted vector.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 Clamp(Vector2 value1, Vector2 min, Vector2 max)
	{
		float x = value1.X;
		x = ((x > max.X) ? max.X : x);
		x = ((x < min.X) ? min.X : x);
		float y = value1.Y;
		y = ((y > max.Y) ? max.Y : y);
		y = ((y < min.Y) ? min.Y : y);
		return new Vector2(x, y);
	}

	/// <summary>Performs a linear interpolation between two vectors based on the given weighting.</summary>
	/// <param name="value1">The first vector.</param>
	/// <param name="value2">The second vector.</param>
	/// <param name="amount">A value between 0 and 1 that indicates the weight of <paramref name="value2" />.</param>
	/// <returns>The interpolated vector.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 Lerp(Vector2 value1, Vector2 value2, float amount)
	{
		return new Vector2(value1.X + (value2.X - value1.X) * amount, value1.Y + (value2.Y - value1.Y) * amount);
	}

	/// <summary>Transforms a vector by a specified 3x2 matrix.</summary>
	/// <param name="position">The vector to transform.</param>
	/// <param name="matrix">The transformation matrix.</param>
	/// <returns>The transformed vector.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 Transform(Vector2 position, Matrix3x2 matrix)
	{
		return new Vector2(position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M31, position.X * matrix.M12 + position.Y * matrix.M22 + matrix.M32);
	}

	/// <summary>Transforms a vector by a specified 4x4 matrix.</summary>
	/// <param name="position">The vector to transform.</param>
	/// <param name="matrix">The transformation matrix.</param>
	/// <returns>The transformed vector.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 Transform(Vector2 position, Matrix4x4 matrix)
	{
		return new Vector2(position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M41, position.X * matrix.M12 + position.Y * matrix.M22 + matrix.M42);
	}

	/// <summary>Transforms a vector normal by the given 3x2 matrix.</summary>
	/// <param name="normal">The source vector.</param>
	/// <param name="matrix">The matrix.</param>
	/// <returns>The transformed vector.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 TransformNormal(Vector2 normal, Matrix3x2 matrix)
	{
		return new Vector2(normal.X * matrix.M11 + normal.Y * matrix.M21, normal.X * matrix.M12 + normal.Y * matrix.M22);
	}

	/// <summary>Transforms a vector normal by the given 4x4 matrix.</summary>
	/// <param name="normal">The source vector.</param>
	/// <param name="matrix">The matrix.</param>
	/// <returns>The transformed vector.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 TransformNormal(Vector2 normal, Matrix4x4 matrix)
	{
		return new Vector2(normal.X * matrix.M11 + normal.Y * matrix.M21, normal.X * matrix.M12 + normal.Y * matrix.M22);
	}

	/// <summary>Transforms a vector by the specified Quaternion rotation value.</summary>
	/// <param name="value">The vector to rotate.</param>
	/// <param name="rotation">The rotation to apply.</param>
	/// <returns>The transformed vector.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 Transform(Vector2 value, Quaternion rotation)
	{
		float num = rotation.X + rotation.X;
		float num2 = rotation.Y + rotation.Y;
		float num3 = rotation.Z + rotation.Z;
		float num4 = rotation.W * num3;
		float num5 = rotation.X * num;
		float num6 = rotation.X * num2;
		float num7 = rotation.Y * num2;
		float num8 = rotation.Z * num3;
		return new Vector2(value.X * (1f - num7 - num8) + value.Y * (num6 - num4), value.X * (num6 + num4) + value.Y * (1f - num5 - num8));
	}

	/// <summary>Adds two vectors together.</summary>
	/// <param name="left">The first vector to add.</param>
	/// <param name="right">The second vector to add.</param>
	/// <returns>The summed vector.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 Add(Vector2 left, Vector2 right)
	{
		return left + right;
	}

	/// <summary>Subtracts the second vector from the first.</summary>
	/// <param name="left">The first vector.</param>
	/// <param name="right">The second vector.</param>
	/// <returns>The difference vector.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 Subtract(Vector2 left, Vector2 right)
	{
		return left - right;
	}

	/// <summary>Returns a new vector whose values are the product of each pair of elements in two specified vectors.</summary>
	/// <param name="left">The first vector.</param>
	/// <param name="right">The second vector.</param>
	/// <returns>The element-wise product vector.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 Multiply(Vector2 left, Vector2 right)
	{
		return left * right;
	}

	/// <summary>Multiplies a vector by a specified scalar.</summary>
	/// <param name="left">The vector to multiply.</param>
	/// <param name="right">The scalar value.</param>
	/// <returns>The scaled vector.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 Multiply(Vector2 left, float right)
	{
		return left * right;
	}

	/// <summary>Multiplies a scalar value by a specified vector.</summary>
	/// <param name="left">The scaled value.</param>
	/// <param name="right">The vector.</param>
	/// <returns>The scaled vector.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 Multiply(float left, Vector2 right)
	{
		return left * right;
	}

	/// <summary>Divides the first vector by the second.</summary>
	/// <param name="left">The first vector.</param>
	/// <param name="right">The second vector.</param>
	/// <returns>The vector resulting from the division.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 Divide(Vector2 left, Vector2 right)
	{
		return left / right;
	}

	/// <summary>Divides the specified vector by a specified scalar value.</summary>
	/// <param name="left">The vector.</param>
	/// <param name="divisor">The scalar value.</param>
	/// <returns>The vector that results from the division.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 Divide(Vector2 left, float divisor)
	{
		return left / divisor;
	}

	/// <summary>Negates a specified vector.</summary>
	/// <param name="value">The vector to negate.</param>
	/// <returns>The negated vector.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 Negate(Vector2 value)
	{
		return -value;
	}

	/// <summary>Creates a new <see cref="T:System.Numerics.Vector2" /> object whose two elements have the same value.</summary>
	/// <param name="value">The value to assign to both elements.</param>
	[JitIntrinsic]
	public Vector2(float value)
		: this(value, value)
	{
	}

	/// <summary>Creates a vector whose elements have the specified values.</summary>
	/// <param name="x">The value to assign to the <see cref="F:System.Numerics.Vector2.X" /> field.</param>
	/// <param name="y">The value to assign to the <see cref="F:System.Numerics.Vector2.Y" /> field.</param>
	[JitIntrinsic]
	public Vector2(float x, float y)
	{
		X = x;
		Y = y;
	}

	/// <summary>Copies the elements of the vector to a specified array.</summary>
	/// <param name="array">The destination array.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="array" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">The number of elements in the current instance is greater than in the array.</exception>
	/// <exception cref="T:System.RankException">
	///   <paramref name="array" /> is multidimensional.</exception>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void CopyTo(float[] array)
	{
		CopyTo(array, 0);
	}

	/// <summary>Copies the elements of the vector to a specified array starting at a specified index position.</summary>
	/// <param name="array">The destination array.</param>
	/// <param name="index">The index at which to copy the first element of the vector.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="array" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">The number of elements in the current instance is greater than in the array.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than zero.  
	/// -or-  
	/// <paramref name="index" /> is greater than or equal to the array length.</exception>
	/// <exception cref="T:System.RankException">
	///   <paramref name="array" /> is multidimensional.</exception>
	public void CopyTo(float[] array, int index)
	{
		if (array == null)
		{
			throw new NullReferenceException("The method was called with a null array argument.");
		}
		if (index < 0 || index >= array.Length)
		{
			throw new ArgumentOutOfRangeException("index", global::SR.Format("Index was out of bounds:", index));
		}
		if (array.Length - index < 2)
		{
			throw new ArgumentException(global::SR.Format("Number of elements in source vector is greater than the destination array", index));
		}
		array[index] = X;
		array[index + 1] = Y;
	}

	/// <summary>Returns a value that indicates whether this instance and another vector are equal.</summary>
	/// <param name="other">The other vector.</param>
	/// <returns>
	///   <see langword="true" /> if the two vectors are equal; otherwise, <see langword="false" />.</returns>
	[JitIntrinsic]
	public bool Equals(Vector2 other)
	{
		if (X == other.X)
		{
			return Y == other.Y;
		}
		return false;
	}

	/// <summary>Returns the dot product of two vectors.</summary>
	/// <param name="value1">The first vector.</param>
	/// <param name="value2">The second vector.</param>
	/// <returns>The dot product.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[JitIntrinsic]
	public static float Dot(Vector2 value1, Vector2 value2)
	{
		return value1.X * value2.X + value1.Y * value2.Y;
	}

	/// <summary>Returns a vector whose elements are the minimum of each of the pairs of elements in two specified vectors.</summary>
	/// <param name="value1">The first vector.</param>
	/// <param name="value2">The second vector.</param>
	/// <returns>The minimized vector.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[JitIntrinsic]
	public static Vector2 Min(Vector2 value1, Vector2 value2)
	{
		return new Vector2((value1.X < value2.X) ? value1.X : value2.X, (value1.Y < value2.Y) ? value1.Y : value2.Y);
	}

	/// <summary>Returns a vector whose elements are the maximum of each of the pairs of elements in two specified vectors.</summary>
	/// <param name="value1">The first vector.</param>
	/// <param name="value2">The second vector.</param>
	/// <returns>The maximized vector.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[JitIntrinsic]
	public static Vector2 Max(Vector2 value1, Vector2 value2)
	{
		return new Vector2((value1.X > value2.X) ? value1.X : value2.X, (value1.Y > value2.Y) ? value1.Y : value2.Y);
	}

	/// <summary>Returns a vector whose elements are the absolute values of each of the specified vector's elements.</summary>
	/// <param name="value">A vector.</param>
	/// <returns>The absolute value vector.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[JitIntrinsic]
	public static Vector2 Abs(Vector2 value)
	{
		return new Vector2(MathF.Abs(value.X), MathF.Abs(value.Y));
	}

	/// <summary>Returns a vector whose elements are the square root of each of a specified vector's elements.</summary>
	/// <param name="value">A vector.</param>
	/// <returns>The square root vector.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[JitIntrinsic]
	public static Vector2 SquareRoot(Vector2 value)
	{
		return new Vector2(MathF.Sqrt(value.X), MathF.Sqrt(value.Y));
	}

	/// <summary>Adds two vectors together.</summary>
	/// <param name="left">The first vector to add.</param>
	/// <param name="right">The second vector to add.</param>
	/// <returns>The summed vector.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[JitIntrinsic]
	public static Vector2 operator +(Vector2 left, Vector2 right)
	{
		return new Vector2(left.X + right.X, left.Y + right.Y);
	}

	/// <summary>Subtracts the second vector from the first.</summary>
	/// <param name="left">The first vector.</param>
	/// <param name="right">The second vector.</param>
	/// <returns>The vector that results from subtracting <paramref name="right" /> from <paramref name="left" />.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[JitIntrinsic]
	public static Vector2 operator -(Vector2 left, Vector2 right)
	{
		return new Vector2(left.X - right.X, left.Y - right.Y);
	}

	/// <summary>Returns a new vector whose values are the product of each pair of elements in two specified vectors.</summary>
	/// <param name="left">The first vector.</param>
	/// <param name="right">The second vector.</param>
	/// <returns>The element-wise product vector.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[JitIntrinsic]
	public static Vector2 operator *(Vector2 left, Vector2 right)
	{
		return new Vector2(left.X * right.X, left.Y * right.Y);
	}

	/// <summary>Multiples the scalar value by the specified vector.</summary>
	/// <param name="left">The vector.</param>
	/// <param name="right">The scalar value.</param>
	/// <returns>The scaled vector.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[JitIntrinsic]
	public static Vector2 operator *(float left, Vector2 right)
	{
		return new Vector2(left, left) * right;
	}

	/// <summary>Multiples the specified vector by the specified scalar value.</summary>
	/// <param name="left">The vector.</param>
	/// <param name="right">The scalar value.</param>
	/// <returns>The scaled vector.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[JitIntrinsic]
	public static Vector2 operator *(Vector2 left, float right)
	{
		return left * new Vector2(right, right);
	}

	/// <summary>Divides the first vector by the second.</summary>
	/// <param name="left">The first vector.</param>
	/// <param name="right">The second vector.</param>
	/// <returns>The vector that results from dividing <paramref name="left" /> by <paramref name="right" />.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[JitIntrinsic]
	public static Vector2 operator /(Vector2 left, Vector2 right)
	{
		return new Vector2(left.X / right.X, left.Y / right.Y);
	}

	/// <summary>Divides the specified vector by a specified scalar value.</summary>
	/// <param name="value1">The vector.</param>
	/// <param name="value2">The scalar value.</param>
	/// <returns>The result of the division.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[JitIntrinsic]
	public static Vector2 operator /(Vector2 value1, float value2)
	{
		float num = 1f / value2;
		return new Vector2(value1.X * num, value1.Y * num);
	}

	/// <summary>Negates the specified vector.</summary>
	/// <param name="value">The vector to negate.</param>
	/// <returns>The negated vector.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 operator -(Vector2 value)
	{
		return Zero - value;
	}

	/// <summary>Returns a value that indicates whether each pair of elements in two specified vectors is equal.</summary>
	/// <param name="left">The first vector to compare.</param>
	/// <param name="right">The second vector to compare.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, <see langword="false" />.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator ==(Vector2 left, Vector2 right)
	{
		return left.Equals(right);
	}

	/// <summary>Returns a value that indicates whether two specified vectors are not equal.</summary>
	/// <param name="left">The first vector to compare.</param>
	/// <param name="right">The second vector to compare.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, <see langword="false" />.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator !=(Vector2 left, Vector2 right)
	{
		return !(left == right);
	}
}
