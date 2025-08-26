using System.Collections.Generic;
using System.Linq;

namespace System.Web.Routing;

internal sealed class ContentPathSegment : PathSegment
{
	public bool IsCatchAll => Subsegments.Any((PathSubsegment seg) => seg is ParameterSubsegment && ((ParameterSubsegment)seg).IsCatchAll);

	public IList<PathSubsegment> Subsegments { get; private set; }

	public ContentPathSegment(IList<PathSubsegment> subsegments)
	{
		Subsegments = subsegments;
	}
}
