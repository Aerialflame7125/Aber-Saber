namespace System.Web.Compilation;

internal class BuildManagerRemoveEntryEventArgs : EventArgs
{
	public string EntryName { get; private set; }

	public HttpContext Context { get; private set; }

	public BuildManagerRemoveEntryEventArgs(string entryName, HttpContext context)
	{
		EntryName = entryName;
		Context = context;
	}
}
