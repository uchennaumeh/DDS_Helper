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
    
    public partial class atc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public atc()
        {
            this.atc_transactions = new HashSet<atc_transactions>();
            this.transport_clearance = new HashSet<transport_clearance>();
            this.weighbridge_images = new HashSet<weighbridge_images>();
        }
    
        public int Id { get; set; }
        public string delivery_no { get; set; }
        public string delv_doc_type { get; set; }
        public string parent_so { get; set; }
        public string sales_doc_number { get; set; }
        public string sales_doc_type { get; set; }
        public string customer_no { get; set; }
        public string customer_name { get; set; }
        public string dispatch_plant { get; set; }
        public string shipping_point { get; set; }
        public string material { get; set; }
        public string del_qty { get; set; }
        public string material_doc_time { get; set; }
        public string uom { get; set; }
        public string dispatch_data { get; set; }
        public string intf_time { get; set; }
        public string waybill { get; set; }
        public string truck_no { get; set; }
        public string drivers_name { get; set; }
        public string trip_id { get; set; }
        public string cab_no { get; set; }
        public string tare_weight { get; set; }
        public string gross_weight { get; set; }
        public string state { get; set; }
        public string region { get; set; }
        public string location { get; set; }
        public string sales_officer { get; set; }
        public string sales_manager { get; set; }
        public string regional_director { get; set; }
        public string mov_type { get; set; }
        public string profit_center { get; set; }
        public string risk_category { get; set; }
        public string total_gds_mvt_stats { get; set; }
        public string bill_stat_order { get; set; }
        public string ovr_pick_status { get; set; }
        public string invoice_no { get; set; }
        public Nullable<System.DateTime> datetime_retrieved { get; set; }
        public Nullable<System.DateTime> datetime_update_to_sap { get; set; }
        public Nullable<bool> updated_to_sap { get; set; }
        public string sap_delivery_no { get; set; }
        public string sap_waybill { get; set; }
        public string sap_invoice_no { get; set; }
        public Nullable<System.DateTime> material_doc_date { get; set; }
        public string hash_sap { get; set; }
        public string hash_weight { get; set; }
        public string delivery_block { get; set; }
        public string billing_block { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<atc_transactions> atc_transactions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<transport_clearance> transport_clearance { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<weighbridge_images> weighbridge_images { get; set; }
    }
}