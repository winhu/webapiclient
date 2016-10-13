using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace QuickWebApi
{

    public class WebApiClient : IDisposable
    {
        Uri _uri;

        KeyValuePair<string, string>[] _authentication;

        public WebApiClient(string baseAddress, KeyValuePair<string, string>[] authentication = null)
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

        public WsModel<Trequest, Tresponse> Invoke<Trequest, Tresponse>(string requestUri, WsModel<Trequest, Tresponse> model, MethodType mtd)
        {
            if (model == null)
            {
                model = new WsModel<Trequest, Tresponse>();
                //model.ERROR("参数为空");
                //return model;
            }

            if (_uri == null)
            {
                model.ERROR(90000, "未指定服务地址");
                return model;
            }
            if (string.IsNullOrWhiteSpace(requestUri))
            {
                model.ERROR(90001, "未指定接口地址");
                return model;
            }
            using (var client = new HttpClient())
            {
                Append_Header(client);
                HttpResponseMessage ret = null;
                client.BaseAddress = _uri;
                BuildHeader(client.DefaultRequestHeaders, client.BaseAddress.Authority, requestUri, "POST");
                if (mtd == MethodType.HTTPPOST)
                    ret = client.PostAsJsonAsync(requestUri, model).Result;
                else if (mtd == MethodType.HTTPPUT)
                    ret = client.PutAsJsonAsync(requestUri, model).Result;

                return HttpResponseMessage2WSModel(ret, model);
            }
        }

        public WsModel<Trequest> Invoke<Trequest>(string requestUri, WsModel<Trequest> model, MethodType mtd)
        {
            if (model == null)
            {
                model = new WsModel<Trequest>();
                //model.ERROR("参数为空");
                //return model;
            }
            if (_uri == null)
            {
                model.ERROR(90000, "未指定服务地址");
                return model;
            }
            if (string.IsNullOrWhiteSpace(requestUri))
            {
                model.ERROR(90001, "未指定接口地址");
                return model;
            }
            using (var client = new HttpClient())
            {
                Append_Header(client);
                HttpResponseMessage ret = null;
                client.BaseAddress = _uri;
                BuildHeader(client.DefaultRequestHeaders, client.BaseAddress.Authority, requestUri, "POST");
                if (mtd == MethodType.HTTPPOST)
                    ret = client.PostAsJsonAsync(requestUri, model).Result;
                else if (mtd == MethodType.HTTPPUT)
                    ret = client.PostAsJsonAsync(requestUri, model).Result;
                //else if (mtd == MethodType.HTTPDEL)
                //    ret = client.PostAsJsonAsync(requestUri, model).Result;

                return HttpResponseMessage2WSModel(ret, model);

                //if (!ret.IsSuccessStatusCode)
                //    _result = new result((int)ret.StatusCode, ret.ReasonPhrase);
                //else _result = ret.Content.ReadAsAsync<ws_model<Trequest, Tresponse>>().Result;
                //return _result;
            }
        }

        public WsModel Invoke(string requestUri, WsModel model, MethodType mtd)
        {
            if (model == null)
            {
                model = new WsModel();
                //model.ERROR("参数为空");
                //return model;
            }
            if (_uri == null)
            {
                model.ERROR(90000, "未指定服务地址");
                return model;
            }
            if (string.IsNullOrWhiteSpace(requestUri))
            {
                model.ERROR(90001, "未指定接口地址");
                return model;
            }
            using (var client = new HttpClient())
            {
                Append_Header(client);
                HttpResponseMessage ret = null;
                client.BaseAddress = _uri;
                BuildHeader(client.DefaultRequestHeaders, client.BaseAddress.Authority, requestUri, "POST");
                if (mtd == MethodType.HTTPPOST)
                    ret = client.PostAsJsonAsync(requestUri, model).Result;
                else if (mtd == MethodType.HTTPPUT)
                    ret = client.PostAsJsonAsync(requestUri, model).Result;
                //else if (mtd == MethodType.HTTPDEL)
                //    ret = client.PostAsJsonAsync(requestUri, model).Result;

                return HttpResponseMessage2WSModel(ret, model);

                //if (!ret.IsSuccessStatusCode)
                //    _result = new result((int)ret.StatusCode, ret.ReasonPhrase);
                //else _result = ret.Content.ReadAsAsync<ws_model<Trequest, Tresponse>>().Result;
                //return _result;
            }
        }

        public WsModel<string, Tresponse> Invoke<Tresponse>(string requestUri, string data, MethodType mtd)
        {
            WsModel<string, Tresponse> model = new WsModel<string, Tresponse>();
            if (_uri == null)
            {
                model.ERROR(90000, "未指定服务地址");
                return model;
            }
            if (string.IsNullOrWhiteSpace(requestUri))
            {
                model.ERROR(90001, "未指定接口地址");
                return model;
            }
            using (var client = new HttpClient())
            {
                Append_Header(client);
                HttpResponseMessage ret = null;
                client.BaseAddress = _uri;
                BuildHeader(client.DefaultRequestHeaders, client.BaseAddress.Authority, requestUri, "POST");
                if (mtd == MethodType.HTTPGET)
                    ret = client.GetAsync(string.Format("{0}?{1}", requestUri, data)).Result;

                return HttpResponseMessage2WSModel(ret, model);
            }
        }

        public WsModel<string> Invoke(string requestUri, string data, MethodType mtd)
        {
            WsModel<string> model = new WsModel<string>();
            if (_uri == null)
            {
                model.ERROR(90000, "未指定服务地址");
                return model;
            }
            if (string.IsNullOrWhiteSpace(requestUri))
            {
                model.ERROR(90001, "未指定接口地址");
                return model;
            }
            using (var client = new HttpClient())
            {
                Append_Header(client);
                HttpResponseMessage ret = null;
                client.BaseAddress = _uri;
                BuildHeader(client.DefaultRequestHeaders, client.BaseAddress.Authority, requestUri, "POST");
                if (mtd == MethodType.HTTPGET)
                    ret = client.GetAsync(string.Format("{0}?{1}", requestUri, data)).Result;

                return HttpResponseMessage2WSModel(ret, model);
            }
        }


        public WsModel<Trequest, Tresponse> HttpResponseMessage2WSModel<Trequest, Tresponse>(HttpResponseMessage response, WsModel<Trequest, Tresponse> model)
        {
            if (response == null)
            {
                model.ERROR("null HttpResponseMessage");
                return model;
            }
            if (!response.IsSuccessStatusCode)
            {
                model.ERROR((int)response.StatusCode, response.ReasonPhrase);
                return model;
            }
            return response.Content.ReadAsAsync<WsModel<Trequest, Tresponse>>().Result;
        }

        public WsModel<Trequest> HttpResponseMessage2WSModel<Trequest>(HttpResponseMessage response, WsModel<Trequest> model)
        {
            if (response == null)
            {
                model.ERROR("null HttpResponseMessage");
                return model;
            }
            if (!response.IsSuccessStatusCode)
            {
                model.ERROR((int)response.StatusCode, response.ReasonPhrase);
                return model;
            }
            return response.Content.ReadAsAsync<WsModel<Trequest>>().Result;
        }

        public WsModel HttpResponseMessage2WSModel(HttpResponseMessage response, WsModel model)
        {
            if (response == null)
            {
                model.ERROR("null HttpResponseMessage");
                return model;
            }
            if (!response.IsSuccessStatusCode)
            {
                model.ERROR((int)response.StatusCode, response.ReasonPhrase);
                return model;
            }
            return response.Content.ReadAsAsync<WsModel>().Result;
        }




















        public void Dispose()
        {
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



    }
}
