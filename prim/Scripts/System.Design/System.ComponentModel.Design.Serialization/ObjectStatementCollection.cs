using System.CodeDom;
using System.Collections;

namespace System.ComponentModel.Design.Serialization;

/// <summary>Holds a table of statements that is offered by the <see cref="T:System.ComponentModel.Design.Serialization.StatementContext" />. This class cannot be inherited.</summary>
public sealed class ObjectStatementCollection : IEnumerable
{
	private Hashtable _statements;

	/// <summary>Gets the statement collection for the given owner.</summary>
	/// <param name="statementOwner">The owner of the statement collection.</param>
	/// <returns>The statement collection for <paramref name="statementOwner" />, or <see langword="null" /> if <paramref name="statementOwner" /> is not in the table.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="statementOwner" /> is <see langword="null" />.</exception>
	public CodeStatementCollection this[object statementOwner] => _statements[statementOwner] as CodeStatementCollection;

	internal ObjectStatementCollection()
	{
		_statements = new Hashtable();
	}

	/// <summary>Determines whether the table contains the given statement owner.</summary>
	/// <param name="statementOwner">The owner of the statement collection.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="statementOwner" /> is in the table; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="statementOwner" /> is <see langword="null" />.</exception>
	public bool ContainsKey(object statementOwner)
	{
		return _statements.ContainsKey(statementOwner);
	}

	/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.ComponentModel.Design.Serialization.ObjectStatementCollection" />.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.ComponentModel.Design.Serialization.ObjectStatementCollection" />.</returns>
	public IDictionaryEnumerator GetEnumerator()
	{
		return _statements.GetEnumerator();
	}

	/// <summary>For a description of this member, see the <see cref="M:System.Collections.IEnumerable.GetEnumerator" /> method.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	/// <summary>Populates the statement table with a statement owner.</summary>
	/// <param name="owner">The statement owner to add to the table.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="owner" /> is <see langword="null" />.</exception>
	public void Populate(object owner)
	{
		if (_statements[owner] == null)
		{
			_statements[owner] = null;
		}
	}

	/// <summary>Populates the statement table with a collection of statement owners.</summary>
	/// <param name="statementOwners">A collection of statement owners to add to the table.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="statementOwner" /> is <see langword="null" />.</exception>
	public void Populate(ICollection statementOwners)
	{
		foreach (object statementOwner in statementOwners)
		{
			Populate(statementOwner);
		}
	}
}
