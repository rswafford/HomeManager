using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
//using HomeManager.API.Config;
using Bootstrap;
using Bootstrap.AutoMapper;
using HomeManager.Domain.Entities.Core;
using HomeManager.Model.RequestModels;
using HomeManager.Web.App_Start;
using HomeManager.Web.DependencyResolution;
using log4net.Config;

namespace HomeManager.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Assembly.Load("HomeManager.Domain");
            Assembly.Load("HomeManager.Model");

            var logFile = new FileInfo(Server.MapPath("log4net.config"));
            XmlConfigurator.Configure(logFile);

            var config = GlobalConfiguration.Configuration;

            AreaRegistration.RegisterAllAreas();

            Bootstrapper.IncludingOnly
                .Assembly(Assembly.GetExecutingAssembly())
                .AndAssembly(Assembly.GetAssembly(typeof(MovieImportRequestModel)))
                .AndAssembly(Assembly.GetAssembly(typeof(IEntity)))
                .With.AutoMapper().Excluding.Assembly("Autofac").Start();


            WebApiConfig.Register(config);
            EFConfig.Initialize();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }
    }
}