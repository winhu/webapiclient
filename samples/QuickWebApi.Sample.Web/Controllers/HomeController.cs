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
            var ret = new webapi<icustomer>().invoke(i => i.list);
            return ret;
        }
        public JsonResult customer_list()
        {
            var ret = new webapi<icustomer, List<customer>>().invoke(i => i.list);
            List<object> custs = new List<object>();
            foreach (var cust in ret.data)
            {
                custs.Add(new { id = cust.id, name = cust.name, age = cust.age });
            }
            return Json(custs, JsonRequestBehavior.AllowGet);
        }
        public object all()
        {
            int a = 1;
            //var b = new webapi<icompany>().invoke(i => i.all, a);
            var ret = new webapi<icustomer>().invoke(i => i.all, DateTime.Now.Ticks);
            return ret;
        }
        public object pick()
        {
            var ret = new webapi<icustomer>().invoke(i => i.pick, DateTime.Now.Ticks);
            return ret;
        }
        public object info()
        {
            var ret = new webapi<icustomer>().invoke(i => i.info, 4);
            return ret;
        }
        public object update()
        {
            var cust = new customer() { id = 3, name = "new name", age = 22, timestamp = DateTime.Now.Ticks, birthday = DateTime.Now.AddYears(-10) };
            var ret = new webapi<icustomer>().invoke(i => i.update, 3, "new name");
            return ret;
        }
        public object save()
        {
            var cust = new customer() { id = 3, name = "new name", age = 22, timestamp = DateTime.Now.Ticks, birthday = DateTime.Now.AddYears(-10) };
            var ret = new webapi<icustomer>().invoke(i => i.save, cust);
            return ret;
        }
        public object delete()
        {
            var ret = new webapi<icustomer>().invoke(i => i.del, 4);
            return ret;
        }
        public object deleteage()
        {
            var ret = new webapi<icustomer>().invoke(i => i.delage, 33);
            return ret;
        }
    }
}