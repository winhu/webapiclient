using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace QuickWebApi.Sample.Apis
{
    [Description("用户信息")]
    public class customer
    {
        [Description("id")]
        public int id { get; set; }
        [Description("时间戳")]
        public long timestamp { get; set; }
        [Description("姓名")]
        public string name { get; set; }
        [Description("性别")]
        public int age { get; set; }
        [Description("生日")]
        public DateTime birthday { get; set; }
        [Description("状态")]
        public bool state { get; set; }
    }

    [WebApi("customer", "api/customer_service", "用户管理")]
    public interface icustomer
    {
        [WebApi(MethodType.HTTPGET, "用户列表", "列举所有用户", typeof(customer[]))]
        IHttpActionResult list();
        [WebApi(MethodType.HTTPGET, "用户信息", "获取指定用户信息", typeof(customer))]
        IHttpActionResult info(int customerid);
        [WebApi(MethodType.HTTPGET, "更新用户", "更新用户信息")]
        IHttpActionResult update(int id, string name);
        [WebApi(MethodType.HTTPGET, "", "删除用户")]
        IHttpActionResult del(int id);
        [WebApi(MethodType.HTTPPUT)]
        IHttpActionResult save(customer customer);
    }

}
