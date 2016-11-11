using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace QuickWebApi
{
    public class WxSuperModel
    {
        public string Dump()
        {
            using (StringWriter stream = new StringWriter())
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(stream, this);
                return stream.ToString();
            }
        }
    }
    public class WsModel<Trequest, Tresponse> : WxSuperModel
    {
        public WsModel()
        {
            this.Secret = new Secret();
            Time = DateTime.Now;
        }
        public WsModel(Trequest request, Client client = null)
        {
            this.Request = request;
            this.Client = client;
            this.Secret = new Secret();
            Time = DateTime.Now;
        }
        public WsModel(Tresponse response)
        {
            this.Response = response;
            Time = DateTime.Now;
        }

        public virtual void ERROR(int errcode, string msg)
        {
            this.ErrCode = errcode;
            this.ErrMsg = msg;
        }
        public virtual void ERROR(string msg)
        {
            this.ErrCode = -9999;
            this.ErrMsg = msg;
        }
        public virtual void ERROR()
        {
            this.ErrCode = -9999;
            this.ErrMsg = "未知错误";
        }

        /// <summary>
        /// indecate successful or fail
        /// </summary>
        public int ErrCode { get; set; }
        /// <summary>
        /// msg for result
        /// </summary>
        public string ErrMsg { get; set; }
        /// <summary>
        /// result time
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// indecate success or fail
        /// </summary>
        /// <returns></returns>
        public bool Ok() { return ErrCode == 0; }
        /// <summary>
        /// indecate success or fail
        /// </summary>
        /// <param name="needresponse">是否需要认定response</param>
        /// <returns></returns>
        public bool Ok(bool needresponse)
        {
            return needresponse ? (ValidResponse() && ErrCode == 0) : ErrCode == 0;
        }
        public bool Err() { return ErrCode != 0; }


        public Trequest Request { get; set; }
        public Tresponse Response { get; set; }
        public Client Client { get; set; }
        public User User { get; set; }
        public Secret Secret { get; set; }
        public string Signature
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
            return _signature_set == Signature;
        }
        void Sign()
        {
            var sreq = Request == null ? "" : JsonConvert.SerializeObject(Request);
            var sresp = Response == null ? "" : JsonConvert.SerializeObject(Response);
            var sc = JsonConvert.SerializeObject(Client);
            var su = JsonConvert.SerializeObject(User);
            var ss = JsonConvert.SerializeObject(Secret);
            var sf = new string[] { sc, sreq, sresp, su, ss };
            Array.Sort(sf);
            _signature = string.Join("", sf).ToSHA1();
        }

        public bool ValidRequest()
        {
            if (Response == null) return false;
            if (Request.GetType().IsValueType) return true;
            return Request != null;
        }
        public bool ValidResponse()
        {
            if (Response == null) return false;
            if (Response.GetType().IsValueType) return true;
            return Response != null;
        }

        public override string ToString()
        {
            return string.Format("errcode={0},errmsg={1},time={2}",
                ErrCode, ErrMsg, Time.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }

    public class WsModel<Trequest> : WsModel<Trequest, object>
    {
        public WsModel()
            : base()
        { }
        public WsModel(Trequest request, Client client = null)
            : base(request, client)
        { }
        public WsModel(object response)
            : base(response)
        { }
    }

    public class WsModel : WsModel<object, object>
    {
        public WsModel()
            : base()
        { }
    }
}
