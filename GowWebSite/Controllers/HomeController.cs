using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GowWebSite.Models;
using System.Data.Entity.Validation;

namespace GowWebSite.Controllers
{
    public class HomeController : Controller
    {
        private GowEntities db = new GowEntities();

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

        public ActionResult FAQ()
        {
            ViewBag.Message = "Your FAQ page.";

            return View();
        }

        public ActionResult Deals()
        {
            ViewBag.Message = "Your Deals page.";

            return View();
        }

        public ActionResult Basic5()
        {
            UserPayItem package = new UserPayItem();
            package.Email = User.Identity.Name;
            package.PayItem = db.PayItems.Find((int)PayItemEnum.Basic5);
            package.Paid = false;
            db.UserPayItems.Add(package);
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            return RedirectToAction("Index", "Payment");
        }

        public ActionResult Basic10()
        {
            UserPayItem package = new UserPayItem();
            package.Email = User.Identity.Name;
            package.PayItem = db.PayItems.Find((int)PayItemEnum.Basic10);
            package.Paid = false;
            db.UserPayItems.Add(package);
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            return RedirectToAction("Index", "Payment");
        }

        public ActionResult Basic25()
        {
            UserPayItem package = new UserPayItem();
            package.Email = User.Identity.Name;
            package.PayItem = db.PayItems.Find((int)PayItemEnum.Basic25);
            package.Paid = false;
            db.UserPayItems.Add(package);
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            return RedirectToAction("Index", "Payment");
        }

        public ActionResult Basic50()
        {
            UserPayItem package = new UserPayItem();
            package.Email = User.Identity.Name;
            package.PayItem = db.PayItems.Find((int)PayItemEnum.Basic50);
            package.Paid = false;
            db.UserPayItems.Add(package);
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            return RedirectToAction("Index", "Payment");
        }

        public ActionResult Premium5()
        {
            UserPayItem package = new UserPayItem();
            package.Email = User.Identity.Name;
            package.PayItem = db.PayItems.Find((int)PayItemEnum.Premium5);
            package.Paid = false;
            db.UserPayItems.Add(package);
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            return RedirectToAction("Index", "Payment");
        }

        public ActionResult Premium10()
        {
            UserPayItem package = new UserPayItem();
            package.Email = User.Identity.Name;
            package.PayItem = db.PayItems.Find((int)PayItemEnum.Premium10);
            package.Paid = false;
            db.UserPayItems.Add(package);
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            return RedirectToAction("Index", "Payment");
        }

        public ActionResult Premium25()
        {
            UserPayItem package = new UserPayItem();
            package.Email = User.Identity.Name;
            package.PayItem = db.PayItems.Find((int)PayItemEnum.Premium25);
            package.Paid = false;
            db.UserPayItems.Add(package);
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            return RedirectToAction("Index", "Payment");
        }

        public ActionResult Premium50()
        {
            UserPayItem package = new UserPayItem();
            package.Email = User.Identity.Name;
            package.PayItem = db.PayItems.Find((int)PayItemEnum.Premium50);
            package.Paid = false;
            db.UserPayItems.Add(package);
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            return RedirectToAction("Index", "Payment");
        }
    }
}