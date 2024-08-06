using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDS_Tz_Helper.Business_Objects
{
    public class Transaction_Datax
    {
        public string msg { get; set; }
        public string atc { get; set; }
        public string sto_loc { get; set; }
        public string driver { get; set; }
        public string vehicle { get; set; }
        public string transporter { get; set; }
        public string gross { get; set; }
        public string tare { get; set; }
        public string nett { get; set; }
        public string sync_status { get; set; }
        public string delivery_no { get; set; }
        public string error_field { get; set; }
        public string pgi_no { get; set; }
        public string waybill_no { get; set; }
        public string sales_type { get; set; }
        public string trip_id { get; set; }
        public string sender { get; set; }
        public string destination { get; set; }
        public string sales_doc_type { get; set; }
        public string operator_weighn { get; set; }
        public string operator_weighout { get; set; }
        public string operator_picking { get; set; }
        public string serial_no { get; set; }
        public string user { get; set; }
        public string seal { get; set; }
        public string atc_no { get; set; }
        public string operatorID { get; set; }
        public DateTime tare_time { get; set; }
        public DateTime gross_time { get; set; }
        public Boolean status { get; set; }
        public Boolean statusPicking { get; set; }
        public Decimal oldTare { get; set; }
        public Decimal oldNet { get; set; }

    }
}