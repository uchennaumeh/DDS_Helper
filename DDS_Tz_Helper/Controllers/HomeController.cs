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

        public ActionResult ATCSwapRequests()
        {
            
            return View();
        }

        public ActionResult ApproveSwap(int? id)
        {
            //ession["recordId"] = id;
            ViewBag.RecordId = id;
            return View();
        }

        public ActionResult GateReport()
        {
            return View();
        }

        public ActionResult MaterialReport()
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
            // db.close_old_open_trips();
            atc_datax atc_Datax = new atc_datax();

            string atc_actual = atc;
            string grade_actual = grade;
            string msge;
            decimal d;


            try
            {
                //atc_data aData = db.atc_data.Where(a => a.used == null);
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
                    //int user_id = int.Parse(userID);
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

                poDetails.transporter = transporter;
                poDetails.trip_id = trip_id;
                poDetails.destination = destination;
                poDetails.driver = driver;
                poDetails.sender = sender;
                poDetails.loc = stoLoc;
                poDetails.vehicle = truckNum;
                poDetails.transporter_name = transporter;
                poDetails.receiver = receiver;



                //poDetails.driver = driver;
                db.Entry(poDetails).State = EntityState.Modified;
                db.SaveChanges();

                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.MODIFY_ENTRIES_HELPER, "MODIFY_ENTRIES : ", DateTime.Now.ToString());

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

                //var trxDataArch 
                if (atcDetails == null)
                {
                    trx_Datax.status = false;
                    trx_Datax.msg = "Wrong ATC or ATC is already used";
                    return Json(trx_Datax);
                }
                var atcQty = atcDetails.cumm_order_qty;

                //trxDetails.quantity = atcQty;
                //trxDetails.sales_doc_number = newATC;
                //trxDetails.trip_id = trip_id;

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



                //TO BE DONE AFTER SUPERVISOR APPROVAL
                //db.Entry(trxDetails).State = EntityState.Modified;
                //db.SaveChanges();



                //atcDetails.used = true;
                //db.Entry(atcDetails).State = EntityState.Modified;
                //db.SaveChanges();

                //atcDetailsold.used = false;
                //db.Entry(atcDetailsold).State = EntityState.Modified;
                //db.SaveChanges();


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

                //trxDetails.quantity = atcQty;
                //trxDetails.sales_doc_number = newATC;
                //trxDetails.trip_id = trip_id;

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


                //TO BE DONE AFTER SUPERVISOR APPROVAL
                //db.Entry(trxDetails).State = EntityState.Modified;
                //db.SaveChanges();

                //atcDetails.used = true;
                //db.Entry(atcDetails).State = EntityState.Modified;
                //db.SaveChanges();

                //atcDetailsold.used = false;
                //db.Entry(atcDetailsold).State = EntityState.Modified;
                //db.SaveChanges();


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

                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.MODIFY_ENTRIES_HELPER, "MODIFY_ENTRIES GATE: ", DateTime.Now.ToString());

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





                //poDetails.driver = driver;
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
            //sto_datax sto_Datax = new sto_datax();
            Transaction_Datax trx_Datax = new Transaction_Datax();
            trade_dto tradex = new trade_dto();
            Sto_Transaction_Datax stdx = new Sto_Transaction_Datax();

            string atc_actual = atc;
            string msge;

            var tradeDetails = db.Trades.Where(a => a.sparenum2.ToString() == atc && a.seconddatetime == null).FirstOrDefault();
            if (tradeDetails != null)
            {
                //DO Trade swap
                //string cc = tradeDetails.truckno;
                tradex.msg = "";
                tradex.status = true;
                tradex.statusPicking = false;
                tradex.atc = tradeDetails.sparenum2.ToString();
                tradex.trip_ID = "0000000000";

                //Session["table"] = "trade";
                ViewBag.DestinationTable = "trade";

                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.FETCH_ATC_ATTEMPT_FOR_SWAP_HELPER, "FETCH ATC ATTEMPT FOR SWAP : ", DateTime.Now.ToString());

                return Json(tradex);

                //return
            }            
            var stoTrxDataDetaisOut = db.sto_transaction_data.Where(x => x.po_doc_number == atc && x.gross == null && x.sales_type == "BAGS" && x.sales_type == "PO_OUT").FirstOrDefault();
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

                //Session["table"] = "sto_trx_data";
                ViewBag.DestinationTable = "sto_trx_data";

                int user_id = int.Parse(Session["user_id"].ToString());
                TransactionLogging TRX_LOG = new TransactionLogging();
                bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.FETCH_ATC_ATTEMPT_FOR_SWAP_HELPER, "FETCH ATC ATTEMPT FOR SWAP : ", DateTime.Now.ToString());

                return Json(stdx);
                //return
            }
            var stoTrxDataDetaisIn = db.sto_transaction_data.Where(x => x.po_doc_number == atc && x.tare == null && x.sales_type == "BAGS" && x.sales_type == "PO_IN").FirstOrDefault();
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

                //Session["table"] = "sto_trx_data";
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
                //DO Trx_data swap
                //return
                //try
                //{
                    int user_id = int.Parse(Session["user_id"].ToString());
                    TransactionLogging TRX_LOG = new TransactionLogging();
                    bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.FETCH_ATC_ATTEMPT_HELPER, "FETCH ATC ATTEMPT FOR SWAP : ", DateTime.Now.ToString());

                    trx_Datax.status = false;
                    trx_Datax.msg = "Wrong ATC or ATC is already weighed-out";
                    return Json(trx_Datax);
                //}
                //catch(Exception ex)
                //{
                    
                //}
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
            //var trxDataDetail = db.Trades.Where(a => a.sparenum2 == int.Parse(oldATC) && a.seconddatetime == null).FirstOrDefault();
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

            var stoTrxDataDetaisOut = db.sto_transaction_data.Where(x => x.po_doc_number == oldATCStoOut && x.gross == null && x.sales_type == "BAGS" && x.sales_type == "PO_OUT").FirstOrDefault();
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

            var stoTrxDataDetaisIn = db.sto_transaction_data.Where(x => x.po_doc_number == oldATCStoIn && x.tare == null && x.sales_type == "BAGS" && x.sales_type == "PO_IN").FirstOrDefault();
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

                    //if (returnedTda.tare_time.HasValue)
                    //{
                        trx_Datax.tare_time = returnedTda.tare_time ?? DateTime.MinValue;
                    //var date = new Date(parseInt(returnedTda.tare_time.substr(6)));
                    //}

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
        public JsonResult DoFetchATCDetailsGate(String __RequestVerificationToken, String atc)
        {

            //sto_datax sto_Datax = new sto_datax();
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
            //trade_dto tradex = new trade_dto();
            //transaction_dto tdx = new transaction_dto();
            //trip_registry_dto tr = new trip_registry_dto();
            //atc_datax atc_Datax = new atc_datax();

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



                    //db.transaction_data_archive.Add(t_data_archive);
                    //db.SaveChanges();

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
                    //Transaction_data_archive t_data = new transaction_data();
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
        //(String __RequestVerificationToken, String trip, String atc)
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
            //trade_dto tradex = new trade_dto();
            //transaction_dto tdx = new transaction_dto();
            //trip_registry_dto tr = new trip_registry_dto();
            //atc_datax atc_Datax = new atc_datax();


            //var tripRegistryDetails = db.trip_registry.Where(x => x.tms_online_status == null && x.atc_no == atc).FirstOrDefault();



            //try
            //{
            //    if (tripRegistryDetails == null)
            //    {
            //        tr.msg = "WRONG ATC NUMBER ENTERED";
            //        tr.status = false;
            //        return Json(tr);
            //    }
            //    else
            //    {

            //        tripRegistryDetails.tms_online_status = "APPROVED";
            //        tripRegistryDetails.trip_id_sap = trip;
            //        db.Entry(tripRegistryDetails).State = EntityState.Modified;
            //        db.SaveChanges();

            //        int user_id = int.Parse(Session["user_id"].ToString());
            //        TransactionLogging TRX_LOG = new TransactionLogging();
            //        bool status = TRX_LOG.RecordLog(db, atc, user_id, ActivityType.UPDATE_ONLINE_TRIP_HELPER, "UPDATE_ONLINE_TRIP_HELPER: ", DateTime.Now.ToString());

            //        tr.msg = "ONLINE TRIP ID ADDED";
            //        tr.status = true;
            //        return Json(tr);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    tr.msg = "Something went wrong, plese contact I.T";
            //    tr.status = false;
            //    return Json(tr);
            //}






            //tr.msg = "Please ensure ATC is correct";
            //tr.status = false;
            //return Json(tr);
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
                        //atcDetails.drivers_name = driver;
                        //db.Entry(atcDetails).State = EntityState.Modified;
                        //db.SaveChanges();
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
                //atc_data aData = db.atc_data.Where(a => a.used == null);
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



                    //Session["company_name"] = db.system_setting.FirstOrDefault().company_name;

                    //update_atc_type_atc_count_Result for TZ. they need DB upgrade to include currecnt_queue column on system_setting
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
            //return();
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
            //return();
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
            //return();
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
                    bool status = TRX_LOG.RecordLog(db, "0000000000", user_id, ActivityType.MATERIAL_REPORT_SPOOL_HELPER, "GATE REPORT SPOOL HELPER: ", DateTime.Now.ToString());

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

            var usertry = db.users.ToList();
            var roletry = db.roles.ToList();

            var jointry = from f in usertry
                          join c in roletry.DefaultIfEmpty() on f.role_id equals c.Id
                          select new { f, c };





            int user_id = int.Parse(Session["user_id"].ToString());
            TransactionLogging TRX_LOG = new TransactionLogging();
            bool status = TRX_LOG.RecordLog(db, "0000000000", user_id, ActivityType.GATE_REPORT_SPOOL_HELPER, "GATE REPORT SPOOL: ", DateTime.Now.ToString());

            db.Configuration.ProxyCreationEnabled = false;
            try
            {

                //var returnedReportPendingSwap0 = from s in db.transaction_data_archive.Where(a => a.operator_weighin == "ATC SWAP REQUEST" && a.sales_doc_type == "PENDING")
                //                                 join t in db.users on s.shp_point equals t.Id.ToString()
                //                                 select new { s, t };

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


    }




}