using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GowWebSite.Models;
using System.Configuration;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Data.Entity.Validation;
using GowWebSite.PayPal;
using PayPal.Api;

namespace GowWebSite.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private GowEntities db = new GowEntities();

        [HttpGet]
        public ActionResult Index()
        {
            var userItems = db.CityPayItems.Where(x => db.UserCities.Where(y => y.Email == User.Identity.Name).Select(id => id.CityID).Contains(x.CityID));
            if (userItems == null || userItems.Count() == 0)
            {
                ViewBag.TotalCost = 0;
                ViewBag.NewCost = 0;
                return View(new List<CityPayItem>());
            }
            ViewBag.TotalCost = userItems.Sum(x => x.PayItem.Cost);

            var userUnpaidItems = userItems.Where(x => !x.Paid);

            ViewBag.ExistingSubscription = db.Subscriptions.Where(x => x.Email == User.Identity.Name).Count() > 0;

            if (userUnpaidItems == null || userUnpaidItems.Count() == 0)
            {
                ViewBag.NewCost = 0;
                ViewBag.PreviousCost = 0;
                return View(new List<CityPayItem>());
            }

            var sub = db.Subscriptions.Where(x => x.Email == User.Identity.Name).First();

            ViewBag.PreviousCost = sub.TotalCost;
            ViewBag.NewCost = userUnpaidItems.Sum(x => x.PayItem.Cost);

            ViewBag.TrialDays = DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month) - DateTime.Today.Day;

            return View(userUnpaidItems);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(IEnumerable<GowWebSite.Models.CityPayItem> items)
        {            
            //Delete the existing plan if one exists
            if (db.Subscriptions.Where(x => x.Email == User.Identity.Name).Count() > 0)
            {
                Subscription sub = db.Subscriptions.Where(x => x.Email == User.Identity.Name).First();
                var apiContext = PaypalConfiguration.GetAPIContext();

                Agreement agreement = new Agreement();
                agreement.id = sub.PaypalSubscriptionID;

                AgreementStateDescriptor state = new AgreementStateDescriptor();
                state.note = "Canceling to renew with new amounts.";

                agreement.Cancel(apiContext, state);
            }
            return RedirectToAction("Pay");
        }
        
        // GET: Payment
        public ActionResult Pay()
        {
            var userItems = db.CityPayItems.Where(x => db.UserCities.Where(y => y.Email == User.Identity.Name).Select(id => id.CityID).Contains(x.CityID));
            if (userItems == null || userItems.Count() == 0)
            {
                ViewBag.TotalCost = 0;
                ViewBag.NewCost = 0;
                return View(new List<CityPayItem>());
            }

            var userUnpaidItems = userItems.Where(x => !x.Paid);

            ViewBag.TotalCost = userItems.Sum(x => x.PayItem.Cost);
            ViewBag.ExistingSubscription = db.Subscriptions.Where(x => x.Email == User.Identity.Name).Count() > 0;
            ViewBag.NewCost = userUnpaidItems.Sum(x => x.PayItem.Cost);
            ViewBag.TrialDays = DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month) - DateTime.Today.Day;

            return View();
        }
                // GET: Payment
        public ActionResult Confirm()
        {
            string authToken = ConfigurationManager.AppSettings["PDTToken"];

            //read in txn token from querystring
            string txToken = Request.QueryString.Get("tx");


            string query = string.Format("cmd=_notify-synch&tx={0}&at={1}", txToken, authToken);

            // Create the request back
            string url = ConfigurationManager.AppSettings["PayPalSubmitUrl"];
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            // Set values for the request back
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = query.Length;

            // Write the request back IPN strings
            StreamWriter stOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
            stOut.Write(query);
            stOut.Close();

            // Do the request to PayPal and get the response
            StreamReader stIn = new StreamReader(req.GetResponse().GetResponseStream());
            string strResponse = stIn.ReadToEnd();
            stIn.Close();
            string temp = string.Empty;

            ViewBag.RawData = HttpUtility.UrlDecode(strResponse).Replace(" ","<br/>");

            // If response was SUCCESS, parse response string and output details
            if (strResponse.StartsWith("SUCCESS"))
            {
                PDTHolder pdt = PDTHolder.Parse(strResponse);
                temp =
                    string.Format("Thank you {0} {1} for your subscription of {3} {4}!",
                    pdt.PayerFirstName, pdt.PayerLastName,
                    pdt.PayerEmail, pdt.GrossTotal.ToString("F"), pdt.Currency);

                //Update the DB now
                var existingSubscritions = db.Subscriptions.Where(x => x.Email == pdt.Custom);
                Subscription userSub;
                if (existingSubscritions != null && existingSubscritions.Count() > 0)
                {
                    //Get existing subscription
                    userSub = existingSubscritions.First();                    
                }
                else
                {
                    //Get a new one
                    userSub = new Subscription();
                }

                //Update the subscription
                userSub.TotalCost = (decimal)pdt.GrossTotal;
                userSub.Email = User.Identity.Name;
                userSub.LastPaid = DateTime.Now;
                userSub.PaypalTxnID = pdt.TransactionId;
                userSub.PaypalEmail = pdt.PayerEmail;
                userSub.PaypalPayerID = pdt.PayerID;
                userSub.PaypalSubscriptionID = pdt.SubscriberId;

                var unpaidItems = db.CityPayItems.Where(x => db.UserCities.Where(y => y.Email == User.Identity.Name).Select(id => id.CityID).Contains(x.CityID) && !x.Paid);
                //Set all city items to paid
                foreach (CityPayItem item in unpaidItems)
                {
                    item.Paid = true;
                    SubscriptionItem sItem = new SubscriptionItem();
                    sItem.Subscription = userSub;
                    item.SubscriptionItems.Add(sItem);
                }

                //Set all cities to Paid
                var userCities = db.Cities.Where(x => db.UserCities.Where(y => y.Email == User.Identity.Name).Select(id => id.CityID).Contains(x.CityID));
               
                //Set all city items to paid
                foreach (Login item in userCities.Select(x=> x.Login))
                {
                    item.Paid = true;
                    item.PaidThrough = DateTime.Today.AddDays(DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month) - DateTime.Today.Day + 1);                    
                }
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    throw e;
                }
            }
            else
            {
                temp = "Something went wrong with the payment.  Your changes are still in your cart.";
            }
            ViewBag.Message = temp;

            return View();
        }
    }
}