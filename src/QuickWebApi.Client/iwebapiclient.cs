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

        //result Excute(string requestUri, object req);
        //result<tresp> Excute<tresp>(string requestUri, object req);// where tresp : class,new();

        ws_model<Trequest, Tresponse> Invoke<Trequest, Tresponse>(string requestUri, ws_model<Trequest, Tresponse> model);
        ws_model<Trequest> Invoke<Trequest>(string requestUri, ws_model<Trequest> model);
        ws_model Invoke(string requestUri, ws_model model);
    }

    public interface iwebapiclient : IDisposable
    {
        //result<tresp> Get<tresp>(string requestUri);// where tresp : class,new();
        //result<tresp> Get<tresp>(string requestUri, object req = null);// where tresp : class,new();

        //result Get(string requestUri, object req = null);
        //result<tresp> Get<tresp>(string requestUri, object req = null);// where tresp : class,new();

        //result Post(string requestUri, object req);
        //result<tresp> Post<tresp>(string requestUri, object req);// where tresp : class,new();

        //result Put(string requestUri, object req);
        //result<tresp> Put<tresp>(string requestUri, object req);// where tresp : class,new();

        //result Delete(string requestUri, object req);
        //result<tresp> Delete<tresp>(string requestUri, object req);// where tresp : class,new();


        ws_model<Trequest, Tresponse> Invoke<Trequest, Tresponse>(string requestUri, ws_model<Trequest, Tresponse> model, MethodType mtd);
        ws_model<Trequest> Invoke<Trequest>(string requestUri, ws_model<Trequest> model, MethodType mtd);
        ws_model Invoke(string requestUri, ws_model model, MethodType mtd);
    }
}
