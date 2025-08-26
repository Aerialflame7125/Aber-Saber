using System.CodeDom;
using System.Configuration;

namespace System.Web.Services.Description;

internal class ProtocolImporterUtil
{
	private ProtocolImporterUtil()
	{
	}

	internal static void GenerateConstructorStatements(CodeConstructor ctor, string url, string appSettingUrlKey, string appSettingBaseUrl, bool soap11)
	{
		bool flag = url != null && url.Length > 0;
		bool flag2 = appSettingUrlKey != null && appSettingUrlKey.Length > 0;
		CodeAssignStatement codeAssignStatement = null;
		if (!flag && !flag2)
		{
			return;
		}
		CodePropertyReferenceExpression left = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Url");
		if (flag)
		{
			CodeExpression right = new CodePrimitiveExpression(url);
			codeAssignStatement = new CodeAssignStatement(left, right);
		}
		if (flag && !flag2)
		{
			ctor.Statements.Add(codeAssignStatement);
		}
		else
		{
			if (!flag2)
			{
				return;
			}
			CodeVariableReferenceExpression codeVariableReferenceExpression = new CodeVariableReferenceExpression("urlSetting");
			CodeExpression right = new CodeIndexerExpression(new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(typeof(ConfigurationManager)), "AppSettings"), new CodePrimitiveExpression(appSettingUrlKey));
			ctor.Statements.Add(new CodeVariableDeclarationStatement(typeof(string), "urlSetting", right));
			if (appSettingBaseUrl == null || appSettingBaseUrl.Length == 0)
			{
				right = codeVariableReferenceExpression;
			}
			else
			{
				if (url == null || url.Length == 0)
				{
					throw new ArgumentException(Res.GetString("IfAppSettingBaseUrlArgumentIsSpecifiedThen0"));
				}
				string value = new Uri(appSettingBaseUrl).MakeRelative(new Uri(url));
				CodeExpression[] parameters = new CodeExpression[2]
				{
					codeVariableReferenceExpression,
					new CodePrimitiveExpression(value)
				};
				right = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(typeof(string)), "Concat", parameters);
			}
			CodeStatement[] trueStatements = new CodeStatement[1]
			{
				new CodeAssignStatement(left, right)
			};
			CodeBinaryOperatorExpression condition = new CodeBinaryOperatorExpression(codeVariableReferenceExpression, CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(null));
			if (flag)
			{
				ctor.Statements.Add(new CodeConditionStatement(condition, trueStatements, new CodeStatement[1] { codeAssignStatement }));
			}
			else
			{
				ctor.Statements.Add(new CodeConditionStatement(condition, trueStatements));
			}
		}
	}
}
