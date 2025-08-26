using System;
using System.IO.Compression;

public class FileCompressionHelper
{
	public static void CreateZipFromDirectoryAsync(string sourceDirectoryName, string destinationArchiveFileName, Action<bool> finishCallback)
	{
		bool success = false;
		Action job = delegate
		{
			success = CreateZipFromDirectory(sourceDirectoryName, destinationArchiveFileName);
		};
		Action finnish = delegate
		{
			if (finishCallback != null)
			{
				finishCallback(success);
			}
		};
		HMTask hMTask = new HMTask(job, finnish);
		hMTask.Run();
	}

	public static void ExtractZipToDirectoryAsync(string sourceArchiveFileName, string destinationDirectoryName, Action<bool> finishCallback)
	{
		bool success = false;
		Action job = delegate
		{
			success = ExtractZipToDirectory(sourceArchiveFileName, destinationDirectoryName);
		};
		Action finnish = delegate
		{
			if (finishCallback != null)
			{
				finishCallback(success);
			}
		};
		HMTask hMTask = new HMTask(job, finnish);
		hMTask.Run();
	}

	public static bool CreateZipFromDirectory(string sourceDirectoryName, string destinationArchiveFileName)
	{
		try
		{
			ZipFile.CreateFromDirectory(sourceDirectoryName, destinationArchiveFileName, CompressionLevel.Fastest, includeBaseDirectory: false);
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static bool ExtractZipToDirectory(string sourceArchiveFileName, string destinationDirectoryName)
	{
		try
		{
			ZipFile.ExtractToDirectory(sourceArchiveFileName, destinationDirectoryName);
		}
		catch
		{
			return false;
		}
		return true;
	}
}
