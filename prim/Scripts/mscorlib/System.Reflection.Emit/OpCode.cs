using System.Runtime.InteropServices;

namespace System.Reflection.Emit;

/// <summary>Describes an intermediate language (IL) instruction.</summary>
[ComVisible(true)]
public struct OpCode
{
	internal byte op1;

	internal byte op2;

	private byte push;

	private byte pop;

	private byte size;

	private byte type;

	private byte args;

	private byte flow;

	/// <summary>The name of the intermediate language (IL) instruction.</summary>
	/// <returns>Read-only. The name of the IL instruction.</returns>
	public string Name
	{
		get
		{
			if (op1 == byte.MaxValue)
			{
				return OpCodeNames.names[op2];
			}
			return OpCodeNames.names[256 + op2];
		}
	}

	/// <summary>The size of the intermediate language (IL) instruction.</summary>
	/// <returns>Read-only. The size of the IL instruction.</returns>
	public int Size => size;

	/// <summary>The type of intermediate language (IL) instruction.</summary>
	/// <returns>Read-only. The type of intermediate language (IL) instruction.</returns>
	public OpCodeType OpCodeType => (OpCodeType)type;

	/// <summary>The operand type of an intermediate language (IL) instruction.</summary>
	/// <returns>Read-only. The operand type of an IL instruction.</returns>
	public OperandType OperandType => (OperandType)args;

	/// <summary>The flow control characteristics of the intermediate language (IL) instruction.</summary>
	/// <returns>Read-only. The type of flow control.</returns>
	public FlowControl FlowControl => (FlowControl)flow;

	/// <summary>How the intermediate language (IL) instruction pops the stack.</summary>
	/// <returns>Read-only. The way the IL instruction pops the stack.</returns>
	public StackBehaviour StackBehaviourPop => (StackBehaviour)pop;

	/// <summary>How the intermediate language (IL) instruction pushes operand onto the stack.</summary>
	/// <returns>Read-only. The way the IL instruction pushes operand onto the stack.</returns>
	public StackBehaviour StackBehaviourPush => (StackBehaviour)push;

	/// <summary>Gets the numeric value of the intermediate language (IL) instruction.</summary>
	/// <returns>Read-only. The numeric value of the IL instruction.</returns>
	public short Value
	{
		get
		{
			if (size == 1)
			{
				return op2;
			}
			return (short)((op1 << 8) | op2);
		}
	}

	internal OpCode(int p, int q)
	{
		op1 = (byte)(p & 0xFF);
		op2 = (byte)((p >> 8) & 0xFF);
		push = (byte)((p >> 16) & 0xFF);
		pop = (byte)((p >> 24) & 0xFF);
		size = (byte)(q & 0xFF);
		type = (byte)((q >> 8) & 0xFF);
		args = (byte)((q >> 16) & 0xFF);
		flow = (byte)((q >> 24) & 0xFF);
	}

	/// <summary>Returns the generated hash code for this <see langword="Opcode" />.</summary>
	/// <returns>The hash code for this instance.</returns>
	public override int GetHashCode()
	{
		return Name.GetHashCode();
	}

	/// <summary>Tests whether the given object is equal to this <see langword="Opcode" />.</summary>
	/// <param name="obj">The object to compare to this object.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="obj" /> is an instance of <see langword="Opcode" /> and is equal to this object; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		if (obj == null || !(obj is OpCode opCode))
		{
			return false;
		}
		if (opCode.op1 == op1)
		{
			return opCode.op2 == op2;
		}
		return false;
	}

	/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.OpCode" />.</summary>
	/// <param name="obj">The <see cref="T:System.Reflection.Emit.OpCode" /> to compare to the current instance.</param>
	/// <returns>
	///   <see langword="true" /> if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, <see langword="false" />.</returns>
	public bool Equals(OpCode obj)
	{
		if (obj.op1 == op1)
		{
			return obj.op2 == op2;
		}
		return false;
	}

	/// <summary>Returns this <see langword="Opcode" /> as a <see cref="T:System.String" />.</summary>
	/// <returns>A string containing the name of this <see langword="Opcode" />.</returns>
	public override string ToString()
	{
		return Name;
	}

	/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.OpCode" /> structures are equal.</summary>
	/// <param name="a">The <see cref="T:System.Reflection.Emit.OpCode" /> to compare to <paramref name="b" />.</param>
	/// <param name="b">The <see cref="T:System.Reflection.Emit.OpCode" /> to compare to <paramref name="a" />.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, <see langword="false" />.</returns>
	public static bool operator ==(OpCode a, OpCode b)
	{
		if (a.op1 == b.op1)
		{
			return a.op2 == b.op2;
		}
		return false;
	}

	/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.OpCode" /> structures are not equal.</summary>
	/// <param name="a">The <see cref="T:System.Reflection.Emit.OpCode" /> to compare to <paramref name="b" />.</param>
	/// <param name="b">The <see cref="T:System.Reflection.Emit.OpCode" /> to compare to <paramref name="a" />.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, <see langword="false" />.</returns>
	public static bool operator !=(OpCode a, OpCode b)
	{
		if (a.op1 == b.op1)
		{
			return a.op2 != b.op2;
		}
		return true;
	}
}
