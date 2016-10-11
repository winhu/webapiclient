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
    public class api
    {
        public api() { }
        public api(string service_prefix, KeyValuePair<string, string>[] authentication = null)
        {
            _service_prefix = service_prefix;
        }

        protected string build_server(string srv)
        {
            return string.IsNullOrWhiteSpace(_service_prefix) ? srv : string.Format("{0}_{1}", _service_prefix, srv);
        }

        string _service_prefix;
        KeyValuePair<string, string>[] _authentication;
        public virtual api WithAuthentication(KeyValuePair<string, string>[] authentication)
        {
            _authentication = authentication;
            return this;
        }

        public ws_model<Trequest> invoke<T, Trequest>(Expression<Func<T, apiaction_o<ws_model<Trequest>>>> func,
            ws_model<Trequest> model)
        {
            return _invoke_data<T, Trequest>(func.Body, model);
        }

        public ws_model<Trequest, Tresponse> invoke<T, Trequest, Tresponse>(Expression<Func<T, apiaction_o<ws_model<Trequest, Tresponse>>>> func,
            ws_model<Trequest, Tresponse> model)
        {
            return _invoke_data<T, Trequest, Tresponse>(func.Body, model);
        }

        #region 请求参数为基本类型，以url的形式进行传送，返回参数为已知类型
        public ws_model<string, Tresponse> invoke<T, Tresponse>(Expression<Func<T, apiaction>> func)
        {
            return _invoke_data<T, string, Tresponse>(func.Body, null);
        }

        public ws_model<string, Tresponse> invoke<T, Tresponse>(Expression<Func<T, apiaction_l>> func, long args1)
        {
            return _invoke<T, Tresponse>(func.Body, args1);
        }
        public ws_model<string, Tresponse> invoke<T, Tresponse>(Expression<Func<T, apiaction_ll>> func, long args1, long args2)
        {
            return _invoke<T, Tresponse>(func.Body, args1, args2);
        }
        public ws_model<string, Tresponse> invoke<T, Tresponse>(Expression<Func<T, apiaction_li>> func, long args1, int args2)
        {
            return _invoke<T, Tresponse>(func.Body, args1, args2);
        }
        public ws_model<string, Tresponse> invoke<T, Tresponse>(Expression<Func<T, apiaction_ls>> func, long args1, string args2)
        {
            return _invoke<T, Tresponse>(func.Body, args1, args2);
        }
        public ws_model<string, Tresponse> invoke<T, Tresponse>(Expression<Func<T, apiaction_i>> func, int args1)
        {
            return _invoke<T, Tresponse>(func.Body, args1);
        }
        public ws_model<string, Tresponse> invoke<T, Tresponse>(Expression<Func<T, apiaction_ii>> func, int args1, int args2)
        {
            return _invoke<T, Tresponse>(func.Body, args1, args2);
        }
        public ws_model<string, Tresponse> invoke<T, Tresponse>(Expression<Func<T, apiaction_il>> func, int args1, long args2)
        {
            return _invoke<T, Tresponse>(func.Body, args1, args2);
        }
        public ws_model<string, Tresponse> invoke<T, Tresponse>(Expression<Func<T, apiaction_is>> func, int args1, string args2)
        {
            return _invoke<T, Tresponse>(func.Body, args1, args2);
        }
        public ws_model<string, Tresponse> invoke<T, Tresponse>(Expression<Func<T, apiaction_ss>> func, string args1, string args2)
        {
            return _invoke<T, Tresponse>(func.Body, args1, args2);
        }
        public ws_model<string, Tresponse> invoke<T, Tresponse>(Expression<Func<T, apiaction_si>> func, string args1, int args2)
        {
            return _invoke<T, Tresponse>(func.Body, args1, args2);
        }
        public ws_model<string, Tresponse> invoke<T, Tresponse>(Expression<Func<T, apiaction_sl>> func, string args1, long args2)
        {
            return _invoke<T, Tresponse>(func.Body, args1, args2);
        }
        public ws_model<string, Tresponse> invoke<T, Tresponse>(Expression<Func<T, apiaction_sss>> func, string args1, string args2, string args3)
        {
            return _invoke<T, Tresponse>(func.Body, args1, args2, args3);
        }
        public ws_model<string, Tresponse> invoke<T, Tresponse>(Expression<Func<T, apiaction_ssl>> func, string args1, string args2, long args3)
        {
            return _invoke<T, Tresponse>(func.Body, args1, args2, args3);
        }
        public ws_model<string, Tresponse> invoke<T, Tresponse>(Expression<Func<T, apiaction_sll>> func, string args1, long args2, long args3)
        {
            return _invoke<T, Tresponse>(func.Body, args1, args2, args3);
        }
        public ws_model<string, Tresponse> invoke<T, Tresponse>(Expression<Func<T, apiaction_ssi>> func, string args1, string args2, int args3)
        {
            return _invoke<T, Tresponse>(func.Body, args1, args2, args3);
        }
        public ws_model<string, Tresponse> invoke<T, Tresponse>(Expression<Func<T, apiaction_sii>> func, string args1, int args2, int args3)
        {
            return _invoke<T, Tresponse>(func.Body, args1, args2, args3);
        }
        #endregion

        #region 请求参数为基本类型，以url的形式进行传送，返回参数为未知类型
        public ws_model<string> invoke<T>(Expression<Func<T, apiaction>> func)
        {
            return _invoke_data<T, string>(func.Body, null);
        }

        public ws_model<string> invoke<T>(Expression<Func<T, apiaction_l>> func, long args1)
        {
            return _invoke<T>(func.Body, args1);
        }
        public ws_model<string> invoke<T>(Expression<Func<T, apiaction_ll>> func, long args1, long args2)
        {
            return _invoke<T>(func.Body, args1, args2);
        }
        public ws_model<string> invoke<T>(Expression<Func<T, apiaction_li>> func, long args1, int args2)
        {
            return _invoke<T>(func.Body, args1, args2);
        }
        public ws_model<string> invoke<T>(Expression<Func<T, apiaction_ls>> func, long args1, string args2)
        {
            return _invoke<T>(func.Body, args1, args2);
        }
        public ws_model<string> invoke<T>(Expression<Func<T, apiaction_i>> func, int args1)
        {
            return _invoke<T>(func.Body, args1);
        }
        public ws_model<string> invoke<T>(Expression<Func<T, apiaction_ii>> func, int args1, int args2)
        {
            return _invoke<T>(func.Body, args1, args2);
        }
        public ws_model<string> invoke<T>(Expression<Func<T, apiaction_il>> func, int args1, long args2)
        {
            return _invoke<T>(func.Body, args1, args2);
        }
        public ws_model<string> invoke<T>(Expression<Func<T, apiaction_is>> func, int args1, string args2)
        {
            return _invoke<T>(func.Body, args1, args2);
        }
        public ws_model<string> invoke<T>(Expression<Func<T, apiaction_ss>> func, string args1, string args2)
        {
            return _invoke<T>(func.Body, args1, args2);
        }
        public ws_model<string> invoke<T>(Expression<Func<T, apiaction_si>> func, string args1, int args2)
        {
            return _invoke<T>(func.Body, args1, args2);
        }
        public ws_model<string> invoke<T>(Expression<Func<T, apiaction_sl>> func, string args1, long args2)
        {
            return _invoke<T>(func.Body, args1, args2);
        }
        public ws_model<string> invoke<T>(Expression<Func<T, apiaction_sss>> func, string args1, string args2, string args3)
        {
            return _invoke<T>(func.Body, args1, args2, args3);
        }
        public ws_model<string> invoke<T>(Expression<Func<T, apiaction_ssl>> func, string args1, string args2, long args3)
        {
            return _invoke<T>(func.Body, args1, args2, args3);
        }
        public ws_model<string> invoke<T>(Expression<Func<T, apiaction_sll>> func, string args1, long args2, long args3)
        {
            return _invoke<T>(func.Body, args1, args2, args3);
        }
        public ws_model<string> invoke<T>(Expression<Func<T, apiaction_ssi>> func, string args1, string args2, int args3)
        {
            return _invoke<T>(func.Body, args1, args2, args3);
        }
        public ws_model<string> invoke<T>(Expression<Func<T, apiaction_sii>> func, string args1, int args2, int args3)
        {
            return _invoke<T>(func.Body, args1, args2, args3);
        }
        #endregion
        

        protected ws_model<string> _invoke<T>(Expression exp, params object[] args)
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
                        return new invoker(build_server(attr.service)).Invoke(code, sb.ToString());
                    }
                }
            }
            var model = new ws_model<string>();
            model.ERROR(-1, "未能找到合适的api定义");
            return model;
        }
        protected ws_model<string, Tresponse> _invoke<T, Tresponse>(Expression exp, params object[] args)
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
                        return new invoker(build_server(attr.service)).Invoke<Tresponse>(code, sb.ToString());
                    }
                }
            }
            var model = new ws_model<string, Tresponse>();
            model.ERROR(-1, "未能找到合适的api定义");
            return model;
        }
        protected ws_model<Trequest, Tresponse> _invoke_data<T, Trequest, Tresponse>(Expression exp, ws_model<Trequest, Tresponse> model)
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
                        if (model == null || model.request == null || model.request is string)
                        {
                            model = new ws_model<Trequest, Tresponse>();
                            var ret = new invoker(build_server(attr.service)).Invoke<Tresponse>(code, string.Empty);
                            model.response = ret.response; ;
                            model.user = ret.user;
                            model.secret = ret.secret;
                            model.client = ret.client;
                            model.time = ret.time;
                            model.signature = ret.signature;
                            model.errcode = ret.errcode;
                            model.errmsg = ret.errmsg;
                            return model;
                        }
                        return new invoker(build_server(attr.service)).Invoke<Trequest, Tresponse>(code, model);
                    }
                }
            }
            if (model == null) model = new ws_model<Trequest, Tresponse>();
            model.ERROR(-1, "未能找到合适的api定义");
            return model;
        }
        protected ws_model<Trequest> _invoke_data<T, Trequest>(Expression exp, ws_model<Trequest> model)
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
                        return new invoker(build_server(attr.service)).Invoke<Trequest>(code, model);
                    }
                }
            }
            if (model == null) model = new ws_model<Trequest>();
            model.ERROR(-1, "未能找到合适的api定义");
            return model;
        }

    }


}
