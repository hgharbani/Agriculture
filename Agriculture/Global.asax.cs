using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using Agriculture.App_Start;
using Agriculture.Uitlity;
using MyCmsWebsite;

namespace Agriculture
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new CustomRazorViewEngine());
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ModelBinders.Binders.Add(typeof(DateTime), new DateTimeBinder());
        }
        public class StructureMapDependencyResolver : IDependencyResolver
        {
            public object GetService(Type serviceType)
            {
                if (serviceType.IsAbstract || serviceType.IsInterface || !serviceType.IsClass)
                    return SmObjectFactory.Container.TryGetInstance(serviceType);
                return SmObjectFactory.Container.GetInstance(serviceType);
            }

            public IEnumerable<object> GetServices(Type serviceType)
            {
                return SmObjectFactory.Container.GetAllInstances(serviceType).Cast<object>();
            }
            private static DateTime ParseDate(string s)
            {
                DateTime result;
                if (!DateTime.TryParse(s, out result))
                {
                    result = DateTime.ParseExact(s, "yyyy-MM-ddT24:mm:ssK", System.Globalization.CultureInfo.InvariantCulture);
                    result = result.AddDays(1);
                }
                return result;
            }
            [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
            public class AjaxOnly : ActionFilterAttribute
            {
                public override void OnActionExecuting(ActionExecutingContext filterContext)
                {
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        base.OnActionExecuting(filterContext);
                    }
                    else
                    {
                        throw new InvalidOperationException("This operation can only be accessed via Ajax requests");
                    }
                }
            }
        }
    }

}
