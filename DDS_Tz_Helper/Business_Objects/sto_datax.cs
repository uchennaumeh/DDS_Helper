using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDS_Tz_Helper.Business_Objects
{
    public class sto_datax
    {
        public string msg { get; set; }
        public string excptn { get; set; }
        public string grade { get; set; }
        public string TruckDriverName { get; set; }
        public string Migo_Details { get; set; }
        public string truckDestination { get; set; }
        public string operatorWeighin { get; set; }
        public string operatorPicking { get; set; }
        public string transporter { get; set; }
        public string receiver { get; set; }
        public string truckNumber { get; set; }
        public string driverName { get; set; }
        public string stoLoc { get; set; }
        public string approvedBy { get; set; }
        public string requestType { get; set; }
        public string requestedBy { get; set; }
        public string sender { get; set; }
        public string destination { get; set; }
        public string atc { get; set; }
        public string trip_id { get; set; }
        public Decimal Quantity { get; set; }
        public DateTime dateOfRequest { get; set; }
        public DateTime dateOfApproval { get; set; }
        public Boolean status { get; set; }
    }
}