using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QuickWebApi
{

    public class webapiclient : iwebapiclient
    {
        Uri _uri;
        result _result;
        public result lastest { get { return _result; } }

        public webapiclient(string baseAddress)
        {
            _uri = new Uri(baseAddress);
        }

        public result Post(string requestUri, object req)
        {
            if (_uri == null) return new result(90000, "未指定服务地址");
            if (string.IsNullOrWhiteSpace(requestUri)) return new result(90001, "未指定接口地址");
            using (var client = new HttpClient())
            {
                HttpResponseMessage ret;
                client.BaseAddress = _uri;
                if (req is string)
                    ret = client.PostAsync(string.Format("{0}?{1}", requestUri, req), new StringContent(string.Empty)).Result;
                else ret = client.PostAsJsonAsync(requestUri, req).Result;

                if (!ret.IsSuccessStatusCode)
                    _result = new result((int)ret.StatusCode, ret.ReasonPhrase);
                else _result = ret.Content.ReadAsAsync<result>().Result;
                return _result;
            }
        }

        public result<tresp> Post<tresp>(string requestUri, object data) where tresp : class, new()
        {
            result<tresp> __result;
            if (_uri == null) return new result<tresp>(90000, "未指定服务地址");
            if (string.IsNullOrWhiteSpace(requestUri)) return new result<tresp>(90001, "未指定接口地址");
            using (var client = new HttpClient())
            {
                client.BaseAddress = _uri;
                HttpResponseMessage ret;
                if (data is string)
                    ret = client.PostAsync(string.Format("{0}?{1}", requestUri, data), new StringContent(string.Empty)).Result;
                else ret = client.PostAsJsonAsync(requestUri, data).Result;
                if (!ret.IsSuccessStatusCode)
                    __result = new result<tresp>((int)ret.StatusCode, ret.ReasonPhrase);
                else __result = ret.Content.ReadAsAsync<result<tresp>>().Result;
                return __result;
            }
        }

        public void Dispose()
        {
        }

        public result Get(string requestUri, object req)
        {
            if (_uri == null) return new result(90000, "未指定服务地址");
            if (string.IsNullOrWhiteSpace(requestUri)) return new result(90001, "未指定接口地址");
            using (var client = new HttpClient())
            {
                client.BaseAddress = _uri;
                HttpResponseMessage ret = client.GetAsync(string.Format("{0}?{1}", requestUri, req)).Result;
                if (!ret.IsSuccessStatusCode)
                    _result = new result((int)ret.StatusCode, ret.ReasonPhrase);
                else _result = ret.Content.ReadAsAsync<result>().Result;
                return _result;
            }
        }
        public result<tresp> Get<tresp>(string requestUri, object req) where tresp : class ,new()
        {
            result<tresp> __result;
            if (_uri == null) return new result<tresp>(90000, "未指定服务地址");
            if (string.IsNullOrWhiteSpace(requestUri)) return new result<tresp>(90001, "未指定接口地址");
            using (var client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Add("username", "hyf");
                client.BaseAddress = _uri;
                HttpResponseMessage ret = client.GetAsync(string.Format("{0}?{1}", requestUri, req)).Result;
                if (!ret.IsSuccessStatusCode)
                    __result = new result<tresp>((int)ret.StatusCode, ret.ReasonPhrase);
                else __result = ret.Content.ReadAsAsync<result<tresp>>().Result;
                return __result;
            }
        }


        public result Put(string requestUri, object req)
        {
            if (_uri == null) return new result(90000, "未指定服务地址");
            if (string.IsNullOrWhiteSpace(requestUri)) return new result(90001, "未指定接口地址");
            using (var client = new HttpClient())
            {
                client.BaseAddress = _uri;
                HttpResponseMessage ret = client.PutAsJsonAsync(requestUri, req).Result;
                if (!ret.IsSuccessStatusCode)
                    _result = new result((int)ret.StatusCode, ret.ReasonPhrase);
                else _result = ret.Content.ReadAsAsync<result>().Result;
                return _result;
            }
        }

        public result Delete(string requestUri, object req)
        {
            if (_uri == null) return new result(90000, "未指定服务地址");
            if (string.IsNullOrWhiteSpace(requestUri)) return new result(90001, "未指定接口地址");
            using (var client = new HttpClient())
            {
                client.BaseAddress = _uri;
                HttpResponseMessage ret = client.DeleteAsync(string.Format("{0}?{1}", requestUri, req)).Result;
                if (!ret.IsSuccessStatusCode)
                    _result = new result((int)ret.StatusCode, ret.ReasonPhrase);
                else _result = ret.Content.ReadAsAsync<result>().Result;
                return _result;
            }
        }


        public result<tresp> Put<tresp>(string requestUri, object req) where tresp : class, new()
        {
            if (_uri == null) return new result<tresp>(90000, "未指定服务地址");
            if (string.IsNullOrWhiteSpace(requestUri)) return new result<tresp>(90001, "未指定接口地址");
            using (var client = new HttpClient())
            {
                result<tresp> __result;
                client.BaseAddress = _uri;
                HttpResponseMessage ret = client.PutAsJsonAsync(requestUri, req).Result;
                if (!ret.IsSuccessStatusCode)
                    __result = new result<tresp>((int)ret.StatusCode, ret.ReasonPhrase);
                else __result = ret.Content.ReadAsAsync<result<tresp>>().Result;
                return __result;
            }
        }

        public result<tresp> Delete<tresp>(string requestUri, object req) where tresp : class, new()
        {
            if (_uri == null) return new result<tresp>(90000, "未指定服务地址");
            if (string.IsNullOrWhiteSpace(requestUri)) return new result<tresp>(90001, "未指定接口地址");
            using (var client = new HttpClient())
            {
                result<tresp> __result;
                client.BaseAddress = _uri;
                HttpResponseMessage ret = client.DeleteAsync(string.Format("{0}?{1}", requestUri, req)).Result;
                if (!ret.IsSuccessStatusCode)
                    __result = new result<tresp>((int)ret.StatusCode, ret.ReasonPhrase);
                else __result = ret.Content.ReadAsAsync<result<tresp>>().Result;
                return __result;
            }
        }
    }
}
