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
    
    public partial class sugar_trx
    {
        public int id { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public Nullable<decimal> quantity { get; set; }
        public string transaction_type { get; set; }
        public Nullable<int> reservation_id { get; set; }
        public Nullable<int> atc_id { get; set; }
        public string created_by { get; set; }
        public string doc_no { get; set; }
        public Nullable<int> raw_weigher_id { get; set; }
        public Nullable<int> rsv_id { get; set; }
        public Nullable<decimal> bags_count { get; set; }
    }
}
