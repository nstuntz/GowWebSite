using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
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
            ViewBag.PiecesList = Enumerable.Empty<SelectListItem>();// db.Pieces.GroupBy(m=>m.PieceName).Select(grp=>grp.FirstOrDefault()).Select(m => new SelectListItem { Value = m.PieceID.ToString(), Text = m.PieceName });
            //ViewBag.Pieces = new GowWebSite.Models.CoresModel(db);

            ViewBag.HelmCoreList = Enumerable.Empty<SelectListItem>();// db.Cores.Where(m => m.GearSlot == "Helm").GroupBy(m => m.GearName).Select(grp => grp.FirstOrDefault()).Select(m => new SelectListItem { Value = m.GearID.ToString(), Text = m.GearName });
            ViewBag.WeaponCoreList = Enumerable.Empty<SelectListItem>();// db.Cores.Where(m => m.GearSlot == "Weapon").GroupBy(m => m.GearName).Select(grp => grp.FirstOrDefault()).Select(m => new SelectListItem { Value = m.GearID.ToString(), Text = m.GearName });
            ViewBag.ArmourCoreList = Enumerable.Empty<SelectListItem>();// db.Cores.Where(m => m.GearSlot == "Armour").GroupBy(m => m.GearName).Select(grp => grp.FirstOrDefault()).Select(m => new SelectListItem { Value = m.GearID.ToString(), Text = m.GearName });
            ViewBag.FootCoreList = Enumerable.Empty<SelectListItem>();// db.Cores.Where(m => m.GearSlot == "Feet").GroupBy(m => m.GearName).Select(grp => grp.FirstOrDefault()).Select(m => new SelectListItem { Value = m.GearID.ToString(), Text = m.GearName });
            ViewBag.AccessoryCoreList = Enumerable.Empty<SelectListItem>();// db.Cores.Where(m => m.GearSlot == "Accessory").GroupBy(m => m.GearName).Select(grp => grp.FirstOrDefault()).Select(m => new SelectListItem { Value = m.GearID.ToString(), Text = m.GearName });            

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

            var overallTypes = new Dictionary<int, string>()
                {     
                    {0,"Troop Attack"},
                    {1,"Defence"},
                    {2,"Health"},
                    {3,"Enemy Attack Debuff"},
                    {4,"Enemy Defence Debuff"},              
                    {5,"Enemy Health Debuff"}
                };

            ViewBag.OverallTypes = overallTypes;

            var infantryTypes = new Dictionary<int, string>()
                {     
                    {0,"Troop Attack"},
                    {1,"Defence"},
                    {2,"Health"},
                    {3,"Enemy Attack Debuff"},
                    {4,"Enemy Defence Debuff"},              
                    {5,"Enemy Health Debuff"}
                };

            ViewBag.InfantryTypes = infantryTypes;

            var cavalryTypes = new Dictionary<int, string>()
                {     
                    {0,"Troop Attack"},
                    {1,"Defence"},
                    {2,"Health"},
                    {3,"Enemy Attack Debuff"},
                    {4,"Enemy Defence Debuff"},              
                    {5,"Enemy Health Debuff"}
                };

            ViewBag.CavalryTypes = cavalryTypes;

            var rangedTypes = new Dictionary<int, string>()
                {     
                    {0,"Troop Attack"},
                    {1,"Defence"},
                    {2,"Health"},
                    {3,"Enemy Attack Debuff"},
                    {4,"Enemy Defence Debuff"},              
                    {5,"Enemy Health Debuff"}
                };

            ViewBag.RangedTypes = rangedTypes;

            var otherTypes = new Dictionary<int, string>()
                {     
                    {0,"Hero"},
                    {1,"Siege"},
                    {2,"Trap"}
                };

            ViewBag.OtherTypes = otherTypes;
            ViewBag.OverallBoost = "Health";

            ViewBag.Filters = "None";
            return View(filter);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cores(FormCollection formValues)
        {
            ViewBag.Message = "Your Cores Information Center.";
            //First get the set of booleans to call the SP with.
            string[] overall = new string[0];
            string[] infantry = new string[0];
            string[] ranged = new string[0];
            string[] cavalry = new string[0];
            string[] other = new string[0];
            StringBuilder filters = new StringBuilder();

            if(formValues["OverallBoost"] != null)
            {
                overall = formValues["OverallBoost"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            }
            if (formValues["InfantryBoost"] != null)
            {
                infantry = formValues["InfantryBoost"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            }
            if (formValues["RangedBoost"] != null)
            {
                ranged = formValues["RangedBoost"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            }
            if (formValues["CavalryBoost"] != null)
            {
                cavalry = formValues["CavalryBoost"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            }
            if (formValues["OtherBoost"] != null)
            {
                other = formValues["OtherBoost"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            }



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

            //otherParam = other;

            foreach(string index in overall)
            {
                switch (index)
                {
                    case "0": 
                        overallAttackParam = true;
                        filters.Append("Overall Attack;");
                        break;
                    case "2":
                        overallHealthParam = true;
                        filters.Append("Overall Health;");
                        break;
                    case "1":
                        overallDefenceParam = true;
                        filters.Append("Overall Defence;");
                        break;
                    case "3":
                        overallAttackDebuffParam = true;
                        filters.Append("Overall Enemy Attack Debuff;");
                        break;
                    case "4":
                        overallHealthDebuffParam = true;
                        filters.Append("Overall Enemy Health Debuff;");
                        break;
                    case "5":
                        overallDefenceDebuffParam = true;
                        filters.Append("Overall Enemy Defence Debuff;");
                        break;
                    default:
                        break;
                }
            }

            foreach (string index in infantry)
            {
                switch (index)
                {
                    case "0":
                        infantryAttackParam = true;
                        filters.Append("Infantry Attack;");
                        break;
                    case "2":
                        infantryHealthParam = true;
                        filters.Append("Infantry Health;");
                        break;
                    case "1":
                        infantryDefenceParam = true;
                        filters.Append("Infantry Defence;");
                        break;
                    case "3":
                        infantryAttackDebuffParam = true;
                        filters.Append("Infantry Enemy Attack Debuff;");
                        break;
                    case "4":
                        infantryHealthDebuffParam = true;
                        filters.Append("Infantry Enemy Health Debuff;");
                        break;
                    case "5":
                        infantryDefenceDebuffParam = true;
                        filters.Append("Infantry Enemy Defence Debuff;");
                        break;
                    default:
                        break;
                }
            }

            foreach (string index in ranged)
            {
                switch (index)
                {
                    case "0":
                        rangedAttackParam = true;
                        filters.Append("Ranged Attack;");
                        break;
                    case "2":
                        rangedHealthParam = true;
                        filters.Append("Ranged Health;");
                        break;
                    case "1":
                        rangedDefenceParam = true;
                        filters.Append("Ranged Defence;");
                        break;
                    case "3":
                        rangedAttackDebuffParam = true;
                        filters.Append("Ranged Enemy Attack Debuff;");
                        break;
                    case "4":
                        rangedHealthDebuffParam = true;
                        filters.Append("Ranged Enemy Health Debuff;");
                        break;
                    case "5":
                        rangedDefenceDebuffParam = true;
                        filters.Append("Ranged Enemy Defence Debuff;");
                        break;
                    default:
                        break;
                }
            }

            foreach (string index in cavalry)
            {
                switch (index)
                {
                    case "0":
                        cavalryAttackParam = true;
                        filters.Append("Cavalry Attack;");
                        break;
                    case "2":
                        cavalryHealthParam = true;
                        filters.Append("Cavalry Health;");
                        break;
                    case "1":
                        cavalryDefenceParam = true;
                        filters.Append("Cavalry Defence;");
                        break;
                    case "3":
                        cavalryAttackDebuffParam = true;
                        filters.Append("Cavalry Enemy Attack Debuff;");
                        break;
                    case "4":
                        cavalryHealthDebuffParam = true;
                        filters.Append("Cavalry Enemy Health Debuff;");
                        break;
                    case "5":
                        cavalryDefenceDebuffParam = true;
                        filters.Append("Cavalry Enemy Defence Debuff;");
                        break;
                    default:
                        break;
                }
            }

            foreach (string index in other)
            {
                switch (index)
                {
                    case "0":
                    case "1":
                    case "2":
                        otherParam = true;
                        filters.Append("Other;");
                        break;
                    default:
                        break;
                }
            }

            //if(overall)
            //{
            //    if (attack) overallAttackParam = true;
            //    if (health) overallHealthParam = true;
            //    if (defence) overallDefenceParam = true;
            //    if (attackDebuff) overallAttackDebuffParam = true;
            //    if (healthDebuff) overallHealthDebuffParam = true;
            //    if (defenceDebuff) overallDefenceDebuffParam = true;
            //}
            //if (ranged)
            //{
            //    if (attack) rangedAttackParam = true;
            //    if (health) rangedHealthParam = true;
            //    if (defence) rangedDefenceParam = true;
            //    if (attackDebuff) rangedAttackDebuffParam = true;
            //    if (healthDebuff) rangedHealthDebuffParam = true;
            //    if (defenceDebuff) rangedDefenceDebuffParam = true;
            //}
            //if (infantry)
            //{
            //    if (attack) infantryAttackParam = true;
            //    if (health) infantryHealthParam = true;
            //    if (defence) infantryDefenceParam = true;
            //    if (attackDebuff) infantryAttackDebuffParam = true;
            //    if (healthDebuff) infantryHealthDebuffParam = true;
            //    if (defenceDebuff) infantryDefenceDebuffParam = true;
            //}
            //if (cavalry)
            //{
            //    if (attack) cavalryAttackParam = true;
            //    if (health) cavalryHealthParam = true;
            //    if (defence) cavalryDefenceParam = true;
            //    if (attackDebuff) cavalryAttackDebuffParam = true;
            //    if (healthDebuff) cavalryHealthDebuffParam = true;
            //    if (defenceDebuff) cavalryDefenceDebuffParam = true;
            //}

            var filteredPieces = db.Database.SqlQuery<SortPiece>("EXEC FilterPieces @Other, @OverallAttack, @InfantryAttack, @RangedAttack, @CavalryAttack, @OverallHealth, @InfantryHealth, @RangedHealth, @CavalryHealth, @OverallDefence, @InfantryDefence, @RangedDefence, @CavalryDefence, @OverallAttackDebuff, @InfantryAttackDebuff, @RangedAttackDebuff, @CavalryAttackDebuff, @OverallHealthDebuff, @InfantryHealthDebuff, @RangedHealthDebuff, @CavalryHealthDebuff, @OverallDefenceDebuff, @InfantryDefenceDebuff, @RangedDefenceDebuff, @CavalryDefenceDebuff ", 
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

            ViewBag.PiecesList = filteredPieces.OrderByDescending(m => m.SortValue).Select(m => new SelectListItem { Value = m.PieceID.ToString(), Text = m.PieceName });


            var filteredCores = db.Database.SqlQuery<SortCore>("EXEC FilterCores @Other, @OverallAttack, @InfantryAttack, @RangedAttack, @CavalryAttack, @OverallHealth, @InfantryHealth, @RangedHealth, @CavalryHealth, @OverallDefence, @InfantryDefence, @RangedDefence, @CavalryDefence, @OverallAttackDebuff, @InfantryAttackDebuff, @RangedAttackDebuff, @CavalryAttackDebuff, @OverallHealthDebuff, @InfantryHealthDebuff, @RangedHealthDebuff, @CavalryHealthDebuff, @OverallDefenceDebuff, @InfantryDefenceDebuff, @RangedDefenceDebuff, @CavalryDefenceDebuff ",
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

            ViewBag.HelmCoreList = filteredCores.Where(m => m.GearSlot == "Helm").OrderByDescending(m => m.SortValue).Select(m => new SelectListItem { Value = m.GearID.ToString(), Text = m.GearName });
            ViewBag.WeaponCoreList = filteredCores.Where(m => m.GearSlot == "Weapon").OrderByDescending(m => m.SortValue).Select(m => new SelectListItem { Value = m.GearID.ToString(), Text = m.GearName });
            ViewBag.ArmourCoreList = filteredCores.Where(m => m.GearSlot == "Armour").OrderByDescending(m => m.SortValue).Select(m => new SelectListItem { Value = m.GearID.ToString(), Text = m.GearName });
            ViewBag.FootCoreList = filteredCores.Where(m => m.GearSlot == "Feet").OrderByDescending(m => m.SortValue).Select(m => new SelectListItem { Value = m.GearID.ToString(), Text = m.GearName });
            ViewBag.AccessoryCoreList = filteredCores.Where(m => m.GearSlot == "Accessory").OrderByDescending(m => m.SortValue).Select(m => new SelectListItem { Value = m.GearID.ToString(), Text = m.GearName });

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

            var overallTypes = new Dictionary<int, string>()
                {     
                    {0,"Troop Attack"},
                    {1,"Defence"},
                    {2,"Health"},
                    {3,"Enemy Attack Debuff"},
                    {4,"Enemy Defence Debuff"},              
                    {5,"Enemy Health Debuff"}
                };

            ViewBag.OverallTypes = overallTypes;

            var infantryTypes = new Dictionary<int, string>()
                {     
                    {0,"Troop Attack"},
                    {1,"Defence"},
                    {2,"Health"},
                    {3,"Enemy Attack Debuff"},
                    {4,"Enemy Defence Debuff"},              
                    {5,"Enemy Health Debuff"}
                };

            ViewBag.InfantryTypes = infantryTypes;

            var cavalryTypes = new Dictionary<int, string>()
                {     
                    {0,"Troop Attack"},
                    {1,"Defence"},
                    {2,"Health"},
                    {3,"Enemy Attack Debuff"},
                    {4,"Enemy Defence Debuff"},              
                    {5,"Enemy Health Debuff"}
                };

            ViewBag.CavalryTypes = cavalryTypes;

            var rangedTypes = new Dictionary<int, string>()
                {     
                    {0,"Troop Attack"},
                    {1,"Defence"},
                    {2,"Health"},
                    {3,"Enemy Attack Debuff"},
                    {4,"Enemy Defence Debuff"},              
                    {5,"Enemy Health Debuff"}
                };

            ViewBag.RangedTypes = rangedTypes;

            var otherTypes = new Dictionary<int, string>()
                {     
                    {0,"Hero"},
                    {1,"Siege"},
                    {2,"Trap"}
                };

            ViewBag.OtherTypes = otherTypes;

            var newFilter = new Filters();
            //newFilter.Overall = overall;
            ViewBag.Filters = filters.ToString();
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