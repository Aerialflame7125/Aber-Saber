namespace UnityEngine.Experimental.UIElements;

public static class UQueryExtensions
{
	public static T Q<T>(this VisualElement e, string name = null, params string[] classes) where T : VisualElement
	{
		return e.Query<T>(name, classes).Build().First();
	}

	public static T Q<T>(this VisualElement e, string name = null, string className = null) where T : VisualElement
	{
		return e.Query<T>(name, className).Build().First();
	}

	public static VisualElement Q(this VisualElement e, string name = null, params string[] classes)
	{
		return e.Query<VisualElement>(name, classes).Build().First();
	}

	public static VisualElement Q(this VisualElement e, string name = null, string className = null)
	{
		return e.Query<VisualElement>(name, className).Build().First();
	}

	public static UQuery.QueryBuilder<VisualElement> Query(this VisualElement e, string name = null, params string[] classes)
	{
		return e.Query<VisualElement>(name, classes);
	}

	public static UQuery.QueryBuilder<VisualElement> Query(this VisualElement e, string name = null, string className = null)
	{
		return e.Query<VisualElement>(name, className);
	}

	public static UQuery.QueryBuilder<T> Query<T>(this VisualElement e, string name = null, params string[] classes) where T : VisualElement
	{
		return new UQuery.QueryBuilder<VisualElement>(e).OfType<T>(name, classes);
	}

	public static UQuery.QueryBuilder<T> Query<T>(this VisualElement e, string name = null, string className = null) where T : VisualElement
	{
		return new UQuery.QueryBuilder<VisualElement>(e).OfType<T>(name, className);
	}

	public static UQuery.QueryBuilder<VisualElement> Query(this VisualElement e)
	{
		return new UQuery.QueryBuilder<VisualElement>(e);
	}
}
