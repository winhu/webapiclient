# quickwebapi
目的：使用Lambada方式，完成对WebApi的开发和调用。

缘由：为了解耦服务和展现，将越来越多的使用WebApi提供各种服务；随着服务的细化，WebApi的接口将越来越多，成百上千。如何方便的管理和调用规模庞大的WebApi接口成了开发者头疼的问题。

设计：通过自定义的QuickWebApiAttribute来对业务接口进行规范和说明，并生成配置文件；可以通过修改配置文件，达成对WebApi的地址的调整而不用修改代码。

用途：除了重新搭建的系统可以使用外，对于一些其它语言（如java等）提供的webapi，只需要定义出相应的业务接口，便可以使用

===================================================================================================================
举例说明：

********************服务端：WebApi业务接口的声明和实现**************************

    //数据模型
    public class customer
    {
        public int id { get; set; }
        public long timestamp { get; set; }
        public string name { get; set; }
        public int age { get; set; }
        public DateTime birthday { get; set; }
        public bool state { get; set; }
    }
    //业务接口动态库声明
    [assembly: QuickWebApiDll("customer", "http://localhost:11520")]
    //业务接口声明
    [QuickWebApi("customer", "api/customer_service", "用户管理")]
    public interface icustomer
    {
        [QuickWebApi(MethodType.HTTPGET, "用户列表", "列举用户信息")]
        IHttpActionResult list();
        [QuickWebApi(MethodType.HTTPGET)]
        IHttpActionResult info(int customerid);
        [QuickWebApi(MethodType.HTTPPOST)]
        IHttpActionResult update(int id, string name);
        [QuickWebApi(MethodType.HTTPDEL)]
        IHttpActionResult del(int id);
        [QuickWebApi(MethodType.HTTPPUT)]
        IHttpActionResult save(customer customer);
    }
    //业务接口实现
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
    }
    
********************调用端：WebApi业务接口的配置生成和加载**************************

    //在服务启动时调用，一般在global中
    webapifactory.Instance.Build_Apis();
    webapifactory.Instance.Load_Apis();

    //配置文件格式(*.xml)如下（每个业务接口生成一个独立的配置文件，便于维护）：
    <?xml version="1.0" encoding="utf-16"?>
    <ArrayOfWebApiNode xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
      <WebApiNode>
        <Name>用户管理</Name>
        <Title>QuickWebApi.Sample.Apis</Title>
        <Version>1.0.0.0</Version>
        <Service>customer</Service>
        <Route>api/customer_service</Route>
        <Uri>http://localhost:11520</Uri>
        <Actions>
          <WebApiMethod>
            <Method>HTTPGET</Method>
            <Action>list</Action>
            <Name>用户列表</Name>
            <Comment>列举用户信息</Comment>
          </WebApiMethod>
          <WebApiMethod>
            <Method>HTTPGET</Method>
            <Action>info</Action>
          </WebApiMethod>
          <WebApiMethod>
            <Method>HTTPPOST</Method>
            <Action>update</Action>
          </WebApiMethod>
          <WebApiMethod>
            <Method>HTTPDEL</Method>
            <Action>del</Action>
          </WebApiMethod>
          <WebApiMethod>
            <Method>HTTPPUT</Method>
            <Action>save</Action>
          </WebApiMethod>
        </Actions>
      </WebApiNode>
    </ArrayOfWebApiNode>

    //并同时生产简单的业务接口描述文件(*.txt)：
    001,用户管理:QuickWebApi.Sample.Apis-1.0.0.0-
    001,http://localhost:11520/api/customer_service/list,用户列表,列举用户信息
    002,http://localhost:11520/api/customer_service/info,,
    005,http://localhost:11520/api/customer_service/update,,
    006,http://localhost:11520/api/customer_service/del,,
    008,http://localhost:11520/api/customer_service/save,,

    
********************调用端：WebApi业务接口的调用**************************

    public class HomeController : Controller
    {
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
        public object info()
        {
            var ret = new webapi<icustomer>().invoke(i => i.info, 4);
            return ret;
        }
        public object update()
        {
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
    }
    
********************其它说明**************************

    //invoke方法的返回结果的数据结构如下：
    public class result<T> where T : class, new()
    {
        //如果通讯不正常，则为返回的HTTP状态吗；否则为服务端返回的状态吗，默认值为0（正常）
        public int errcode { get; set; }    
        //服务端返回的信息
        public string errmsg { get; set; }
        //服务端返回的复杂数据结构，通过Json进行传输
        public T data { get; set; }
        //如果需要返回整型
        public long id { get; set; }
        //服务器端时间
        public DateTime time { get; set; }
     }
     //如果页面上通过ajax直接调用WebApi接口，则需要在服务端注册JsonFormatter，则ajax可以直接得到result的json格式：
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            webapifactory.Instance.Register_JsonFormatter(config);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        }

===================================================================================================================
感谢您的阅读。
目前QuickWebApi还有一些其它想法和设计会未加入其中，后续会继续修缮。且还存在一些未解决的问题，欢迎您能贡献您的智慧和意见。
