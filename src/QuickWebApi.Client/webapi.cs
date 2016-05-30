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

    public class webapi<T, tresp> where tresp : class,new()
    {
        public webapi() { }
        public webapi(string service_prefix)
        {
            _service_prefix = service_prefix;
        }
        public webapi(long service_prefix_id)
        {
            _service_prefix = service_prefix_id.ToString();
        }

        protected string build_server(string srv)
        {
            return string.IsNullOrWhiteSpace(_service_prefix) ? srv : string.Format("{0}_{1}", _service_prefix, srv);
        }

        string _service_prefix;

        public result<tresp> invoke(Expression<Func<T, apiaction_l>> func, long args1)
        {
            return _invoke(func.Body, args1);
        }
        public result<tresp> invoke(Expression<Func<T, apiaction_ll>> func, long args1, long args2)
        {
            return _invoke(func.Body, args1, args2);
        }
        public result<tresp> invoke(Expression<Func<T, apiaction_li>> func, long args1, int args2)
        {
            return _invoke(func.Body, args1, args2);
        }
        public result<tresp> invoke(Expression<Func<T, apiaction_ls>> func, long args1, string args2)
        {
            return _invoke(func.Body, args1, args2);
        }

        //public result<tresp> invoke<treq>(Expression<Func<T, apiaction_s<treq>>> func, treq args1) where treq : struct
        //{
        //    return _invoke(func.Body, args1);
        //}
        public result<tresp> invoke(Expression<Func<T, apiaction_i>> func, int args1)
        {
            return _invoke(func.Body, args1);
        }
        public result<tresp> invoke(Expression<Func<T, apiaction_ii>> func, int args1, int args2)
        {
            return _invoke(func.Body, args1, args2);
        }
        public result<tresp> invoke(Expression<Func<T, apiaction_il>> func, int args1, long args2)
        {
            return _invoke(func.Body, args1, args2);
        }
        public result<tresp> invoke(Expression<Func<T, apiaction_is>> func, int args1, string args2)
        {
            return _invoke(func.Body, args1, args2);
        }
        public result<tresp> invoke(Expression<Func<T, apiaction_ss>> func, string args1, string args2)
        {
            return _invoke(func.Body, args1, args2);
        }
        public result<tresp> invoke(Expression<Func<T, apiaction_sss>> func, string args1, string args2, string args3)
        {
            return _invoke(func.Body, args1, args2, args3);
        }

        public result<tresp> invoke<treq>(Expression<Func<T, apiaction_o<treq>>> func, treq data) where treq : class,new()
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

        result<tresp> _invoke_data<treq>(Expression exp, treq data) where treq : class
        {
            var method = ((exp as UnaryExpression).Operand as MethodCallExpression);
            string code = ((method.Object as ConstantExpression).Value as MethodInfo).Name;

            foreach (var m in method.Arguments)
            {
                if (m.Type == typeof(T))
                {
                    var attr = m.Type.GetCustomAttribute<QuickWebApiAttribute>();
                    if (attr != null)
                    {
                        return new invoker(build_server(attr.service)).Excute<tresp>(code, data);
                    }
                }
            }
            return new result<tresp>(-1, "未能找到合适的api定义");
        }
        result<tresp> _invoke(Expression exp, params object[] args)
        {
            var method = ((exp as UnaryExpression).Operand as MethodCallExpression);
            string code = ((method.Object as ConstantExpression).Value as MethodInfo).Name;

            foreach (var m in method.Arguments)
            {
                if (m.Type == typeof(T))
                {
                    var attr = m.Type.GetCustomAttribute<QuickWebApiAttribute>();
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

    public class webapi<T> : webapi<T, object>
    {
        public webapi() { }
        public webapi(string service_prefix)
            : base(service_prefix)
        { }
        public webapi(long service_prefix_id)
            : base(service_prefix_id)
        { }
    }
}
