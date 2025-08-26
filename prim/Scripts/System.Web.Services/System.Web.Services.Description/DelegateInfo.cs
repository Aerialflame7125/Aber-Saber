namespace System.Web.Services.Description;

internal class DelegateInfo
{
	internal string handlerType;

	internal string handlerArgs;

	internal DelegateInfo(string handlerType, string handlerArgs)
	{
		this.handlerType = handlerType;
		this.handlerArgs = handlerArgs;
	}
}
