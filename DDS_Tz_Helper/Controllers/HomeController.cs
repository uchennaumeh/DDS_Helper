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

        public ActionResult Receiver()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        //public JsonResult DoChangeATCGrade(String __RequestVerificationToken, String atc, String grade)
        public JsonResult DoChangeATCGrade(String atc, String grade)
        {
            atc_datax atc_Datax = new atc_datax();

            string atc_actual = atc;
            string grade_actual = grade;
            string msge;
            decimal d;


            try
            {
                //atc_data aData = db.atc_data.Where(a => a.used == null);
                var atcDetails = db.atc_data.Where(x => x.sales_doc_number == atc).FirstOrDefault();

                if (atcDetails == null)
                {
                    atc_Datax.msg = "Wrong ATC Entered";
                    atc_Datax.status = false;
                    return Json(atc_Datax);
                }

                var d2 = atcDetails.sales_doc_number;

                //var q = (from ad in db.atc_data
                //         join td in db.transaction_data on ad.sales_doc_number equals td.sales_doc_number
                //         where td.gross != null && td.sales_doc_number == d2

                //select new
                //         {
                //             td.sales_doc_number                          
                //         }).FirstOrDefault();


                //if(q == null)
                //{
                //    atc_Datax.msg = "ATC Updated Successfully";
                //    atc_Datax.status = true;
                //    return Json(atc_Datax);
                //}
                //else
                //{
                //    atc_Datax.msg = "Be sure you have entered a correct ATC";
                //    atc_Datax.status = false;
                //    return Json(atc_Datax);
                //}




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
                return Json(atc_Datax);
            }

            //return Json(atc_Datax);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        //public JsonResult DoChangeATCGrade(String __RequestVerificationToken, String atc, String grade)
        public JsonResult DoUpdateDriver(String atc, String driver)
        {
            trade_dto tradex = new trade_dto();
            transaction_dto tdx = new transaction_dto();
            atc_datax atc_Datax = new atc_datax();
            decimal atcEntered;
            Decimal.TryParse(atc, out atcEntered);

            var tradeDetails = db.Trades.Where(x => x.sparenum2 == atcEntered).FirstOrDefault();
            var atcDetails = db.atcs.Where(x => x.sales_doc_number == atc).FirstOrDefault();
            var transDataDetails = db.transaction_data.Where(x => x.sales_doc_number == atc && x.sync_status != true).FirstOrDefault();

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
                tradex.msg = "ATC not found, please ensure ATC is correct";
                tradex.status = false;
                return Json(tradex);
            }

           


            return Json(tradex);
        }





        [HttpPost]
        //[ValidateAntiForgeryToken]
        //public JsonResult DoChangeATCGrade(String __RequestVerificationToken, String atc, String grade)
        public JsonResult DoUpdateReceiver(String receiver, String atc)
        {
            atc_datax atc_Datax = new atc_datax();

            string atc_actual = atc;
            string receiver_actual = receiver;
            
            string msge;
            


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
                var atcDetails = db.atc_data.Where(x => x.sales_doc_number == atc && x.customer_name == null && x.used == null).FirstOrDefault();


                //if (atcDetails == null)
                //{
                //    atc_Datax.msg = "Wrong ATC Entered or Customer Name Already Inputed";
                //    atc_Datax.status = false;
                //    return Json(atc_Datax);
                //}

                //var d2 = atcDetails.sales_doc_number;

                //var q = (from ad in db.atc_data
                //         join td in db.transaction_data on ad.sales_doc_number equals td.sales_doc_number
                //         where td.gross != null && td.sales_doc_number == d2

                //select new
                //         {
                //             td.sales_doc_number                          
                //         }).FirstOrDefault();


                //if(q == null)
                //{
                //    atc_Datax.msg = "ATC Updated Successfully";
                //    atc_Datax.status = true;
                //    return Json(atc_Datax);
                //}
                //else
                //{
                //    atc_Datax.msg = "Be sure you have entered a correct ATC";
                //    atc_Datax.status = false;
                //    return Json(atc_Datax);
                //}




                if (atcDetails != null)
                {
                    atcDetails.customer_name = receiver;
                    db.Entry(atcDetails).State = EntityState.Modified;
                    db.SaveChanges();



                    atc_Datax.msg = "Receiver Updated Successfully";
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
                atc_Datax.msg = "Error occured somewhere. cal on I.T";
                atc_Datax.status = false;
                return Json(atc_Datax);
            }

            //return Json(atc_Datax);



        }

    }
}