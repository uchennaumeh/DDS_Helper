using DDS_Tz_Helper.Business_Objects;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DDS_Tz_Helper.Controllers
{
    public class HomeController : Controller
    {
        private DispatchDBEntities db = new DispatchDBEntities();
        public ActionResult Index()
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

        public ActionResult STO_Modify()
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
        public JsonResult DoModifyEntries(String __RequestVerificationToken, String atc, String driver, String truckNum, String stoLoc, String transporter, String sender, String destination, String trip_id)
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


                //poDetails.driver = driver;
                db.Entry(poDetails).State = EntityState.Modified;
                db.SaveChanges();

                sto_Datax.msg = "UPDATED SUCCESSFULLY!!!";
                sto_Datax.status = true;
                return Json(sto_Datax);
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
                    sto_Datax.atc = atc;

                    return Json(sto_Datax);
                }

              
            }
            catch (Exception ex)
            {

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

            //return Json(atc_Datax);



        }

    }
}