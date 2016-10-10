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
        public static Client RequestDevice(this HttpContextBase context, string devicecode = "web", string deviceinfo = null)
        {
            return new Client(context.GetIP(), null, null, devicecode, context.Request.Browser.Id);
        }

        public static ws_model ApiModel(this HttpContextBase context, string devicecode = "web")
        //where Trequest : class,new()
        {
            ws_model model = new ws_model();
            model.client = new Client(context.GetIP(), null, null, devicecode, context.Request.Browser.Id);
            model.user = new User(context.Session.SessionID, context.Session[context.Session.SessionID]);
            return model;
        }
        public static ws_model<Trequest> ApiModel<Trequest>(this HttpContextBase context, Trequest request, string devicecode = "web")
        //where Trequest : class,new()
        {
            ws_model<Trequest> model = new ws_model<Trequest>();
            model.request = request;
            model.client = new Client(context.GetIP(), null, null, devicecode, context.Request.Browser.Id);
            model.user = new User(context.Session.SessionID, context.Session[context.Session.SessionID]);
            return model;
        }
        public static ws_model<Trequest, Tresponse> ApiModel<Trequest, Tresponse>(this HttpContextBase context, Trequest request, string devicecode = "web")
        //where Trequest : class,new()
        //where Tresponse : class,new()
        {
            ws_model<Trequest, Tresponse> model = new ws_model<Trequest, Tresponse>();
            model.request = request;
            model.client = new Client(context.GetIP(), null, null, devicecode, context.Request.Browser.Id);
            model.user = new User(context.Session.SessionID, context.Session[context.Session.SessionID]);
            return model;
        }

        public static result<ws_model<Trequest, Tresponse>> ApiInvoke<T, Trequest, Tresponse>(this HttpContextBase context, Expression<Func<T, apiaction_o<ws_model<Trequest>>>> func, Trequest request, string prefix = null, string devicecode = "web")
        //where Trequest : class,new()
        //where Tresponse : class,new()
        {
            return new webapi<T, ws_model<Trequest, Tresponse>>(prefix).invoke(func, context.ApiModel<Trequest>(request, devicecode));
        }

        public static result<ws_model<Trequest, Tresponse>> ApiInvoke<T, Trequest, Tresponse>(this HttpContextBase context,
            Expression<Func<T, apiaction_o<ws_model<Trequest, Tresponse>>>> func,
            Trequest request,
            string prefix = null,
            string devicecode = "web")
        //where Trequest : class,new()
        //where Tresponse : class,new()
        {
            return new webapi<T, ws_model<Trequest, Tresponse>>(prefix).invoke(
                func,
                context.ApiModel<Trequest, Tresponse>(request, devicecode));
        }

        public static result ApiInvoke<T, Trequest>(this HttpContextBase context, Expression<Func<T, apiaction_o<ws_model<Trequest>>>> func, Trequest request, string prefix = null, string devicecode = "web")
        //where Trequest : class,new()
        {
            return new webapi<T>(prefix).invoke(func, context.ApiModel<Trequest>(request, devicecode));
        }

        public static result<Tresponse> ApiInvoke<T, Tresponse>(this HttpContextBase context,
                                                                    Expression<Func<T, apiaction>> func,
                                                                    string prefix = null,
                                                                    string devicecode = "web")
        //where Trequest : class,new()
        //where Tresponse : class,new()
        {
            return new webapi(prefix).invoke<T, Tresponse>(func);
        }
        public static result<Tresponse> ApiInvoke<T, Tresponse>(this HttpContextBase context,
                                                                    Expression<Func<T, apiaction_sss>> func, string arg1, string arg2, string arg3,
                                                                    string prefix = null,
                                                                    string devicecode = "web")
        //where Trequest : class,new()
        //where Tresponse : class,new()
        {
            return new webapi(prefix).invoke<T, Tresponse>(func, arg1, arg2, arg3);
        }
        public static result<Tresponse> ApiInvoke<T, Tresponse>(this HttpContextBase context,
                                                           Expression<Func<T, apiaction_ii>> func, int arg1, int arg2,
                                                           string prefix = null,
                                                           string devicecode = "web")
        //where Trequest : class,new()
        //where Tresponse : class,new()
        {
            return new webapi(prefix).invoke<T, Tresponse>(func, arg1, arg2);
        }
        public static result<Tresponse> ApiInvoke<T, Tresponse>(this HttpContextBase context,
                                                           Expression<Func<T, apiaction_il>> func, int arg1, long arg2,
                                                           string prefix = null,
                                                           string devicecode = "web")
        //where Trequest : class,new()
        //where Tresponse : class,new()
        {
            return new webapi(prefix).invoke<T, Tresponse>(func, arg1, arg2);
        }
        public static result<Tresponse> ApiInvoke<T, Tresponse>(this HttpContextBase context,
                                                           Expression<Func<T, apiaction_ll>> func, long arg1, long arg2,
                                                           string prefix = null,
                                                           string devicecode = "web")
        //where Trequest : class,new()
        //where Tresponse : class,new()
        {
            return new webapi(prefix).invoke<T, Tresponse>(func, arg1, arg2);
        }
        public static result<Tresponse> ApiInvoke<T, Tresponse>(this HttpContextBase context,
                                                           Expression<Func<T, apiaction_li>> func, long arg1, int arg2,
                                                           string prefix = null,
                                                           string devicecode = "web")
        //where Trequest : class,new()
        //where Tresponse : class,new()
        {
            return new webapi(prefix).invoke<T, Tresponse>(func, arg1, arg2);
        }
        public static result<Tresponse> ApiInvoke<T, Tresponse>(this HttpContextBase context,
                                                           Expression<Func<T, apiaction_i>> func, int arg1,
                                                           string prefix = null,
                                                           string devicecode = "web")
        //where Trequest : class,new()
        //where Tresponse : class,new()
        {
            return new webapi(prefix).invoke<T, Tresponse>(func, arg1);
        }
        public static result<Tresponse> ApiInvoke<T, Tresponse>(this HttpContextBase context,
                                                           Expression<Func<T, apiaction_l>> func, long arg1,
                                                           string prefix = null,
                                                           string devicecode = "web")
        //where Trequest : class,new()
        //where Tresponse : class,new()
        {
            return new webapi(prefix).invoke<T, Tresponse>(func, arg1);
        }
        public static result<Tresponse> ApiInvoke<T, Tresponse>(this HttpContextBase context,
                                                           Expression<Func<T, apiaction_si>> func, string arg1, int arg2,
                                                           string prefix = null,
                                                           string devicecode = "web")
        //where Trequest : class,new()
        //where Tresponse : class,new()
        {
            return new webapi(prefix).invoke<T, Tresponse>(func, arg1, arg2);
        }
        public static result<Tresponse> ApiInvoke<T, Tresponse>(this HttpContextBase context,
                                                           Expression<Func<T, apiaction_sl>> func, string arg1, long arg2,
                                                           string prefix = null,
                                                           string devicecode = "web")
        //where Trequest : class,new()
        //where Tresponse : class,new()
        {
            return new webapi(prefix).invoke<T, Tresponse>(func, arg1, arg2);
        }
        public static result<Tresponse> ApiInvoke<T, Tresponse>(this HttpContextBase context,
                                                           Expression<Func<T, apiaction_is>> func, int arg1, string arg2,
                                                           string prefix = null,
                                                           string devicecode = "web")
        //where Trequest : class,new()
        //where Tresponse : class,new()
        {
            return new webapi(prefix).invoke<T, Tresponse>(func, arg1, arg2);
        }
        public static result<Tresponse> ApiInvoke<T, Tresponse>(this HttpContextBase context,
                                                           Expression<Func<T, apiaction_ls>> func, long arg1, string arg2,
                                                           string prefix = null,
                                                           string devicecode = "web")
        //where Trequest : class,new()
        //where Tresponse : class,new()
        {
            return new webapi(prefix).invoke<T, Tresponse>(func, arg1, arg2);
        }
        public static result<Tresponse> ApiInvoke<T, Tresponse>(this HttpContextBase context,
                                                           Expression<Func<T, apiaction_ss>> func, string arg1, string arg2,
                                                           string prefix = null,
                                                           string devicecode = "web")
        //where Trequest : class,new()
        //where Tresponse : class,new()
        {
            return new webapi(prefix).invoke<T, Tresponse>(func, arg1, arg2);
        }
        public static result<Tresponse> ApiInvoke<T, Tresponse>(this HttpContextBase context,
                                                           Expression<Func<T, apiaction_ssl>> func, string arg1, string arg2, long arg3,
                                                           string prefix = null,
                                                           string devicecode = "web")
        //where Trequest : class,new()
        //where Tresponse : class,new()
        {
            return new webapi(prefix).invoke<T, Tresponse>(func, arg1, arg2, arg3);
        }
        public static result<Tresponse> ApiInvoke<T, Tresponse>(this HttpContextBase context,
                                                           Expression<Func<T, apiaction_ssi>> func, string arg1, string arg2, int arg3,
                                                           string prefix = null,
                                                           string devicecode = "web")
        //where Trequest : class,new()
        //where Tresponse : class,new()
        {
            return new webapi(prefix).invoke<T, Tresponse>(func, arg1, arg2, arg3);
        }
        public static result<Tresponse> ApiInvoke<T, Tresponse>(this HttpContextBase context,
                                                           Expression<Func<T, apiaction_sll>> func, string arg1, long arg2, long arg3,
                                                           string prefix = null,
                                                           string devicecode = "web")
        //where Trequest : class,new()
        //where Tresponse : class,new()
        {
            return new webapi(prefix).invoke<T, Tresponse>(func, arg1, arg2, arg3);
        }
        public static result<Tresponse> ApiInvoke<T, Tresponse>(this HttpContextBase context,
                                                           Expression<Func<T, apiaction_sii>> func, string arg1, int arg2, int arg3,
                                                           string prefix = null,
                                                           string devicecode = "web")
        //where Trequest : class,new()
        //where Tresponse : class,new()
        {
            return new webapi(prefix).invoke<T, Tresponse>(func, arg1, arg2, arg3);
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
