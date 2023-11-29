using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDS_Tz_Helper.Business_Objects
{
    public class view_closure_dto
    {
        public int Id { get; set; }
        public string trip_id { get; set; }
        public string trip_id_sap { get; set; }
        public string damaged_qty { get; set; }
        public string caked_qty { get; set; }
        public string short_qty { get; set; }
        public string customer_no { get; set; }
        public string address { get; set; }
        public string waybill { get; set; }
        public string miss_reason { get; set; }
        public string atc_type { get; set; }
        public string fleet { get; set; }
        public string migo_details { get; set; }
        public string driver_id_sap { get; set; }
        public string driver_name { get; set; }
        public string atc_no { get; set; }
        public string msg { get; set; }
        public bool status { get; set; }
        public Nullable<System.DateTime> closure_date { get; set; }
        public Nullable<System.DateTime> sap_time { get; set; }
        //sap_status

        public string trx_type { get; set; }
    }
}