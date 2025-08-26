using System;

namespace UnityEngine.Experimental.UIElements;

public abstract class UxmlFactory<T> : IUxmlFactory where T : VisualElement
{
	public Type CreatesType => typeof(T);

	public VisualElement Create(IUxmlAttributes bag, CreationContext cc)
	{
		return DoCreate(bag, cc);
	}

	protected abstract T DoCreate(IUxmlAttributes bag, CreationContext cc);
}
