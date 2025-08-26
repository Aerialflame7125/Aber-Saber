using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251;

public class RfcCompareResponse : RfcLdapResult
{
	[CLSCompliant(false)]
	public RfcCompareResponse(Asn1Decoder dec, Stream in_Renamed, int len)
		: base(dec, in_Renamed, len)
	{
	}

	public RfcCompareResponse(Asn1Enumerated resultCode, RfcLdapDN matchedDN, RfcLdapString errorMessage, RfcReferral referral)
		: base(resultCode, matchedDN, errorMessage, referral)
	{
	}

	public override Asn1Identifier getIdentifier()
	{
		return new Asn1Identifier(1, constructed: true, 15);
	}
}
