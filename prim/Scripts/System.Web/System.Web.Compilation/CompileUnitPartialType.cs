using System.CodeDom;

namespace System.Web.Compilation;

internal class CompileUnitPartialType
{
	public readonly CodeCompileUnit Unit;

	public readonly CodeNamespace ParentNamespace;

	public readonly CodeTypeDeclaration PartialType;

	private string typeName;

	public string TypeName
	{
		get
		{
			if (typeName == null)
			{
				if (ParentNamespace == null || PartialType == null)
				{
					return null;
				}
				typeName = ParentNamespace.Name;
				if (string.IsNullOrEmpty(typeName))
				{
					typeName = PartialType.Name;
				}
				else
				{
					typeName = typeName + "." + PartialType.Name;
				}
			}
			return typeName;
		}
	}

	public CompileUnitPartialType(CodeCompileUnit unit, CodeNamespace parentNamespace, CodeTypeDeclaration type)
	{
		Unit = unit;
		ParentNamespace = parentNamespace;
		PartialType = type;
	}
}
