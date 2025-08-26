using System.Collections;
using System.Text;

namespace System.Web.Mail;

internal class MailAddressCollection : IEnumerable
{
	protected ArrayList data = new ArrayList();

	public MailAddress this[int index] => Get(index);

	public int Count => data.Count;

	public void Add(MailAddress addr)
	{
		data.Add(addr);
	}

	public MailAddress Get(int index)
	{
		return (MailAddress)data[index];
	}

	public IEnumerator GetEnumerator()
	{
		return data.GetEnumerator();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < data.Count; i++)
		{
			MailAddress value = Get(i);
			stringBuilder.Append(value);
			if (i != data.Count - 1)
			{
				stringBuilder.Append(",\r\n  ");
			}
		}
		return stringBuilder.ToString();
	}

	public static MailAddressCollection Parse(string str)
	{
		if (str == null)
		{
			throw new ArgumentNullException("Null is not allowed as an address string");
		}
		MailAddressCollection mailAddressCollection = new MailAddressCollection();
		string[] array = str.Split(',', ';');
		for (int i = 0; i < array.Length; i++)
		{
			MailAddress mailAddress = MailAddress.Parse(array[i]);
			if (mailAddress != null)
			{
				mailAddressCollection.Add(mailAddress);
			}
		}
		return mailAddressCollection;
	}
}
