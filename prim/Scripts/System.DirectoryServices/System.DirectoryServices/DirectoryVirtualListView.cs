using System.ComponentModel;

namespace System.DirectoryServices;

/// <summary>The <see cref="T:System.DirectoryServices.DirectoryVirtualListView" /> class specifies how to conduct a virtual list view search. A virtual list view search enables users to view search results as address-book style virtual list views. It is specifically designed for very large result sets. Search data is retrieved in contiguous subsets of a sorted directory search.</summary>
public class DirectoryVirtualListView
{
	/// <summary>Gets or sets a value to indicate the number of entries before the target entry that the client is requesting from the server.</summary>
	/// <returns>An integer value that represents the number of entries before the target entry that the client is requesting from the server.</returns>
	/// <exception cref="T:System.ArgumentException">The <see cref="P:System.DirectoryServices.DirectoryVirtualListView.BeforeCount" /> property is set to a value less than 0.</exception>
	[DefaultValue(0)]
	[DSDescription("DSBeforeCount")]
	public int BeforeCount
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets a value to indicate the number of entries after the target entry that the client is requesting from the server.</summary>
	/// <returns>An integer value that represents the number of entries after the target entry that the client is requesting from the server.</returns>
	/// <exception cref="T:System.ArgumentException">The <see cref="P:System.DirectoryServices.DirectoryVirtualListView.AfterCount" /> property is set to a value less than zero.</exception>
	[DefaultValue(0)]
	[DSDescription("DSAfterCount")]
	public int AfterCount
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets a value to indicate the target entry's offset within the list.</summary>
	/// <returns>An integer value that represents the target entry's estimated offset within the list.</returns>
	/// <exception cref="T:System.ArgumentException">The <see cref="P:System.DirectoryServices.DirectoryVirtualListView.Offset" /> property is set to a value less than 0.</exception>
	[DefaultValue(0)]
	[DSDescription("DSOffset")]
	public int Offset
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>The <see cref="P:System.DirectoryServices.DirectoryVirtualListView.TargetPercentage" /> property gets or sets a value to indicate the estimated target entry's requested offset within the list, as a percentage of the total number of items in the list.</summary>
	/// <returns>An integer value that represents the estimated percentage offset within the list of the target entry.</returns>
	/// <exception cref="T:System.ArgumentException">The <see cref="P:System.DirectoryServices.DirectoryVirtualListView.TargetPercentage" /> property is set to a value greater than 100 or less than 0.</exception>
	[DefaultValue(0)]
	[DSDescription("DSTargetPercentage")]
	public int TargetPercentage
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>The <see cref="P:System.DirectoryServices.DirectoryVirtualListView.Target" /> property gets or sets a value to indicate the target entry that was requested by the client.</summary>
	/// <returns>A string that contains the target entry that was requested by the client.</returns>
	[DefaultValue("")]
	[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DSDescription("DSTarget")]
	public string Target
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets a value to indicate the estimated total count of items in the list.</summary>
	/// <returns>An integer value that represents the estimated total count of items in the list.</returns>
	/// <exception cref="T:System.ArgumentException">The <see cref="P:System.DirectoryServices.DirectoryVirtualListView.ApproximateTotal" /> property is set to a value less than zero.</exception>
	[DefaultValue(0)]
	[DSDescription("DSApproximateTotal")]
	public int ApproximateTotal
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets a value to indicate the virtual list view search response.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.DirectoryVirtualListViewContext" /> that indicates the virtual list view search response.</returns>
	[DefaultValue(null)]
	[DSDescription("DSDirectoryVirtualListViewContext")]
	public DirectoryVirtualListViewContext DirectoryVirtualListViewContext
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectoryVirtualListView" /> class.</summary>
	public DirectoryVirtualListView()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectoryVirtualListView" /> class with the after count set.</summary>
	/// <param name="afterCount">A <see cref="T:System.Int32" /> data type object that gets or sets a value to indicate the number of entries after the target entry that the client is requesting from the server.</param>
	public DirectoryVirtualListView(int afterCount)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectoryVirtualListView" /> class with the before count, after count, and offset set.</summary>
	/// <param name="beforeCount">A <see cref="T:System.Int32" /> data type objects that gets or sets a value to indicate the number of entries after the target entry that the client is requesting from the server.</param>
	/// <param name="afterCount">A <see cref="T:System.Int32" /> data type object that gets or sets a value to indicate the number of entries after the target entry that the client is requesting from the server.</param>
	/// <param name="offset">An <see cref="T:System.Int32" /> data type that gets or sets a value to indicate the estimated target entry's requested offset within the list.</param>
	public DirectoryVirtualListView(int beforeCount, int afterCount, int offset)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectoryVirtualListView" /> class with the before count, after count, and target set.</summary>
	/// <param name="beforeCount">A <see cref="T:System.Int32" /> data type objects that gets or sets a value to indicate the number of entries after the target entry that the client is requesting from the server.</param>
	/// <param name="afterCount">A <see cref="T:System.Int32" /> data type object that gets or sets a value to indicate the number of entries after the target entry that the client is requesting from the server.</param>
	/// <param name="target">A <see cref="T:System.String" /> that gets or sets a value to indicate the desired target entry requested by the client.</param>
	public DirectoryVirtualListView(int beforeCount, int afterCount, string target)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectoryVirtualListView" /> class with the before count, after count, offset and context set.</summary>
	/// <param name="beforeCount">A <see cref="T:System.Int32" /> data type objects that gets or sets a value to indicate the number of entries after the target entry that the client is requesting from the server.</param>
	/// <param name="afterCount">A <see cref="T:System.Int32" /> data type object that gets or sets a value to indicate the number of entries after the target entry that the client is requesting from the server.</param>
	/// <param name="offset">An <see cref="T:System.Int32" /> data type that gets or sets a value to indicate the estimated target entry's requested offset within the list.</param>
	/// <param name="context">A <see cref="T:System.DirectoryServices.DirectoryVirtualListViewContext" /> data type objects that gets or sets a value to indicate the virtual list view search response.</param>
	public DirectoryVirtualListView(int beforeCount, int afterCount, int offset, DirectoryVirtualListViewContext context)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectoryVirtualListView" /> class with the before count, after count, target and context set.</summary>
	/// <param name="beforeCount">A <see cref="T:System.Int32" /> data type objects that gets or sets a value to indicate the number of entries after the target entry that the client is requesting from the server.</param>
	/// <param name="afterCount">A <see cref="T:System.Int32" /> data type object that gets or sets a value to indicate the number of entries after the target entry that the client is requesting from the server.</param>
	/// <param name="target">A <see cref="T:System.String" /> that gets or sets a value to indicate the desired target entry requested by the client.</param>
	/// <param name="context">A <see cref="T:System.DirectoryServices.DirectoryVirtualListViewContext" /> data type objects that gets or sets a value to indicate the virtual list view search response.</param>
	public DirectoryVirtualListView(int beforeCount, int afterCount, string target, DirectoryVirtualListViewContext context)
	{
	}
}
