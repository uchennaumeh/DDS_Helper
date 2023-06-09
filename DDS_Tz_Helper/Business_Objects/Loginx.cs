using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDS_Tz_Helper.Business_Objects
{
    public class Loginx
    {
         public string msg { get; set; }
        public string excptn { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int role_id { get; set; }
        public string user_status { get; set; }
        public int active { get; set; }
        public Boolean status { get; set; }

    }
}