using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CourseWorkMT2.DAL;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using System.Web.OData.Batch;
using Microsoft.OData.Edm;

namespace CourseWorkMT2.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapODataServiceRoute("odata", null, GetEdmModel(), new DefaultODataBatchHandler(GlobalConfiguration.DefaultServer));
            config.EnsureInitialized();
            //// Web API configuration and services
            //ODataModelBuilder builder = new ODataConventionModelBuilder();
            //builder.EntitySet<Region>("Regions");

            //// Web API routes
            //config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }

        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.Namespace = "Demos";
            builder.ContainerName = "DefaultContainer";
            builder.EntitySet<Region>("Regions");
            builder.EntitySet<Territory>("Territories");
            builder.EntitySet<Category>("Categories");
            builder.EntitySet<Supplier>("Suppliers");
            builder.EntitySet<Employee>("Employees");
            var edmModel = builder.GetEdmModel();
            return edmModel;
        }
    }
}
