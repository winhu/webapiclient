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
            this.Ip = ip;
            this.Imei = sn_imei;
            this.Imsi = sn_imsi;
            this.DeviceCode = devicecode;
            this.DeviceInfo = deviceinfo;
        }
        public string Imsi { get; set; }
        public string Imei { get; set; }
        public string Ip { get; set; }
        public string DeviceCode { get; set; }
        public string DeviceInfo { get; set; }
    }

    public class User
    {
        public User() { }
        public User(string sessionid, object ticket = null, string uid = null, string orgcode = null, string syscode = null)
        {
            if (ticket != null)
                this.Ticket = ticket.ToString();
            this.Uid = uid;
            this.SessionId = sessionid;
            this.OrgCode = orgcode;
            this.SysCode = syscode;
        }

        public string Ticket { get; set; }
        public string Uid { get; set; }
        public string OrgCode { get; set; }
        public string SysCode { get; set; }
        public string SessionId { get; set; }
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
                    _signature = String.Join(null, param).ToSHA1();
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
