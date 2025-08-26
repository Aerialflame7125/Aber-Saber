namespace System.ComponentModel.Design.Data;

/// <summary>Specifies the type of data-store query the design environment should construct.</summary>
public enum QueryBuilderMode
{
	/// <summary>The query being built is a Select query.</summary>
	Select,
	/// <summary>The query being built is an Update query.</summary>
	Update,
	/// <summary>The query being built is an Insert query.</summary>
	Insert,
	/// <summary>The query being built is a Delete query.</summary>
	Delete
}
