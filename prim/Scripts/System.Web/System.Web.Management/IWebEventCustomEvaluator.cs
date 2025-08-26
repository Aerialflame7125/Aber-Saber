namespace System.Web.Management;

/// <summary>Evaluates whether an event should be sent to the related provider for processing.</summary>
public interface IWebEventCustomEvaluator
{
	/// <summary>Evaluates whether an event should be raised.</summary>
	/// <param name="raisedEvent">The event to raise. </param>
	/// <param name="record">The <see cref="T:System.Web.Management.RuleFiringRecord" /> containing information about the event. </param>
	/// <returns>
	///     <see langword="true" /> if the event should be raised; otherwise, <see langword="false" />.</returns>
	bool CanFire(WebBaseEvent raisedEvent, RuleFiringRecord record);
}
