using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace QuickWebApi
{
    [WebApi("access_token", "api/access_token", "安全相关接口")]
    public interface i_access_token
    {
        /// <summary>
        /// 获取安全票据
        /// </summary>
        /// <returns></returns>
        [WebApi(MethodType.HTTPPOST, "", "获取安全票据")]
        IHttpActionResult get(TokenRequest request);

        /// <summary>
        /// 获取服务端时间
        /// </summary>
        [WebApi(MethodType.HTTPPOST, "", "获取服务端时间")]
        IHttpActionResult timer();
    }
}
