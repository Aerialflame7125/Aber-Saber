using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web.Util;

namespace System.Web;

internal class ExceptionPageTemplateFragment
{
	public string Name { get; set; }

	public string FilePath { get; set; }

	public string ResourceName { get; set; }

	public string ResourceAssembly { get; set; }

	public List<string> MacroNames { get; set; }

	public List<string> RequiredMacros { get; set; }

	public string Value { get; set; }

	public ExceptionPageTemplateType ValidForPageType { get; set; }

	public ExceptionPageTemplateFragment()
	{
		ValidForPageType = ExceptionPageTemplateType.Any;
	}

	public virtual void Init(ExceptionPageTemplateValues values)
	{
		if (values == null)
		{
			throw new ArgumentNullException("values");
		}
		string value = Value;
		if (value != null)
		{
			values.Add(Name, value);
			return;
		}
		value = FilePath;
		if (!string.IsNullOrEmpty(value))
		{
			values.Add(Name, LoadFile(value));
			return;
		}
		value = ResourceName;
		if (!string.IsNullOrEmpty(value))
		{
			values.Add(Name, LoadResource(value));
		}
	}

	public virtual bool Visible(ExceptionPageTemplateValues values)
	{
		List<string> requiredMacros = RequiredMacros;
		if (requiredMacros == null || requiredMacros.Count == 0)
		{
			return true;
		}
		if (values == null || values.Count == 0)
		{
			return false;
		}
		foreach (string item in requiredMacros)
		{
			if (values.Get(item) == null)
			{
				return false;
			}
		}
		return true;
	}

	public string ReplaceMacros(string value, ExceptionPageTemplateValues values)
	{
		if (string.IsNullOrEmpty(value))
		{
			return value;
		}
		if (values == null)
		{
			throw new ArgumentNullException("values");
		}
		List<string> macroNames = MacroNames;
		if (macroNames == null || macroNames.Count == 0)
		{
			return value;
		}
		StringBuilder stringBuilder = new StringBuilder(value);
		foreach (string item in macroNames)
		{
			if (!string.IsNullOrEmpty(item))
			{
				string text = values.Get(item);
				if (text == null)
				{
					text = string.Empty;
				}
				stringBuilder.Replace("@" + item + "@", text);
			}
		}
		return stringBuilder.ToString();
	}

	protected virtual string LoadFile(string path)
	{
		if (!File.Exists(path))
		{
			Console.Error.WriteLine("File '{0}' not found. Required for exception template.", path);
			return string.Empty;
		}
		try
		{
			return File.ReadAllText(path);
		}
		catch (Exception ex)
		{
			Console.Error.WriteLine("Error reading file '{0}'. Required for exception template. Exception {1} has been thrown: {2}", path, ex.GetType(), ex.Message);
			if (RuntimeHelpers.DebuggingEnabled)
			{
				Console.Error.WriteLine(ex.StackTrace);
			}
			return string.Empty;
		}
	}

	protected virtual string LoadResource(string resourceName)
	{
		string resourceAssembly = ResourceAssembly;
		Assembly assembly;
		if (string.IsNullOrEmpty(resourceAssembly))
		{
			assembly = GetType().Assembly;
		}
		else
		{
			try
			{
				assembly = Assembly.Load(resourceAssembly);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine("Unable to load assembly '{0}' needed to retrieve an exception template resource '{1}'. Exception {2} has been thrown: {3}", resourceAssembly, resourceName, ex.GetType(), ex.Message);
				if (RuntimeHelpers.DebuggingEnabled)
				{
					Console.Error.WriteLine(ex.StackTrace);
				}
				return string.Empty;
			}
		}
		try
		{
			Stream manifestResourceStream = assembly.GetManifestResourceStream(resourceName);
			if (manifestResourceStream == null)
			{
				Console.Error.WriteLine("Manifest resource '{0}' required for exception template not found in assembly '{1}'.", resourceName, resourceAssembly);
				return string.Empty;
			}
			using StreamReader streamReader = new StreamReader(manifestResourceStream);
			return streamReader.ReadToEnd();
		}
		catch (Exception ex2)
		{
			Console.Error.WriteLine("Error reading manifest resource '{0}' from assembly '{1}', required for exception template. Exception {2} has been thrown: {3}", resourceName, resourceAssembly, ex2.GetType(), ex2.Message);
			if (RuntimeHelpers.DebuggingEnabled)
			{
				Console.Error.WriteLine(ex2.StackTrace);
			}
			return string.Empty;
		}
	}
}
