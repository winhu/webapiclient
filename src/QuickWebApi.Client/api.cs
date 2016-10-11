using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickWebApi
{
    public class api
    {
        public static api get(string localcode, string targetcode, string prefix = null)
        {
            return new api(prefix).WithAuthentication(get_authentications(localcode, targetcode));
        }

        static KeyValuePair<string, string>[] get_authentications(string localcode, string targetcode)
        {
            var token = Tokens.SingleOrDefault(t => t.LocalCode == localcode && t.TargetCode == targetcode);
            if (token == null) return null;
            return token.GetAuthentication();
        }

        static List<WebApiAccessToken<i_access_token>> Tokens = new List<WebApiAccessToken<i_access_token>>();
        public static void Init<Tiaccesstoker>(string localcode, string appkey, string targetcode) where Tiaccesstoker : i_access_token
        {
            if (!Tokens.Exists(t => t.LocalCode == localcode && t.TargetCode == targetcode))
                Tokens.Add(new WebApiAccessToken<i_access_token>(localcode, appkey, targetcode));
        }

        static bool Expired(string localcode, string targetcode, string prefix = null)
        {
            var token = Tokens.SingleOrDefault(t => t.LocalCode == localcode && t.TargetCode == targetcode);
            if (token == null) return true;
            return token.Expired(prefix);
        }
    }
}
