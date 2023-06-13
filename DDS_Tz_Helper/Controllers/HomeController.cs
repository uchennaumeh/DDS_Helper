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

        public ActionResult NewHome()
        {
            return View();
        }

        public ActionResult STO_Modify()
        {
            return View();
        }

        public ActionResult Remove_TripID()
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
        //[ValidateAntiForgeryToken]
        public JsonResult checkLogin( String loginVAr)
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
            catch(Exception ex)
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
            catch(Exception ex)
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
           

            var tripRegistryDetails = db.trip_registry.Where(x => (x.tms_online_status == "FUEL_POSTED" || x.tms_online_status =="CLOSED") && (x.tms_status == "FUEL_POSTED" || x.tms_online_status == "CLOSED") && x.atc_no == trip).FirstOrDefault();

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
                

                    Session["change_password"] = UM.change_password;


                    Session["company_name"] = db.system_setting.FirstOrDefault().company_name;

                  

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

    }
}