using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Web.UI.WebControls;

/// <summary>Contains a sequential list of role groups that the <see cref="T:System.Web.UI.WebControls.LoginView" /> control uses to determine which control template to display to users based on their role. This class cannot be inherited.</summary>
[Editor("System.Web.UI.Design.WebControls.RoleGroupCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class RoleGroupCollection : CollectionBase
{
	/// <summary>Gets the role group at the specified index.</summary>
	/// <param name="index">The index of the role group to return. </param>
	/// <returns>The <see cref="T:System.Web.UI.WebControls.RoleGroup" /> at the specified index.</returns>
	public RoleGroup this[int index] => (RoleGroup)base.List[index];

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.RoleGroupCollection" /> class.</summary>
	public RoleGroupCollection()
	{
	}

	/// <summary>Adds a role group to the end of the collection.</summary>
	/// <param name="group">The <see cref="T:System.Web.UI.WebControls.RoleGroup" /> to add to the collection. </param>
	public void Add(RoleGroup group)
	{
		base.List.Add(group);
	}

	/// <summary>Indicates whether the collection contains the specified role group.</summary>
	/// <param name="group">The <see cref="T:System.Web.UI.WebControls.RoleGroup" /> to look for in the collection. </param>
	/// <returns>
	///     <see langword="true" /> if the specified role group is a member of the collection; otherwise <see langword="false" />.</returns>
	public bool Contains(RoleGroup group)
	{
		return base.List.Contains(group);
	}

	/// <summary>Copies all the items from the <see cref="T:System.Web.UI.WebControls.RoleGroupCollection" /> collection to a compatible one-dimensional array of <see cref="T:System.Web.UI.WebControls.RoleGroup" /> objects, starting at the specified index in the target array.</summary>
	/// <param name="array">A zero-based array of <see cref="T:System.Web.UI.WebControls.RoleGroup" /> objects that receives the items copied from the collection.</param>
	/// <param name="index">The position in the target array at which the array starts receiving the copied items.</param>
	public void CopyTo(RoleGroup[] array, int index)
	{
		if (array == null)
		{
			throw new ArgumentNullException("array");
		}
		if (index < 0)
		{
			throw new ArgumentException(Locale.GetText("Negative index."), "index");
		}
		if (base.Count <= array.Length - index)
		{
			throw new ArgumentException(Locale.GetText("Destination isn't large enough to copy collection."), "array");
		}
		for (int i = 0; i < base.Count; i++)
		{
			array[i + index] = this[i];
		}
	}

	/// <summary>Returns the first role group that contains the specified user account.</summary>
	/// <param name="user">An <see cref="T:System.Security.Principal.IPrincipal" /> that represents the user account to find the role group collection.</param>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.RoleGroup" /> representing the first role group in the collection that contains the specified user account. If the user is not part of a role group in the collection, it returns <see langword="null" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="user" /> is <see langword="null" />.</exception>
	public RoleGroup GetMatchingRoleGroup(IPrincipal user)
	{
		if (user == null)
		{
			throw new ArgumentNullException("user");
		}
		if (base.Count > 0)
		{
			{
				IEnumerator enumerator = GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						RoleGroup roleGroup = (RoleGroup)enumerator.Current;
						if (roleGroup.ContainsUser(user))
						{
							return roleGroup;
						}
					}
				}
				finally
				{
					IDisposable disposable = enumerator as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
			}
		}
		return null;
	}

	/// <summary>Searches the collection and returns the zero-based index of the first occurrence of the specified <see cref="T:System.Web.UI.WebControls.RoleGroup" />.</summary>
	/// <param name="group">The <see cref="T:System.Web.UI.WebControls.RoleGroup" /> to locate in the collection. </param>
	/// <returns>The zero-based index of the first occurrence of <paramref name="group" /> within the entire <see cref="T:System.Web.UI.WebControls.RoleGroupCollection" />, if found; otherwise, -1.</returns>
	public int IndexOf(RoleGroup group)
	{
		return base.List.IndexOf(group);
	}

	/// <summary>Adds a <see cref="T:System.Web.UI.WebControls.RoleGroup" /> to the collection at the specified index.</summary>
	/// <param name="index">The zero-based index at which to insert the role group. </param>
	/// <param name="group">The role group to insert. </param>
	public void Insert(int index, RoleGroup group)
	{
		base.List.Insert(index, group);
	}

	protected override void OnValidate(object value)
	{
		base.OnValidate(value);
	}

	/// <summary>Deletes the first occurrence of the specified role group from the collection.</summary>
	/// <param name="group">The <see cref="T:System.Web.UI.WebControls.RoleGroup" /> to remove from the collection. </param>
	public void Remove(RoleGroup group)
	{
		if (group != null && Contains(group))
		{
			base.List.Remove(group);
		}
	}
}
