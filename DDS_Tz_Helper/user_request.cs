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
    
    public partial class user_request
    {
        public int id { get; set; }
        public Nullable<int> userid { get; set; }
        public Nullable<int> role_requested { get; set; }
        public Nullable<int> role_granted { get; set; }
        public string description { get; set; }
        public Nullable<System.DateTime> request_date { get; set; }
        public Nullable<System.DateTime> grant_date { get; set; }
        public string status { get; set; }
    
        public virtual user user { get; set; }
    }
}
