using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickWebApi
{

    internal class Invoker : IDisposable
    {
        public Invoker(string service)
        {
            _service = service;
        }

        private string _service;

        public void Dispose()
        {
        }

        public WsModel<string> Invoke(string requestUri, string data)
        {
            WsModel<string> model = new WsModel<string>();
            model.Request = data;
            var api = QuickWebApiFactory.Instance.Get(_service);
            if (api == null)
            {
                model.ERROR(-9999998, string.Format("未找到{0}->{1}的webapi配置", _service, requestUri));
                return model;
            }
            if (string.IsNullOrWhiteSpace(api.Uri))
            {
                model.ERROR(-9999997, "未指定服务地址");
                return model;
            }
            if (string.IsNullOrWhiteSpace(api.Url(requestUri)))
            {
                model.ERROR(-9999996, "未指定接口路由");
                return model;
            }
            using (WebApiClient client = new WebApiClient(api.Uri))
            {
                var mtd = api.Method(requestUri);
                return client.Invoke(api.Url(requestUri), data, mtd.Method);
            }
        }
        public WsModel<string, Tresponse> Invoke<Tresponse>(string requestUri, string data)
        {
            WsModel<string, Tresponse> model = new WsModel<string, Tresponse>();
            model.Request = data;
            var api = QuickWebApiFactory.Instance.Get(_service);
            if (api == null)
            {
                model.ERROR(-9999998, string.Format("未找到{0}->{1}的webapi配置", _service, requestUri));
                return model;
            }
            if (string.IsNullOrWhiteSpace(api.Uri))
            {
                model.ERROR(-9999997, "未指定服务地址");
                return model;
            }
            if (string.IsNullOrWhiteSpace(api.Url(requestUri)))
            {
                model.ERROR(-9999996, "未指定接口路由");
                return model;
            }
            using (WebApiClient client = new WebApiClient(api.Uri))
            {
                var mtd = api.Method(requestUri);
                return client.Invoke<Tresponse>(api.Url(requestUri), data, mtd.Method);
            }
        }



        public WsModel<Trequest, Tresponse> Invoke<Trequest, Tresponse>(string requestUri, WsModel<Trequest, Tresponse> model)
        {
            var api = QuickWebApiFactory.Instance.Get(_service);
            if (api == null)
            {
                model.ERROR(-9999998, string.Format("未找到{0}->{1}的webapi配置", _service, requestUri));
                return model;
            }
            if (string.IsNullOrWhiteSpace(api.Uri))
            {
                model.ERROR(-9999997, "未指定服务地址");
                return model;
            }
            if (string.IsNullOrWhiteSpace(api.Url(requestUri)))
            {
                model.ERROR(-9999996, "未指定接口路由");
                return model;
            }
            using (WebApiClient client = new WebApiClient(api.Uri))
            {
                var mtd = api.Method(requestUri);
                return client.Invoke<Trequest, Tresponse>(api.Url(requestUri), model, mtd.Method);
            }
        }
        public WsModel<Trequest> Invoke<Trequest>(string requestUri, WsModel<Trequest> model)
        {
            var api = QuickWebApiFactory.Instance.Get(_service);
            if (api == null)
            {
                model.ERROR(-9999998, string.Format("未找到{0}->{1}的webapi配置", _service, requestUri));
                return model;
            }
            if (string.IsNullOrWhiteSpace(api.Uri))
            {
                model.ERROR(-9999997, "未指定服务地址");
                return model;
            }
            if (string.IsNullOrWhiteSpace(api.Url(requestUri)))
            {
                model.ERROR(-9999996, "未指定接口路由");
                return model;
            }
            using (WebApiClient client = new WebApiClient(api.Uri))
            {
                var mtd = api.Method(requestUri);
                return client.Invoke<Trequest>(api.Url(requestUri), model, mtd.Method);
            }
        }
        public WsModel Invoke(string requestUri, WsModel model)
        {
            var api = QuickWebApiFactory.Instance.Get(_service);
            if (api == null)
            {
                model.ERROR(-9999998, string.Format("未找到{0}->{1}的webapi配置", _service, requestUri));
                return model;
            }
            if (string.IsNullOrWhiteSpace(api.Uri))
            {
                model.ERROR(-9999997, "未指定服务地址");
                return model;
            }
            if (string.IsNullOrWhiteSpace(api.Url(requestUri)))
            {
                model.ERROR(-9999996, "未指定接口路由");
                return model;
            }
            using (WebApiClient client = new WebApiClient(api.Uri))
            {
                var mtd = api.Method(requestUri);
                return client.Invoke(api.Url(requestUri), model, mtd.Method);
            }
        }

    }
}
