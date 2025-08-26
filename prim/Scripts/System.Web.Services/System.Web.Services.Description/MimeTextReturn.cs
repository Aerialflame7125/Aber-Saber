namespace System.Web.Services.Description;

internal class MimeTextReturn : MimeReturn
{
	private MimeTextBinding textBinding;

	internal MimeTextBinding TextBinding
	{
		get
		{
			return textBinding;
		}
		set
		{
			textBinding = value;
		}
	}
}
