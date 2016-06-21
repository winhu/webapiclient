using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickWebApi
{
    public enum MethodType
    {
        NONE = 0,
        HTTPGET = 1,
        HTTPPOST = 2,
        HTTPPUT = 4,
        HTTPDEL = 8
    }
    [AttributeUsage(AttributeTargets.Assembly)]
    public class QuickWebApiDllAttribute : Attribute
    {
        public QuickWebApiDllAttribute(string name, string domain) { _name = name; _domain = domain; }
        string _name = "default", _domain = "http://localhost";

        public string Name { get { return _name; } }
        public string Domain { get { return _domain; } }
    }

    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method)]
    public class QuickWebApiAttribute : Attribute
    {
        public QuickWebApiAttribute(string service, string route, string name = null, string comment = null)
            : this(service, route, MethodType.NONE, name, comment, null)
        { }
        public QuickWebApiAttribute(MethodType methodtype, string name = null, string comment = null, Type result_type = null)
            : this(null, null, methodtype, name, comment, result_type)
        { }
        public QuickWebApiAttribute(string service, string route, MethodType methodtype = MethodType.NONE, string name = null, string comment = null, Type result_type = null)
        {
            _service = service;
            _name = name;
            _comment = comment;
            _methodtype = methodtype;
            _route = route;
            _result_type = result_type;
        }
        string _service, _name, _comment, _route;
        MethodType _methodtype;
        Type _result_type;
        public Type resultype { get { return _result_type; } }
        public string service { get { return _service; } }
        public MethodType methodtype { get { return _methodtype; } }
        public string name { get { return _name; } }
        public string comment { get { return _comment; } }
        public string route { get { return _route; } }
    }
}
