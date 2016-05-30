using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace QuickWebApi.Sample.Apis
{
    [QuickWebApi("icompany", "api/icompany_service", "用户管理")]
    public interface icompany
    {

        [QuickWebApi(MethodType.HTTPGET)]
        IHttpActionResult info(int customerid);
        [QuickWebApi(MethodType.HTTPGET)]
        IHttpActionResult all(long timestamp);
        [QuickWebApi(MethodType.HTTPGET)]
        IHttpActionResult remove(int id);
    }
}
