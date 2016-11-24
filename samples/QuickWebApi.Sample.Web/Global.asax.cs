using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QuickWebApi.Sample.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //DependencyResolver.SetResolver(IDependencyResolver
            //QuickWebApiFactory.Instance.Set_User_DependencyReslover(solver);
            QuickWebApiFactory.Instance.Build_Apis(true);
            QuickWebApiFactory.Instance.Load_Apis();
        }
        //public static User solver(HttpContextBase context) {
        //    //context.
        //}
    }

}
