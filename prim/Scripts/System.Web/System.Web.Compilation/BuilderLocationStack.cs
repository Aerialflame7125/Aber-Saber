using System.Collections;
using System.Web.UI;

namespace System.Web.Compilation;

internal class BuilderLocationStack : Stack
{
	public ControlBuilder Builder => Peek().Builder;

	public override void Push(object o)
	{
		if (!(o is BuilderLocation))
		{
			throw new InvalidOperationException();
		}
		base.Push(o);
	}

	public virtual void Push(ControlBuilder builder, ILocation location)
	{
		BuilderLocation obj = new BuilderLocation(builder, location);
		Push(obj);
	}

	public new BuilderLocation Peek()
	{
		return (BuilderLocation)base.Peek();
	}

	public new BuilderLocation Pop()
	{
		return (BuilderLocation)base.Pop();
	}
}
