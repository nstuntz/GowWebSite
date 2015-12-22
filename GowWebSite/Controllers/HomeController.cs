using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GowWebSite.Models;
using System.Data.Entity.Validation;
using System.Data.SqlClient;

namespace GowWebSite.Controllers
{
    public class HomeController : Controller
    {
        private GowEntities db = new GowEntities();
        private Filters filter = new Filters();

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

            ViewBag.HelmCoreList = db.Cores.Where(m => m.GearSlot == "Helm").GroupBy(m => m.GearName).Select(grp => grp.FirstOrDefault()).Select(m => new SelectListItem { Value = m.GearID.ToString(), Text = m.GearName });
            ViewBag.WeaponCoreList = db.Cores.Where(m => m.GearSlot == "Weapon").GroupBy(m => m.GearName).Select(grp => grp.FirstOrDefault()).Select(m => new SelectListItem { Value = m.GearID.ToString(), Text = m.GearName });
            ViewBag.ArmourCoreList = db.Cores.Where(m => m.GearSlot == "Armour").GroupBy(m => m.GearName).Select(grp => grp.FirstOrDefault()).Select(m => new SelectListItem { Value = m.GearID.ToString(), Text = m.GearName });
            ViewBag.FootCoreList = db.Cores.Where(m => m.GearSlot == "Feet").GroupBy(m => m.GearName).Select(grp => grp.FirstOrDefault()).Select(m => new SelectListItem { Value = m.GearID.ToString(), Text = m.GearName });
            ViewBag.AccessoryCoreList = db.Cores.Where(m => m.GearSlot == "Accessory").GroupBy(m => m.GearName).Select(grp => grp.FirstOrDefault()).Select(m => new SelectListItem { Value = m.GearID.ToString(), Text = m.GearName });            

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

            return View(filter);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cores(bool infantry, bool overall, bool cavalry, bool ranged, bool other, 
                                    bool attack, bool defence, bool health,
                                    bool attackDebuff, bool defenceDebuff, bool healthDebuff)
        {
            ViewBag.Message = "Your Cores Information Center.";
            //First get the set of booleans to call the SP with.

            bool otherParam = false;
            bool overallAttackParam = false;
            bool overallHealthParam = false;
            bool overallDefenceParam = false;
            bool overallAttackDebuffParam = false;
            bool overallHealthDebuffParam = false;
            bool overallDefenceDebuffParam = false;

            bool infantryAttackParam = false;
            bool infantryHealthParam = false;
            bool infantryDefenceParam = false;
            bool infantryAttackDebuffParam = false;
            bool infantryHealthDebuffParam = false;
            bool infantryDefenceDebuffParam = false;

            bool cavalryAttackParam = false;
            bool cavalryHealthParam = false;
            bool cavalryDefenceParam = false;
            bool cavalryAttackDebuffParam = false;
            bool cavalryHealthDebuffParam = false;
            bool cavalryDefenceDebuffParam = false;

            bool rangedAttackParam = false;
            bool rangedHealthParam = false;
            bool rangedDefenceParam = false;
            bool rangedAttackDebuffParam = false;
            bool rangedHealthDebuffParam = false;
            bool rangedDefenceDebuffParam = false;

            otherParam = other;

            if(overall)
            {
                if (attack) overallAttackParam = true;
                if (health) overallHealthParam = true;
                if (defence) overallDefenceParam = true;
                if (attackDebuff) overallAttackDebuffParam = true;
                if (healthDebuff) overallHealthDebuffParam = true;
                if (defenceDebuff) overallDefenceDebuffParam = true;
            }
            if (ranged)
            {
                if (attack) rangedAttackParam = true;
                if (health) rangedHealthParam = true;
                if (defence) rangedDefenceParam = true;
                if (attackDebuff) rangedAttackDebuffParam = true;
                if (healthDebuff) rangedHealthDebuffParam = true;
                if (defenceDebuff) rangedDefenceDebuffParam = true;
            }
            if (infantry)
            {
                if (attack) infantryAttackParam = true;
                if (health) infantryHealthParam = true;
                if (defence) infantryDefenceParam = true;
                if (attackDebuff) infantryAttackDebuffParam = true;
                if (healthDebuff) infantryHealthDebuffParam = true;
                if (defenceDebuff) infantryDefenceDebuffParam = true;
            }
            if (cavalry)
            {
                if (attack) cavalryAttackParam = true;
                if (health) cavalryHealthParam = true;
                if (defence) cavalryDefenceParam = true;
                if (attackDebuff) cavalryAttackDebuffParam = true;
                if (healthDebuff) cavalryHealthDebuffParam = true;
                if (defenceDebuff) cavalryDefenceDebuffParam = true;
            }

            var filteredPieces = db.Database.SqlQuery<Piece>("EXEC FilterPieces @Other, @OverallAttack, @InfantryAttack, @RangedAttack, @CavalryAttack, @OverallHealth, @InfantryHealth, @RangedHealth, @CavalryHealth, @OverallDefence, @InfantryDefence, @RangedDefence, @CavalryDefence, @OverallAttackDebuff, @InfantryAttackDebuff, @RangedAttackDebuff, @CavalryAttackDebuff, @OverallHealthDebuff, @InfantryHealthDebuff, @RangedHealthDebuff, @CavalryHealthDebuff, @OverallDefenceDebuff, @InfantryDefenceDebuff, @RangedDefenceDebuff, @CavalryDefenceDebuff ", 
                new SqlParameter ("Other", otherParam),
                new SqlParameter("OverallAttack", overallAttackParam),
                new SqlParameter("InfantryAttack", infantryAttackParam),
                new SqlParameter("RangedAttack", rangedAttackParam),
                new SqlParameter("CavalryAttack", cavalryAttackParam),
                new SqlParameter("OverallHealth", overallHealthParam),
                new SqlParameter("InfantryHealth", infantryHealthParam),
                new SqlParameter("RangedHealth", rangedHealthParam),
                new SqlParameter("CavalryHealth", cavalryHealthParam),
                new SqlParameter("OverallDefence", overallDefenceParam),
                new SqlParameter("InfantryDefence", infantryDefenceParam),
                new SqlParameter("RangedDefence", rangedDefenceParam),
                new SqlParameter("CavalryDefence", cavalryDefenceParam),
                new SqlParameter("OverallAttackDebuff", overallAttackDebuffParam),
                new SqlParameter("InfantryAttackDebuff", infantryAttackDebuffParam),
                new SqlParameter("RangedAttackDebuff", rangedAttackDebuffParam),
                new SqlParameter("CavalryAttackDebuff", cavalryAttackDebuffParam),
                new SqlParameter("OverallHealthDebuff", overallHealthDebuffParam),
                new SqlParameter("InfantryHealthDebuff", infantryHealthDebuffParam),
                new SqlParameter("RangedHealthDebuff", rangedHealthDebuffParam),
                new SqlParameter("CavalryHealthDebuff", cavalryHealthDebuffParam),
                new SqlParameter("OverallDefenceDebuff", overallDefenceDebuffParam),
                new SqlParameter("InfantryDefenceDebuff", infantryDefenceDebuffParam),
                new SqlParameter("RangedDefenceDebuff", rangedDefenceDebuffParam),
                new SqlParameter("CavalryDefenceDebuff", cavalryDefenceDebuffParam)).ToList();

            ViewBag.PiecesList = filteredPieces.GroupBy(m => m.PieceName).Select(grp => grp.FirstOrDefault()).Select(m => new SelectListItem { Value = m.PieceID.ToString(), Text = m.PieceName });


            var filteredCores = db.Database.SqlQuery<Core>("EXEC FilterCores @Other, @OverallAttack, @InfantryAttack, @RangedAttack, @CavalryAttack, @OverallHealth, @InfantryHealth, @RangedHealth, @CavalryHealth, @OverallDefence, @InfantryDefence, @RangedDefence, @CavalryDefence, @OverallAttackDebuff, @InfantryAttackDebuff, @RangedAttackDebuff, @CavalryAttackDebuff, @OverallHealthDebuff, @InfantryHealthDebuff, @RangedHealthDebuff, @CavalryHealthDebuff, @OverallDefenceDebuff, @InfantryDefenceDebuff, @RangedDefenceDebuff, @CavalryDefenceDebuff ",
               new SqlParameter("Other", otherParam),
               new SqlParameter("OverallAttack", overallAttackParam),
               new SqlParameter("InfantryAttack", infantryAttackParam),
               new SqlParameter("RangedAttack", rangedAttackParam),
               new SqlParameter("CavalryAttack", cavalryAttackParam),
               new SqlParameter("OverallHealth", overallHealthParam),
               new SqlParameter("InfantryHealth", infantryHealthParam),
               new SqlParameter("RangedHealth", rangedHealthParam),
               new SqlParameter("CavalryHealth", cavalryHealthParam),
               new SqlParameter("OverallDefence", overallDefenceParam),
               new SqlParameter("InfantryDefence", infantryDefenceParam),
               new SqlParameter("RangedDefence", rangedDefenceParam),
               new SqlParameter("CavalryDefence", cavalryDefenceParam),
               new SqlParameter("OverallAttackDebuff", overallAttackDebuffParam),
               new SqlParameter("InfantryAttackDebuff", infantryAttackDebuffParam),
               new SqlParameter("RangedAttackDebuff", rangedAttackDebuffParam),
               new SqlParameter("CavalryAttackDebuff", cavalryAttackDebuffParam),
               new SqlParameter("OverallHealthDebuff", overallHealthDebuffParam),
               new SqlParameter("InfantryHealthDebuff", infantryHealthDebuffParam),
               new SqlParameter("RangedHealthDebuff", rangedHealthDebuffParam),
               new SqlParameter("CavalryHealthDebuff", cavalryHealthDebuffParam),
               new SqlParameter("OverallDefenceDebuff", overallDefenceDebuffParam),
               new SqlParameter("InfantryDefenceDebuff", infantryDefenceDebuffParam),
               new SqlParameter("RangedDefenceDebuff", rangedDefenceDebuffParam),
               new SqlParameter("CavalryDefenceDebuff", cavalryDefenceDebuffParam)).ToList();

            ViewBag.HelmCoreList = filteredCores.Where(m => m.GearSlot == "Helm").GroupBy(m => m.GearName).Select(grp => grp.FirstOrDefault()).Select(m => new SelectListItem { Value = m.GearID.ToString(), Text = m.GearName });
            ViewBag.WeaponCoreList = filteredCores.Where(m => m.GearSlot == "Weapon").GroupBy(m => m.GearName).Select(grp => grp.FirstOrDefault()).Select(m => new SelectListItem { Value = m.GearID.ToString(), Text = m.GearName });
            ViewBag.ArmourCoreList = filteredCores.Where(m => m.GearSlot == "Armour").GroupBy(m => m.GearName).Select(grp => grp.FirstOrDefault()).Select(m => new SelectListItem { Value = m.GearID.ToString(), Text = m.GearName });
            ViewBag.FootCoreList = filteredCores.Where(m => m.GearSlot == "Feet").GroupBy(m => m.GearName).Select(grp => grp.FirstOrDefault()).Select(m => new SelectListItem { Value = m.GearID.ToString(), Text = m.GearName });
            ViewBag.AccessoryCoreList = filteredCores.Where(m => m.GearSlot == "Accessory").GroupBy(m => m.GearName).Select(grp => grp.FirstOrDefault()).Select(m => new SelectListItem { Value = m.GearID.ToString(), Text = m.GearName });

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
            var newFilter = new Filters();
            newFilter.Overall = overall;
            return View(newFilter);
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