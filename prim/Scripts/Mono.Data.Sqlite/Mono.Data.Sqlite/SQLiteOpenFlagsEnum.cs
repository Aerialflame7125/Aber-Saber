using System;

namespace Mono.Data.Sqlite;

[Flags]
internal enum SQLiteOpenFlagsEnum
{
	None = 0,
	ReadOnly = 1,
	ReadWrite = 2,
	Create = 4,
	Default = 6,
	FileProtectionComplete = 0x100000,
	FileProtectionCompleteUnlessOpen = 0x200000,
	FileProtectionCompleteUntilFirstUserAuthentication = 0x300000,
	FileProtectionNone = 0x400000
}
