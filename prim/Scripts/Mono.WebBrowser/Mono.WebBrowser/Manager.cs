using System;
using System.Reflection;
using Mono.Mozilla;

namespace Mono.WebBrowser;

public sealed class Manager
{
	public static IWebBrowser GetNewInstance()
	{
		return GetNewInstance(Platform.Winforms);
	}

	public static IWebBrowser GetNewInstance(Platform platform)
	{
		string text = Environment.GetEnvironmentVariable("MONO_BROWSER_ENGINE");
		if (text == "webkit")
		{
			try
			{
				return (IWebBrowser)Assembly.LoadWithPartialName("mono-webkit").CreateInstance("Mono.WebKit.WebBrowser");
			}
			catch
			{
				text = null;
			}
		}
		if (text == null || text == "mozilla")
		{
			return new Mono.Mozilla.WebBrowser(platform);
		}
		throw new Exception(Exception.ErrorCodes.EngineNotSupported, text);
	}
}
