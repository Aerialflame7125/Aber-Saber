using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace Unity.Collections.LowLevel.Unsafe;

[VisibleToOtherModules]
[NativeHeader("Runtime/Export/Unsafe/UnsafeUtility.bindings.h")]
[StaticAccessor("UnsafeUtility", StaticAccessorType.DoubleColon)]
public static class UnsafeUtility
{
	[MethodImpl(MethodImplOptions.InternalCall)]
	[ThreadSafe]
	private static extern int GetFieldOffsetInStruct(FieldInfo field);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[ThreadSafe]
	private static extern int GetFieldOffsetInClass(FieldInfo field);

	public static int GetFieldOffset(FieldInfo field)
	{
		if (field.DeclaringType.IsValueType)
		{
			return GetFieldOffsetInStruct(field);
		}
		if (field.DeclaringType.IsClass)
		{
			return GetFieldOffsetInClass(field);
		}
		return -1;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[ThreadSafe]
	public unsafe static extern void* PinGCObjectAndGetAddress(object target, out ulong gcHandle);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[ThreadSafe]
	public static extern void ReleaseGCObject(ulong gcHandle);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[ThreadSafe]
	public unsafe static extern void CopyObjectAddressToPtr(object target, void* dstPtr);

	public static bool IsBlittable<T>() where T : struct
	{
		return IsBlittable(typeof(T));
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[ThreadSafe]
	public unsafe static extern void* Malloc(long size, int alignment, Allocator allocator);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[ThreadSafe]
	public unsafe static extern void Free(void* memory, Allocator allocator);

	public static bool IsValidAllocator(Allocator allocator)
	{
		return allocator > Allocator.None;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[ThreadSafe]
	public unsafe static extern void MemCpy(void* destination, void* source, long size);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[ThreadSafe]
	public unsafe static extern void MemCpyReplicate(void* destination, void* source, int size, int count);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[ThreadSafe]
	public unsafe static extern void MemCpyStride(void* destination, int destinationStride, void* source, int sourceStride, int elementSize, int count);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[ThreadSafe]
	public unsafe static extern void MemMove(void* destination, void* source, long size);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[ThreadSafe]
	public unsafe static extern void MemClear(void* destination, long size);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[ThreadSafe]
	public static extern int SizeOf(Type type);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[ThreadSafe]
	public static extern bool IsBlittable(Type type);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[ThreadSafe]
	internal static extern void LogError(string msg, string filename, int linenumber);

	public unsafe static void CopyPtrToStructure<T>(void* ptr, out T output) where T : struct
	{
		output = System.Runtime.CompilerServices.Unsafe.Read<T>(ptr);
	}

	public unsafe static void CopyStructureToPtr<T>(ref T input, void* ptr) where T : struct
	{
		System.Runtime.CompilerServices.Unsafe.Write(ptr, input);
	}

	public unsafe static T ReadArrayElement<T>(void* source, int index)
	{
		return System.Runtime.CompilerServices.Unsafe.Read<T>((byte*)source + index * System.Runtime.CompilerServices.Unsafe.SizeOf<T>());
	}

	public unsafe static T ReadArrayElementWithStride<T>(void* source, int index, int stride)
	{
		return System.Runtime.CompilerServices.Unsafe.Read<T>((byte*)source + index * stride);
	}

	public unsafe static void WriteArrayElement<T>(void* destination, int index, T value)
	{
		System.Runtime.CompilerServices.Unsafe.Write((byte*)destination + index * System.Runtime.CompilerServices.Unsafe.SizeOf<T>(), value);
	}

	public unsafe static void WriteArrayElementWithStride<T>(void* destination, int index, int stride, T value)
	{
		System.Runtime.CompilerServices.Unsafe.Write((byte*)destination + index * stride, value);
	}

	public unsafe static void* AddressOf<T>(ref T output) where T : struct
	{
		return System.Runtime.CompilerServices.Unsafe.AsPointer(ref output);
	}

	public static int SizeOf<T>() where T : struct
	{
		return System.Runtime.CompilerServices.Unsafe.SizeOf<T>();
	}

	public static int AlignOf<T>() where T : struct
	{
		return 4;
	}
}
