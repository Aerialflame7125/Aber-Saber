using System.Collections;

namespace System.Windows.Forms;

/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.Form" /> objects.</summary>
/// <filterpriority>2</filterpriority>
public class FormCollection : ReadOnlyCollectionBase
{
	/// <summary>Gets or sets an element in the collection by its numeric index.</summary>
	/// <param name="index">The location of the <see cref="T:System.Windows.Forms.Form" /> within the collection.</param>
	/// <filterpriority>1</filterpriority>
	public virtual Form this[int index] => (Form)base.InnerList[index];

	/// <summary>Gets or sets an element in the collection by the name of the associated <see cref="T:System.Windows.Forms.Form" /> object.</summary>
	/// <param name="name">The name of the <see cref="T:System.Windows.Forms.Form" />.</param>
	/// <filterpriority>1</filterpriority>
	public virtual Form this[string name]
	{
		get
		{
			foreach (Form inner in base.InnerList)
			{
				if (inner.Name == name)
				{
					return inner;
				}
			}
			return null;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.FormCollection" /> class. </summary>
	public FormCollection()
	{
	}

	internal void Add(Form form)
	{
		if (!base.InnerList.Contains(form))
		{
			base.InnerList.Add(form);
		}
	}

	internal void Remove(Form form)
	{
		base.InnerList.Remove(form);
	}
}
