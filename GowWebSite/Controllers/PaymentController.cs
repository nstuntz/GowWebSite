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
            string userEmail = User.Identity.Name;
            var cityPayItems = db.CityPayItems.Where(x => db.UserCities.Where(y => y.Email == userEmail).Select(id => id.CityID).Contains(x.CityID));
            var userPayItems = db.UserPayItems.Where(y => y.Email == userEmail);

            //If there are no items shoe a blanked page
            if ((cityPayItems == null || cityPayItems.Count() == 0) && (userPayItems == null || userPayItems.Count() == 0))
            {
                ViewBag.TotalCost = 0;
                ViewBag.NewCost = 0;
                ViewBag.PreviousCost = 0;
                ViewBag.ExistingSubscription = false;
                return View(new List<CityPayItem>());
            }
            
            decimal itemsCost = 0;
            if (cityPayItems.Count() > 0)
            {
                itemsCost = cityPayItems.Sum(x => x.PayItem.Cost);
            }
            decimal userCost = 0;
            if (userPayItems.Count() > 0)
            {
                userCost = userPayItems.Sum(x => x.PayItem.Cost);
            }
            ViewBag.TotalCost = itemsCost + userCost;

            decimal unpaidCityCost = 0;
            var userUnpaidCityItems = cityPayItems.Where(x => !x.Paid);
            if (userUnpaidCityItems.Count() > 0)
            {
                unpaidCityCost = userUnpaidCityItems.Sum(x => x.PayItem.Cost);
            }            

            decimal unpaidUserCost = 0;
            var unpaidUserItems = userPayItems.Where(x => !x.Paid);
            if (unpaidUserItems.Count() > 0)
            {
                unpaidUserCost = unpaidUserItems.Sum(x => x.PayItem.Cost);
            }

            ViewBag.NewCost = unpaidCityCost + unpaidUserCost;
                        
            ViewBag.TrialDays = DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month) - DateTime.Today.Day;

            AllPayItems payitems = new AllPayItems();
            payitems.CityItems = userUnpaidCityItems.ToList();
            payitems.UserItems = unpaidUserItems.ToList();

            AddAllowedUsedCitiesToViewBag();

            return View(payitems);
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
                if (!string.IsNullOrWhiteSpace(sub.PaypalSubscriptionID))
                {
                    agreement.Cancel(apiContext, state);
                }
            }
            return RedirectToAction("Pay");
        }
        
        // GET: Payment
        public ActionResult Pay()
        {
            var cityPayItems = db.CityPayItems.Where(x => db.UserCities.Where(y => y.Email == User.Identity.Name).Select(id => id.CityID).Contains(x.CityID));
            var userPayItems = db.UserPayItems.Where(y => y.Email == User.Identity.Name);

            //If there are no items shoe a blanked page
            if ((cityPayItems == null || cityPayItems.Count() == 0) && (userPayItems == null || userPayItems.Count() == 0))
            {
                RedirectToAction("Index");
            }


            decimal itemsCost = 0;
            if (cityPayItems.Count() > 0)
            {
                itemsCost = cityPayItems.Sum(x => x.PayItem.Cost);
            }
            decimal userCost = 0;
            if (userPayItems.Count() > 0)
            {
                userCost = userPayItems.Sum(x => x.PayItem.Cost);
            }
            ViewBag.TotalCost = itemsCost + userCost;

            decimal unpaidCityCost = 0;
            var userUnpaidCityItems = cityPayItems.Where(x => !x.Paid);
            if (userUnpaidCityItems.Count() > 0)
            {
                unpaidCityCost = userUnpaidCityItems.Sum(x => x.PayItem.Cost);
            }

            decimal unpaidUserCost = 0;
            var unpaidUserItems = userPayItems.Where(x => !x.Paid);
            if (unpaidUserItems.Count() > 0)
            {
                unpaidUserCost = unpaidUserItems.Sum(x => x.PayItem.Cost);
            }

            ViewBag.NewCost = unpaidCityCost + unpaidUserCost;
            
            ViewBag.TrialDays = DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month) - DateTime.Today.Day;
            ViewBag.PayEndPoint = ConfigurationManager.AppSettings["PayPalSubmitUrl"];

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
                    string.Format("Thank you {0} {1} for your subscription!",
                    pdt.PayerFirstName, pdt.PayerLastName,
                    pdt.PayerEmail, pdt.GrossTotal.ToString("F"), pdt.Currency);

                //Update the DB now
                var existingSubscritions = db.Subscriptions.Where(x => x.Email == pdt.Custom);
                Subscription userSub;
                bool newSub = true;
                if (existingSubscritions != null && existingSubscritions.Count() > 0)
                {
                    //Get existing subscription
                    userSub = existingSubscritions.First();  
                    newSub = false;
                }
                else
                {
                    //Get a new one
                    userSub = new Subscription();
                    newSub = true;
                }

                //Update the subscription
                userSub.TotalCost = (decimal)pdt.GrossTotal;
                userSub.Email = User.Identity.Name;
                userSub.LastPaid = DateTime.Now;
                userSub.PaypalTxnID = pdt.TransactionId;
                userSub.PaypalEmail = pdt.PayerEmail;
                userSub.PaypalPayerID = pdt.PayerID;
                userSub.PaypalSubscriptionID = pdt.SubscriberId;

                if (newSub)
                {
                    db.Subscriptions.Add(userSub);
                }

                var unpaidItems = db.CityPayItems.Where(x => db.UserCities.Where(y => y.Email == User.Identity.Name).Select(id => id.CityID).Contains(x.CityID) && !x.Paid);
                //Set all city items to paid
                foreach (CityPayItem item in unpaidItems)
                {
                    item.Paid = true;
                }

                var userPay = db.UserPayItems.Where(y => y.Email == User.Identity.Name);
                foreach (UserPayItem item in userPay)
                {
                    item.Paid = true;
                }
                
                //Set all cities to Paid
                var userCities = db.Cities.Where(x => db.UserCities.Where(y => y.Email == User.Identity.Name).Select(id => id.CityID).Contains(x.CityID));
               
                //Set all city items to paid
                foreach (Login item in userCities.Select(x=> x.Login))
                {
                    item.Paid = true;
                    item.PaidThrough = DateTime.Today.AddDays(DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month) - DateTime.Today.Day + 3);                    
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
                temp = "Something went wrong with the payment.  Your charges are still in your cart.";
            }
            ViewBag.Message = temp;

            return View();
        }


        private void AddAllowedUsedCitiesToViewBag()
        {
            //Get user's basic cities count and # left
            var userPayBasicCities = db.UserPayItems.Where(x => x.Email == User.Identity.Name && x.PayItem.ItemType == (int)PayItemTypeEnum.BasicCityPackage);
            if (userPayBasicCities.Count() > 0)
            {
                ViewBag.BasicCitiesUsed = db.UserCities.Where(x => x.Email == User.Identity.Name).Count(x => x.City.CityPayItems.Where(z => z.PayItem.ItemType == (int)PayItemTypeEnum.BasicCity).Count() > 0);
                ViewBag.BasicCitiesAllowed = userPayBasicCities.Sum(x => x.PayItem.Number);
            }
            else
            {
                ViewBag.BasicCitiesUsed = 0;
                ViewBag.BasicCitiesAllowed = 0;
            }

            //Get user's premium cities count and # left
            var userPayPremCities = db.UserPayItems.Where(x => x.Email == User.Identity.Name && x.PayItem.ItemType == (int)PayItemTypeEnum.PremiumCityPackage);
            if (userPayPremCities.Count() > 0)
            {
                ViewBag.PremiumCitiesUsed = db.UserCities.Where(x => x.Email == User.Identity.Name).Count(x => x.City.CityPayItems.Where(z => z.PayItem.ItemType == (int)PayItemTypeEnum.PremiumCity).Count() > 0);
                ViewBag.PremiumCitiesAllowed = userPayPremCities.Sum(x => x.PayItem.Number);
            }
            else
            {
                ViewBag.PremiumCitiesUsed = 0;
                ViewBag.PremiumCitiesAllowed = 0;
            }
        }
    }
}