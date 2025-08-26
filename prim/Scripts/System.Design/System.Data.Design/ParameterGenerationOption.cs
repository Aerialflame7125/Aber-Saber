namespace System.Data.Design;

/// <summary>Sets the type of parameters that are generated in a typed <see cref="T:System.Data.DataSet" /> class.</summary>
public enum ParameterGenerationOption
{
	/// <summary>Parameters in the typed dataset are CLR types.</summary>
	ClrTypes,
	/// <summary>Parameters in the typed dataset are Sql types.</summary>
	SqlTypes,
	/// <summary>Parameters in the typed dataset are all of <see cref="T:System.Object" />.</summary>
	Objects
}
