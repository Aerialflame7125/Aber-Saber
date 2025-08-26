using System.CodeDom;
using System.Collections;

namespace System.ComponentModel.Design.Serialization;

/// <summary>Serializes an object to a new type.</summary>
public class TypeCodeDomSerializer : CodeDomSerializerBase
{
	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.TypeCodeDomSerializer" /> class.</summary>
	public TypeCodeDomSerializer()
	{
	}

	/// <summary>Serializes the object root by creating a new type declaration that defines root.</summary>
	/// <param name="manager">The serialization manager to use for serialization.</param>
	/// <param name="root">The object to serialize.</param>
	/// <param name="members">Optional collection of members. Can be <see langword="null" /> or empty.</param>
	/// <returns>A <see cref="T:System.CodeDom.CodeTypeDeclaration" /> that defines the root object.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="manager" /> or <paramref name="root" /> is <see langword="null" />.</exception>
	public virtual CodeTypeDeclaration Serialize(IDesignerSerializationManager manager, object root, ICollection members)
	{
		if (root == null)
		{
			throw new ArgumentNullException("root");
		}
		if (manager == null)
		{
			throw new ArgumentNullException("manager");
		}
		RootContext context = new RootContext(new CodeThisReferenceExpression(), root);
		StatementContext statementContext = new StatementContext();
		if (members != null)
		{
			statementContext.StatementCollection.Populate(members);
		}
		statementContext.StatementCollection.Populate(root);
		CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration(manager.GetName(root));
		manager.Context.Push(context);
		manager.Context.Push(statementContext);
		manager.Context.Push(codeTypeDeclaration);
		if (members != null)
		{
			foreach (object member in members)
			{
				SerializeToExpression(manager, member);
			}
		}
		SerializeToExpression(manager, root);
		manager.Context.Pop();
		manager.Context.Pop();
		manager.Context.Pop();
		return codeTypeDeclaration;
	}

	/// <summary>Deserializes the given type declaration.</summary>
	/// <param name="manager">The serialization manager to use for serialization.</param>
	/// <param name="declaration">Type declaration to use for serialization.</param>
	/// <returns>The root object.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="manager" /> or <paramref name="typeDecl" /> is <see langword="null" />.</exception>
	public virtual object Deserialize(IDesignerSerializationManager manager, CodeTypeDeclaration declaration)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns the method where statements used to serialize a member are stored.</summary>
	/// <param name="manager">The serialization manager to use for serialization.</param>
	/// <param name="declaration">The type declaration to use for serialization.</param>
	/// <param name="value">The value to use for serialization.</param>
	/// <returns>The method used to emit all of the initialization code for the given member.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="manager" />, <paramref name="typeDecl" />, or <paramref name="value" /> is <see langword="null" />.</exception>
	protected virtual CodeMemberMethod GetInitializeMethod(IDesignerSerializationManager manager, CodeTypeDeclaration declaration, object value)
	{
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		if (declaration == null)
		{
			throw new ArgumentNullException("declaration");
		}
		if (manager == null)
		{
			throw new ArgumentNullException("manager");
		}
		return new CodeConstructor();
	}

	/// <summary>Returns an array of methods to be interpreted during deserialization.</summary>
	/// <param name="manager">The serialization manager to use for serialization.</param>
	/// <param name="declaration">The type declaration to use for serialization.</param>
	/// <returns>A <see cref="T:System.CodeDom.CodeMemberMethod" /> array of methods to be interpreted during deserialization.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="manager" /> or <paramref name="typeDecl" /> is <see langword="null" />.</exception>
	protected virtual CodeMemberMethod[] GetInitializeMethods(IDesignerSerializationManager manager, CodeTypeDeclaration declaration)
	{
		if (manager == null)
		{
			throw new ArgumentNullException("manager");
		}
		if (declaration == null)
		{
			throw new ArgumentNullException("declaration");
		}
		return new CodeMemberMethod[1]
		{
			new CodeConstructor()
		};
	}
}
