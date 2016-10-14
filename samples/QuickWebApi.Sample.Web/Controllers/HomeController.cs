using QuickWebApi.Sample.Apis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuickWebApi.Sample.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() { return View(); }

        public object customers()
        {
            var ret = HttpContext.ApiInvoke<icustomer, response_list>(i => i.list);
            return ret;
        }
        public object query(int id = 2, string name = "name3")
        {
            //var ret1 = HttpContext.ApiInvoke<icustomer>(i => i.newone, name);
            var ret = HttpContext.ApiInvoke<icustomer, response_list>(i => i.query, id, name);
            return ret;
        }
        public object queryname(string name = "name3")
        {
            var ret = HttpContext.ApiInvoke<icustomer, response_list>(i => i.querybyname, name);
            return ret;
        }
        public object query1(int id = 2, string name = "name3")
        {
            var ret = HttpContext.ApiInvoke<icustomer>(i => i.query, id, name);
            return ret;
        }
        public JsonResult customer_list()
        {
            var ret = HttpContext.ApiInvoke<icustomer, response_list>(i => i.list);
            if (ret.Ok())
            {
                List<object> custs = new List<object>();
                foreach (var cust in ret.Response.list)
                {
                    custs.Add(new { id = cust.id, name = cust.name, age = cust.age });
                }
                return Json(custs, JsonRequestBehavior.AllowGet);
            }
            return Json(ret, JsonRequestBehavior.AllowGet);
        }
        public object info()
        {
            var ret = HttpContext.ApiInvoke<icustomer, int>(i => i.info, 4);
            return ret;
        }
        public object update()
        {
            var ret = HttpContext.ApiInvoke<icustomer, request_update, com_result>(i => i.update, new request_update() { id = 3, name = "new name" });
            return ret;
        }
        public object save()
        {
            var cust = new customer() { id = 3, name = "new name", age = 22, timestamp = DateTime.Now.Ticks, birthday = DateTime.Now.AddYears(-10) };
            var ret = HttpContext.ApiInvoke<icustomer, customer, com_result>(i => i.save, cust);
            return ret;
        }
        public object delete()
        {
            var ret = HttpContext.ApiInvoke<icustomer, int, com_result>(i => i.del, 4);
            return ret;
        }
        public object trigger()
        {
            var ret = HttpContext.ApiInvoke<icustomer>(i => i.trigger);
            return ret;
        }
        public object newone(string name = "new hyf one")
        {
            var ret = HttpContext.ApiInvoke<icustomer, response_list>(i => i.newone, name);
            return ret;
        }
    }
}