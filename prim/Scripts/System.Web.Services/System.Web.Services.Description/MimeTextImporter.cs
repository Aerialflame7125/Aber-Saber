using System.CodeDom;
using System.CodeDom.Compiler;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

internal class MimeTextImporter : MimeImporter
{
	private string methodName;

	internal override MimeParameterCollection ImportParameters()
	{
		return null;
	}

	internal override MimeReturn ImportReturn()
	{
		MimeTextBinding mimeTextBinding = (MimeTextBinding)base.ImportContext.OperationBinding.Output.Extensions.Find(typeof(MimeTextBinding));
		if (mimeTextBinding == null)
		{
			return null;
		}
		if (mimeTextBinding.Matches.Count == 0)
		{
			base.ImportContext.UnsupportedOperationBindingWarning(Res.GetString("MissingMatchElement0"));
			return null;
		}
		methodName = CodeIdentifier.MakeValid(base.ImportContext.OperationBinding.Name);
		return new MimeTextReturn
		{
			TypeName = base.ImportContext.ClassNames.AddUnique(methodName + "Matches", mimeTextBinding),
			TextBinding = mimeTextBinding,
			ReaderType = typeof(TextReturnReader)
		};
	}

	internal override void GenerateCode(MimeReturn[] importedReturns, MimeParameterCollection[] importedParameters)
	{
		for (int i = 0; i < importedReturns.Length; i++)
		{
			if (importedReturns[i] is MimeTextReturn)
			{
				GenerateCode((MimeTextReturn)importedReturns[i], base.ImportContext.ServiceImporter.CodeGenerationOptions);
			}
		}
	}

	private void GenerateCode(MimeTextReturn importedReturn, CodeGenerationOptions options)
	{
		GenerateCode(importedReturn.TypeName, importedReturn.TextBinding.Matches, options);
	}

	private void GenerateCode(string typeName, MimeTextMatchCollection matches, CodeGenerationOptions options)
	{
		CodeIdentifiers codeIdentifiers = new CodeIdentifiers();
		CodeTypeDeclaration codeClass = WebCodeGenerator.AddClass(base.ImportContext.CodeNamespace, typeName, string.Empty, new string[0], null, CodeFlags.IsPublic, base.ImportContext.ServiceImporter.CodeGenerator.Supports(GeneratorSupport.PartialTypes));
		string[] array = new string[matches.Count];
		for (int i = 0; i < matches.Count; i++)
		{
			MimeTextMatch mimeTextMatch = matches[i];
			string text = codeIdentifiers.AddUnique(CodeIdentifier.MakeValid((mimeTextMatch.Name.Length == 0) ? (methodName + "Match") : mimeTextMatch.Name), mimeTextMatch);
			CodeAttributeDeclarationCollection metadata = new CodeAttributeDeclarationCollection();
			if (mimeTextMatch.Pattern.Length == 0)
			{
				throw new ArgumentException(Res.GetString("WebTextMatchMissingPattern"));
			}
			CodeExpression codeExpression = new CodePrimitiveExpression(mimeTextMatch.Pattern);
			int num = 0;
			if (mimeTextMatch.Group != 1)
			{
				num++;
			}
			if (mimeTextMatch.Capture != 0)
			{
				num++;
			}
			if (mimeTextMatch.IgnoreCase)
			{
				num++;
			}
			if (mimeTextMatch.Repeats != 1 && mimeTextMatch.Repeats != int.MaxValue)
			{
				num++;
			}
			CodeExpression[] array2 = new CodeExpression[num];
			string[] array3 = new string[array2.Length];
			num = 0;
			if (mimeTextMatch.Group != 1)
			{
				array2[num] = new CodePrimitiveExpression(mimeTextMatch.Group);
				array3[num] = "Group";
				num++;
			}
			if (mimeTextMatch.Capture != 0)
			{
				array2[num] = new CodePrimitiveExpression(mimeTextMatch.Capture);
				array3[num] = "Capture";
				num++;
			}
			if (mimeTextMatch.IgnoreCase)
			{
				array2[num] = new CodePrimitiveExpression(mimeTextMatch.IgnoreCase);
				array3[num] = "IgnoreCase";
				num++;
			}
			if (mimeTextMatch.Repeats != 1 && mimeTextMatch.Repeats != int.MaxValue)
			{
				array2[num] = new CodePrimitiveExpression(mimeTextMatch.Repeats);
				array3[num] = "MaxRepeats";
				num++;
			}
			WebCodeGenerator.AddCustomAttribute(metadata, typeof(MatchAttribute), new CodeExpression[1] { codeExpression }, array3, array2);
			string text2 = ((mimeTextMatch.Matches.Count <= 0) ? typeof(string).FullName : (array[i] = base.ImportContext.ClassNames.AddUnique(CodeIdentifier.MakeValid((mimeTextMatch.Type.Length == 0) ? text : mimeTextMatch.Type), mimeTextMatch)));
			if (mimeTextMatch.Repeats != 1)
			{
				text2 += "[]";
			}
			CodeTypeMember codeTypeMember = WebCodeGenerator.AddMember(codeClass, text2, text, null, metadata, CodeFlags.IsPublic, options);
			if (mimeTextMatch.Matches.Count == 0 && mimeTextMatch.Type.Length > 0)
			{
				base.ImportContext.Warnings |= ServiceDescriptionImportWarnings.OptionalExtensionsIgnored;
				ProtocolImporter.AddWarningComment(codeTypeMember.Comments, Res.GetString("WebTextMatchIgnoredTypeWarning"));
			}
		}
		for (int j = 0; j < array.Length; j++)
		{
			string text3 = array[j];
			if (text3 != null)
			{
				GenerateCode(text3, matches[j].Matches, options);
			}
		}
	}
}
