using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace QuickWebApi.Sample.Service
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            //GlobalConfiguration.Configuration.MessageHandlers.Add(new AuthenticationHandler());  
            //webapifactory.Instance.Register_AuthenticationHandler(GlobalConfiguration.Configuration);
            QuickWebApiFactory.Instance.Build_Apis();
            //webapifactory.Instance.Load_Apis();
        }

        protected void Session_Start()
        {
            Console.WriteLine(Session.SessionID);
        }
        protected void Application_PostAuthorizeRequest()
        {
            System.Web.HttpContext.Current.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required);
        }
    }
}
