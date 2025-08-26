using System.IO;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web;

internal class StaticFileHandler : IHttpHandler
{
	public bool IsReusable => true;

	private static bool ValidFileName(string fileName)
	{
		if (!RuntimeHelpers.RunningOnWindows)
		{
			return true;
		}
		if (fileName == null || fileName.Length == 0)
		{
			return false;
		}
		if (!StrUtils.EndsWith(fileName, " "))
		{
			return !StrUtils.EndsWith(fileName, ".");
		}
		return false;
	}

	public void ProcessRequest(HttpContext context)
	{
		HttpRequest request = context.Request;
		HttpResponse response = context.Response;
		if (HostingEnvironment.HaveCustomVPP)
		{
			VirtualFile virtualFile = null;
			VirtualPathProvider virtualPathProvider = HostingEnvironment.VirtualPathProvider;
			string filePath = request.FilePath;
			if (virtualPathProvider.FileExists(filePath))
			{
				virtualFile = virtualPathProvider.GetFile(filePath);
			}
			if (virtualFile == null)
			{
				throw new HttpException(404, "Path '" + filePath + "' was not found.", filePath);
			}
			response.ContentType = MimeTypes.GetMimeType(filePath);
			response.TransmitFile(virtualFile, final_flush: true);
			return;
		}
		string physicalPath = request.PhysicalPath;
		FileInfo fileInfo = new FileInfo(physicalPath);
		if (!fileInfo.Exists || !ValidFileName(physicalPath))
		{
			throw new HttpException(404, "Path '" + request.FilePath + "' was not found.", request.FilePath);
		}
		if ((fileInfo.Attributes & FileAttributes.Directory) != 0)
		{
			response.Redirect(request.Path + "/");
			return;
		}
		string text = request.Headers["If-Modified-Since"];
		try
		{
			if (text != null)
			{
				DateTime dateTime = DateTime.ParseExact(text, "r", null);
				if (fileInfo.LastWriteTime.ToUniversalTime() <= dateTime)
				{
					response.ContentType = MimeTypes.GetMimeType(physicalPath);
					response.StatusCode = 304;
					return;
				}
			}
		}
		catch
		{
		}
		try
		{
			response.AddHeader("Last-Modified", fileInfo.LastWriteTime.ToUniversalTime().ToString("r"));
			response.ContentType = MimeTypes.GetMimeType(physicalPath);
			response.TransmitFile(physicalPath, final_flush: true);
		}
		catch (Exception)
		{
			throw new HttpException(403, "Forbidden.");
		}
	}
}
