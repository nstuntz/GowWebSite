using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GowWebSite.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Pay()
        //{

        //    if (PaymentOption == "PayPal")
        //    {
        //        NVPAPICaller test = new NVPAPICaller();

        //        string retMsg = "";
        //        string token = "";

        //        if (Session["payment_amt"] != null)
        //        {
        //            string amt = Session["payment_amt"].ToString();

        //            //Optional Shipping Address entered on the merchant site
        //            string shipToName = "<PAYMENTREQUEST_0_SHIPTONAME>";
        //            string shipToStreet = "<PAYMENTREQUEST_0_SHIPTOSTREET>";
        //            string shipToStreet2 = "<PAYMENTREQUEST_0_SHIPTOSTREET2>";
        //            string shipToCity = "<PAYMENTREQUEST_0_SHIPTOCITY>";
        //            string shipToState = "<PAYMENTREQUEST_0_SHIPTOSTATE>";
        //            string shipToZip = "<PAYMENTREQUEST_0_SHIPTOZIP>";
        //            string shipToCountryCode = "<PAYMENTREQUEST_0_SHIPTOCOUNTRYCODE>";

        //            bool ret = test.MarkExpressCheckout(amt, shipToName, shipToStreet, shipToStreet2,
        //                            shipToCity, shipToState, shipToZip, shipToCountryCode,
        //                            ref token, ref retMsg);
        //            if (ret)
        //            {
        //                Session["token"] = token;
        //                Response.Redirect(retMsg);
        //            }
        //            else
        //            {
        //                Response.Redirect("APIError.aspx?" + retMsg);
        //            }
        //        }
        //        else
        //        {
        //            Response.Redirect("APIError.aspx?ErrorCode=AmtMissing");
        //        }
        //    }
        //}

        //public ActionResult Review()
        //{
        //    if (PaymentOption == "PayPal")
        //    {
        //        NVPAPICaller test = new NVPAPICaller();

        //        string retMsg = "";
        //        string token = "";
        //        string payerId = "";

        //        token = Session["token"].ToString();

        //        bool ret = test.GetShippingDetails(token, ref payerId, ref shippingAddress, ref retMsg);
        //        if (ret)
        //        {
        //            Session["payerId"] = payerId;
        //            Response.Write(shippingAddress);
        //        }
        //        else
        //        {
        //            Response.Redirect("APIError.aspx?" + retMsg);
        //        }
        //    }
        //}

        //public ActionResult Confirm()
        //{
        //    if (PaymentOption == "PayPal")
        //    {
        //        NVPAPICaller test = new NVPAPICaller();

        //        string retMsg = "";
        //        string token = "";
        //        string finalPaymentAmount = "";
        //        string payerId = "";
        //        NVPCodec decoder;

        //        token = Session["token"].ToString();
        //        payerId = Session["payerId"].ToString();
        //        finalPaymentAmount = Session["payment_amt"].ToString();

        //        bool ret = test.ConfirmPayment(finalPaymentAmount, token, PayerId, ref decoder, ref retMsg);
        //        if (ret)
        //        {
        //            // Unique transaction ID of the payment. Note:  If the PaymentAction of the request was Authorization or Order, this value is your AuthorizationID for use with the Authorization & Capture APIs. 
        //            string transactionId = decoder["PAYMENTINFO_0_TRANSACTIONID"];

        //            // The type of transaction Possible values: l  cart l  express-checkout 
        //            string transactionType = decoder["PAYMENTINFO_0_TRANSACTIONTYPE"];

        //            // Indicates whether the payment is instant or delayed. Possible values: l  none l  echeck l  instant 
        //            string paymentType = decoder["PAYMENTINFO_0_PAYMENTTYPE"];

        //            // Time/date stamp of payment
        //            string orderTime = decoder["PAYMENTINFO_0_ORDERTIME"];

        //            // The final amount charged, including any shipping and taxes from your Merchant Profile.
        //            string amt = decoder["PAYMENTINFO_0_AMT"];

        //            // A three-character currency code for one of the currencies listed in PayPay-Supported Transactional Currencies. Default: USD.    
        //            string currencyCode = decoder["PAYMENTINFO_0_CURRENCYCODE"];

        //            // PayPal fee amount charged for the transaction    
        //            string feeAmt = decoder["PAYMENTINFO_0_FEEAMT"];

        //            // Amount deposited in your PayPal account after a currency conversion.    
        //            string settleAmt = decoder["PAYMENTINFO_0_SETTLEAMT"];

        //            // Tax charged on the transaction.    
        //            string taxAmt = decoder["PAYMENTINFO_0_TAXAMT"];

        //            //' Exchange rate if a currency conversion occurred. Relevant only if your are billing in their non-primary currency. If 
        //            string exchangeRate = decoder["PAYMENTINFO_0_EXCHANGERATE"];
        //        }
        //        else
        //        {
        //            Response.Redirect("APIError.aspx?" + retMsg);
        //        }
        //    }
        //}
    }
}