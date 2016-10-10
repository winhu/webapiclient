using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickWebApi
{

    /// <summary>
    /// commonly used result,
    /// successful when errcode=0
    /// </summary>
    public class result<T>
    //where T : class, new()
    {
        /// <summary>
        /// commonly used result（errcode=0）
        /// </summary>
        public result() : this(0, null, -9999) { }
        /// <summary>
        /// commonly used result（errcode=0）
        /// </summary>
        public result(bool ret) : this(ret ? 0 : -9999, null, -9999) { }
        /// <summary>
        /// commonly used result
        /// </summary>
        public result(bool ret, string err = null) : this(ret ? 0 : -9999, err, -9999) { }
        /// <summary>
        /// commonly used result（errcode=-1 when data=null）
        /// </summary>
        public result(T data) : this(data == null ? -1 : 0, null, -9999, data) { }
        /// <summary>
        /// commonly used result（errcode=0 when errmsg is null or empty）
        /// </summary>
        public result(string errmsg) : this(string.IsNullOrWhiteSpace(errmsg) ? 0 : -1, errmsg, -9999) { }
        /// <summary>
        /// commonly used result
        /// </summary>
        /// <param name="errcode">successful when errcode=0</param>
        /// <param name="errmsg">err msg</param>
        public result(int errcode, string errmsg = null) : this(errcode, errmsg, -9999) { }
        /// <summary>
        /// commonly used result
        /// </summary>
        /// <param name="errcode">successful when errcode=0</param>
        /// <param name="id">if you need, default value is 0</param>
        public result(int errcode, long id) : this(errcode, null, id) { }
        /// <summary>
        /// commonly used result
        /// </summary>
        /// <param name="errcode">successful when errcode=0</param>
        /// <param name="data">if you need, default value is null</param>
        public result(int errcode, T data) : this(errcode, null, -9999, data) { }
        /// <summary>
        /// commonly used result
        /// </summary>
        /// <param name="errcode">commonly used result</param>
        /// <param name="errmsg">err msg</param>
        /// <param name="id">if you need</param>
        public result(int errcode, string errmsg, long id)
        {
            this.errcode = errcode;
            this.errmsg = errmsg;
            this.id = id;
            time = DateTime.Now;
        }
        /// <summary>
        /// commonly used result
        /// </summary>
        /// <param name="errcode">commonly used result</param>
        /// <param name="errmsg">err msg</param>
        /// <param name="id">if you need</param>
        /// <param name="data">if you need</param>
        public result(int errcode, string errmsg, long id, T data)
        {
            this.errcode = errcode;
            this.errmsg = errmsg;
            this.id = id;
            this.data = data;
            time = DateTime.Now;
        }
        /// <summary>
        /// indecate successful or fail
        /// </summary>
        public int errcode { get; set; }
        /// <summary>
        /// msg for result
        /// </summary>
        public string errmsg { get; set; }
        /// <summary>
        /// another result type if you need
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// result time
        /// </summary>
        public DateTime time { get; set; }
        /// <summary>
        /// another result type if you need
        /// </summary>
        public T data { get; set; }

        /// <summary>
        /// indecate success or fail
        /// </summary>
        /// <returns></returns>
        public bool OK() { return errcode == 0; }
        public bool ERR() { return errcode != 0; }

        public bool HasData() { return data != null; }

        public override string ToString()
        {
            return string.Format("errcode={0},errmsg={1},id={2},data={3},time={4}",
                errcode, errmsg, id, data == null ? string.Empty : data.ToString(), time.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }


    /// <summary>
    /// commonly used result,
    /// successful when errcode=0
    /// </summary>
    public class result : result<object>
    {
        /// <summary>
        /// commonly used result（errcode=0）
        /// </summary>
        public result() : this(0, null, -9999, null) { }
        /// <summary>
        /// commonly used result（errcode=0）
        /// </summary>
        public result(bool ret) : this(ret ? 0 : -9999, null, -9999, null) { }
        /// <summary>
        /// commonly used result（errcode=-1 when data=null）
        /// </summary>
        public result(object data) : this(data == null ? -1 : 0, null, -9999, data) { }
        /// <summary>
        /// commonly used result（errcode=0 when errmsg is null or empty）
        /// </summary>
        public result(string errmsg) : this(string.IsNullOrWhiteSpace(errmsg) ? 0 : -9999, errmsg, -9999, null) { }
        /// <summary>
        /// commonly used result
        /// </summary>
        /// <param name="errcode">successful when errcode=0</param>
        /// <param name="errmsg">err msg</param>
        public result(int errcode, string errmsg = null) : this(errcode, errmsg, -9999, null) { }
        /// <summary>
        /// commonly used result
        /// </summary>
        /// <param name="errcode">successful when errcode=0</param>
        /// <param name="id">if you need, default value is 0</param>
        public result(int errcode, long id) : this(errcode, null, id, null) { }
        /// <summary>
        /// commonly used result
        /// </summary>
        /// <param name="errcode">successful when errcode=0</param>
        /// <param name="data">if you need, default value is null</param>
        public result(int errcode, object data) : this(errcode, null, -9999, data) { }
        /// <summary>
        /// commonly used result
        /// </summary>
        /// <param name="errcode">commonly used result</param>
        /// <param name="errmsg">err msg</param>
        /// <param name="id">if you need</param>
        /// <param name="data">if you need</param>
        public result(int errcode, string errmsg, long id, object data)
            : base(errcode, errmsg, id, data)
        { }
    }
}
