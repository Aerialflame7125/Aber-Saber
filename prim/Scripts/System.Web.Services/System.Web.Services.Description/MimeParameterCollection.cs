using System.Collections;

namespace System.Web.Services.Description;

internal class MimeParameterCollection : CollectionBase
{
	private Type writerType;

	internal Type WriterType
	{
		get
		{
			return writerType;
		}
		set
		{
			writerType = value;
		}
	}

	internal MimeParameter this[int index]
	{
		get
		{
			return (MimeParameter)base.List[index];
		}
		set
		{
			base.List[index] = value;
		}
	}

	internal int Add(MimeParameter parameter)
	{
		return base.List.Add(parameter);
	}

	internal void Insert(int index, MimeParameter parameter)
	{
		base.List.Insert(index, parameter);
	}

	internal int IndexOf(MimeParameter parameter)
	{
		return base.List.IndexOf(parameter);
	}

	internal bool Contains(MimeParameter parameter)
	{
		return base.List.Contains(parameter);
	}

	internal void Remove(MimeParameter parameter)
	{
		base.List.Remove(parameter);
	}

	internal void CopyTo(MimeParameter[] array, int index)
	{
		base.List.CopyTo(array, index);
	}
}
