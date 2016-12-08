using CourseWorkMT2.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;


namespace CourseWorkMT2
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            ODataModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Category>("Categories");
            builder.EntitySet<CustomerDemographic>("CustomerDemographics");
            builder.EntitySet<Region>("Regions");
            builder.EntitySet<Shipper>("Shippers");
            builder.EntitySet<Territory>("Territories");
            config.Routes.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: null,
                model: builder.GetEdmModel());
        }
    }
}
