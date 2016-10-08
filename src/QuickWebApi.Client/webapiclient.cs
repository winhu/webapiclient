using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace QuickWebApi
{

    public class webapiclient : iwebapiclient
    {
        Uri _uri;
        result _result;
        public result lastest { get { return _result; } }

        //public webapiclient(string baseAddress)
        //{
        //    _uri = new Uri(baseAddress);
        //}
        KeyValuePair<string, string>[] _authentication;
        public webapiclient(string baseAddress, KeyValuePair<string, string>[] authentication = null)
        {
            _uri = new Uri(baseAddress);
            _authentication = authentication;
        }

        void Append_Header(HttpClient client)
        {
            if (_authentication != null)
            {
                foreach (var header in _authentication)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
        }
        public result Post(string requestUri, object req)
        {
            if (_uri == null) return new result(90000, "未指定服务地址");
            if (string.IsNullOrWhiteSpace(requestUri)) return new result(90001, "未指定接口地址");
            using (var client = new HttpClient())
            {
                Append_Header(client);
                HttpResponseMessage ret;
                client.BaseAddress = _uri;
                BuildHeader(client.DefaultRequestHeaders, client.BaseAddress.Authority, requestUri, "POST");
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
                Append_Header(client);
                client.BaseAddress = _uri;
                BuildHeader(client.DefaultRequestHeaders, client.BaseAddress.Authority, requestUri, "POST");
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
                Append_Header(client);
                client.BaseAddress = _uri;
                BuildHeader(client.DefaultRequestHeaders, client.BaseAddress.Authority, requestUri, "GET");
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
                client.BaseAddress = _uri;
                BuildHeader(client.DefaultRequestHeaders, client.BaseAddress.Authority, requestUri, "GET");
                HttpResponseMessage ret = client.GetAsync(string.Format("{0}?{1}", requestUri, req)).Result;
                if (!ret.IsSuccessStatusCode)
                    __result = new result<tresp>((int)ret.StatusCode, ret.ReasonPhrase);
                else __result = ret.Content.ReadAsAsync<result<tresp>>().Result;
                return __result;
            }
        }

        static long ClientNonceCounter = 0;
        public void BuildHeader(HttpRequestHeaders header, string realm, string uri, string method)
        {
            string username = "hyf";
            string password = "hyf";
            string nonce = Nonce.Generate();
            string cnonce = Nonce.Generate();

            string ha1 = String.Format("{0}:{1}:{2}", username, realm, password).ToMD5();

            string ha2 = String.Format("{0}:{1}", method, uri).ToMD5();

            string computedResponse = String.Format("{0}:{1}:{2}:{3}:{4}:{5}",
                                ha1, nonce, ClientNonceCounter.ToString("d5"), cnonce, "auth", ha2).ToMD5();
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("realm={0}", username);
            sb.AppendFormat(",username={0}", password);
            sb.AppendFormat(",nonce={0}", nonce);
            sb.AppendFormat(",uri={0}", uri);
            sb.AppendFormat(",nc={0}", ClientNonceCounter++);
            sb.AppendFormat(",cnonce={0}", cnonce);
            sb.AppendFormat(",response={0}", computedResponse);
            sb.AppendFormat(",method=GET", realm);

            //header.Add("username", "hyf");
            //header.Add("realm", realm);
            //header.Add("nonce", nonce);
            //header.Add("uri", uri);
            //header.Add("nc", ClientNonceCounter++.ToString());
            //header.Add("cnonce", cnonce);
            //header.Add("response", computedResponse);
            //header.Add("method", "GET");
            header.Authorization = new AuthenticationHeaderValue("WWW-Authenticate", sb.ToString());
        }


        public result Put(string requestUri, object req)
        {
            if (_uri == null) return new result(90000, "未指定服务地址");
            if (string.IsNullOrWhiteSpace(requestUri)) return new result(90001, "未指定接口地址");
            using (var client = new HttpClient())
            {
                Append_Header(client);
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
                Append_Header(client);
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
                Append_Header(client);
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
                Append_Header(client);
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
