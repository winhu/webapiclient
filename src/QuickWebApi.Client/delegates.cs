using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace QuickWebApi
{
    public delegate IHttpActionResult ApiAction();
    public delegate IHttpActionResult ApiActionL(long args);
    public delegate IHttpActionResult ApiActionLL(long args1, long args2);
    public delegate IHttpActionResult ApiActionLI(long args1, int arg2);
    public delegate IHttpActionResult ApiActionLS(long args1, string args2);
    public delegate IHttpActionResult ApiActionI(int args1);
    public delegate IHttpActionResult ApiActionII(int args1, int args2);
    public delegate IHttpActionResult ApiActionIS(int args1, string args2);
    public delegate IHttpActionResult ApiActionIL(int args1, long args2);
    public delegate IHttpActionResult ApiActionSI(string args1, int args2);
    public delegate IHttpActionResult ApiActionSS(string args1, string args2);
    public delegate IHttpActionResult ApiActionSL(string args1, long args2);
    public delegate IHttpActionResult ApiActionSSS(string args1, string args2, string args3);
    public delegate IHttpActionResult ApiActionSSL(string args1, string args2, long args3);
    public delegate IHttpActionResult ApiActionSLL(string args1, long args2, long args3);
    public delegate IHttpActionResult ApiActionSSI(string args1, string args2, int args3);
    public delegate IHttpActionResult ApiActionSII(string args1, int args2, int args3);
    public delegate IHttpActionResult ApiActionO<treq>(treq data);//where treq : class,new();
    public delegate IHttpActionResult ApiActionS(string args);
}
