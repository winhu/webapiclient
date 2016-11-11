using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Xml;
using System.Xml.Serialization;

namespace QuickWebApi
{

    public class WebApiNode
    {
        public WebApiNode()
            : this("http://localhost")
        { }
        public WebApiNode(string domain)
        {
            Methods = new List<WebApiMethod>();
            Uri = domain;
        }

        public string Name { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
        public string Service { get; set; }
        public string Route { get; set; }
        public string Uri { get; set; }
        public string Comment { get; set; }
        public List<WebApiMethod> Methods { get; set; }

        public string Url(string action)
        {
            var act = Methods.SingleOrDefault(a => a.Action == action);
            if (act == null) return string.Empty;
            return string.Format("{0}/{1}", Route, act.Action);
        }
        public WebApiMethod Method(string method)
        {
            var mtd = Methods.SingleOrDefault(a => a.Action == method);
            return mtd;
        }
    }
    public class WebApiMethod
    {
        public WebApiMethod() { Params = new List<WebApiMethodParam>(); }

        public string Code { get; set; }
        public MethodType Method { get; set; }
        public string Action { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }

        [NonSerialized]
        internal List<WebApiMethodParam> Params;

        [NonSerialized]
        internal Type OutputType;

        public string ParamsDesc()
        {
            if (Params.Count == 0) return "无参数";
            StringBuilder sb = new StringBuilder();
            foreach (var p in Params)
            {
                sb.AppendFormat("{0}:{1},{2},{3};", p.Name, p.TypeName, p.DefaultValue, p.Desc);
            }
            return sb.ToString();
        }
    }
    public class WebApiMethodParam
    {
        public string Desc { get; set; }
        public string TypeName { get; set; }
        public string Name { get; set; }
        public string DefaultValue { get; set; }

        public override string ToString()
        {
            return string.Format("{0}:{1},{2},{3};", Name, TypeName, DefaultValue, Desc);

        }
    }
    public class QuickWebApiFactory
    {
        static QuickWebApiFactory _instance = new QuickWebApiFactory();
        public static QuickWebApiFactory Instance { get { return _instance; } }

        internal WebApiNode Get(string service)
        {
            var config = apis.SingleOrDefault(c => c.Service == service);
            if (config == null) return null;
            return config;
        }
        internal WebApiMethod GetAction(string service, string code)
        {
            var config = apis.SingleOrDefault(c => c.Service == service);
            if (config == null) return null;
            return config.Methods.SingleOrDefault(a => a.Action == code);
        }
        List<WebApiNode> apis = new List<WebApiNode>();

        public WebApiNode[] AllApi { get { return apis.ToArray(); } }

        static Func<HttpContextBase, User> _func_;
        public void Set_User_DependencyReslover(Func<HttpContextBase, User> func)
        {
            _func_ = func;
        }

        public void Register_JsonFormatter(HttpConfiguration config)
        {
            // Web API configuration and services
            var json = config.Formatters.JsonFormatter;
            // 解决json序列化时的循环引用问题
            json.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            // 干掉XML序列化器
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
        }
        public void Register_AuthenticationHandler(HttpConfiguration config)
        {
            config.MessageHandlers.Add(new AuthenticationHandler());
        }


        public string Load_Apis()
        {
            var files = System.IO.Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "apidoc"), "apis_*.xml", System.IO.SearchOption.AllDirectories);
            apis.Clear();
            foreach (var path in files)
            {
                var jss = System.IO.File.ReadAllText(path);
                var _apis = Deserialize<List<WebApiNode>>(jss);
                if (_apis == null || _apis.Count == 0) continue;
                foreach (var api in _apis)
                {
                    if (apis.Exists(a => a.Service == api.Service)) continue;
                    apis.Add(api);
                }
            }
            return string.Format("service:{0}, action:{1}", apis.Count, apis.Sum(a => a.Methods.Count));
        }

        string Serializer<T>(T data)
        {
            using (StringWriter stream = new StringWriter())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stream, data);
                return stream.ToString();
            }
        }
        T Deserialize<T>(string data) //where T : class,new()
        {
            using (StringReader stream = new StringReader(data))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stream);
            }
        }

        public void Build_Apis(bool overwrite = true)
        {
            foreach (var ass in AppDomain.CurrentDomain.GetAssemblies().Where(a => a.GetCustomAttributes(typeof(WebApiDllAttribute), true).Length > 0))
            {
                var vatt = ass.GetCustomAttribute<AssemblyFileVersionAttribute>();
                var tatt = ass.GetCustomAttribute<AssemblyTitleAttribute>();
                var datt = ass.GetCustomAttribute<WebApiDllAttribute>();
                apis.Clear();
                var input_types = new List<Type>();
                foreach (var type in ass.GetTypes())
                {
                    var attr = type.GetCustomAttribute<WebApiAttribute>();
                    if (attr != null)
                    {
                        WebApiNode api = new WebApiNode(datt.Domain) { Name = attr.name, Service = attr.service, Route = attr.route, Comment = attr.comment, Version = vatt.Version, Title = tatt.Title };
                        foreach (var mi in type.GetMethods())
                        {
                            var att = mi.GetCustomAttribute<WebApiAttribute>();
                            if (att != null)
                            {
                                var act = new WebApiMethod() { Action = mi.Name, Code = att.service, Method = att.methodtype, Name = string.IsNullOrWhiteSpace(att.name) ? mi.Name : att.name, Comment = att.comment, OutputType = att.resultype };
                                foreach (var arg in mi.GetParameters())
                                {
                                    var mdatt = arg.ParameterType.GetCustomAttribute<DescriptionAttribute>();

                                    act.Params.Add(new WebApiMethodParam() { Name = arg.Name, TypeName = arg.ParameterType.Name, DefaultValue = string.IsNullOrWhiteSpace(arg.DefaultValue.ToString()) ? "无默认值" : arg.DefaultValue.ToString(), Desc = mdatt == null ? "" : mdatt.Description });
                                    if (arg.ParameterType.IsClass && arg.ParameterType != typeof(string))
                                    {
                                        if (!input_types.Exists(t => t.Name == arg.ParameterType.Name))
                                            input_types.Add(arg.ParameterType);
                                    }
                                }
                                if (!api.Methods.Exists(a => a.Action == act.Action))
                                    api.Methods.Add(act);

                                if (att.resultype != null && att.resultype.IsClass && att.resultype != typeof(string))
                                {
                                    if (!input_types.Exists(t => t.Name == att.resultype.Name))
                                        input_types.Add(att.resultype);
                                }
                            }
                        }
                        if (!apis.Exists(a => a.Service == api.Service))
                            apis.Add(api);
                    }
                }
                Build_Apids_Config(apis, datt.Name, overwrite);
                Build_Apids_Doc(apis, datt.Name, input_types);
            }
        }

        void Build_Apids_Config(List<WebApiNode> apis, string name, bool overwrite = true)
        {
            if (apis == null || apis.Count == 0) return;
            //var jss = new JavaScriptSerializer().Serialize(apis);
            var jss = Serializer<List<WebApiNode>>(apis);// JsonConvert.SerializeObject(apis);
            if (!overwrite && System.IO.File.Exists(string.Format("{0}/apis_{1}.xml", Check_Dir(), name)))
                return;
            System.IO.File.WriteAllText(string.Format("{0}/apis_{1}.xml", Check_Dir(), name), jss);
        }
        string Check_Dir()
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory + "/apidoc";
            if (!System.IO.Directory.Exists(dir))
                System.IO.Directory.CreateDirectory(dir);
            return dir;
        }
        void Build_Apids_Doc(List<WebApiNode> apis, string name, List<Type> types = null, bool overwrite = true)
        {
            if (apis == null || apis.Count == 0) return;

            StringBuilder sb = new StringBuilder();
            int counter2 = 1;
            foreach (var api in apis)
            {
                sb.AppendLine(string.Format("{0},{1}", counter2++.ToString("d3"), api.Name));
                sb.AppendLine(string.Format("    类型：{0}", api.Title));
                sb.AppendLine(string.Format("    版本：{0}", api.Version));
                sb.AppendLine(string.Format("    描述：{0}", api.Comment));
                foreach (var act in api.Methods)
                {
                    sb.AppendLine(string.Format("        名称：{0}", act.Name));
                    sb.AppendLine(string.Format("        说明：{0}", act.Comment));
                    sb.AppendLine(string.Format("        地址：{0}/{1}/{2},", api.Uri, api.Route, act.Action));
                    sb.AppendLine(string.Format("        方式：{0}", act.Method.ToString()));
                    if (act.OutputType != null)
                        sb.AppendLine(string.Format("        输出：{0}", act.OutputType.Name));
                    foreach (var p in act.Params)
                    {
                        sb.AppendLine(string.Format("        参数：{0}, {1}, {2}", p.Name, p.TypeName, p.Desc));
                    }
                    sb.AppendLine();
                }
                sb.AppendLine("------------------------------------------------------------------------------------------------");

                sb.AppendLine();
            }
            if (types != null && types.Count > 0)
            {
                foreach (var type in types)
                {
                    if (type.IsArray ||
                        type.IsEnum ||
                        type.IsInterface ||
                        type.IsValueType ||
                        type.Name.StartsWith("List") ||
                        type.IsSubclassOf(typeof(WxSuperModel)))
                        continue;

                    DescriptionAttribute patt = type.GetCustomAttribute<DescriptionAttribute>();
                    sb.AppendFormat("类型：{0}, {1}", type.Name, patt == null ? "" : patt.Description);
                    sb.AppendLine();
                    foreach (var p in type.GetProperties())
                    {
                        patt = p.GetCustomAttribute<DescriptionAttribute>();
                        sb.AppendFormat("    属性：{0}, {1}, {2}", p.Name, p.PropertyType.Name, patt == null ? "" : patt.Description);
                        sb.AppendLine();
                    }
                    sb.AppendLine();
                }
                sb.AppendLine();
                sb.AppendLine("------------------------------------------------------------------------------------------------");

            }
            if (!overwrite && System.IO.File.Exists(string.Format("{0}/apis_{1}.txt", Check_Dir(), name)))
                return;
            System.IO.File.WriteAllText(string.Format("{0}/apis_{1}.txt", Check_Dir(), name), sb.ToString());
        }

    }
}