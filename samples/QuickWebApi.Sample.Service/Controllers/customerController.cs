using QuickWebApi.Sample.Apis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace QuickWebApi.Sample.Service.Controllers
{
    public class DB
    {
        static DB()
        {
            customers = new List<customer>();
            for (int i = 0; i < 10; i++)
            {
                customers.Add(new customer() { name = "name" + i, age = 30 + i, id = i, birthday = DateTime.Now.AddYears(-30).AddYears(-i), state = i / 2 == 0 });
            }
        }
        public static List<customer> customers;

    }


    [Route("api/customer_service/{action}/")]
    //[Authorize]
    public class customerController : ApiController, icustomer
    {
        [HttpGet]
        public IHttpActionResult list()
        {
            response_list res = new response_list();
            res.list = DB.customers.ToArray();
            res.count = res.list.Length;
            HttpContext.Current.Base().ApiModel();
            WsModel<string, response_list> model = HttpContext.Current.Base().ApiModel<string, response_list>(null);
            model.Response = res;
            return Ok(model);
        }

        [HttpPost]
        public IHttpActionResult info(WsModel<int> model)
        {
            model.Response = DB.customers.SingleOrDefault(c => c.id == model.Request);
            return Ok(model);
        }

        [HttpPost]
        public IHttpActionResult update(WsModel<request_update, com_result> model)
        {
            var cust = DB.customers.SingleOrDefault(c => c.id == model.Request.id);
            cust.name = model.Request.name;
            model.Response = new com_result();
            return Ok(model);
        }

        [HttpDelete]
        public IHttpActionResult del(WsModel<int, com_result> model)
        {
            DB.customers.RemoveAll(c => c.id == model.Request);
            model.Response = new com_result();
            return Ok(model);
        }

        [HttpPut]
        public IHttpActionResult save(WsModel<customer, com_result> model)
        {
            DB.customers.RemoveAll(c => c.id == model.Request.id);
            DB.customers.Add(model.Request);
            model.Response = new com_result();
            return Ok(model);
        }


        [HttpGet]
        public IHttpActionResult all(long timestamp)
        {
            //new webapi<icompany>().invoke(i => i.all, timestamp);
            return Ok(DB.customers.Where(c => c.timestamp < timestamp));
        }


        [HttpDelete]
        public IHttpActionResult delage(int age)
        {
            return Ok(DB.customers.Where(c => c.age == age));
        }


        [HttpGet]
        public IHttpActionResult pick(int id)
        {
            WsModel<string, customer> model = new WsModel<string, customer>();
            model.Response = DB.customers.SingleOrDefault(c => c.id == id);
            return Ok(model);
        }


        [HttpGet]
        public IHttpActionResult query(int id, string name)
        {
            response_list res = new response_list();
            res.list = DB.customers.Where(c => c.id == id || c.name == name).ToArray();
            res.count = res.list.Length;
            WsModel<string, response_list> model = HttpContext.Current.Base().ApiModel<string, response_list>(null);
            model.Response = res;
            return Ok(model);
        }

        [HttpPost]
        public IHttpActionResult newone(string name)
        {
            WsModel<string, response_list> model = HttpContext.Current.Base().ApiModel<string, response_list>(name);
            response_list res = new response_list();
            res.list = DB.customers.Where(c => c.name == name).ToArray();
            res.count = res.list.Length;
            model.Response = res;
            return Ok(model);
        }

        [HttpPost]
        public IHttpActionResult trigger()
        {
            return Ok(HttpContext.Current.Base().ApiModel());
        }
    }
}
