namespace System.ComponentModel.Design;

/// <summary>Specifies the type of object-bound smart tag with respect to how it was associated with the component.</summary>
public enum ComponentActionsType
{
	/// <summary>Both types of smart tags.</summary>
	All,
	/// <summary>Pull model smart tags only.</summary>
	Component,
	/// <summary>Push model smart tags only.</summary>
	Service
}
