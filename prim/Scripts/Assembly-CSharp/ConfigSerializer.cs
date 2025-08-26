using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

public class ConfigSerializer
{
	public static void SaveConfig(object config, string filePath)
	{
		List<string> list = new List<string>();
		Type type = config.GetType();
		FieldInfo[] fields = type.GetFields();
		for (int i = 0; i < fields.Length; i++)
		{
			Type fieldType = fields[i].FieldType;
			if (fieldType == typeof(float) || fieldType == typeof(int) || fieldType == typeof(bool))
			{
				list.Add(fields[i].Name + "=" + fields[i].GetValue(config).ToString());
			}
			else if (fieldType == typeof(string))
			{
				list.Add(string.Concat(fields[i].Name, "=\"", fields[i].GetValue(config), "\""));
			}
		}
		File.WriteAllLines(filePath, list.ToArray());
	}

	public static bool LoadConfig(object config, string filePath)
	{
		try
		{
			string[] array = File.ReadAllLines(filePath);
			string[] array2 = array;
			foreach (string text in array2)
			{
				string[] array3 = text.Split('=');
				if (array3.Length != 2)
				{
					continue;
				}
				string name = array3[0];
				FieldInfo field = config.GetType().GetField(name);
				if (!(field != null))
				{
					continue;
				}
				Type fieldType = field.FieldType;
				if (fieldType == typeof(float))
				{
					field.SetValue(config, float.Parse(array3[1]));
				}
				else if (fieldType == typeof(int))
				{
					field.SetValue(config, int.Parse(array3[1]));
				}
				else if (fieldType == typeof(bool))
				{
					if (array3[1].Length == 1)
					{
						field.SetValue(config, array3[1] == "1");
					}
					else
					{
						field.SetValue(config, Convert.ToBoolean(array3[1]));
					}
				}
				else if (fieldType == typeof(string))
				{
					field.SetValue(config, array3[1].Trim('"'));
				}
			}
		}
		catch
		{
			return false;
		}
		return true;
	}
}
