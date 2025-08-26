using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CommandBufferOwners
{
	private HashSet<Object> _owners;

	public CommandBuffer commandBuffer;

	public int NumberOfOwners => _owners.Count;

	public void AddOwner(Object owner)
	{
		if (_owners == null)
		{
			_owners = new HashSet<Object>();
		}
		_owners.Add(owner);
	}

	public void RemoveOwner(Object owner)
	{
		if (_owners != null)
		{
			_owners.Remove(owner);
		}
	}

	public bool ContainsOwner(Object owner)
	{
		return _owners.Contains(owner);
	}
}
