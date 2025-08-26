using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions;

public class AbortPartitionOperationRequest : LdapExtendedOperation
{
	public AbortPartitionOperationRequest(string partitionDN, int flags)
		: base("2.16.840.1.113719.1.27.100.29", null)
	{
		try
		{
			if (partitionDN == null)
			{
				throw new ArgumentException("PARAM_ERROR");
			}
			MemoryStream memoryStream = new MemoryStream();
			LBEREncoder enc = new LBEREncoder();
			Asn1Integer asn1Integer = new Asn1Integer(flags);
			Asn1OctetString asn1OctetString = new Asn1OctetString(partitionDN);
			asn1Integer.encode(enc, memoryStream);
			asn1OctetString.encode(enc, memoryStream);
			setValue(SupportClass.ToSByteArray(memoryStream.ToArray()));
		}
		catch (IOException)
		{
			throw new LdapException("ENCODING_ERROR", 83, null);
		}
	}
}
