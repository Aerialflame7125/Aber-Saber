using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace System.Web.Services.Description;

/// <summary>Contains a strongly typed collection of <see cref="T:System.Web.Services.Description.BasicProfileViolation" /> objects.</summary>
public class BasicProfileViolationCollection : CollectionBase, IEnumerable<BasicProfileViolation>, IEnumerable
{
	private Hashtable violations = new Hashtable();

	/// <summary>Gets or sets the <see cref="T:System.Web.Services.Description.BasicProfileViolation" /> element at a specified index in the collection.</summary>
	/// <param name="index">The zero-based index in the collection.</param>
	/// <returns>A <see cref="T:System.Web.Services.Description.BasicProfileViolation" /> object that exists at the specified index.</returns>
	public BasicProfileViolation this[int index]
	{
		get
		{
			return (BasicProfileViolation)base.List[index];
		}
		set
		{
			base.List[index] = value;
		}
	}

	internal int Add(BasicProfileViolation violation)
	{
		BasicProfileViolation basicProfileViolation = (BasicProfileViolation)violations[violation.NormativeStatement];
		if (basicProfileViolation == null)
		{
			violations[violation.NormativeStatement] = violation;
			return base.List.Add(violation);
		}
		StringEnumerator enumerator = violation.Elements.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				string current = enumerator.Current;
				basicProfileViolation.Elements.Add(current);
			}
		}
		finally
		{
			if (enumerator is IDisposable disposable)
			{
				disposable.Dispose();
			}
		}
		return IndexOf(basicProfileViolation);
	}

	internal int Add(string normativeStatement)
	{
		return Add(new BasicProfileViolation(normativeStatement));
	}

	internal int Add(string normativeStatement, string element)
	{
		return Add(new BasicProfileViolation(normativeStatement, element));
	}

	/// <summary>Returns an enumerator that iterates through a collection of <see cref="T:System.Web.Services.Description.BasicProfileViolation" /> objects.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
	IEnumerator<BasicProfileViolation> IEnumerable<BasicProfileViolation>.GetEnumerator()
	{
		return new BasicProfileViolationEnumerator(this);
	}

	/// <summary>Inserts a <see cref="T:System.Web.Services.Description.BasicProfileViolation" /> to the collection at the specified location.</summary>
	/// <param name="index">The zero-based index in the collection at which to insert the <paramref name="violation" />.</param>
	/// <param name="violation">The <see cref="T:System.Web.Services.Description.BasicProfileViolation" /> to insert.</param>
	public void Insert(int index, BasicProfileViolation violation)
	{
		base.List.Insert(index, violation);
	}

	/// <summary>Returns the zero-based index of a specified <see cref="T:System.Web.Services.Description.BasicProfileViolation" /> in the collection.</summary>
	/// <param name="violation">The <see cref="T:System.Web.Services.Description.BasicProfileViolation" /> to find in the collection.</param>
	/// <returns>The zero-based index of the specified <see cref="T:System.Web.Services.Description.BasicProfileViolation" />, or -1 if the element was not found in the collection.</returns>
	public int IndexOf(BasicProfileViolation violation)
	{
		return base.List.IndexOf(violation);
	}

	/// <summary>Checks whether the collection contains the specified <see cref="T:System.Web.Services.Description.BasicProfileViolation" /> object.</summary>
	/// <param name="violation">The <see cref="T:System.Web.Services.Description.BasicProfileViolation" /> object to locate in the collection.</param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.Services.Description.BasicProfileViolation" /> object exists in the collection; otherwise <see langword="false" />.</returns>
	public bool Contains(BasicProfileViolation violation)
	{
		return base.List.Contains(violation);
	}

	/// <summary>Removes a specified <see cref="T:System.Web.Services.Description.BasicProfileViolation" /> from the collection.</summary>
	/// <param name="violation">The <see cref="T:System.Web.Services.Description.BasicProfileViolation" /> to remove from the collection.</param>
	public void Remove(BasicProfileViolation violation)
	{
		base.List.Remove(violation);
	}

	/// <summary>Copies the elements from the collection to an array, starting at a specified index of the array.</summary>
	/// <param name="array">An array of type <see cref="T:System.Web.Services.Description.BasicProfileViolation" /> to which to copy the contents of the collection.</param>
	/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
	public void CopyTo(BasicProfileViolation[] array, int index)
	{
		base.List.CopyTo(array, index);
	}

	/// <summary>Returns a <see cref="T:System.String" /> representation of the <see cref="T:System.Web.Services.Description.BasicProfileViolation" /> objects in the collection.</summary>
	/// <returns>A <see cref="T:System.String" /> representation of the <see cref="T:System.Web.Services.Description.BasicProfileViolation" /> objects in the collection.</returns>
	public override string ToString()
	{
		if (base.List.Count > 0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < base.List.Count; i++)
			{
				BasicProfileViolation basicProfileViolation = this[i];
				if (i != 0)
				{
					stringBuilder.Append(Environment.NewLine);
				}
				stringBuilder.Append(basicProfileViolation.NormativeStatement);
				stringBuilder.Append(": ");
				stringBuilder.Append(basicProfileViolation.Details);
				StringEnumerator enumerator = basicProfileViolation.Elements.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						string current = enumerator.Current;
						stringBuilder.Append(Environment.NewLine);
						stringBuilder.Append("  -  ");
						stringBuilder.Append(current);
					}
				}
				finally
				{
					if (enumerator is IDisposable disposable)
					{
						disposable.Dispose();
					}
				}
				if (basicProfileViolation.Recommendation != null && basicProfileViolation.Recommendation.Length > 0)
				{
					stringBuilder.Append(Environment.NewLine);
					stringBuilder.Append(basicProfileViolation.Recommendation);
				}
			}
			return stringBuilder.ToString();
		}
		return string.Empty;
	}

	/// <summary>Initializes an instance of the <see cref="T:System.Web.Services.Description.BasicProfileViolationCollection" /> class.</summary>
	public BasicProfileViolationCollection()
	{
	}
}
