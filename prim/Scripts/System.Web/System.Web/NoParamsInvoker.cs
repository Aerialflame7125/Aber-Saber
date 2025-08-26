using System.Reflection;

namespace System.Web;

internal sealed class NoParamsInvoker
{
	private EventHandler faked;

	private NoParamsDelegate real;

	public EventHandler FakeDelegate => faked;

	public NoParamsInvoker(object o, MethodInfo method)
	{
		if (method.IsStatic)
		{
			real = (NoParamsDelegate)Delegate.CreateDelegate(typeof(NoParamsDelegate), method);
		}
		else
		{
			real = (NoParamsDelegate)Delegate.CreateDelegate(typeof(NoParamsDelegate), o, method);
		}
		faked = InvokeNoParams;
	}

	private void InvokeNoParams(object o, EventArgs args)
	{
		real();
	}
}
