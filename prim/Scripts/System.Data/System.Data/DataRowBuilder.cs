namespace System.Data;

/// <summary>The <see langword="DataRowBuilder" /> type supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
public sealed class DataRowBuilder
{
	internal readonly DataTable _table;

	internal int _record;

	internal DataRowBuilder(DataTable table, int record)
	{
		_table = table;
		_record = record;
	}
}
