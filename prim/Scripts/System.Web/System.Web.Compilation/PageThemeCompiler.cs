using System.CodeDom;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Compilation;

internal class PageThemeCompiler : TemplateControlCompiler
{
	private PageThemeParser parser;

	public PageThemeCompiler(PageThemeParser parser)
		: base(parser)
	{
		this.parser = parser;
	}

	protected internal override void CreateMethods()
	{
		CodeMemberField codeMemberField = new CodeMemberField(typeof(HybridDictionary), "__controlSkins");
		codeMemberField.Attributes = MemberAttributes.Private;
		codeMemberField.InitExpression = new CodeObjectCreateExpression(typeof(HybridDictionary));
		mainClass.Members.Add(codeMemberField);
		CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
		codeMemberProperty.Name = "ControlSkins";
		codeMemberProperty.Attributes = (MemberAttributes)12292;
		codeMemberProperty.Type = new CodeTypeReference(typeof(IDictionary));
		codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodeVariableReferenceExpression("__controlSkins")));
		mainClass.Members.Add(codeMemberProperty);
		codeMemberField = new CodeMemberField(typeof(string[]), "__linkedStyleSheets");
		codeMemberField.Attributes = MemberAttributes.Private;
		codeMemberField.InitExpression = CreateLinkedStyleSheets();
		mainClass.Members.Add(codeMemberField);
		codeMemberProperty = new CodeMemberProperty();
		codeMemberProperty.Name = "LinkedStyleSheets";
		codeMemberProperty.Attributes = (MemberAttributes)12292;
		codeMemberProperty.Type = new CodeTypeReference(typeof(string[]));
		codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodeVariableReferenceExpression("__linkedStyleSheets")));
		mainClass.Members.Add(codeMemberProperty);
		codeMemberProperty = new CodeMemberProperty();
		codeMemberProperty.Name = "AppRelativeTemplateSourceDirectory";
		codeMemberProperty.Attributes = (MemberAttributes)12292;
		codeMemberProperty.Type = new CodeTypeReference(typeof(string));
		codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(VirtualPathUtility.ToAbsolute(parser.BaseVirtualDir))));
		mainClass.Members.Add(codeMemberProperty);
		ControlBuilder rootBuilder = parser.RootBuilder;
		if (rootBuilder.Children == null)
		{
			return;
		}
		foreach (object child in rootBuilder.Children)
		{
			if (child is ControlBuilder && !(child is CodeRenderBuilder))
			{
				ControlBuilder builder = (ControlBuilder)child;
				CreateControlSkinMethod(builder);
			}
		}
	}

	private CodeExpression CreateLinkedStyleSheets()
	{
		string[] linkedStyleSheets = parser.LinkedStyleSheets;
		if (linkedStyleSheets == null)
		{
			return new CodePrimitiveExpression(null);
		}
		CodeExpression[] array = new CodeExpression[linkedStyleSheets.Length];
		for (int i = 0; i < linkedStyleSheets.Length; i++)
		{
			array[i] = new CodePrimitiveExpression(linkedStyleSheets[i]);
		}
		return new CodeArrayCreateExpression(typeof(string), array);
	}

	protected override string HandleUrlProperty(string str, MemberInfo member)
	{
		if (str.StartsWith("~", StringComparison.Ordinal))
		{
			return str;
		}
		return "~/App_Themes/" + UrlUtils.Combine(Path.GetFileName(parser.InputFile), str);
	}

	private void CreateControlSkinMethod(ControlBuilder builder)
	{
		if (builder.ControlType == null)
		{
			return;
		}
		EnsureID(builder);
		CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
		codeMemberMethod.Name = "__BuildControl_" + builder.ID;
		codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(typeof(Control), "ctrl"));
		mainClass.Members.Add(codeMemberMethod);
		builder.Method = codeMemberMethod;
		builder.MethodStatements = codeMemberMethod.Statements;
		codeMemberMethod.ReturnType = new CodeTypeReference(typeof(Control));
		CodeCastExpression right = new CodeCastExpression(builder.ControlType, new CodeVariableReferenceExpression("ctrl"));
		codeMemberMethod.Statements.Add(new CodeVariableDeclarationStatement(builder.ControlType, "__ctrl"));
		CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
		codeAssignStatement.Left = TemplateControlCompiler.ctrlVar;
		codeAssignStatement.Right = right;
		codeMemberMethod.Statements.Add(codeAssignStatement);
		CreateAssignStatementsFromAttributes(builder);
		if (builder.Children != null)
		{
			foreach (object child in builder.Children)
			{
				if (!(child is ControlBuilder))
				{
					continue;
				}
				ControlBuilder controlBuilder = (ControlBuilder)child;
				if (controlBuilder.ControlType == null)
				{
					continue;
				}
				if (controlBuilder is CollectionBuilder)
				{
					PropertyInfo propertyInfo = null;
					try
					{
						propertyInfo = controlBuilder.GetType().GetProperty("Items");
					}
					catch (Exception)
					{
					}
					if (propertyInfo != null)
					{
						CodePropertyReferenceExpression targetObject = new CodePropertyReferenceExpression(new CodePropertyReferenceExpression(TemplateControlCompiler.ctrlVar, controlBuilder.TagName), "Items");
						codeMemberMethod.Statements.Add(new CodeMethodInvokeExpression(targetObject, "Clear"));
					}
				}
				CreateControlTree(controlBuilder, inTemplate: false, builder.ChildrenAsProperties);
				AddChildCall(builder, controlBuilder);
			}
		}
		builder.Method.Statements.Add(new CodeMethodReturnStatement(TemplateControlCompiler.ctrlVar));
	}

	protected override void AddClassAttributes()
	{
		base.AddClassAttributes();
	}

	protected override void CreateStaticFields()
	{
		base.CreateStaticFields();
		ControlBuilder rootBuilder = parser.RootBuilder;
		if (rootBuilder.Children == null)
		{
			return;
		}
		foreach (object child in rootBuilder.Children)
		{
			if (child is string || child is CodeRenderBuilder)
			{
				continue;
			}
			ControlBuilder controlBuilder = (ControlBuilder)child;
			EnsureID(controlBuilder);
			Type controlType = controlBuilder.ControlType;
			if (!(controlType == null))
			{
				string iD = controlBuilder.ID;
				string text = ((controlBuilder.Attributes != null) ? (controlBuilder.Attributes["skinid"] as string) : null);
				if (text == null)
				{
					text = "";
				}
				CodeMemberField codeMemberField = new CodeMemberField(typeof(object), "__BuildControl_" + iD + "_skinKey");
				codeMemberField.Attributes = (MemberAttributes)20483;
				codeMemberField.InitExpression = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(typeof(PageTheme)), "CreateSkinKey", new CodeTypeOfExpression(controlType), new CodePrimitiveExpression(text));
				mainClass.Members.Add(codeMemberField);
			}
		}
	}

	protected override void CreateConstructor(CodeStatementCollection localVars, CodeStatementCollection trueStmt)
	{
		ControlBuilder rootBuilder = parser.RootBuilder;
		if (rootBuilder.Children == null)
		{
			return;
		}
		foreach (object child in rootBuilder.Children)
		{
			if (child is string || child is CodeRenderBuilder)
			{
				continue;
			}
			ControlBuilder controlBuilder = (ControlBuilder)child;
			Type controlType = controlBuilder.ControlType;
			if (!(controlType == null))
			{
				string iD = controlBuilder.ID;
				if (localVars == null)
				{
					localVars = new CodeStatementCollection();
				}
				localVars.Add(new CodeAssignStatement(new CodeIndexerExpression(new CodePropertyReferenceExpression(BaseCompiler.thisRef, "__controlSkins"), new CodeVariableReferenceExpression("__BuildControl_" + iD + "_skinKey")), new CodeObjectCreateExpression(typeof(ControlSkin), new CodeTypeOfExpression(controlType), new CodeDelegateCreateExpression(new CodeTypeReference(typeof(ControlSkinDelegate)), BaseCompiler.thisRef, "__BuildControl_" + iD))));
			}
		}
		base.CreateConstructor(localVars, trueStmt);
	}
}
