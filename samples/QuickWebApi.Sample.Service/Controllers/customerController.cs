using QuickWebApi.Sample.Apis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    public class customerController : ApiController, icustomer
    {
        [HttpGet]
        public IHttpActionResult list()
        {
            return Ok(new result(0, null, DB.customers.ToList().Count, DB.customers.ToList()));
        }

        [HttpGet]
        public IHttpActionResult info(int customerid)
        {
            return Ok(new result(DB.customers.SingleOrDefault(c => c.id == customerid)));
        }

        [HttpPost]
        public IHttpActionResult update(int id, string name)
        {
            var cust = DB.customers.SingleOrDefault(c => c.id == id);
            cust.name = name;
            return Ok(new result());
        }

        [HttpDelete]
        public IHttpActionResult del(int id)
        {
            DB.customers.RemoveAll(c => c.id == id);
            return Ok(new result());
        }

        [HttpPut]
        public IHttpActionResult save(customer customer)
        {
            DB.customers.RemoveAll(c => c.id == customer.id);
            DB.customers.Add(customer);
            return Ok(new result());
        }


        [HttpGet]
        public IHttpActionResult all(long timestamp)
        {
            new webapi<icompany>().invoke(i => i.all, timestamp);
            return Ok(new result(DB.customers.Where(c => c.timestamp < timestamp)));
        }


        [HttpDelete]
        public IHttpActionResult delage(int age)
        {
            return Ok(new result(DB.customers.Where(c => c.age == age)));
        }


        [HttpGet]
        public IHttpActionResult pick(long timestamp)
        {
            return Ok(new result(DB.customers.Where(c => c.timestamp > timestamp)));
        }
    }
}
