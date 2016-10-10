using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickWebApi
{
    public class Client
    {
        public Client() { }
        public Client(string ip, string sn_imei, string sn_imsi, string devicecode, string deviceinfo)
        {
            this.ip = ip;
            this.sn_imei = sn_imei;
            this.sn_imsi = sn_imsi;
            this.devicecode = devicecode;
            this.deviceinfo = deviceinfo;
        }
        public string sn_imsi { get; set; }
        public string sn_imei { get; set; }
        public string ip { get; set; }
        public string devicecode { get; set; }
        public string deviceinfo { get; set; }
    }

    public class User
    {
        public User() { }
        public User(string sessionid, object ticket = null, string uid = null, string orgcode = null, string syscode = null)
        {
            if (ticket != null)
                this.ticket = ticket.ToString();
            this.uid = uid;
            this.sessionid = sessionid;
            this.orgcode = orgcode;
            this.syscode = syscode;
        }

        public string ticket { get; set; }
        public string uid { get; set; }
        public string orgcode { get; set; }
        public string syscode { get; set; }
        public string sessionid { get; set; }
    }

    public class Secret
    {
        public Secret()
        {
            nonce = Guid.NewGuid().ToString("N");
            time = DateTime.Now;
        }
        string _nonce;
        DateTime _time = DateTime.Now;
        public string nonce
        {
            get
            {
                var n = _nonce;
                _nonce = Guid.NewGuid().ToString("N");
                return n;
            }
            set { _nonce = value; }
        }
        public DateTime time
        {
            get
            {
                var n = _time;
                _time = DateTime.Now;
                return n;
            }
            set { _time = value; }
        }
    }

    public class TokenRequest
    {
        public TokenRequest(string secret)
        {
            _secret = secret;
        }
        public TokenRequest Set(string ip, string realm)
        {
            Ip = ip;
            Realm = realm;
            return this;
        }

        string _nonce, _crypt, _signature, _secret;
        long _timestamp = DateTime.Now.Ticks;
        public string Ip { get; set; }
        public string Realm { get; set; }

        public string Nonce
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_nonce))
                    _nonce = Guid.NewGuid().ToString("N");
                return _nonce;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(_nonce))
                    _nonce = value;
            }
        }
        public long Timestamp
        {
            get
            {
                if (_timestamp == 0)
                    _signature = Guid.NewGuid().ToString("N");
                return _timestamp;
            }
            set
            {
                if (_timestamp == 0)
                    _timestamp = value;
            }
        }
        public string Signature
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_signature))
                {

                    var param = new string[] { Nonce, Timestamp.ToString(), _secret, Ip, Realm };
                    Array.Sort(param);
                    var _sign = String.Join(null, param);
                }
                return _signature;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(_signature))
                    _signature = value;
            }
        }
        public string Crypt
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_crypt))
                    _crypt = Guid.NewGuid().ToString("N");
                return _crypt;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(_crypt))
                    _crypt = value;
            }
        }
    }
}
