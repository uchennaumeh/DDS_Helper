﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


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
        CANCEL_ATC_GATE,
        FETCH_ATC_ATTEMPT_HELPER,
        ERR_REQUESTING_MORE_GROSS_WT_ADD_HELPER,
        ERR_REQUESTING_ZSTO_MOVEMENT_HELPER,
        UPDATE_DRIVER_HELPER,
        UPDATE_TRIP_HELPER,
        UPDATE_OFFLINE_TRIP_for_REPUSH_HELPER,
        REMOVE_CTES_TRIP_HELPER,
        UPDATE_ONLINE_TRIP_HELPER,
        UPDATE_DESTINATION_HELPER,
        UPDATE_RECEIVER_HELPER,
        CANCEL_TRIP_HELPER,
        GATE_REPORT_SPOOL_HELPER,
        TRIP_CREATE_REPORT_SPOOL_HELPER,
        TRIP_CLOSURE_REPORT_SPOOL_HELPER,
        POSTING_ERRORS_REPORT_SPOOL_HELPER,
        FETCH_ATC_ATTEMPT_FOR_SWAP_HELPER,
        ATC_SWAP_HELPER,
        ATC_SWAP_APPROVAL_HELPER,
        ATC_SWAP_REQUEST_HELPER,
        ADDITIONAL_GROSS_WT_REQUEST_SUBMITTED_HELPER,
        PLACED_REQUEST_FOR_ZSTO_MOVEMENT,
        MATERIAL_REPORT_SPOOL_HELPER,
        FETCH_ATC_ATTEMPT_FOR_ARCHIVE_HELPER,
        SEND_FOR_ARCHIVE_APPROVAL_HELPER,
        APPROVE_ARCHIVING_HELPER,
        REJECT_ARCHIVING_HELPER,
        REJECT_GWT_CHANGE_HELPER,
        REJECT_ZSTO_MOVE_HELPER,
        REJECT_TARE_WEIGHT_MODIFY_HELPER,
        REJECT_ZSTO_WEIGHT_MODIFY_HELPER,
        APPROVE_TARE_WEIGHT_MODIFY_HELPER,
        APPROVE_STO_WEIGHT_MODIFY_HELPER,
        FETCH_ATC_ATTEMPT_FOR_TARE_CHANGE_HELPER,
        STO_Weights_Fetch_HELPER,
        FETCH_ATC_ATTEMPT_FOR_MATERIAL_TO_ZSTO,
        FETCH_ATC_ATTEMPT_FOR_GROSS_CHANGE_HELPER,
        CHECK_FOR_DDS_APPROVAL_MAX_GROSS_WT_CHANGE,
        TARE_WEIGHT_CHANGE_REQUEST_HELPER,
        STO_WEIGHT_CHANGE_REQUEST_HELPER,
        ERROR_APPROVING_GROSS_WT_ADDITION_HELPER,
        ERROR_APPROVING_ZSTO_MOVEMENT_HELPER,
        APPROVED_GROSS_WT_ADDITION_HELPER,
        APPROVED_ZSTO_MOVEMENT,
    }
}