namespace System.Web;

/// <summary>
///     Represents an interface that is implemented by an object and that can be used to unsubscribe listeners.</summary>
public interface ISubscriptionToken
{
	/// <summary>Returns a value that indicates whether the subscription is currently active.</summary>
	/// <returns>
	///     <see langword="true" /> if the subscription is currently active; otherwise, <see langword="false" />.</returns>
	bool IsActive { get; }

	/// <summary>Unsubscribes a listener from the event.</summary>
	void Unsubscribe();
}
