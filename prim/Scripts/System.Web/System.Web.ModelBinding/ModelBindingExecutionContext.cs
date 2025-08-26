using System.Collections.Generic;

namespace System.Web.ModelBinding;

/// <summary>Encapsulates all of the information that is external to the model binding system that the model binding system needs. </summary>
public class ModelBindingExecutionContext
{
	private Dictionary<Type, object> _services = new Dictionary<Type, object>();

	private HttpContextBase _httpContext;

	private ModelStateDictionary _modelState;

	/// <summary>Gets the HTTP context.</summary>
	/// <returns>The HTTP context.</returns>
	public virtual HttpContextBase HttpContext => _httpContext;

	/// <summary>Gets the model state.</summary>
	/// <returns>The model state.</returns>
	public virtual ModelStateDictionary ModelState => _modelState;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelBindingExecutionContext" /> class, using the HTTP context and the model state.</summary>
	/// <param name="httpContext">The HTTP context.</param>
	/// <param name="modelState">The model state.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="httpContext" /> or <paramref name="modelState" /> parameter is <see langword="null" />.</exception>
	public ModelBindingExecutionContext(HttpContextBase httpContext, ModelStateDictionary modelState)
	{
		if (httpContext == null)
		{
			throw new ArgumentNullException("httpContext");
		}
		if (modelState == null)
		{
			throw new ArgumentNullException("modelState");
		}
		_httpContext = httpContext;
		_modelState = modelState;
	}

	/// <summary>Stores an object that contains values that are used for model binding and that will be accessed by using the <see cref="M:System.Web.ModelBinding.ModelBindingExecutionContext.GetService``1" /> method.</summary>
	/// <param name="service">The object that contains values to store.</param>
	/// <typeparam name="TService">The type of the object that contains values to store.</typeparam>
	public virtual void PublishService<TService>(TService service)
	{
		_services[typeof(TService)] = service;
	}

	/// <summary>Gets an object that contains values that are used for model binding and that have been stored by using the <see cref="M:System.Web.ModelBinding.ModelBindingExecutionContext.PublishService``1(``0)" /> method. </summary>
	/// <typeparam name="TService">The type of the object that contains values that are used for model binding.</typeparam>
	/// <returns>The object that contains values that are used for model binding.</returns>
	public virtual TService GetService<TService>()
	{
		return (TService)_services[typeof(TService)];
	}

	/// <summary>Gets an object that contains values that are used for model binding and that have been stored by using the <see cref="M:System.Web.ModelBinding.ModelBindingExecutionContext.PublishService``1(``0)" /> method.</summary>
	/// <typeparam name="TService">The type of the object that contains values that are used for model binding.</typeparam>
	/// <returns>The object that contains values that are used for model binding, or the default value of <paramref name="TService" /> if the requested object is not found.</returns>
	public virtual TService TryGetService<TService>()
	{
		if (_services.ContainsKey(typeof(TService)))
		{
			return (TService)_services[typeof(TService)];
		}
		return default(TService);
	}
}
