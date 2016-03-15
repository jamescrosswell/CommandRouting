using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Abstractions;
using Microsoft.AspNet.Mvc.Controllers;
using Microsoft.AspNet.Mvc.Formatters;
using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.AspNet.Mvc.ModelBinding.Validation;
using Microsoft.AspNet.Routing;

namespace CommandRouting.Router
{
    /// <summary>
    /// Used to create an instance of a command request model from the body of an an HTTP Request. 
    /// </summary>
    public class RequestModelActivator
    {
        private readonly HttpContext _httpContext;
        private readonly RouteData _routeData;

        // List of input formatters (currently we only support json)
        IInputFormatter[] InputFormatters => new IInputFormatter[] { new JsonInputFormatter() };

        public RequestModelActivator(HttpContext httpContext, RouteData routeData)
        {
            // Make sure the httpContext is not null
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            _httpContext = httpContext;
            _routeData = routeData ?? new RouteData();
        }

        public object CreateRequestModel(Type requestType)
        {
            // We'll try to create a basic model from the message body or, failing that, just create a 
            // default instance of the model
            dynamic model = DeserializeMessageBody(requestType) ?? Activator.CreateInstance(requestType);

            // Merge in any values from the route data
            MergeRouteValues(requestType, model);

            // Return the result
            return model;
        }

        /// <summary>
        /// Merges values into our request model from the route data - note that unlike 
        /// MCV 6 we ignore other value sources such as query strings and form data. The
        /// kind of applications that use those value sources are crap and need to be 
        /// updated... 
        /// </summary>
        /// <param name="requestType"></param>
        /// <param name="model"></param>
        private void MergeRouteValues(Type requestType, object model)
        {
            // We're going to assume no prefix
            string prefix = "";

            // Create a model state dictionary for (the model binder needs this for custom model validation)
            ModelStateDictionary modelState = new ModelStateDictionary();

            // Now build the meta data
            EmptyModelMetadataProvider metaDataProvider = new EmptyModelMetadataProvider();
            ModelMetadata metaData = metaDataProvider.GetMetadataForType(requestType);

            // Next create a model binder
            //IModelBinder modelBinder = new GenericModelBinder();
            IModelBinder modelBinder = new GenericModelBinder();

            // Create a value provider (currently we only support route data)
            IValueProviderFactory valueProviderFactory = new RouteValueValueProviderFactory();
            Task<IValueProvider> valueProvider = valueProviderFactory.GetValueProviderAsync(new ValueProviderFactoryContext(_httpContext, _routeData.Values));            

            // Now some validators
            var excludeFilters = new List<IExcludeTypeValidationFilter>();
            IObjectModelValidator objectModelValidator = new DefaultObjectValidator(excludeFilters, metaDataProvider);
            IModelValidatorProvider validatorProvider = new DefaultModelValidatorProvider();

            // Now try to bind the model
            bool success = ModelBindingHelper.TryUpdateModelAsync(
                model,
                prefix,
                _httpContext,
                modelState,
                metaDataProvider,
                modelBinder,
                valueProvider.Result,
                InputFormatters,
                objectModelValidator,
                validatorProvider
                ).Result;
            Console.WriteLine(success);
        }

        /// <summary>
        /// Read the model from the message body using a JsonInputFormatter
        /// </summary>
        /// <param name="requestType">The type of the request model that we will try to activate</param>
        /// <returns>An instance of requestType</returns>
        private dynamic DeserializeMessageBody(Type requestType)
        {
            // First create a context so that our formatter knows how to 
            // deserialize the model
            var context = new InputFormatterContext(
                _httpContext,
                modelName: string.Empty,
                modelState: new ModelStateDictionary(),
                metadata: requestType.MetaData());

            // Next create a formatter (we only support JSON at the moment)
            var formatter = new JsonInputFormatter();

            // Finally, have the formatter return a model from the http request body
            return formatter.ReadAsync(context).Result.Model;
        }
    }
}

