﻿using System;
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

        public ActionResult Cores()
        {
            ViewBag.Message = "Your Cores Information Center.";
            ViewBag.PiecesList = db.Pieces.GroupBy(m=>m.PieceName).Select(grp=>grp.FirstOrDefault()).Select(m => new SelectListItem { Value = m.PieceID.ToString(), Text = m.PieceName });
            //ViewBag.Pieces = new GowWebSite.Models.CoresModel(db);

            ViewBag.HelmCoreList = db.Cores.Where(m => m.GearSlot == "Helm").Select(m => new SelectListItem { Value = m.GearID.ToString(), Text = m.GearName });
            ViewBag.WeaponCoreList = db.Cores.Where(m => m.GearSlot == "Weapon").Select(m => new SelectListItem { Value = m.GearID.ToString(), Text = m.GearName });
            ViewBag.ArmourCoreList = db.Cores.Where(m => m.GearSlot == "Armour").Select(m => new SelectListItem { Value = m.GearID.ToString(), Text = m.GearName });
            ViewBag.FootCoreList = db.Cores.Where(m => m.GearSlot == "Foot").Select(m => new SelectListItem { Value = m.GearID.ToString(), Text = m.GearName });
            ViewBag.AccessoryCoreList = db.Cores.Where(m => m.GearSlot == "Accessory").Select(m => new SelectListItem { Value = m.GearID.ToString(), Text = m.GearName });            

            var levels = new Dictionary<int, string>()
                {     
                    {6,"Gold (6)"},
                    {5,"Purple (5)"},
                    {4,"Blue (4)"},
                    {3,"Green (3)"},
                    {2,"Grey (2)"},              
                    {1,"White (1)"}
                };

            ViewBag.Levels = levels;

            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetPieceBoosts(string PieceName, int PieceLevel)
        {
            Piece p = db.Pieces.Where(m => m.PieceName == PieceName && m.PieceLevel == PieceLevel).FirstOrDefault();
            return Json(p, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetCoreBoosts(string coreName, int coreLevel)
        {
            Core p = db.Cores.Where(m => m.GearName == coreName && m.GearLevel == coreLevel).FirstOrDefault();
            return Json(p, JsonRequestBehavior.AllowGet);
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

        //public ActionResult Premium5()
        //{
        //    UserPayItem package = new UserPayItem();
        //    package.Email = User.Identity.Name;
        //    package.PayItem = db.PayItems.Find((int)PayItemEnum.Premium5);
        //    package.Paid = false;
        //    db.UserPayItems.Add(package);
        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbEntityValidationException e)
        //    {
        //        throw e;
        //    }
        //    return RedirectToAction("Index", "Payment");
        //}

        //public ActionResult Premium10()
        //{
        //    UserPayItem package = new UserPayItem();
        //    package.Email = User.Identity.Name;
        //    package.PayItem = db.PayItems.Find((int)PayItemEnum.Premium10);
        //    package.Paid = false;
        //    db.UserPayItems.Add(package);
        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbEntityValidationException e)
        //    {
        //        throw e;
        //    }
        //    return RedirectToAction("Index", "Payment");
        //}

        //public ActionResult Premium25()
        //{
        //    UserPayItem package = new UserPayItem();
        //    package.Email = User.Identity.Name;
        //    package.PayItem = db.PayItems.Find((int)PayItemEnum.Premium25);
        //    package.Paid = false;
        //    db.UserPayItems.Add(package);
        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbEntityValidationException e)
        //    {
        //        throw e;
        //    }
        //    return RedirectToAction("Index", "Payment");
        //}

        //public ActionResult Premium50()
        //{
        //    UserPayItem package = new UserPayItem();
        //    package.Email = User.Identity.Name;
        //    package.PayItem = db.PayItems.Find((int)PayItemEnum.Premium50);
        //    package.Paid = false;
        //    db.UserPayItems.Add(package);
        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbEntityValidationException e)
        //    {
        //        throw e;
        //    }
        //    return RedirectToAction("Index", "Payment");
        //}
    }
}