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
        [WebApi(MethodType.HTTPGET, "用户列表", "列举所有用户", typeof(customer))]
        IHttpActionResult pick(int id);
        [WebApi(MethodType.HTTPGET, "用户列表", "列举所有用户", typeof(response_list))]
        IHttpActionResult list();
        [WebApi(MethodType.HTTPPOST, "更新用户", "更新用户信息")]
        IHttpActionResult querybyname(WsModel<string, response_list> model);
        [WebApi(MethodType.HTTPGET, "用户列表", "列举所有用户", typeof(response_list))]
        IHttpActionResult query(int id, string name);
        [WebApi(MethodType.HTTPPOST, "用户信息", "获取指定用户信息", typeof(customer))]
        IHttpActionResult info(WsModel<int> model);
        [WebApi(MethodType.HTTPPOST, "更新用户", "更新用户信息")]
        IHttpActionResult update(WsModel<request_update, com_result> model);
        [WebApi(MethodType.HTTPPOST, "更新用户", "更新用户信息")]
        IHttpActionResult newone(string name);
        [WebApi(MethodType.HTTPPOST, "", "删除用户")]
        IHttpActionResult del(WsModel<int, com_result> model);
        [WebApi(MethodType.HTTPPUT)]
        IHttpActionResult save(WsModel<customer, com_result> model);
        [WebApi(MethodType.HTTPPOST)]
        IHttpActionResult trigger();
    }

}
