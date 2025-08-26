using System;
using System.Diagnostics;

namespace Zenject;

[NoReflectionBaking]
[DebuggerStepThrough]
public class ZenjectException : Exception
{
	public ZenjectException(string message)
		: base(message)
	{
	}

	public ZenjectException(string message, Exception innerException)
		: base(message, innerException)
	{
	}
}
