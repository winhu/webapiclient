using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace QuickWebApi
{
    public class wssupermodel { }
    public class ws_model<Trequest, Tresponse> : wssupermodel
    {
        public ws_model()
        {
            this.secret = new Secret();
            time = DateTime.Now;
        }
        public ws_model(Trequest request, Client client = null)
        {
            this.request = request;
            this.client = client;
            this.secret = new Secret();
            time = DateTime.Now;
        }
        public ws_model(Tresponse response)
        {
            this.response = response;
            time = DateTime.Now;
        }

        public virtual void ERROR(int errcode, string msg)
        {
            this.errcode = errcode;
            this.errmsg = msg;
        }
        public virtual void ERROR(string msg)
        {
            this.errcode = -9999;
            this.errmsg = msg;
        }
        public virtual void ERROR()
        {
            this.errcode = -9999;
            this.errmsg = "未知错误";
        }

        /// <summary>
        /// indecate successful or fail
        /// </summary>
        public int errcode { get; set; }
        /// <summary>
        /// msg for result
        /// </summary>
        public string errmsg { get; set; }
        /// <summary>
        /// result time
        /// </summary>
        public DateTime time { get; set; }

        /// <summary>
        /// indecate success or fail
        /// </summary>
        /// <returns></returns>
        public bool Ok() { return errcode == 0; }
        /// <summary>
        /// indecate success or fail
        /// </summary>
        /// <param name="needresponse">是否需要认定response</param>
        /// <returns></returns>
        public bool Ok(bool needresponse)
        {
            return needresponse ? (ValidResponse() && errcode == 0) : errcode == 0;
        }
        public bool Err() { return errcode != 0; }


        public Trequest request { get; set; }
        public Tresponse response { get; set; }
        public Client client { get; set; }
        public User user { get; set; }
        public Secret secret { get; set; }
        public string signature
        {
            get
            {
                Sign();
                return _signature;
            }
            set { _signature_set = value; }
        }
        private string _signature_set = null;
        private string _signature = null;
        bool ValidSignature()
        {
            return _signature_set == signature;
        }
        void Sign()
        {
            var sreq = request == null ? "" : JsonConvert.SerializeObject(request);
            var sresp = response == null ? "" : JsonConvert.SerializeObject(response);
            var sc = JsonConvert.SerializeObject(client);
            var su = JsonConvert.SerializeObject(user);
            var ss = JsonConvert.SerializeObject(secret);
            var sf = new string[] { sc, sreq, sresp, su, ss };
            Array.Sort(sf);
            _signature = string.Join("", sf).ToSHA1();
        }

        public bool ValidRequest()
        {
            if (request.GetType().IsValueType) return true;
            return request != null;
        }
        public bool ValidResponse()
        {
            if (response.GetType().IsValueType) return true;
            return response != null;
        }

        public override string ToString()
        {
            return string.Format("errcode={0},errmsg={1},time={2}",
                errcode, errmsg, time.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }

    public class ws_model<Trequest> : ws_model<Trequest, object>
    {
        public ws_model()
            : base()
        { }
        public ws_model(Trequest request, Client client = null)
            : base(request, client)
        { }
        public ws_model(object response)
            : base(response)
        { }
    }

    public class ws_model : ws_model<object, object>
    {
        public ws_model()
            : base()
        { }
    }
}
