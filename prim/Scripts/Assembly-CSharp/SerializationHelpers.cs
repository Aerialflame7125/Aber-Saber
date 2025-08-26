using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SerializationHelpers
{
	public static T DeserializeData<T>(byte[] data)
	{
		T result = default(T);
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		MemoryStream memoryStream = new MemoryStream(data);
		using (memoryStream)
		{
			try
			{
				result = (T)binaryFormatter.Deserialize(memoryStream);
			}
			catch
			{
				result = default(T);
			}
		}
		return result;
	}

	public static byte[] SerializeObject<T>(T serializableObject)
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		MemoryStream memoryStream = new MemoryStream();
		using (memoryStream)
		{
			binaryFormatter.Serialize(memoryStream, serializableObject);
		}
		return memoryStream.ToArray();
	}

	public static T DeserializeDataFromPlayerPrefs<T>(string key)
	{
		T result = default(T);
		string @string = PlayerPrefs.GetString(key, null);
		if (@string != null)
		{
			byte[] data = Convert.FromBase64String(@string);
			result = DeserializeData<T>(data);
		}
		return result;
	}

	public static void SerializeObjectIntoPlayerPrefs<T>(string key, T serializableObject)
	{
		byte[] inArray = SerializeObject(serializableObject);
		PlayerPrefs.SetString(key, Convert.ToBase64String(inArray));
	}

	public static T DeserializeDataFromFile<T>(string filePath)
	{
		if (!File.Exists(filePath))
		{
			return default(T);
		}
		T val = default(T);
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		try
		{
			FileStream fileStream = File.Open(filePath, FileMode.Open);
			val = (T)binaryFormatter.Deserialize(fileStream);
			fileStream.Close();
		}
		catch
		{
			val = default(T);
		}
		return val;
	}

	public static void SerializeObjectToFile<T>(string filePath, T serializableObject)
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		FileStream fileStream = File.Create(filePath);
		binaryFormatter.Serialize(fileStream, serializableObject);
		fileStream.Close();
	}
}
