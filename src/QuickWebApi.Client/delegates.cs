using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace QuickWebApi
{
    public delegate IHttpActionResult apiaction();
    public delegate IHttpActionResult apiaction_l(long args);
    public delegate IHttpActionResult apiaction_ll(long args1, long args2);
    public delegate IHttpActionResult apiaction_li(long args1, int arg2);
    public delegate IHttpActionResult apiaction_ls(long args1, string args2);
    public delegate IHttpActionResult apiaction_i(int args1);
    public delegate IHttpActionResult apiaction_ii(int args1, int args2);
    public delegate IHttpActionResult apiaction_is(int args1, string args2);
    public delegate IHttpActionResult apiaction_il(int args1, long args2);
    public delegate IHttpActionResult apiaction_si(string args1, int args2);
    public delegate IHttpActionResult apiaction_ss(string args1, string args2);
    public delegate IHttpActionResult apiaction_sl(string args1, long args2);
    public delegate IHttpActionResult apiaction_sss(string args1, string args2, string args3);
    public delegate IHttpActionResult apiaction_o<treq>(treq data) where treq : class,new();
    //public delegate IHttpActionResult apiaction_s<treq>(treq data) where treq : struct;
}
