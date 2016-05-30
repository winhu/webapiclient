using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace QuickWebApi
{
    public interface iinvoker : IDisposable
    {
        //void Excute(EventHandler eh, string data);
        //result Excute(string requestUri, string data);
        result Excute(string requestUri, object req);
        result<tresp> Excute<tresp>(string requestUri, object req) where tresp : class,new();
    }

    public interface iwebapiclient : IDisposable
    {
        result Get(string requestUri, object req);
        result<tresp> Get<tresp>(string requestUri, object req) where tresp : class,new();

        result Post(string requestUri, object req);
        result<tresp> Post<tresp>(string requestUri, object req) where tresp : class,new();

        result Put(string requestUri, object req);
        result<tresp> Put<tresp>(string requestUri, object req) where tresp : class,new();

        result Delete(string requestUri, object req);
        result<tresp> Delete<tresp>(string requestUri, object req) where tresp : class,new();
    }
}
