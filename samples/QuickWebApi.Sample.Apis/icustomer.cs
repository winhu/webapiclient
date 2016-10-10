using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace QuickWebApi.Sample.Apis
{

    [WebApi("customer", "api/customer_service", "用户管理")]
    public interface icustomer
    {
        [WebApi(MethodType.HTTPGET, "用户列表", "列举所有用户", typeof(response_list))]
        IHttpActionResult list();
        [WebApi(MethodType.HTTPGET, "用户列表", "列举所有用户", typeof(response_list))]
        IHttpActionResult query(int id, string name);
        [WebApi(MethodType.HTTPGET, "用户信息", "获取指定用户信息", typeof(customer))]
        IHttpActionResult info(ws_model<int, customer> model);
        [WebApi(MethodType.HTTPPOST, "更新用户", "更新用户信息")]
        IHttpActionResult update(ws_model<request_update, com_result> model);
        [WebApi(MethodType.HTTPDEL, "", "删除用户")]
        IHttpActionResult del(ws_model<int, com_result> model);
        [WebApi(MethodType.HTTPPUT)]
        IHttpActionResult save(ws_model<customer, com_result> model);
    }

}
