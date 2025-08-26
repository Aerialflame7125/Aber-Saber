namespace System.Web.Services;

internal sealed class Soap12
{
	internal class Attribute
	{
		internal const string UpgradeEnvelopeQname = "qname";

		internal const string Role = "role";

		internal const string Relay = "relay";

		private Attribute()
		{
		}
	}

	internal sealed class Element
	{
		internal const string Upgrade = "Upgrade";

		internal const string UpgradeEnvelope = "SupportedEnvelope";

		internal const string FaultRole = "Role";

		internal const string FaultReason = "Reason";

		internal const string FaultReasonText = "Text";

		internal const string FaultCode = "Code";

		internal const string FaultNode = "Node";

		internal const string FaultCodeValue = "Value";

		internal const string FaultSubcode = "Subcode";

		internal const string FaultDetail = "Detail";

		private Element()
		{
		}
	}

	internal sealed class Code
	{
		internal const string VersionMismatch = "VersionMismatch";

		internal const string MustUnderstand = "MustUnderstand";

		internal const string DataEncodingUnknown = "DataEncodingUnknown";

		internal const string Sender = "Sender";

		internal const string Receiver = "Receiver";

		internal const string RpcProcedureNotPresentSubcode = "ProcedureNotPresent";

		internal const string RpcBadArgumentsSubcode = "BadArguments";

		internal const string EncodingMissingIDFaultSubcode = "MissingID";

		internal const string EncodingUntypedValueFaultSubcode = "UntypedValue";

		private Code()
		{
		}
	}

	internal const string Namespace = "http://www.w3.org/2003/05/soap-envelope";

	internal const string Encoding = "http://www.w3.org/2003/05/soap-encoding";

	internal const string RpcNamespace = "http://www.w3.org/2003/05/soap-rpc";

	internal const string Prefix = "soap12";

	private Soap12()
	{
	}
}
