using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickWebApi.Sample.Apis
{
    public class response_list : com_result
    {
        public int count { get; set; }
        public customer[] list { get; set; }
    }
    public class request_update
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    public class com_result
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }
    }


    [Description("用户信息")]
    public class customer
    {
        [Description("id")]
        public int id { get; set; }
        [Description("时间戳")]
        public long timestamp { get; set; }
        [Description("姓名")]
        public string name { get; set; }
        [Description("性别")]
        public int age { get; set; }
        [Description("生日")]
        public DateTime birthday { get; set; }
        [Description("状态")]
        public bool state { get; set; }
    }
}
