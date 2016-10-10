using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickWebApi
{
    public class AccessToken
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
    }

    public class WebApiAccessToken<T> where T : i_access_token
    {
        public WebApiAccessToken(string syscode, string appkey, string targetcode)
        {
            this.LocalCode = syscode;
            this.Secret = appkey;
            this.TargetCode = targetcode;
            Expires = DateTime.Now;
            //TAccessToken = taccesstoken;
            _authentication[0] = new KeyValuePair<string, string>("REMOTE_SYSCODE", LocalCode);
        }

        public AccessToken Token { get; set; }
        public KeyValuePair<string, string>[] Authentication { get; set; }
        public string LocalCode { get; set; }
        public string TargetCode { get; set; }
        public string Secret { get; set; }
        public DateTime Expires { get; set; }
        //public T TAccessToken { get; set; }

        KeyValuePair<string, string>[] _authentication = new KeyValuePair<string, string>[2];
        public KeyValuePair<string, string>[] GetAuthentication()
        {
            return _authentication;
        }

        public bool Expired(string prefix = null)
        {
            if (Expires >= DateTime.Now)
            {
                var tr = new TokenRequest(Secret);
                //string nonce = Guid.NewGuid().ToString("N");
                //long timespan = DateTime.Now.Ticks;
                //var param = new string[] { nonce, timespan.ToString(), LocalCode, Secret };
                //Array.Sort(param);
                //var _sign = String.Join(null, param);
                //string data = string.Format("nonce={0}&timespan={1}&signature={2}", nonce, timespan, _sign);
                var ret = new webapi<T, AccessToken>(prefix).invoke(i => i.get, tr);
                if (ret.OK())
                {
                    Token = ret.data;
                    Expires = DateTime.Now.AddSeconds(Token.expires_in - 120);
                    _authentication[1] = new KeyValuePair<string, string>("REMOTE_ACCESS_TOKEN", Token.access_token);
                    return true;
                }
                return false;
            }
            return true;
        }
    }
}
