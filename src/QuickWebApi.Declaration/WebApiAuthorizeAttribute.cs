using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace QuickWebApi
{

    public class WebApiAuthorizeAttribute : AuthorizeAttribute
    {
        private object GetCache(string CacheKey)
        {
            return HttpRuntime.Cache[CacheKey];
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);
            var s = actionContext.Request.Content.ReadAsStringAsync();
            s.Wait();

            var msg = ValidRequest(s.Result);
            if (string.IsNullOrWhiteSpace(msg)) return;

            HttpResponseMessage message = new HttpResponseMessage
            {
                Content = new StringContent(msg, Encoding.GetEncoding("UTF-8"), "application/json"),
                StatusCode = HttpStatusCode.Unauthorized
            };
            actionContext.Response = message;
        }

        private void SetCache(string CacheKey, object objObject, double expires_in)
        {
            HttpRuntime.Cache.Insert(CacheKey, objObject, null, DateTime.Now.AddHours(expires_in), Cache.NoSlidingExpiration);
        }

        public string ValidRequest(string requestdata)
        {
            WsModel model = Prepare(requestdata);
            if (model == null) return "Unauthorized Data";
            return ValidAccessToken(model.User.SysCode, model.Client.Ip, model.Secret.AccessToken) ??
                    ValidUser(model.User.Ticket, model.User.Uid, model.User.Uid);
        }

        public virtual WsModel Prepare(string requestdata)
        {
            return JsonConvert.DeserializeObject<WsModel>(requestdata);
        }

        public virtual string ValidUser(string ticket, string uid, string key)
        {
            return null;
            //return "Unauthorized User";
        }
        public virtual string ValidAccessToken(string syscode, string token, string ip)
        {
            //string cacheKey = string.Format("{0},{1},{2}", user.SysCode, client.Ip, secret.AccessToken);
            return null;
            //return "Unauthorized Access Token";
        }
    }
}
