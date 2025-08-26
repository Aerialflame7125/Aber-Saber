using System;

namespace JetBrains.Annotations;

[AttributeUsage(AttributeTargets.Method)]
[Obsolete("Use [ContractAnnotation('=> halt')] instead")]
internal sealed class TerminatesProgramAttribute : Attribute
{
}
