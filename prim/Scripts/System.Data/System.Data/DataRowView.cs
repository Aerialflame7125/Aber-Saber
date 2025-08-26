using System.ComponentModel;

namespace System.Data;

/// <summary>Represents a customized view of a <see cref="T:System.Data.DataRow" />.</summary>
public class DataRowView : ICustomTypeDescriptor, IEditableObject, IDataErrorInfo, INotifyPropertyChanged
{
	private readonly DataView _dataView;

	private readonly DataRow _row;

	private bool _delayBeginEdit;

	private static readonly PropertyDescriptorCollection s_zeroPropertyDescriptorCollection = new PropertyDescriptorCollection(null);

	/// <summary>Gets the <see cref="T:System.Data.DataView" /> to which this row belongs.</summary>
	/// <returns>The <see langword="DataView" /> to which this row belongs.</returns>
	public DataView DataView => _dataView;

	internal int ObjectID => _row._objectID;

	/// <summary>Gets or sets a value in a specified column.</summary>
	/// <param name="ndx">The specified column.</param>
	/// <returns>The value of the column.</returns>
	public object this[int ndx]
	{
		get
		{
			return Row[ndx, RowVersionDefault];
		}
		set
		{
			if (!_dataView.AllowEdit && !IsNew)
			{
				throw ExceptionBuilder.CanNotEdit();
			}
			SetColumnValue(_dataView.Table.Columns[ndx], value);
		}
	}

	/// <summary>Gets or sets a value in a specified column.</summary>
	/// <param name="property">String that contains the specified column.</param>
	/// <returns>The value of the column.</returns>
	public object this[string property]
	{
		get
		{
			DataColumn dataColumn = _dataView.Table.Columns[property];
			if (dataColumn != null)
			{
				return Row[dataColumn, RowVersionDefault];
			}
			if (_dataView.Table.DataSet != null && _dataView.Table.DataSet.Relations.Contains(property))
			{
				return CreateChildView(property);
			}
			throw ExceptionBuilder.PropertyNotFound(property, _dataView.Table.TableName);
		}
		set
		{
			DataColumn dataColumn = _dataView.Table.Columns[property];
			if (dataColumn == null)
			{
				throw ExceptionBuilder.SetFailed(property);
			}
			if (!_dataView.AllowEdit && !IsNew)
			{
				throw ExceptionBuilder.CanNotEdit();
			}
			SetColumnValue(dataColumn, value);
		}
	}

	/// <summary>Gets the error message for the property with the given name.</summary>
	/// <param name="colName">The name of the property whose error message to get.</param>
	/// <returns>The error message for the property. The default is an empty string ("").</returns>
	string IDataErrorInfo.this[string colName] => Row.GetColumnError(colName);

	/// <summary>Gets a message that describes any validation errors for the object.</summary>
	/// <returns>The validation error on the object.</returns>
	string IDataErrorInfo.Error => Row.RowError;

	/// <summary>Gets the current version description of the <see cref="T:System.Data.DataRow" />.</summary>
	/// <returns>One of the <see cref="T:System.Data.DataRowVersion" /> values. Possible values for the <see cref="P:System.Data.DataRowView.RowVersion" /> property are <see langword="Default" />, <see langword="Original" />, <see langword="Current" />, and <see langword="Proposed" />.</returns>
	public DataRowVersion RowVersion => RowVersionDefault & (DataRowVersion)(-1025);

	private DataRowVersion RowVersionDefault => Row.GetDefaultRowVersion(_dataView.RowStateFilter);

	/// <summary>Gets the <see cref="T:System.Data.DataRow" /> being viewed.</summary>
	/// <returns>The <see cref="T:System.Data.DataRow" /> being viewed by the <see cref="T:System.Data.DataRowView" />.</returns>
	public DataRow Row => _row;

	/// <summary>Indicates whether a <see cref="T:System.Data.DataRowView" /> is new.</summary>
	/// <returns>
	///   <see langword="true" /> if the row is new; otherwise <see langword="false" />.</returns>
	public bool IsNew => _row == _dataView._addNewRow;

	/// <summary>Indicates whether the row is in edit mode.</summary>
	/// <returns>
	///   <see langword="true" /> if the row is in edit mode; otherwise <see langword="false" />.</returns>
	public bool IsEdit
	{
		get
		{
			if (!Row.HasVersion(DataRowVersion.Proposed))
			{
				return _delayBeginEdit;
			}
			return true;
		}
	}

	/// <summary>Event that is raised when a <see cref="T:System.Data.DataRowView" /> property is changed.</summary>
	public event PropertyChangedEventHandler PropertyChanged;

	internal DataRowView(DataView dataView, DataRow row)
	{
		_dataView = dataView;
		_row = row;
	}

	/// <summary>Gets a value indicating whether the current <see cref="T:System.Data.DataRowView" /> is identical to the specified object.</summary>
	/// <param name="other">An <see cref="T:System.Object" /> to be compared.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="object" /> is a <see cref="T:System.Data.DataRowView" /> and it returns the same row as the current <see cref="T:System.Data.DataRowView" />; otherwise <see langword="false" />.</returns>
	public override bool Equals(object other)
	{
		return this == other;
	}

	/// <summary>Returns the hash code of the <see cref="T:System.Data.DataRow" /> object.</summary>
	/// <returns>A 32-bit signed integer hash code 1, which represents Boolean <see langword="true" /> if the value of this instance is nonzero; otherwise the integer zero, which represents Boolean <see langword="false" />.</returns>
	public override int GetHashCode()
	{
		return Row.GetHashCode();
	}

	internal int GetRecord()
	{
		return Row.GetRecordFromVersion(RowVersionDefault);
	}

	internal bool HasRecord()
	{
		return Row.HasVersion(RowVersionDefault);
	}

	internal object GetColumnValue(DataColumn column)
	{
		return Row[column, RowVersionDefault];
	}

	internal void SetColumnValue(DataColumn column, object value)
	{
		if (_delayBeginEdit)
		{
			_delayBeginEdit = false;
			Row.BeginEdit();
		}
		if (DataRowVersion.Original == RowVersionDefault)
		{
			throw ExceptionBuilder.SetFailed(column.ColumnName);
		}
		Row[column] = value;
	}

	/// <summary>Returns a <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" /> with the specified <see cref="T:System.Data.DataRelation" /> and parent.</summary>
	/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> object.</param>
	/// <param name="followParent">The parent object.</param>
	/// <returns>A <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" />.</returns>
	public DataView CreateChildView(DataRelation relation, bool followParent)
	{
		if (relation == null || relation.ParentKey.Table != DataView.Table)
		{
			throw ExceptionBuilder.CreateChildView();
		}
		RelatedView relatedView;
		if (!followParent)
		{
			int record = GetRecord();
			object[] keyValues = relation.ParentKey.GetKeyValues(record);
			relatedView = new RelatedView(relation.ChildColumnsReference, keyValues);
		}
		else
		{
			relatedView = new RelatedView(this, relation.ParentKey, relation.ChildColumnsReference);
		}
		relatedView.SetIndex("", DataViewRowState.CurrentRows, null);
		relatedView.SetDataViewManager(DataView.DataViewManager);
		return relatedView;
	}

	/// <summary>Returns a <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" /> with the specified child <see cref="T:System.Data.DataRelation" />.</summary>
	/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> object.</param>
	/// <returns>a <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" />.</returns>
	public DataView CreateChildView(DataRelation relation)
	{
		return CreateChildView(relation, followParent: false);
	}

	/// <summary>Returns a <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" /> with the specified <see cref="T:System.Data.DataRelation" /> name and parent.</summary>
	/// <param name="relationName">A string containing the <see cref="T:System.Data.DataRelation" /> name.</param>
	/// <param name="followParent">The parent</param>
	/// <returns>a <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" />.</returns>
	public DataView CreateChildView(string relationName, bool followParent)
	{
		return CreateChildView(DataView.Table.ChildRelations[relationName], followParent);
	}

	/// <summary>Returns a <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" /> with the specified child <see cref="T:System.Data.DataRelation" /> name.</summary>
	/// <param name="relationName">A string containing the <see cref="T:System.Data.DataRelation" /> name.</param>
	/// <returns>a <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" />.</returns>
	public DataView CreateChildView(string relationName)
	{
		return CreateChildView(relationName, followParent: false);
	}

	/// <summary>Begins an edit procedure.</summary>
	public void BeginEdit()
	{
		_delayBeginEdit = true;
	}

	/// <summary>Cancels an edit procedure.</summary>
	public void CancelEdit()
	{
		DataRow row = Row;
		if (IsNew)
		{
			_dataView.FinishAddNew(success: false);
		}
		else
		{
			row.CancelEdit();
		}
		_delayBeginEdit = false;
	}

	/// <summary>Commits changes to the underlying <see cref="T:System.Data.DataRow" /> and ends the editing session that was begun with <see cref="M:System.Data.DataRowView.BeginEdit" />.  Use <see cref="M:System.Data.DataRowView.CancelEdit" /> to discard the changes made to the <see cref="T:System.Data.DataRow" />.</summary>
	public void EndEdit()
	{
		if (IsNew)
		{
			_dataView.FinishAddNew(success: true);
		}
		else
		{
			Row.EndEdit();
		}
		_delayBeginEdit = false;
	}

	/// <summary>Deletes a row.</summary>
	public void Delete()
	{
		_dataView.Delete(Row);
	}

	internal void RaisePropertyChangedEvent(string propName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
	}

	/// <summary>Returns a collection of custom attributes for this instance of a component.</summary>
	/// <returns>An AttributeCollection containing the attributes for this object.</returns>
	AttributeCollection ICustomTypeDescriptor.GetAttributes()
	{
		return new AttributeCollection((Attribute[])null);
	}

	/// <summary>Returns the class name of this instance of a component.</summary>
	/// <returns>The class name of this instance of a component.</returns>
	string ICustomTypeDescriptor.GetClassName()
	{
		return null;
	}

	/// <summary>Returns the name of this instance of a component.</summary>
	/// <returns>The name of this instance of a component.</returns>
	string ICustomTypeDescriptor.GetComponentName()
	{
		return null;
	}

	/// <summary>Returns a type converter for this instance of a component.</summary>
	/// <returns>The type converter for this instance of a component.</returns>
	TypeConverter ICustomTypeDescriptor.GetConverter()
	{
		return null;
	}

	/// <summary>Returns the default event for this instance of a component.</summary>
	/// <returns>The default event for this instance of a component.</returns>
	EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
	{
		return null;
	}

	/// <summary>Returns the default property for this instance of a component.</summary>
	/// <returns>The default property for this instance of a component.</returns>
	PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
	{
		return null;
	}

	/// <summary>Returns an editor of the specified type for this instance of a component.</summary>
	/// <param name="editorBaseType">A <see cref="T:System.Type" /> that represents the editor for this object.</param>
	/// <returns>An <see cref="T:System.Object" /> of the specified type that is the editor for this object, or <see langword="null" /> if the editor cannot be found.</returns>
	object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
	{
		return null;
	}

	/// <summary>Returns the events for this instance of a component.</summary>
	/// <returns>The events for this instance of a component.</returns>
	EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
	{
		return new EventDescriptorCollection(null);
	}

	/// <summary>Returns the events for this instance of a component with specified attributes.</summary>
	/// <param name="attributes">The attributes</param>
	/// <returns>The events for this instance of a component.</returns>
	EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
	{
		return new EventDescriptorCollection(null);
	}

	/// <summary>Returns the properties for this instance of a component.</summary>
	/// <returns>The properties for this instance of a component.</returns>
	PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
	{
		return ((ICustomTypeDescriptor)this).GetProperties((Attribute[])null);
	}

	/// <summary>Returns the properties for this instance of a component with specified attributes.</summary>
	/// <param name="attributes">The attributes.</param>
	/// <returns>The properties for this instance of a component.</returns>
	PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
	{
		if (_dataView.Table == null)
		{
			return s_zeroPropertyDescriptorCollection;
		}
		return _dataView.Table.GetPropertyDescriptorCollection(attributes);
	}

	/// <summary>Returns an object that contains the property described by the specified property descriptor.</summary>
	/// <param name="pd">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the property whose owner is to be found.</param>
	/// <returns>An <see cref="T:System.Object" /> that represents the owner of the specified property.</returns>
	object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
	{
		return this;
	}
}
