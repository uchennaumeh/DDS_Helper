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
    
    public partial class weight_correction
    {
        public int id { get; set; }
        public string atc_no { get; set; }
        public string truck_no { get; set; }
        public string gross_weight { get; set; }
        public string tare_weight { get; set; }
        public string net_weight { get; set; }
        public string new_gross { get; set; }
        public string new_net { get; set; }
        public string new_tare { get; set; }
        public string reason { get; set; }
        public string approved_by { get; set; }
        public string operated_by { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public string status { get; set; }
        public Nullable<int> change_request_id { get; set; }
    }
}
