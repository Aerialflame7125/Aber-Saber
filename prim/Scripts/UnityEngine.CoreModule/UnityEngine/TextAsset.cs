using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine;

public class TextAsset : Object
{
	public extern string text
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern byte[] bytes
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public TextAsset()
	{
		Internal_CreateInstance(this, null);
	}

	public TextAsset(string text)
	{
		Internal_CreateInstance(this, text);
	}

	public override string ToString()
	{
		return text;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void Internal_CreateInstance([Writable] TextAsset self, string text);
}
