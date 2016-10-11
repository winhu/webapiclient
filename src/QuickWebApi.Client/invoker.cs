using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickWebApi
{

    internal class invoker : IDisposable
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

        //public result Excute(string requestUri)
        //{
        //    var api = webapifactory.Instance.Get(_service);
        //    if (api == null) return new result(-1, string.Format("未找到{0}->{1}的webapi配置", _service, requestUri));
        //    using (iwebapiclient client = new webapiclient(api.Uri))
        //    {
        //        var mtd = api.Method(requestUri);
        //        switch (mtd.Method)
        //        {
        //            case MethodType.HTTPGET:
        //                return client.Get(api.Url(requestUri));
        //        }
        //    }
        //    return new result(-1, string.Format("未找到{0}->{1}.{2}的webapi配置", _service, requestUri, requestUri));
        //}

        //public result Excute(string requestUri, object req)
        //{
        //    var api = webapifactory.Instance.Get(_service);
        //    if (api == null) return new result(-1, string.Format("未找到{0}->{1}的webapi配置", _service, requestUri));
        //    using (iwebapiclient client = new webapiclient(api.Uri))
        //    {
        //        var mtd = api.Method(requestUri);
        //        switch (mtd.Method)
        //        {
        //            case MethodType.HTTPGET:
        //                return client.Get(api.Url(requestUri), req);
        //            case MethodType.HTTPPOST:
        //                return client.Post(api.Url(requestUri), req);
        //            case MethodType.HTTPPUT:
        //                return client.Put(api.Url(requestUri), req);
        //            //case MethodType.HTTPDEL:
        //            //    return client.Delete(api.Url(requestUri), req);
        //        }
        //    }
        //    return new result(-1, string.Format("未找到{0}->{1}.{2}的webapi配置", _service, requestUri, requestUri));
        //}

        //public result<tresp> Excute<tresp>(string requestUri, object req)
        //{
        //    var api = webapifactory.Instance.Get(_service);
        //    if (api == null) return new result<tresp>(-1, string.Format("未找到{0}->{1}的webapi配置", _service, requestUri));
        //    using (iwebapiclient client = new webapiclient(api.Uri))
        //    {
        //        var mtd = api.Method(requestUri);
        //        switch (mtd.Method)
        //        {
        //            case MethodType.HTTPGET:
        //                return client.Get<tresp>(api.Url(requestUri), req);
        //            case MethodType.HTTPPOST:
        //                return client.Post<tresp>(api.Url(requestUri), req);
        //            case MethodType.HTTPPUT:
        //                return client.Put<tresp>(api.Url(requestUri), req);
        //            //case MethodType.HTTPDEL:
        //            //    return client.Delete<tresp>(api.Url(requestUri), req);
        //        }
        //    }
        //    return new result<tresp>(-1, string.Format("未找到{0}->{1}.{2}的webapi配置", _service, requestUri, requestUri));
        //}








        public ws_model<string> Invoke(string requestUri, string data)
        {
            ws_model<string> model = new ws_model<string>();
            model.request = data;
            var api = webapifactory.Instance.Get(_service);
            if (api == null)
            {
                model.ERROR(-1, string.Format("未找到{0}->{1}的webapi配置", _service, requestUri));
                return model;
            }
            if (string.IsNullOrWhiteSpace(api.Uri))
            {
                model.ERROR(90000, "未指定服务地址");
                return model;
            }
            if (string.IsNullOrWhiteSpace(api.Url(requestUri)))
            {
                model.ERROR(90001, "未指定接口地址");
                return model;
            }
            using (webapiclient client = new webapiclient(api.Uri))
            {
                var mtd = api.Method(requestUri);
                return client.Invoke(api.Url(requestUri), data, mtd.Method);
            }
        }
        public ws_model<string, Tresponse> Invoke<Tresponse>(string requestUri, string data)
        {
            ws_model<string, Tresponse> model = new ws_model<string, Tresponse>();
            model.request = data;
            var api = webapifactory.Instance.Get(_service);
            if (api == null)
            {
                model.ERROR(-1, string.Format("未找到{0}->{1}的webapi配置", _service, requestUri));
                return model;
            }
            if (string.IsNullOrWhiteSpace(api.Uri))
            {
                model.ERROR(90000, "未指定服务地址");
                return model;
            }
            if (string.IsNullOrWhiteSpace(api.Url(requestUri)))
            {
                model.ERROR(90001, "未指定接口地址");
                return model;
            }
            using (webapiclient client = new webapiclient(api.Uri))
            {
                var mtd = api.Method(requestUri);
                return client.Invoke<Tresponse>(api.Url(requestUri), data, mtd.Method);
            }
        }



        public ws_model<Trequest, Tresponse> Invoke<Trequest, Tresponse>(string requestUri, ws_model<Trequest, Tresponse> model)
        {
            //if (model == null)
            //{
            //    model = new ws_model<Trequest, Tresponse>();
            //    model.ERROR("参数为空");
            //    return model;
            //}
            var api = webapifactory.Instance.Get(_service);
            if (api == null)
            {
                model.ERROR(-1, string.Format("未找到{0}->{1}的webapi配置", _service, requestUri));
                return model;
            }
            if (string.IsNullOrWhiteSpace(api.Uri))
            {
                model.ERROR(90000, "未指定服务地址");
                return model;
            }
            if (string.IsNullOrWhiteSpace(api.Url(requestUri)))
            {
                model.ERROR(90001, "未指定接口地址");
                return model;
            }
            using (webapiclient client = new webapiclient(api.Uri))
            {
                var mtd = api.Method(requestUri);
                return client.Invoke<Trequest, Tresponse>(api.Url(requestUri), model, mtd.Method);
            }
        }
        public ws_model<Trequest> Invoke<Trequest>(string requestUri, ws_model<Trequest> model)
        {
            //if (model == null)
            //{
            //    model = new ws_model<Trequest>();
            //    model.ERROR("参数为空");
            //    return model;
            //}
            var api = webapifactory.Instance.Get(_service);
            if (api == null)
            {
                model.ERROR(-1, string.Format("未找到{0}->{1}的webapi配置", _service, requestUri));
                return model;
            }
            if (string.IsNullOrWhiteSpace(api.Uri))
            {
                model.ERROR(90000, "未指定服务地址");
                return model;
            }
            if (string.IsNullOrWhiteSpace(api.Url(requestUri)))
            {
                model.ERROR(90001, "未指定接口地址");
                return model;
            }
            using (webapiclient client = new webapiclient(api.Uri))
            {
                var mtd = api.Method(requestUri);
                return client.Invoke<Trequest>(api.Url(requestUri), model, mtd.Method);
            }
        }
        public ws_model Invoke(string requestUri, ws_model model)
        {
            //if (model == null)
            //{
            //    model = new ws_model();
            //    model.ERROR("参数为空");
            //    return model;
            //}
            var api = webapifactory.Instance.Get(_service);
            if (api == null)
            {
                model.ERROR(-1, string.Format("未找到{0}->{1}的webapi配置", _service, requestUri));
                return model;
            }
            if (string.IsNullOrWhiteSpace(api.Uri))
            {
                model.ERROR(90000, "未指定服务地址");
                return model;
            }
            if (string.IsNullOrWhiteSpace(api.Url(requestUri)))
            {
                model.ERROR(90001, "未指定接口地址");
                return model;
            }
            using (webapiclient client = new webapiclient(api.Uri))
            {
                var mtd = api.Method(requestUri);
                return client.Invoke(api.Url(requestUri), model, mtd.Method);
            }
        }

    }
}
