using DDS_Tz_Helper.Business_Objects;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Security.Cryptography;
using System.Text;
using System.Globalization;
using System.Diagnostics;
using System.Net;
using System.Drawing;
using System.DirectoryServices;

namespace DDS_Tz_Helper.Controllers
{
    public class HomeController : Controller
    {
        private DispatchDBEntities db = new DispatchDBEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TripCheck()
        {
            return View();
        }
        public ActionResult RepairInbound()
        {
            return View();
        }

        public ActionResult ATCSwapRequests()
        {
            
            return View();
        }

        public ActionResult GrossWeightModif()
        {

            return View();
        }

        public ActionResult ApproveGWT(int? id)
        {
            ViewBag.RecordId = id;
            return View();
        }

        public ActionResult GrossWTChangeApprove()
        {

            return View();
        }

        public ActionResult ATCArchiveRequests()
        {

            return View();
        }

        public ActionResult ApproveTareAlternative()
        {

            return View();
        }

        public ActionResult ModifyTareWeightRequests(int? id)
        {
            ViewBag.RecordId = id;
            return View();
        }

        public ActionResult ApproveTareModify(int? id)
        {
            //ession["recordId"] = id;
            ViewBag.RecordId = id;
            return View();
        }

        public ActionResult ApproveZSTOMovement(int? id)
        {
            //ession["recordId"] = id;
            ViewBag.RecordId = id;
            return View();
        }



        public ActionResult ApproveSwap(int? id)
        {
            //ession["recordId"] = id;
            ViewBag.RecordId = id;
            return View();
        }

        public ActionResult ApproveArchive(int? id)
        {
            //ession["recordId"] = id;
            ViewBag.RecordId = id;
            return View();
        }

        public ActionResult GateReport()
        {
            return View();
        }

        public ActionResult ModifyTareWeight()
        {
            return View();
        }

        public ActionResult MaterialReport()
        {
            return View();
        }

        public ActionResult ATCArchive()
        {
            return View();
        }

        public ActionResult NotAllowed()
        {
            return View();
        }

        public ActionResult TripReport()
        {
            return View();
        }


        public ActionResult ReportGate()
        {
            return View();
        }

        public ActionResult ATCSwap()
        {
            return View();
        }

        public ActionResult PostingErrorsReport()
        {
            return View();
        }

        public ActionResult STOPostingErrorReport()
        {
            return View();
        }

        public ActionResult LogOff()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Default()
        {
            return View();
        }

        public ActionResult CancelTrip()
        {
            return View();
        }

        public ActionResult NewHome()
        {
            return View();
        }

        public ActionResult STO_Modify()
        {
            return View();
        }

        public ActionResult Gate_Modify()
        {
            return View();
        }

        public ActionResult Remove_TripID()
        {
            return View();
        }
        public ActionResult Welcome()
        {
            return View();
        }

        public ActionResult Receiver()
        {
            return View();
        }

        public ActionResult AddOnlineTrip()
        {
            return View();
        }

        public ActionResult MaterialToZSTO()
        {
            return View();
        }

        public ActionResult MAterialToZSTORequests()
        {
            return View();
        }

        public ActionResult Driver()
        {
            return View();
        }

        public ActionResult Trip_Validate()
        {
            return View();
        }

        public ActionResult Offline_Repush()
        {
            return View();
        }

        public ActionResult Destination()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DoChangeATCGrade(String __RequestVerificationToken, String atc, String grade)
        {
            
            atc_datax atc_Datax = new atc_datax();

            string atc_actual = atc;
            string grade_actual = grade;
            string msge;
            decimal d;


            try
            {
                var atcDetails = db.atc_data.Where(x => x.sales_doc_number == atc && x.item_number != null).FirstOrDefault();
                var transDataDetals = db.transaction_data.Where(a => a.sales_doc_number == atc && a.gross_time != null).FirstOrDefault();

                if (transDataDetals != null)
                {
                    atc_Datax.msg = "ATC has been USED and WEIGHED-OUT!!!";
                    atc_Datax.status = false;
                    return Json(atc_Datax);

                }

                if (atcDetails == null)
                {
                    atc_Datax.msg = "Wrong ATC Entered";
                    atc_Datax.status = false;
                    return Json(atc_Datax);
                }

                var d2 = atcDetails.sales_doc_number;



                if (atcDetails != null)
                {
                    atcDetails.sales_order_item = grade;
                    db.Entry(atcDetails).State = EntityState.Modified;
                    db.SaveChanges();

                    int user_id = int.Parse(Session["user_id"].ToString());
                   
                    TransactionLogging TRX_LOG = new TransactionLogging();
                    bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.ATC_GRADE_CHANGE_HELPER, "ATC_GRADE_CHANGE : ", DateTime.Now.ToString());



                    atc_Datax.msg = "ATC Updated Successfully";
                    atc_Datax.status = true;
                    return Json(atc_Datax);


                }
                else
                {
                    atc_Datax.msg = "Be sure you have entered a correct ATC";
                    atc_Datax.status = false;
                    return Json(atc_Datax);
                }

            }
            catch (Exception ex)
            {

                msge = ex.Message;
                atc_Datax.msg = "Error occured somewhere";
                atc_Datax.status = false;
                atc_Datax.excptn = ex.ToString();
                return Json(atc_Datax);
            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DoModifyEntries(String __RequestVerificationToken, String atc, String driver, String truckNum, String stoLoc, String transporter, String sender, String destination, String trip_id, String receiver)
        {
            sto_datax sto_Datax = new sto_datax();



            var poDetails = db.sto_transaction_data.Where(x => x.po_doc_number == atc && x.sync_status == false).FirstOrDefault();
            if (poDetails == null)
            {
                sto_Datax.status = false;
                sto_Datax.msg = "Wrong PO or PO entered";
                return Json(sto_Datax);
            }
            else
            {
                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.MODIFY_ENTRIES_HELPER,  poDetails.transporter + " " + "Old Trip ID: " + poDetails.trip_id + " " + "Old Destination: " + poDetails.destination + " " +
                    "Old Driver:" + " " + poDetails.driver + " " + "Old Sender:" + " " + poDetails.sender + " " + "Old Storage loc:" + " " + poDetails.loc + " " + "Old Vehicle:" + " " + poDetails.vehicle + " " +
                    " " + "Old Transporter:" + " " + poDetails.transporter_name + " " + "Old Receiver:" + " " + poDetails.receiver + " " + "MODIFY_STO_ENTRIES : OLD DETAILS", DateTime.Now.ToString());

                poDetails.transporter = transporter;
                poDetails.trip_id = trip_id;
                poDetails.destination = destination;
                poDetails.driver = driver;
                poDetails.sender = sender;
                poDetails.loc = stoLoc;
                poDetails.vehicle = truckNum;
                poDetails.transporter_name = transporter;
                poDetails.receiver = receiver;

                
                db.Entry(poDetails).State = EntityState.Modified;
                db.SaveChanges();
                
                status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.MODIFY_ENTRIES_HELPER, transporter + " " + trip_id + " " + destination + " " + driver + " " + sender + " " +
                    stoLoc + " " +
                    truckNum + " " + transporter + " " + receiver + " " + "MODIFY_ENTRIES : NEW DETAILS", DateTime.Now.ToString());

                sto_Datax.msg = "UPDATED SUCCESSFULLY!!!";
                sto_Datax.status = true;
                return Json(sto_Datax);
            }

        }


        



        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DoATCSwap(String __RequestVerificationToken, String atc, String newATC, String trip_id, String reason)
        {

            if (Session["TrxSwapPicking"].ToString() == "1")
            {
                Transaction_Datax trx_Datax = new Transaction_Datax();
                var trxDetails = db.transaction_data.Where(a => a.sales_doc_number == atc && a.gross == null && a.sales_type == "BAGS").FirstOrDefault();
                var atcDetails = db.atc_data.Where(x => x.sales_doc_number == newATC && x.used == null && x.sales_type == "BAGS").FirstOrDefault();
                var atcDetailsold = db.atc_data.Where(x => x.sales_doc_number == atc && x.used != null && x.sales_type == "BAGS").FirstOrDefault();
                int user_id = int.Parse(Session["user_id"].ToString());

                if (atcDetails == null)
                {
                    trx_Datax.status = false;
                    trx_Datax.msg = "Wrong ATC or ATC is already used";
                    return Json(trx_Datax);
                }
                var atcQty = atcDetails.cumm_order_qty;
                

                transaction_data_archive tda = new transaction_data_archive();
                tda.sales_doc_number = newATC;
                tda.trip_id = trip_id;
                tda.vehicle = atc;
                tda.shp_point = user_id.ToString();
                tda.operator_weighin = "ATC SWAP REQUEST";
                tda.sales_type = "BAGS";
                tda.tare_time = DateTime.Now;
                tda.sales_doc_type = "PENDING";
                tda.seal = reason;
          

                db.transaction_data_archive.Add(tda);
                db.SaveChanges();

                

                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.ATC_SWAP_REQUEST_HELPER, newATC + " " + reason, DateTime.Now.ToString());

                trx_Datax.msg = "SWAP REQUEST SUBMITTED SUCCESSFULLY; APPROVAL IS PENDING!";
                trx_Datax.status = true;
                return Json(trx_Datax);
            }
            else
            {
                Transaction_Datax trx_Datax = new Transaction_Datax();
                var trxDetails = db.transaction_data.Where(a => a.sales_doc_number == atc && a.gross == null).FirstOrDefault();
                var atcDetails = db.atc_data.Where(x => x.sales_doc_number == newATC && x.used == null && x.sales_type == "BAGS").FirstOrDefault();
                var atcDetailsold = db.atc_data.Where(x => x.sales_doc_number == atc && x.used != null && x.sales_type == "BAGS").FirstOrDefault();
                int user_id = int.Parse(Session["user_id"].ToString());

                if (atcDetails == null)
                {
                    trx_Datax.status = false;
                    trx_Datax.msg = "New ATC for the swap is either used or incorrect";
                    return Json(trx_Datax);
                }
                var atcQty = atcDetails.del_qty;
                

                transaction_data_archive tda = new transaction_data_archive();
                tda.sales_doc_number = newATC;
                tda.trip_id = trip_id;
                tda.vehicle = atc;
                tda.shp_point = user_id.ToString();
                tda.operator_weighin = "ATC SWAP REQUEST";
                tda.sales_type = "BAGS";
                tda.tare_time = DateTime.Now;
                tda.sales_doc_type = "PENDING";
                tda.seal = reason;
                //tda.destination = destination;

                db.transaction_data_archive.Add(tda);
                db.SaveChanges();



                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.ATC_SWAP_REQUEST_HELPER, newATC + " " + reason, DateTime.Now.ToString());

                trx_Datax.msg = "SWAP REQUEST SUBMITTED SUCCESSFULLY; APPROVAL IS PENDING!";
                trx_Datax.status = true;
                return Json(trx_Datax);
            }


        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DoModifyEntriesGate(String __RequestVerificationToken, String atc, String driver, String truckNum, String customer, String license, String transporter, String destination, String statusDB, String transType, String shippingPoint, String product, String parentSO)
        {
            Gate_Datax gate_Datax = new Gate_Datax();



            var atcDetails = db.gateaccess_info.Where(x => x.atc_no == atc && x.status != "Weigh-Out Successful." && x.status != "CANCELLED").FirstOrDefault();
            if (atcDetails == null)
            {
                gate_Datax.status = false;
                gate_Datax.msg = "Wrong ATC or ATC already weighed in";
                return Json(gate_Datax);
            }
            else
            {
                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.MODIFY_ENTRIES_HELPER, atcDetails.transporter + " " + atcDetails.status + " " + atcDetails.truck_no + " " +
                    atcDetails.transaction_type + " " + atcDetails.shipping_point + " " + atcDetails.fleet_no + " " + atcDetails.license + " " + atcDetails.customer + " " + atcDetails.destination + " " +
                    atcDetails.driver + " " + atcDetails.parent_so + " " + "MODIFY_ENTRIES_GATE: " + " " + "OLD DETAILS" , DateTime.Now.ToString());


                atcDetails.transporter = transporter;
                atcDetails.status = statusDB;
                atcDetails.truck_no = truckNum;
                atcDetails.transaction_type = transType;
                atcDetails.shipping_point = shippingPoint;
                atcDetails.fleet_no = product;
                atcDetails.license = license;
                atcDetails.customer = customer;
                atcDetails.destination = destination;
                atcDetails.driver = driver;
                atcDetails.parent_so = parentSO;




                //poDetails.driver = driver;
                db.Entry(atcDetails).State = EntityState.Modified;
                db.SaveChanges();
                
                status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.MODIFY_ENTRIES_HELPER, transporter + " " + statusDB + " " + truckNum + " " + transType + " " +
                    shippingPoint + " " + product + " " + license + " " + customer + " " + destination + " " + driver + " " +
                    parentSO + " " + "MODIFY_ENTRIES GATE: " + " " + "NEW DETAILS", DateTime.Now.ToString());



                gate_Datax.msg = "UPDATED SUCCESSFULLY!!!";
                gate_Datax.status = true;
                return Json(gate_Datax);
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DoModifyEntriesGateCancel(String __RequestVerificationToken, String atc)
        {
            Gate_Datax gate_Datax = new Gate_Datax();



            var atcDetails = db.gateaccess_info.Where(x => x.atc_no == atc && x.status != "Weigh-Out Successful." && x.status != "CANCELLED").FirstOrDefault();
            if (atcDetails == null)
            {
                gate_Datax.status = false;
                gate_Datax.msg = "Wrong ATC or ATC already weighed in";
                return Json(gate_Datax);
            }
            else
            {

                atcDetails.atc_no = "C" + atc.Substring(1);

                atcDetails.status = "CANCELLED";
                
                db.Entry(atcDetails).State = EntityState.Modified;
                db.SaveChanges();

                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.CANCEL_ATC_GATE, "CANCEL GATE ATC: ", DateTime.Now.ToString());

                gate_Datax.msg = "ATC CANCELLED SUCCESSFULLY!!!";
                gate_Datax.status = true;
                return Json(gate_Datax);
            }

        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DoModifyGrossWeightRequest(String __RequestVerificationToken, String atc, String approvedBy, String requestedBy, String stoLoc, String transporter, String sender, String dateOfApproval, String oldGrosss, String receiver, Decimal newWT, String numberOfBags, Decimal net, 
            String reason, String ActualPicking, String ActualWeighin, String ActualDestination, String ActualTransType, String ActualDriverName, String ActualPlateNum)
        {
            sto_datax sto_Datax = new sto_datax();
            int user_id = int.Parse(Session["user_id"].ToString());
            Transaction_Datax trx_Datax = new Transaction_Datax();
            transaction_data_archive tda = new transaction_data_archive();
            try
            {
                tda.sales_doc_number = atc;
                tda.shp_point = user_id.ToString();
                tda.operator_weighin = "ADDITIONAL GROSS WEIGHT MODIF REQUEST";
                tda.sales_type = "BAGS";
                tda.tare_time = DateTime.Now;
                tda.sales_doc_type = "PENDING";
                tda.seal = reason;
       
                tda.nett = net;
                tda.loc = newWT.ToString();
                
                tda.driver = ActualDriverName;
                tda.vehicle = ActualPlateNum;
                tda.migo_details = ActualTransType;
                tda.destination = ActualDestination;
                tda.bin_1 = ActualWeighin;
                tda.bin_2 = ActualPicking;


                db.transaction_data_archive.Add(tda);
                db.SaveChanges();



                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.ADDITIONAL_GROSS_WT_REQUEST_SUBMITTED_HELPER, atc + " " + reason + " " + newWT, DateTime.Now.ToString());

                trx_Datax.msg = "ADDITIONAL GROSS WEIGHT CHANGE REQUEST SUBMITTED SUCCESSFULLY; APPROVAL IS PENDING!";
                trx_Datax.status = true;
                return Json(trx_Datax);
            }
            catch(Exception ex)
            {
               
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.ERR_REQUESTING_MORE_GROSS_WT_ADD_HELPER, "ERR_REQUESTING_MORE_GROSS_WT_ADD_HELPER : ", DateTime.Now.ToString());
               
                sto_Datax.msg = "Error occured somewhere";
                sto_Datax.status = false;
                sto_Datax.excptn = ex.ToString();

                return Json(sto_Datax);
            }
            


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DoMoveToZSTORequest(String __RequestVerificationToken, String atc, String drvName, String weiginOp, String transact_Type, String firstWeight, String weighInDate, String destination, String sender, String receiver,
           String reason, String sto_loc, String trip_id, String Transporter_name, String vehiclex, String waybill)
        {
            sto_datax sto_Datax = new sto_datax();
            int user_id = int.Parse(Session["user_id"].ToString());
            Transaction_Datax trx_Datax = new Transaction_Datax();
            transaction_data_archive tda = new transaction_data_archive();
            string oldATCx = atc.PadLeft(10, '0');


            var TradeTransType = "";
            try
            {
                tda.sales_doc_number = oldATCx;
                tda.shp_point = user_id.ToString();
                tda.operator_weighin = "MOVE FROM MATERIAL TO ZSTO";
                if (transact_Type == "InboundZSTO") {
                    TradeTransType = "PO_IN";
                    tda.sales_type = TradeTransType;
                    tda.gross = Decimal.Parse(firstWeight);                  
                    tda.gross_time = DateTime.ParseExact(weighInDate, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

                }
                else
                {
                    TradeTransType = "PO_OUT";
                    tda.sales_type = TradeTransType;
                    tda.nett = Decimal.Parse(firstWeight);
                    tda.sap_post_time = DateTime.ParseExact(weighInDate, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

                }

               
                
                tda.tare_time = DateTime.Now;
                tda.sales_doc_type = "PENDING";
                tda.seal = reason;
                tda.transporter = Transporter_name;
                tda.transporter_name = Transporter_name;
                tda.destination = destination;
                tda.loc = sto_loc;
                tda.driver = drvName;
                tda.bin_1 = weiginOp;
                tda.vehicle = vehiclex;
                //tda.sync_status = false;
                tda.tmp_waybill_no = waybill;
                tda.parent_sales_order = atc;
                tda.bin_2 = Session["sales_code_type"].ToString();
                tda.sender = sender;
                tda.trip_id = trip_id;




                db.transaction_data_archive.Add(tda);
                db.SaveChanges();


                var stoDataDetails = db.sto_data.Where(x => x.used == null && x.delivery_number == oldATCx || x.delivery_number_out == oldATCx).FirstOrDefault();
                try
                {
                    //stoDataDetails.used = true;
                    //db.Entry(stoDataDetails).State = EntityState.Modified;
                    //db.SaveChanges();
                }
                catch(Exception ex)
                {

                }
               


                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.PLACED_REQUEST_FOR_ZSTO_MOVEMENT, atc + " " + reason, DateTime.Now.ToString());

                trx_Datax.msg = "ZSTO SWAP SUBMITTED SUCCESSFULLY; APPROVAL IS PENDING!";
                trx_Datax.status = true;
                return Json(trx_Datax);
            }
            catch (Exception ex)
            {

                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, oldATCx, user_id, ActivityType.ERR_REQUESTING_ZSTO_MOVEMENT_HELPER, "ERR_REQUESTING_ZSTO_MOVEMENT_HELPER : ", DateTime.Now.ToString());

                sto_Datax.msg = "Error occured somewhere";
                sto_Datax.status = false;
                sto_Datax.excptn = ex.ToString();

                return Json(sto_Datax);
            }



        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DoModifyGrossWeight(String __RequestVerificationToken, String atc, String driver, String truckNum, String stoLoc, String transporter, String sender, String destination, String trip_id, String receiver, String newWT, String numberOfBags, String net)
        {
            sto_datax sto_Datax = new sto_datax();



            var poDetails = db.sto_transaction_data.Where(x => x.po_doc_number == atc && x.sync_status == false).FirstOrDefault();
            if (poDetails == null)
            {
                sto_Datax.status = false;
                sto_Datax.msg = "Wrong PO or PO entered";
                return Json(sto_Datax);
            }
            else
            {
                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.MODIFY_ENTRIES_HELPER, poDetails.transporter + " " + "Old Trip ID: " + poDetails.trip_id + " " + "Old Destination: " + poDetails.destination + " " +
                    "Old Driver:" + " " + poDetails.driver + " " + "Old Sender:" + " " + poDetails.sender + " " + "Old Storage loc:" + " " + poDetails.loc + " " + "Old Vehicle:" + " " + poDetails.vehicle + " " +
                    " " + "Old Transporter:" + " " + poDetails.transporter_name + " " + "Old Receiver:" + " " + poDetails.receiver + " " + "MODIFY_STO_ENTRIES : OLD DETAILS", DateTime.Now.ToString());

                poDetails.transporter = transporter;
                poDetails.trip_id = trip_id;
                poDetails.destination = destination;
                poDetails.driver = driver;
                poDetails.sender = sender;
                poDetails.loc = stoLoc;
                poDetails.vehicle = truckNum;
                poDetails.transporter_name = transporter;
                poDetails.receiver = receiver;
                
                db.Entry(poDetails).State = EntityState.Modified;
                db.SaveChanges();
                
                status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.MODIFY_ENTRIES_HELPER, transporter + " " + trip_id + " " + destination + " " + driver + " " + sender + " " +
                    stoLoc + " " +
                    truckNum + " " + transporter + " " + receiver + " " + "MODIFY_ENTRIES : NEW DETAILS", DateTime.Now.ToString());

                sto_Datax.msg = "UPDATED SUCCESSFULLY!!!";
                sto_Datax.status = true;
                return Json(sto_Datax);
            }

        }






        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult checkLogin(String loginVAr)
        {
            Loginx loginx = new Loginx();
            try
            {
                if (Session["LoginStatus"].ToString() == "LoggedIn")
                {
                    loginx.status = true;
                    return Json(loginx);
                }
                else
                {
                    loginx.status = false;
                    return Json(loginx);
                }
            }
            catch (Exception ex)
            {
                loginx.status = false;
                return Json(loginx);

            }


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DoFetchATCDetails(String __RequestVerificationToken, String atc)
        {

            sto_datax sto_Datax = new sto_datax();

            string atc_actual = atc;
            string msge;



            try
            {
                var poDetails = db.sto_transaction_data.Where(x => x.po_doc_number == atc && x.sync_status == false).FirstOrDefault();
                if (poDetails == null)
                {

                    int user_id = int.Parse(Session["user_id"].ToString());
                    TransactionLogging TRX_LOG = new TransactionLogging();
                    bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.FETCH_ATC_ATTEMPT_HELPER, "FETCH_ATC_ATTEMPT : ", DateTime.Now.ToString());

                    sto_Datax.status = false;
                    sto_Datax.msg = "Wrong PO or PO already weighed-out and Pushed to SAP";
                    return Json(sto_Datax);
                }
                else
                {
                    sto_Datax.msg = "";
                    sto_Datax.status = true;
                    sto_Datax.transporter = poDetails.transporter_name;
                    sto_Datax.trip_id = poDetails.trip_id;
                    sto_Datax.destination = poDetails.destination;
                    sto_Datax.driverName = poDetails.driver;
                    sto_Datax.sender = poDetails.sender;
                    sto_Datax.stoLoc = poDetails.loc;
                    sto_Datax.truckNumber = poDetails.vehicle;
                    sto_Datax.receiver = poDetails.receiver;
                    sto_Datax.atc = atc;

                    int user_id = int.Parse(Session["user_id"].ToString());
                    TransactionLogging TRX_LOG = new TransactionLogging();
                    bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.FETCH_ATC_ATTEMPT_HELPER, "FETCH_ATC_ATTEMPT : ", DateTime.Now.ToString());

                    return Json(sto_Datax);
                }


            }
            catch (Exception ex)
            {
                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.FETCH_ATC_ATTEMPT_HELPER, "FETCH_ATC_ATTEMPT : ", DateTime.Now.ToString());

                msge = ex.Message;
                sto_Datax.msg = "Error occured somewhere";
                sto_Datax.status = false;
                sto_Datax.excptn = ex.ToString();

                return Json(sto_Datax);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DoFetchATCDetailsForSwap(String __RequestVerificationToken, String atc)
        {
            Session["TrxSwapPicking"] = 0;
            
            Transaction_Datax trx_Datax = new Transaction_Datax();
            trade_dto tradex = new trade_dto();
            Sto_Transaction_Datax stdx = new Sto_Transaction_Datax();

            string atc_actual = atc;
            string msge;

            var tradeDetails = db.Trades.Where(a => a.sparenum2.ToString() == atc && a.seconddatetime == null).FirstOrDefault();
            if (tradeDetails != null)
            {
                tradex.msg = "";
                tradex.status = true;
                tradex.statusPicking = false;
                tradex.atc = tradeDetails.sparenum2.ToString();
                tradex.trip_ID = "0000000000";

               
                ViewBag.DestinationTable = "trade";

                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.FETCH_ATC_ATTEMPT_FOR_SWAP_HELPER, "FETCH ATC ATTEMPT FOR SWAP : ", DateTime.Now.ToString());

                return Json(tradex);

            }            
            var stoTrxDataDetaisOut = db.sto_transaction_data.Where(x => x.po_doc_number == atc && x.gross == null && x.sales_type == "PO_OUT").FirstOrDefault();
            if(stoTrxDataDetaisOut != null)
            {
                //DO STO Outbound swap swap
                stdx.msg = "";
                stdx.status = true;

                if (stoTrxDataDetaisOut.picking_time != null)
                {
                    stdx.statusPicking = true;
                    Session["TrxSwapPicking"] = 1;

                }
                else
                {
                    stdx.statusPicking = false;
                    Session["TrxSwapPicking"] = 0;
                }
                stdx.statusPicking = false;
                stdx.atc = stoTrxDataDetaisOut.po_doc_number;
                stdx.trip_id = stoTrxDataDetaisOut.trip_id;
                
                ViewBag.DestinationTable = "sto_trx_data";

                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.FETCH_ATC_ATTEMPT_FOR_SWAP_HELPER, "FETCH ATC ATTEMPT FOR SWAP : ", DateTime.Now.ToString());

                return Json(stdx);
                
            }
            var stoTrxDataDetaisIn = db.sto_transaction_data.Where(x => x.po_doc_number == atc && x.tare == null && x.sales_type == "PO_IN").FirstOrDefault();
            if (stoTrxDataDetaisOut != null)
            {
                //DO STO Inbound swap swap
                stdx.msg = "";
                stdx.status = true;

                if (stoTrxDataDetaisIn.picking_time != null)
                {
                    stdx.statusPicking = true;
                    Session["TrxSwapPicking"] = 1;

                }
                else
                {
                    stdx.statusPicking = false;
                    Session["TrxSwapPicking"] = 0;
                }
                stdx.statusPicking = false;
                stdx.atc = stoTrxDataDetaisIn.po_doc_number;
                stdx.trip_id = stoTrxDataDetaisIn.trip_id;

                ViewBag.DestinationTable = "sto_trx_data";

                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.FETCH_ATC_ATTEMPT_FOR_SWAP_HELPER, "FETCH ATC ATTEMPT FOR SWAP : ", DateTime.Now.ToString());

                return Json(stdx);
                //return
            }
            var atcDetails = db.transaction_data.Where(x => x.sales_doc_number == atc && x.gross == null && x.sales_type == "BAGS").FirstOrDefault();

            if (atcDetails == null)
            {
                    int user_id = int.Parse(Session["user_id"].ToString());
                    TransactionLogging TRX_LOG = new TransactionLogging();
                    bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.FETCH_ATC_ATTEMPT_HELPER, "FETCH ATC ATTEMPT FOR SWAP : ", DateTime.Now.ToString());

                    trx_Datax.status = false;
                    trx_Datax.msg = "Wrong ATC or ATC is already weighed-out";
                    return Json(trx_Datax);
             
            }
            else
            {
                trx_Datax.msg = "";
                trx_Datax.status = true;
                if (atcDetails.picking_time != null)
                {
                    trx_Datax.statusPicking = true;
                    Session["TrxSwapPicking"] = 1;

                }
                trx_Datax.atc = atc;
                trx_Datax.trip_id = atcDetails.trip_id;

                //Session["table"] = "trx_data";
                ViewBag.DestinationTable = "trx_data";


                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.FETCH_ATC_ATTEMPT_FOR_SWAP_HELPER, "FETCH ATC ATTEMPT FOR SWAP : ", DateTime.Now.ToString());

                return Json(trx_Datax);
            }


            

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DoActualATCSwap(String __RequestVerificationToken, int id, String comment)
        {
           
            Session["TrxSwapPicking"] = 0;
            Transaction_Datax trx_Datax = new Transaction_Datax();
            trade_dto tradex = new trade_dto();
            Sto_Transaction_Datax stdx = new Sto_Transaction_Datax();

            string msge;


            //TRADE
            var tdaDetails = db.transaction_data_archive.Where(a => a.id == id && a.sales_doc_type == "PENDING").FirstOrDefault();
            if (tdaDetails == null)
            {
                //DO SOMETHING
                //return
            }
            string oldATC = tdaDetails.vehicle;
            string newATC = tdaDetails.sales_doc_number;
            string trip_id = tdaDetails.trip_id;  
            
            var tradeDetails = db.Trades.Where(a => a.sparenum2.ToString() == oldATC && a.seconddatetime == null).FirstOrDefault();
          
            var ATCtradeDetails = db.atcs.Where(a => a.sales_doc_number == oldATC).FirstOrDefault();
            if (tradeDetails != null)
            {
                
                tradeDetails.sparenum2 = int.Parse(newATC);
                tradeDetails.specification = newATC;


                db.Entry(tradeDetails).State = EntityState.Modified;
                db.SaveChanges();


                ATCtradeDetails.sales_doc_number = newATC;
                ATCtradeDetails.waybill = newATC;

                db.Entry(ATCtradeDetails).State = EntityState.Modified;
                db.SaveChanges();


                tdaDetails.operator_picking = comment;
                tdaDetails.sales_doc_type = "APPROVED";
                tdaDetails.picking_time = DateTime.Now;

                db.Entry(tdaDetails).State = EntityState.Modified;
                db.SaveChanges();


                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, oldATC, user_id, ActivityType.FETCH_ATC_ATTEMPT_FOR_SWAP_HELPER, newATC, DateTime.Now.ToString());


                tradex.msg = "MATERIAL WAYBILL SWAP IS SUCCESSFUL!!";
                tradex.status = true;            
                
                return Json(tradex);

                //return
            }

            //STO_TRANSACTION_DATA OUTBOUND
            var StoOutTdaDetails = db.transaction_data_archive.Where(a => a.id == id && a.sales_doc_type == "PENDING").FirstOrDefault();
            if (StoOutTdaDetails == null)
            {
                //DO SOMETHING
                //return
            }
            string oldATCStoOut = StoOutTdaDetails.vehicle;
            string newATCStoOut = StoOutTdaDetails.sales_doc_number;
            string trip_idStoOut = StoOutTdaDetails.trip_id;

            var stoTrxDataDetaisOut = db.sto_transaction_data.Where(x => x.po_doc_number == oldATCStoOut && x.gross == null && x.sales_type == "PO_OUT").FirstOrDefault();
            var ATCstoTrxDataDetaisOutOld = db.sto_data.Where(x => x.delivery_number_out == oldATCStoOut && x.sales_type == "PO_OUT").FirstOrDefault();
            var ATCstoTrxDataDetaisOutNew = db.sto_data.Where(x => x.delivery_number_out == newATCStoOut && x.sales_type == "PO_OUT").FirstOrDefault();
            if (stoTrxDataDetaisOut != null)
            {
                stoTrxDataDetaisOut.po_doc_number = newATCStoOut;
                stoTrxDataDetaisOut.trip_id = trip_idStoOut;

                db.Entry(stoTrxDataDetaisOut).State = EntityState.Modified;
                db.SaveChanges();

                ATCstoTrxDataDetaisOutOld.used = null;

                db.Entry(ATCstoTrxDataDetaisOutOld).State = EntityState.Modified;
                db.SaveChanges();

                ATCstoTrxDataDetaisOutNew.used = true;

                db.Entry(ATCstoTrxDataDetaisOutNew).State = EntityState.Modified;
                db.SaveChanges();


                StoOutTdaDetails.operator_picking = comment;
                StoOutTdaDetails.sales_doc_type = "APPROVED";
                StoOutTdaDetails.picking_time = DateTime.Now;

                db.Entry(StoOutTdaDetails).State = EntityState.Modified;
                db.SaveChanges();



                stdx.status = true;
                stdx.msg = "ZSTO DELIVERY SWAP IS SUCCESSFULL";


                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, oldATCStoOut, user_id, ActivityType.FETCH_ATC_ATTEMPT_FOR_SWAP_HELPER, newATCStoOut, DateTime.Now.ToString());

                return Json(stdx);

            }

            //STO_TRANSACTION_DATA INBOUND
            var StoInTdaDetails = db.transaction_data_archive.Where(a => a.id == id && a.sales_doc_type == "PENDING").FirstOrDefault();
            if (StoInTdaDetails == null)
            {
                //DO SOMETHING
                //return
            }

            string oldATCStoIn = StoInTdaDetails.vehicle;
            string newATCStoIn = StoInTdaDetails.sales_doc_number;
            string trip_idStoIn = StoInTdaDetails.trip_id;

            var stoTrxDataDetaisIn = db.sto_transaction_data.Where(x => x.po_doc_number == oldATCStoIn && x.tare == null && x.sales_type == "PO_IN").FirstOrDefault();
            var ATCstoTrxDataDetaisInOld = db.sto_data.Where(x => x.delivery_number == oldATCStoIn && x.sales_type == "PO_IN").FirstOrDefault();
            var ATCstoTrxDataDetaisInNew = db.sto_data.Where(x => x.delivery_number == newATCStoIn && x.sales_type == "PO_IN").FirstOrDefault();
            if (stoTrxDataDetaisIn != null)
            {
                stoTrxDataDetaisIn.po_doc_number = newATCStoIn;
                stoTrxDataDetaisIn.trip_id = trip_idStoIn;

                db.Entry(stoTrxDataDetaisIn).State = EntityState.Modified;
                db.SaveChanges();

                ATCstoTrxDataDetaisInOld.used = null;

                db.Entry(ATCstoTrxDataDetaisInOld).State = EntityState.Modified;
                db.SaveChanges();

                ATCstoTrxDataDetaisInNew.used = true;

                db.Entry(ATCstoTrxDataDetaisInNew).State = EntityState.Modified;
                db.SaveChanges();


                StoInTdaDetails.operator_picking = comment;
                StoInTdaDetails.sales_doc_type = "APPROVED";
                StoInTdaDetails.picking_time = DateTime.Now;

                db.Entry(StoInTdaDetails).State = EntityState.Modified;
                db.SaveChanges();


                //DO STO Outbound swap swap
                stdx.status = true;
                stdx.msg = "ZSTO DELIVERY SWAP IS SUCCESSFULL";


                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, oldATCStoIn, user_id, ActivityType.FETCH_ATC_ATTEMPT_FOR_SWAP_HELPER, newATCStoIn, DateTime.Now.ToString());

                return Json(stdx);
            }

            //TRANSACTION_DATA
            var atcDetails = db.transaction_data_archive.Where(a => a.id == id && a.sales_doc_type == "PENDING").FirstOrDefault();
            if (atcDetails == null)
            {
                //DO SOMETHING
                //return
            }

            string oldATCTrxData = atcDetails.vehicle;
            string newATCTrxData = atcDetails.sales_doc_number;
            string trip_idTrxData = atcDetails.trip_id;

            var TrxDataDetais = db.transaction_data.Where(x => x.sales_doc_number == oldATCTrxData && x.gross == null && x.sales_type == "BAGS").FirstOrDefault();
            var ATCTrxDataDetaisOld = db.atc_data.Where(x => x.sales_doc_number == oldATCTrxData && x.sales_type == "BAGS").FirstOrDefault();
            var ATCTrxDataDetaisNew = db.atc_data.Where(x => x.sales_doc_number == newATCTrxData && x.sales_type == "BAGS").FirstOrDefault();

            if (TrxDataDetais != null)
            {
                TrxDataDetais.sales_doc_number = newATCTrxData;
                TrxDataDetais.trip_id = trip_idTrxData;

                db.Entry(TrxDataDetais).State = EntityState.Modified;
                db.SaveChanges();


                ATCTrxDataDetaisOld.used = null;

                db.Entry(ATCTrxDataDetaisOld).State = EntityState.Modified;
                db.SaveChanges();


                ATCTrxDataDetaisNew.used = true;

                db.Entry(ATCTrxDataDetaisNew).State = EntityState.Modified;
                db.SaveChanges();


                atcDetails.operator_picking = comment;
                atcDetails.sales_doc_type = "APPROVED";
                atcDetails.picking_time = DateTime.Now;

                db.Entry(atcDetails).State = EntityState.Modified;
                db.SaveChanges();


                //DO STO Outbound swap swap
                trx_Datax.status = true;
                trx_Datax.msg = "ATC SWAP IS SUCCESSFULL";


                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, oldATCTrxData, user_id, ActivityType.FETCH_ATC_ATTEMPT_FOR_SWAP_HELPER, newATCTrxData, DateTime.Now.ToString());

                return Json(trx_Datax);
            }


            return Json(trx_Datax);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DoFetchATcForGrossModif(String __RequestVerificationToken, String atc)
        {

            sto_datax sto_Datax = new sto_datax();

            string atc_actual = atc;
            string msge;

            var transDetailsExistTda = db.transaction_data_archive.Where(x => x.sales_doc_number == atc && x.operator_weighin == "ADDITIONAL GROSS WEIGHT MODIF REQUEST").FirstOrDefault();
            if (transDetailsExistTda != null)
            {
                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.FETCH_ATC_ATTEMPT_FOR_GROSS_CHANGE_HELPER, "FETCH_ATC_ATTEMPT_FOR GROSS_CHANGE : ", DateTime.Now.ToString());

                sto_Datax.status = false;
                sto_Datax.msg = "Theres A Pending Request For Gross WT Change For This Same ATC!!!";
                return Json(sto_Datax);
            }


            try
            {
                var transDetails = db.transaction_data.Where(x => x.sales_doc_number == atc && x.sync_status == false && x.operator_picking != null && x.gross == null).FirstOrDefault();
                if(transDetails == null)
                {
                    int user_id = int.Parse(Session["user_id"].ToString());
                    TransactionLogging TRX_LOG = new TransactionLogging();
                    bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.FETCH_ATC_ATTEMPT_FOR_GROSS_CHANGE_HELPER, "FETCH_ATC_ATTEMPT_FOR GROSS_CHANGE : ", DateTime.Now.ToString());

                    sto_Datax.status = false;
                    sto_Datax.msg = "Wrong ATC Or ATC Is Weighed Out Or ATC Is Pushed To SAP Already";
                    return Json(sto_Datax);
                }

                var transDetailsWC = db.weight_change.Where(x => x.change_request_id != null && x.status == "APPROVED" && x.net_weight != null && x.atc_no == atc && x.weight_change1 == "-150" || x.weight_change1 == "150").FirstOrDefault();
                if (transDetailsWC == null)
                {
                    int user_id = int.Parse(Session["user_id"].ToString());
                    TransactionLogging TRX_LOG = new TransactionLogging();
                    bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.CHECK_FOR_DDS_APPROVAL_MAX_GROSS_WT_CHANGE, "CHECK_FOR_DDS_APPROVAL_MAX_GROSS_WT_CHANGE : ", DateTime.Now.ToString());

                    sto_Datax.status = false;
                    sto_Datax.msg = "MAXIMUM Gross Weight Change Needs To Be Requested And Approved From DDS First";
                    return Json(sto_Datax);
                }
                else
                {
                    var change_id = transDetailsWC.change_request_id;
                    var transChangeRequestDetails = db.change_request.Where(x => x.id == change_id && x.request_type == "GROSS_WEIGHT_CHANGE" && x.date_of_approval != null && x.approved == true).FirstOrDefault();
                    if (transChangeRequestDetails == null)
                    {

                        int user_id = int.Parse(Session["user_id"].ToString());
                        TransactionLogging TRX_LOG = new TransactionLogging();
                        bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.CHECK_FOR_DDS_APPROVAL_MAX_GROSS_WT_CHANGE, "CHECK_FOR_DDS_APPROVAL_MAX_GROSS_WT_CHANGE : ", DateTime.Now.ToString());


                        sto_Datax.msg = "Please Ensure Initial DDS Approval Request Was Granted";
                        sto_Datax.status = false;                        

                        return Json(sto_Datax);
                    }
                    else
                    {

                        int user_id = int.Parse(Session["user_id"].ToString());
                        TransactionLogging TRX_LOG = new TransactionLogging();
                        bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.CHECK_FOR_DDS_APPROVAL_MAX_GROSS_WT_CHANGE, "CHECK_FOR_DDS_APPROVAL_MAX_GROSS_WT_CHANGE : ", DateTime.Now.ToString());


                        sto_Datax.msg = "";
                        sto_Datax.status = true;                       
                        sto_Datax.grade = transDetailsWC.weight_change1; //loc
                        sto_Datax.stoLoc = transDetailsWC.status;
                        sto_Datax.sender = transDetailsWC.net_weight; //nett
                        sto_Datax.driverName = transDetailsWC.tare_weight;
                        sto_Datax.destination = transDetailsWC.gross_weight;
                        sto_Datax.receiver = transChangeRequestDetails.notes;
                        sto_Datax.requestedBy = transChangeRequestDetails.requested_by;
                        sto_Datax.approvedBy = transChangeRequestDetails.approved_by;
                        sto_Datax.requestType = transChangeRequestDetails.request_type;



                        sto_Datax.dateOfApproval = transChangeRequestDetails.date_of_approval ?? DateTime.MinValue;
                        sto_Datax.dateOfRequest = transChangeRequestDetails.date_of_request ?? DateTime.MinValue;
                        sto_Datax.Quantity = transDetails.quantity ?? 0;
                        sto_Datax.truckNumber = transDetails.vehicle; //vehicle
                        sto_Datax.TruckDriverName = transDetails.driver; //driver
                        sto_Datax.Migo_Details = "CEMENT"; //migo_details
                        sto_Datax.truckDestination = transDetails.destination; //destination
                        sto_Datax.operatorWeighin = transDetails.operator_weighin; //bin_1
                        sto_Datax.operatorPicking = transDetails.operator_picking; //bin_2


                        sto_Datax.atc = atc; //sales_doc_number
                    }
               
                }



            }
            catch (Exception ex)
            {
                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.FETCH_ATC_ATTEMPT_FOR_GROSS_CHANGE_HELPER, "FETCH_ATC_ATTEMPT_FOR_GROSS_CHANGE_HELPER : ", DateTime.Now.ToString());

                msge = ex.Message;
                sto_Datax.msg = "Error (an exception) occured somewhere";
                sto_Datax.status = false;
                sto_Datax.excptn = ex.ToString();

                return Json(sto_Datax);

            }
            
            
            return Json(sto_Datax);

        }


        //Inbound/outbound to ZSTO 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DoFetchTradeDetailsForZSTOMovement(String __RequestVerificationToken, String atc)
        {

            sto_datax sto_Datax = new sto_datax();
            trade_dto tradex = new trade_dto();

            //string atc_actual = atc;
            string msge;
            string oldATC = atc;
            string oldATCx = atc.PadLeft(10, '0');

            var stoExistCheck = db.sto_transaction_data.Where(x => x.po_doc_number == atc).FirstOrDefault();

            if (stoExistCheck != null)
            {
                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.FETCH_ATC_ATTEMPT_FOR_MATERIAL_TO_ZSTO, "FETCH_ATC_ATTEMPT_FOR_MATERIAL_TO_ZSTO : ", DateTime.Now.ToString());

                sto_Datax.status = false;
                //sto_Datax.msg = "Theres A Pending Request For Gross WT Change For This Same ATC!!!";
                sto_Datax.msg = "Delivery Is Wrong Or Already weighed in as ZSTO!!!";
                return Json(sto_Datax);
            }

            var stoTradeDetails = db.Trades.Where(x => x.sparenum2.ToString() == oldATC && x.seconddatetime == null).FirstOrDefault();

            try
            {
                if (stoTradeDetails == null)
                {
                    int user_id = int.Parse(Session["user_id"].ToString());
                    TransactionLogging TRX_LOG = new TransactionLogging();
                    bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.FETCH_ATC_ATTEMPT_FOR_MATERIAL_TO_ZSTO, "FETCH_ATC_ATTEMPT_FOR_MATERIAL_TO_ZSTO : ", DateTime.Now.ToString());

                    sto_Datax.status = false;
                    //sto_Datax.msg = "Theres A Pending Request For Gross WT Change For This Same ATC!!!";
                    sto_Datax.msg = "Delivery Is Wrong Or Already weighed Out as Material!!!";
                    return Json(sto_Datax);
                }
                else
                {
                    var stoTradeSTO_DataDetails = db.sto_data.Where(x => x.used == null && x.delivery_number == oldATCx || x.delivery_number_out == oldATCx).FirstOrDefault();
                    if (stoTradeSTO_DataDetails != null)
                    {
                        var deliveryIn = stoTradeSTO_DataDetails.delivery_number;
                        var deliveryOut = stoTradeSTO_DataDetails.delivery_number_out;
                        var sales_doc_type = stoTradeSTO_DataDetails.sales_doc_type;
                        Session["Sales_code_type"] = sales_doc_type;

                        var del_num = "";
                        String transTypeCheck = "All";
                        if (deliveryIn != "")
                        {
                            transTypeCheck = "InboundZSTO";
                            Session["Trans_Type"] = transTypeCheck;
                            ViewBag.TransType = transTypeCheck;




                        }
                        else
                        {
                            transTypeCheck = "OutboundZSTO";
                            Session["Trans_Type"] = transTypeCheck;
                            ViewBag.TransType = transTypeCheck;
                        }


                            //del_num = deliveryIn; Session["Trans_Type"] = "InboundZSTO" ?? deliveryOut; Session["Trans_Type"] = "OutboundZSTO";

                        //if (deliveryIn != null) Session["Trans_Type"] = "InboundZSTO" ?? "OutboundZSTO";
                        int user_id = int.Parse(Session["user_id"].ToString());
                        TransactionLogging TRX_LOG = new TransactionLogging();
                        bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.FETCH_ATC_ATTEMPT_FOR_MATERIAL_TO_ZSTO, "FETCH_ATC_ATTEMPT_FOR_MATERIAL_TO_ZSTO : ", DateTime.Now.ToString());



                        tradex.msg = "";
                        tradex.status = true;
                        tradex.driver = stoTradeDetails.sparestr1; //loc
                        tradex.operatorWeighIn = stoTradeDetails.username1;
                        tradex.spareStr6 = stoTradeDetails.sparestr5; //nett
                        tradex.transporter_name = stoTradeDetails.transporter_name;
                        tradex.product = stoTradeDetails.product;
                        tradex.vehicle = stoTradeDetails.truckno;
                        tradex.Tare = stoTradeDetails.firstweight.ToString();
                        tradex.FirstDateTime = stoTradeDetails.firstdatetime;
                        tradex.sender = stoTradeDetails.sender;
                        tradex.receiver = stoTradeDetails.receiver;
                        tradex.Transaction_Type = transTypeCheck;
                        tradex.waybill = stoTradeDetails.sparestr6;
                        //tradex.approvedBy = transChangeRequestDetails.approved_by;
                        tradex.atc = atc;
                        tradex.status = true;

                        //sto_Datax.msg = "Delivery Is Not Vallid Or Has already been weighed in as ZSTO";
                        //sto_Datax.status = false;
                        return Json(tradex);


                    }
                }
            }
            catch (Exception ex)
            {
                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.FETCH_ATC_ATTEMPT_FOR_MATERIAL_TO_ZSTO, "FETCH_ATC_ATTEMPT_FOR_MATERIAL_TO_ZSTO : ", DateTime.Now.ToString());

               msge = ex.Message;
                sto_Datax.msg = "Error (an exception) occured somewhere. Contact Admin.";
                sto_Datax.status = false;
                sto_Datax.excptn = ex.ToString();

                return Json(sto_Datax);
            }

            sto_Datax.msg = "Delivery Is Not Vallid Or Has already been weighed in as ZSTO";            
            sto_Datax.status = false;            
            return Json(sto_Datax);

        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DoActualATCArchive(String __RequestVerificationToken, int id, String comment)
        {


            Transaction_Datax trx_Datax = new Transaction_Datax();
            trade_dto tradex = new trade_dto();
            Sto_Transaction_Datax stdx = new Sto_Transaction_Datax();

            string msge;


            //TRADE
            var tdaDetails = db.transaction_data_archive.Where(a => a.id == id && a.sales_doc_type == "PENDING").FirstOrDefault();
            if (tdaDetails == null)
            {
                //DO SOMETHING
                //return
            }
            string oldATC = tdaDetails.sales_doc_number;
            string oldATCx = tdaDetails.sales_doc_number.PadLeft(10, '0');

            var tradeDetails = db.Trades.Where(a => a.sparenum2.ToString() == oldATC && a.seconddatetime == null).FirstOrDefault();
            var ATCtradeDetails = db.atcs.Where(a => a.sales_doc_number == oldATCx).FirstOrDefault();
            if (tradeDetails != null)
            {

                db.Entry(tradeDetails).State = EntityState.Deleted;
                db.SaveChanges();

                db.Entry(ATCtradeDetails).State = EntityState.Deleted;
                db.SaveChanges();


                tdaDetails.operator_picking = comment;
                tdaDetails.sales_doc_type = "APPROVED";
                tdaDetails.picking_time = DateTime.Now;

                db.Entry(tdaDetails).State = EntityState.Modified;
                db.SaveChanges();


                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, oldATC, user_id, ActivityType.APPROVE_ARCHIVING_HELPER, oldATC, DateTime.Now.ToString());


                tradex.msg = "ATC ARCHIVE IS SUCCESSFUL!!";
                tradex.status = true;

                return Json(tradex);

                //return
            }

            //STO_TRANSACTION_DATA OUTBOUND
            var StoOutTdaDetails = db.transaction_data_archive.Where(a => a.id == id && a.sales_doc_type == "PENDING").FirstOrDefault();
            if (StoOutTdaDetails == null)
            {
                //DO SOMETHING
                //return
            }
            string oldATCStoOut = StoOutTdaDetails.sales_doc_number;

            var stoTrxDataDetaisOut = db.sto_transaction_data.Where(x => x.po_doc_number == oldATCStoOut && x.gross == null && x.sales_type == "PO_OUT").FirstOrDefault();
            var ATCstoTrxDataDetaisOutOld = db.sto_data.Where(x => x.delivery_number_out == oldATCStoOut && x.sales_type == "PO_OUT").FirstOrDefault();

            if (stoTrxDataDetaisOut != null)
            {
                
                db.Entry(stoTrxDataDetaisOut).State = EntityState.Deleted;
                db.SaveChanges();

                ATCstoTrxDataDetaisOutOld.used = null;

                db.Entry(ATCstoTrxDataDetaisOutOld).State = EntityState.Modified;
                db.SaveChanges();


                StoOutTdaDetails.operator_picking = comment;
                StoOutTdaDetails.sales_doc_type = "APPROVED";
                StoOutTdaDetails.picking_time = DateTime.Now;

                db.Entry(StoOutTdaDetails).State = EntityState.Modified;
                db.SaveChanges();



                stdx.status = true;
                stdx.msg = "ATC ARCHIVE IS SUCCESSFULL";


                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, oldATCStoOut, user_id, ActivityType.APPROVE_ARCHIVING_HELPER, oldATCStoOut, DateTime.Now.ToString());

                return Json(stdx);

            }

            //STO_TRANSACTION_DATA INBOUND
            var StoInTdaDetails = db.transaction_data_archive.Where(a => a.id == id && a.sales_doc_type == "PENDING").FirstOrDefault();
            if (StoInTdaDetails == null)
            {
                //DO SOMETHING
                //return
            }

            string oldATCStoIn = StoInTdaDetails.sales_doc_number;

            var stoTrxDataDetaisIn = db.sto_transaction_data.Where(x => x.po_doc_number == oldATCStoIn && x.tare == null && x.sales_type == "PO_IN").FirstOrDefault();
            var ATCstoTrxDataDetaisInOld = db.sto_data.Where(x => x.delivery_number == oldATCStoIn && x.sales_type == "PO_IN").FirstOrDefault();
            if (stoTrxDataDetaisIn != null)
            {

                db.Entry(stoTrxDataDetaisIn).State = EntityState.Deleted;
                db.SaveChanges();

                ATCstoTrxDataDetaisInOld.used = null;

                db.Entry(ATCstoTrxDataDetaisInOld).State = EntityState.Modified;
                db.SaveChanges();


                StoInTdaDetails.operator_picking = comment;
                StoInTdaDetails.sales_doc_type = "APPROVED";
                StoInTdaDetails.picking_time = DateTime.Now;

                db.Entry(StoInTdaDetails).State = EntityState.Modified;
                db.SaveChanges();


                //DO STO Outbound swap swap
                stdx.status = true;
                stdx.msg = "ATC ARCHIVE IS SUCCESSFULL";


                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, oldATCStoIn, user_id, ActivityType.APPROVE_ARCHIVING_HELPER, oldATCStoIn, DateTime.Now.ToString());

                return Json(stdx);
            }

            //TRANSACTION_DATA
            var atcDetails = db.transaction_data_archive.Where(a => a.id == id && a.sales_doc_type == "PENDING").FirstOrDefault();
            if (atcDetails == null)
            {
                //DO SOMETHING
                //return
            }

            string oldATCTrxData = atcDetails.sales_doc_number;

            var TrxDataDetais = db.transaction_data.Where(x => x.sales_doc_number == oldATCTrxData && x.gross == null && x.sales_type == "BAGS").FirstOrDefault();
            var ATCTrxDataDetaisOld = db.atc_data.Where(x => x.sales_doc_number == oldATCTrxData && x.sales_type == "BAGS").FirstOrDefault();

            if (TrxDataDetais != null)
            {

                db.Entry(TrxDataDetais).State = EntityState.Deleted;
                db.SaveChanges();


                ATCTrxDataDetaisOld.used = null;

                db.Entry(ATCTrxDataDetaisOld).State = EntityState.Modified;
                db.SaveChanges();



                atcDetails.operator_picking = comment;
                atcDetails.sales_doc_type = "APPROVED";
                atcDetails.picking_time = DateTime.Now;

                db.Entry(atcDetails).State = EntityState.Modified;
                db.SaveChanges();


                //DO STO Outbound swap swap
                trx_Datax.status = true;
                trx_Datax.msg = "ATC ARCHIVE IS SUCCESSFULL";


                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, oldATCTrxData, user_id, ActivityType.APPROVE_ARCHIVING_HELPER, oldATCTrxData, DateTime.Now.ToString());

                return Json(trx_Datax);
            }


            return Json(trx_Datax);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DoActualGWTApproval(String __RequestVerificationToken, int id, String comment, String weightToBeAdded)
        {


            Transaction_Datax trx_Datax = new Transaction_Datax();
            trade_dto tradex = new trade_dto();
            Sto_Transaction_Datax stdx = new Sto_Transaction_Datax();

            string msge;
            var tdaDetails = db.transaction_data_archive.Where(a => a.id == id && a.sales_doc_type == "PENDING").FirstOrDefault();
            string oldATC = tdaDetails.sales_doc_number;
            string oldATCx = tdaDetails.sales_doc_number.PadLeft(10, '0');

            try
            {
                
                if (tdaDetails == null)
                {
                    //DO SOMETHING
                    //return
                }
                

                var ATCGWTCHange = db.weight_change.Where(x => x.atc_no == oldATC && x.status == "APPROVED").FirstOrDefault();
                int initialWeightAdded = int.Parse(ATCGWTCHange.weight_change1);
                int newWTToBeAdded = int.Parse(weightToBeAdded);
                int newGWTReApproved = newWTToBeAdded + initialWeightAdded;

                ATCGWTCHange.weight_change1 = newGWTReApproved.ToString();
                ATCGWTCHange.filename = "Weight Re-Approved By HELPER";


                db.Entry(ATCGWTCHange).State = EntityState.Modified;
                db.SaveChanges();


                tdaDetails.operator_picking = comment;
                tdaDetails.sales_doc_type = "APPROVED";
                tdaDetails.picking_time = DateTime.Now;


                db.Entry(tdaDetails).State = EntityState.Modified;
                db.SaveChanges();


                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, oldATC, user_id, ActivityType.APPROVED_GROSS_WT_ADDITION_HELPER, oldATC + "old DDS requested " + initialWeightAdded + " New HELPER Requested" + newWTToBeAdded + " SUM = " + newGWTReApproved, DateTime.Now.ToString());


                tradex.msg = "GROSS WEIGHT ADDITION APPROVAL IS SUCCESSFUL!!";
                tradex.status = true;

                return Json(tradex);
            }
            catch(Exception ex)
            {

                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, oldATC, user_id, ActivityType.ERROR_APPROVING_GROSS_WT_ADDITION_HELPER, oldATC, DateTime.Now.ToString());

                tradex.msg = "";
                tradex.status = false;
            }



            return Json(tradex);

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DoActualZSTOMoveApproval(String __RequestVerificationToken, int id, String comment)
        {


            Transaction_Datax trx_Datax = new Transaction_Datax();
            trade_dto tradex = new trade_dto();
            Sto_Transaction_Datax stdx = new Sto_Transaction_Datax();
            sto_transaction_data stdax = new sto_transaction_data();

            string msge;
            var StoTdaDetails = db.transaction_data_archive.Where(a => a.id == id && a.sales_doc_type == "PENDING").FirstOrDefault();
            string oldATC = StoTdaDetails.sales_doc_number;
            string trimmedATc = oldATC.TrimStart('0');
            string oldATCx = StoTdaDetails.sales_doc_number.PadLeft(10, '0');

            try
            {

                if (StoTdaDetails == null)
                {
                    //DO SOMETHING
                    //return
                }

                stdax.po_doc_number = StoTdaDetails.sales_doc_number;
                stdax.loc = StoTdaDetails.loc;
                stdax.trip_id = StoTdaDetails.trip_id;
                stdax.vehicle = StoTdaDetails.vehicle;
                stdax.driver = StoTdaDetails.driver;
                stdax.transporter = StoTdaDetails.transporter;
               
                stdax.tmp_waybill_no = StoTdaDetails.tmp_waybill_no;
                stdax.parent_sales_order = StoTdaDetails.parent_sales_order;
                stdax.sender = StoTdaDetails.sender;
                stdax.destination = StoTdaDetails.destination;
                stdax.operator_weighin = StoTdaDetails.bin_1;
                stdax.sales_doc_type = StoTdaDetails.bin_2;
                stdax.transporter_name = StoTdaDetails.transporter_name;
                stdax.gross = StoTdaDetails.gross;
                stdax.sales_type = StoTdaDetails.sales_type;
                if (stdax.sales_type == "PO_IN")
                {
                    stdax.gross = StoTdaDetails.gross;
                    stdax.gross_time = StoTdaDetails.gross_time;
                }
                else
                {
                    stdax.tare = StoTdaDetails.nett;
                    stdax.tare_time = StoTdaDetails.sap_post_time;
                }

                
                
                
                
                //stda



                db.sto_transaction_data.Add(stdax);
                //var ATCGWTCHange = db.weight_change.Where(x => x.atc_no == oldATC && x.status == "APPROVED").FirstOrDefault();
                //int initialWeightAdded = int.Parse(ATCGWTCHange.weight_change1);
                //int newWTToBeAdded = int.Parse(weightToBeAdded);
                //int newGWTReApproved = newWTToBeAdded + initialWeightAdded;

                //ATCGWTCHange.weight_change1 = newGWTReApproved.ToString();
                //ATCGWTCHange.filename = "Weight Re-Approved By HELPER";


                //db.Entry(ATCGWTCHange).State = EntityState.Modified;
                db.SaveChanges();


                StoTdaDetails.operator_picking = comment;
                StoTdaDetails.sales_doc_type = "APPROVED";
                StoTdaDetails.picking_time = DateTime.Now;

                db.Entry(StoTdaDetails).State = EntityState.Modified;
                db.SaveChanges();


                var stoDataDetails = db.sto_data.Where(x => x.used == null && x.delivery_number == oldATCx || x.delivery_number_out == oldATCx).FirstOrDefault();

                stoDataDetails.used = true;
                db.Entry(stoDataDetails).State = EntityState.Modified;
                db.SaveChanges();



                var tradeDetails = db.Trades.Where(a => a.sparenum2.ToString() == trimmedATc && a.seconddatetime == null).FirstOrDefault();
                var ATCtradeDetails = db.atcs.Where(a => a.sales_doc_number == oldATCx).FirstOrDefault();
                if (tradeDetails != null)
                {
                    db.Entry(tradeDetails).State = EntityState.Deleted;
                    db.SaveChanges();

                    db.Entry(ATCtradeDetails).State = EntityState.Deleted;
                    db.SaveChanges();
                }
                


                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, oldATC, user_id, ActivityType.APPROVED_ZSTO_MOVEMENT, oldATC, DateTime.Now.ToString());


                tradex.msg = "MATERIAL TO ZSTO MOVEMENT APPROVAL IS SUCCESSFUL!!";
                tradex.status = true;

                return Json(tradex);
            }
            catch (Exception ex)
            {

                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, oldATC, user_id, ActivityType.ERROR_APPROVING_GROSS_WT_ADDITION_HELPER, oldATC, DateTime.Now.ToString());

                tradex.msg = "";
                tradex.status = false;
            }



            return Json(tradex);

        }






        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DoActualTareModification(String __RequestVerificationToken, int id, String comment)
        {


            Transaction_Datax trx_Datax = new Transaction_Datax();
            trade_dto tradex = new trade_dto();
            Sto_Transaction_Datax stdx = new Sto_Transaction_Datax();

            string msge;


            //TRADE
            var tdaDetails = db.transaction_data_archive.Where(a => a.id == id && a.sales_doc_type == "PENDING").FirstOrDefault();
            if (tdaDetails == null)
            {
                //DO SOMETHING
                //return
            }
            string oldATC = tdaDetails.sales_doc_number;
            string oldATCx = tdaDetails.sales_doc_number.PadLeft(10, '0');

            var tradeDetails = db.Trades.Where(a => a.sparenum2.ToString() == oldATC && a.seconddatetime == null).FirstOrDefault();
            var ATCtradeDetails = db.atcs.Where(a => a.sales_doc_number == oldATCx).FirstOrDefault();
            var ATCtradeDetails2 = db.atcs.Where(a => a.sales_doc_number == oldATC).FirstOrDefault();
            if (tradeDetails != null)
            {
                

                tdaDetails.operator_picking = comment;
                tdaDetails.sales_doc_type = "APPROVED";
                tdaDetails.picking_time = DateTime.Now;
                db.Entry(tdaDetails).State = EntityState.Modified;
                db.SaveChanges();


                tradeDetails.firstweight = tdaDetails.bin_2_tare;       
                db.Entry(tradeDetails).State = EntityState.Modified;
                db.SaveChanges();


                ATCtradeDetails2.tare_weight = tdaDetails.bin_2_tare.ToString();
                db.Entry(ATCtradeDetails2).State = EntityState.Modified;
                db.SaveChanges();


                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, oldATC, user_id, ActivityType.APPROVE_TARE_WEIGHT_MODIFY_HELPER, oldATC, DateTime.Now.ToString());


                tradex.msg = "TARE MODIFICATION APPROVAL IS SUCCESSFUL!!";
                tradex.status = true;

                return Json(tradex);

                
            }

            //STO_TRANSACTION_DATA OUTBOUND
            var StoOutTdaDetails = db.transaction_data_archive.Where(a => a.id == id && a.sales_doc_type == "PENDING").FirstOrDefault();
            if (StoOutTdaDetails == null)
            {
                //DO SOMETHING
                //return
            }
            string oldATCStoOut = StoOutTdaDetails.sales_doc_number;

            var stoTrxDataDetaisOut = db.sto_transaction_data.Where(x => x.po_doc_number == oldATCStoOut && x.gross == null && x.sales_type == "PO_OUT").FirstOrDefault();
            var ATCstoTrxDataDetaisOutOld = db.sto_data.Where(x => x.delivery_number_out == oldATCStoOut && x.sales_type == "PO_OUT").FirstOrDefault();

            if (stoTrxDataDetaisOut != null)
            {


                StoOutTdaDetails.operator_picking = comment;
                StoOutTdaDetails.sales_doc_type = "APPROVED";
                StoOutTdaDetails.picking_time = DateTime.Now;
                db.Entry(StoOutTdaDetails).State = EntityState.Modified;
                db.SaveChanges();


                stoTrxDataDetaisOut.tare = tdaDetails.bin_2_tare;
                db.Entry(stoTrxDataDetaisOut).State = EntityState.Modified;
                db.SaveChanges();

                
                stdx.status = true;
                stdx.msg = "TARE MODIFICATION APPROVAL IS SUCCESSFULL";


                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, oldATCStoOut, user_id, ActivityType.APPROVE_TARE_WEIGHT_MODIFY_HELPER, oldATCStoOut, DateTime.Now.ToString());

                return Json(stdx);

            }

            //STO_TRANSACTION_DATA INBOUND
            var StoInTdaDetails = db.transaction_data_archive.Where(a => a.id == id && a.sales_doc_type == "PENDING").FirstOrDefault();
            if (StoInTdaDetails == null)
            {
                //DO SOMETHING
                //return
            }

            string oldATCStoIn = StoInTdaDetails.sales_doc_number;

            var stoTrxDataDetaisIn = db.sto_transaction_data.Where(x => x.po_doc_number == oldATCStoIn && x.tare == null && x.sales_type == "PO_IN").FirstOrDefault();
            var ATCstoTrxDataDetaisInOld = db.sto_data.Where(x => x.delivery_number == oldATCStoIn && x.sales_type == "PO_IN").FirstOrDefault();
            if (stoTrxDataDetaisIn != null)
            {



                StoInTdaDetails.operator_picking = comment;
                StoInTdaDetails.sales_doc_type = "APPROVED";
                StoInTdaDetails.picking_time = DateTime.Now;

                db.Entry(StoInTdaDetails).State = EntityState.Modified;
                db.SaveChanges();


                stoTrxDataDetaisIn.tare = StoInTdaDetails.bin_2_tare;
                db.Entry(stoTrxDataDetaisIn).State = EntityState.Modified;
                db.SaveChanges();


                //DO STO Outbound swap swap
                stdx.status = true;
                stdx.msg = "TARE MODIFICATION APPROVAL IS SUCCESSFULL";


                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, oldATCStoIn, user_id, ActivityType.APPROVE_TARE_WEIGHT_MODIFY_HELPER, oldATCStoIn, DateTime.Now.ToString());

                return Json(stdx);
            }

            //TRANSACTION_DATA
            var atcDetails = db.transaction_data_archive.Where(a => a.id == id && a.sales_doc_type == "PENDING").FirstOrDefault();
            if (atcDetails == null)
            {
                //DO SOMETHING
                //return
            }

            string oldATCTrxData = atcDetails.sales_doc_number;

            var TrxDataDetais = db.transaction_data.Where(x => x.sales_doc_number == oldATCTrxData && x.gross == null && x.sales_type == "BAGS").FirstOrDefault();
            var ATCTrxDataDetaisOld = db.atc_data.Where(x => x.sales_doc_number == oldATCTrxData && x.sales_type == "BAGS").FirstOrDefault();

            if (TrxDataDetais != null)
            {



                atcDetails.operator_picking = comment;
                atcDetails.sales_doc_type = "APPROVED";
                atcDetails.picking_time = DateTime.Now;

                db.Entry(atcDetails).State = EntityState.Modified;
                db.SaveChanges();


                TrxDataDetais.tare = atcDetails.bin_2_tare;
                db.Entry(TrxDataDetais).State = EntityState.Modified;
                db.SaveChanges();


                //DO STO Outbound swap swap
                trx_Datax.status = true;
                trx_Datax.msg = "TARE MODIFICATION APPROVAL IS SUCCESSFULL";


                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, oldATCTrxData, user_id, ActivityType.APPROVE_TARE_WEIGHT_MODIFY_HELPER, oldATCTrxData, DateTime.Now.ToString());

                return Json(trx_Datax);
            }


            return Json(trx_Datax);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DoActualModifyTareWeight(String __RequestVerificationToken, int id, String comment)
        {


            Transaction_Datax trx_Datax = new Transaction_Datax();
            trade_dto tradex = new trade_dto();
            Sto_Transaction_Datax stdx = new Sto_Transaction_Datax();

            string msge;


            //TRADE
            var tdaDetails = db.transaction_data_archive.Where(a => a.id == id && a.sales_doc_type == "PENDING").FirstOrDefault();
            if (tdaDetails == null)
            {
                //DO SOMETHING
                //return
            }
            string oldATC = tdaDetails.sales_doc_number;
            string oldATCx = tdaDetails.sales_doc_number.PadLeft(10, '0');

            var tradeDetails = db.Trades.Where(a => a.sparenum2.ToString() == oldATC && a.seconddatetime == null).FirstOrDefault();
            var ATCtradeDetails = db.atcs.Where(a => a.sales_doc_number == oldATCx).FirstOrDefault();
            if (tradeDetails != null)
            {

                tdaDetails.operator_picking = comment;
                tdaDetails.sales_doc_type = "APPROVED";
                tdaDetails.picking_time = DateTime.Now;

                db.Entry(tdaDetails).State = EntityState.Modified;
                db.SaveChanges();


                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, oldATC, user_id, ActivityType.APPROVE_TARE_WEIGHT_MODIFY_HELPER, oldATC, DateTime.Now.ToString());


                tradex.msg = "TARE WEIGHT MODIFICATION IS APPROVED!!";
                tradex.status = true;

                return Json(tradex);

                //return
            }

            //STO_TRANSACTION_DATA OUTBOUND
            var StoOutTdaDetails = db.transaction_data_archive.Where(a => a.id == id && a.sales_doc_type == "PENDING").FirstOrDefault();
            if (StoOutTdaDetails == null)
            {
                //DO SOMETHING
                //return
            }
            string oldATCStoOut = StoOutTdaDetails.sales_doc_number;

            var stoTrxDataDetaisOut = db.sto_transaction_data.Where(x => x.po_doc_number == oldATCStoOut && x.gross == null && x.sales_type == "PO_OUT").FirstOrDefault();
            var ATCstoTrxDataDetaisOutOld = db.sto_data.Where(x => x.delivery_number_out == oldATCStoOut && x.sales_type == "PO_OUT").FirstOrDefault();

            if (stoTrxDataDetaisOut != null)
            {
                StoOutTdaDetails.operator_picking = comment;
                StoOutTdaDetails.sales_doc_type = "APPROVED";
                StoOutTdaDetails.picking_time = DateTime.Now;

                db.Entry(StoOutTdaDetails).State = EntityState.Modified;
                db.SaveChanges();



                stdx.status = true;
                stdx.msg = "TARE WEIGHT MODIFICATION IS APPROVED!!";


                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, oldATCStoOut, user_id, ActivityType.APPROVE_TARE_WEIGHT_MODIFY_HELPER, oldATCStoOut, DateTime.Now.ToString());

                return Json(stdx);

            }

            //STO_TRANSACTION_DATA INBOUND
            var StoInTdaDetails = db.transaction_data_archive.Where(a => a.id == id && a.sales_doc_type == "PENDING").FirstOrDefault();
            if (StoInTdaDetails == null)
            {
                //DO SOMETHING
                //return
            }

            string oldATCStoIn = StoInTdaDetails.vehicle;

            var stoTrxDataDetaisIn = db.sto_transaction_data.Where(x => x.po_doc_number == oldATCStoIn && x.tare == null && x.sales_type == "PO_IN").FirstOrDefault();
            var ATCstoTrxDataDetaisInOld = db.sto_data.Where(x => x.delivery_number == oldATCStoIn && x.sales_type == "PO_IN").FirstOrDefault();
            if (stoTrxDataDetaisIn != null)
            {
                

                StoInTdaDetails.operator_picking = comment;
                StoInTdaDetails.sales_doc_type = "APPROVED";
                StoInTdaDetails.picking_time = DateTime.Now;

                db.Entry(StoInTdaDetails).State = EntityState.Modified;
                db.SaveChanges();


                //DO STO Outbound swap swap
                stdx.status = true;
                stdx.msg = "TARE WEIGHT MODIFICATION IS APPROVED!!";


                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, oldATCStoIn, user_id, ActivityType.APPROVE_TARE_WEIGHT_MODIFY_HELPER, oldATCStoIn, DateTime.Now.ToString());

                return Json(stdx);
            }

            //TRANSACTION_DATA
            var atcDetails = db.transaction_data_archive.Where(a => a.id == id && a.sales_doc_type == "PENDING").FirstOrDefault();
            if (atcDetails == null)
            {
                //DO SOMETHING
                //return
            }

            string oldATCTrxData = atcDetails.vehicle;

            var TrxDataDetais = db.transaction_data.Where(x => x.sales_doc_number == oldATCTrxData && x.gross == null).FirstOrDefault();
            var ATCTrxDataDetaisOld = db.atc_data.Where(x => x.sales_doc_number == oldATCTrxData).FirstOrDefault();

            if (TrxDataDetais != null)
            {
                atcDetails.operator_picking = comment;
                atcDetails.sales_doc_type = "APPROVED";
                atcDetails.picking_time = DateTime.Now;

                db.Entry(atcDetails).State = EntityState.Modified;
                db.SaveChanges();


                //DO STO Outbound swap swap
                trx_Datax.status = true;
                trx_Datax.msg = "TARE WEIGHT MODIFICATION IS APPROVED!!";


                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, oldATCTrxData, user_id, ActivityType.APPROVE_TARE_WEIGHT_MODIFY_HELPER, oldATCTrxData, DateTime.Now.ToString());

                return Json(trx_Datax);
            }


            return Json(trx_Datax);
        }







        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult RejectATCSwap(String __RequestVerificationToken, int id, String comment)
        {

            Transaction_Datax trx_Datax = new Transaction_Datax();
            trade_dto tradex = new trade_dto();
            Sto_Transaction_Datax stdx = new Sto_Transaction_Datax();

            var tdaDetails = db.transaction_data_archive.Where(a => a.id == id && a.sales_doc_type == "PENDING").FirstOrDefault();
            if (tdaDetails == null)
            {
                //DO SOMETHING
                //return
            }
            else
            {
                string oldATC = tdaDetails.sales_doc_number;
                string newATC = tdaDetails.vehicle;
                string trip_id = tdaDetails.trip_id;

                tdaDetails.operator_picking = comment;
                tdaDetails.sales_doc_type = "REJECTED";
                tdaDetails.picking_time = DateTime.Now;
                tdaDetails.wb_in = Session["user_id"].ToString();


                db.Entry(tdaDetails).State = EntityState.Modified;
                db.SaveChanges();

                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, oldATC, user_id, ActivityType.FETCH_ATC_ATTEMPT_FOR_SWAP_HELPER, newATC, DateTime.Now.ToString());


                tradex.msg = "REJECT IS SUCCESSFUL!!";
                tradex.status = true;

                return Json(tradex);



            }


            return Json(tradex);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult RejectATCArchive(String __RequestVerificationToken, int id, String comment)
        {

            Transaction_Datax trx_Datax = new Transaction_Datax();
            trade_dto tradex = new trade_dto();
            Sto_Transaction_Datax stdx = new Sto_Transaction_Datax();

            var tdaDetails = db.transaction_data_archive.Where(a => a.id == id && a.sales_doc_type == "PENDING").FirstOrDefault();
            if (tdaDetails == null)
            {
                //DO SOMETHING
                //return
            }
            else
            {
                string oldATC = tdaDetails.sales_doc_number;

                tdaDetails.operator_picking = comment;
                tdaDetails.sales_doc_type = "REJECTED";
                tdaDetails.picking_time = DateTime.Now;
                tdaDetails.wb_in = Session["user_id"].ToString();


                db.Entry(tdaDetails).State = EntityState.Modified;
                db.SaveChanges();

                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, oldATC, user_id, ActivityType.REJECT_ARCHIVING_HELPER, oldATC, DateTime.Now.ToString());


                tradex.msg = "ATC ARCHIVE REJECT IS SUCCESSFUL!!";
                tradex.status = true;

                return Json(tradex);



            }


            return Json(tradex);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult RejectGWTRequest(String __RequestVerificationToken, int id, String comment)
        {

            Transaction_Datax trx_Datax = new Transaction_Datax();
            trade_dto tradex = new trade_dto();
            Sto_Transaction_Datax stdx = new Sto_Transaction_Datax();

            var tdaDetails = db.transaction_data_archive.Where(a => a.id == id && a.sales_doc_type == "PENDING").FirstOrDefault();
            if (tdaDetails == null)
            {
                //DO SOMETHING
                //return
            }
            else
            {
                string oldATC = tdaDetails.sales_doc_number;

                tdaDetails.operator_picking = comment;
                tdaDetails.sales_doc_type = "REJECTED";
                tdaDetails.picking_time = DateTime.Now;
                tdaDetails.wb_in = Session["user_id"].ToString();


                db.Entry(tdaDetails).State = EntityState.Modified;
                db.SaveChanges();

                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, oldATC, user_id, ActivityType.REJECT_GWT_CHANGE_HELPER, oldATC, DateTime.Now.ToString());


                tradex.msg = "GROSS WEIGHT CHANGE REJECTED";
                tradex.status = true;

                return Json(tradex);



            }


            return Json(tradex);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult RejectZSTOMoveRequest(String __RequestVerificationToken, int id, String comment)
        {

            Transaction_Datax trx_Datax = new Transaction_Datax();
            trade_dto tradex = new trade_dto();
            Sto_Transaction_Datax stdx = new Sto_Transaction_Datax();

            var tdaDetails = db.transaction_data_archive.Where(a => a.id == id && a.sales_doc_type == "PENDING").FirstOrDefault();
            if (tdaDetails == null)
            {
                //DO SOMETHING
                //return
            }
            else
            {
                string oldATC = tdaDetails.sales_doc_number;

                tdaDetails.operator_picking = comment;
                tdaDetails.sales_doc_type = "REJECTED";
                tdaDetails.picking_time = DateTime.Now;
                tdaDetails.wb_in = Session["user_id"].ToString();


                db.Entry(tdaDetails).State = EntityState.Modified;
                db.SaveChanges();

                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, oldATC, user_id, ActivityType.REJECT_ZSTO_MOVE_HELPER, oldATC, DateTime.Now.ToString());


                tradex.msg = "ZSTO MOVEMENT SWITCH REJECTED";
                tradex.status = true;

                return Json(tradex);



            }


            return Json(tradex);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult RejectTareWeightModification(String __RequestVerificationToken, int id, String comment)
        {

            Transaction_Datax trx_Datax = new Transaction_Datax();
            trade_dto tradex = new trade_dto();
            Sto_Transaction_Datax stdx = new Sto_Transaction_Datax();

            var tdaDetails = db.transaction_data_archive.Where(a => a.id == id && a.sales_doc_type == "PENDING").FirstOrDefault();
            if (tdaDetails == null)
            {
                //DO SOMETHING
                //return
            }
            else
            {
                string atc = tdaDetails.sales_doc_number;

                tdaDetails.operator_picking = comment;
                tdaDetails.sales_doc_type = "REJECTED";
                tdaDetails.picking_time = DateTime.Now;
                tdaDetails.wb_in = Session["user_id"].ToString();


                db.Entry(tdaDetails).State = EntityState.Modified;
                db.SaveChanges();

                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.REJECT_TARE_WEIGHT_MODIFY_HELPER, atc, DateTime.Now.ToString());


                tradex.msg = "TARE MODIFICATION IS REJECTED!!";
                tradex.status = true;

                return Json(tradex);



            }


            return Json(tradex);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DoApproveATCSwapFetch(String __RequestVerificationToken, int id)
        {
            

            Transaction_Datax trx_Datax = new Transaction_Datax();
            var returnedTda = db.transaction_data_archive.Where(a => a.id == id && a.sales_doc_type == "PENDING").FirstOrDefault();

            var user_id = int.Parse(returnedTda.shp_point);

            var operatorUsername = db.users.Where(a => a.Id == user_id).FirstOrDefault();
            var loggedInUser = operatorUsername.username;


            try
            {
                if(returnedTda == null)
                {
                    
                    trx_Datax.status = false;
                    trx_Datax.msg = "Wrong ATC or ATC is already weighed-out";
                    return Json(trx_Datax);





                }
                else
                {
                    trx_Datax.msg = "";
                    trx_Datax.status = true;
                    trx_Datax.atc_no = returnedTda.sales_doc_number;
                    trx_Datax.vehicle = returnedTda.vehicle;
                    trx_Datax.trip_id = returnedTda.trip_id;

                        trx_Datax.tare_time = returnedTda.tare_time ?? DateTime.MinValue;
                    

                    trx_Datax.operatorID = loggedInUser;
                    trx_Datax.seal = returnedTda.seal;

                    return Json(trx_Datax);



                }
            }
            catch (Exception ex)
            {
                
                trx_Datax.msg = "Error occured during ATC fetch";
                trx_Datax.status = false;
                //trx_Datax.excptn = ex.ToString();

                return Json(trx_Datax);
            }
                                             

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DoApproveATCArchiveFetch(String __RequestVerificationToken, int id)
        {


            Transaction_Datax trx_Datax = new Transaction_Datax();
            var returnedTda = db.transaction_data_archive.Where(a => a.id == id && a.sales_doc_type == "PENDING").FirstOrDefault();

            var user_id = int.Parse(returnedTda.shp_point);

            var operatorUsername = db.users.Where(a => a.Id == user_id).FirstOrDefault();
            var loggedInUser = operatorUsername.username;


            try
            {
                if (returnedTda == null)
                {

                    trx_Datax.status = false;
                    trx_Datax.msg = "Wrong ATC or ATC is already weighed-out";
                    return Json(trx_Datax);





                }
                else
                {
                    trx_Datax.msg = "";
                    trx_Datax.status = true;
                    trx_Datax.atc_no = returnedTda.sales_doc_number;

                    trx_Datax.tare_time = returnedTda.tare_time ?? DateTime.MinValue;
                

                    trx_Datax.operatorID = loggedInUser;
                    trx_Datax.seal = returnedTda.seal;

                    return Json(trx_Datax);



                }
            }
            catch (Exception ex)
            {

                trx_Datax.msg = "Error occured during ATC fetch";
                trx_Datax.status = false;
               

                return Json(trx_Datax);
            }


        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DoApproveATCGWTFetch(String __RequestVerificationToken, int id)
        {


            Transaction_Datax trx_Datax = new Transaction_Datax();
            var returnedTda = db.transaction_data_archive.Where(a => a.id == id && a.sales_doc_type == "PENDING").FirstOrDefault();

            var user_id = int.Parse(returnedTda.shp_point);

            var operatorUsername = db.users.Where(a => a.Id == user_id).FirstOrDefault();
            var loggedInUser = operatorUsername.username;


            try
            {
                if (returnedTda == null)
                {

                    trx_Datax.status = false;
                    trx_Datax.msg = "Wrong ATC or Request Already Approved";
                    return Json(trx_Datax);





                }
                else
                {
                    trx_Datax.msg = "";
                    trx_Datax.status = true;
                    trx_Datax.atc_no = returnedTda.sales_doc_number;

                    trx_Datax.tare_time = returnedTda.tare_time ?? DateTime.MinValue;
                   

                    trx_Datax.operatorID = loggedInUser;
                    trx_Datax.seal = returnedTda.seal;
                    trx_Datax.nett = returnedTda.loc;
                    trx_Datax.vehicle = returnedTda.vehicle;
                    trx_Datax.driver = returnedTda.driver;
                    trx_Datax.operator_weighn = returnedTda.bin_1;
                    trx_Datax.operator_weighout = returnedTda.bin_2;



             


                    return Json(trx_Datax);



                }
            }
            catch (Exception ex)
            {

                trx_Datax.msg = "Error occured during transaction fetch";
                trx_Datax.status = false;
              

                return Json(trx_Datax);
            }


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DoApproveZSTOMoveFetch(String __RequestVerificationToken, int id)
        {


            Transaction_Datax trx_Datax = new Transaction_Datax();
            var returnedTda = db.transaction_data_archive.Where(a => a.id == id && a.sales_doc_type == "PENDING").FirstOrDefault();

            var user_id = int.Parse(returnedTda.shp_point);

            var operatorUsername = db.users.Where(a => a.Id == user_id).FirstOrDefault();
            var loggedInUser = operatorUsername.username;


            try
            {
                if (returnedTda == null)
                {

                    trx_Datax.status = false;
                    trx_Datax.msg = "Wrong DELIVERY or Request Already Approved";
                    return Json(trx_Datax);
                    
                }
                else
                {
                    trx_Datax.msg = "";
                    trx_Datax.status = true;
                    trx_Datax.atc_no = returnedTda.sales_doc_number;

                    trx_Datax.tare_time = returnedTda.tare_time ?? DateTime.MinValue;


                    trx_Datax.operatorID = loggedInUser;
                    trx_Datax.seal = returnedTda.seal;
                    trx_Datax.sto_loc = returnedTda.loc;
                    //trx_Datax.nett = returnedTda.loc;
                    trx_Datax.vehicle = returnedTda.vehicle;
                    trx_Datax.driver = returnedTda.driver;
                    trx_Datax.operator_weighn = returnedTda.bin_1;
                    trx_Datax.transporter = returnedTda.transporter;
                    trx_Datax.gross = returnedTda.gross.ToString();
                    trx_Datax.tare = returnedTda.tare.ToString();
                    trx_Datax.waybill_no = returnedTda.tmp_waybill_no;
                    trx_Datax.sales_type = returnedTda.sales_type;
                    //trx_Datax.operator_weighout = returnedTda.bin_2;






                    return Json(trx_Datax);



                }
            }
            catch (Exception ex)
            {

                trx_Datax.msg = "Error occured during transaction fetch. Please Contact IT";
                trx_Datax.status = false;


                return Json(trx_Datax);
            }


        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DoApproveTareModifFetch(String __RequestVerificationToken, int id)
        {


            Transaction_Datax trx_Datax = new Transaction_Datax();
            var returnedTda = db.transaction_data_archive.Where(a => a.id == id && a.sales_doc_type == "PENDING").FirstOrDefault();

            var user_id = int.Parse(returnedTda.shp_point);

            var operatorUsername = db.users.Where(a => a.Id == user_id).FirstOrDefault();
            var loggedInUser = operatorUsername.username;


            try
            {
                if (returnedTda == null)
                {

                    trx_Datax.status = false;
                    trx_Datax.msg = "Wrong ATC or ATC is already weighed-out";
                    return Json(trx_Datax);





                }
                else
                {
                    trx_Datax.msg = "";
                    trx_Datax.status = true;
                    trx_Datax.atc_no = returnedTda.sales_doc_number;

                    trx_Datax.tare_time = returnedTda.tare_time ?? DateTime.MinValue;
                   

                    trx_Datax.operatorID = loggedInUser;
                    trx_Datax.seal = returnedTda.seal;

                    return Json(trx_Datax);



                }
            }
            catch (Exception ex)
            {

                trx_Datax.msg = "Error occured during ATC fetch";
                trx_Datax.status = false;
                

                return Json(trx_Datax);
            }


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DoFetchATCDetailsGate(String __RequestVerificationToken, String atc)
        {
            
            Gate_Datax gate_Datax = new Gate_Datax();

            string atc_actual = atc;
            string msge;



            try
            {
                var atcDetails = db.gateaccess_info.Where(x => x.atc_no == atc && x.status != "Weigh-Out Successful." && x.status != "CANCELLED").FirstOrDefault();
                if (atcDetails == null)
                {

                    int user_id = int.Parse(Session["user_id"].ToString());
                    TransactionLogging TRX_LOG = new TransactionLogging();
                    bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.FETCH_ATC_ATTEMPT_HELPER, "FETCH_ATC_ATTEMPT GATE : ", DateTime.Now.ToString());

                    gate_Datax.status = false;
                    gate_Datax.msg = "Wrong ATC or ATC already weighed-out";
                    return Json(gate_Datax);
                }
                else
                {
                    gate_Datax.msg = "";
                    gate_Datax.status = true;
                    gate_Datax.transporter = atcDetails.transporter;
                    gate_Datax.statusDB = atcDetails.status;
                    gate_Datax.destination = atcDetails.destination;
                    gate_Datax.driverName = atcDetails.driver;
                    gate_Datax.transType = atcDetails.transaction_type;
                    gate_Datax.ShippingPoint = atcDetails.shipping_point;
                    gate_Datax.License = atcDetails.license;
                    gate_Datax.customer = atcDetails.customer;
                    gate_Datax.truckNumber = atcDetails.truck_no;
                    gate_Datax.product = atcDetails.fleet_no;
                    gate_Datax.parentSO = atcDetails.parent_so;
                    gate_Datax.atc = atc;

                    int user_id = int.Parse(Session["user_id"].ToString());
                    TransactionLogging TRX_LOG = new TransactionLogging();
                    bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.FETCH_ATC_ATTEMPT_HELPER, "FETCH_ATC_ATTEMPT GATE : ", DateTime.Now.ToString());

                    return Json(gate_Datax);
                }


            }
            catch (Exception ex)
            {
                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.FETCH_ATC_ATTEMPT_HELPER, "FETCH_ATC_ATTEMPT GATE: ", DateTime.Now.ToString());

                msge = ex.Message;
                gate_Datax.msg = "Error occured somewhere";
                gate_Datax.status = false;
                gate_Datax.excptn = ex.ToString();

                return Json(gate_Datax);
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DoUpdateDriver(String __RequestVerificationToken, String atc, String driver)
        {
            trade_dto tradex = new trade_dto();
            transaction_dto tdx = new transaction_dto();
            atc_datax atc_Datax = new atc_datax();
            decimal atcEntered;
            Decimal.TryParse(atc, out atcEntered);

            var tradeDetails = db.Trades.Where(x => x.sparenum2 == atcEntered).FirstOrDefault();
            var atcDetails = db.atcs.Where(x => x.sales_doc_number == atc).FirstOrDefault();
            var transDataDetails = db.transaction_data.Where(x => x.sales_doc_number == atc && x.sync_status != true).FirstOrDefault();

            var transDataDetals = db.transaction_data.Where(a => a.sales_doc_number == atc && a.gross_time != null).FirstOrDefault();

            if (transDataDetals != null)
            {
                atc_Datax.msg = "ATC has been USED and WEIGHED-OUT!!!";
                atc_Datax.status = false;
                return Json(atc_Datax);

            }

            try
            {
                if (tradeDetails != null)
                {
                    tradeDetails.sparestr1 = driver;
                    tradeDetails.sparestr5 = driver;
                    db.Entry(tradeDetails).State = EntityState.Modified;
                    db.SaveChanges();

                    if (atcDetails != null)
                    {
                        atcDetails.drivers_name = driver;
                        db.Entry(atcDetails).State = EntityState.Modified;
                        db.SaveChanges();

                        int user_id = int.Parse(Session["user_id"].ToString());
                        TransactionLogging TRX_LOG = new TransactionLogging();
                        bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.UPDATE_DRIVER_HELPER, "UPDATE_DRIVER_HELPER : ", DateTime.Now.ToString());
                    }

                    tradex.msg = "Driver Updated Successfully";
                    tradex.status = true;
                    return Json(tradex);
                }
                else if (tradeDetails == null)
                {
                    if (transDataDetails != null)
                    {
                        transDataDetails.driver = driver;
                        db.Entry(transDataDetails).State = EntityState.Modified;
                        db.SaveChanges();

                        int user_id = int.Parse(Session["user_id"].ToString());
                        TransactionLogging TRX_LOG = new TransactionLogging();
                        bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.UPDATE_DRIVER_HELPER, "UPDATE_DRIVER_HELPER : ", DateTime.Now.ToString());

                        tdx.msg = "Driver Updated Successfully";
                        tdx.status = true;
                        return Json(tdx);
                    }
                }
                else
                {
                    tradex.msg = "ATC not found, please ensure ATC is correct";
                    tradex.status = false;
                    return Json(tradex);
                }
            }
            catch (Exception ex)
            {
                tradex.msg = "Something went wrong, plese contact I.T";
                tradex.status = false;
                return Json(tradex);
            }



            tradex.msg = "ATC not found, please ensure ATC is correct";
            tradex.status = false;
            return Json(tradex);
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DoUpdateTrip(String __RequestVerificationToken, String trip)
        {
            trade_dto tradex = new trade_dto();
            transaction_dto tdx = new transaction_dto();
            trip_registry_dto tr = new trip_registry_dto();
            atc_datax atc_Datax = new atc_datax();


            var tripRegistryDetails = db.trip_registry.Where(x => (x.tms_online_status == "FUEL_POSTED" || x.tms_online_status == "CLOSED") && (x.tms_status == "FUEL_POSTED" || x.tms_online_status == "CLOSED") && x.atc_no == trip).FirstOrDefault();

            try
            {
                if (tripRegistryDetails == null)
                {
                    tr.msg = "WRONG ATC ID ENTERED";
                    tr.status = false;
                    return Json(tr);
                }
                else
                {
                    tripRegistryDetails.tms_status = "APPROVED";
                    tripRegistryDetails.tms_online_status = "APPROVED";
                    db.Entry(tripRegistryDetails).State = EntityState.Modified;
                    db.SaveChanges();

                    int user_id = int.Parse(Session["user_id"].ToString());
                    TransactionLogging TRX_LOG = new TransactionLogging();
                    bool status = TRX_LOG.RecordLog(db, trip, user_id, ActivityType.UPDATE_TRIP_HELPER, "UPDATE_TRIP_HELPER : ", DateTime.Now.ToString());

                    tr.msg = "TRIP VALIDATED";
                    tr.status = true;
                    return Json(tr);
                }
            }
            catch (Exception ex)
            {
                tr.msg = "Something went wrong, plese contact I.T";
                tr.status = false;
                return Json(tr);
            }





            tr.msg = "TRIP not found, please ensure ATC is correct";
            tr.status = false;
            return Json(tr);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DoCancelDDSTrip(String __RequestVerificationToken, String trip)
        {
           

            transaction_data tripRegistry = new transaction_data();
            Trip_Registryx tripCancelDetail = new Trip_Registryx();

            try
            {
                var tripRegistryDetails = db.trip_registry.Where(x => x.trip_id_sap == trip).FirstOrDefault();
                if (tripRegistryDetails == null)
                {
                    tripCancelDetail.msg = "WRONG TRIP ID ENTERED!!!";
                    tripCancelDetail.status = false;
                    return Json(tripCancelDetail);
                }

                var tripRegistryCancelled = db.trip_registry.Where(x => x.trip_id_sap == trip && x.tms_status == "CANCELLED").FirstOrDefault();
                if (tripRegistryCancelled != null)
                {
                    tripCancelDetail.msg = "TRIP IS ALREADY CANCELLED!!!";
                    tripCancelDetail.status = false;
                    return Json(tripCancelDetail);
                }

                string atc = tripRegistryDetails.atc_no;
                var tripATC = db.transaction_data.Where(x => x.sales_doc_number == atc).FirstOrDefault();
                if (tripATC == null)
                {


                    tripRegistryDetails.atc_no = "C" + tripRegistryDetails.atc_no.Substring(1);

                    tripRegistryDetails.tms_online_status = "CANCELLED";

                    tripRegistryDetails.tms_status = "CANCELLED";

                    tripRegistryDetails.ship_to_party_sap = "C" + tripRegistryDetails.atc_no.Substring(1);

                    db.Entry(tripRegistryDetails).State = EntityState.Modified;
                    db.SaveChanges();



                    int user_id = int.Parse(Session["user_id"].ToString());
                    TransactionLogging TRX_LOG = new TransactionLogging();
                    bool status = TRX_LOG.RecordLog(db, trip, user_id, ActivityType.CANCEL_TRIP_HELPER, "CANCEL_TRIP_HELPER: ", DateTime.Now.ToString());


                    tripCancelDetail.msg = "SUCCESSFULL!!! TRIP IS CANCELLED ONLY ON DDS";
                    tripCancelDetail.status = true;
                    return Json(tripCancelDetail);
                }
                else
                {
                    tripCancelDetail.msg = "CANCELLATION FAILED!!! ATC IS WEIGHED IN ALREADY";
                    tripCancelDetail.status = false;
                    return Json(tripCancelDetail);

                }
            }
            catch (Exception ex)
            {
                tripCancelDetail.msg = "CANCELLATION FAILED!!! SOMETHING WENT WRONG";
                tripCancelDetail.status = false;
                return Json(tripCancelDetail);

            }



        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DoUpdateOfflineTripTForRepush(String __RequestVerificationToken, String atc)
        {
            trade_dto tradex = new trade_dto();
            transaction_dto tdx = new transaction_dto();
            trip_registry_dto tr = new trip_registry_dto();
            atc_datax atc_Datax = new atc_datax();


            var offlineTrip = db.transaction_data.Where(x => x.sync_status == false && x.gross != null && x.gross_time != null && x.sales_doc_number == atc && x.error_field == "OFFLINE").FirstOrDefault();

            try
            {
                if (offlineTrip == null)
                {
                    tr.msg = "WRONG ATC ID ENTERED or ATC NOT SHOWING 'OFFLINE'!!!";
                    tr.status = false;
                    return Json(tr);
                }
                else
                {
                    offlineTrip.error_field = "Repush";
                    db.Entry(offlineTrip).State = EntityState.Modified;
                    db.SaveChanges();

                    int user_id = int.Parse(Session["user_id"].ToString());
                    TransactionLogging TRX_LOG = new TransactionLogging();
                    bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.UPDATE_OFFLINE_TRIP_for_REPUSH_HELPER, "UPDATE_OFFLINE_TRIP_for_REPUSH_HELPER : ", DateTime.Now.ToString());

                    tr.msg = "REPUSH ATC FROM POSTING-ERRORS PAGE";
                    tr.status = true;
                    return Json(tr);
                }
            }
            catch (Exception ex)
            {
                tr.msg = "Something went wrong, plese contact I.T";
                tr.status = false;
                return Json(tr);
            }


            tr.msg = "ATC not found, please ensure ATC is correct";
            tr.status = false;
            return Json(tr);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DoRemoveTripIDForCTES(String __RequestVerificationToken, String atc)
        {
            trade_dto tradex = new trade_dto();
            transaction_dto tdx = new transaction_dto();
            trip_registry_dto tr = new trip_registry_dto();
            atc_datax atc_Datax = new atc_datax();


            var offlineTrip = db.transaction_data.Where(x => x.sync_status == false && x.gross != null && x.gross_time != null && x.sales_doc_number == atc && x.error_field == "Invalid Trip ID and Delivery combination").FirstOrDefault();

            try
            {
                if (offlineTrip == null)
                {
                    tr.msg = "WRONG ATC ID ENTERED or ATC NOT WEIGHED OUT or NO INVALID COMBINATION!!!";
                    tr.status = false;
                    return Json(tr);
                }
                else
                {
                    
                    transaction_data_archive t_data_archive = new transaction_data_archive();
                    t_data_archive.trip_id = offlineTrip.trip_id;
                    t_data_archive.sales_doc_number = offlineTrip.sales_doc_number;
                    t_data_archive.message_field = "archived after changing trip via DDS HELPER";

                    db.transaction_data_archive.Add(t_data_archive);
                    db.SaveChanges();

                    int user_id = int.Parse(Session["user_id"].ToString());
                    TransactionLogging TRX_LOG = new TransactionLogging();
                    bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.REMOVE_CTES_TRIP_HELPER, "REMOVE_CTES_TRIP_HELPER: ", DateTime.Now.ToString());




                    offlineTrip.error_field = "Repush";
                    offlineTrip.trip_id = "0000000000";
                    db.Entry(offlineTrip).State = EntityState.Modified;
                    db.SaveChanges();

                    tr.msg = "REPUSH ATC FROM POSTING-ERRORS PAGE";
                    tr.status = true;
                    return Json(tr);
                }
            }
            catch (Exception ex)
            {
                tr.msg = "Something went wrong, plese contact I.T";
                tr.status = false;
                return Json(tr);
            }


            tr.msg = "ATC not found, please ensure ATC is correct";
            tr.status = false;
            return Json(tr);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DoUpdateOnlineTrip(String __RequestVerificationToken, String trip, String atc)
        {
            trade_dto tradex = new trade_dto();
            transaction_dto tdx = new transaction_dto();
            trip_registry_dto tr = new trip_registry_dto();
            atc_datax atc_Datax = new atc_datax();


            var tripRegistryDetails = db.trip_registry.Where(x => x.tms_online_status == null && x.atc_no == atc).FirstOrDefault();



            try
            {
                if (tripRegistryDetails == null)
                {
                    tr.msg = "WRONG ATC NUMBER ENTERED";
                    tr.status = false;
                    return Json(tr);
                }
                else
                {

                    tripRegistryDetails.tms_online_status = "APPROVED";
                    tripRegistryDetails.trip_id_sap = trip;
                    db.Entry(tripRegistryDetails).State = EntityState.Modified;
                    db.SaveChanges();

                    int user_id = int.Parse(Session["user_id"].ToString());
                    TransactionLogging TRX_LOG = new TransactionLogging();
                    bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.UPDATE_ONLINE_TRIP_HELPER, "UPDATE_ONLINE_TRIP_HELPER: ", DateTime.Now.ToString());

                    tr.msg = "ONLINE TRIP ID ADDED";
                    tr.status = true;
                    return Json(tr);
                }
            }
            catch (Exception ex)
            {
                tr.msg = "Something went wrong, plese contact I.T";
                tr.status = false;
                return Json(tr);
            }






            tr.msg = "Please ensure ATC is correct";
            tr.status = false;
            return Json(tr);
        }



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LoadReportData()
      
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();

                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

                //Paging Size (10,20,50,100)    
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                // Getting all Customer data    
                var customerData = (from tempcustomer in db.gateaccess_info
                                    select tempcustomer);

                //Sorting    
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    //customerData = customerData.OrderBy(sortColumn + " " + sortColumnDir);


                }
                //Search    
                if (!string.IsNullOrEmpty(searchValue))
                {
                    customerData = customerData.Where(m => m.atc_no == searchValue);
                }

                //total number of rows count     
                recordsTotal = customerData.Count();
                //Paging     
                var data = customerData.Skip(skip).Take(pageSize).ToList();
                //Returning Json Data    
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
            
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DoUpdateDestination(String __RequestVerificationToken, String atc, String destination)
        {
            trade_dto tradex = new trade_dto();
            transaction_dto tdx = new transaction_dto();
            atc_datax atc_Datax = new atc_datax();
            decimal atcEntered;
            Decimal.TryParse(atc, out atcEntered);

            var tradeDetails = db.Trades.Where(x => x.sparenum2 == atcEntered).FirstOrDefault();
            var transDataDetails = db.transaction_data.Where(x => x.sales_doc_number == atc && x.gross_time == null).FirstOrDefault();
            var atcDetails = db.atcs.Where(x => x.sales_doc_number == atc).FirstOrDefault();


            try
            {
                if (tradeDetails != null)
                {
                    tradeDetails.sparestr2 = destination;
                    tradeDetails.receiver = destination;
                    tradeDetails.receivercode = destination;
                    db.Entry(tradeDetails).State = EntityState.Modified;
                    db.SaveChanges();

                    int user_id = int.Parse(Session["user_id"].ToString());
                    TransactionLogging TRX_LOG = new TransactionLogging();
                    bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.UPDATE_DESTINATION_HELPER, "UPDATE_DESTINATION_HELPER: ", DateTime.Now.ToString());

                    if (atcDetails != null)
                    {
                        
                    }

                    tradex.msg = "Destination Updated Successfully";
                    tradex.status = true;
                    return Json(tradex);
                }
                else if (tradeDetails == null)
                {
                    if (transDataDetails != null)
                    {
                        transDataDetails.destination = destination;
                        db.Entry(transDataDetails).State = EntityState.Modified;
                        db.SaveChanges();

                        int user_id = int.Parse(Session["user_id"].ToString());
                        TransactionLogging TRX_LOG = new TransactionLogging();
                        bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.UPDATE_DESTINATION_HELPER, "UPDATE_DESTINATION_HELPER: ", DateTime.Now.ToString());

                        tdx.msg = "Destination Updated Successfully";
                        tdx.status = true;
                        return Json(tdx);
                    }
                }
                else
                {
                    tradex.msg = "ATC not found, please ensure ATC is correct";
                    tradex.status = false;
                    return Json(tradex);
                }
            }
            catch (Exception ex)
            {
                tradex.msg = "Something went wrong, plese contact I.T";
                tradex.status = false;
                return Json(tradex);
            }



            tradex.msg = "ATC not found, please ensure ATC is correct";
            tradex.status = false;
            return Json(tradex);
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DoUpdateReceiver(String __RequestVerificationToken, String receiver, String atc)
        {
            atc_datax atc_Datax = new atc_datax();

            string atc_actual = atc;
            string receiver_actual = receiver;

            string msge;

            var transDataDetals = db.transaction_data.Where(a => a.sales_doc_number == atc && a.gross_time != null).FirstOrDefault();

            if (transDataDetals != null)
            {
                atc_Datax.msg = "ATC has been USED and WEIGHED-OUT!!!";
                atc_Datax.status = false;
                return Json(atc_Datax);

            }



            try
            {
              
                var atcDet = db.atc_data.Where(x => x.sales_doc_number == atc);
                if (atcDet == null)
                {
                    atc_Datax.msg = "invalid ATC entered";
                    atc_Datax.status = false;
                    return Json(atc_Datax);
                }

                atcDet = db.atc_data.Where(x => x.sales_doc_number == atc && x.used == null);
                if (atcDet == null)
                {
                    atc_Datax.msg = "ATC is already used";
                    atc_Datax.status = false;
                    return Json(atc_Datax);
                }

                atcDet = db.atc_data.Where(x => x.sales_doc_number == atc && x.used == null && x.customer_name != null);
                if (atcDet == null)
                {
                    atc_Datax.msg = "customer name already exist for the ATC";
                    atc_Datax.status = false;
                    return Json(atc_Datax);
                }
                var atcDetails = db.atc_data.Where(x => x.sales_doc_number == atc && x.customer_name == "" && x.used == null).FirstOrDefault();






                if (atcDetails != null)
                {
                    atcDetails.customer_name = receiver;
                    db.Entry(atcDetails).State = EntityState.Modified;
                    db.SaveChanges();

                    int user_id = int.Parse(Session["user_id"].ToString());
                    TransactionLogging TRX_LOG = new TransactionLogging();
                    bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.UPDATE_RECEIVER_HELPER, "UPDATE_RECEIVER_HELPER: ", DateTime.Now.ToString());



                    atc_Datax.msg = "Receiver/Customer Updated Successfully";
                    atc_Datax.status = true;
                    return Json(atc_Datax);


                }
                else
                {
                    atc_Datax.msg = "Invalid ATC or Customer Already Exists!";
                    atc_Datax.status = false;
                    return Json(atc_Datax);
                }

            }
            catch (Exception ex)
            {

                msge = ex.Message;
                atc_Datax.msg = "Error occured somewhere. cal on I.T";
                atc_Datax.status = false;
                return Json(atc_Datax);
            }


        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public JsonResult Login(string username, string password, string returnUrl)
        {
            String BaseUrl = Request.Url.GetLeftPart(UriPartial.Authority);
            Loginx loginx = new Loginx();



            if (Session["num_of_tries"] != null)
            {
                int num_of_tries = (int)Session["num_of_tries"];
                if (num_of_tries >= 3)
                {

                    // invalid username or password
                    ModelState.AddModelError("", "You have been locked out. Please contact administrator for access.");

                    user current_user = db.users.Where(a => a.username == username).FirstOrDefault();

                    TransactionLogging TRX_LOG = new TransactionLogging();
                    String msg = username + " has been locked out!!";
                    if (current_user != null)
                    {
                        current_user.active = false;
                        db.Entry(current_user).State = EntityState.Modified;
                        db.SaveChanges();

                        int user_id = current_user.Id;

                        bool status1 = TRX_LOG.RecordLog(db, "", user_id, ActivityType.LOCK_OUT, msg, "");

                    }
                    else
                    {
                        bool status1 = TRX_LOG.RecordLog(db, "", 0, ActivityType.LOCK_OUT, msg, "");
                    }
                    Session["num_of_tries"] = 0;

                    loginx.msg = "has been locked out!!";
                    loginx.status = false;
                    return Json(loginx);


                }
            }
            try
            {
                if (new UserManager().IsValid(username, password, BaseUrl))
                {

                    UserManager UM = new UserManager();
                    UM.FillUserInfo(username);
                    int user_id = UM.user_id;



                    Session["username"] = username;
                    Session["user_id"] = user_id;
                    //Session["Role_id"] = role_id;



                    Session["change_password"] = UM.change_password;
                    user userx = db.users.FirstOrDefault(u => u.username == username);
                    string role = userx.role_id.ToString();

                    bool ca = false;
                    bool ad = false;

                    if (userx.can_approve == true)
                    {
                        ca = true;

                    }
                    Session["can_approve"] = ca;
                    if (userx.admin == true)
                    {
                        ad = true;

                    }
                    Session["admin"] = ad;
                    Session["Role"] = role;



                    Session["canApproveTare"] = "cnat";
                    string approvers = Properties.Settings.Default.CheckerUser;

                    var tareApproverUsers = approvers.Split(',');
                    foreach (var approver in tareApproverUsers)
                    {
                        if (approver.Trim() != "" && username.ToUpper() == approver)
                        {
                            
                            Session["canApproveTare"] = "cat";
                                                                          
                        }
                       
                    }

                 
                    Session["company_name"] = "Dangote Cement Tanzania";



                    TransactionLogging TRX_LOG = new TransactionLogging();
                    bool status = TRX_LOG.RecordLog(db, "N/A", user_id, ActivityType.LOGIN_HELPER, "Login_HELPER : ", DateTime.Now.ToString());

                    Session["LoginStatus"] = "LoggedIn";
                    var loggedInStatus = Session["LoginStatus"];

                    loginx.msg = "";
                    loginx.status = true;
                    return Json(loginx);


                }
                else
                {

                    loginx.msg = "Invalid or blocked username/password";
                    loginx.status = false;
                    loginx.excptn = "Invalid or blocked username/password";
                    return Json(loginx);
                }
            }
            catch (Exception ex)
            {
                loginx.msg = "Invalid or blocked username/password";
                loginx.status = false;
                loginx.excptn = ex.Message;
                return Json(loginx);
            }


            if (Session["num_of_tries"] == null)
            {
                Session["num_of_tries"] = 0;
            }
            Session["num_of_tries"] = (int)Session["num_of_tries"] + 1;

            ModelState.AddModelError("", "invalid username or password");

        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Default", "Home");
        }


        [HttpPost]
        public JsonResult LoginOff(string logoutVar)
        {
            Loginx loginx = new Loginx();
            loginx.status = true;

            int user_id = int.Parse(Session["user_id"].ToString());
            TransactionLogging TRX_LOG = new TransactionLogging();
            bool status = TRX_LOG.RecordLog(db, "N/A", user_id, ActivityType.LOGOUT_HELPER, "LOGOUT_HELPER: ", DateTime.Now.ToString());

            Session.Clear();


            return Json(loginx);
        }


        public ActionResult GetReportData()
        {
            
            try
            {
                var returnedReport = db.gateaccess_info.ToList<gateaccess_info>();
                return Json(new { data = returnedReport }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Gate_Datax gate_Datax = new Gate_Datax();
                gate_Datax.msg = "Something went wrong";
                gate_Datax.status = false;

                return Json(gate_Datax);
            }
        }

        public ActionResult GetReportDataFilter(DateTime fromDate, DateTime toDate, string trxType, string product, string truckStatus)
        {
           
            try
            {
                var returnedReport = db.gateaccess_info.Where(a => a.entry_datetime >= fromDate && a.entry_datetime <= toDate).OrderBy(s => s.entry_datetime).ToList<gateaccess_info>();


                if (product != null)
                {
                    returnedReport = returnedReport.Where(a => a.fleet_no == product).ToList();
                }
                if (truckStatus != "-- select --")
                {
                    returnedReport = returnedReport.Where(a => a.status == truckStatus).ToList();
                }

                if (trxType != "-- select --")
                {
                    returnedReport = returnedReport.Where(a => a.transaction_type == trxType).ToList();
                }

                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, "0000000000", user_id, ActivityType.GATE_REPORT_SPOOL_HELPER, "GATE REPORT SPOOL: ", DateTime.Now.ToString());

                return Json(new { data = returnedReport }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Gate_Datax gate_Datax = new Gate_Datax();
                gate_Datax.msg = "Something went wrong";
                gate_Datax.status = false;

                return Json(gate_Datax);
            }
        }


        public ActionResult GetMaterialReport(DateTime fromDate, DateTime toDate, String product)
        {
           
            try
            {
                trade_dto stdx = new trade_dto();
                List<Sto_Transaction_Datax> materialReport = new List<Sto_Transaction_Datax>();
                var returnedReport = db.gateaccess_info.Where(a => a.entry_datetime >= fromDate && a.entry_datetime <= toDate).OrderBy(s => s.entry_datetime).ToList<gateaccess_info>();

                
                if (product == null || product.Length == 0)
                {
                    var allMaterialsReturned = db.Truck_Analysis_Material_only(fromDate, toDate).ToList();

                    int user_id = int.Parse(Session["user_id"].ToString());
                    TransactionLogging TRX_LOG = new TransactionLogging();
                    bool status = TRX_LOG.RecordLog(db, "0000000000", user_id, ActivityType.MATERIAL_REPORT_SPOOL_HELPER, "MATERIAL REPORT SPOOL HELPER: ", DateTime.Now.ToString());

                    return Json(new { data = allMaterialsReturned }, JsonRequestBehavior.AllowGet);
                 
                }
                else
                {
                    var allMaterialsReturned = db.Truck_Analysis_Material_only(fromDate, toDate).Where(a => a.PRODUCT == product).ToList();
                    int user_id = int.Parse(Session["user_id"].ToString());
                    TransactionLogging TRX_LOG = new TransactionLogging();
                    bool status = TRX_LOG.RecordLog(db, "0000000000", user_id, ActivityType.MATERIAL_REPORT_SPOOL_HELPER, "MATERIAL REPORT SPOOL HELPER: ", DateTime.Now.ToString());

                    return Json(new { data = allMaterialsReturned }, JsonRequestBehavior.AllowGet);
                }
                

                

            }
            catch (Exception ex)
            {
                Gate_Datax gate_Datax = new Gate_Datax();
                gate_Datax.msg = "Something went wrong";
                gate_Datax.status = false;

                return Json(gate_Datax);
            }
        }


        public ActionResult GetPendingApprovalRequestForSwap()
        {

            db.Configuration.ProxyCreationEnabled = false;
            try
            {
                

                var trxDataArc = db.transaction_data_archive.Where(a => a.operator_weighin == "ATC SWAP REQUEST" && a.sales_doc_type == "PENDING").ToList();
                var allUsers = db.users.ToList();
                var returnedReportPendingSwap = from b in trxDataArc
                                                join v in allUsers on b.shp_point equals v.Id.ToString()
                                                select new { b, v };

                returnedReportPendingSwap = returnedReportPendingSwap.ToList();


                return Json(new { data = returnedReportPendingSwap }, JsonRequestBehavior.AllowGet);



            }
            catch (Exception ex)
            {
                Gate_Datax gate_Datax = new Gate_Datax();
                gate_Datax.msg = "Something during data spool";
                gate_Datax.status = false;

                return Json(gate_Datax);
            }
        }


        public ActionResult GetPendingApprovalRequestForTareModify()
        {
            
            db.Configuration.ProxyCreationEnabled = false;
            try
            {

             

                var trxDataArc = db.transaction_data_archive.Where(a => a.operator_weighin == "TARE WEIGHT CHANGE REQUEST" && a.sales_doc_type == "PENDING").ToList();
                var allUsers = db.users.ToList();
                var returnedReportPendingTareModify = from b in trxDataArc
                                                join v in allUsers on b.shp_point equals v.Id.ToString()
                                                select new { b, v };

                returnedReportPendingTareModify = returnedReportPendingTareModify.ToList();


                return Json(new { data = returnedReportPendingTareModify }, JsonRequestBehavior.AllowGet);



            }
            catch (Exception ex)
            {
                Gate_Datax gate_Datax = new Gate_Datax();
                gate_Datax.msg = "Something went wrong during data spool";
                gate_Datax.status = false;

                return Json(gate_Datax);
            }
        }


        public ActionResult GetPendingArchivingRequestForSwap()
        {
            

            db.Configuration.ProxyCreationEnabled = false;
            try
            {

            

                var trxDataArc = db.transaction_data_archive.Where(a => a.operator_weighin == "ATC ARCHIVING REQUEST" && a.sales_doc_type == "PENDING").ToList();
                var allUsers = db.users.ToList();
                var returnedReportPendingArchive = from b in trxDataArc
                                                join v in allUsers on b.shp_point equals v.Id.ToString()
                                                select new { b, v };

                returnedReportPendingArchive = returnedReportPendingArchive.ToList();


                return Json(new { data = returnedReportPendingArchive }, JsonRequestBehavior.AllowGet);



            }
            catch (Exception ex)
            {
                Gate_Datax gate_Datax = new Gate_Datax();
                gate_Datax.msg = "Something during data spool";
                gate_Datax.status = false;

                return Json(gate_Datax);
            }
        }


        public ActionResult GetPendingGrossWTRequestForSwap()
        {

            db.Configuration.ProxyCreationEnabled = false;
            try
            {
                
                var trxDataArc = db.transaction_data_archive.Where(a => a.operator_weighin == "ADDITIONAL GROSS WEIGHT MODIF REQUEST" && a.sales_doc_type == "PENDING").ToList();
                var allUsers = db.users.ToList();
                var returnedReportPendingArchive = from b in trxDataArc
                                                   join v in allUsers on b.shp_point equals v.Id.ToString()
                                                   select new { b, v };

                returnedReportPendingArchive = returnedReportPendingArchive.ToList();


                return Json(new { data = returnedReportPendingArchive }, JsonRequestBehavior.AllowGet);



            }
            catch (Exception ex)
            {
                Gate_Datax gate_Datax = new Gate_Datax();
                gate_Datax.msg = "Something during data spool";
                gate_Datax.status = false;

                return Json(gate_Datax);
            }
        }


        public ActionResult GetPendingMatToZSTORequestForMovement()
        {

            db.Configuration.ProxyCreationEnabled = false;
            try
            {
                
                var trxDataZSTO = db.transaction_data_archive.Where(a => a.operator_weighin == "MOVE FROM MATERIAL TO ZSTO" && a.sales_doc_type == "PENDING").ToList();
                var allUsers = db.users.ToList();
                var returnedReportPendingZSTOMovement = from b in trxDataZSTO
                                                        join v in allUsers on b.shp_point equals v.Id.ToString()
                                                   select new { b, v };

                returnedReportPendingZSTOMovement = returnedReportPendingZSTOMovement.ToList();


                return Json(new { data = returnedReportPendingZSTOMovement }, JsonRequestBehavior.AllowGet);



            }
            catch (Exception ex)
            {
                Gate_Datax gate_Datax = new Gate_Datax();
                gate_Datax.msg = "Something went wrong during data spool";
                gate_Datax.status = false;

                return Json(gate_Datax);
            }
        }


        public ActionResult GetTripReportDataFilter(DateTime fromDate, DateTime toDate, string trxType, string driver, string truckStatus, string Vehicle, string createdBy)
        {

            db.Configuration.ProxyCreationEnabled = false;

            try
            {
                if (trxType == "Trip Creation")
                {
                    var transactions_SAP1 = from s in db.trip_registry.Where(a => a.creation_datetime >= fromDate && a.creation_datetime <= toDate)
                                            join t in db.transaction_log on s.atc_no equals t.atc
                                            join u in db.users on t.user_id equals u.Id
                                            select new { s, t, u };

                    //transactions_SAP1 = transactions_SAP1.Where(a => a.activity = "CREATE_TRIP" )

                    var trip_reg = db.trip_registry.Where(a => a.creation_datetime >= fromDate && a.creation_datetime <= toDate).ToList();
                    var trans_log = db.transaction_log.Where(a => a.activity == "CREATE_TRIP" && a.trx_date >= fromDate && a.trx_date <= toDate).ToList();
                    var user_data = db.users.ToList();
                    var returnedReportTripCreate = from s in trip_reg
                                                   join t in trans_log on s.atc_no equals t.atc
                                                   join u in user_data on t.user_id equals u.Id
                                                   select new { s, t, u };

                    returnedReportTripCreate = returnedReportTripCreate.ToList();

                    return Json(new { data = returnedReportTripCreate }, JsonRequestBehavior.AllowGet);


                }

                Gate_Datax gate_Datax = new Gate_Datax();
                gate_Datax.status = true;

                return Json(gate_Datax);




            }
            catch (Exception ex)
            {
                Gate_Datax gate_Datax = new Gate_Datax();
                gate_Datax.msg = "Something went wrong";
                gate_Datax.status = false;

                return Json(gate_Datax);
            }
        }


        [HttpGet]
        public ActionResult GetTripDetailsReportDataFilter(DateTime fromDate, DateTime toDate, string trxType, string driver, string truckStatus, string Vehicle, string createdBy)
        {

            db.Configuration.ProxyCreationEnabled = false;
            if (trxType == "Trip Creation")
            {
                try
                {
                    var returnedReport = db.trip_registry.Where(a => a.creation_datetime >= fromDate && a.creation_datetime <= toDate && a.trip_no.StartsWith("38")).ToList<trip_registry>();



                    if (Vehicle != "")
                    {
                        returnedReport = returnedReport.Where(a => a.fleet == Vehicle).ToList();
                    }
                    if (truckStatus != "-- select --")
                    {
                        returnedReport = returnedReport.Where(a => a.tms_status == truckStatus).ToList();
                    }

                    if (driver != "")
                    {
                        returnedReport = returnedReport.Where(a => a.driver_id_sap == driver).ToList();
                    }

                    int user_id = int.Parse(Session["user_id"].ToString());
                    TransactionLogging TRX_LOG = new TransactionLogging();
                    bool status = TRX_LOG.RecordLog(db, "0000000000", user_id, ActivityType.TRIP_CREATE_REPORT_SPOOL_HELPER, "TRIP REPORT SPOOL: ", DateTime.Now.ToString());

                    return Json(new { data = returnedReport }, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    Gate_Datax gate_Datax = new Gate_Datax();
                    gate_Datax.msg = "Something went wrong";
                    gate_Datax.status = false;

                    return Json(gate_Datax);
                }

            }

            if (trxType == "Trip Closure")
            {
                List<view_closure_dto> closure_full_list = new List<view_closure_dto>();
                try
                {


                    var trip_close = db.trip_closure.Where(a => a.closure_date >= fromDate && a.closure_date <= toDate && a.customer_no != "SAP").ToList<trip_closure>();
                    var trip_reg = db.trip_registry.ToList();

                    var returnedReportTripClose1 = from s in trip_close
                                                   join t in trip_reg on s.trip_id equals t.trip_id_sap
                                                   select new { s, t };

                    var returnedReportTripClose2 = from s in trip_close
                                                   join t in trip_reg on s.trip_id equals t.trip_no
                                                   select new { s, t };


                    closure_full_list.AddRange(
               from clos in returnedReportTripClose1
               select new view_closure_dto
               {
                   trip_id = clos.s.trip_id,
                   waybill = clos.s.waybill,
                   closure_date = clos.s.closure_date,
                   damaged_qty = clos.s.damaged_qty.ToString(),
                   caked_qty = clos.s.caked_qty.ToString(),
                   short_qty = clos.s.short_qty.ToString(),
                   miss_reason = clos.s.miss_reason,
                   driver_id_sap = clos.t.driver_id_sap.ToString(),
                   driver_name = clos.t.driver_name,
                   atc_no = clos.t.atc_no,
                   trip_id_sap = clos.t.trip_id_sap,
                   atc_type = clos.t.atc_type,
                   fleet = clos.t.fleet,
                   migo_details = clos.t.migo_details

               });

                    closure_full_list.AddRange(
              from clos in returnedReportTripClose2
              select new view_closure_dto
              {
                  trip_id = clos.s.trip_id,
                  waybill = clos.s.waybill,
                  closure_date = clos.s.closure_date,
                  damaged_qty = clos.s.damaged_qty.ToString(),
                  caked_qty = clos.s.caked_qty.ToString(),
                  short_qty = clos.s.short_qty.ToString(),
                  miss_reason = clos.s.miss_reason,
                  driver_id_sap = clos.t.driver_id_sap.ToString(),
                  driver_name = clos.t.driver_name,
                  atc_no = clos.t.atc_no,
                  trip_id_sap = clos.t.trip_id_sap,
                  atc_type = clos.t.atc_type,
                  fleet = clos.t.fleet,
                  migo_details = clos.t.migo_details

              });

                    var returnedClosures = closure_full_list.OrderByDescending(s => s.closure_date).ToList();

                    int user_id = int.Parse(Session["user_id"].ToString());
                    TransactionLogging TRX_LOG = new TransactionLogging();
                    bool status = TRX_LOG.RecordLog(db, "0000000000", user_id, ActivityType.TRIP_CLOSURE_REPORT_SPOOL_HELPER, "TRIP REPORT SPOOL: ", DateTime.Now.ToString());

                    return Json(new { data = returnedClosures }, JsonRequestBehavior.AllowGet);




                }
                catch (Exception ex)
                {
                    view_closure_dto view_Closure = new view_closure_dto();
                    view_Closure.msg = "Something went wrong";
                    view_Closure.status = false;

                    return Json(view_Closure);
                }



            }

            if (trxType == "Trip Cancellation")
            {
                //Gate_Datax gate_Datax = new Gate_Datax();
                //gate_Datax.msg = "NOT YET AVAILABLE";
                //gate_Datax.status = false;

                //return Json(gate_Datax);
            }



            Gate_Datax gate_Dataxx = new Gate_Datax();
            gate_Dataxx.msg = "Something went wrong";
            gate_Dataxx.status = false;

            return Json(gate_Dataxx);

        }

        public ActionResult GetReportPostingErrors(DateTime fromDate, DateTime toDate)
        {

            db.Configuration.ProxyCreationEnabled = false;

            try
            {


                var returnedRecord = db.transaction_data.Where(a => a.gross_time >= fromDate && a.gross_time <= toDate && a.error_field != "OFFLINE" && a.sync_status == false && a.error_field != " ").ToList<transaction_data>();
                //var check2 = from s in returnedRecord.DefaultIfEmpty()
                //             join d in db.atc_data.DefaultIfEmpty().ToList() on s.sales_doc_number equals d.sales_doc_number
                //             join v in db.atc_data.DefaultIfEmpty().ToList() on s.parent_sales_order equals v.sales_doc_number
                //             select new { s, d, v};

                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, "0000000000", user_id, ActivityType.POSTING_ERRORS_REPORT_SPOOL_HELPER, "POSTING ERRORS REPORT SPOOL: ", DateTime.Now.ToString());

                return Json(new { data = returnedRecord }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                Gate_Datax gate_Datax = new Gate_Datax();
                gate_Datax.msg = "Something went wrong";
                gate_Datax.status = false;

                return Json(gate_Datax);
            }
        }

        public ActionResult GetReportSTOPostingErrors(DateTime fromDate, DateTime toDate)
        {

            db.Configuration.ProxyCreationEnabled = false;

            try
            {

                
                var returnedRecord = db.sto_transaction_data.Where(a => a.gross_time >= fromDate && a.gross_time <= toDate && a.error_field != "OFFLINE" && a.sync_status == false && a.error_field != " ").ToList<sto_transaction_data>();
            

                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, "0000000000", user_id, ActivityType.POSTING_ERRORS_REPORT_SPOOL_HELPER, "STO POSTING ERRORS REPORT SPOOL: ", DateTime.Now.ToString());

                return Json(new { data = returnedRecord }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                Gate_Datax gate_Datax = new Gate_Datax();
                gate_Datax.msg = "Something went wrong";
                gate_Datax.status = false;

                return Json(gate_Datax);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DoFetchATcForArchiveDetails(String __RequestVerificationToken, String atc)
        {
            Transaction_Datax trx_Datax = new Transaction_Datax();
            var trimmedATC = atc.TrimStart('0');
            trade_dto tradex = new trade_dto();
            Sto_Transaction_Datax stdx = new Sto_Transaction_Datax();

            string atc_actual = atc;
            string msge;

            //var tradeDetails = db.Trades.Where(a => a.sparenum2.ToString() == atc && a.seconddatetime == null).FirstOrDefault();
            var tradeDetails = db.Trades.Where(a => a.sparenum2.ToString() == trimmedATC && a.seconddatetime == null).FirstOrDefault();
            if (tradeDetails != null)
            {
                //DO Trade swap
                tradex.msg = "";
                tradex.status = true;
                tradex.statusPicking = false;
                tradex.atc = tradeDetails.sparenum2.ToString();
                tradex.trip_ID = "0000000000";

                ViewBag.DestinationTable = "trade";

                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.FETCH_ATC_ATTEMPT_FOR_ARCHIVE_HELPER, "FETCH ATC ATTEMPT FOR ARCHIVE : ", DateTime.Now.ToString());

                return Json(tradex);

                //return
            }
            var stoTrxDataDetaisOut = db.sto_transaction_data.Where(x => x.po_doc_number == atc && x.gross == null && x.sales_type == "PO_OUT").FirstOrDefault();
            if (stoTrxDataDetaisOut != null)
            {
                stdx.msg = "";
                stdx.status = true;

                stdx.atc = stoTrxDataDetaisOut.po_doc_number;
                stdx.trip_id = stoTrxDataDetaisOut.trip_id;


                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.FETCH_ATC_ATTEMPT_FOR_ARCHIVE_HELPER, "FETCH ATC ATTEMPT FOR ARCHIVE : ", DateTime.Now.ToString());

                return Json(stdx);
              
            }
            var stoTrxDataDetaisIn = db.sto_transaction_data.Where(x => x.po_doc_number == atc && x.tare == null && x.sales_type == "PO_IN").FirstOrDefault();
            if (stoTrxDataDetaisOut != null)
            {
            //    //DO STO Inbound swap swap
                stdx.msg = "";
                stdx.status = true;

          
                stdx.atc = stoTrxDataDetaisIn.po_doc_number;
                stdx.trip_id = stoTrxDataDetaisIn.trip_id;


                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.FETCH_ATC_ATTEMPT_FOR_ARCHIVE_HELPER, "FETCH ATC ATTEMPT FOR ARCHIVE : ", DateTime.Now.ToString());

                return Json(stdx);
        
            }
            var atcDetails = db.transaction_data.Where(x => x.sales_doc_number == atc && x.gross == null).FirstOrDefault();

            if (atcDetails == null)
            {
                //DO Trx_data swap

                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.FETCH_ATC_ATTEMPT_FOR_ARCHIVE_HELPER, "FETCH ATC ATTEMPT FOR ARCHIVE : ", DateTime.Now.ToString());

                trx_Datax.status = false;
                trx_Datax.msg = "Wrong ATC or ATC is already weighed-out";
                return Json(trx_Datax);

            }
            else
            {
                trx_Datax.msg = "";
                trx_Datax.status = true;

                trx_Datax.atc = atc;
                trx_Datax.trip_id = atcDetails.trip_id;

                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.FETCH_ATC_ATTEMPT_FOR_ARCHIVE_HELPER, "FETCH ATC ATTEMPT FOR ARCHIVE : ", DateTime.Now.ToString());

                return Json(trx_Datax);
            }




        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DoActualATCArchiving(String __RequestVerificationToken, String atc, String reason)
        {
            Transaction_Datax trx_Datax = new Transaction_Datax();
            trade_dto tradex = new trade_dto();
            Sto_Transaction_Datax stdx = new Sto_Transaction_Datax();
            var trimmedATC = atc.TrimStart('0');

            string atc_actual = atc;
            string msge;

            var tradeDetails = db.Trades.Where(a => a.sparenum2.ToString() == atc && a.seconddatetime == null).FirstOrDefault();
            if (tradeDetails != null)
            {
                //DO Trade swap
                tradex.msg = "";
                tradex.status = true;
                tradex.statusPicking = false;
                tradex.atc = tradeDetails.sparenum2.ToString();
                tradex.trip_ID = "0000000000";

                ViewBag.DestinationTable = "trade";

                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.FETCH_ATC_ATTEMPT_FOR_ARCHIVE_HELPER, "FETCH ATC ATTEMPT FOR ARCHIVE : ", DateTime.Now.ToString());

                return Json(tradex);

            }
            var stoTrxDataDetaisOut = db.sto_transaction_data.Where(x => x.po_doc_number == atc && x.gross == null && x.sales_type == "PO_OUT").FirstOrDefault();
            if (stoTrxDataDetaisOut != null)
            {
                stdx.msg = "";
                stdx.status = true;

                stdx.atc = stoTrxDataDetaisOut.po_doc_number;
                stdx.trip_id = stoTrxDataDetaisOut.trip_id;


                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.FETCH_ATC_ATTEMPT_FOR_ARCHIVE_HELPER, "FETCH ATC ATTEMPT FOR ARCHIVE : ", DateTime.Now.ToString());

                return Json(stdx);
       
            }
            var stoTrxDataDetaisIn = db.sto_transaction_data.Where(x => x.po_doc_number == atc && x.tare == null && x.sales_type == "PO_IN").FirstOrDefault();
            if (stoTrxDataDetaisOut != null)
            {
            //  DO STO Inbound swap swap
                stdx.msg = "";
                stdx.status = true;

            
                stdx.atc = stoTrxDataDetaisIn.po_doc_number;
                stdx.trip_id = stoTrxDataDetaisIn.trip_id;



                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.FETCH_ATC_ATTEMPT_FOR_ARCHIVE_HELPER, "FETCH ATC ATTEMPT FOR ARCHIVE : ", DateTime.Now.ToString());

                return Json(stdx);

            }
            var atcDetails = db.transaction_data.Where(x => x.sales_doc_number == atc && x.gross == null).FirstOrDefault();

            if (atcDetails == null)
            {
                //DO Trx_data swap

                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.FETCH_ATC_ATTEMPT_FOR_ARCHIVE_HELPER, "FETCH ATC ATTEMPT FOR ARCHIVE : ", DateTime.Now.ToString());

                trx_Datax.status = false;
                trx_Datax.msg = "Wrong ATC or ATC is already weighed-out";
                return Json(trx_Datax);

            }
            else
            {
                trx_Datax.msg = "";
                trx_Datax.status = true;

                trx_Datax.atc = atc;
                trx_Datax.trip_id = atcDetails.trip_id;

                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.FETCH_ATC_ATTEMPT_FOR_ARCHIVE_HELPER, "FETCH ATC ATTEMPT FOR ARCHIVE : ", DateTime.Now.ToString());

                return Json(trx_Datax);
            }




        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DoTempATCArchiving(String __RequestVerificationToken, String atc, String reason)
        {
            //Session["TrxSwapPicking"] = 0;
            Transaction_Datax trx_Datax = new Transaction_Datax();
            trade_dto tradex = new trade_dto();
            Sto_Transaction_Datax stdx = new Sto_Transaction_Datax();
            transaction_data_archive tda = new transaction_data_archive();
            

            string atc_actual = atc;
            string msge;

            var tradeDetails = db.Trades.Where(a => a.sparenum2.ToString() == atc && a.seconddatetime == null).FirstOrDefault();
            if (tradeDetails != null)
            {

                int user_id = int.Parse(Session["user_id"].ToString());
                //DO Trade swap
                //string cc = tradeDetails.truckno;
                tradex.msg = "";
                tradex.status = true;
                tradex.statusPicking = false;
                tda.sales_doc_number = tradeDetails.sparenum2.ToString().PadLeft(10, '0');
                tda.sales_doc_type = "PENDING";
                tda.operator_weighin = "ATC ARCHIVING REQUEST";
                tda.seal = reason;
                tda.shp_point = user_id.ToString();
                tda.tare = tradeDetails.firstweight;
                tda.gross = tradeDetails.secondweight;
                tda.tare_time = DateTime.Now;
                tda.sap_post_time = tradeDetails.firstdatetime;
                tda.gross_time = tradeDetails.seconddatetime;
                tda.vehicle = tradeDetails.truckno;
                tda.driver = tradeDetails.sparestr5;
                tda.migo_details = tradeDetails.product;
                tda.sender = tradeDetails.sender;
                tda.destination = tradeDetails.receiver;
                tda.transporter = tradeDetails.transporter;
                tda.transporter_name = tradeDetails.transcode;
                tda.operator_weighout = tradeDetails.username1 + " " + "weighin";


                db.transaction_data_archive.Add(tda);
                db.SaveChanges();

                tradex.msg = "ARCHIVE IS PENDING APPROVAL";
                tradex.status = true;

                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.SEND_FOR_ARCHIVE_APPROVAL_HELPER, "SEND ATC FOR ARCHIVE APPROVAL :", DateTime.Now.ToString());

                return Json(tradex);

            }
            var stoTrxDataDetaisOut = db.sto_transaction_data.Where(x => x.po_doc_number == atc && x.gross == null && x.sales_type == "PO_OUT").FirstOrDefault();
            if (stoTrxDataDetaisOut != null)
            {


                int user_id = int.Parse(Session["user_id"].ToString());
                
                tradex.msg = "";
                tradex.status = true;
                tradex.statusPicking = false;
                tda.sales_doc_number = stoTrxDataDetaisOut.po_doc_number;
                tda.sales_doc_type = "PENDING";
                tda.operator_weighin = "ATC ARCHIVING REQUEST";
                tda.seal = reason;
                tda.shp_point = user_id.ToString();
                tda.tare = stoTrxDataDetaisOut.tare;
                tda.gross = stoTrxDataDetaisOut.gross;
                tda.tare_time = DateTime.Now;
                tda.sap_post_time = stoTrxDataDetaisOut.tare_time;
                tda.gross_time = stoTrxDataDetaisOut.gross_time;
                tda.vehicle = stoTrxDataDetaisOut.vehicle;
                tda.driver = stoTrxDataDetaisOut.driver;
                tda.migo_details = "ZSTO";
                tda.sender = stoTrxDataDetaisOut.sender;
                tda.destination = stoTrxDataDetaisOut.receiver;
                tda.transporter = stoTrxDataDetaisOut.transporter;
                tda.transporter_name = stoTrxDataDetaisOut.transporter_name;
                tda.operator_weighout = stoTrxDataDetaisOut.operator_weighin + " " + "weighin";


                db.transaction_data_archive.Add(tda);
                db.SaveChanges();

             
                tradex.msg = "ARCHIVE IS PENDING APPROVAL";
                tradex.status = true;
                

                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.SEND_FOR_ARCHIVE_APPROVAL_HELPER, "SEND ATC FOR ARCHIVE APPROVAL : ", DateTime.Now.ToString());

                return Json(tradex);
                
            }
            var stoTrxDataDetaisIn = db.sto_transaction_data.Where(x => x.po_doc_number == atc && x.tare == null && x.sales_type == "PO_IN").FirstOrDefault();
            if (stoTrxDataDetaisIn != null)
            {
            //    //DO STO Inbound swap swap
                stdx.msg = "";
                stdx.status = true;

                int user_id = int.Parse(Session["user_id"].ToString());

                tradex.msg = "";
                tradex.status = true;
                tradex.statusPicking = false;
                tda.sales_doc_number = stoTrxDataDetaisIn.po_doc_number;
                tda.sales_doc_type = "PENDING";
                tda.operator_weighin = "ATC ARCHIVING REQUEST";
                tda.seal = reason;
                tda.shp_point = user_id.ToString();
                tda.tare = stoTrxDataDetaisIn.tare;
                tda.gross = stoTrxDataDetaisIn.gross;
                tda.tare_time = DateTime.Now;
                tda.sap_post_time = stoTrxDataDetaisIn.tare_time;
                tda.gross_time = stoTrxDataDetaisIn.gross_time;
                tda.vehicle = stoTrxDataDetaisIn.vehicle;
                tda.driver = stoTrxDataDetaisIn.driver;
                tda.migo_details = "ZSTO";
                tda.sender = stoTrxDataDetaisIn.sender;
                tda.destination = stoTrxDataDetaisIn.receiver;
                tda.transporter = stoTrxDataDetaisIn.transporter;
                tda.transporter_name = stoTrxDataDetaisIn.transporter_name;
                tda.operator_weighout = stoTrxDataDetaisIn.operator_weighin + " " + "weighin";


                db.transaction_data_archive.Add(tda);
                db.SaveChanges();


                tradex.msg = "ARCHIVE IS PENDING APPROVAL";
                tradex.status = true;


                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.SEND_FOR_ARCHIVE_APPROVAL_HELPER, "SEND ATC FOR ARCHIVE APPROVAL : ", DateTime.Now.ToString());

                return Json(tradex);
            }
            var atcDetails = db.transaction_data.Where(x => x.sales_doc_number == atc && x.gross == null).FirstOrDefault();

            if (atcDetails == null)
            {
                //DO Trx_data swap

                trx_Datax.status = false;
                trx_Datax.msg = "Wrong ATC or ATC is already weighed-out";
                return Json(trx_Datax);

            }
            else
            {
                int user_id = int.Parse(Session["user_id"].ToString());

                tradex.msg = "";
                tradex.status = true;
                tradex.statusPicking = false;
                tda.sales_doc_number = atcDetails.sales_doc_number;
                tda.sales_doc_type = "PENDING";
                tda.operator_weighin = "ATC ARCHIVING REQUEST";
                tda.seal = reason;
                tda.shp_point = user_id.ToString();
                tda.tare = atcDetails.tare;
                tda.gross = atcDetails.gross;
                tda.tare_time = DateTime.Now;
                tda.sap_post_time = atcDetails.tare_time;
                tda.gross_time = atcDetails.gross_time;
                tda.vehicle = atcDetails.vehicle;
                tda.driver = atcDetails.driver;
                tda.migo_details = "CEMENT";
                tda.sender = atcDetails.sender;
                tda.destination = atcDetails.destination;
                tda.transporter = atcDetails.transporter;
                tda.transporter_name = atcDetails.transporter_name;
                tda.operator_weighout = atcDetails.operator_weighin + " " + "weighin";


                db.transaction_data_archive.Add(tda);
                db.SaveChanges();

                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.SEND_FOR_ARCHIVE_APPROVAL_HELPER, "SEND ATC FOR ARCHIVE APPROVAL : ", DateTime.Now.ToString());


                tradex.msg = "ARCHIVE IS PENDING APPROVAL";
                tradex.status = true;
                return Json(tradex);
            }




        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DoFetchATcForTareChangeDetails(String __RequestVerificationToken, String atc)
        {
            Transaction_Datax trx_Datax = new Transaction_Datax();
            var trimmedATC = atc.TrimStart('0');
            trade_dto tradex = new trade_dto();
            Sto_Transaction_Datax stdx = new Sto_Transaction_Datax();

            string atc_actual = atc;
            string msge;

        
            var tradeDetails = db.Trades.Where(a => a.sparenum2.ToString() == trimmedATC && a.seconddatetime == null).FirstOrDefault();
            if (tradeDetails != null)
            {
                //DO Trade swap
                tradex.msg = "";
                tradex.status = true;
                tradex.statusPicking = false;
                tradex.operator_weighn = tradeDetails.username1;
                tradex.Tare = tradeDetails.tare.ToString();
                tradex.tare_time = tradeDetails.firstdatetime;
                tradex.atc = tradeDetails.sparenum2.ToString();
             

                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.FETCH_ATC_ATTEMPT_FOR_TARE_CHANGE_HELPER, "FETCH ATC ATTEMPT FOR TARE CHANGE : ", DateTime.Now.ToString());

                return Json(tradex);

                //return
            }
            var stoTrxDataDetaisOut = db.sto_transaction_data.Where(x => x.po_doc_number == atc && x.gross == null && x.sales_type == "PO_OUT").FirstOrDefault();
            if (stoTrxDataDetaisOut != null)
            {
                stdx.msg = "";
                stdx.status = true;

                stdx.atc = stoTrxDataDetaisOut.po_doc_number;
                stdx.tare_time = stoTrxDataDetaisOut.tare_time ?? DateTime.MinValue;
                stdx.operator_weighn = stoTrxDataDetaisOut.operator_weighin;
                stdx.tare = stoTrxDataDetaisOut.tare.ToString();
                stdx.trip_id = stoTrxDataDetaisOut.trip_id;

            

                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.FETCH_ATC_ATTEMPT_FOR_TARE_CHANGE_HELPER, "FETCH ATC ATTEMPT FOR TARE CHANGE : ", DateTime.Now.ToString());

                return Json(stdx);

            }
            var stoTrxDataDetaisIn = db.sto_transaction_data.Where(x => x.po_doc_number == atc && x.tare == null && x.sales_type == "PO_IN").FirstOrDefault();
            if (stoTrxDataDetaisOut != null)
            {
                //    //DO STO Inbound swap swap
                stdx.msg = "";
                stdx.status = true;


                stdx.atc = stoTrxDataDetaisIn.po_doc_number;
                stdx.trip_id = stoTrxDataDetaisIn.trip_id;
                stdx.tare_time = stoTrxDataDetaisOut.tare_time ?? DateTime.MinValue;
                stdx.operator_weighn = stoTrxDataDetaisOut.operator_weighin;
                stdx.tare = stoTrxDataDetaisOut.tare.ToString();


                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.FETCH_ATC_ATTEMPT_FOR_TARE_CHANGE_HELPER, "FETCH ATC ATTEMPT FOR TARE CHANGE : ", DateTime.Now.ToString());

                return Json(stdx);

            }
            var atcDetails = db.transaction_data.Where(x => x.sales_doc_number == atc && x.gross == null).FirstOrDefault();

            if (atcDetails == null)
            {
                //DO Trx_data swap

                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.FETCH_ATC_ATTEMPT_FOR_TARE_CHANGE_HELPER, "FETCH ATC ATTEMPT FOR TARE CHANGE : ", DateTime.Now.ToString());

                trx_Datax.status = false;
                trx_Datax.msg = "Wrong ATC or ATC is already weighed-out";
                return Json(trx_Datax);

            }
            else
            {
                trx_Datax.msg = "";
                trx_Datax.status = true;

                trx_Datax.atc = atc;
                trx_Datax.trip_id = atcDetails.trip_id;
                //trx_Datax.atc = atcDetails.sales_doc_number;
                trx_Datax.tare = atcDetails.tare.ToString();
                trx_Datax.operator_weighn = atcDetails.operator_weighin;
                trx_Datax.tare_time = atcDetails.tare_time ?? DateTime.MinValue;

                

                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.FETCH_ATC_ATTEMPT_FOR_TARE_CHANGE_HELPER, "FETCH ATC ATTEMPT FOR TARE CHANGE : ", DateTime.Now.ToString());

                return Json(trx_Datax);
            }




        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DoTempTareWeightChange(String __RequestVerificationToken, String atc, String reason, String newTare, String Operator, String oldTare)
        {


            Transaction_Datax trx_Datax = new Transaction_Datax();
            var trimmedATC = atc.TrimStart('0');
            trade_dto tradex = new trade_dto();
            Sto_Transaction_Datax stdx = new Sto_Transaction_Datax();

            transaction_data_archive tda = new transaction_data_archive();


            string atc_actual = atc;
            string msge;

            //var tradeDetails = db.Trades.Where(a => a.sparenum2.ToString() == atc && a.seconddatetime == null).FirstOrDefault();
            var tradeDetails = db.Trades.Where(a => a.sparenum2.ToString() == trimmedATC && a.seconddatetime == null).FirstOrDefault();
            if (tradeDetails != null)
            {

                int user_id = int.Parse(Session["user_id"].ToString());
                //DO Trade swap
                //string cc = tradeDetails.truckno;
                tradex.msg = "";
                tradex.status = true;
                tradex.statusPicking = false;
                tda.sales_doc_number = tradeDetails.sparenum2.ToString().PadLeft(10, '0');
                tda.sales_doc_type = "PENDING";
                tda.operator_weighin = "TARE WEIGHT CHANGE REQUEST";
                tda.seal = reason;
                tda.shp_point = user_id.ToString();
                tda.tare = tradeDetails.firstweight;
                tda.gross = tradeDetails.secondweight;
                tda.tare_time = DateTime.Now;
                tda.sap_post_time = tradeDetails.firstdatetime;
                tda.gross_time = tradeDetails.seconddatetime;
                tda.vehicle = tradeDetails.truckno;
                tda.driver = tradeDetails.sparestr5;
                tda.migo_details = tradeDetails.product;
                tda.sender = tradeDetails.sender;
                tda.destination = tradeDetails.receiver;
                tda.transporter = tradeDetails.transporter;
                tda.transporter_name = tradeDetails.transcode;
                tda.operator_weighout = tradeDetails.username1 + " " + "weighin";
                tda.bin_1_tare = Decimal.Parse(oldTare);
                tda.bin_2_tare = Decimal.Parse(newTare);
                



                db.transaction_data_archive.Add(tda);
                db.SaveChanges();

                tradex.msg = "TARE WEIGHT CHANGE IS PENDING APPROVAL";
                tradex.status = true;

                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.TARE_WEIGHT_CHANGE_REQUEST_HELPER, "TARE WEIGHT CHANGE REQUEST HELPER :", DateTime.Now.ToString());

                return Json(tradex);

            }

            var stoTrxDataDetaisOut = db.sto_transaction_data.Where(x => x.po_doc_number == atc && x.gross == null && x.sales_type == "PO_OUT").FirstOrDefault();
            if (stoTrxDataDetaisOut != null)
            {


                int user_id = int.Parse(Session["user_id"].ToString());
                //DO Trade swap
                //string cc = tradeDetails.truckno;
                tradex.msg = "";
                tradex.status = true;
                tradex.statusPicking = false;
                tda.sales_doc_number = stoTrxDataDetaisOut.po_doc_number;
                tda.sales_doc_type = "PENDING";
                tda.operator_weighin = "TARE WEIGHT CHANGE REQUEST";
                tda.seal = reason;
                tda.shp_point = user_id.ToString();
                tda.tare = stoTrxDataDetaisOut.tare;
                tda.gross = stoTrxDataDetaisOut.gross;
                tda.tare_time = DateTime.Now;
                tda.sap_post_time = stoTrxDataDetaisOut.tare_time;
                tda.gross_time = stoTrxDataDetaisOut.gross_time;
                tda.vehicle = stoTrxDataDetaisOut.vehicle;
                tda.driver = stoTrxDataDetaisOut.driver;
                tda.migo_details = "ZSTO";
                tda.sender = stoTrxDataDetaisOut.sender;
                tda.destination = stoTrxDataDetaisOut.receiver;
                tda.transporter = stoTrxDataDetaisOut.transporter;
                tda.transporter_name = stoTrxDataDetaisOut.transporter_name;
                tda.operator_weighout = stoTrxDataDetaisOut.operator_weighin + " " + "weighin";
                tda.bin_1_tare = Decimal.Parse(oldTare);
                tda.bin_2_tare = Decimal.Parse(newTare);


                db.transaction_data_archive.Add(tda);
                db.SaveChanges();


                tradex.msg = "TARE WEIGHT CHANGE IS PENDING APPROVAL";
                tradex.status = true;


                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.TARE_WEIGHT_CHANGE_REQUEST_HELPER, "TARE WEIGHT CHANGE REQUEST HELPER : ", DateTime.Now.ToString());

                return Json(tradex);

            }
            var stoTrxDataDetaisIn = db.sto_transaction_data.Where(x => x.po_doc_number == atc && x.tare == null && x.sales_type == "PO_IN").FirstOrDefault();
            if (stoTrxDataDetaisIn != null)
            {
                //    //DO STO Inbound swap swap
                stdx.msg = "";
                stdx.status = true;

                int user_id = int.Parse(Session["user_id"].ToString());

                tradex.msg = "";
                tradex.status = true;
                tradex.statusPicking = false;
                tda.sales_doc_number = stoTrxDataDetaisIn.po_doc_number;
                tda.sales_doc_type = "PENDING";
                tda.operator_weighin = "TARE WEIGHT CHANGE REQUEST";
                tda.seal = reason;
                tda.shp_point = user_id.ToString();
                tda.tare = stoTrxDataDetaisIn.tare;
                tda.gross = stoTrxDataDetaisIn.gross;
                tda.tare_time = DateTime.Now;
                tda.sap_post_time = stoTrxDataDetaisIn.tare_time;
                tda.gross_time = stoTrxDataDetaisIn.gross_time;
                tda.vehicle = stoTrxDataDetaisIn.vehicle;
                tda.driver = stoTrxDataDetaisIn.driver;
                tda.migo_details = "ZSTO";
                tda.sender = stoTrxDataDetaisIn.sender;
                tda.destination = stoTrxDataDetaisIn.receiver;
                tda.transporter = stoTrxDataDetaisIn.transporter;
                tda.transporter_name = stoTrxDataDetaisIn.transporter_name;
                tda.operator_weighout = stoTrxDataDetaisIn.operator_weighin + " " + "weighin";
                tda.bin_1_tare = Decimal.Parse(oldTare);
                tda.bin_2_tare = Decimal.Parse(newTare);


                db.transaction_data_archive.Add(tda);
                db.SaveChanges();


                tradex.msg = "TARE WEIGHT CHANGE IS PENDING APPROVAL";
                tradex.status = true;


                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.TARE_WEIGHT_CHANGE_REQUEST_HELPER, "TARE WEIGHT CHANGE REQUEST HELPER :", DateTime.Now.ToString());

                return Json(tradex);
            }
            var atcDetails = db.transaction_data.Where(x => x.sales_doc_number == atc && x.gross == null).FirstOrDefault();

            if (atcDetails == null)
            {
                //DO Trx_data swap

                trx_Datax.status = false;
                trx_Datax.msg = "Wrong ATC or ATC is already weighed-out";
                return Json(trx_Datax);

            }
            else
            {
                int user_id = int.Parse(Session["user_id"].ToString());

                tradex.msg = "";
                tradex.status = true;
                tradex.statusPicking = false;
                tda.sales_doc_number = atcDetails.sales_doc_number;
                tda.sales_doc_type = "PENDING";
                tda.operator_weighin = "TARE WEIGHT CHANGE REQUEST";
                tda.seal = reason;
                tda.shp_point = user_id.ToString();
                tda.tare = atcDetails.tare;
                tda.gross = atcDetails.gross;
                tda.tare_time = DateTime.Now;
                tda.sap_post_time = atcDetails.tare_time;
                tda.gross_time = atcDetails.gross_time;
                tda.vehicle = atcDetails.vehicle;
                tda.driver = atcDetails.driver;
                tda.migo_details = "CEMENT";
                tda.sales_type = atcDetails.sales_type;
                tda.sender = atcDetails.sender;
                tda.destination = atcDetails.destination;
                tda.transporter = atcDetails.transporter;
                tda.transporter_name = atcDetails.transporter_name;
                tda.operator_weighout = atcDetails.operator_weighin + " " + "weighin";
                tda.bin_1_tare = Decimal.Parse(oldTare);
                tda.bin_2_tare = Decimal.Parse(newTare);


                db.transaction_data_archive.Add(tda);
                db.SaveChanges();

                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.TARE_WEIGHT_CHANGE_REQUEST_HELPER, "TARE WEIGHT CHANGE REQUEST HELPER : ", DateTime.Now.ToString());


                tradex.msg = "TARE WEIGHT CHANGE IS PENDING APPROVAL";
                tradex.status = true;
                return Json(tradex);
            }




        }


    }




}