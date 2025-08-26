using System.Collections.ObjectModel;
using System.Text;

namespace System.Net.Mail;

/// <summary>Store email addresses that are associated with an email message.</summary>
public class MailAddressCollection : Collection<MailAddress>
{
	/// <summary>Add a list of email addresses to the collection.</summary>
	/// <param name="addresses">The email addresses to add to the <see cref="T:System.Net.Mail.MailAddressCollection" />. Multiple email addresses must be separated with a comma character (",").</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="addresses" /> parameter is null.</exception>
	/// <exception cref="T:System.ArgumentException">The <paramref name="addresses" /> parameter is an empty string.</exception>
	/// <exception cref="T:System.FormatException">The <paramref name="addresses" /> parameter contains an email address that is invalid or not supported.</exception>
	public void Add(string addresses)
	{
		string[] array = addresses.Split(',');
		foreach (string address in array)
		{
			Add(new MailAddress(address));
		}
	}

	/// <summary>Inserts an email address into the <see cref="T:System.Net.Mail.MailAddressCollection" />, at the specified location.</summary>
	/// <param name="index">The location at which to insert the email address that is specified by <paramref name="item" />.</param>
	/// <param name="item">The email address to be inserted into the collection.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="item" /> parameter is null.</exception>
	protected override void InsertItem(int index, MailAddress item)
	{
		if (item == null)
		{
			throw new ArgumentNullException();
		}
		base.InsertItem(index, item);
	}

	/// <summary>Replaces the element at the specified index.</summary>
	/// <param name="index">The index of the email address element to be replaced.</param>
	/// <param name="item">An email address that will replace the element in the collection.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="item" /> parameter is null.</exception>
	protected override void SetItem(int index, MailAddress item)
	{
		if (item == null)
		{
			throw new ArgumentNullException();
		}
		base.SetItem(index, item);
	}

	/// <summary>Returns a string representation of the email addresses in this <see cref="T:System.Net.Mail.MailAddressCollection" /> object.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the email addresses in this collection.</returns>
	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < base.Count; i++)
		{
			if (i > 0)
			{
				stringBuilder.Append(", ");
			}
			stringBuilder.Append(base[i].ToString());
		}
		return stringBuilder.ToString();
	}

	/// <summary>Initializes an empty instance of the <see cref="T:System.Net.Mail.MailAddressCollection" /> class.</summary>
	public MailAddressCollection()
	{
	}
}
