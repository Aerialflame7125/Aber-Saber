using System;

namespace JetBrains.Annotations;

[BaseTypeRequired(typeof(Attribute))]
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
internal sealed class BaseTypeRequiredAttribute : Attribute
{
	[NotNull]
	public Type BaseType { get; private set; }

	public BaseTypeRequiredAttribute([NotNull] Type baseType)
	{
		BaseType = baseType;
	}
}
