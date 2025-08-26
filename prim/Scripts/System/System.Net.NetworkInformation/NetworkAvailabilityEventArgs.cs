namespace System.Net.NetworkInformation;

/// <summary>Provides data for the <see cref="E:System.Net.NetworkInformation.NetworkChange.NetworkAvailabilityChanged" /> event.</summary>
public class NetworkAvailabilityEventArgs : EventArgs
{
	private bool isAvailable;

	/// <summary>Gets the current status of the network connection.</summary>
	/// <returns>
	///   <see langword="true" /> if the network is available; otherwise, <see langword="false" />.</returns>
	public bool IsAvailable => isAvailable;

	internal NetworkAvailabilityEventArgs(bool isAvailable)
	{
		this.isAvailable = isAvailable;
	}
}
