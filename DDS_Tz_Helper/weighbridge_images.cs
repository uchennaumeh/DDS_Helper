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
    
    public partial class weighbridge_images
    {
        public int id { get; set; }
        public Nullable<int> atc_id { get; set; }
        public Nullable<int> device_id { get; set; }
        public string filename { get; set; }
        public string location { get; set; }
    
        public virtual atc atc { get; set; }
    }
}
