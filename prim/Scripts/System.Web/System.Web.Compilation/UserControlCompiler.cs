using System.CodeDom;
using System.Web.UI;

namespace System.Web.Compilation;

internal class UserControlCompiler : TemplateControlCompiler
{
	private UserControlParser parser;

	public UserControlCompiler(UserControlParser parser)
		: base(parser)
	{
		this.parser = parser;
	}

	public static Type CompileUserControlType(UserControlParser parser)
	{
		return new UserControlCompiler(parser).GetCompiledType();
	}

	protected override void AddClassAttributes()
	{
		if (parser.OutputCache)
		{
			AddOutputCacheAttribute();
		}
	}

	protected internal override void CreateMethods()
	{
		base.CreateMethods();
		CreateProfileProperty();
	}

	private void AddOutputCacheAttribute()
	{
		CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration("System.Web.UI.PartialCachingAttribute");
		CodeAttributeArgumentCollection arguments = codeAttributeDeclaration.Arguments;
		AddPrimitiveArgument(arguments, parser.OutputCacheDuration);
		AddPrimitiveArgument(arguments, parser.OutputCacheVaryByParam);
		AddPrimitiveArgument(arguments, parser.OutputCacheVaryByControls);
		AddPrimitiveArgument(arguments, parser.OutputCacheVaryByCustom);
		AddPrimitiveArgument(arguments, parser.OutputCacheSqlDependency);
		AddPrimitiveArgument(arguments, parser.OutputCacheShared);
		arguments.Add(new CodeAttributeArgument("ProviderName", new CodePrimitiveExpression(parser.ProviderName)));
		mainClass.CustomAttributes.Add(codeAttributeDeclaration);
	}

	private void AddPrimitiveArgument(CodeAttributeArgumentCollection arguments, object obj)
	{
		arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(obj)));
	}

	protected override void AddStatementsToInitMethodTop(ControlBuilder builder, CodeMemberMethod method)
	{
		base.AddStatementsToInitMethodTop(builder, method);
		if (parser.MasterPageFile != null)
		{
			CodeExpression left = new CodePropertyReferenceExpression(new CodeArgumentReferenceExpression("__ctrl"), "MasterPageFile");
			CodeExpression right = new CodePrimitiveExpression(parser.MasterPageFile);
			method.Statements.Add(AddLinePragma(new CodeAssignStatement(left, right), parser.DirectiveLocation));
		}
	}
}
