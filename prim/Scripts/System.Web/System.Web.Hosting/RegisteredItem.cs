namespace System.Web.Hosting;

internal class RegisteredItem
{
	public IRegisteredObject Item;

	public bool AutoClean;

	public RegisteredItem(IRegisteredObject item, bool autoclean)
	{
		Item = item;
		AutoClean = autoclean;
	}
}
