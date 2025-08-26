using System.Collections;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices;

/// <summary>Controls access to shared property groups. This class cannot be inherited.</summary>
[ComVisible(false)]
public sealed class SharedPropertyGroupManager : IEnumerable
{
	/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.SharedPropertyGroupManager" /> class.</summary>
	public SharedPropertyGroupManager()
	{
	}

	/// <summary>Finds or creates a property group with the given information.</summary>
	/// <param name="name">The name of requested property.</param>
	/// <param name="dwIsoMode">One of the <see cref="T:System.EnterpriseServices.PropertyLockMode" /> values. See the Remarks section for more information.</param>
	/// <param name="dwRelMode">One of the <see cref="T:System.EnterpriseServices.PropertyReleaseMode" /> values. See the Remarks section for more information.</param>
	/// <param name="fExist">When this method returns, contains <see langword="true" /> if the property already existed; <see langword="false" /> if the call created the property.</param>
	/// <returns>The requested <see cref="T:System.EnterpriseServices.SharedPropertyGroup" />.</returns>
	[System.MonoTODO]
	public SharedPropertyGroup CreatePropertyGroup(string name, ref PropertyLockMode dwIsoMode, ref PropertyReleaseMode dwRelMode, out bool fExist)
	{
		throw new NotImplementedException();
	}

	/// <summary>Retrieves the enumeration interface for the collection.</summary>
	/// <returns>The enumerator interface for the collection.</returns>
	[System.MonoTODO]
	public IEnumerator GetEnumerator()
	{
		throw new NotImplementedException();
	}

	/// <summary>Finds the property group with the given name.</summary>
	/// <param name="name">The name of requested property.</param>
	/// <returns>The requested <see cref="T:System.EnterpriseServices.SharedPropertyGroup" />.</returns>
	[System.MonoTODO]
	public SharedPropertyGroup Group(string name)
	{
		throw new NotImplementedException();
	}
}
