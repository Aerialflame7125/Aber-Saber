using System.Collections.Generic;
using UnityEngine;

public class LevelMissionParser
{
	public delegate bool ParserFunction(float[] functionParams, int paramCount);

	private Dictionary<string, ParserFunction> _functions;

	public LevelMissionParser()
	{
		_functions = new Dictionary<string, ParserFunction>(10);
	}

	public void AddFunction(string name, ParserFunction function)
	{
		_functions[name] = function;
	}

	public bool Parse(string s)
	{
		return Parse(s, 0, s.Length);
	}

	private bool Parse(string s, int start, int length)
	{
		if (length == 0 || start + length > s.Length)
		{
			Debug.LogError("Bad mission string format.");
			return false;
		}
		if (s[start] == '(')
		{
			if (s[start + length - 1] != ')' || length < 2)
			{
				Debug.LogError("Bad mission string format.");
				return false;
			}
			return Parse(s, start + 1, length - 2);
		}
		int num = 0;
		for (int i = start; i < start + length; i++)
		{
			if (s[i] == ')')
			{
				num--;
			}
			else if (s[i] == '(')
			{
				num++;
			}
			if (num < 0)
			{
				Debug.LogError("Bad mission string format.");
				return false;
			}
			if (num == 0)
			{
				if (s[i] == '&')
				{
					return Parse(s, start, i - start) & Parse(s, i + 1, length - i + start - 1);
				}
				if (s[i] == '|')
				{
					return Parse(s, start, i - start) | Parse(s, i + 1, length - i + start - 1);
				}
			}
		}
		if (s[start] == '!')
		{
			return !Parse(s, start + 1, length - 1);
		}
		return ParseFunction(s, start, length);
	}

	private bool ParseFunction(string s, int start, int length)
	{
		if (length < 3 || start + length > s.Length)
		{
			Debug.LogError("Bad mission string format.");
			return false;
		}
		string key = string.Empty;
		int num = 0;
		for (int i = start; i < start + length; i++)
		{
			if (s[i] == '(')
			{
				key = s.Substring(start, i - start);
				num = i + 1;
			}
		}
		if (num < start + 2 || num + 1 > start + length || s[start + length - 1] != ')')
		{
			Debug.LogError("Bad mission string format.");
			return false;
		}
		float[] array = new float[5];
		int num2 = 0;
		int num3 = num;
		for (int j = num; j < start + length - 1; j++)
		{
			if (s[j] == ',')
			{
				if (num2 + 1 > 5)
				{
					Debug.LogError("Bad mission string format.");
					return false;
				}
				array[num2] = float.Parse(s.Substring(num3, j - num3 + 1));
				num3 = j + 1;
				num2++;
			}
		}
		if (num2 < 5 && start + length - num3 > 1)
		{
			array[num2] = float.Parse(s.Substring(num3, start + length - num3 - 1));
			num2++;
		}
		return _functions[key]?.Invoke(array, num2) ?? true;
	}
}
