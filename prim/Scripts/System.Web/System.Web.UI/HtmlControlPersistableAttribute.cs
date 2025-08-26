namespace System.Web.UI;

[AttributeUsage(AttributeTargets.Property)]
internal sealed class HtmlControlPersistableAttribute : Attribute
{
	private bool persist;

	public bool Persist => persist;

	public HtmlControlPersistableAttribute(bool persist)
	{
		this.persist = persist;
	}
}
