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
                else
                {
                    if (model == null) model = new WsModel<Trequest, Tresponse>();
                    model.ERROR(-9999990, string.Format("{0}/{1}未配置{2}请求", _uri.AbsoluteUri, requestUri, mtd.ToString()));
                    return model;
                }
                return HttpResponseMessage2WSModel<Trequest, Tresponse>(ret);
            }
        }

        public WsModel<Trequest> Invoke<Trequest>(string requestUri, WsModel<Trequest> model, MethodType mtd)
        {

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
                else
                {
                    if (model == null)
                        model = new WsModel<Trequest>();
                    model.ERROR(-9999990, string.Format("{0}/{1}未配置{2}请求", _uri.AbsoluteUri, requestUri, mtd.ToString()));
                    return model;
                }
                return HttpResponseMessage2WSModel<Trequest>(ret);
            }
        }

        public WsModel Invoke(string requestUri, WsModel model, MethodType mtd)
        {
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
                else
                {
                    if (model == null)
                        model = new WsModel();
                    model.ERROR(-9999990, string.Format("{0}/{1}未配置{2}请求", _uri.AbsoluteUri, requestUri, mtd.ToString()));
                    return model;
                }

                return HttpResponseMessage2WSModel(ret);
            }
        }

        public WsModel<string, Tresponse> Invoke<Tresponse>(string requestUri, string data, MethodType mtd)
        {
            WsModel<string, Tresponse> model = null;
            using (var client = new HttpClient())
            {
                Append_Header(client);
                HttpResponseMessage ret = null;
                client.BaseAddress = _uri;
                BuildHeader(client.DefaultRequestHeaders, client.BaseAddress.Authority, requestUri, "POST");
                if (mtd == MethodType.HTTPGET)
                    ret = client.GetAsync(string.Format("{0}?{1}", requestUri, data)).Result;
                else if (mtd == MethodType.HTTPPOST)
                    ret = client.PostAsync(string.Format("{0}?{1}", requestUri, data), new StringContent(string.Empty)).Result;
                else if (mtd == MethodType.HTTPDEL)
                    ret = client.DeleteAsync(string.Format("{0}?{1}", requestUri, data)).Result;
                else
                {
                    model = new WsModel<string, Tresponse>();
                    model.ERROR(-9999990, string.Format("{0}/{1}未配置{2}请求", _uri.AbsoluteUri, requestUri, mtd.ToString()));
                    return model;
                }
                model = HttpResponseMessage2WSModel<string, Tresponse>(ret);
                model.Request = data;// string.Format("{0}:{1}/{2}", mtd.ToString(), _uri.AbsoluteUri, requestUri);
                return model;
            }
        }

        public WsModel<string> Invoke(string requestUri, string data, MethodType mtd)
        {
            WsModel<string> model = null;
            using (var client = new HttpClient())
            {
                Append_Header(client);
                HttpResponseMessage ret = null;
                client.BaseAddress = _uri;
                BuildHeader(client.DefaultRequestHeaders, client.BaseAddress.Authority, requestUri, "POST");
                if (mtd == MethodType.HTTPGET)
                    ret = client.GetAsync(string.Format("{0}?{1}", requestUri, data)).Result;
                else if (mtd == MethodType.HTTPPOST)
                    ret = client.PostAsync(string.Format("{0}?{1}", requestUri, data), new StringContent(string.Empty)).Result;
                else if (mtd == MethodType.HTTPDEL)
                    ret = client.DeleteAsync(string.Format("{0}?{1}", requestUri, data)).Result;
                else
                {
                    if (model == null) model = new WsModel<string>();
                    model.ERROR(-9999995, string.Format("{0}/{1}未配置{2}请求", _uri.AbsoluteUri, requestUri, mtd.ToString()));
                    return model;
                }
                model = HttpResponseMessage2WSModel<string>(ret);
                model.Request = data;// string.Format("{0}:{1}/{2}", mtd.ToString(), _uri.AbsoluteUri, requestUri);
                return model;
            }
        }

        public WsModel<Trequest, Tresponse> HttpResponseMessage2WSModel<Trequest, Tresponse>(HttpResponseMessage response)
        {
            if (response == null)
            {
                var model = new WsModel<Trequest, Tresponse>();
                model.ERROR(-9999990, "Null HttpResponseMessage");
                return model;
            }
            if (!response.IsSuccessStatusCode)
            {
                var model = new WsModel<Trequest, Tresponse>();
                model.ERROR((int)response.StatusCode, response.ReasonPhrase);
                return model;
            }
            return response.Content.ReadAsAsync<WsModel<Trequest, Tresponse>>().Result;
        }

        public WsModel<Trequest> HttpResponseMessage2WSModel<Trequest>(HttpResponseMessage response)
        {
            if (response == null)
            {
                WsModel<Trequest> model = new WsModel<Trequest>();
                model.ERROR(-9999990, "Null HttpResponseMessage");
                return model;
            }
            if (!response.IsSuccessStatusCode)
            {
                WsModel<Trequest> model = new WsModel<Trequest>();
                model.ERROR((int)response.StatusCode, response.ReasonPhrase);
                return model;
            }
            return response.Content.ReadAsAsync<WsModel<Trequest>>().Result;
        }

        public WsModel HttpResponseMessage2WSModel(HttpResponseMessage response)
        {
            if (response == null)
            {
                WsModel model = new WsModel();
                model.ERROR(-9999990, "Null HttpResponseMessage");
                return model;
            }
            if (!response.IsSuccessStatusCode)
            {
                WsModel model = new WsModel();
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
