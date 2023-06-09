using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using DDS_Tz_Helper.Models;

namespace DDS_Tz_Helper.Business_Objects
{
    public class TransactionLogging
    {
        public bool RecordLog(DispatchDBEntities db, String atc_no, int user_id, ActivityType activity,
            String field_name, String field_value)
        {

            atc_session current_session = db.atc_session.OrderByDescending(a => a.session_start).FirstOrDefault();

            transaction_log new_transaction = new transaction_log();

            new_transaction.atc = atc_no;
            new_transaction.activity = activity.ToString();
            new_transaction.field_name = field_name;
            new_transaction.field_value = field_value;
            new_transaction.trx_date = DateTime.Now;
            new_transaction.user_id = user_id;
            if (current_session.status == "open")
            {
                new_transaction.session_id = current_session.Id;
            }

            try
            {
                db.transaction_log.Add(new_transaction);
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                throw;
            }

            return true;
        }
        //public string GenerateWaybillNo(DispatchDBEntities db)
        //{


        //    DocNo document_number = new DocNo();

        //    IEnumerable<string> document = document_number.Get("WB");

        //    if (document.ToList()[0] == "SUCCESS")
        //    {
        //        return dispatch_plant(db) + document.ToList()[1];
        //    }

        //    return "00000000";

        //    String waybill_no = dispatch_plant(db);
        //    waybill_no += "WB";

        //    String max_do = db.atc_transactions.Where(b => b.doc_type == "WAYBILL" && b.doc_number.StartsWith(waybill_no)).OrderByDescending(c => c.doc_number).Select(a => a.doc_number).Take(1).FirstOrDefault();

        //    if (max_do == null)
        //    {
        //        return waybill_no + "100001";
        //    }
        //    max_do = max_do.Replace(waybill_no, "");

        //    int max_id = Convert.ToInt32(max_do);
        //    if (max_id == 0)
        //    {
        //        max_id = 100001;
        //    }
        //    else
        //    {
        //        max_id++;
        //    }
        //    return waybill_no + max_id;
        //}
        //public string GeneratePGINo(ManualDispatchDatabaseEntities2 db)
        //{

        //    DocNo document_number = new DocNo();

        //    IEnumerable<string> document = document_number.Get("DN");

        //    if (document.ToList()[0] == "SUCCESS")
        //    {
        //        return dispatch_plant(db) + document.ToList()[1];
        //    }

        //    return "00000000";
        //    String pgi_no = dispatch_plant(db);
        //    pgi_no += "PG";

        //    String max_do = db.atc_transactions.Where(b => b.doc_type == "PGI" && b.doc_number.StartsWith(pgi_no)).OrderByDescending(c => c.doc_number).Select(a => a.doc_number).Take(1).FirstOrDefault();

        //    if (max_do == null)
        //    {
        //        return pgi_no + "100001";
        //    }
        //    max_do = max_do.Replace(pgi_no, "");

        //    int max_id = Convert.ToInt32(max_do);
        //    if (max_id == 0)
        //    {
        //        max_id = 100001;
        //    }
        //    else
        //    {
        //        max_id++;
        //    }
        //    return pgi_no + max_id;
        //}

        //public string GenerateWaybillNoSAP(ManualDispatchDatabaseEntities2 db) //10Digits
        //{

        //    DocNo document_number = new DocNo();

        //    IEnumerable<string> document = document_number.Get("WB");

        //    if (document.ToList()[0] == "SUCCESS")
        //    {
        //        return document.ToList()[1];
        //    }

        //    return "00000000";
        //    String delivery_no = dispatch_plant(db);
        //    delivery_no += "DO";
        //    delivery_no = "";
        //    String max_do = db.transaction_data.OrderByDescending(c => c.tmp_waybill_no).Select(a => a.tmp_waybill_no).Take(1).FirstOrDefault();

        //    if (max_do == null)
        //    {
        //        return delivery_no + "1020600001";
        //    }
        //    //max_do = max_do.Replace(delivery_no, "");

        //    int max_id = Convert.ToInt32(max_do);
        //    if (max_id == 0)
        //    {
        //        max_id = 100001;
        //    }
        //    else
        //    {
        //        max_id++;
        //    }
        //    return delivery_no + max_id;

        //}
        //public string GenerateDeliveryNoSAP(ManualDispatchDatabaseEntities2 db) //10Digits
        //{
        //    DocNo document_number = new DocNo();

        //    IEnumerable<string> document = document_number.Get("DN");

        //    if (document.ToList()[0] == "SUCCESS")
        //    {
        //        return document.ToList()[1];
        //    }

        //    return "00000000";

        //    String delivery_no = dispatch_plant(db);
        //    delivery_no += "DO";
        //    delivery_no = "";
        //    String max_do = db.transaction_data.OrderByDescending(c => c.tmp_delivery_no).Select(a => a.tmp_delivery_no).Take(1).FirstOrDefault();

        //    if (max_do == null)
        //    {
        //        return delivery_no + "1010400001";
        //    }
        //    //max_do = max_do.Replace(delivery_no, "");

        //    int max_id = Convert.ToInt32(max_do);
        //    if (max_id == 0)
        //    {
        //        max_id = 100001;
        //    }
        //    else
        //    {
        //        max_id++;
        //    }
        //    return delivery_no + max_id;

        //}
        //public string GenerateDeliveryNo(ManualDispatchDatabaseEntities2 db) //10Digits
        //{

        //    DocNo document_number = new DocNo();

        //    IEnumerable<string> document = document_number.Get("DN");

        //    if (document.ToList()[0] == "SUCCESS")
        //    {
        //        return document.ToList()[1];
        //    }

        //    return "00000000";

        //    String delivery_no = dispatch_plant(db);
        //    delivery_no += "DO";

        //    String max_do = db.atc_transactions.Where(b => b.doc_type == "DELIVERY" && b.doc_number.StartsWith(delivery_no)).OrderByDescending(c => c.doc_number).Select(a => a.doc_number).Take(1).FirstOrDefault();

        //    if (max_do == null)
        //    {
        //        return delivery_no + "100001";
        //    }
        //    max_do = max_do.Replace(delivery_no, "");

        //    int max_id = Convert.ToInt32(max_do);
        //    if (max_id == 0)
        //    {
        //        max_id = 100001;
        //    }
        //    else
        //    {
        //        max_id++;
        //    }
        //    return delivery_no + max_id;

        //}
        //public string GenerateTripNo(ManualDispatchDatabaseEntities2 db) //10Digits
        //{

        //    String trip_no = dispatch_plant(db);
        //    trip_no += "TR";
        //    DocNo document_number = new DocNo();

        //    IEnumerable<string> document = document_number.Get("TRP");

        //    if (document.ToList()[0] == "SUCCESS")
        //    {
        //        return trip_no + document.ToList()[1];
        //    }

        //    return "00000000";


        //    String max_do = db.atc_transactions.Where(b => b.doc_type == "TRIP_CREATE" && b.doc_number.StartsWith(trip_no)).OrderByDescending(c => c.doc_number).Select(a => a.doc_number).Take(1).FirstOrDefault();

        //    if (max_do == null)
        //    {
        //        return trip_no + "100001";
        //    }
        //    max_do = max_do.Replace(trip_no, "");

        //    int max_id = Convert.ToInt32(max_do);
        //    if (max_id == 0)
        //    {
        //        max_id = 100001;
        //    }
        //    else
        //    {
        //        max_id++;
        //    }
        //    return trip_no + max_id;
        //}

        //private String dispatch_plant(ManualDispatchDatabaseEntities2 db)
        //{
        //    String plant_code_db;
        //    String plant_code;

        //    plant_code_db = db.system_setting.Select(a => a.plant_code).FirstOrDefault();
        //    plant_code = plant_code_db.Split(',')[0];

        //    return plant_code;
        //}
    }


    public enum ActivityType
    {
        WB_IN,
        PICKING,
        WB_OUT,
        GATE_ENTRY,
        GATE_EXIT,
        DISPATCH,
        RECONCILE,
        APPROVAL_START,
        APPROVAL_STOP,
        ACTIVATE_DISPATCH,
        DEACTIVATE_DISPATCH,
        SYNC_START,
        SYNC_RECORDS,
        SYNC_COMPLETE,
        SYNC_ERROR,
        LOGIN,
        LOGOUT,
        CLOSE_TRIP,
        CREATE_TRIP,
        APPROVE_TRIP,
        DISPATCH_TRIP,
        FUEL_POSTING_TRIP,
        PRINT_FUEL_VOUCHER,
        CREATE_FUEL_ITEM,
        DELETE_TRIP,
        USER_REQUEST,
        USER_COMPLETE_REQUEST,
        REPUSH,
        MODIFIED,
        NEW_ENTRY,
        LOCK_OUT,
        WEIGHT_SWITCH,
        WEIGHT_MODIFICATION,
        GROSS_MODIFICATION,
        PICKING_MODIFICATION,
        ATC_MODIFICATION,
        QUEUE_ENTRY,
        QUEUE_EXIT,
        QUEUE_PROCESSING,
        ABOVE_TOLERANCE,
        BELOW_TOLERANCE,
        SIMILAR_GROSS,
        OFFLINE_TRANSACTION,
        WEIGHT_VIOLATION_BYPASS,
        APPROVED_GROSS_CHANGE,
        LOGIN_HELPER,
        LOGOUT_HELPER,
        ATC_GRADE_CHANGE_HELPER,
        MODIFY_ENTRIES_HELPER,
        FETCH_ATC_ATTEMPT_HELPER,
        UPDATE_DRIVER_HELPER,
        UPDATE_TRIP_HELPER,
        UPDATE_OFFLINE_TRIP_for_REPUSH_HELPER,
        REMOVE_CTES_TRIP_HELPER,
        UPDATE_ONLINE_TRIP_HELPER,
        UPDATE_DESTINATION_HELPER,
        UPDATE_RECEIVER_HELPER,
    }
}