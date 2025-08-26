using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine;

[ExcludeFromPreset]
public sealed class AssetBundle : Object
{
	public extern Object mainAsset
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern bool isStreamedSceneAssetBundle
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	private AssetBundle()
	{
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern void UnloadAllAssetBundles(bool unloadAllObjects);

	public static IEnumerable<AssetBundle> GetAllLoadedAssetBundles()
	{
		return GetAllLoadedAssetBundles_Internal();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	internal static extern AssetBundle[] GetAllLoadedAssetBundles_Internal();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern AssetBundleCreateRequest LoadFromFileAsync(string path, [DefaultValue("0")] uint crc, [DefaultValue("0")] ulong offset);

	[ExcludeFromDocs]
	public static AssetBundleCreateRequest LoadFromFileAsync(string path, uint crc)
	{
		ulong offset = 0uL;
		return LoadFromFileAsync(path, crc, offset);
	}

	[ExcludeFromDocs]
	public static AssetBundleCreateRequest LoadFromFileAsync(string path)
	{
		ulong offset = 0uL;
		uint crc = 0u;
		return LoadFromFileAsync(path, crc, offset);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern AssetBundle LoadFromFile(string path, [DefaultValue("0")] uint crc, [DefaultValue("0")] ulong offset);

	[ExcludeFromDocs]
	public static AssetBundle LoadFromFile(string path, uint crc)
	{
		ulong offset = 0uL;
		return LoadFromFile(path, crc, offset);
	}

	[ExcludeFromDocs]
	public static AssetBundle LoadFromFile(string path)
	{
		ulong offset = 0uL;
		uint crc = 0u;
		return LoadFromFile(path, crc, offset);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern AssetBundleCreateRequest LoadFromMemoryAsync(byte[] binary, [DefaultValue("0")] uint crc);

	[ExcludeFromDocs]
	public static AssetBundleCreateRequest LoadFromMemoryAsync(byte[] binary)
	{
		uint crc = 0u;
		return LoadFromMemoryAsync(binary, crc);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern AssetBundle LoadFromMemory(byte[] binary, [DefaultValue("0")] uint crc);

	[ExcludeFromDocs]
	public static AssetBundle LoadFromMemory(byte[] binary)
	{
		uint crc = 0u;
		return LoadFromMemory(binary, crc);
	}

	internal static void ValidateLoadFromStream(Stream stream)
	{
		if (stream == null)
		{
			throw new ArgumentNullException("ManagedStream object must be non-null", "stream");
		}
		if (!stream.CanRead)
		{
			throw new ArgumentException("ManagedStream object must be readable (stream.CanRead must return true)", "stream");
		}
		if (!stream.CanSeek)
		{
			throw new ArgumentException("ManagedStream object must be seekable (stream.CanSeek must return true)", "stream");
		}
	}

	[ExcludeFromDocs]
	public static AssetBundleCreateRequest LoadFromStreamAsync(Stream stream, uint crc)
	{
		uint managedReadBufferSize = 0u;
		return LoadFromStreamAsync(stream, crc, managedReadBufferSize);
	}

	[ExcludeFromDocs]
	public static AssetBundleCreateRequest LoadFromStreamAsync(Stream stream)
	{
		uint managedReadBufferSize = 0u;
		uint crc = 0u;
		return LoadFromStreamAsync(stream, crc, managedReadBufferSize);
	}

	public static AssetBundleCreateRequest LoadFromStreamAsync(Stream stream, [DefaultValue("0")] uint crc, [DefaultValue("0")] uint managedReadBufferSize)
	{
		ValidateLoadFromStream(stream);
		return LoadFromStreamAsyncInternal(stream, crc, managedReadBufferSize);
	}

	[ExcludeFromDocs]
	public static AssetBundle LoadFromStream(Stream stream, uint crc)
	{
		uint managedReadBufferSize = 0u;
		return LoadFromStream(stream, crc, managedReadBufferSize);
	}

	[ExcludeFromDocs]
	public static AssetBundle LoadFromStream(Stream stream)
	{
		uint managedReadBufferSize = 0u;
		uint crc = 0u;
		return LoadFromStream(stream, crc, managedReadBufferSize);
	}

	public static AssetBundle LoadFromStream(Stream stream, [DefaultValue("0")] uint crc, [DefaultValue("0")] uint managedReadBufferSize)
	{
		ValidateLoadFromStream(stream);
		return LoadFromStreamInternal(stream, crc, managedReadBufferSize);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	internal static extern AssetBundleCreateRequest LoadFromStreamAsyncInternal(Stream stream, uint crc, [DefaultValue("0")] uint managedReadBufferSize);

	[ExcludeFromDocs]
	internal static AssetBundleCreateRequest LoadFromStreamAsyncInternal(Stream stream, uint crc)
	{
		uint managedReadBufferSize = 0u;
		return LoadFromStreamAsyncInternal(stream, crc, managedReadBufferSize);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	internal static extern AssetBundle LoadFromStreamInternal(Stream stream, uint crc, [DefaultValue("0")] uint managedReadBufferSize);

	[ExcludeFromDocs]
	internal static AssetBundle LoadFromStreamInternal(Stream stream, uint crc)
	{
		uint managedReadBufferSize = 0u;
		return LoadFromStreamInternal(stream, crc, managedReadBufferSize);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern bool Contains(string name);

	[Obsolete("Method Load has been deprecated. Script updater cannot update it as the loading behaviour has changed. Please use LoadAsset instead and check the documentation for details.", true)]
	public Object Load(string name)
	{
		return null;
	}

	[Obsolete("Method Load has been deprecated. Script updater cannot update it as the loading behaviour has changed. Please use LoadAsset instead and check the documentation for details.", true)]
	public T Load<T>(string name) where T : Object
	{
		return (T)null;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[Obsolete("Method Load has been deprecated. Script updater cannot update it as the loading behaviour has changed. Please use LoadAsset instead and check the documentation for details.", true)]
	[TypeInferenceRule(TypeInferenceRules.TypeReferencedBySecondArgument)]
	[GeneratedByOldBindingsGenerator]
	public extern Object Load(string name, Type type);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[Obsolete("Method LoadAsync has been deprecated. Script updater cannot update it as the loading behaviour has changed. Please use LoadAssetAsync instead and check the documentation for details.", true)]
	[GeneratedByOldBindingsGenerator]
	public extern AssetBundleRequest LoadAsync(string name, Type type);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[Obsolete("Method LoadAll has been deprecated. Script updater cannot update it as the loading behaviour has changed. Please use LoadAllAssets instead and check the documentation for details.", true)]
	[GeneratedByOldBindingsGenerator]
	public extern Object[] LoadAll(Type type);

	[Obsolete("Method LoadAll has been deprecated. Script updater cannot update it as the loading behaviour has changed. Please use LoadAllAssets instead and check the documentation for details.", true)]
	public Object[] LoadAll()
	{
		return null;
	}

	[Obsolete("Method LoadAll has been deprecated. Script updater cannot update it as the loading behaviour has changed. Please use LoadAllAssets instead and check the documentation for details.", true)]
	public T[] LoadAll<T>() where T : Object
	{
		return null;
	}

	public Object LoadAsset(string name)
	{
		return LoadAsset(name, typeof(Object));
	}

	public T LoadAsset<T>(string name) where T : Object
	{
		return (T)LoadAsset(name, typeof(T));
	}

	[TypeInferenceRule(TypeInferenceRules.TypeReferencedBySecondArgument)]
	public Object LoadAsset(string name, Type type)
	{
		if (name == null)
		{
			throw new NullReferenceException("The input asset name cannot be null.");
		}
		if (name.Length == 0)
		{
			throw new ArgumentException("The input asset name cannot be empty.");
		}
		if ((object)type == null)
		{
			throw new NullReferenceException("The input type cannot be null.");
		}
		return LoadAsset_Internal(name, type);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	[TypeInferenceRule(TypeInferenceRules.TypeReferencedBySecondArgument)]
	private extern Object LoadAsset_Internal(string name, Type type);

	public AssetBundleRequest LoadAssetAsync(string name)
	{
		return LoadAssetAsync(name, typeof(Object));
	}

	public AssetBundleRequest LoadAssetAsync<T>(string name)
	{
		return LoadAssetAsync(name, typeof(T));
	}

	public AssetBundleRequest LoadAssetAsync(string name, Type type)
	{
		if (name == null)
		{
			throw new NullReferenceException("The input asset name cannot be null.");
		}
		if (name.Length == 0)
		{
			throw new ArgumentException("The input asset name cannot be empty.");
		}
		if ((object)type == null)
		{
			throw new NullReferenceException("The input type cannot be null.");
		}
		return LoadAssetAsync_Internal(name, type);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private extern AssetBundleRequest LoadAssetAsync_Internal(string name, Type type);

	public Object[] LoadAssetWithSubAssets(string name)
	{
		return LoadAssetWithSubAssets(name, typeof(Object));
	}

	internal static T[] ConvertObjects<T>(Object[] rawObjects) where T : Object
	{
		if (rawObjects == null)
		{
			return null;
		}
		T[] array = new T[rawObjects.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = (T)rawObjects[i];
		}
		return array;
	}

	public T[] LoadAssetWithSubAssets<T>(string name) where T : Object
	{
		return ConvertObjects<T>(LoadAssetWithSubAssets(name, typeof(T)));
	}

	public Object[] LoadAssetWithSubAssets(string name, Type type)
	{
		if (name == null)
		{
			throw new NullReferenceException("The input asset name cannot be null.");
		}
		if (name.Length == 0)
		{
			throw new ArgumentException("The input asset name cannot be empty.");
		}
		if ((object)type == null)
		{
			throw new NullReferenceException("The input type cannot be null.");
		}
		return LoadAssetWithSubAssets_Internal(name, type);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	internal extern Object[] LoadAssetWithSubAssets_Internal(string name, Type type);

	public AssetBundleRequest LoadAssetWithSubAssetsAsync(string name)
	{
		return LoadAssetWithSubAssetsAsync(name, typeof(Object));
	}

	public AssetBundleRequest LoadAssetWithSubAssetsAsync<T>(string name)
	{
		return LoadAssetWithSubAssetsAsync(name, typeof(T));
	}

	public AssetBundleRequest LoadAssetWithSubAssetsAsync(string name, Type type)
	{
		if (name == null)
		{
			throw new NullReferenceException("The input asset name cannot be null.");
		}
		if (name.Length == 0)
		{
			throw new ArgumentException("The input asset name cannot be empty.");
		}
		if ((object)type == null)
		{
			throw new NullReferenceException("The input type cannot be null.");
		}
		return LoadAssetWithSubAssetsAsync_Internal(name, type);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private extern AssetBundleRequest LoadAssetWithSubAssetsAsync_Internal(string name, Type type);

	public Object[] LoadAllAssets()
	{
		return LoadAllAssets(typeof(Object));
	}

	public T[] LoadAllAssets<T>() where T : Object
	{
		return ConvertObjects<T>(LoadAllAssets(typeof(T)));
	}

	public Object[] LoadAllAssets(Type type)
	{
		if ((object)type == null)
		{
			throw new NullReferenceException("The input type cannot be null.");
		}
		return LoadAssetWithSubAssets_Internal("", type);
	}

	public AssetBundleRequest LoadAllAssetsAsync()
	{
		return LoadAllAssetsAsync(typeof(Object));
	}

	public AssetBundleRequest LoadAllAssetsAsync<T>()
	{
		return LoadAllAssetsAsync(typeof(T));
	}

	public AssetBundleRequest LoadAllAssetsAsync(Type type)
	{
		if ((object)type == null)
		{
			throw new NullReferenceException("The input type cannot be null.");
		}
		return LoadAssetWithSubAssetsAsync_Internal("", type);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void Unload(bool unloadAllLoadedObjects);

	[Obsolete("This method is deprecated. Use GetAllAssetNames() instead.")]
	public string[] AllAssetNames()
	{
		return GetAllAssetNames();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern string[] GetAllAssetNames();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern string[] GetAllScenePaths();
}
