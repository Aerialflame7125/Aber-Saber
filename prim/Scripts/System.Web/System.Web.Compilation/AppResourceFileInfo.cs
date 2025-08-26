using System.IO;

namespace System.Web.Compilation;

internal class AppResourceFileInfo
{
	public readonly bool Embeddable;

	public readonly bool Compilable;

	public readonly FileInfo Info;

	public readonly AppResourceFileKind Kind;

	public bool Seen;

	public AppResourceFileInfo(FileInfo info, AppResourceFileKind kind)
	{
		Embeddable = kind == AppResourceFileKind.Resource || kind == AppResourceFileKind.Binary;
		Compilable = kind == AppResourceFileKind.ResX;
		Info = info;
		Kind = kind;
		Seen = false;
	}
}
