using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
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
            Actions = new List<WebApiMethod>();
            Uri = domain;
        }

        public string Name { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
        public string Service { get; set; }
        public string Route { get; set; }
        public string Uri { get; set; }
        public string Comment { get; set; }
        public List<WebApiMethod> Actions { get; set; }

        public string Url(string action)
        {
            var act = Actions.SingleOrDefault(a => a.Action == action);
            if (act == null) return string.Empty;
            return string.Format("{0}/{1}", Route, act.Action);
        }
        public WebApiMethod Method(string method)
        {
            var mtd = Actions.SingleOrDefault(a => a.Action == method);
            return mtd;
        }
    }
    public class WebApiMethod
    {
        public string Code { get; set; }
        public MethodType Method { get; set; }
        public string Action { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
    }
    public class webapifactory
    {
        static webapifactory _instance = new webapifactory();
        public static webapifactory Instance { get { return _instance; } }

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
            return config.Actions.SingleOrDefault(a => a.Action == code);
        }
        List<WebApiNode> apis = new List<WebApiNode>();

        public WebApiNode[] AllApi { get { return apis.ToArray(); } }

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
        //public void Register_AuthenticationHandler(HttpConfiguration config)
        //{
        //    config.MessageHandlers.Add(new AuthenticationHandler());
        //}


        public string Load_Apis()
        {
            var files = System.IO.Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "apis_*.xml", System.IO.SearchOption.AllDirectories);
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
            return string.Format("service:{0}, action:{1}", apis.Count, apis.Sum(a => a.Actions.Count));
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
        T Deserialize<T>(string data) where T : class,new()
        {
            using (StringReader stream = new StringReader(data))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return serializer.Deserialize(stream) as T;
            }
        }

        public void Build_Apis()
        {
            foreach (var ass in AppDomain.CurrentDomain.GetAssemblies().Where(a => a.GetCustomAttributes(typeof(QuickWebApiDllAttribute), true).Length > 0))
            {
                var vatt = ass.GetCustomAttribute<AssemblyFileVersionAttribute>();
                var tatt = ass.GetCustomAttribute<AssemblyTitleAttribute>();
                var datt = ass.GetCustomAttribute<QuickWebApiDllAttribute>();
                apis.Clear();
                foreach (var type in ass.GetTypes())
                {
                    var attr = type.GetCustomAttribute<QuickWebApiAttribute>();
                    if (attr != null)
                    {
                        WebApiNode api = new WebApiNode(datt.Domain) { Name = attr.name, Service = attr.service, Route = attr.route, Comment = attr.comment, Version = vatt.Version, Title = tatt.Title };
                        foreach (var mi in type.GetMethods())
                        {
                            var att = mi.GetCustomAttribute<QuickWebApiAttribute>();
                            if (att != null)
                            {
                                var act = new WebApiMethod() { Action = mi.Name, Code = att.service, Method = att.methodtype, Name = att.name, Comment = att.comment };
                                if (!api.Actions.Exists(a => a.Action == act.Action))
                                    api.Actions.Add(act);
                            }
                        }
                        if (!apis.Exists(a => a.Service == api.Service))
                            apis.Add(api);
                    }
                }
                Build_Apids_Json(apis, datt.Name);
                Build_Apids_Doc(apis, datt.Name);
            }
        }

        void Build_Apids_Json(List<WebApiNode> apis, string name)
        {
            if (apis == null || apis.Count == 0) return;
            //var jss = new JavaScriptSerializer().Serialize(apis);
            var jss = Serializer<List<WebApiNode>>(apis);// JsonConvert.SerializeObject(apis);
            if (!System.IO.File.Exists(string.Format("{0}/apis_{1}.xml", Check_Dir(), name)))
                System.IO.File.WriteAllText(string.Format("{0}/apis_{1}.xml", Check_Dir(), name), jss);
        }
        string Check_Dir()
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory + "/apidoc";
            if (!System.IO.Directory.Exists(dir))
                System.IO.Directory.CreateDirectory(dir);
            return dir;
        }
        void Build_Apids_Doc(List<WebApiNode> apis, string name)
        {
            if (apis == null || apis.Count == 0) return;

            StringBuilder sb = new StringBuilder();
            int counter = 0;
            int counter2 = 1;
            foreach (var api in apis)
            {
                counter = 1;
                sb.AppendLine(string.Format("{0},{1}:{2}-{3}-{4}", counter2++.ToString("d3"), api.Name, api.Title, api.Version, api.Comment));
                foreach (var act in api.Actions)
                {
                    sb.AppendLine(string.Format("{0},{1}/{2}/{3},{4},{5}", counter++.ToString("d3"), api.Uri, api.Route, act.Action, act.Name, act.Comment));
                }
                sb.AppendLine();
            }

            if (!System.IO.File.Exists(string.Format("{0}/apis_{1}.txt", Check_Dir(), name)))
                System.IO.File.WriteAllText(string.Format("{0}/apis_{1}.txt", Check_Dir(), name), sb.ToString());
        }
    }
    public class apiconfig_node
    {
        public apiconfig_node() { }
        public apiconfig_node(string code, string uri, MethodType mothod)
        {
            Code = code; Uri = uri; Method = mothod;
        }
        //public string Server
        public string Code { get; set; }
        public string Uri { get; set; }
        public MethodType Method { get; set; }
    }
}
