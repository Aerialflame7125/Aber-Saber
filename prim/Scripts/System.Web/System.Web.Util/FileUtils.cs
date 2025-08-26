using System.Diagnostics;
using System.IO;

namespace System.Web.Util;

internal sealed class FileUtils
{
	internal delegate object CreateTempFile(string path);

	private static Random rnd = new Random();

	internal static object CreateTemporaryFile(string tempdir, CreateTempFile createFile)
	{
		return CreateTemporaryFile(tempdir, null, null, createFile);
	}

	internal static object CreateTemporaryFile(string tempdir, string extension, CreateTempFile createFile)
	{
		return CreateTemporaryFile(tempdir, null, extension, createFile);
	}

	internal static object CreateTemporaryFile(string tempdir, string prefix, string extension, CreateTempFile createFile)
	{
		if (tempdir == null || tempdir.Length == 0)
		{
			return null;
		}
		if (createFile == null)
		{
			return null;
		}
		string text = null;
		object obj = null;
		do
		{
			int num;
			lock (rnd)
			{
				num = rnd.Next();
			}
			text = Path.Combine(tempdir, string.Format("{0}{1}{2}", (prefix != null) ? (prefix + ".") : "", num.ToString("x", Helpers.InvariantCulture), (extension != null) ? ("." + extension) : ""));
			try
			{
				obj = createFile(text);
			}
			catch (IOException)
			{
			}
			catch
			{
				throw;
			}
		}
		while (obj == null);
		return obj;
	}

	[Conditional("DEVEL")]
	public static void WriteLineLog(string logFilePath, string format, params object[] parms)
	{
	}

	[Conditional("DEVEL")]
	public static void WriteLog(string logFilePath, string format, params object[] parms)
	{
		using TextWriter textWriter = new StreamWriter((logFilePath != null && logFilePath.Length > 0) ? logFilePath : Path.Combine(Path.GetTempPath(), "System.Web.log"), append: true);
		if (parms != null && parms.Length != 0)
		{
			textWriter.Write(format, parms);
		}
		else
		{
			textWriter.Write(format);
		}
	}
}
