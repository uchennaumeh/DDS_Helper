using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDS_Tz_Helper.Business_Objects
{
    public class Trip_Registryx
    {

        public string msg { get; set; }
        public string excptn { get; set; }
        public string username { get; set; }
        public string driver_id { get; set; }
        public string truck_id { get; set; }
        public string source { get; set; }
        public string destination { get; set; }
        public string atc_no { get; set; }
        public string driver_id_sap { get; set; }
        public string trip_id_sap { get; set; }
        public string fleet { get; set; }
        public string tms_status { get; set; }
        public string tms_online_status { get; set; }
        public int role_id { get; set; }
        public string user_status { get; set; }
        public string ship_to_party_sap { get; set; }
        public int active { get; set; }
        public Boolean status { get; set; }
    }
}