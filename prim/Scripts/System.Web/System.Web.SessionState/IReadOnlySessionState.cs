namespace System.Web.SessionState;

/// <summary>Specifies that the target HTTP handler requires only read access to session-state values. This is a marker interface and has no methods.</summary>
public interface IReadOnlySessionState : IRequiresSessionState
{
}
