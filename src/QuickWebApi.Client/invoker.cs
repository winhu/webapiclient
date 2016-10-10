using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickWebApi
{

    internal class invoker : iinvoker
    {
        public invoker(string service)
        {
            _service = service;
        }

        private string _service;

        public void Dispose()
        {
        }



        //public result Excute(string requestUri, string data)
        //{
        //    var api = webapifactory.Instance.Get(_service);
        //    if (api == null) return new result(-1, string.Format("未找到{0}->{1}的webapi配置", _service, requestUri));
        //    using (iwebapiclient client = new webapiclient(api.Uri))
        //    {
        //        var mtd = api.Method(requestUri);
        //        //var code = apiconfig_factory.Instance.GetNode(_server, requestUri);
        //        switch (mtd.MT)
        //        {
        //            case MethodType.HTTPGET:
        //                return client.Get(api.Url(requestUri), data);
        //            case MethodType.HTTPPOST:
        //                return client.Post(api.Url(requestUri), data);
        //            case MethodType.HTTPPUT:
        //                return client.Put(api.Url(requestUri), data);
        //            case MethodType.HTTPDEL:
        //                return client.Delete(api.Url(requestUri), data);
        //        }
        //    }
        //    return new result(-1, string.Format("未找到{0}->{1}的webapi配置", _service, requestUri, requestUri));
        //}

        public result Excute(string requestUri)
        {
            var api = webapifactory.Instance.Get(_service);
            if (api == null) return new result(-1, string.Format("未找到{0}->{1}的webapi配置", _service, requestUri));
            using (iwebapiclient client = new webapiclient(api.Uri))
            {
                var mtd = api.Method(requestUri);
                switch (mtd.Method)
                {
                    case MethodType.HTTPGET:
                        return client.Get(api.Url(requestUri));
                }
            }
            return new result(-1, string.Format("未找到{0}->{1}.{2}的webapi配置", _service, requestUri, requestUri));
        }
        public result Excute(string requestUri, object req)
        {
            var api = webapifactory.Instance.Get(_service);
            if (api == null) return new result(-1, string.Format("未找到{0}->{1}的webapi配置", _service, requestUri));
            using (iwebapiclient client = new webapiclient(api.Uri))
            {
                var mtd = api.Method(requestUri);
                switch (mtd.Method)
                {
                    case MethodType.HTTPGET:
                        return client.Get(api.Url(requestUri), req);
                    case MethodType.HTTPPOST:
                        return client.Post(api.Url(requestUri), req);
                    case MethodType.HTTPPUT:
                        return client.Put(api.Url(requestUri), req);
                    case MethodType.HTTPDEL:
                        return client.Delete(api.Url(requestUri), req);
                }
            }
            return new result(-1, string.Format("未找到{0}->{1}.{2}的webapi配置", _service, requestUri, requestUri));
        }

        public result<tresp> Excute<tresp>(string requestUri, object req)
        //where tresp : class,new()
        {
            var api = webapifactory.Instance.Get(_service);
            if (api == null) return new result<tresp>(-1, string.Format("未找到{0}->{1}的webapi配置", _service, requestUri));
            using (iwebapiclient client = new webapiclient(api.Uri))
            {
                var mtd = api.Method(requestUri);
                switch (mtd.Method)
                {
                    case MethodType.HTTPGET:
                        return client.Get<tresp>(api.Url(requestUri), req);
                    case MethodType.HTTPPOST:
                        return client.Post<tresp>(api.Url(requestUri), req);
                    case MethodType.HTTPPUT:
                        return client.Put<tresp>(api.Url(requestUri), req);
                    case MethodType.HTTPDEL:
                        return client.Delete<tresp>(api.Url(requestUri), req);
                }
            }
            return new result<tresp>(-1, string.Format("未找到{0}->{1}.{2}的webapi配置", _service, requestUri, requestUri));
        }
    }
}
