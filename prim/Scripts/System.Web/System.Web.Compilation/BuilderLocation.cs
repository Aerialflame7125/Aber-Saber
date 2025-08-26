using System.Web.UI;

namespace System.Web.Compilation;

internal class BuilderLocation
{
	public ControlBuilder Builder;

	public ILocation Location;

	public BuilderLocation(ControlBuilder builder, ILocation location)
	{
		Builder = builder;
		Location = new Location(location);
	}
}
