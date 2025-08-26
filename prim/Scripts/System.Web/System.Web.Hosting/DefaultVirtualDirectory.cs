using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace System.Web.Hosting;

internal sealed class DefaultVirtualDirectory : VirtualDirectory
{
	private string phys_dir;

	private string virtual_dir;

	public override IEnumerable Children
	{
		get
		{
			Init();
			List<VirtualFileBase> list = new List<VirtualFileBase>();
			AddDirectories(list, phys_dir);
			return AddFiles(list, phys_dir);
		}
	}

	public override IEnumerable Directories
	{
		get
		{
			Init();
			return AddDirectories(new List<VirtualFileBase>(), phys_dir);
		}
	}

	public override IEnumerable Files
	{
		get
		{
			Init();
			return AddFiles(new List<VirtualFileBase>(), phys_dir);
		}
	}

	internal DefaultVirtualDirectory(string virtualPath)
		: base(virtualPath)
	{
	}

	private void Init()
	{
		if (phys_dir == null)
		{
			string virtualPath = base.VirtualPath;
			string path = HostingEnvironment.MapPath(virtualPath);
			if (File.Exists(path))
			{
				virtual_dir = VirtualPathUtility.GetDirectory(virtualPath);
				phys_dir = HostingEnvironment.MapPath(virtual_dir);
			}
			else
			{
				virtual_dir = VirtualPathUtility.AppendTrailingSlash(virtualPath);
				phys_dir = path;
			}
		}
	}

	private List<VirtualFileBase> AddDirectories(List<VirtualFileBase> list, string dir)
	{
		if (string.IsNullOrEmpty(dir) || !Directory.Exists(dir))
		{
			return list;
		}
		string[] directories = Directory.GetDirectories(phys_dir);
		foreach (string path in directories)
		{
			list.Add(new DefaultVirtualDirectory(VirtualPathUtility.Combine(virtual_dir, Path.GetFileName(path))));
		}
		return list;
	}

	private List<VirtualFileBase> AddFiles(List<VirtualFileBase> list, string dir)
	{
		if (string.IsNullOrEmpty(dir) || !Directory.Exists(dir))
		{
			return list;
		}
		string[] files = Directory.GetFiles(phys_dir);
		foreach (string path in files)
		{
			list.Add(new DefaultVirtualFile(VirtualPathUtility.Combine(virtual_dir, Path.GetFileName(path))));
		}
		return list;
	}
}
