using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Lifetime;

/// <summary>Provides a default implementation for a lifetime sponsor class.</summary>
[ComVisible(true)]
public class ClientSponsor : MarshalByRefObject, ISponsor
{
	private TimeSpan renewal_time;

	private Hashtable registered_objects = new Hashtable();

	/// <summary>Gets or sets the <see cref="T:System.TimeSpan" /> by which to increase the lifetime of the sponsored objects when renewal is requested.</summary>
	/// <returns>The <see cref="T:System.TimeSpan" /> by which to increase the lifetime of the sponsored objects when renewal is requested.</returns>
	public TimeSpan RenewalTime
	{
		get
		{
			return renewal_time;
		}
		set
		{
			renewal_time = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" /> class with default values.</summary>
	public ClientSponsor()
	{
		renewal_time = new TimeSpan(0, 2, 0);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" /> class with the renewal time of the sponsored object.</summary>
	/// <param name="renewalTime">The <see cref="T:System.TimeSpan" /> by which to increase the lifetime of the sponsored objects when renewal is requested.</param>
	public ClientSponsor(TimeSpan renewalTime)
	{
		renewal_time = renewalTime;
	}

	/// <summary>Empties the list objects registered with the current <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" />.</summary>
	public void Close()
	{
		foreach (MarshalByRefObject value in registered_objects.Values)
		{
			(value.GetLifetimeService() as ILease).Unregister(this);
		}
		registered_objects.Clear();
	}

	/// <summary>Frees the resources of the current <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" /> before the garbage collector reclaims them.</summary>
	~ClientSponsor()
	{
		Close();
	}

	/// <summary>Initializes a new instance of <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" />, providing a lease for the current object.</summary>
	/// <returns>An <see cref="T:System.Runtime.Remoting.Lifetime.ILease" /> for the current object.</returns>
	public override object InitializeLifetimeService()
	{
		return base.InitializeLifetimeService();
	}

	/// <summary>Registers the specified <see cref="T:System.MarshalByRefObject" /> for sponsorship.</summary>
	/// <param name="obj">The object to register for sponsorship with the <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" />.</param>
	/// <returns>
	///   <see langword="true" /> if registration succeeded; otherwise, <see langword="false" />.</returns>
	public bool Register(MarshalByRefObject obj)
	{
		if (registered_objects.ContainsKey(obj))
		{
			return false;
		}
		if (!(obj.GetLifetimeService() is ILease lease))
		{
			return false;
		}
		lease.Register(this);
		registered_objects.Add(obj, obj);
		return true;
	}

	/// <summary>Requests a sponsoring client to renew the lease for the specified object.</summary>
	/// <param name="lease">The lifetime lease of the object that requires lease renewal.</param>
	/// <returns>The additional lease time for the specified object.</returns>
	public TimeSpan Renewal(ILease lease)
	{
		return renewal_time;
	}

	/// <summary>Unregisters the specified <see cref="T:System.MarshalByRefObject" /> from the list of objects sponsored by the current <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" />.</summary>
	/// <param name="obj">The object to unregister.</param>
	public void Unregister(MarshalByRefObject obj)
	{
		if (registered_objects.ContainsKey(obj))
		{
			(obj.GetLifetimeService() as ILease).Unregister(this);
			registered_objects.Remove(obj);
		}
	}
}
