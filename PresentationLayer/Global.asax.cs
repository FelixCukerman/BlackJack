using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using BusinessLogicLayer.Service;

namespace PresentationLayer
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfiguration.Configure();
        }

        public class AutoMapperConfiguration
        {
            public static void Configure()
            {
                Mapper.Initialize(x =>
                {
                    x.AddProfile<MapperServiceProfile>();
                });
                Mapper.Configuration.AssertConfigurationIsValid();
            }
        }
    }
}
