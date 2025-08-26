namespace System.Web.Util;

internal interface ITypedWebObjectFactory : IWebObjectFactory
{
	Type InstantiatedType { get; }
}
