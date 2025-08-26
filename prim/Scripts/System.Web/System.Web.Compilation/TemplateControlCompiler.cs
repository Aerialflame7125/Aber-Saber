using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace System.Web.Compilation;

internal class TemplateControlCompiler : BaseCompiler
{
	private static BindingFlags noCaseFlags = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

	private static Type monoTypeType = Type.GetType("System.MonoType");

	private TemplateControlParser parser;

	private int dataBoundAtts;

	internal ILocation currentLocation;

	private static TypeConverter colorConverter;

	internal static CodeVariableReferenceExpression ctrlVar = new CodeVariableReferenceExpression("__ctrl");

	private List<string> masterPageContentPlaceHolders;

	private static Regex startsWithBindRegex = new Regex("^Bind\\s*\\(", RegexOptions.IgnoreCase | RegexOptions.Compiled);

	private static Regex bindRegex = new Regex("Bind\\s*\\(\\s*[\"']+(.*?)[\"']+((\\s*,\\s*[\"']+(.*?)[\"']+)?)\\s*\\)\\s*%>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

	private static Regex bindRegexInValue = new Regex("Bind\\s*\\(\\s*[\"']+(.*?)[\"']+((\\s*,\\s*[\"']+(.*?)[\"']+)?)\\s*\\)\\s*$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

	private static Regex evalRegexInValue = new Regex("(.*)Eval\\s*\\(\\s*[\"']+(.*?)[\"']+((\\s*,\\s*[\"']+(.*?)[\"']+)?)\\s*\\)(.*)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

	private List<string> MasterPageContentPlaceHolders
	{
		get
		{
			if (masterPageContentPlaceHolders == null)
			{
				masterPageContentPlaceHolders = new List<string>();
			}
			return masterPageContentPlaceHolders;
		}
	}

	public TemplateControlCompiler(TemplateControlParser parser)
		: base(parser)
	{
		this.parser = parser;
	}

	protected void EnsureID(ControlBuilder builder)
	{
		string iD = builder.ID;
		if (iD == null || iD.Trim() == string.Empty)
		{
			builder.ID = builder.GetNextID(null);
		}
	}

	private void CreateField(ControlBuilder builder, bool check)
	{
		if (builder == null || builder.ID == null || builder.ControlType == null || partialNameOverride[builder.ID] != null)
		{
			return;
		}
		MemberAttributes ma = MemberAttributes.Family;
		currentLocation = builder.Location;
		if (!check || !CheckBaseFieldOrProperty(builder.ID, builder.ControlType, ref ma))
		{
			CodeMemberField codeMemberField = new CodeMemberField(builder.ControlType.FullName, builder.ID);
			codeMemberField.Attributes = ma;
			codeMemberField.Type.Options |= CodeTypeReferenceOptions.GlobalReference;
			if (partialClass != null)
			{
				partialClass.Members.Add(AddLinePragma(codeMemberField, builder));
			}
			else
			{
				mainClass.Members.Add(AddLinePragma(codeMemberField, builder));
			}
		}
	}

	private bool CheckBaseFieldOrProperty(string id, Type type, ref MemberAttributes ma)
	{
		FieldInfo field = parser.BaseType.GetField(id, noCaseFlags);
		Type type2 = null;
		if (field == null || field.IsPrivate)
		{
			PropertyInfo property = parser.BaseType.GetProperty(id, noCaseFlags);
			if (property != null && property.GetSetMethod(nonPublic: true) != null)
			{
				type2 = property.PropertyType;
			}
		}
		else
		{
			type2 = field.FieldType;
		}
		if (type2 == null)
		{
			return false;
		}
		if (!type2.IsAssignableFrom(type))
		{
			ma |= MemberAttributes.New;
			return false;
		}
		return true;
	}

	private void AddParsedSubObjectStmt(ControlBuilder builder, CodeExpression expr)
	{
		if (!builder.HaveParserVariable)
		{
			CodeVariableDeclarationStatement codeVariableDeclarationStatement = new CodeVariableDeclarationStatement();
			codeVariableDeclarationStatement.Name = "__parser";
			codeVariableDeclarationStatement.Type = new CodeTypeReference(typeof(IParserAccessor));
			codeVariableDeclarationStatement.InitExpression = new CodeCastExpression(typeof(IParserAccessor), ctrlVar);
			builder.MethodStatements.Add(codeVariableDeclarationStatement);
			builder.HaveParserVariable = true;
		}
		CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("__parser"), "AddParsedSubObject");
		codeMethodInvokeExpression.Parameters.Add(expr);
		builder.MethodStatements.Add(AddLinePragma(codeMethodInvokeExpression, builder));
	}

	private CodeStatement CreateControlVariable(Type type, ControlBuilder builder, CodeMemberMethod method, CodeTypeReference ctrlTypeRef)
	{
		CodeObjectCreateExpression codeObjectCreateExpression = new CodeObjectCreateExpression(ctrlTypeRef);
		object[] array = ((type != null) ? type.GetCustomAttributes(typeof(ConstructorNeedsTagAttribute), inherit: true) : null);
		if (array != null && array.Length != 0)
		{
			if (((ConstructorNeedsTagAttribute)array[0]).NeedsTag)
			{
				codeObjectCreateExpression.Parameters.Add(new CodePrimitiveExpression(builder.TagName));
			}
		}
		else if (builder is DataBindingBuilder)
		{
			codeObjectCreateExpression.Parameters.Add(new CodePrimitiveExpression(0));
			codeObjectCreateExpression.Parameters.Add(new CodePrimitiveExpression(1));
		}
		method.Statements.Add(new CodeVariableDeclarationStatement(ctrlTypeRef, "__ctrl"));
		return new CodeAssignStatement
		{
			Left = ctrlVar,
			Right = codeObjectCreateExpression
		};
	}

	private void InitMethod(ControlBuilder builder, bool isTemplate, bool childrenAsProperties)
	{
		currentLocation = builder.Location;
		bool flag = builder is RootBuilder;
		string text = (flag ? "Tree" : ("_" + builder.ID));
		CodeMemberMethod codeMemberMethod2 = (builder.Method = new CodeMemberMethod());
		builder.MethodStatements = codeMemberMethod2.Statements;
		codeMemberMethod2.Name = "__BuildControl" + text;
		codeMemberMethod2.Attributes = (MemberAttributes)20482;
		Type controlType = builder.ControlType;
		if (flag)
		{
			SetCustomAttributes(codeMemberMethod2);
			AddStatementsToInitMethodTop(builder, codeMemberMethod2);
		}
		if (builder.HasAspCode)
		{
			CodeMemberMethod codeMemberMethod4 = (builder.RenderMethod = new CodeMemberMethod());
			codeMemberMethod4.Name = "__Render" + text;
			codeMemberMethod4.Attributes = (MemberAttributes)20482;
			CodeParameterDeclarationExpression codeParameterDeclarationExpression = new CodeParameterDeclarationExpression();
			codeParameterDeclarationExpression.Type = new CodeTypeReference(typeof(HtmlTextWriter));
			codeParameterDeclarationExpression.Name = "__output";
			CodeParameterDeclarationExpression codeParameterDeclarationExpression2 = new CodeParameterDeclarationExpression();
			codeParameterDeclarationExpression2.Type = new CodeTypeReference(typeof(Control));
			codeParameterDeclarationExpression2.Name = "parameterContainer";
			codeMemberMethod4.Parameters.Add(codeParameterDeclarationExpression);
			codeMemberMethod4.Parameters.Add(codeParameterDeclarationExpression2);
			mainClass.Members.Add(codeMemberMethod4);
		}
		if (childrenAsProperties || controlType == null)
		{
			bool flag2 = true;
			string text2;
			bool flag3;
			if (builder is RootBuilder)
			{
				text2 = parser.ClassName;
				flag2 = false;
				flag3 = false;
			}
			else
			{
				flag3 = builder.PropertyBuilderShouldReturnValue;
				if (controlType != null && builder.IsProperty && !typeof(ITemplate).IsAssignableFrom(controlType))
				{
					text2 = controlType.FullName;
					flag2 = !controlType.IsPrimitive;
				}
				else
				{
					text2 = "System.Web.UI.Control";
				}
				ProcessTemplateChildren(builder);
			}
			CodeTypeReference codeTypeReference = new CodeTypeReference(text2);
			if (flag2)
			{
				codeTypeReference.Options |= CodeTypeReferenceOptions.GlobalReference;
			}
			if (flag3)
			{
				codeMemberMethod2.ReturnType = codeTypeReference;
				codeMemberMethod2.Statements.Add(CreateControlVariable(controlType, builder, codeMemberMethod2, codeTypeReference));
			}
			else
			{
				codeMemberMethod2.Parameters.Add(new CodeParameterDeclarationExpression(text2, "__ctrl"));
			}
		}
		else
		{
			CodeTypeReference codeTypeReference2 = new CodeTypeReference(controlType.FullName);
			if (!controlType.IsPrimitive)
			{
				codeTypeReference2.Options |= CodeTypeReferenceOptions.GlobalReference;
			}
			if (typeof(Control).IsAssignableFrom(controlType))
			{
				codeMemberMethod2.ReturnType = codeTypeReference2;
			}
			codeMemberMethod2.Statements.Add(AddLinePragma(CreateControlVariable(controlType, builder, codeMemberMethod2, codeTypeReference2), builder));
			CodeFieldReferenceExpression codeFieldReferenceExpression = new CodeFieldReferenceExpression();
			codeFieldReferenceExpression.TargetObject = BaseCompiler.thisRef;
			codeFieldReferenceExpression.FieldName = builder.ID;
			CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
			codeAssignStatement.Left = codeFieldReferenceExpression;
			codeAssignStatement.Right = ctrlVar;
			codeMemberMethod2.Statements.Add(AddLinePragma(codeAssignStatement, builder));
			if (typeof(UserControl).IsAssignableFrom(controlType))
			{
				CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression
				{
					TargetObject = codeFieldReferenceExpression,
					MethodName = "InitializeAsUserControl"
				});
				codeMethodInvokeExpression.Parameters.Add(new CodePropertyReferenceExpression(BaseCompiler.thisRef, "Page"));
				codeMemberMethod2.Statements.Add(codeMethodInvokeExpression);
			}
			if (builder.ParentTemplateBuilder is ContentBuilderInternal)
			{
				PropertyInfo propertyInfo;
				try
				{
					propertyInfo = controlType.GetProperty("TemplateControl");
				}
				catch (Exception)
				{
					propertyInfo = null;
				}
				if (propertyInfo != null && propertyInfo.CanWrite)
				{
					codeAssignStatement = new CodeAssignStatement();
					codeAssignStatement.Left = new CodePropertyReferenceExpression(ctrlVar, "TemplateControl");
					codeAssignStatement.Right = BaseCompiler.thisRef;
					codeMemberMethod2.Statements.Add(codeAssignStatement);
				}
			}
			if (!string.IsNullOrEmpty(builder.GetAttribute("skinid")))
			{
				CreateAssignStatementFromAttribute(builder, "skinid");
			}
			if (typeof(WebControl).IsAssignableFrom(controlType))
			{
				CodeMethodInvokeExpression codeMethodInvokeExpression2 = new CodeMethodInvokeExpression(ctrlVar, "ApplyStyleSheetSkin");
				if (typeof(Page).IsAssignableFrom(parser.BaseType))
				{
					codeMethodInvokeExpression2.Parameters.Add(BaseCompiler.thisRef);
				}
				else
				{
					codeMethodInvokeExpression2.Parameters.Add(new CodePropertyReferenceExpression(BaseCompiler.thisRef, "Page"));
				}
				codeMemberMethod2.Statements.Add(codeMethodInvokeExpression2);
			}
			ProcessTemplateChildren(builder);
			string attribute = builder.GetAttribute("id");
			if (attribute != null && attribute.Length != 0)
			{
				CreateAssignStatementFromAttribute(builder, "id");
			}
			if (typeof(ContentPlaceHolder).IsAssignableFrom(controlType))
			{
				List<string> list = MasterPageContentPlaceHolders;
				string iD = builder.ID;
				if (!list.Contains(iD))
				{
					list.Add(iD);
				}
				string text3 = "__Template_" + iD;
				CodeMemberField codeMemberField = new CodeMemberField(typeof(ITemplate), text3);
				codeMemberField.Attributes = MemberAttributes.Private;
				mainClass.Members.Add(codeMemberField);
				CodeFieldReferenceExpression codeFieldReferenceExpression2 = new CodeFieldReferenceExpression();
				codeFieldReferenceExpression2.TargetObject = BaseCompiler.thisRef;
				codeFieldReferenceExpression2.FieldName = text3;
				CreateContentPlaceHolderTemplateProperty(text3, "Template_" + iD);
				CodeFieldReferenceExpression left = new CodeFieldReferenceExpression
				{
					TargetObject = BaseCompiler.thisRef,
					FieldName = "ContentTemplates"
				};
				CodeIndexerExpression expression = new CodeIndexerExpression
				{
					TargetObject = new CodePropertyReferenceExpression(BaseCompiler.thisRef, "ContentTemplates"),
					Indices = { (CodeExpression)new CodePrimitiveExpression(iD) }
				};
				codeAssignStatement = new CodeAssignStatement
				{
					Left = codeFieldReferenceExpression2,
					Right = new CodeCastExpression(new CodeTypeReference(typeof(ITemplate)), expression)
				};
				CodeConditionStatement value = new CodeConditionStatement(new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(null)), codeAssignStatement);
				codeMemberMethod2.Statements.Add(value);
				CodeMethodInvokeExpression expression2 = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression
				{
					TargetObject = codeFieldReferenceExpression2,
					MethodName = "InstantiateIn"
				}, ctrlVar);
				value = new CodeConditionStatement(new CodeBinaryOperatorExpression(codeFieldReferenceExpression2, CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(null)), new CodeExpressionStatement(expression2));
				codeMemberMethod2.Statements.Add(value);
				builder.MethodStatements = value.FalseStatements;
			}
		}
		if (flag)
		{
			AddStatementsToInitMethodBottom(builder, codeMemberMethod2);
		}
		mainClass.Members.Add(codeMemberMethod2);
	}

	private void ProcessTemplateChildren(ControlBuilder builder)
	{
		ArrayList templateChildren = builder.TemplateChildren;
		if (templateChildren == null || templateChildren.Count <= 0)
		{
			return;
		}
		foreach (TemplateBuilder item in templateChildren)
		{
			CreateControlTree(item, inTemplate: true, childrenAsProperties: false);
			if (item.BindingDirection == BindingDirection.TwoWay)
			{
				string extractMethodName = CreateExtractValuesMethod(item);
				AddBindableTemplateInvocation(builder, item.TagName, item.Method.Name, extractMethodName);
			}
			else
			{
				AddTemplateInvocation(builder, item.TagName, item.Method.Name);
			}
		}
	}

	private void SetCustomAttribute(CodeMemberMethod method, UnknownAttributeDescriptor uad)
	{
		CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
		codeAssignStatement.Left = new CodePropertyReferenceExpression(new CodeArgumentReferenceExpression("__ctrl"), uad.Info.Name);
		codeAssignStatement.Right = GetExpressionFromString(uad.Value.GetType(), uad.Value.ToString(), uad.Info);
		method.Statements.Add(codeAssignStatement);
	}

	private void SetCustomAttributes(CodeMemberMethod method)
	{
		if (parser.BaseType == null)
		{
			return;
		}
		List<UnknownAttributeDescriptor> unknownMainAttributes = parser.UnknownMainAttributes;
		if (unknownMainAttributes == null || unknownMainAttributes.Count == 0)
		{
			return;
		}
		foreach (UnknownAttributeDescriptor item in unknownMainAttributes)
		{
			SetCustomAttribute(method, item);
		}
	}

	protected virtual void AddStatementsToInitMethodTop(ControlBuilder builder, CodeMemberMethod method)
	{
		ClientIDMode? clientIDMode = parser.ClientIDMode;
		if (clientIDMode.HasValue)
		{
			CodeTypeReferenceExpression codeTypeReferenceExpression = new CodeTypeReferenceExpression(typeof(ClientIDMode));
			codeTypeReferenceExpression.Type.Options = CodeTypeReferenceOptions.GlobalReference;
			CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
			codeAssignStatement.Left = new CodePropertyReferenceExpression(BaseCompiler.thisRef, "ClientIDMode");
			codeAssignStatement.Right = new CodeFieldReferenceExpression(codeTypeReferenceExpression, clientIDMode.Value.ToString());
			method.Statements.Add(codeAssignStatement);
		}
	}

	protected virtual void AddStatementsToInitMethodBottom(ControlBuilder builder, CodeMemberMethod method)
	{
	}

	private void AddLiteralSubObject(ControlBuilder builder, string str)
	{
		if (!builder.HasAspCode)
		{
			CodeObjectCreateExpression expr = new CodeObjectCreateExpression(typeof(LiteralControl), new CodePrimitiveExpression(str));
			AddParsedSubObjectStmt(builder, expr);
			return;
		}
		CodeMethodInvokeExpression value = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression
		{
			TargetObject = new CodeArgumentReferenceExpression("__output"),
			MethodName = "Write"
		}, new CodePrimitiveExpression(str));
		builder.RenderMethod.Statements.Add(value);
	}

	private string TrimDB(string value, bool trimTail)
	{
		string text = value.Trim();
		int num = text.Length;
		int num2 = text.IndexOf('#', 2) + 1;
		if (num2 >= num)
		{
			return string.Empty;
		}
		if (trimTail)
		{
			num -= 2;
		}
		return text.Substring(num2, num - num2).Trim();
	}

	private CodeExpression CreateEvalInvokeExpression(Regex regex, string value, bool isBind)
	{
		Match match = regex.Match(value);
		if (!match.Success)
		{
			if (isBind)
			{
				throw new HttpParseException("Bind invocation wasn't formatted properly.");
			}
			return null;
		}
		string value2 = ((!isBind) ? value : SanitizeBindCall(match));
		return new CodeSnippetExpression(value2);
	}

	private string SanitizeBindCall(Match match)
	{
		GroupCollection groups = match.Groups;
		StringBuilder stringBuilder = new StringBuilder(string.Concat("Eval(\"", groups[1], "\""));
		Group group = groups[4];
		if (group != null)
		{
			string value = group.Value;
			if (value != null && value.Length > 0)
			{
				stringBuilder.Append(string.Concat(",\"", group, "\""));
			}
		}
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}

	private string DataBoundProperty(ControlBuilder builder, Type type, string varName, string value)
	{
		value = TrimDB(value, trimTail: true);
		string name = builder.Method.Name + "_DB_" + dataBoundAtts++;
		CodeExpression codeExpression = null;
		value = value.Trim();
		bool flag = false;
		if (startsWithBindRegex.Match(value).Success)
		{
			codeExpression = CreateEvalInvokeExpression(bindRegexInValue, value, isBind: true);
			if (codeExpression != null)
			{
				flag = true;
			}
		}
		else if (StrUtils.StartsWith(value, "Eval", ignore_case: true))
		{
			codeExpression = CreateEvalInvokeExpression(evalRegexInValue, value, isBind: false);
		}
		if (codeExpression == null)
		{
			codeExpression = new CodeSnippetExpression(value);
		}
		CodeMemberMethod codeMemberMethod = CreateDBMethod(builder, name, GetContainerType(builder), builder.ControlType);
		CodeFieldReferenceExpression left = new CodeFieldReferenceExpression(new CodeVariableReferenceExpression("target"), varName);
		CodeExpression right;
		if (type == typeof(string))
		{
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression();
			CodeTypeReferenceExpression targetObject = new CodeTypeReferenceExpression(typeof(Convert));
			codeMethodInvokeExpression.Method = new CodeMethodReferenceExpression(targetObject, "ToString");
			codeMethodInvokeExpression.Parameters.Add(codeExpression);
			right = codeMethodInvokeExpression;
		}
		else
		{
			right = new CodeCastExpression(type, codeExpression);
		}
		CodeAssignStatement codeAssignStatement = new CodeAssignStatement(left, right);
		if (flag)
		{
			CodeConditionStatement value2 = new CodeConditionStatement(new CodeBinaryOperatorExpression(new CodeMethodInvokeExpression(new CodePropertyReferenceExpression(BaseCompiler.thisRef, "Page"), "GetDataItem"), CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(null)), codeAssignStatement);
			codeMemberMethod.Statements.Add(value2);
		}
		else
		{
			codeMemberMethod.Statements.Add(codeAssignStatement);
		}
		mainClass.Members.Add(codeMemberMethod);
		return codeMemberMethod.Name;
	}

	private void AddCodeForPropertyOrField(ControlBuilder builder, Type type, string var_name, string att, MemberInfo member, bool isDataBound, bool isExpression)
	{
		CodeMemberMethod method = builder.Method;
		bool flag = IsWritablePropertyOrField(member);
		if (isDataBound && flag)
		{
			string value = DataBoundProperty(builder, type, var_name, att);
			AddEventAssign(method, builder, "DataBinding", typeof(EventHandler), value);
			return;
		}
		if (isExpression && flag)
		{
			AddExpressionAssign(method, builder, member, type, var_name, att);
			return;
		}
		CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
		codeAssignStatement.Left = new CodePropertyReferenceExpression(ctrlVar, var_name);
		currentLocation = builder.Location;
		codeAssignStatement.Right = GetExpressionFromString(type, att, member);
		method.Statements.Add(AddLinePragma(codeAssignStatement, builder));
	}

	private void RegisterBindingInfo(ControlBuilder builder, string propName, ref string value)
	{
		string text = TrimDB(value, trimTail: false);
		if (!StrUtils.StartsWith(text, "Bind", ignore_case: true))
		{
			return;
		}
		Match match = bindRegex.Match(text);
		if (!match.Success)
		{
			return;
		}
		string value2 = match.Groups[1].Value;
		TemplateBuilder parentTemplateBuilder = builder.ParentTemplateBuilder;
		if (parentTemplateBuilder == null)
		{
			throw new HttpException("Bind expression not allowed in this context.");
		}
		if (parentTemplateBuilder.BindingDirection != 0)
		{
			string attribute = builder.GetAttribute("ID");
			if (string.IsNullOrEmpty(attribute))
			{
				throw new HttpException(string.Concat("Control of type '", builder.ControlType, "' using two-way binding on property '", propName, "' must have an ID."));
			}
			parentTemplateBuilder.RegisterBoundProperty(builder.ControlType, propName, attribute, value2);
		}
	}

	private static bool InvariantCompareNoCase(string a, string b)
	{
		return string.Compare(a, b, ignoreCase: true, Helpers.InvariantCulture) == 0;
	}

	internal static MemberInfo GetFieldOrProperty(Type type, string name)
	{
		MemberInfo memberInfo = null;
		try
		{
			memberInfo = type.GetProperty(name, noCaseFlags & ~BindingFlags.NonPublic);
		}
		catch
		{
		}
		if (memberInfo != null)
		{
			return memberInfo;
		}
		try
		{
			memberInfo = type.GetField(name, noCaseFlags & ~BindingFlags.NonPublic);
		}
		catch
		{
		}
		return memberInfo;
	}

	private static bool IsWritablePropertyOrField(MemberInfo member)
	{
		PropertyInfo propertyInfo = member as PropertyInfo;
		if (propertyInfo != null)
		{
			return propertyInfo.GetSetMethod(nonPublic: false) != null;
		}
		FieldInfo fieldInfo = member as FieldInfo;
		if (fieldInfo != null)
		{
			return !fieldInfo.IsInitOnly;
		}
		throw new ArgumentException("Argument must be of PropertyInfo or FieldInfo type", "member");
	}

	private bool ProcessPropertiesAndFields(ControlBuilder builder, MemberInfo member, string id, string attValue, string prefix)
	{
		int num = id.IndexOf('-');
		bool num2 = member is PropertyInfo;
		bool flag = BaseParser.IsDataBound(attValue);
		bool isExpression = !flag && BaseParser.IsExpression(attValue);
		Type type = ((!num2) ? ((FieldInfo)member).FieldType : ((PropertyInfo)member).PropertyType);
		if (InvariantCompareNoCase(member.Name, id))
		{
			if (flag)
			{
				RegisterBindingInfo(builder, member.Name, ref attValue);
			}
			if (!IsWritablePropertyOrField(member))
			{
				return false;
			}
			AddCodeForPropertyOrField(builder, type, member.Name, attValue, member, flag, isExpression);
			return true;
		}
		if (num == -1)
		{
			return false;
		}
		string[] array = id.Replace('-', '.').Split('.');
		int num3 = array.Length;
		if (num3 < 2 || !InvariantCompareNoCase(member.Name, array[0]))
		{
			return false;
		}
		if (num3 > 2)
		{
			MemberInfo fieldOrProperty = GetFieldOrProperty(type, array[1]);
			if (fieldOrProperty == null)
			{
				return false;
			}
			string prefix2 = prefix + member.Name + ".";
			string id2 = id.Substring(num + 1);
			return ProcessPropertiesAndFields(builder, fieldOrProperty, id2, attValue, prefix2);
		}
		MemberInfo fieldOrProperty2 = GetFieldOrProperty(type, array[1]);
		if (!(fieldOrProperty2 is PropertyInfo))
		{
			return false;
		}
		PropertyInfo propertyInfo = (PropertyInfo)fieldOrProperty2;
		if (!propertyInfo.CanWrite)
		{
			return false;
		}
		bool flag2 = propertyInfo.PropertyType == typeof(bool);
		if (!flag2 && attValue == null)
		{
			return false;
		}
		string att = attValue;
		if (attValue == null && flag2)
		{
			att = "true";
		}
		if (flag)
		{
			RegisterBindingInfo(builder, prefix + member.Name + "." + propertyInfo.Name, ref attValue);
		}
		AddCodeForPropertyOrField(builder, propertyInfo.PropertyType, prefix + member.Name + "." + propertyInfo.Name, att, propertyInfo, flag, isExpression);
		return true;
	}

	internal CodeExpression CompileExpression(MemberInfo member, Type type, string value, bool useSetAttribute)
	{
		value = value.Substring(3, value.Length - 5).Trim();
		int num = value.IndexOf(':');
		if (num == -1)
		{
			return null;
		}
		string text = value.Substring(0, num).Trim();
		string text2 = value.Substring(num + 1).Trim();
		CompilationSection compilationSection = (CompilationSection)WebConfigurationManager.GetWebApplicationSection("system.web/compilation");
		if (compilationSection == null)
		{
			return null;
		}
		if (compilationSection.ExpressionBuilders == null || compilationSection.ExpressionBuilders.Count == 0)
		{
			return null;
		}
		System.Web.Configuration.ExpressionBuilder expressionBuilder = compilationSection.ExpressionBuilders[text];
		if (expressionBuilder == null)
		{
			return null;
		}
		string type2 = expressionBuilder.Type;
		Type type3;
		try
		{
			type3 = HttpApplication.LoadType(type2, throwOnMissing: true);
		}
		catch (Exception innerException)
		{
			throw new HttpException($"Failed to load expression builder type `{type2}'", innerException);
		}
		if (!typeof(ExpressionBuilder).IsAssignableFrom(type3))
		{
			throw new HttpException($"Type {type2} is not descendant from System.Web.Compilation.ExpressionBuilder");
		}
		ExpressionBuilder expressionBuilder2 = null;
		ExpressionBuilderContext context;
		object parsedData;
		try
		{
			expressionBuilder2 = Activator.CreateInstance(type3) as ExpressionBuilder;
			context = new ExpressionBuilderContext(HttpContext.Current.Request.FilePath);
			parsedData = expressionBuilder2.ParseExpression(text2, type, context);
		}
		catch (Exception innerException2)
		{
			throw new HttpException($"Failed to create an instance of type `{type2}'", innerException2);
		}
		BoundPropertyEntry entry = CreateBoundPropertyEntry(member as PropertyInfo, text, text2, useSetAttribute);
		return expressionBuilder2.GetCodeExpression(entry, parsedData, context);
	}

	private void AddExpressionAssign(CodeMemberMethod method, ControlBuilder builder, MemberInfo member, Type type, string name, string value)
	{
		CodeExpression codeExpression = CompileExpression(member, type, value, useSetAttribute: false);
		if (codeExpression != null)
		{
			CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
			codeAssignStatement.Left = new CodePropertyReferenceExpression(ctrlVar, name);
			TypeCode typeCode = Type.GetTypeCode(type);
			if (typeCode != 0 && typeCode != TypeCode.Object && typeCode != TypeCode.DBNull)
			{
				codeAssignStatement.Right = CreateConvertToCall(typeCode, codeExpression);
			}
			else
			{
				codeAssignStatement.Right = new CodeCastExpression(type, codeExpression);
			}
			builder.Method.Statements.Add(AddLinePragma(codeAssignStatement, builder));
		}
	}

	internal static CodeMethodInvokeExpression CreateConvertToCall(TypeCode typeCode, CodeExpression expr)
	{
		CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression();
		string methodName = typeCode switch
		{
			TypeCode.Boolean => "ToBoolean", 
			TypeCode.Char => "ToChar", 
			TypeCode.SByte => "ToSByte", 
			TypeCode.Byte => "ToByte", 
			TypeCode.Int16 => "ToInt16", 
			TypeCode.UInt16 => "ToUInt16", 
			TypeCode.Int32 => "ToInt32", 
			TypeCode.UInt32 => "ToUInt32", 
			TypeCode.Int64 => "ToInt64", 
			TypeCode.UInt64 => "ToUInt64", 
			TypeCode.Single => "ToSingle", 
			TypeCode.Double => "ToDouble", 
			TypeCode.Decimal => "ToDecimal", 
			TypeCode.DateTime => "ToDateTime", 
			TypeCode.String => "ToString", 
			_ => throw new InvalidOperationException($"Unsupported TypeCode '{typeCode}'"), 
		};
		CodeTypeReferenceExpression codeTypeReferenceExpression = new CodeTypeReferenceExpression(typeof(Convert));
		codeTypeReferenceExpression.Type.Options = CodeTypeReferenceOptions.GlobalReference;
		codeMethodInvokeExpression.Method = new CodeMethodReferenceExpression(codeTypeReferenceExpression, methodName);
		codeMethodInvokeExpression.Parameters.Add(expr);
		codeMethodInvokeExpression.Parameters.Add(new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(typeof(CultureInfo)), "CurrentCulture"));
		return codeMethodInvokeExpression;
	}

	private BoundPropertyEntry CreateBoundPropertyEntry(PropertyInfo pi, string prefix, string expr, bool useSetAttribute)
	{
		BoundPropertyEntry boundPropertyEntry = new BoundPropertyEntry();
		boundPropertyEntry.Expression = expr;
		boundPropertyEntry.ExpressionPrefix = prefix;
		boundPropertyEntry.Generated = false;
		if (pi != null)
		{
			boundPropertyEntry.Name = pi.Name;
			boundPropertyEntry.PropertyInfo = pi;
			boundPropertyEntry.Type = pi.PropertyType;
		}
		boundPropertyEntry.UseSetAttribute = useSetAttribute;
		return boundPropertyEntry;
	}

	private bool ResourceProviderHasObject(string key)
	{
		IResourceProvider resourceProvider = HttpContext.GetResourceProvider(base.InputVirtualPath.Absolute, isLocal: true);
		if (resourceProvider == null)
		{
			return false;
		}
		IResourceReader resourceReader = resourceProvider.ResourceReader;
		if (resourceReader == null)
		{
			return false;
		}
		try
		{
			IDictionaryEnumerator enumerator = resourceReader.GetEnumerator();
			if (enumerator == null)
			{
				return false;
			}
			while (enumerator.MoveNext())
			{
				string text = enumerator.Key as string;
				if (!string.IsNullOrEmpty(text) && string.Compare(key, text, StringComparison.Ordinal) == 0)
				{
					return true;
				}
			}
		}
		finally
		{
			resourceReader.Close();
		}
		return false;
	}

	private void AssignPropertyFromResources(ControlBuilder builder, MemberInfo mi, string attvalue)
	{
		bool flag = mi.MemberType == MemberTypes.Property;
		bool flag2 = !flag && mi.MemberType == MemberTypes.Field;
		if ((!flag && !flag2) || !IsWritablePropertyOrField(mi))
		{
			return;
		}
		object[] customAttributes = mi.GetCustomAttributes(typeof(LocalizableAttribute), inherit: true);
		if (customAttributes != null && customAttributes.Length != 0 && !((LocalizableAttribute)customAttributes[0]).IsLocalizable)
		{
			return;
		}
		string name = mi.Name;
		string text = attvalue + "." + name;
		if (!ResourceProviderHasObject(text))
		{
			return;
		}
		string inputFile = parser.InputFile;
		string physicalApplicationPath = HttpContext.Current.Request.PhysicalApplicationPath;
		if (StrUtils.StartsWith(inputFile, physicalApplicationPath))
		{
			string appDomainAppVirtualPath = HttpRuntime.AppDomainAppVirtualPath;
			inputFile = parser.InputFile.Substring(physicalApplicationPath.Length - 1);
			if (appDomainAppVirtualPath != "/")
			{
				inputFile = appDomainAppVirtualPath + inputFile;
			}
			char directorySeparatorChar = Path.DirectorySeparatorChar;
			if (directorySeparatorChar != '/')
			{
				inputFile = inputFile.Replace(directorySeparatorChar, '/');
			}
			if (HttpContext.GetLocalResourceObject(inputFile, text) != null && (flag || flag2))
			{
				CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
				codeAssignStatement.Left = new CodePropertyReferenceExpression(ctrlVar, name);
				codeAssignStatement.Right = ResourceExpressionBuilder.CreateGetLocalResourceObject(mi, text);
				builder.Method.Statements.Add(AddLinePragma(codeAssignStatement, builder));
			}
		}
	}

	private void AssignPropertiesFromResources(ControlBuilder builder, Type controlType, string attvalue)
	{
		FieldInfo[] fields = controlType.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
		PropertyInfo[] properties = controlType.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
		FieldInfo[] array = fields;
		foreach (FieldInfo mi in array)
		{
			AssignPropertyFromResources(builder, mi, attvalue);
		}
		PropertyInfo[] array2 = properties;
		foreach (PropertyInfo mi2 in array2)
		{
			AssignPropertyFromResources(builder, mi2, attvalue);
		}
	}

	private void AssignPropertiesFromResources(ControlBuilder builder, string attvalue)
	{
		if (attvalue != null && attvalue.Length != 0)
		{
			Type controlType = builder.ControlType;
			if (!(controlType == null))
			{
				AssignPropertiesFromResources(builder, controlType, attvalue);
			}
		}
	}

	private void AddEventAssign(CodeMemberMethod method, ControlBuilder builder, string name, Type type, string value)
	{
		CodeEventReferenceExpression eventRef = new CodeEventReferenceExpression(ctrlVar, name);
		CodeDelegateCreateExpression listener = new CodeDelegateCreateExpression(new CodeTypeReference(type), BaseCompiler.thisRef, value);
		CodeAttachEventStatement value2 = new CodeAttachEventStatement(eventRef, listener);
		method.Statements.Add(value2);
	}

	private void CreateAssignStatementFromAttribute(ControlBuilder builder, string id)
	{
		EventInfo[] array = null;
		Type controlType = builder.ControlType;
		string attribute = builder.GetAttribute(id);
		if (id.Length > 2 && string.Compare(id.Substring(0, 2), "ON", ignoreCase: true, Helpers.InvariantCulture) == 0)
		{
			if (array == null)
			{
				array = controlType.GetEvents();
			}
			string b = id.Substring(2);
			EventInfo[] array2 = array;
			foreach (EventInfo eventInfo in array2)
			{
				if (InvariantCompareNoCase(eventInfo.Name, b))
				{
					AddEventAssign(builder.Method, builder, eventInfo.Name, eventInfo.EventHandlerType, attribute);
					return;
				}
			}
		}
		if (string.Compare(id, "meta:resourcekey", StringComparison.OrdinalIgnoreCase) == 0)
		{
			AssignPropertiesFromResources(builder, attribute);
			return;
		}
		int num = id.IndexOf('-');
		string name = id;
		if (num != -1)
		{
			name = id.Substring(0, num);
		}
		MemberInfo fieldOrProperty = GetFieldOrProperty(controlType, name);
		if (fieldOrProperty != null && ProcessPropertiesAndFields(builder, fieldOrProperty, id, attribute, null))
		{
			return;
		}
		if (!typeof(IAttributeAccessor).IsAssignableFrom(controlType))
		{
			throw new ParseException(builder.Location, "Unrecognized attribute: " + id);
		}
		CodeMemberMethod method = builder.Method;
		bool num2 = BaseParser.IsDataBound(attribute);
		bool flag = !num2 && BaseParser.IsExpression(attribute);
		if (num2)
		{
			string text = attribute.Substring(3, attribute.Length - 5).Trim();
			CodeExpression codeExpression = null;
			if (startsWithBindRegex.Match(text).Success)
			{
				codeExpression = CreateEvalInvokeExpression(bindRegexInValue, text, isBind: true);
			}
			else if (StrUtils.StartsWith(text, "Eval", ignore_case: true))
			{
				codeExpression = CreateEvalInvokeExpression(evalRegexInValue, text, isBind: false);
			}
			if (codeExpression == null && text != null && text.Trim() != string.Empty)
			{
				codeExpression = new CodeSnippetExpression(text);
			}
			CreateDBAttributeMethod(builder, id, codeExpression);
		}
		else
		{
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeCastExpression(typeof(IAttributeAccessor), ctrlVar), "SetAttribute"));
			codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(id));
			CodeExpression codeExpression2 = null;
			if (flag)
			{
				codeExpression2 = CompileExpression(null, typeof(string), attribute, useSetAttribute: true);
			}
			if (codeExpression2 == null)
			{
				codeExpression2 = new CodePrimitiveExpression(attribute);
			}
			codeMethodInvokeExpression.Parameters.Add(codeExpression2);
			method.Statements.Add(AddLinePragma(codeMethodInvokeExpression, builder));
		}
	}

	protected void CreateAssignStatementsFromAttributes(ControlBuilder builder)
	{
		dataBoundAtts = 0;
		IDictionary attributes = builder.Attributes;
		if (attributes == null || attributes.Count == 0)
		{
			return;
		}
		foreach (string key in attributes.Keys)
		{
			if (!InvariantCompareNoCase(key, "runat") && !InvariantCompareNoCase(key, "id") && !InvariantCompareNoCase(key, "skinid") && !InvariantCompareNoCase(key, "meta:resourcekey"))
			{
				CreateAssignStatementFromAttribute(builder, key);
			}
		}
	}

	private void CreateDBAttributeMethod(ControlBuilder builder, string attr, CodeExpression code)
	{
		if (code != null)
		{
			string nextID = builder.GetNextID(null);
			string text = "__DataBind_" + nextID;
			CodeMemberMethod method = builder.Method;
			AddEventAssign(method, builder, "DataBinding", typeof(EventHandler), text);
			method = (builder.DataBindingMethod = CreateDBMethod(builder, text, GetContainerType(builder), builder.ControlType));
			CodeVariableReferenceExpression expression = new CodeVariableReferenceExpression("target");
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeCastExpression(typeof(IAttributeAccessor), expression), "SetAttribute"));
			codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(attr));
			CodeMethodInvokeExpression codeMethodInvokeExpression2 = new CodeMethodInvokeExpression();
			codeMethodInvokeExpression2.Method = new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(Convert)), "ToString");
			codeMethodInvokeExpression2.Parameters.Add(code);
			codeMethodInvokeExpression.Parameters.Add(codeMethodInvokeExpression2);
			method.Statements.Add(codeMethodInvokeExpression);
			mainClass.Members.Add(method);
		}
	}

	private void AddRenderControl(ControlBuilder builder)
	{
		CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeIndexerExpression
		{
			TargetObject = new CodePropertyReferenceExpression(new CodeArgumentReferenceExpression("parameterContainer"), "Controls"),
			Indices = { (CodeExpression)new CodePrimitiveExpression(builder.RenderIndex) }
		}, "RenderControl");
		codeMethodInvokeExpression.Parameters.Add(new CodeArgumentReferenceExpression("__output"));
		builder.RenderMethod.Statements.Add(codeMethodInvokeExpression);
		builder.IncreaseRenderIndex();
	}

	protected void AddChildCall(ControlBuilder parent, ControlBuilder child)
	{
		if (parent == null || child == null)
		{
			return;
		}
		CodeStatementCollection methodStatements = parent.MethodStatements;
		CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(BaseCompiler.thisRef, child.Method.Name));
		object[] array = null;
		if (child.ControlType != null)
		{
			array = child.ControlType.GetCustomAttributes(typeof(PartialCachingAttribute), inherit: true);
		}
		if (array != null && array.Length != 0)
		{
			PartialCachingAttribute partialCachingAttribute = (PartialCachingAttribute)array[0];
			CodeMethodInvokeExpression codeMethodInvokeExpression2 = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("System.Web.UI.StaticPartialCachingControl"), "BuildCachedControl");
			CodeExpressionCollection parameters = codeMethodInvokeExpression2.Parameters;
			parameters.Add(new CodeArgumentReferenceExpression("__ctrl"));
			parameters.Add(new CodePrimitiveExpression(child.ID));
			if (partialCachingAttribute.Shared)
			{
				parameters.Add(new CodePrimitiveExpression(child.ControlType.GetHashCode().ToString()));
			}
			else
			{
				parameters.Add(new CodePrimitiveExpression(Guid.NewGuid().ToString()));
			}
			parameters.Add(new CodePrimitiveExpression(partialCachingAttribute.Duration));
			parameters.Add(new CodePrimitiveExpression(partialCachingAttribute.VaryByParams));
			parameters.Add(new CodePrimitiveExpression(partialCachingAttribute.VaryByControls));
			parameters.Add(new CodePrimitiveExpression(partialCachingAttribute.VaryByCustom));
			parameters.Add(new CodePrimitiveExpression(partialCachingAttribute.SqlDependency));
			parameters.Add(new CodeDelegateCreateExpression(new CodeTypeReference(typeof(BuildMethod)), BaseCompiler.thisRef, child.Method.Name));
			string providerName = partialCachingAttribute.ProviderName;
			if (!string.IsNullOrEmpty(providerName) && string.Compare("AspNetInternalProvider", providerName, StringComparison.Ordinal) != 0)
			{
				parameters.Add(new CodePrimitiveExpression(providerName));
			}
			else
			{
				parameters.Add(new CodePrimitiveExpression(null));
			}
			methodStatements.Add(AddLinePragma(codeMethodInvokeExpression2, parent));
			if (parent.HasAspCode)
			{
				AddRenderControl(parent);
			}
		}
		else if (child.IsProperty || parent.ChildrenAsProperties)
		{
			if (!child.PropertyBuilderShouldReturnValue)
			{
				codeMethodInvokeExpression.Parameters.Add(new CodeFieldReferenceExpression(ctrlVar, child.TagName));
				parent.MethodStatements.Add(AddLinePragma(codeMethodInvokeExpression, parent));
				return;
			}
			string nextLocalVariableName = parent.GetNextLocalVariableName("__ctrl");
			methodStatements.Add(new CodeVariableDeclarationStatement(child.Method.ReturnType, nextLocalVariableName));
			CodeVariableReferenceExpression codeVariableReferenceExpression = new CodeVariableReferenceExpression(nextLocalVariableName);
			CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
			codeAssignStatement.Left = codeVariableReferenceExpression;
			codeAssignStatement.Right = codeMethodInvokeExpression;
			methodStatements.Add(AddLinePragma(codeAssignStatement, parent));
			codeAssignStatement = new CodeAssignStatement();
			codeAssignStatement.Left = new CodeFieldReferenceExpression(ctrlVar, child.TagName);
			codeAssignStatement.Right = codeVariableReferenceExpression;
			methodStatements.Add(AddLinePragma(codeAssignStatement, parent));
		}
		else
		{
			methodStatements.Add(AddLinePragma(codeMethodInvokeExpression, parent));
			CodeFieldReferenceExpression codeFieldReferenceExpression = new CodeFieldReferenceExpression(BaseCompiler.thisRef, child.ID);
			if (parent.ControlType == null || typeof(IParserAccessor).IsAssignableFrom(parent.ControlType))
			{
				AddParsedSubObjectStmt(parent, codeFieldReferenceExpression);
			}
			else
			{
				CodeMethodInvokeExpression codeMethodInvokeExpression3 = new CodeMethodInvokeExpression(ctrlVar, "Add");
				codeMethodInvokeExpression3.Parameters.Add(codeFieldReferenceExpression);
				methodStatements.Add(AddLinePragma(codeMethodInvokeExpression3, parent));
			}
			if (parent.HasAspCode)
			{
				AddRenderControl(parent);
			}
		}
	}

	private void AddTemplateInvocation(ControlBuilder builder, string name, string methodName)
	{
		CodePropertyReferenceExpression left = new CodePropertyReferenceExpression(ctrlVar, name);
		CodeDelegateCreateExpression value = new CodeDelegateCreateExpression(new CodeTypeReference(typeof(BuildTemplateMethod)), BaseCompiler.thisRef, methodName);
		CodeAssignStatement statement = new CodeAssignStatement(left, new CodeObjectCreateExpression(typeof(CompiledTemplateBuilder))
		{
			Parameters = { (CodeExpression)value }
		});
		builder.Method.Statements.Add(AddLinePragma(statement, builder));
	}

	private void AddBindableTemplateInvocation(ControlBuilder builder, string name, string methodName, string extractMethodName)
	{
		CodePropertyReferenceExpression left = new CodePropertyReferenceExpression(ctrlVar, name);
		CodeDelegateCreateExpression value = new CodeDelegateCreateExpression(new CodeTypeReference(typeof(BuildTemplateMethod)), BaseCompiler.thisRef, methodName);
		CodeDelegateCreateExpression value2 = new CodeDelegateCreateExpression(new CodeTypeReference(typeof(ExtractTemplateValuesMethod)), BaseCompiler.thisRef, extractMethodName);
		CodeAssignStatement statement = new CodeAssignStatement(left, new CodeObjectCreateExpression(typeof(CompiledBindableTemplateBuilder))
		{
			Parameters = 
			{
				(CodeExpression)value,
				(CodeExpression)value2
			}
		});
		builder.Method.Statements.Add(AddLinePragma(statement, builder));
	}

	private string CreateExtractValuesMethod(TemplateBuilder builder)
	{
		CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
		codeMemberMethod.Name = "__ExtractValues_" + builder.ID;
		codeMemberMethod.Attributes = (MemberAttributes)20482;
		codeMemberMethod.ReturnType = new CodeTypeReference(typeof(IOrderedDictionary));
		CodeParameterDeclarationExpression codeParameterDeclarationExpression = new CodeParameterDeclarationExpression();
		codeParameterDeclarationExpression.Type = new CodeTypeReference(typeof(Control));
		codeParameterDeclarationExpression.Name = "__container";
		codeMemberMethod.Parameters.Add(codeParameterDeclarationExpression);
		mainClass.Members.Add(codeMemberMethod);
		CodeObjectCreateExpression codeObjectCreateExpression = new CodeObjectCreateExpression();
		codeObjectCreateExpression.CreateType = new CodeTypeReference(typeof(OrderedDictionary));
		codeMemberMethod.Statements.Add(new CodeVariableDeclarationStatement(typeof(OrderedDictionary), "__table", codeObjectCreateExpression));
		CodeVariableReferenceExpression codeVariableReferenceExpression = new CodeVariableReferenceExpression("__table");
		if (builder.Bindings != null)
		{
			Hashtable hashtable = new Hashtable();
			foreach (TemplateBinding binding in builder.Bindings)
			{
				CodeConditionStatement codeConditionStatement;
				CodeVariableReferenceExpression left;
				CodeAssignStatement codeAssignStatement;
				if (hashtable[binding.ControlId] == null)
				{
					CodeVariableDeclarationStatement value = new CodeVariableDeclarationStatement(binding.ControlType, binding.ControlId);
					codeMemberMethod.Statements.Add(value);
					CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("__container"), "FindControl");
					codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(binding.ControlId));
					codeAssignStatement = new CodeAssignStatement();
					left = (CodeVariableReferenceExpression)(codeAssignStatement.Left = new CodeVariableReferenceExpression(binding.ControlId));
					codeAssignStatement.Right = new CodeCastExpression(binding.ControlType, codeMethodInvokeExpression);
					codeMemberMethod.Statements.Add(codeAssignStatement);
					codeConditionStatement = new CodeConditionStatement();
					codeConditionStatement.Condition = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(null));
					codeMemberMethod.Statements.Add(codeConditionStatement);
					hashtable[binding.ControlId] = codeConditionStatement;
				}
				codeConditionStatement = (CodeConditionStatement)hashtable[binding.ControlId];
				left = new CodeVariableReferenceExpression(binding.ControlId);
				codeAssignStatement = new CodeAssignStatement();
				codeAssignStatement.Left = new CodeIndexerExpression(codeVariableReferenceExpression, new CodePrimitiveExpression(binding.FieldName));
				codeAssignStatement.Right = new CodePropertyReferenceExpression(left, binding.ControlProperty);
				codeConditionStatement.TrueStatements.Add(codeAssignStatement);
			}
		}
		codeMemberMethod.Statements.Add(new CodeMethodReturnStatement(codeVariableReferenceExpression));
		return codeMemberMethod.Name;
	}

	private void AddContentTemplateInvocation(ContentBuilderInternal cbuilder, CodeMemberMethod method, string methodName)
	{
		CodeDelegateCreateExpression value = new CodeDelegateCreateExpression(new CodeTypeReference(typeof(BuildTemplateMethod)), BaseCompiler.thisRef, methodName);
		CodeObjectCreateExpression codeObjectCreateExpression = new CodeObjectCreateExpression(typeof(CompiledTemplateBuilder));
		codeObjectCreateExpression.Parameters.Add(value);
		CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(BaseCompiler.thisRef, "AddContentTemplate");
		codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(cbuilder.ContentPlaceHolderID));
		codeMethodInvokeExpression.Parameters.Add(codeObjectCreateExpression);
		method.Statements.Add(AddLinePragma(codeMethodInvokeExpression, cbuilder));
	}

	private void AddCodeRender(ControlBuilder parent, CodeRenderBuilder cr)
	{
		if (cr.Code != null && !(cr.Code.Trim() == ""))
		{
			if (!cr.IsAssign)
			{
				CodeSnippetStatement statement = new CodeSnippetStatement(cr.Code);
				parent.RenderMethod.Statements.Add(AddLinePragma(statement, cr));
				return;
			}
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression();
			codeMethodInvokeExpression.Method = new CodeMethodReferenceExpression(new CodeArgumentReferenceExpression("__output"), "Write");
			codeMethodInvokeExpression.Parameters.Add(GetWrappedCodeExpression(cr));
			parent.RenderMethod.Statements.Add(AddLinePragma(codeMethodInvokeExpression, cr));
		}
	}

	private CodeExpression GetWrappedCodeExpression(CodeRenderBuilder cr)
	{
		CodeSnippetExpression codeSnippetExpression = new CodeSnippetExpression(cr.Code);
		if (cr.HtmlEncode)
		{
			return new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(HttpUtility)), "HtmlEncode"), codeSnippetExpression);
		}
		return codeSnippetExpression;
	}

	private static Type GetContainerType(ControlBuilder builder)
	{
		return builder.BindingContainerType;
	}

	private CodeMemberMethod CreateDBMethod(ControlBuilder builder, string name, Type container, Type target)
	{
		CodeMemberMethod obj = new CodeMemberMethod
		{
			Attributes = (MemberAttributes)24578,
			Name = name,
			Parameters = 
			{
				new CodeParameterDeclarationExpression(typeof(object), "sender"),
				new CodeParameterDeclarationExpression(typeof(EventArgs), "e")
			}
		};
		CodeTypeReference codeTypeReference = new CodeTypeReference(container);
		CodeTypeReference codeTypeReference2 = new CodeTypeReference(target);
		CodeVariableDeclarationStatement value = new CodeVariableDeclarationStatement
		{
			Name = "Container",
			Type = codeTypeReference
		};
		obj.Statements.Add(value);
		value = new CodeVariableDeclarationStatement
		{
			Name = "target",
			Type = codeTypeReference2
		};
		obj.Statements.Add(value);
		CodeVariableReferenceExpression codeVariableReferenceExpression = new CodeVariableReferenceExpression("target");
		CodeAssignStatement statement = new CodeAssignStatement
		{
			Left = codeVariableReferenceExpression,
			Right = new CodeCastExpression(codeTypeReference2, new CodeArgumentReferenceExpression("sender"))
		};
		obj.Statements.Add(AddLinePragma(statement, builder));
		statement = new CodeAssignStatement
		{
			Left = new CodeVariableReferenceExpression("Container"),
			Right = new CodeCastExpression(codeTypeReference, new CodePropertyReferenceExpression(codeVariableReferenceExpression, "BindingContainer"))
		};
		obj.Statements.Add(AddLinePragma(statement, builder));
		return obj;
	}

	private void AddDataBindingLiteral(ControlBuilder builder, DataBindingBuilder db)
	{
		if (db.Code != null && !(db.Code.Trim() == ""))
		{
			EnsureID(db);
			CreateField(db, check: false);
			string text = "__DataBind_" + db.ID;
			InitMethod(db, isTemplate: false, childrenAsProperties: false);
			CodeMemberMethod method = db.Method;
			AddEventAssign(method, builder, "DataBinding", typeof(EventHandler), text);
			method.Statements.Add(new CodeMethodReturnStatement(ctrlVar));
			method = (builder.DataBindingMethod = CreateDBMethod(builder, text, GetContainerType(builder), typeof(DataBoundLiteralControl)));
			CodeVariableReferenceExpression targetObject = new CodeVariableReferenceExpression("target");
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression();
			codeMethodInvokeExpression.Method = new CodeMethodReferenceExpression(targetObject, "SetDataBoundString");
			codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(0));
			CodeMethodInvokeExpression codeMethodInvokeExpression2 = new CodeMethodInvokeExpression();
			codeMethodInvokeExpression2.Method = new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(Convert)), "ToString");
			codeMethodInvokeExpression2.Parameters.Add(new CodeSnippetExpression(db.Code));
			codeMethodInvokeExpression.Parameters.Add(codeMethodInvokeExpression2);
			method.Statements.Add(AddLinePragma(codeMethodInvokeExpression, builder));
			mainClass.Members.Add(method);
			AddChildCall(builder, db);
		}
	}

	private void FlushText(ControlBuilder builder, StringBuilder sb)
	{
		if (sb.Length > 0)
		{
			AddLiteralSubObject(builder, sb.ToString());
			sb.Length = 0;
		}
	}

	protected void CreateControlTree(ControlBuilder builder, bool inTemplate, bool childrenAsProperties)
	{
		EnsureID(builder);
		bool isTemplate = builder.IsTemplate;
		if (!isTemplate && !inTemplate)
		{
			CreateField(builder, check: true);
		}
		else if (!isTemplate)
		{
			bool check = false;
			bool flag = false;
			ControlBuilder parentBuilder = builder.ParentBuilder;
			while (parentBuilder != null)
			{
				if (!(parentBuilder is TemplateBuilder { TemplateInstance: var templateInstance }))
				{
					parentBuilder = parentBuilder.ParentBuilder;
					continue;
				}
				if (templateInstance == TemplateInstance.Single)
				{
					flag = true;
				}
				break;
			}
			if (!flag)
			{
				builder.ID = builder.GetNextID(null);
			}
			else
			{
				check = true;
			}
			CreateField(builder, check);
		}
		InitMethod(builder, isTemplate, childrenAsProperties);
		if (!isTemplate || builder.GetType() == typeof(RootBuilder))
		{
			CreateAssignStatementsFromAttributes(builder);
		}
		if (builder.Children != null && builder.Children.Count > 0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object child in builder.Children)
			{
				if (child is string)
				{
					stringBuilder.Append((string)child);
					continue;
				}
				FlushText(builder, stringBuilder);
				if (child is ObjectTagBuilder)
				{
					ProcessObjectTag((ObjectTagBuilder)child);
				}
				else if (child is StringPropertyBuilder)
				{
					StringPropertyBuilder stringPropertyBuilder = child as StringPropertyBuilder;
					if (stringPropertyBuilder.Children != null && stringPropertyBuilder.Children.Count > 0)
					{
						StringBuilder stringBuilder2 = new StringBuilder();
						foreach (string child2 in stringPropertyBuilder.Children)
						{
							stringBuilder2.Append(child2);
						}
						CodeMemberMethod method = builder.Method;
						CodeAssignStatement statement = new CodeAssignStatement
						{
							Left = new CodePropertyReferenceExpression(ctrlVar, stringPropertyBuilder.PropertyName),
							Right = new CodePrimitiveExpression(stringBuilder2.ToString())
						};
						method.Statements.Add(AddLinePragma(statement, builder));
					}
				}
				else
				{
					if (child is ContentBuilderInternal)
					{
						ContentBuilderInternal contentBuilderInternal = (ContentBuilderInternal)child;
						CreateControlTree(contentBuilderInternal, inTemplate: false, childrenAsProperties: true);
						AddContentTemplateInvocation(contentBuilderInternal, builder.Method, contentBuilderInternal.Method.Name);
						continue;
					}
					if (!(child is TemplateBuilder))
					{
						if (child is CodeRenderBuilder)
						{
							AddCodeRender(builder, (CodeRenderBuilder)child);
						}
						else
						{
							if (!(child is DataBindingBuilder))
							{
								if (child is ControlBuilder)
								{
									ControlBuilder controlBuilder = (ControlBuilder)child;
									CreateControlTree(controlBuilder, inTemplate, builder.ChildrenAsProperties);
									AddChildCall(builder, controlBuilder);
									continue;
								}
								throw new Exception("???");
							}
							AddDataBindingLiteral(builder, (DataBindingBuilder)child);
						}
					}
				}
				ControlBuilder controlBuilder2 = child as ControlBuilder;
				controlBuilder2.ProcessGeneratedCode(base.CompileUnit, base.BaseType, base.DerivedType, controlBuilder2.Method, controlBuilder2.DataBindingMethod);
			}
			FlushText(builder, stringBuilder);
		}
		ControlBuilder defaultPropertyBuilder = builder.DefaultPropertyBuilder;
		if (defaultPropertyBuilder != null)
		{
			CreateControlTree(defaultPropertyBuilder, inTemplate: false, childrenAsProperties: true);
			AddChildCall(builder, defaultPropertyBuilder);
		}
		if (builder.HasAspCode)
		{
			CodeMemberMethod renderMethod = builder.RenderMethod;
			new CodeMethodReferenceExpression
			{
				TargetObject = BaseCompiler.thisRef,
				MethodName = renderMethod.Name
			};
			CodeDelegateCreateExpression codeDelegateCreateExpression = new CodeDelegateCreateExpression();
			codeDelegateCreateExpression.DelegateType = new CodeTypeReference(typeof(RenderMethod));
			codeDelegateCreateExpression.TargetObject = BaseCompiler.thisRef;
			codeDelegateCreateExpression.MethodName = renderMethod.Name;
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression();
			codeMethodInvokeExpression.Method = new CodeMethodReferenceExpression(ctrlVar, "SetRenderMethodDelegate");
			codeMethodInvokeExpression.Parameters.Add(codeDelegateCreateExpression);
			builder.MethodStatements.Add(codeMethodInvokeExpression);
		}
		if (builder is RootBuilder && !string.IsNullOrEmpty(parser.MetaResourceKey))
		{
			AssignPropertiesFromResources(builder, parser.BaseType, parser.MetaResourceKey);
		}
		if ((!isTemplate || builder is RootBuilder) && !string.IsNullOrEmpty(builder.GetAttribute("meta:resourcekey")))
		{
			CreateAssignStatementFromAttribute(builder, "meta:resourcekey");
		}
		if ((childrenAsProperties && builder.PropertyBuilderShouldReturnValue) || (!childrenAsProperties && typeof(Control).IsAssignableFrom(builder.ControlType)))
		{
			builder.Method.Statements.Add(new CodeMethodReturnStatement(ctrlVar));
		}
		builder.ProcessGeneratedCode(base.CompileUnit, base.BaseType, base.DerivedType, builder.Method, builder.DataBindingMethod);
	}

	protected override void AddStatementsToConstructor(CodeConstructor ctor)
	{
		if (masterPageContentPlaceHolders == null || masterPageContentPlaceHolders.Count == 0)
		{
			return;
		}
		CodeVariableDeclarationStatement codeVariableDeclarationStatement = new CodeVariableDeclarationStatement();
		codeVariableDeclarationStatement.Name = "__contentPlaceHolders";
		codeVariableDeclarationStatement.Type = new CodeTypeReference(typeof(IList));
		codeVariableDeclarationStatement.InitExpression = new CodePropertyReferenceExpression(BaseCompiler.thisRef, "ContentPlaceHolders");
		CodeVariableReferenceExpression targetObject = new CodeVariableReferenceExpression("__contentPlaceHolders");
		CodeStatementCollection statements = ctor.Statements;
		statements.Add(codeVariableDeclarationStatement);
		foreach (string masterPageContentPlaceHolder in masterPageContentPlaceHolders)
		{
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(targetObject, "Add");
			codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(masterPageContentPlaceHolder.ToLowerInvariant()));
			statements.Add(codeMethodInvokeExpression);
		}
	}

	protected internal override void CreateMethods()
	{
		base.CreateMethods();
		CreateProperties();
		CreateControlTree(parser.RootBuilder, inTemplate: false, childrenAsProperties: false);
		CreateFrameworkInitializeMethod();
	}

	protected override void InitializeType()
	{
		List<string> registeredTagNames = parser.RegisteredTagNames;
		RootBuilder rootBuilder = parser.RootBuilder;
		if (rootBuilder == null || registeredTagNames == null || registeredTagNames.Count == 0)
		{
			return;
		}
		foreach (string item in registeredTagNames)
		{
			AspComponent component = rootBuilder.Foundry.GetComponent(item);
			if (component == null || component.Type == null)
			{
				throw new HttpException("Custom control '" + item + "' cannot be found.");
			}
			if (!typeof(UserControl).IsAssignableFrom(component.Type))
			{
				throw new ParseException(parser.Location, "Type '" + component.Type.ToString() + "' does not derive from 'System.Web.UI.UserControl'.");
			}
			AddReferencedAssembly(component.Type.Assembly);
		}
	}

	private void CallBaseFrameworkInitialize(CodeMemberMethod method)
	{
		CodeMethodInvokeExpression value = new CodeMethodInvokeExpression(new CodeBaseReferenceExpression(), "FrameworkInitialize");
		method.Statements.Add(value);
	}

	private void CallSetStringResourcePointer(CodeMemberMethod method)
	{
		CodeFieldReferenceExpression mainClassFieldReferenceExpression = GetMainClassFieldReferenceExpression("__stringResource");
		method.Statements.Add(new CodeMethodInvokeExpression(BaseCompiler.thisRef, "SetStringResourcePointer", mainClassFieldReferenceExpression, new CodePrimitiveExpression(0)));
	}

	private void CreateFrameworkInitializeMethod()
	{
		CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
		codeMemberMethod.Name = "FrameworkInitialize";
		codeMemberMethod.Attributes = (MemberAttributes)12292;
		PrependStatementsToFrameworkInitialize(codeMemberMethod);
		CallBaseFrameworkInitialize(codeMemberMethod);
		CallSetStringResourcePointer(codeMemberMethod);
		AppendStatementsToFrameworkInitialize(codeMemberMethod);
		mainClass.Members.Add(codeMemberMethod);
	}

	protected virtual void PrependStatementsToFrameworkInitialize(CodeMemberMethod method)
	{
	}

	protected virtual void AppendStatementsToFrameworkInitialize(CodeMemberMethod method)
	{
		if (!parser.EnableViewState)
		{
			CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
			codeAssignStatement.Left = new CodePropertyReferenceExpression(BaseCompiler.thisRef, "EnableViewState");
			codeAssignStatement.Right = new CodePrimitiveExpression(false);
			method.Statements.Add(codeAssignStatement);
		}
		CodeMethodInvokeExpression expression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(BaseCompiler.thisRef, "__BuildControlTree"), BaseCompiler.thisRef);
		method.Statements.Add(new CodeExpressionStatement(expression));
	}

	protected override void AddApplicationAndSessionObjects()
	{
		foreach (ObjectTagBuilder applicationObject in GlobalAsaxCompiler.ApplicationObjects)
		{
			CreateFieldForObject(applicationObject.Type, applicationObject.ObjectID);
			CreateApplicationOrSessionPropertyForObject(applicationObject.Type, applicationObject.ObjectID, isApplication: true, isPublic: false);
		}
		foreach (ObjectTagBuilder sessionObject in GlobalAsaxCompiler.SessionObjects)
		{
			CreateApplicationOrSessionPropertyForObject(sessionObject.Type, sessionObject.ObjectID, isApplication: false, isPublic: false);
		}
	}

	protected override void CreateStaticFields()
	{
		base.CreateStaticFields();
		CodeMemberField codeMemberField = new CodeMemberField(typeof(object), "__stringResource");
		codeMemberField.Attributes = (MemberAttributes)20483;
		codeMemberField.InitExpression = new CodePrimitiveExpression(null);
		mainClass.Members.Add(codeMemberField);
	}

	protected void ProcessObjectTag(ObjectTagBuilder tag)
	{
		string fieldName = CreateFieldForObject(tag.Type, tag.ObjectID);
		CreatePropertyForObject(tag.Type, tag.ObjectID, fieldName, isPublic: false);
	}

	private void CreateProperties()
	{
		if (!parser.AutoEventWireup)
		{
			CreateAutoEventWireup();
		}
		else
		{
			CreateAutoHandlers();
		}
		CreateApplicationInstance();
	}

	private void CreateApplicationInstance()
	{
		CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
		Type typeFromHandle = typeof(HttpApplication);
		codeMemberProperty.Type = new CodeTypeReference(typeFromHandle);
		codeMemberProperty.Name = "ApplicationInstance";
		codeMemberProperty.Attributes = (MemberAttributes)12290;
		CodePropertyReferenceExpression targetObject = new CodePropertyReferenceExpression(BaseCompiler.thisRef, "Context");
		targetObject = new CodePropertyReferenceExpression(targetObject, "ApplicationInstance");
		CodeCastExpression expression = new CodeCastExpression(typeFromHandle.FullName, targetObject);
		codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(expression));
		if (partialClass != null)
		{
			partialClass.Members.Add(codeMemberProperty);
		}
		else
		{
			mainClass.Members.Add(codeMemberProperty);
		}
	}

	private void CreateContentPlaceHolderTemplateProperty(string backingField, string name)
	{
		CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
		codeMemberProperty.Type = new CodeTypeReference(typeof(ITemplate));
		codeMemberProperty.Name = name;
		codeMemberProperty.Attributes = MemberAttributes.Public;
		CodeMethodReturnStatement codeMethodReturnStatement = new CodeMethodReturnStatement();
		CodeFieldReferenceExpression left = (CodeFieldReferenceExpression)(codeMethodReturnStatement.Expression = new CodeFieldReferenceExpression(BaseCompiler.thisRef, backingField));
		codeMemberProperty.GetStatements.Add(codeMethodReturnStatement);
		codeMemberProperty.SetStatements.Add(new CodeAssignStatement(left, new CodePropertySetValueReferenceExpression()));
		codeMemberProperty.CustomAttributes.Add(new CodeAttributeDeclaration("TemplateContainer", new CodeAttributeArgument(new CodeTypeOfExpression(new CodeTypeReference(typeof(MasterPage))))));
		CodeFieldReferenceExpression value = new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(TemplateInstance)), "Single");
		codeMemberProperty.CustomAttributes.Add(new CodeAttributeDeclaration("TemplateInstanceAttribute", new CodeAttributeArgument(value)));
		mainClass.Members.Add(codeMemberProperty);
	}

	private void CreateAutoHandlers()
	{
		CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
		codeMemberProperty.Type = new CodeTypeReference(typeof(int));
		codeMemberProperty.Name = "AutoHandlers";
		codeMemberProperty.Attributes = (MemberAttributes)12292;
		CodeMethodReturnStatement codeMethodReturnStatement = new CodeMethodReturnStatement();
		CodeFieldReferenceExpression left = (CodeFieldReferenceExpression)(codeMethodReturnStatement.Expression = new CodeFieldReferenceExpression(mainClassExpr, "__autoHandlers"));
		codeMemberProperty.GetStatements.Add(codeMethodReturnStatement);
		codeMemberProperty.SetStatements.Add(new CodeAssignStatement(left, new CodePropertySetValueReferenceExpression()));
		CodeAttributeDeclaration value = new CodeAttributeDeclaration("System.Obsolete");
		codeMemberProperty.CustomAttributes.Add(value);
		mainClass.Members.Add(codeMemberProperty);
		CodeMemberField codeMemberField = new CodeMemberField(typeof(int), "__autoHandlers");
		codeMemberField.Attributes = (MemberAttributes)20483;
		mainClass.Members.Add(codeMemberField);
	}

	private void CreateAutoEventWireup()
	{
		CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
		codeMemberProperty.Type = new CodeTypeReference(typeof(bool));
		codeMemberProperty.Name = "SupportAutoEvents";
		codeMemberProperty.Attributes = (MemberAttributes)12292;
		codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(false)));
		mainClass.Members.Add(codeMemberProperty);
	}

	protected virtual string HandleUrlProperty(string str, MemberInfo member)
	{
		return str;
	}

	private TypeConverter GetConverterForMember(MemberInfo member)
	{
		TypeDescriptionProvider typeDescriptionProvider = TypeDescriptor.GetProvider(member.ReflectedType);
		if (typeDescriptionProvider == null)
		{
			return null;
		}
		PropertyDescriptorCollection propertyDescriptorCollection = typeDescriptionProvider.GetTypeDescriptor(member.ReflectedType)?.GetProperties();
		if (propertyDescriptorCollection == null || propertyDescriptorCollection.Count == 0)
		{
			return null;
		}
		return propertyDescriptorCollection.Find(member.Name, ignoreCase: false)?.Converter;
	}

	private CodeExpression CreateNullableExpression(Type type, CodeExpression inst, bool nullable)
	{
		if (!nullable)
		{
			return inst;
		}
		return new CodeObjectCreateExpression(type, inst);
	}

	private bool SafeCanConvertFrom(Type type, TypeConverter cvt)
	{
		try
		{
			return cvt.CanConvertFrom(type);
		}
		catch (NotImplementedException)
		{
			return false;
		}
	}

	private bool SafeCanConvertTo(Type type, TypeConverter cvt)
	{
		try
		{
			return cvt.CanConvertTo(type);
		}
		catch (NotImplementedException)
		{
			return false;
		}
	}

	private CodeExpression GetExpressionFromString(Type type, string str, MemberInfo member)
	{
		TypeConverter typeConverter = GetConverterForMember(member);
		if (typeConverter != null && !SafeCanConvertFrom(typeof(string), typeConverter))
		{
			typeConverter = null;
		}
		object obj = null;
		bool flag = false;
		if (typeConverter != null && str != null)
		{
			obj = typeConverter.ConvertFromInvariantString(str);
			if (obj != null)
			{
				type = obj.GetType();
				flag = true;
			}
		}
		bool flag2 = false;
		Type type2 = type;
		if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
		{
			Type[] genericArguments = type.GetGenericArguments();
			type2 = type;
			type = genericArguments[0];
			flag2 = true;
		}
		if (type == typeof(string))
		{
			if (member.GetCustomAttributes(typeof(UrlPropertyAttribute), inherit: true).Length != 0)
			{
				str = HandleUrlProperty((flag && obj is string) ? ((string)obj) : str, member);
			}
			else if (flag)
			{
				return CreateNullableExpression(type2, new CodePrimitiveExpression((string)obj), flag2);
			}
			return CreateNullableExpression(type2, new CodePrimitiveExpression(str), flag2);
		}
		if (type == typeof(bool))
		{
			if (flag)
			{
				return CreateNullableExpression(type2, new CodePrimitiveExpression((bool)obj), flag2);
			}
			if (str == null || str == "" || InvariantCompareNoCase(str, "true"))
			{
				return CreateNullableExpression(type2, new CodePrimitiveExpression(true), flag2);
			}
			if (InvariantCompareNoCase(str, "false"))
			{
				return CreateNullableExpression(type2, new CodePrimitiveExpression(false), flag2);
			}
			if (flag2 && InvariantCompareNoCase(str, "null"))
			{
				return new CodePrimitiveExpression(null);
			}
			throw new ParseException(currentLocation, "Value '" + str + "' is not a valid boolean.");
		}
		if (type == monoTypeType)
		{
			type = typeof(Type);
		}
		if (str == null)
		{
			return new CodePrimitiveExpression(null);
		}
		if (type.IsPrimitive)
		{
			return CreateNullableExpression(type2, new CodePrimitiveExpression(Convert.ChangeType(flag ? obj : str, type, Helpers.InvariantCulture)), flag2);
		}
		if (type == typeof(string[]))
		{
			string[] array = ((!flag) ? str.Split(',') : ((string[])obj));
			CodeArrayCreateExpression codeArrayCreateExpression = new CodeArrayCreateExpression();
			codeArrayCreateExpression.CreateType = new CodeTypeReference(typeof(string));
			string[] array2 = array;
			foreach (string text in array2)
			{
				codeArrayCreateExpression.Initializers.Add(new CodePrimitiveExpression(text.Trim()));
			}
			return CreateNullableExpression(type2, codeArrayCreateExpression, flag2);
		}
		if (type == typeof(Color))
		{
			Color color;
			if (!flag)
			{
				if (colorConverter == null)
				{
					colorConverter = TypeDescriptor.GetConverter(typeof(Color));
				}
				if (str.Trim().Length == 0)
				{
					CodeTypeReferenceExpression targetObject = new CodeTypeReferenceExpression(typeof(Color));
					return CreateNullableExpression(type2, new CodeFieldReferenceExpression(targetObject, "Empty"), flag2);
				}
				try
				{
					if (str.IndexOf(',') == -1)
					{
						color = (Color)colorConverter.ConvertFromString(str);
					}
					else
					{
						int[] array3 = new int[4] { 255, 0, 0, 0 };
						string[] array4 = str.Split(',');
						int num = array4.Length;
						if (num < 3)
						{
							throw new Exception();
						}
						int num2 = ((num != 4) ? 1 : 0);
						for (int num3 = num - 1; num3 >= 0; num3--)
						{
							array3[num2 + num3] = byte.Parse(array4[num3]);
						}
						color = Color.FromArgb(array3[0], array3[1], array3[2], array3[3]);
					}
				}
				catch (Exception inner)
				{
					if (!InvariantCompareNoCase("LightGrey", str))
					{
						throw new ParseException(currentLocation, "Color " + str + " is not a valid color.", inner);
					}
					color = Color.LightGray;
				}
			}
			else
			{
				color = (Color)obj;
			}
			if (color.IsKnownColor)
			{
				CodeFieldReferenceExpression codeFieldReferenceExpression = new CodeFieldReferenceExpression();
				if (color.IsSystemColor)
				{
					type = typeof(SystemColors);
				}
				codeFieldReferenceExpression.TargetObject = new CodeTypeReferenceExpression(type);
				codeFieldReferenceExpression.FieldName = color.Name;
				return CreateNullableExpression(type2, codeFieldReferenceExpression, flag2);
			}
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression
			{
				TargetObject = new CodeTypeReferenceExpression(type),
				MethodName = "FromArgb"
			});
			codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(color.A));
			codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(color.R));
			codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(color.G));
			codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(color.B));
			return CreateNullableExpression(type2, codeMethodInvokeExpression, flag2);
		}
		TypeConverter typeConverter2 = (flag ? typeConverter : (flag2 ? TypeDescriptor.GetConverter(type) : null));
		if (typeConverter2 == null)
		{
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(member.DeclaringType)[member.Name];
			if (propertyDescriptor != null)
			{
				typeConverter2 = propertyDescriptor.Converter;
			}
			else
			{
				Type type3 = member.MemberType switch
				{
					MemberTypes.Field => ((FieldInfo)member).FieldType, 
					MemberTypes.Property => ((PropertyInfo)member).PropertyType, 
					_ => null, 
				};
				if (type3 == null)
				{
					return null;
				}
				typeConverter2 = TypeDescriptor.GetConverter(type3);
			}
		}
		if (flag || (typeConverter2 != null && SafeCanConvertFrom(typeof(string), typeConverter2)))
		{
			object value = (flag ? obj : typeConverter2.ConvertFromInvariantString(str));
			if (SafeCanConvertTo(typeof(InstanceDescriptor), typeConverter2))
			{
				InstanceDescriptor idesc = (InstanceDescriptor)typeConverter2.ConvertTo(value, typeof(InstanceDescriptor));
				if (flag2)
				{
					return CreateNullableExpression(type2, GenerateInstance(idesc, throwOnError: true), flag2);
				}
				CodeExpression codeExpression = GenerateInstance(idesc, throwOnError: true);
				if (type.IsPublic)
				{
					return new CodeCastExpression(type, codeExpression);
				}
				return codeExpression;
			}
			CodeExpression codeExpression2 = GenerateObjectInstance(value, throwOnError: false);
			if (codeExpression2 != null)
			{
				return CreateNullableExpression(type2, codeExpression2, flag2);
			}
			CodeMethodInvokeExpression codeMethodInvokeExpression2 = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression
			{
				TargetObject = new CodeTypeReferenceExpression(typeof(TypeDescriptor)),
				MethodName = "GetConverter"
			});
			CodeTypeReference type4 = new CodeTypeReference(type);
			codeMethodInvokeExpression2.Parameters.Add(new CodeTypeOfExpression(type4));
			codeMethodInvokeExpression2 = new CodeMethodInvokeExpression(codeMethodInvokeExpression2, "ConvertFrom");
			codeMethodInvokeExpression2.Parameters.Add(new CodePrimitiveExpression(str));
			if (flag2)
			{
				return CreateNullableExpression(type2, codeMethodInvokeExpression2, flag2);
			}
			return new CodeCastExpression(type, codeMethodInvokeExpression2);
		}
		Console.WriteLine(string.Concat("Unknown type: ", type, " value: ", str));
		return CreateNullableExpression(type2, new CodePrimitiveExpression(str), flag2);
	}

	private CodeExpression GenerateInstance(InstanceDescriptor idesc, bool throwOnError)
	{
		CodeExpression[] array = new CodeExpression[idesc.Arguments.Count];
		int num = 0;
		foreach (object argument in idesc.Arguments)
		{
			CodeExpression codeExpression = GenerateObjectInstance(argument, throwOnError);
			if (codeExpression == null)
			{
				return null;
			}
			array[num++] = codeExpression;
		}
		return idesc.MemberInfo.MemberType switch
		{
			MemberTypes.Constructor => new CodeObjectCreateExpression(new CodeTypeReference(idesc.MemberInfo.DeclaringType), array), 
			MemberTypes.Method => new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(idesc.MemberInfo.DeclaringType), idesc.MemberInfo.Name, array), 
			MemberTypes.Field => new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(idesc.MemberInfo.DeclaringType), idesc.MemberInfo.Name), 
			MemberTypes.Property => new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(idesc.MemberInfo.DeclaringType), idesc.MemberInfo.Name), 
			_ => throw new ParseException(currentLocation, "Invalid instance type."), 
		};
	}

	private CodeExpression GenerateObjectInstance(object value, bool throwOnError)
	{
		if (value == null)
		{
			return new CodePrimitiveExpression(null);
		}
		if (value is Type)
		{
			return new CodeTypeOfExpression(new CodeTypeReference(value.ToString()));
		}
		Type type = value.GetType();
		if (type.IsPrimitive || value is string)
		{
			return new CodePrimitiveExpression(value);
		}
		if (type.IsArray)
		{
			Array array = (Array)value;
			CodeExpression[] array2 = new CodeExpression[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				CodeExpression codeExpression = GenerateObjectInstance(array.GetValue(i), throwOnError);
				if (codeExpression == null)
				{
					return null;
				}
				array2[i] = codeExpression;
			}
			return new CodeArrayCreateExpression(new CodeTypeReference(type), array2);
		}
		TypeConverter converter = TypeDescriptor.GetConverter(type);
		if (converter != null && converter.CanConvertTo(typeof(InstanceDescriptor)))
		{
			InstanceDescriptor idesc = (InstanceDescriptor)converter.ConvertTo(value, typeof(InstanceDescriptor));
			return GenerateInstance(idesc, throwOnError);
		}
		InstanceDescriptor defaultInstanceDescriptor = GetDefaultInstanceDescriptor(value);
		if (defaultInstanceDescriptor != null)
		{
			return GenerateInstance(defaultInstanceDescriptor, throwOnError);
		}
		if (throwOnError)
		{
			throw new ParseException(currentLocation, "Cannot generate an instance for the type: " + type);
		}
		return null;
	}

	private InstanceDescriptor GetDefaultInstanceDescriptor(object value)
	{
		if (value is Unit unit)
		{
			if (unit.IsEmpty)
			{
				return new InstanceDescriptor(typeof(Unit).GetField("Empty"), null);
			}
			return new InstanceDescriptor(typeof(Unit).GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, new Type[2]
			{
				typeof(double),
				typeof(UnitType)
			}, null), new object[2] { unit.Value, unit.Type });
		}
		if (value is FontUnit fontUnit)
		{
			if (fontUnit.IsEmpty)
			{
				return new InstanceDescriptor(typeof(FontUnit).GetField("Empty"), null);
			}
			Type type = null;
			object obj = null;
			FontSize type2 = fontUnit.Type;
			if ((uint)type2 <= 1u)
			{
				type = typeof(Unit);
				obj = fontUnit.Unit;
			}
			else
			{
				type = typeof(string);
				obj = fontUnit.Type.ToString();
			}
			ConstructorInfo constructor = typeof(FontUnit).GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, new Type[1] { type }, null);
			if (constructor != null)
			{
				return new InstanceDescriptor(constructor, new object[1] { obj });
			}
		}
		return null;
	}
}
