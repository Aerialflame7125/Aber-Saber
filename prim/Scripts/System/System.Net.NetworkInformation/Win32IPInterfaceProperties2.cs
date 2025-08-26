using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation;

internal class Win32IPInterfaceProperties2 : IPInterfaceProperties
{
	private readonly Win32_IP_ADAPTER_ADDRESSES addr;

	private readonly Win32_MIB_IFROW mib4;

	private readonly Win32_MIB_IFROW mib6;

	public override IPAddressInformationCollection AnycastAddresses => Win32FromAnycast(addr.FirstAnycastAddress);

	public override IPAddressCollection DhcpServerAddresses
	{
		get
		{
			Win32_IP_ADAPTER_INFO adapterInfoByIndex = Win32NetworkInterface2.GetAdapterInfoByIndex(mib4.Index);
			try
			{
				return new Win32IPAddressCollection(adapterInfoByIndex.DhcpServer);
			}
			catch (IndexOutOfRangeException)
			{
				return Win32IPAddressCollection.Empty;
			}
		}
	}

	public override IPAddressCollection DnsAddresses => Win32IPAddressCollection.FromDnsServer(addr.FirstDnsServerAddress);

	public override string DnsSuffix => addr.DnsSuffix;

	public override GatewayIPAddressInformationCollection GatewayAddresses
	{
		get
		{
			GatewayIPAddressInformationCollection gatewayIPAddressInformationCollection = new GatewayIPAddressInformationCollection();
			try
			{
				Win32_IP_ADDR_STRING gatewayList = Win32NetworkInterface2.GetAdapterInfoByIndex(mib4.Index).GatewayList;
				if (!string.IsNullOrEmpty(gatewayList.IpAddress))
				{
					gatewayIPAddressInformationCollection.InternalAdd(new SystemGatewayIPAddressInformation(IPAddress.Parse(gatewayList.IpAddress)));
					AddSubsequently(gatewayList.Next, gatewayIPAddressInformationCollection);
				}
			}
			catch (IndexOutOfRangeException)
			{
			}
			return gatewayIPAddressInformationCollection;
		}
	}

	public override bool IsDnsEnabled => Win32NetworkInterface.FixedInfo.EnableDns != 0;

	public override bool IsDynamicDnsEnabled => addr.DdnsEnabled;

	public override MulticastIPAddressInformationCollection MulticastAddresses => Win32FromMulticast(addr.FirstMulticastAddress);

	public override UnicastIPAddressInformationCollection UnicastAddresses
	{
		get
		{
			try
			{
				Win32NetworkInterface2.GetAdapterInfoByIndex(mib4.Index);
				return Win32FromUnicast(addr.FirstUnicastAddress);
			}
			catch (IndexOutOfRangeException)
			{
				return new UnicastIPAddressInformationCollection();
			}
		}
	}

	public override IPAddressCollection WinsServersAddresses
	{
		get
		{
			try
			{
				Win32_IP_ADAPTER_INFO adapterInfoByIndex = Win32NetworkInterface2.GetAdapterInfoByIndex(mib4.Index);
				return new Win32IPAddressCollection(adapterInfoByIndex.PrimaryWinsServer, adapterInfoByIndex.SecondaryWinsServer);
			}
			catch (IndexOutOfRangeException)
			{
				return Win32IPAddressCollection.Empty;
			}
		}
	}

	public Win32IPInterfaceProperties2(Win32_IP_ADAPTER_ADDRESSES addr, Win32_MIB_IFROW mib4, Win32_MIB_IFROW mib6)
	{
		this.addr = addr;
		this.mib4 = mib4;
		this.mib6 = mib6;
	}

	public override IPv4InterfaceProperties GetIPv4Properties()
	{
		return new Win32IPv4InterfaceProperties(Win32NetworkInterface2.GetAdapterInfoByIndex(mib4.Index), mib4);
	}

	public override IPv6InterfaceProperties GetIPv6Properties()
	{
		Win32NetworkInterface2.GetAdapterInfoByIndex(mib6.Index);
		return new Win32IPv6InterfaceProperties(mib6);
	}

	private static IPAddressInformationCollection Win32FromAnycast(IntPtr ptr)
	{
		IPAddressInformationCollection iPAddressInformationCollection = new IPAddressInformationCollection();
		IntPtr intPtr = ptr;
		while (intPtr != IntPtr.Zero)
		{
			Win32_IP_ADAPTER_ANYCAST_ADDRESS win32_IP_ADAPTER_ANYCAST_ADDRESS = (Win32_IP_ADAPTER_ANYCAST_ADDRESS)Marshal.PtrToStructure(intPtr, typeof(Win32_IP_ADAPTER_ANYCAST_ADDRESS));
			iPAddressInformationCollection.InternalAdd(new SystemIPAddressInformation(win32_IP_ADAPTER_ANYCAST_ADDRESS.Address.GetIPAddress(), win32_IP_ADAPTER_ANYCAST_ADDRESS.LengthFlags.IsDnsEligible, win32_IP_ADAPTER_ANYCAST_ADDRESS.LengthFlags.IsTransient));
			intPtr = win32_IP_ADAPTER_ANYCAST_ADDRESS.Next;
		}
		return iPAddressInformationCollection;
	}

	private static void AddSubsequently(IntPtr head, GatewayIPAddressInformationCollection col)
	{
		IntPtr intPtr = head;
		while (intPtr != IntPtr.Zero)
		{
			Win32_IP_ADDR_STRING win32_IP_ADDR_STRING = (Win32_IP_ADDR_STRING)Marshal.PtrToStructure(intPtr, typeof(Win32_IP_ADDR_STRING));
			col.InternalAdd(new SystemGatewayIPAddressInformation(IPAddress.Parse(win32_IP_ADDR_STRING.IpAddress)));
			intPtr = win32_IP_ADDR_STRING.Next;
		}
	}

	private static MulticastIPAddressInformationCollection Win32FromMulticast(IntPtr ptr)
	{
		MulticastIPAddressInformationCollection multicastIPAddressInformationCollection = new MulticastIPAddressInformationCollection();
		IntPtr intPtr = ptr;
		while (intPtr != IntPtr.Zero)
		{
			Win32_IP_ADAPTER_MULTICAST_ADDRESS win32_IP_ADAPTER_MULTICAST_ADDRESS = (Win32_IP_ADAPTER_MULTICAST_ADDRESS)Marshal.PtrToStructure(intPtr, typeof(Win32_IP_ADAPTER_MULTICAST_ADDRESS));
			multicastIPAddressInformationCollection.InternalAdd(new SystemMulticastIPAddressInformation(new SystemIPAddressInformation(win32_IP_ADAPTER_MULTICAST_ADDRESS.Address.GetIPAddress(), win32_IP_ADAPTER_MULTICAST_ADDRESS.LengthFlags.IsDnsEligible, win32_IP_ADAPTER_MULTICAST_ADDRESS.LengthFlags.IsTransient)));
			intPtr = win32_IP_ADAPTER_MULTICAST_ADDRESS.Next;
		}
		return multicastIPAddressInformationCollection;
	}

	private static UnicastIPAddressInformationCollection Win32FromUnicast(IntPtr ptr)
	{
		UnicastIPAddressInformationCollection unicastIPAddressInformationCollection = new UnicastIPAddressInformationCollection();
		IntPtr intPtr = ptr;
		while (intPtr != IntPtr.Zero)
		{
			Win32_IP_ADAPTER_UNICAST_ADDRESS info = (Win32_IP_ADAPTER_UNICAST_ADDRESS)Marshal.PtrToStructure(intPtr, typeof(Win32_IP_ADAPTER_UNICAST_ADDRESS));
			unicastIPAddressInformationCollection.InternalAdd(new Win32UnicastIPAddressInformation(info));
			intPtr = info.Next;
		}
		return unicastIPAddressInformationCollection;
	}
}
