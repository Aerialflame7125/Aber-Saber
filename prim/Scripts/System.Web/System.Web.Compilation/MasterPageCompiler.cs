using System.CodeDom;
using System.Web.UI;

namespace System.Web.Compilation;

internal class MasterPageCompiler : UserControlCompiler
{
	private MasterPageParser parser;

	public MasterPageCompiler(MasterPageParser parser)
		: base(parser)
	{
		this.parser = parser;
	}

	protected internal override void CreateMethods()
	{
		base.CreateMethods();
		Type masterType = parser.MasterType;
		if (masterType != null)
		{
			CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
			codeMemberProperty.Name = "Master";
			codeMemberProperty.Type = new CodeTypeReference(parser.MasterType);
			codeMemberProperty.Attributes = (MemberAttributes)24592;
			CodeExpression expression = new CodePropertyReferenceExpression(new CodeBaseReferenceExpression(), "Master");
			expression = new CodeCastExpression(parser.MasterType, expression);
			codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(expression));
			mainClass.Members.Add(codeMemberProperty);
			AddReferencedAssembly(masterType.Assembly);
		}
	}
}
