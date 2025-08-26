using System.CodeDom;
using System.Web.Services.Protocols;

namespace System.Web.Services.Description;

internal class SoapHttpTransportImporter : SoapTransportImporter
{
	public override bool IsSupportedTransport(string transport)
	{
		return transport == "http://schemas.xmlsoap.org/soap/http";
	}

	public override void ImportClass()
	{
		SoapAddressBinding soapAddressBinding = ((base.ImportContext.Port == null) ? null : ((SoapAddressBinding)base.ImportContext.Port.Extensions.Find(typeof(SoapAddressBinding))));
		if (base.ImportContext.Style == ServiceDescriptionImportStyle.Client)
		{
			base.ImportContext.CodeTypeDeclaration.BaseTypes.Add(typeof(SoapHttpClientProtocol).FullName);
			CodeConstructor codeConstructor = WebCodeGenerator.AddConstructor(base.ImportContext.CodeTypeDeclaration, new string[0], new string[0], null, CodeFlags.IsPublic);
			codeConstructor.Comments.Add(new CodeCommentStatement(Res.GetString("CodeRemarks"), docComment: true));
			bool flag = true;
			if (base.ImportContext is Soap12ProtocolImporter)
			{
				flag = false;
				CodeFieldReferenceExpression right = new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(SoapProtocolVersion)), Enum.Format(typeof(SoapProtocolVersion), SoapProtocolVersion.Soap12, "G"));
				CodeAssignStatement value = new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "SoapVersion"), right);
				codeConstructor.Statements.Add(value);
			}
			ServiceDescription serviceDescription = base.ImportContext.Binding.ServiceDescription;
			string url = soapAddressBinding?.Location;
			string appSettingUrlKey = serviceDescription.AppSettingUrlKey;
			string appSettingBaseUrl = serviceDescription.AppSettingBaseUrl;
			ProtocolImporterUtil.GenerateConstructorStatements(codeConstructor, url, appSettingUrlKey, appSettingBaseUrl, flag && !base.ImportContext.IsEncodedBinding);
		}
		else if (base.ImportContext.Style == ServiceDescriptionImportStyle.Server)
		{
			base.ImportContext.CodeTypeDeclaration.BaseTypes.Add(typeof(WebService).FullName);
		}
	}
}
