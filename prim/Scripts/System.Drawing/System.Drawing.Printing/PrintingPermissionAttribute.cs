using System.Security;
using System.Security.Permissions;

namespace System.Drawing.Printing;

/// <summary>Allows declarative printing permission checks.</summary>
[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public sealed class PrintingPermissionAttribute : CodeAccessSecurityAttribute
{
	private PrintingPermissionLevel level;

	/// <summary>Gets or sets the type of printing allowed.</summary>
	/// <returns>One of the <see cref="T:System.Drawing.Printing.PrintingPermissionLevel" /> values.</returns>
	/// <exception cref="T:System.ArgumentException">The value is not one of the <see cref="T:System.Drawing.Printing.PrintingPermissionLevel" /> values.</exception>
	public PrintingPermissionLevel Level
	{
		get
		{
			return level;
		}
		set
		{
			if (value < PrintingPermissionLevel.NoPrinting || value > PrintingPermissionLevel.AllPrinting)
			{
				throw new ArgumentException(global::SR.Format("Permission level must be between PrintingPermissionLevel.NoPrinting and PrintingPermissionLevel.AllPrinting."), "value");
			}
			level = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrintingPermissionAttribute" /> class.</summary>
	/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
	public PrintingPermissionAttribute(SecurityAction action)
		: base(action)
	{
	}

	/// <summary>Creates the permission based on the requested access levels, which are set through the <see cref="P:System.Drawing.Printing.PrintingPermissionAttribute.Level" /> property on the attribute.</summary>
	/// <returns>An <see cref="T:System.Security.IPermission" /> that represents the created permission.</returns>
	public override IPermission CreatePermission()
	{
		if (base.Unrestricted)
		{
			return new PrintingPermission(PermissionState.Unrestricted);
		}
		return new PrintingPermission(level);
	}
}
