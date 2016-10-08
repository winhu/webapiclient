using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace QuickWebApi
{
    public class ws_model<Trequest, Tresponse>
        where Trequest : class ,new()
        where Tresponse : class,new()
    {
        public ws_model()
        {
            this.secret = new Secret();
        }
        public ws_model(Trequest request, Client client)
        {
            this.request = request;
            this.client = client;
            this.secret = new Secret();
        }

        public Trequest request { get; set; }
        public Tresponse response { get; set; }
        public Client client { get; set; }
        public User user { get; set; }
        public Secret secret { get; set; }
        public string signature
        {
            get
            {
                sign();
                return _signature;
            }
            set { _signature_set = value; }
        }
        private string _signature_set = null;
        private string _signature = null;
        bool valid_sign()
        {
            return _signature_set == signature;
        }
        void sign()
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
    }

    public class ws_model<Trequest> : ws_model<Trequest, object>
        where Trequest : class ,new()
    {
        public ws_model()
        {
            this.secret = new Secret();
        }
        public ws_model(Trequest request)
        {
            this.request = request;
            this.secret = new Secret();
        }
    }
}
