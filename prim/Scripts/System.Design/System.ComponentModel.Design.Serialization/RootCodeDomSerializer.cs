using System.CodeDom;
using System.Collections;
using System.Collections.Generic;

namespace System.ComponentModel.Design.Serialization;

internal class RootCodeDomSerializer : CodeDomSerializer
{
	internal class CodeMap
	{
		private string _className;

		private Type _classType;

		private List<CodeMemberField> _fields;

		private CodeStatementCollection _initializers;

		private CodeStatementCollection _begin;

		private CodeStatementCollection _default;

		private CodeStatementCollection _end;

		public CodeMap(Type classType, string className)
		{
			if (classType == null)
			{
				throw new ArgumentNullException("classType");
			}
			if (className == null)
			{
				throw new ArgumentNullException("className");
			}
			_classType = classType;
			_className = className;
			_fields = new List<CodeMemberField>();
			_initializers = new CodeStatementCollection();
			_begin = new CodeStatementCollection();
			_default = new CodeStatementCollection();
			_end = new CodeStatementCollection();
		}

		public void AddField(CodeMemberField field)
		{
			_fields.Add(field);
		}

		public void Add(CodeStatementCollection statements)
		{
			foreach (CodeStatement statement in statements)
			{
				Add(statement);
			}
		}

		public void Add(CodeStatement statement)
		{
			if (statement.UserData["statement-order"] == null)
			{
				_default.Add(statement);
			}
			else if ((string)statement.UserData["statement-order"] == "initializer")
			{
				_initializers.Add(statement);
			}
			else if ((string)statement.UserData["statement-order"] == "begin")
			{
				_begin.Add(statement);
			}
			else if ((string)statement.UserData["statement-order"] == "end")
			{
				_end.Add(statement);
			}
		}

		public CodeTypeDeclaration GenerateClass()
		{
			CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration(_className);
			codeTypeDeclaration.BaseTypes.Add(_classType);
			codeTypeDeclaration.StartDirectives.Add(new CodeRegionDirective(CodeRegionMode.Start, "Windows Form Designer generated code"));
			CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
			codeMemberMethod.Name = "InitializeComponent";
			codeMemberMethod.ReturnType = new CodeTypeReference(typeof(void));
			codeMemberMethod.Attributes = MemberAttributes.Private;
			codeMemberMethod.Statements.AddRange(_initializers);
			codeMemberMethod.Statements.AddRange(_begin);
			codeMemberMethod.Statements.AddRange(_default);
			codeMemberMethod.Statements.AddRange(_end);
			codeTypeDeclaration.Members.Add(codeMemberMethod);
			foreach (CodeMemberField field in _fields)
			{
				codeTypeDeclaration.Members.Add(field);
			}
			codeTypeDeclaration.EndDirectives.Add(new CodeRegionDirective(CodeRegionMode.End, null));
			return codeTypeDeclaration;
		}

		public void Clear()
		{
			_fields.Clear();
			_initializers.Clear();
			_begin.Clear();
			_default.Clear();
			_end.Clear();
		}
	}

	private CodeMap _codeMap;

	public override object Serialize(IDesignerSerializationManager manager, object value)
	{
		if (manager == null)
		{
			throw new ArgumentNullException("manager");
		}
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		if (_codeMap == null)
		{
			_codeMap = new CodeMap(value.GetType(), manager.GetName(value));
		}
		_codeMap.Clear();
		RootContext context = new RootContext(new CodeThisReferenceExpression(), value);
		manager.Context.Push(context);
		SerializeComponents(manager, ((IComponent)value).Site.Container.Components, (IComponent)value);
		CodeStatementCollection codeStatementCollection = new CodeStatementCollection();
		codeStatementCollection.Add(new CodeCommentStatement(string.Empty));
		codeStatementCollection.Add(new CodeCommentStatement(manager.GetName(value)));
		codeStatementCollection.Add(new CodeCommentStatement(string.Empty));
		SerializeProperties(manager, codeStatementCollection, value, new Attribute[0]);
		SerializeEvents(manager, codeStatementCollection, value);
		_codeMap.Add(codeStatementCollection);
		manager.Context.Pop();
		return _codeMap.GenerateClass();
	}

	private void SerializeComponents(IDesignerSerializationManager manager, ICollection components, IComponent rootComponent)
	{
		foreach (IComponent component in components)
		{
			if (component != rootComponent)
			{
				SerializeComponent(manager, component);
			}
		}
	}

	private void SerializeComponent(IDesignerSerializationManager manager, IComponent component)
	{
		CodeDomSerializer serializer = GetSerializer(manager, component);
		if (serializer != null)
		{
			_codeMap.AddField(new CodeMemberField(component.GetType(), manager.GetName(component)));
			if (serializer.Serialize(manager, component) is CodeStatementCollection statements)
			{
				_codeMap.Add(statements);
			}
			if (serializer.Serialize(manager, component) is CodeStatement statement)
			{
				_codeMap.Add(statement);
			}
		}
	}

	public override object Deserialize(IDesignerSerializationManager manager, object codeObject)
	{
		CodeTypeDeclaration codeTypeDeclaration = (CodeTypeDeclaration)codeObject;
		Type type = manager.GetType(codeTypeDeclaration.BaseTypes[0].BaseType);
		object obj = manager.CreateInstance(type, null, codeTypeDeclaration.Name, addToContainer: true);
		RootContext context = new RootContext(new CodeThisReferenceExpression(), obj);
		manager.Context.Push(context);
		foreach (CodeStatement statement in (GetInitializeMethod(codeTypeDeclaration) ?? throw new InvalidOperationException("InitializeComponent method is missing in: " + codeTypeDeclaration.Name)).Statements)
		{
			DeserializeStatement(manager, statement);
		}
		manager.Context.Pop();
		return obj;
	}

	private CodeMemberMethod GetInitializeMethod(CodeTypeDeclaration declaration)
	{
		CodeMemberMethod codeMemberMethod = null;
		foreach (CodeTypeMember member in declaration.Members)
		{
			codeMemberMethod = member as CodeMemberMethod;
			if (codeMemberMethod != null && codeMemberMethod.Name == "InitializeComponent")
			{
				break;
			}
		}
		return codeMemberMethod;
	}
}
