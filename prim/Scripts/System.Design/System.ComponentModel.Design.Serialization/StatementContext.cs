namespace System.ComponentModel.Design.Serialization;

/// <summary>Provides a location into which statements can be serialized. This class cannot be inherited.</summary>
public sealed class StatementContext
{
	private ObjectStatementCollection _statements;

	/// <summary>Gets a collection of statements offered by the statement context.</summary>
	/// <returns>An <see cref="T:System.ComponentModel.Design.Serialization.ObjectStatementCollection" /> containing statements offered by the statement context.</returns>
	public ObjectStatementCollection StatementCollection
	{
		get
		{
			if (_statements == null)
			{
				_statements = new ObjectStatementCollection();
			}
			return _statements;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.StatementContext" /> class.</summary>
	public StatementContext()
	{
	}
}
