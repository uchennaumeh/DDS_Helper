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

        [HttpPost]
        //[ValidateAntiForgeryToken]
        //public JsonResult DoChangeATCGrade(String __RequestVerificationToken, String atc, String grade)
        public JsonResult DoChangeATCGrade(String atc, String grade)
        {
            atc_datax atc_Datax = new atc_datax();

            //userLoging user_login = new userLoging();
            //string token = __RequestVerificationToken;
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

    }
}