using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace QuickWebApi
{

    public class webapiclient : IDisposable
    {
        Uri _uri;

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

        public ws_model<Trequest, Tresponse> Invoke<Trequest, Tresponse>(string requestUri, ws_model<Trequest, Tresponse> model, MethodType mtd)
        {
            if (model == null)
            {
                model = new ws_model<Trequest, Tresponse>();
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

        public ws_model<Trequest> Invoke<Trequest>(string requestUri, ws_model<Trequest> model, MethodType mtd)
        {
            if (model == null)
            {
                model = new ws_model<Trequest>();
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

        public ws_model Invoke(string requestUri, ws_model model, MethodType mtd)
        {
            if (model == null)
            {
                model = new ws_model();
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

        public ws_model<string, Tresponse> Invoke<Tresponse>(string requestUri, string data, MethodType mtd)
        {
            ws_model<string, Tresponse> model = new ws_model<string, Tresponse>();
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

        public ws_model<string> Invoke(string requestUri, string data, MethodType mtd)
        {
            ws_model<string> model = new ws_model<string>();
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


        public ws_model<Trequest, Tresponse> HttpResponseMessage2WSModel<Trequest, Tresponse>(HttpResponseMessage response, ws_model<Trequest, Tresponse> model)
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
            return response.Content.ReadAsAsync<ws_model<Trequest, Tresponse>>().Result;
        }

        public ws_model<Trequest> HttpResponseMessage2WSModel<Trequest>(HttpResponseMessage response, ws_model<Trequest> model)
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
            return response.Content.ReadAsAsync<ws_model<Trequest>>().Result;
        }

        public ws_model HttpResponseMessage2WSModel(HttpResponseMessage response, ws_model model)
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
            return response.Content.ReadAsAsync<ws_model>().Result;
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
