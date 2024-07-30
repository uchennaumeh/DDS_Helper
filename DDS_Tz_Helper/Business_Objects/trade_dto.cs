using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDS_Tz_Helper.Business_Objects
{
    public class trade_dto
    {
        public string msg { get; set; }
        public string driver { get; set; }
        public string opWeighIn { get; set; }
        public string opWeighOut { get; set; }
        public string spareStr6 { get; set; }
        public string transporter_name { get; set; }
        public string waybill { get; set; }
        public string product { get; set; }
        public string sender { get; set; }
        public string receiver { get; set; }
        public string sto_loc { get; set; }
        public string vehicle { get; set; }
        public string atc { get; set; }
        public bool statusPicking { get; set; }
        public string trip_ID { get; set; }
        public string Tare { get; set; }
        public string Gross { get; set; }        
        public string Transaction_Type { get; set; }        
        public string operator_weighn { get; set; }
        public string operatorWeighIn { get; set; }
        public DateTime? tareTime { get; set; }
        public DateTime? FirstDateTime { get; set; }
        public DateTime? tare_time { get; set; }
        public Boolean status { get; set; }
    }
}