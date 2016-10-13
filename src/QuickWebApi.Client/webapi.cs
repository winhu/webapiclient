using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace QuickWebApi
{
    public class Api
    {
        public Api() { }
        public Api(string service_prefix, KeyValuePair<string, string>[] authentication = null)
        {
            _service_prefix = service_prefix;
        }

        protected string build_server(string srv)
        {
            return string.IsNullOrWhiteSpace(_service_prefix) ? srv : string.Format("{0}_{1}", _service_prefix, srv);
        }

        string _service_prefix;
        KeyValuePair<string, string>[] _authentication;
        public virtual Api WithAuthentication(KeyValuePair<string, string>[] authentication)
        {
            _authentication = authentication;
            return this;
        }

        public WsModel<Trequest> invoke<T, Trequest>(Expression<Func<T, ApiActionO<WsModel<Trequest>>>> func,
            WsModel<Trequest> model)
        {
            return _invoke_data<T, Trequest>(func.Body, model);
        }

        public WsModel<Trequest, Tresponse> invoke<T, Trequest, Tresponse>(Expression<Func<T, ApiActionO<WsModel<Trequest, Tresponse>>>> func,
            WsModel<Trequest, Tresponse> model)
        {
            return _invoke_data<T, Trequest, Tresponse>(func.Body, model);
        }

        #region 请求参数为基本类型，以url的形式进行传送，返回参数为已知类型
        public WsModel<string, Tresponse> invoke<T, Tresponse>(Expression<Func<T, ApiAction>> func)
        {
            return _invoke_data<T, string, Tresponse>(func.Body, null);
        }

        public WsModel<string, Tresponse> invoke<T, Tresponse>(Expression<Func<T, ApiActionL>> func, long args1)
        {
            return _invoke<T, Tresponse>(func.Body, args1);
        }
        public WsModel<string, Tresponse> invoke<T, Tresponse>(Expression<Func<T, ApiActionLL>> func, long args1, long args2)
        {
            return _invoke<T, Tresponse>(func.Body, args1, args2);
        }
        public WsModel<string, Tresponse> invoke<T, Tresponse>(Expression<Func<T, ApiActionLI>> func, long args1, int args2)
        {
            return _invoke<T, Tresponse>(func.Body, args1, args2);
        }
        public WsModel<string, Tresponse> invoke<T, Tresponse>(Expression<Func<T, ApiActionLS>> func, long args1, string args2)
        {
            return _invoke<T, Tresponse>(func.Body, args1, args2);
        }
        public WsModel<string, Tresponse> invoke<T, Tresponse>(Expression<Func<T, ApiActionI>> func, int args1)
        {
            return _invoke<T, Tresponse>(func.Body, args1);
        }
        public WsModel<string, Tresponse> invoke<T, Tresponse>(Expression<Func<T, ApiActionII>> func, int args1, int args2)
        {
            return _invoke<T, Tresponse>(func.Body, args1, args2);
        }
        public WsModel<string, Tresponse> invoke<T, Tresponse>(Expression<Func<T, ApiActionIL>> func, int args1, long args2)
        {
            return _invoke<T, Tresponse>(func.Body, args1, args2);
        }
        public WsModel<string, Tresponse> invoke<T, Tresponse>(Expression<Func<T, ApiActionIS>> func, int args1, string args2)
        {
            return _invoke<T, Tresponse>(func.Body, args1, args2);
        }
        public WsModel<string, Tresponse> invoke<T, Tresponse>(Expression<Func<T, ApiActionSS>> func, string args1, string args2)
        {
            return _invoke<T, Tresponse>(func.Body, args1, args2);
        }
        public WsModel<string, Tresponse> invoke<T, Tresponse>(Expression<Func<T, ApiActionSI>> func, string args1, int args2)
        {
            return _invoke<T, Tresponse>(func.Body, args1, args2);
        }
        public WsModel<string, Tresponse> invoke<T, Tresponse>(Expression<Func<T, ApiActionSL>> func, string args1, long args2)
        {
            return _invoke<T, Tresponse>(func.Body, args1, args2);
        }
        public WsModel<string, Tresponse> invoke<T, Tresponse>(Expression<Func<T, ApiActionSSS>> func, string args1, string args2, string args3)
        {
            return _invoke<T, Tresponse>(func.Body, args1, args2, args3);
        }
        public WsModel<string, Tresponse> invoke<T, Tresponse>(Expression<Func<T, ApiActionSSL>> func, string args1, string args2, long args3)
        {
            return _invoke<T, Tresponse>(func.Body, args1, args2, args3);
        }
        public WsModel<string, Tresponse> invoke<T, Tresponse>(Expression<Func<T, ApiActionSLL>> func, string args1, long args2, long args3)
        {
            return _invoke<T, Tresponse>(func.Body, args1, args2, args3);
        }
        public WsModel<string, Tresponse> invoke<T, Tresponse>(Expression<Func<T, ApiActionSSI>> func, string args1, string args2, int args3)
        {
            return _invoke<T, Tresponse>(func.Body, args1, args2, args3);
        }
        public WsModel<string, Tresponse> invoke<T, Tresponse>(Expression<Func<T, ApiActionSII>> func, string args1, int args2, int args3)
        {
            return _invoke<T, Tresponse>(func.Body, args1, args2, args3);
        }
        #endregion

        #region 请求参数为基本类型，以url的形式进行传送，返回参数为未知类型
        public WsModel<string> invoke<T>(Expression<Func<T, ApiAction>> func)
        {
            return _invoke_data<T, string>(func.Body, null);
        }

        public WsModel<string> invoke<T>(Expression<Func<T, ApiActionL>> func, long args1)
        {
            return _invoke<T>(func.Body, args1);
        }
        public WsModel<string> invoke<T>(Expression<Func<T, ApiActionLL>> func, long args1, long args2)
        {
            return _invoke<T>(func.Body, args1, args2);
        }
        public WsModel<string> invoke<T>(Expression<Func<T, ApiActionLI>> func, long args1, int args2)
        {
            return _invoke<T>(func.Body, args1, args2);
        }
        public WsModel<string> invoke<T>(Expression<Func<T, ApiActionLS>> func, long args1, string args2)
        {
            return _invoke<T>(func.Body, args1, args2);
        }
        public WsModel<string> invoke<T>(Expression<Func<T, ApiActionI>> func, int args1)
        {
            return _invoke<T>(func.Body, args1);
        }
        public WsModel<string> invoke<T>(Expression<Func<T, ApiActionII>> func, int args1, int args2)
        {
            return _invoke<T>(func.Body, args1, args2);
        }
        public WsModel<string> invoke<T>(Expression<Func<T, ApiActionIL>> func, int args1, long args2)
        {
            return _invoke<T>(func.Body, args1, args2);
        }
        public WsModel<string> invoke<T>(Expression<Func<T, ApiActionIS>> func, int args1, string args2)
        {
            return _invoke<T>(func.Body, args1, args2);
        }
        public WsModel<string> invoke<T>(Expression<Func<T, ApiActionSS>> func, string args1, string args2)
        {
            return _invoke<T>(func.Body, args1, args2);
        }
        public WsModel<string> invoke<T>(Expression<Func<T, ApiActionSI>> func, string args1, int args2)
        {
            return _invoke<T>(func.Body, args1, args2);
        }
        public WsModel<string> invoke<T>(Expression<Func<T, ApiActionSL>> func, string args1, long args2)
        {
            return _invoke<T>(func.Body, args1, args2);
        }
        public WsModel<string> invoke<T>(Expression<Func<T, ApiActionSSS>> func, string args1, string args2, string args3)
        {
            return _invoke<T>(func.Body, args1, args2, args3);
        }
        public WsModel<string> invoke<T>(Expression<Func<T, ApiActionSSL>> func, string args1, string args2, long args3)
        {
            return _invoke<T>(func.Body, args1, args2, args3);
        }
        public WsModel<string> invoke<T>(Expression<Func<T, ApiActionSLL>> func, string args1, long args2, long args3)
        {
            return _invoke<T>(func.Body, args1, args2, args3);
        }
        public WsModel<string> invoke<T>(Expression<Func<T, ApiActionSSI>> func, string args1, string args2, int args3)
        {
            return _invoke<T>(func.Body, args1, args2, args3);
        }
        public WsModel<string> invoke<T>(Expression<Func<T, ApiActionSII>> func, string args1, int args2, int args3)
        {
            return _invoke<T>(func.Body, args1, args2, args3);
        }
        #endregion
        

        protected WsModel<string> _invoke<T>(Expression exp, params object[] args)
        {
            var method = ((exp as UnaryExpression).Operand as MethodCallExpression);
            string code = ((method.Object as ConstantExpression).Value as MethodInfo).Name;

            foreach (var m in method.Arguments)
            {
                if (m.Type == typeof(T))
                {
                    var attr = m.Type.GetCustomAttribute<WebApiAttribute>();
                    StringBuilder sb = new StringBuilder();
                    var pis = m.Type.GetMethod(code).GetParameters();

                    for (int i = 0; i < pis.Length; i++)
                    {
                        sb.AppendFormat("{0}={1}&", pis[i].Name, args[i] is DateTime ? ((DateTime)args[i]).ToString("yyyy-MM-dd HH:mm:ss") : args[i]);
                    }

                    if (attr != null)
                    {
                        return new Invoker(build_server(attr.service)).Invoke(code, sb.ToString());
                    }
                }
            }
            var model = new WsModel<string>();
            model.ERROR(-1, "未能找到合适的api定义");
            return model;
        }
        protected WsModel<string, Tresponse> _invoke<T, Tresponse>(Expression exp, params object[] args)
        {
            var method = ((exp as UnaryExpression).Operand as MethodCallExpression);
            string code = ((method.Object as ConstantExpression).Value as MethodInfo).Name;

            foreach (var m in method.Arguments)
            {
                if (m.Type == typeof(T))
                {
                    var attr = m.Type.GetCustomAttribute<WebApiAttribute>();
                    StringBuilder sb = new StringBuilder();
                    var pis = m.Type.GetMethod(code).GetParameters();

                    for (int i = 0; i < pis.Length; i++)
                    {
                        sb.AppendFormat("{0}={1}&", pis[i].Name, args[i] is DateTime ? ((DateTime)args[i]).ToString("yyyy-MM-dd HH:mm:ss") : args[i]);
                    }

                    if (attr != null)
                    {
                        return new Invoker(build_server(attr.service)).Invoke<Tresponse>(code, sb.ToString());
                    }
                }
            }
            var model = new WsModel<string, Tresponse>();
            model.ERROR(-1, "未能找到合适的api定义");
            return model;
        }
        protected WsModel<Trequest, Tresponse> _invoke_data<T, Trequest, Tresponse>(Expression exp, WsModel<Trequest, Tresponse> model)
        {
            var method = ((exp as UnaryExpression).Operand as MethodCallExpression);
            string code = ((method.Object as ConstantExpression).Value as MethodInfo).Name;

            foreach (var m in method.Arguments)
            {
                if (m.Type == typeof(T))
                {
                    var attr = m.Type.GetCustomAttribute<WebApiAttribute>();
                    if (attr != null)
                    {
                        if (model == null || model.Request == null || model.Request is string)
                        {
                            model = new WsModel<Trequest, Tresponse>();
                            var ret = new Invoker(build_server(attr.service)).Invoke<Tresponse>(code, string.Empty);
                            model.Response = ret.Response; ;
                            model.User = ret.User;
                            model.Secret = ret.Secret;
                            model.Client = ret.Client;
                            model.Time = ret.Time;
                            model.Signature = ret.Signature;
                            model.ErrCode = ret.ErrCode;
                            model.ErrMsg = ret.ErrMsg;
                            return model;
                        }
                        return new Invoker(build_server(attr.service)).Invoke<Trequest, Tresponse>(code, model);
                    }
                }
            }
            if (model == null) model = new WsModel<Trequest, Tresponse>();
            model.ERROR(-1, "未能找到合适的api定义");
            return model;
        }
        protected WsModel<Trequest> _invoke_data<T, Trequest>(Expression exp, WsModel<Trequest> model)
        {
            var method = ((exp as UnaryExpression).Operand as MethodCallExpression);
            string code = ((method.Object as ConstantExpression).Value as MethodInfo).Name;

            foreach (var m in method.Arguments)
            {
                if (m.Type == typeof(T))
                {
                    var attr = m.Type.GetCustomAttribute<WebApiAttribute>();
                    if (attr != null)
                    {
                        return new Invoker(build_server(attr.service)).Invoke<Trequest>(code, model);
                    }
                }
            }
            if (model == null) model = new WsModel<Trequest>();
            model.ERROR(-1, "未能找到合适的api定义");
            return model;
        }

    }


}
