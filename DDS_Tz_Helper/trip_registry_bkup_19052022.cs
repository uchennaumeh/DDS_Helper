//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DDS_Tz_Helper
{
    using System;
    using System.Collections.Generic;
    
    public partial class trip_registry_bkup_19052022
    {
        public int Id { get; set; }
        public string trip_no { get; set; }
        public string cab_id { get; set; }
        public Nullable<int> driver_id { get; set; }
        public Nullable<int> truck_id { get; set; }
        public Nullable<int> atc_id { get; set; }
        public Nullable<int> clearance_id { get; set; }
        public Nullable<System.DateTime> creation_datetime { get; set; }
        public string contact_person { get; set; }
        public string source { get; set; }
        public string fleet_number { get; set; }
        public Nullable<double> fuel_allowed { get; set; }
        public Nullable<int> ship_to_party { get; set; }
        public Nullable<int> sold_to_party { get; set; }
        public string destination { get; set; }
        public Nullable<double> initial_km { get; set; }
        public Nullable<double> additional_km { get; set; }
        public Nullable<double> fuel_allowance1 { get; set; }
        public Nullable<double> fuel_allowance2 { get; set; }
        public Nullable<double> fuel_allowance3 { get; set; }
        public Nullable<double> fuel_allowance4 { get; set; }
        public string status { get; set; }
        public Nullable<int> transporter_id { get; set; }
        public Nullable<int> sap_atc_id { get; set; }
        public string atc_no { get; set; }
        public string driver_id_sap { get; set; }
        public string ship_to_party_sap { get; set; }
        public string sold_to_party_sap { get; set; }
        public Nullable<bool> sap_status { get; set; }
        public string sap_message { get; set; }
        public string trip_id_sap { get; set; }
        public string atc_type { get; set; }
        public string frst_fuel_reason { get; set; }
        public string fleet { get; set; }
        public string driver_name { get; set; }
        public string tms_status { get; set; }
        public string plant_code { get; set; }
        public string delivery_address { get; set; }
        public string tms_online_status { get; set; }
        public Nullable<bool> cng_allowed { get; set; }
        public string contact_no { get; set; }
        public Nullable<System.DateTime> exp_arrival { get; set; }
        public Nullable<decimal> excessFuel { get; set; }
        public string fuel2_allowed_rs { get; set; }
        public string fuel3_allowed_rs { get; set; }
        public string fuel4_allowed_rs { get; set; }
    }
}