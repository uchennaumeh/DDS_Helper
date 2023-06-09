﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DispatchDBEntities : DbContext
    {
        public DispatchDBEntities()
            : base("name=DispatchDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<C__RefactorLog> C__RefactorLog { get; set; }
        public virtual DbSet<alert_message> alert_message { get; set; }
        public virtual DbSet<alert_recipient> alert_recipient { get; set; }
        public virtual DbSet<approval> approvals { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<atc> atcs { get; set; }
        public virtual DbSet<atc_card> atc_card { get; set; }
        public virtual DbSet<atc_correction> atc_correction { get; set; }
        public virtual DbSet<atc_data> atc_data { get; set; }
        public virtual DbSet<atc_session> atc_session { get; set; }
        public virtual DbSet<atc_transactions> atc_transactions { get; set; }
        public virtual DbSet<ATCDriverMaster> ATCDriverMasters { get; set; }
        public virtual DbSet<ATCDriverRemark> ATCDriverRemarks { get; set; }
        public virtual DbSet<bag_counter> bag_counter { get; set; }
        public virtual DbSet<bag_counter_config> bag_counter_config { get; set; }
        public virtual DbSet<bin> bins { get; set; }
        public virtual DbSet<block_reason> block_reason { get; set; }
        public virtual DbSet<card> cards { get; set; }
        public virtual DbSet<card_history> card_history { get; set; }
        public virtual DbSet<change_request> change_request { get; set; }
        public virtual DbSet<cng_master> cng_master { get; set; }
        public virtual DbSet<cng_posting> cng_posting { get; set; }
        public virtual DbSet<config_conversion> config_conversion { get; set; }
        public virtual DbSet<customer> customers { get; set; }
        public virtual DbSet<customer_block_reason> customer_block_reason { get; set; }
        public virtual DbSet<dds_config> dds_config { get; set; }
        public virtual DbSet<depot> depots { get; set; }
        public virtual DbSet<device> devices { get; set; }
        public virtual DbSet<DocNumber> DocNumbers { get; set; }
        public virtual DbSet<DocType> DocTypes { get; set; }
        public virtual DbSet<driver> drivers { get; set; }
        public virtual DbSet<eATC> eATCs { get; set; }
        public virtual DbSet<email> emails { get; set; }
        public virtual DbSet<equipment_detail> equipment_detail { get; set; }
        public virtual DbSet<equipment_master> equipment_master { get; set; }
        public virtual DbSet<estate_field> estate_field { get; set; }
        public virtual DbSet<excel_batch> excel_batch { get; set; }
        public virtual DbSet<excel_upload> excel_upload { get; set; }
        public virtual DbSet<fleet_master> fleet_master { get; set; }
        public virtual DbSet<fuel_master> fuel_master { get; set; }
        public virtual DbSet<fuel_posting> fuel_posting { get; set; }
        public virtual DbSet<gas_station> gas_station { get; set; }
        public virtual DbSet<gate_access_log> gate_access_log { get; set; }
        public virtual DbSet<gateaccess_info> gateaccess_info { get; set; }
        public virtual DbSet<harvest> harvests { get; set; }
        public virtual DbSet<harvest_crop_cycle> harvest_crop_cycle { get; set; }
        public virtual DbSet<harvest_estate> harvest_estate { get; set; }
        public virtual DbSet<harvest_field> harvest_field { get; set; }
        public virtual DbSet<harvest_plot> harvest_plot { get; set; }
        public virtual DbSet<harvest_spacing> harvest_spacing { get; set; }
        public virtual DbSet<harvest_variety> harvest_variety { get; set; }
        public virtual DbSet<Kna1> Kna1 { get; set; }
        public virtual DbSet<loading_bay> loading_bay { get; set; }
        public virtual DbSet<loadingbay_queue> loadingbay_queue { get; set; }
        public virtual DbSet<master_file> master_file { get; set; }
        public virtual DbSet<material> materials { get; set; }
        public virtual DbSet<median_tareweight> median_tareweight { get; set; }
        public virtual DbSet<module_access> module_access { get; set; }
        public virtual DbSet<monthly_target> monthly_target { get; set; }
        public virtual DbSet<outgrower> outgrowers { get; set; }
        public virtual DbSet<page_view> page_view { get; set; }
        public virtual DbSet<person> people { get; set; }
        public virtual DbSet<picking_correction> picking_correction { get; set; }
        public virtual DbSet<plant> plants { get; set; }
        public virtual DbSet<raw_weigher> raw_weigher { get; set; }
        public virtual DbSet<raw_weigher_trx> raw_weigher_trx { get; set; }
        public virtual DbSet<region_data> region_data { get; set; }
        public virtual DbSet<reservation> reservations { get; set; }
        public virtual DbSet<role> roles { get; set; }
        public virtual DbSet<ScaleWinNT> ScaleWinNTs { get; set; }
        public virtual DbSet<shift> shifts { get; set; }
        public virtual DbSet<sloc_material> sloc_material { get; set; }
        public virtual DbSet<sloc_shipping> sloc_shipping { get; set; }
        public virtual DbSet<sto_data> sto_data { get; set; }
        public virtual DbSet<sto_transaction_data> sto_transaction_data { get; set; }
        public virtual DbSet<sto_transaction_pgi> sto_transaction_pgi { get; set; }
        public virtual DbSet<sugar_trx> sugar_trx { get; set; }
        public virtual DbSet<system_setting> system_setting { get; set; }
        public virtual DbSet<tracker> trackers { get; set; }
        public virtual DbSet<Trade> Trades { get; set; }
        public virtual DbSet<transaction_data> transaction_data { get; set; }
        public virtual DbSet<transaction_log> transaction_log { get; set; }
        public virtual DbSet<transport_clearance> transport_clearance { get; set; }
        public virtual DbSet<transporter> transporters { get; set; }
        public virtual DbSet<trip_closure> trip_closure { get; set; }
        public virtual DbSet<trip_receipt_views> trip_receipt_views { get; set; }
        public virtual DbSet<trip_registry> trip_registry { get; set; }
        public virtual DbSet<trip_sync> trip_sync { get; set; }
        public virtual DbSet<truck> trucks { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<user_modification> user_modification { get; set; }
        public virtual DbSet<user_request> user_request { get; set; }
        public virtual DbSet<vbbe> vbbes { get; set; }
        public virtual DbSet<vbpa> vbpas { get; set; }
        public virtual DbSet<vbpa1> vbpas1 { get; set; }
        public virtual DbSet<waybill_views> waybill_views { get; set; }
        public virtual DbSet<weighbridge> weighbridges { get; set; }
        public virtual DbSet<weighbridge_images> weighbridge_images { get; set; }
        public virtual DbSet<weighbridge_images_sap> weighbridge_images_sap { get; set; }
        public virtual DbSet<weight_change> weight_change { get; set; }
        public virtual DbSet<weight_correction> weight_correction { get; set; }
        public virtual DbSet<weight_switch> weight_switch { get; set; }
        public virtual DbSet<atc_audit> atc_audit { get; set; }
        public virtual DbSet<trade_audit> trade_audit { get; set; }
        public virtual DbSet<UserList> UserLists { get; set; }
        public virtual DbSet<Weighbridge_Line> Weighbridge_Line { get; set; }
        public virtual DbSet<view_atc_session_transaction> view_atc_session_transaction { get; set; }
        public virtual DbSet<view_bag_counter> view_bag_counter { get; set; }
        public virtual DbSet<view_dispatch> view_dispatch { get; set; }
        public virtual DbSet<view_driver_trip> view_driver_trip { get; set; }
        public virtual DbSet<view_equipment> view_equipment { get; set; }
        public virtual DbSet<view_equipment_full> view_equipment_full { get; set; }
        public virtual DbSet<view_equipment_trip> view_equipment_trip { get; set; }
        public virtual DbSet<view_estate_field> view_estate_field { get; set; }
        public virtual DbSet<view_field> view_field { get; set; }
        public virtual DbSet<view_harvest> view_harvest { get; set; }
        public virtual DbSet<view_outgrowers> view_outgrowers { get; set; }
        public virtual DbSet<view_plot> view_plot { get; set; }
        public virtual DbSet<view_sugar_trx> view_sugar_trx { get; set; }
        public virtual DbSet<view_transactions> view_transactions { get; set; }
        public virtual DbSet<transaction_data_archive> transaction_data_archive { get; set; }
        public virtual DbSet<atc_cleanup> atc_cleanup { get; set; }
        public virtual DbSet<close_transaction> close_transaction { get; set; }
        public virtual DbSet<destination> destinations { get; set; }
        public virtual DbSet<po_plant> po_plant { get; set; }
        public virtual DbSet<sales_doc_type> sales_doc_type { get; set; }
        public virtual DbSet<sto_transaction_data_archive> sto_transaction_data_archive { get; set; }
        public virtual DbSet<transaction_data_backup190720222> transaction_data_backup190720222 { get; set; }
        public virtual DbSet<trx_data_audit> trx_data_audit { get; set; }
        public virtual DbSet<customer_back_up_07102021> customer_back_up_07102021 { get; set; }
        public virtual DbSet<driver_bkup_04062022> driver_bkup_04062022 { get; set; }
        public virtual DbSet<equipment_detail_bkup_04062022> equipment_detail_bkup_04062022 { get; set; }
        public virtual DbSet<equipment_detail_bkup_19052022> equipment_detail_bkup_19052022 { get; set; }
        public virtual DbSet<fleet_master_back_up_24022022> fleet_master_back_up_24022022 { get; set; }
        public virtual DbSet<fuel_master_back_up_13052022> fuel_master_back_up_13052022 { get; set; }
        public virtual DbSet<fuel_master_bkp> fuel_master_bkp { get; set; }
        public virtual DbSet<fuel_master_bkup_06052023> fuel_master_bkup_06052023 { get; set; }
        public virtual DbSet<fuel_master_bkup_06082022> fuel_master_bkup_06082022 { get; set; }
        public virtual DbSet<fuel_master_bkup_14032023> fuel_master_bkup_14032023 { get; set; }
        public virtual DbSet<fuel_master_bkup_21082022> fuel_master_bkup_21082022 { get; set; }
        public virtual DbSet<fuel_master_bkup08112022> fuel_master_bkup08112022 { get; set; }
        public virtual DbSet<region_data_bkup_06052023> region_data_bkup_06052023 { get; set; }
        public virtual DbSet<sto_data_backup> sto_data_backup { get; set; }
        public virtual DbSet<trade_archive> trade_archive { get; set; }
        public virtual DbSet<trade_bakup> trade_bakup { get; set; }
        public virtual DbSet<transaction_data_back_up_01102021> transaction_data_back_up_01102021 { get; set; }
        public virtual DbSet<transaction_data_back_up_14042022> transaction_data_back_up_14042022 { get; set; }
        public virtual DbSet<transaction_data_bkup_19072022> transaction_data_bkup_19072022 { get; set; }
        public virtual DbSet<transactions_returned> transactions_returned { get; set; }
        public virtual DbSet<trip_registry_bkup_06052023> trip_registry_bkup_06052023 { get; set; }
        public virtual DbSet<trip_registry_bkup_06062022> trip_registry_bkup_06062022 { get; set; }
        public virtual DbSet<trip_registry_bkup_17052022> trip_registry_bkup_17052022 { get; set; }
        public virtual DbSet<trip_registry_bkup_19052022> trip_registry_bkup_19052022 { get; set; }
        public virtual DbSet<trip_registry_bkup_25052022> trip_registry_bkup_25052022 { get; set; }
        public virtual DbSet<trip_registry_bkup_26052022> trip_registry_bkup_26052022 { get; set; }
        public virtual DbSet<user_back_up_27092021> user_back_up_27092021 { get; set; }
        public virtual DbSet<user_request_back_up_27092021> user_request_back_up_27092021 { get; set; }
        public virtual DbSet<cng_master_bkup_06062022> cng_master_bkup_06062022 { get; set; }
        public virtual DbSet<trip_registry_backup> trip_registry_backup { get; set; }
    
        public virtual ObjectResult<Cement_Truck_Analysis_All_in_One_Result> Cement_Truck_Analysis_All_in_One(Nullable<System.DateTime> datefrom, Nullable<System.DateTime> dateto, string rpt_Type)
        {
            var datefromParameter = datefrom.HasValue ?
                new ObjectParameter("datefrom", datefrom) :
                new ObjectParameter("datefrom", typeof(System.DateTime));
    
            var datetoParameter = dateto.HasValue ?
                new ObjectParameter("dateto", dateto) :
                new ObjectParameter("dateto", typeof(System.DateTime));
    
            var rpt_TypeParameter = rpt_Type != null ?
                new ObjectParameter("rpt_Type", rpt_Type) :
                new ObjectParameter("rpt_Type", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Cement_Truck_Analysis_All_in_One_Result>("Cement_Truck_Analysis_All_in_One", datefromParameter, datetoParameter, rpt_TypeParameter);
        }
    
        public virtual ObjectResult<offline_TMS_product_created_count_Result> offline_TMS_product_created_count(Nullable<System.DateTime> datefrom, Nullable<System.DateTime> dateto)
        {
            var datefromParameter = datefrom.HasValue ?
                new ObjectParameter("datefrom", datefrom) :
                new ObjectParameter("datefrom", typeof(System.DateTime));
    
            var datetoParameter = dateto.HasValue ?
                new ObjectParameter("dateto", dateto) :
                new ObjectParameter("dateto", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<offline_TMS_product_created_count_Result>("offline_TMS_product_created_count", datefromParameter, datetoParameter);
        }
    
        public virtual ObjectResult<offline_TMS_Trip_Created_Details_Result> offline_TMS_Trip_Created_Details(Nullable<System.DateTime> datefrom, Nullable<System.DateTime> dateto)
        {
            var datefromParameter = datefrom.HasValue ?
                new ObjectParameter("datefrom", datefrom) :
                new ObjectParameter("datefrom", typeof(System.DateTime));
    
            var datetoParameter = dateto.HasValue ?
                new ObjectParameter("dateto", dateto) :
                new ObjectParameter("dateto", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<offline_TMS_Trip_Created_Details_Result>("offline_TMS_Trip_Created_Details", datefromParameter, datetoParameter);
        }
    
        public virtual ObjectResult<Offline_TMS_Trips_Hourly_Count_Result> Offline_TMS_Trips_Hourly_Count(Nullable<System.DateTime> datefrom)
        {
            var datefromParameter = datefrom.HasValue ?
                new ObjectParameter("datefrom", datefrom) :
                new ObjectParameter("datefrom", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Offline_TMS_Trips_Hourly_Count_Result>("Offline_TMS_Trips_Hourly_Count", datefromParameter);
        }
    
        public virtual ObjectResult<picked_cement_not_weighed_out_Result> picked_cement_not_weighed_out(Nullable<System.DateTime> datefrom, Nullable<System.DateTime> dateto)
        {
            var datefromParameter = datefrom.HasValue ?
                new ObjectParameter("datefrom", datefrom) :
                new ObjectParameter("datefrom", typeof(System.DateTime));
    
            var datetoParameter = dateto.HasValue ?
                new ObjectParameter("dateto", dateto) :
                new ObjectParameter("dateto", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<picked_cement_not_weighed_out_Result>("picked_cement_not_weighed_out", datefromParameter, datetoParameter);
        }
    
        public virtual ObjectResult<region_SAP_Truck_Analysis_All_in_One_xx_Result> region_SAP_Truck_Analysis_All_in_One_xx(Nullable<System.DateTime> datefrom, Nullable<System.DateTime> dateto)
        {
            var datefromParameter = datefrom.HasValue ?
                new ObjectParameter("datefrom", datefrom) :
                new ObjectParameter("datefrom", typeof(System.DateTime));
    
            var datetoParameter = dateto.HasValue ?
                new ObjectParameter("dateto", dateto) :
                new ObjectParameter("dateto", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<region_SAP_Truck_Analysis_All_in_One_xx_Result>("region_SAP_Truck_Analysis_All_in_One_xx", datefromParameter, datetoParameter);
        }
    
        public virtual ObjectResult<region_SAP_Truck_Analysis_Cement_Material_Result> region_SAP_Truck_Analysis_Cement_Material(Nullable<System.DateTime> datefrom, Nullable<System.DateTime> dateto)
        {
            var datefromParameter = datefrom.HasValue ?
                new ObjectParameter("datefrom", datefrom) :
                new ObjectParameter("datefrom", typeof(System.DateTime));
    
            var datetoParameter = dateto.HasValue ?
                new ObjectParameter("dateto", dateto) :
                new ObjectParameter("dateto", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<region_SAP_Truck_Analysis_Cement_Material_Result>("region_SAP_Truck_Analysis_Cement_Material", datefromParameter, datetoParameter);
        }
    
        public virtual ObjectResult<region_SAP_Truck_Analysis_Cement_Material_Dispatched_Result> region_SAP_Truck_Analysis_Cement_Material_Dispatched(Nullable<System.DateTime> datefrom, Nullable<System.DateTime> dateto)
        {
            var datefromParameter = datefrom.HasValue ?
                new ObjectParameter("datefrom", datefrom) :
                new ObjectParameter("datefrom", typeof(System.DateTime));
    
            var datetoParameter = dateto.HasValue ?
                new ObjectParameter("dateto", dateto) :
                new ObjectParameter("dateto", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<region_SAP_Truck_Analysis_Cement_Material_Dispatched_Result>("region_SAP_Truck_Analysis_Cement_Material_Dispatched", datefromParameter, datetoParameter);
        }
    
        public virtual ObjectResult<SAP_Truck_Analysis_All_in_One_xx_Result> SAP_Truck_Analysis_All_in_One_xx(Nullable<System.DateTime> datefrom, Nullable<System.DateTime> dateto)
        {
            var datefromParameter = datefrom.HasValue ?
                new ObjectParameter("datefrom", datefrom) :
                new ObjectParameter("datefrom", typeof(System.DateTime));
    
            var datetoParameter = dateto.HasValue ?
                new ObjectParameter("dateto", dateto) :
                new ObjectParameter("dateto", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SAP_Truck_Analysis_All_in_One_xx_Result>("SAP_Truck_Analysis_All_in_One_xx", datefromParameter, datetoParameter);
        }
    
        public virtual int SearchAllTables(string searchStr)
        {
            var searchStrParameter = searchStr != null ?
                new ObjectParameter("SearchStr", searchStr) :
                new ObjectParameter("SearchStr", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SearchAllTables", searchStrParameter);
        }
    
        public virtual ObjectResult<Summary_Count_Truck_Analysis_All_in_one_Result> Summary_Count_Truck_Analysis_All_in_one(Nullable<System.DateTime> datefrom, Nullable<System.DateTime> dateto)
        {
            var datefromParameter = datefrom.HasValue ?
                new ObjectParameter("datefrom", datefrom) :
                new ObjectParameter("datefrom", typeof(System.DateTime));
    
            var datetoParameter = dateto.HasValue ?
                new ObjectParameter("dateto", dateto) :
                new ObjectParameter("dateto", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Summary_Count_Truck_Analysis_All_in_one_Result>("Summary_Count_Truck_Analysis_All_in_one", datefromParameter, datetoParameter);
        }
    
        public virtual ObjectResult<Truck_Analysis_All_in_One_Result> Truck_Analysis_All_in_One(Nullable<System.DateTime> datefrom, Nullable<System.DateTime> dateto, string rpt_Type)
        {
            var datefromParameter = datefrom.HasValue ?
                new ObjectParameter("datefrom", datefrom) :
                new ObjectParameter("datefrom", typeof(System.DateTime));
    
            var datetoParameter = dateto.HasValue ?
                new ObjectParameter("dateto", dateto) :
                new ObjectParameter("dateto", typeof(System.DateTime));
    
            var rpt_TypeParameter = rpt_Type != null ?
                new ObjectParameter("rpt_Type", rpt_Type) :
                new ObjectParameter("rpt_Type", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Truck_Analysis_All_in_One_Result>("Truck_Analysis_All_in_One", datefromParameter, datetoParameter, rpt_TypeParameter);
        }
    
        public virtual ObjectResult<Truck_Analysis_All_in_One_dispatched_Result> Truck_Analysis_All_in_One_dispatched(Nullable<System.DateTime> datefrom, Nullable<System.DateTime> dateto)
        {
            var datefromParameter = datefrom.HasValue ?
                new ObjectParameter("datefrom", datefrom) :
                new ObjectParameter("datefrom", typeof(System.DateTime));
    
            var datetoParameter = dateto.HasValue ?
                new ObjectParameter("dateto", dateto) :
                new ObjectParameter("dateto", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Truck_Analysis_All_in_One_dispatched_Result>("Truck_Analysis_All_in_One_dispatched", datefromParameter, datetoParameter);
        }
    
        public virtual ObjectResult<Truck_Analysis_All_in_One_xx_Result> Truck_Analysis_All_in_One_xx(Nullable<System.DateTime> datefrom, Nullable<System.DateTime> dateto)
        {
            var datefromParameter = datefrom.HasValue ?
                new ObjectParameter("datefrom", datefrom) :
                new ObjectParameter("datefrom", typeof(System.DateTime));
    
            var datetoParameter = dateto.HasValue ?
                new ObjectParameter("dateto", dateto) :
                new ObjectParameter("dateto", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Truck_Analysis_All_in_One_xx_Result>("Truck_Analysis_All_in_One_xx", datefromParameter, datetoParameter);
        }
    
        public virtual ObjectResult<Truck_Analysis_All_in_One_xx_Audit_Result> Truck_Analysis_All_in_One_xx_Audit(Nullable<System.DateTime> datefrom, Nullable<System.DateTime> dateto)
        {
            var datefromParameter = datefrom.HasValue ?
                new ObjectParameter("datefrom", datefrom) :
                new ObjectParameter("datefrom", typeof(System.DateTime));
    
            var datetoParameter = dateto.HasValue ?
                new ObjectParameter("dateto", dateto) :
                new ObjectParameter("dateto", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Truck_Analysis_All_in_One_xx_Audit_Result>("Truck_Analysis_All_in_One_xx_Audit", datefromParameter, datetoParameter);
        }
    
        public virtual ObjectResult<Truck_Analysis_Material_only_Result> Truck_Analysis_Material_only(Nullable<System.DateTime> datefrom, Nullable<System.DateTime> dateto)
        {
            var datefromParameter = datefrom.HasValue ?
                new ObjectParameter("datefrom", datefrom) :
                new ObjectParameter("datefrom", typeof(System.DateTime));
    
            var datetoParameter = dateto.HasValue ?
                new ObjectParameter("dateto", dateto) :
                new ObjectParameter("dateto", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Truck_Analysis_Material_only_Result>("Truck_Analysis_Material_only", datefromParameter, datetoParameter);
        }
    
        public virtual ObjectResult<Trucks_Hourly_Count_Result> Trucks_Hourly_Count(Nullable<System.DateTime> datefrom)
        {
            var datefromParameter = datefrom.HasValue ?
                new ObjectParameter("datefrom", datefrom) :
                new ObjectParameter("datefrom", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Trucks_Hourly_Count_Result>("Trucks_Hourly_Count", datefromParameter);
        }
    
        public virtual int update_atc_type(string atc, string atcType)
        {
            var atcParameter = atc != null ?
                new ObjectParameter("atc", atc) :
                new ObjectParameter("atc", typeof(string));
    
            var atcTypeParameter = atcType != null ?
                new ObjectParameter("atcType", atcType) :
                new ObjectParameter("atcType", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("update_atc_type", atcParameter, atcTypeParameter);
        }
    
        public virtual ObjectResult<update_atc_type_atc_count_Result> update_atc_type_atc_count(string atc)
        {
            var atcParameter = atc != null ?
                new ObjectParameter("atc", atc) :
                new ObjectParameter("atc", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<update_atc_type_atc_count_Result>("update_atc_type_atc_count", atcParameter);
        }
    
        public virtual int close_old_open_trips()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("close_old_open_trips");
        }
    
        [DbFunction("DispatchDBEntities", "GetTransactionsDesc")]
        public virtual IQueryable<GetTransactionsDesc_Result> GetTransactionsDesc()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GetTransactionsDesc_Result>("[DispatchDBEntities].[GetTransactionsDesc]()");
        }
    
        public virtual int update_customer_name_after_sync()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("update_customer_name_after_sync");
        }
    }
}
