using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace QuickWebApi.Sample.Apis
{
    public class customer
    {
        public int id { get; set; }
        public long timestamp { get; set; }
        public string name { get; set; }
        public int age { get; set; }
        public DateTime birthday { get; set; }
        public bool state { get; set; }
    }

    [QuickWebApi("customer", "api/customer_service", "用户管理")]
    public interface icustomer
    {
        [QuickWebApi(MethodType.HTTPGET, "用户列表", "列举用户信息")]
        IHttpActionResult list();
        [QuickWebApi(MethodType.HTTPGET)]
        IHttpActionResult info(int customerid);
        [QuickWebApi(MethodType.HTTPGET)]
        IHttpActionResult all(long timestamp);
        [QuickWebApi(MethodType.HTTPGET)]
        IHttpActionResult pick(long timestamp);

        [QuickWebApi(MethodType.HTTPPOST)]
        IHttpActionResult update(int id, string name);
        [QuickWebApi(MethodType.HTTPDEL)]
        IHttpActionResult del(int id);
        [QuickWebApi(MethodType.HTTPDEL)]
        IHttpActionResult delage(int age);
        [QuickWebApi(MethodType.HTTPPUT)]
        IHttpActionResult save(customer customer);
    }

}
