using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ModestTree;

namespace Zenject;

[NoReflectionBaking]
public class ConventionFilterTypesBinder : ConventionAssemblySelectionBinder
{
	public ConventionFilterTypesBinder(ConventionBindInfo bindInfo)
		: base(bindInfo)
	{
	}

	public ConventionFilterTypesBinder DerivingFromOrEqual<T>()
	{
		return DerivingFromOrEqual(typeof(T));
	}

	public ConventionFilterTypesBinder DerivingFromOrEqual(Type parentType)
	{
		base.BindInfo.AddTypeFilter((Type type) => TypeExtensions.DerivesFromOrEqual(type, parentType));
		return this;
	}

	public ConventionFilterTypesBinder DerivingFrom<T>()
	{
		return DerivingFrom(typeof(T));
	}

	public ConventionFilterTypesBinder DerivingFrom(Type parentType)
	{
		base.BindInfo.AddTypeFilter((Type type) => TypeExtensions.DerivesFrom(type, parentType));
		return this;
	}

	public ConventionFilterTypesBinder WithAttribute<T>() where T : Attribute
	{
		return WithAttribute(typeof(T));
	}

	public ConventionFilterTypesBinder WithAttribute(Type attribute)
	{
		Assert.That(TypeExtensions.DerivesFrom<Attribute>(attribute));
		base.BindInfo.AddTypeFilter((Type t) => TypeExtensions.HasAttribute(t, attribute));
		return this;
	}

	public ConventionFilterTypesBinder WithoutAttribute<T>() where T : Attribute
	{
		return WithoutAttribute(typeof(T));
	}

	public ConventionFilterTypesBinder WithoutAttribute(Type attribute)
	{
		Assert.That(TypeExtensions.DerivesFrom<Attribute>(attribute));
		base.BindInfo.AddTypeFilter((Type t) => !TypeExtensions.HasAttribute(t, attribute));
		return this;
	}

	public ConventionFilterTypesBinder WithAttributeWhere<T>(Func<T, bool> predicate) where T : Attribute
	{
		base.BindInfo.AddTypeFilter((Type t) => TypeExtensions.HasAttribute<T>(t) && TypeExtensions.AllAttributes<T>(t).All(predicate));
		return this;
	}

	public ConventionFilterTypesBinder Where(Func<Type, bool> predicate)
	{
		base.BindInfo.AddTypeFilter(predicate);
		return this;
	}

	public ConventionFilterTypesBinder InNamespace(string ns)
	{
		return InNamespaces(ns);
	}

	public ConventionFilterTypesBinder InNamespaces(params string[] namespaces)
	{
		return InNamespaces((IEnumerable<string>)namespaces);
	}

	public ConventionFilterTypesBinder InNamespaces(IEnumerable<string> namespaces)
	{
		base.BindInfo.AddTypeFilter((Type t) => namespaces.Any((string n) => IsInNamespace(t, n)));
		return this;
	}

	public ConventionFilterTypesBinder WithSuffix(string suffix)
	{
		base.BindInfo.AddTypeFilter((Type t) => t.Name.EndsWith(suffix));
		return this;
	}

	public ConventionFilterTypesBinder WithPrefix(string prefix)
	{
		base.BindInfo.AddTypeFilter((Type t) => t.Name.StartsWith(prefix));
		return this;
	}

	public ConventionFilterTypesBinder MatchingRegex(string pattern)
	{
		return MatchingRegex(pattern, RegexOptions.None);
	}

	public ConventionFilterTypesBinder MatchingRegex(string pattern, RegexOptions options)
	{
		return MatchingRegex(new Regex(pattern, options));
	}

	public ConventionFilterTypesBinder MatchingRegex(Regex regex)
	{
		base.BindInfo.AddTypeFilter((Type t) => regex.IsMatch(t.Name));
		return this;
	}

	private static bool IsInNamespace(Type type, string requiredNs)
	{
		string text = type.Namespace ?? string.Empty;
		if (requiredNs.Length > text.Length)
		{
			return false;
		}
		return text.StartsWith(requiredNs) && (text.Length == requiredNs.Length || text[requiredNs.Length] == '.');
	}
}
