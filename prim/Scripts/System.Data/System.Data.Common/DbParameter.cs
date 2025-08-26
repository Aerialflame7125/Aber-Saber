using System.ComponentModel;

namespace System.Data.Common;

/// <summary>Represents a parameter to a <see cref="T:System.Data.Common.DbCommand" /> and optionally, its mapping to a <see cref="T:System.Data.DataSet" /> column. For more information on parameters, see Configuring Parameters and Parameter Data Types.</summary>
public abstract class DbParameter : MarshalByRefObject, IDbDataParameter, IDataParameter
{
	/// <summary>Gets or sets the <see cref="T:System.Data.DbType" /> of the parameter.</summary>
	/// <returns>One of the <see cref="T:System.Data.DbType" /> values. The default is <see cref="F:System.Data.DbType.String" />.</returns>
	/// <exception cref="T:System.ArgumentException">The property is not set to a valid <see cref="T:System.Data.DbType" />.</exception>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[RefreshProperties(RefreshProperties.All)]
	public abstract DbType DbType { get; set; }

	/// <summary>Gets or sets a value that indicates whether the parameter is input-only, output-only, bidirectional, or a stored procedure return value parameter.</summary>
	/// <returns>One of the <see cref="T:System.Data.ParameterDirection" /> values. The default is <see langword="Input" />.</returns>
	/// <exception cref="T:System.ArgumentException">The property is not set to one of the valid <see cref="T:System.Data.ParameterDirection" /> values.</exception>
	[DefaultValue(ParameterDirection.Input)]
	[RefreshProperties(RefreshProperties.All)]
	public abstract ParameterDirection Direction { get; set; }

	/// <summary>Gets or sets a value that indicates whether the parameter accepts null values.</summary>
	/// <returns>
	///   <see langword="true" /> if null values are accepted; otherwise <see langword="false" />. The default is <see langword="false" />.</returns>
	[Browsable(false)]
	[DesignOnly(true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public abstract bool IsNullable { get; set; }

	/// <summary>Gets or sets the name of the <see cref="T:System.Data.Common.DbParameter" />.</summary>
	/// <returns>The name of the <see cref="T:System.Data.Common.DbParameter" />. The default is an empty string ("").</returns>
	[DefaultValue("")]
	public abstract string ParameterName { get; set; }

	/// <summary>Indicates the precision of numeric parameters.</summary>
	/// <returns>The maximum number of digits used to represent the <see langword="Value" /> property of a data provider <see langword="Parameter" /> object. The default value is 0, which indicates that a data provider sets the precision for <see langword="Value" />.</returns>
	byte IDbDataParameter.Precision
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	/// <summary>For a description of this member, see <see cref="P:System.Data.IDbDataParameter.Scale" />.</summary>
	/// <returns>The number of decimal places to which <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> is resolved. The default is 0.</returns>
	byte IDbDataParameter.Scale
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	/// <summary>Gets or sets the maximum number of digits used to represent the <see cref="P:System.Data.Common.DbParameter.Value" /> property.</summary>
	/// <returns>The maximum number of digits used to represent the <see cref="P:System.Data.Common.DbParameter.Value" /> property.</returns>
	public virtual byte Precision
	{
		get
		{
			return ((IDbDataParameter)this).Precision;
		}
		set
		{
			((IDbDataParameter)this).Precision = value;
		}
	}

	/// <summary>Gets or sets the number of decimal places to which <see cref="P:System.Data.Common.DbParameter.Value" /> is resolved.</summary>
	/// <returns>The number of decimal places to which <see cref="P:System.Data.Common.DbParameter.Value" /> is resolved.</returns>
	public virtual byte Scale
	{
		get
		{
			return ((IDbDataParameter)this).Scale;
		}
		set
		{
			((IDbDataParameter)this).Scale = value;
		}
	}

	/// <summary>Gets or sets the maximum size, in bytes, of the data within the column.</summary>
	/// <returns>The maximum size, in bytes, of the data within the column. The default value is inferred from the parameter value.</returns>
	public abstract int Size { get; set; }

	/// <summary>Gets or sets the name of the source column mapped to the <see cref="T:System.Data.DataSet" /> and used for loading or returning the <see cref="P:System.Data.Common.DbParameter.Value" />.</summary>
	/// <returns>The name of the source column mapped to the <see cref="T:System.Data.DataSet" />. The default is an empty string.</returns>
	[DefaultValue("")]
	public abstract string SourceColumn { get; set; }

	/// <summary>Sets or gets a value which indicates whether the source column is nullable. This allows <see cref="T:System.Data.Common.DbCommandBuilder" /> to correctly generate Update statements for nullable columns.</summary>
	/// <returns>
	///   <see langword="true" /> if the source column is nullable; <see langword="false" /> if it is not.</returns>
	[DefaultValue(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[RefreshProperties(RefreshProperties.All)]
	public abstract bool SourceColumnNullMapping { get; set; }

	/// <summary>Gets or sets the <see cref="T:System.Data.DataRowVersion" /> to use when you load <see cref="P:System.Data.Common.DbParameter.Value" />.</summary>
	/// <returns>One of the <see cref="T:System.Data.DataRowVersion" /> values. The default is <see langword="Current" />.</returns>
	/// <exception cref="T:System.ArgumentException">The property is not set to one of the <see cref="T:System.Data.DataRowVersion" /> values.</exception>
	[DefaultValue(DataRowVersion.Current)]
	public virtual DataRowVersion SourceVersion
	{
		get
		{
			return DataRowVersion.Default;
		}
		set
		{
		}
	}

	/// <summary>Gets or sets the value of the parameter.</summary>
	/// <returns>An <see cref="T:System.Object" /> that is the value of the parameter. The default value is null.</returns>
	[DefaultValue(null)]
	[RefreshProperties(RefreshProperties.All)]
	public abstract object Value { get; set; }

	/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbParameter" /> class.</summary>
	protected DbParameter()
	{
	}

	/// <summary>Resets the DbType property to its original settings.</summary>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public abstract void ResetDbType();
}
