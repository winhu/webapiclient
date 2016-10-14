using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace QuickWebApi
{

    public static class HttpContextHelper
    {
        public static HttpContextBase Base(this HttpContext context)
        {
            return new HttpContextWrapper(context);
        }

        #region READY
        public static Client GetClientInfo(this HttpContextBase context, string devicecode = "web", string deviceinfo = null)
        {
            return new Client(context.GetIP(), null, null, devicecode, context.Request.Browser.Id);
        }

        public static WsModel ApiModel(this HttpContextBase context, string devicecode = "web")
        {
            WsModel model = new WsModel();
            model.Client = new Client(context.GetIP(), null, null, devicecode, context.Request.Browser.Id);
            model.User = new User(context.Session.SessionID, context.Session[context.Session.SessionID]);
            return model;
        }
        public static WsModel<Trequest> ApiModel<Trequest>(this HttpContextBase context, Trequest request, string devicecode = "web")
        {
            WsModel<Trequest> model = new WsModel<Trequest>();
            model.Request = request;
            model.Client = new Client(context.GetIP(), null, null, devicecode, context.Request.Browser.Id);
            model.User = new User(context.Session.SessionID, context.Session[context.Session.SessionID]);
            return model;
        }
        public static WsModel<Trequest, Tresponse> ApiModel<Trequest, Tresponse>(this HttpContextBase context, Trequest request, string devicecode = "web")
        {
            WsModel<Trequest, Tresponse> model = new WsModel<Trequest, Tresponse>();
            model.Request = request;
            model.Client = new Client(context.GetIP(), null, null, devicecode, context.Request.Browser.Id);
            model.User = new User(context.Session.SessionID, context.Session[context.Session.SessionID]);
            return model;
        }
        #endregion

        //public static ws_model<Trequest, Tresponse> ApiInvoke<T, Trequest, Tresponse>(this HttpContextBase context, 
        //    Expression<Func<T, apiaction_o<ws_model<Trequest>>>> func, 
        //    Trequest request, 
        //    string prefix = null, 
        //    string devicecode = "web")
        //{
        //    return new webapi<T, Trequest, Tresponse>(prefix).invoke(func, context.ApiModel<Trequest, Tresponse>(request, devicecode));
        //    //return new webapi<T, ws_model<Trequest, Tresponse>>(prefix).invoke(func, context.ApiModel<Trequest>(request, devicecode));
        //}

        public static WsModel<Trequest, Tresponse> ApiInvoke<T, Trequest, Tresponse>(this HttpContextBase context,
            Expression<Func<T, ApiActionO<WsModel<Trequest, Tresponse>>>> func,
            Trequest request,
            string prefix = null,
            string devicecode = "web")
        {

            return new Api(prefix).invoke<T, Trequest, Tresponse>(
                func,
                context.ApiModel<Trequest, Tresponse>(request, devicecode));
        }
        public static WsModel<string, Tresponse> ApiInvoke<T, Tresponse>(this HttpContextBase context,
            Expression<Func<T, ApiActionO<WsModel<string, Tresponse>>>> func,
            string request,
            string prefix = null,
            string devicecode = "web")
        {

            return new Api(prefix).invoke<T, string, Tresponse>(
                func,
                context.ApiModel<string, Tresponse>(request, devicecode));
        }

        public static WsModel<Trequest> ApiInvoke<T, Trequest>(this HttpContextBase context,
            Expression<Func<T, ApiActionO<WsModel<Trequest>>>> func,
            Trequest request,
            string prefix = null,
            string devicecode = "web")
        {
            return new Api(prefix).invoke<T, Trequest>(func, context.ApiModel<Trequest>(request, devicecode));
        }

        public static WsModel<string, Tresponse> ApiInvoke<T, Tresponse>(this HttpContextBase context,
                                                                    Expression<Func<T, ApiAction>> func,
                                                                    string prefix = null,
                                                                    string devicecode = "web")
        {
            return new Api(prefix).invoke<T, Tresponse>(func);
        }
        public static WsModel<string, Tresponse> ApiInvoke<T, Tresponse>(this HttpContextBase context,
                                                                    Expression<Func<T, ApiActionS>> func, string arg,
                                                                    string prefix = null,
                                                                    string devicecode = "web")
        {
            return new Api(prefix).invoke<T, Tresponse>(func, arg);
        }
        public static WsModel<string, Tresponse> ApiInvoke<T, Tresponse>(this HttpContextBase context,
                                                                    Expression<Func<T, ApiActionSSS>> func, string arg1, string arg2, string arg3,
                                                                    string prefix = null,
                                                                    string devicecode = "web")
        {
            return new Api(prefix).invoke<T, Tresponse>(func, arg1, arg2, arg3);
        }
        public static WsModel<string, Tresponse> ApiInvoke<T, Tresponse>(this HttpContextBase context,
                                                           Expression<Func<T, ApiActionII>> func, int arg1, int arg2,
                                                           string prefix = null,
                                                           string devicecode = "web")
        {
            return new Api(prefix).invoke<T, Tresponse>(func, arg1, arg2);
        }
        public static WsModel<string, Tresponse> ApiInvoke<T, Tresponse>(this HttpContextBase context,
                                                           Expression<Func<T, ApiActionIL>> func, int arg1, long arg2,
                                                           string prefix = null,
                                                           string devicecode = "web")
        {
            return new Api(prefix).invoke<T, Tresponse>(func, arg1, arg2);
        }
        public static WsModel<string, Tresponse> ApiInvoke<T, Tresponse>(this HttpContextBase context,
                                                           Expression<Func<T, ApiActionLL>> func, long arg1, long arg2,
                                                           string prefix = null,
                                                           string devicecode = "web")
        {
            return new Api(prefix).invoke<T, Tresponse>(func, arg1, arg2);
        }
        public static WsModel<string, Tresponse> ApiInvoke<T, Tresponse>(this HttpContextBase context,
                                                           Expression<Func<T, ApiActionLI>> func, long arg1, int arg2,
                                                           string prefix = null,
                                                           string devicecode = "web")
        {
            return new Api(prefix).invoke<T, Tresponse>(func, arg1, arg2);
        }
        public static WsModel<string, Tresponse> ApiInvoke<T, Tresponse>(this HttpContextBase context,
                                                           Expression<Func<T, ApiActionI>> func, int arg1,
                                                           string prefix = null,
                                                           string devicecode = "web")
        {
            return new Api(prefix).invoke<T, Tresponse>(func, arg1);
        }
        public static WsModel<string, Tresponse> ApiInvoke<T, Tresponse>(this HttpContextBase context,
                                                           Expression<Func<T, ApiActionL>> func, long arg1,
                                                           string prefix = null,
                                                           string devicecode = "web")
        {
            return new Api(prefix).invoke<T, Tresponse>(func, arg1);
        }
        public static WsModel<string, Tresponse> ApiInvoke<T, Tresponse>(this HttpContextBase context,
                                                           Expression<Func<T, ApiActionSI>> func, string arg1, int arg2,
                                                           string prefix = null,
                                                           string devicecode = "web")
        {
            return new Api(prefix).invoke<T, Tresponse>(func, arg1, arg2);
        }
        public static WsModel<string, Tresponse> ApiInvoke<T, Tresponse>(this HttpContextBase context,
                                                           Expression<Func<T, ApiActionSL>> func, string arg1, long arg2,
                                                           string prefix = null,
                                                           string devicecode = "web")
        {
            return new Api(prefix).invoke<T, Tresponse>(func, arg1, arg2);
        }
        public static WsModel<string, Tresponse> ApiInvoke<T, Tresponse>(this HttpContextBase context,
                                                           Expression<Func<T, ApiActionIS>> func, int arg1, string arg2,
                                                           string prefix = null,
                                                           string devicecode = "web")
        {
            return new Api(prefix).invoke<T, Tresponse>(func, arg1, arg2);
        }
        public static WsModel<string, Tresponse> ApiInvoke<T, Tresponse>(this HttpContextBase context,
                                                           Expression<Func<T, ApiActionLS>> func, long arg1, string arg2,
                                                           string prefix = null,
                                                           string devicecode = "web")
        {
            return new Api(prefix).invoke<T, Tresponse>(func, arg1, arg2);
        }
        public static WsModel<string, Tresponse> ApiInvoke<T, Tresponse>(this HttpContextBase context,
                                                           Expression<Func<T, ApiActionSS>> func, string arg1, string arg2,
                                                           string prefix = null,
                                                           string devicecode = "web")
        {
            return new Api(prefix).invoke<T, Tresponse>(func, arg1, arg2);
        }
        public static WsModel<string, Tresponse> ApiInvoke<T, Tresponse>(this HttpContextBase context,
                                                           Expression<Func<T, ApiActionSSL>> func, string arg1, string arg2, long arg3,
                                                           string prefix = null,
                                                           string devicecode = "web")
        {
            return new Api(prefix).invoke<T, Tresponse>(func, arg1, arg2, arg3);
        }
        public static WsModel<string, Tresponse> ApiInvoke<T, Tresponse>(this HttpContextBase context,
                                                           Expression<Func<T, ApiActionSSI>> func, string arg1, string arg2, int arg3,
                                                           string prefix = null,
                                                           string devicecode = "web")
        {
            return new Api(prefix).invoke<T, Tresponse>(func, arg1, arg2, arg3);
        }
        public static WsModel<string, Tresponse> ApiInvoke<T, Tresponse>(this HttpContextBase context,
                                                           Expression<Func<T, ApiActionSLL>> func, string arg1, long arg2, long arg3,
                                                           string prefix = null,
                                                           string devicecode = "web")
        {
            return new Api(prefix).invoke<T, Tresponse>(func, arg1, arg2, arg3);
        }
        public static WsModel<string, Tresponse> ApiInvoke<T, Tresponse>(this HttpContextBase context,
                                                           Expression<Func<T, ApiActionSII>> func, string arg1, int arg2, int arg3,
                                                           string prefix = null,
                                                           string devicecode = "web")
        {
            return new Api(prefix).invoke<T, Tresponse>(func, arg1, arg2, arg3);
        }

        //==================================================================================

        public static WsModel<string> ApiInvoke<T>(this HttpContextBase context,
                                                                    Expression<Func<T, ApiAction>> func,
                                                                    string prefix = null,
                                                                    string devicecode = "web")
        {
            return new Api(prefix).invoke<T>(func);
        }
        public static WsModel<string> ApiInvoke<T>(this HttpContextBase context,
                                                                    Expression<Func<T, ApiActionSSS>> func, string arg1, string arg2, string arg3,
                                                                    string prefix = null,
                                                                    string devicecode = "web")
        {
            return new Api(prefix).invoke<T>(func, arg1, arg2, arg3);
        }
        public static WsModel<string> ApiInvoke<T>(this HttpContextBase context,
                                                           Expression<Func<T, ApiActionII>> func, int arg1, int arg2,
                                                           string prefix = null,
                                                           string devicecode = "web")
        {
            return new Api(prefix).invoke<T>(func, arg1, arg2);
        }
        public static WsModel<string> ApiInvoke<T>(this HttpContextBase context,
                                                           Expression<Func<T, ApiActionIL>> func, int arg1, long arg2,
                                                           string prefix = null,
                                                           string devicecode = "web")
        {
            return new Api(prefix).invoke<T>(func, arg1, arg2);
        }
        public static WsModel<string> ApiInvoke<T>(this HttpContextBase context,
                                                           Expression<Func<T, ApiActionLL>> func, long arg1, long arg2,
                                                           string prefix = null,
                                                           string devicecode = "web")
        {
            return new Api(prefix).invoke<T>(func, arg1, arg2);
        }
        public static WsModel<string> ApiInvoke<T>(this HttpContextBase context,
                                                           Expression<Func<T, ApiActionLI>> func, long arg1, int arg2,
                                                           string prefix = null,
                                                           string devicecode = "web")
        {
            return new Api(prefix).invoke<T>(func, arg1, arg2);
        }
        public static WsModel<string> ApiInvoke<T>(this HttpContextBase context,
                                                           Expression<Func<T, ApiActionI>> func, int arg1,
                                                           string prefix = null,
                                                           string devicecode = "web")
        {
            return new Api(prefix).invoke<T>(func, arg1);
        }
        public static WsModel<string> ApiInvoke<T>(this HttpContextBase context,
                                                           Expression<Func<T, ApiActionL>> func, long arg1,
                                                           string prefix = null,
                                                           string devicecode = "web")
        {
            return new Api(prefix).invoke<T>(func, arg1);
        }
        public static WsModel<string> ApiInvoke<T>(this HttpContextBase context,
                                                           Expression<Func<T, ApiActionSI>> func, string arg1, int arg2,
                                                           string prefix = null,
                                                           string devicecode = "web")
        {
            return new Api(prefix).invoke<T>(func, arg1, arg2);
        }
        public static WsModel<string> ApiInvoke<T>(this HttpContextBase context,
                                                           Expression<Func<T, ApiActionSL>> func, string arg1, long arg2,
                                                           string prefix = null,
                                                           string devicecode = "web")
        {
            return new Api(prefix).invoke<T>(func, arg1, arg2);
        }
        public static WsModel<string> ApiInvoke<T>(this HttpContextBase context,
                                                           Expression<Func<T, ApiActionIS>> func, int arg1, string arg2,
                                                           string prefix = null,
                                                           string devicecode = "web")
        {
            return new Api(prefix).invoke<T>(func, arg1, arg2);
        }
        public static WsModel<string> ApiInvoke<T>(this HttpContextBase context,
                                                           Expression<Func<T, ApiActionLS>> func, long arg1, string arg2,
                                                           string prefix = null,
                                                           string devicecode = "web")
        {
            return new Api(prefix).invoke<T>(func, arg1, arg2);
        }
        public static WsModel<string> ApiInvoke<T>(this HttpContextBase context,
                                                           Expression<Func<T, ApiActionSS>> func, string arg1, string arg2,
                                                           string prefix = null,
                                                           string devicecode = "web")
        {
            return new Api(prefix).invoke<T>(func, arg1, arg2);
        }
        public static WsModel<string> ApiInvoke<T>(this HttpContextBase context,
                                                           Expression<Func<T, ApiActionSSL>> func, string arg1, string arg2, long arg3,
                                                           string prefix = null,
                                                           string devicecode = "web")
        {
            return new Api(prefix).invoke<T>(func, arg1, arg2, arg3);
        }
        public static WsModel<string> ApiInvoke<T>(this HttpContextBase context,
                                                           Expression<Func<T, ApiActionSSI>> func, string arg1, string arg2, int arg3,
                                                           string prefix = null,
                                                           string devicecode = "web")
        {
            return new Api(prefix).invoke<T>(func, arg1, arg2, arg3);
        }
        public static WsModel<string> ApiInvoke<T>(this HttpContextBase context,
                                                           Expression<Func<T, ApiActionSLL>> func, string arg1, long arg2, long arg3,
                                                           string prefix = null,
                                                           string devicecode = "web")
        {
            return new Api(prefix).invoke<T>(func, arg1, arg2, arg3);
        }
        public static WsModel<string> ApiInvoke<T>(this HttpContextBase context,
                                                           Expression<Func<T, ApiActionSII>> func, string arg1, int arg2, int arg3,
                                                           string prefix = null,
                                                           string devicecode = "web")
        {
            return new Api(prefix).invoke<T>(func, arg1, arg2, arg3);
        }



        /// <summary>
        /// 获取当前用户的IP地址
        /// </summary>
        /// <returns></returns>
        static string GetIP(this HttpContextBase context)
        {
            return context.Request.GetIP();
        }
        static string GetIP(this HttpRequestBase request)
        {
            // 穿过代理服务器取远程用户真实IP地址
            string Ip = string.Empty;
            if (request.ServerVariables["HTTP_VIA"] != null)
            {
                if (request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null)
                {
                    if (request.ServerVariables["HTTP_CLIENT_IP"] != null)
                        Ip = request.ServerVariables["HTTP_CLIENT_IP"].ToString();
                    else
                        if (request.ServerVariables["REMOTE_ADDR"] != null)
                            Ip = request.ServerVariables["REMOTE_ADDR"].ToString();
                }
                else
                    Ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (request.ServerVariables["REMOTE_ADDR"] != null)
            {
                Ip = request.ServerVariables["REMOTE_ADDR"].ToString();
            }

            if (ip_reg.IsMatch(Ip))
                return Ip;
            else return "127.0.0.1";
        }
        private static Regex ip_reg = new Regex(@"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])(\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])){3}$");

    }
}
