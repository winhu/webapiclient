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
    public class webapi<T, tresp>
    //where tresp : class,new()
    {
        public webapi() { }
        public webapi(string service_prefix, KeyValuePair<string, string>[] authentication = null)
        {
            _service_prefix = service_prefix;
        }

        protected string build_server(string srv)
        {
            return string.IsNullOrWhiteSpace(_service_prefix) ? srv : string.Format("{0}_{1}", _service_prefix, srv);
        }

        string _service_prefix;
        KeyValuePair<string, string>[] _authentication;
        public virtual webapi<T, tresp> WithAuthentication(KeyValuePair<string, string>[] authentication)
        {
            _authentication = authentication;
            return this;
        }

        //public result<tresp> invoke(Expression<Func<T, apiaction_l>> func, long args1)
        //{
        //    return _invoke(func.Body, args1);
        //}
        //public result<tresp> invoke(Expression<Func<T, apiaction_ll>> func, long args1, long args2)
        //{
        //    return _invoke(func.Body, args1, args2);
        //}
        //public result<tresp> invoke(Expression<Func<T, apiaction_li>> func, long args1, int args2)
        //{
        //    return _invoke(func.Body, args1, args2);
        //}
        //public result<tresp> invoke(Expression<Func<T, apiaction_ls>> func, long args1, string args2)
        //{
        //    return _invoke(func.Body, args1, args2);
        //}
        //public result<tresp> invoke(Expression<Func<T, apiaction_i>> func, int args1)
        //{
        //    return _invoke(func.Body, args1);
        //}
        //public result<tresp> invoke(Expression<Func<T, apiaction_ii>> func, int args1, int args2)
        //{
        //    return _invoke(func.Body, args1, args2);
        //}
        //public result<tresp> invoke(Expression<Func<T, apiaction_il>> func, int args1, long args2)
        //{
        //    return _invoke(func.Body, args1, args2);
        //}
        //public result<tresp> invoke(Expression<Func<T, apiaction_is>> func, int args1, string args2)
        //{
        //    return _invoke(func.Body, args1, args2);
        //}
        //public result<tresp> invoke(Expression<Func<T, apiaction_ss>> func, string args1, string args2)
        //{
        //    return _invoke(func.Body, args1, args2);
        //}
        //public result<tresp> invoke(Expression<Func<T, apiaction_sss>> func, string args1, string args2, string args3)
        //{
        //    return _invoke(func.Body, args1, args2, args3);
        //}
        //public result<tresp> invoke(Expression<Func<T, apiaction_ssl>> func, string args1, string args2, long args3)
        //{
        //    return _invoke(func.Body, args1, args2, args3);
        //}
        //public result<tresp> invoke(Expression<Func<T, apiaction_sll>> func, string args1, long args2, long args3)
        //{
        //    return _invoke(func.Body, args1, args2, args3);
        //}
        //public result<tresp> invoke(Expression<Func<T, apiaction_ssi>> func, string args1, string args2, int args3)
        //{
        //    return _invoke(func.Body, args1, args2, args3);
        //}
        //public result<tresp> invoke(Expression<Func<T, apiaction_sii>> func, string args1, int args2, int args3)
        //{
        //    return _invoke(func.Body, args1, args2, args3);
        //}

        public result<tresp> invoke<treq>(Expression<Func<T, apiaction_o<treq>>> func, treq data)
        //where treq : class,new()
        {
            if (data != null && data is String)
            {
                return _invoke(func.Body, data);
            }
            return _invoke_data<treq>(func.Body, data);
        }

        public result<tresp> invoke(Expression<Func<T, apiaction>> func)
        {
            return _invoke_data<object>(func.Body, null);
        }
        protected result<tresp> _invoke_data<treq>(Expression exp, treq data)
        //where treq : class
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
                        return new invoker(build_server(attr.service)).Excute<tresp>(code, data);
                    }
                }
            }
            return new result<tresp>(-1, "未能找到合适的api定义");
        }
        protected result<tresp> _invoke(Expression exp, params object[] args)
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
                        return new invoker(build_server(attr.service)).Excute<tresp>(code, sb.ToString());
                    }
                }
            }
            return new result<tresp>(-1, "未能找到合适的api定义");
        }
    }

    public class webapi<T> //: webapi<T, object>
    {
        public webapi() { }
        public webapi(string service_prefix, KeyValuePair<string, string>[] authentication = null)
        {
            _service_prefix = service_prefix;
        }

        protected string build_server(string srv)
        {
            return string.IsNullOrWhiteSpace(_service_prefix) ? srv : string.Format("{0}_{1}", _service_prefix, srv);
        }

        string _service_prefix;
        KeyValuePair<string, string>[] _authentication;

        public webapi<T> WithAuthentication(KeyValuePair<string, string>[] authentication)
        {
            _authentication = authentication;
            return this;
        }

        //public result invoke(Expression<Func<T, apiaction_l>> func, long args1)
        //{
        //    return _invoke(func.Body, args1);
        //}
        //public result invoke(Expression<Func<T, apiaction_ll>> func, long args1, long args2)
        //{
        //    return _invoke(func.Body, args1, args2);
        //}
        //public result invoke(Expression<Func<T, apiaction_li>> func, long args1, int args2)
        //{
        //    return _invoke(func.Body, args1, args2);
        //}
        //public result invoke(Expression<Func<T, apiaction_ls>> func, long args1, string args2)
        //{
        //    return _invoke(func.Body, args1, args2);
        //}
        //public result invoke(Expression<Func<T, apiaction_i>> func, int args1)
        //{
        //    return _invoke(func.Body, args1);
        //}
        //public result invoke(Expression<Func<T, apiaction_ii>> func, int args1, int args2)
        //{
        //    return _invoke(func.Body, args1, args2);
        //}
        //public result invoke(Expression<Func<T, apiaction_il>> func, int args1, long args2)
        //{
        //    return _invoke(func.Body, args1, args2);
        //}
        //public result invoke(Expression<Func<T, apiaction_is>> func, int args1, string args2)
        //{
        //    return _invoke(func.Body, args1, args2);
        //}
        //public result invoke(Expression<Func<T, apiaction_ss>> func, string args1, string args2)
        //{
        //    return _invoke(func.Body, args1, args2);
        //}
        //public result invoke(Expression<Func<T, apiaction_sss>> func, string args1, string args2, string args3)
        //{
        //    return _invoke(func.Body, args1, args2, args3);
        //}
        //public result invoke(Expression<Func<T, apiaction_ssl>> func, string args1, string args2, long args3)
        //{
        //    return _invoke(func.Body, args1, args2, args3);
        //}
        //public result invoke(Expression<Func<T, apiaction_sll>> func, string args1, long args2, long args3)
        //{
        //    return _invoke(func.Body, args1, args2, args3);
        //}
        //public result invoke(Expression<Func<T, apiaction_ssi>> func, string args1, string args2, int args3)
        //{
        //    return _invoke(func.Body, args1, args2, args3);
        //}
        //public result invoke(Expression<Func<T, apiaction_sii>> func, string args1, int args2, int args3)
        //{
        //    return _invoke(func.Body, args1, args2, args3);
        //}

        public result invoke<treq>(Expression<Func<T, apiaction_o<treq>>> func, treq data)
        //where treq : class,new()
        {
            if (data != null && data is String)
            {
                return _invoke(func.Body, data);
            }
            return _invoke_data<treq>(func.Body, data);
        }

        public result invoke(Expression<Func<T, apiaction>> func)
        {
            return _invoke_data<object>(func.Body, null);
        }
        protected result _invoke_data<treq>(Expression exp, treq data)
        //where treq : class
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
                        return new invoker(build_server(attr.service)).Excute(code, data);
                    }
                }
            }
            return new result(-1, "未能找到合适的api定义");
        }
        protected result _invoke(Expression exp, params object[] args)
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
                        return new invoker(build_server(attr.service)).Excute(code, sb.ToString());
                    }
                }
            }
            return new result(-1, "未能找到合适的api定义");
        }

    }
    public class webapi //: webapi<T, object>
    {
        public webapi() { }
        public webapi(string service_prefix, KeyValuePair<string, string>[] authentication = null)
        {
            _service_prefix = service_prefix;
        }

        protected string build_server(string srv)
        {
            return string.IsNullOrWhiteSpace(_service_prefix) ? srv : string.Format("{0}_{1}", _service_prefix, srv);
        }

        string _service_prefix;
        KeyValuePair<string, string>[] _authentication;

        public webapi WithAuthentication(KeyValuePair<string, string>[] authentication)
        {
            _authentication = authentication;
            return this;
        }


        public result<tresp> invoke<T, tresp>(Expression<Func<T, apiaction_l>> func, long args1)
        {
            return _invoke<T, tresp>(func.Body, args1);
        }
        public result<tresp> invoke<T, tresp>(Expression<Func<T, apiaction_ll>> func, long args1, long args2)
        {
            return _invoke<T, tresp>(func.Body, args1, args2);
        }
        public result<tresp> invoke<T, tresp>(Expression<Func<T, apiaction_li>> func, long args1, int args2)
        {
            return _invoke<T, tresp>(func.Body, args1, args2);
        }
        public result<tresp> invoke<T, tresp>(Expression<Func<T, apiaction_ls>> func, long args1, string args2)
        {
            return _invoke<T, tresp>(func.Body, args1, args2);
        }
        public result<tresp> invoke<T, tresp>(Expression<Func<T, apiaction_i>> func, int args1)
        {
            return _invoke<T, tresp>(func.Body, args1);
        }
        public result<tresp> invoke<T, tresp>(Expression<Func<T, apiaction_ii>> func, int args1, int args2)
        {
            return _invoke<T, tresp>(func.Body, args1, args2);
        }
        public result<tresp> invoke<T, tresp>(Expression<Func<T, apiaction_il>> func, int args1, long args2)
        {
            return _invoke<T, tresp>(func.Body, args1, args2);
        }
        public result<tresp> invoke<T, tresp>(Expression<Func<T, apiaction_is>> func, int args1, string args2)
        {
            return _invoke<T, tresp>(func.Body, args1, args2);
        }
        public result<tresp> invoke<T, tresp>(Expression<Func<T, apiaction_ss>> func, string args1, string args2)
        {
            return _invoke<T, tresp>(func.Body, args1, args2);
        }
        public result<tresp> invoke<T, tresp>(Expression<Func<T, apiaction_si>> func, string args1, int args2)
        {
            return _invoke<T, tresp>(func.Body, args1, args2);
        }
        public result<tresp> invoke<T, tresp>(Expression<Func<T, apiaction_sl>> func, string args1, long args2)
        {
            return _invoke<T, tresp>(func.Body, args1, args2);
        }
        public result<tresp> invoke<T, tresp>(Expression<Func<T, apiaction_sss>> func, string args1, string args2, string args3)
        {
            return _invoke<T, tresp>(func.Body, args1, args2, args3);
        }
        public result<tresp> invoke<T, tresp>(Expression<Func<T, apiaction_ssl>> func, string args1, string args2, long args3)
        {
            return _invoke<T, tresp>(func.Body, args1, args2, args3);
        }
        public result<tresp> invoke<T, tresp>(Expression<Func<T, apiaction_sll>> func, string args1, long args2, long args3)
        {
            return _invoke<T, tresp>(func.Body, args1, args2, args3);
        }
        public result<tresp> invoke<T, tresp>(Expression<Func<T, apiaction_ssi>> func, string args1, string args2, int args3)
        {
            return _invoke<T, tresp>(func.Body, args1, args2, args3);
        }
        public result<tresp> invoke<T, tresp>(Expression<Func<T, apiaction_sii>> func, string args1, int args2, int args3)
        {
            return _invoke<T, tresp>(func.Body, args1, args2, args3);
        }


        public result<tresp> invoke<T, tresp>(Expression<Func<T, apiaction>> func)
        //where treq : class,new()
        {
            return _invoke<T, tresp>(func.Body);
        }
        protected result<tresp> _invoke<T, tresp>(Expression exp, params object[] args)
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
                        return new invoker(build_server(attr.service)).Excute<tresp>(code, sb.ToString());
                    }
                }
            }
            return new result<tresp>(-1, "未能找到合适的api定义");
        }

        protected result _invoke<T>(Expression exp, params object[] args)
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
                        return new invoker(build_server(attr.service)).Excute(code, sb.ToString());
                    }
                }
            }
            return new result(-1, "未能找到合适的api定义");
        }

    }
}
