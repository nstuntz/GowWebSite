using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using GowWebSite.Models;
using System.Data.Entity.Validation;

namespace GowWebSite.Controllers
{
    [Authorize]
    public class CityController : Controller
    {
        private GowEntities db = new GowEntities();

        // GET: City
        public ActionResult Index()
        {
            string selectedUser = User.Identity.Name;
            if (Request.Cookies["SelectedUser"] != null)
            {
                selectedUser = Request.Cookies["SelectedUser"].Value;
            }

            try
            {
                var userCities = db.Cities.Where(x => db.UserCities.Where(y => y.Email == selectedUser).Select(id => id.CityID).Contains(x.CityID));
                var orderedCities = userCities.OrderBy(x => x.CityName);

                var cities = orderedCities.Include(c => c.Login).Include(c => c.ResourceType).Include(c => c.CityInfo);
                return View(cities.ToList());
            }
            catch
            {
                return View(new List<City>());
            }
        }

        [HttpPost]
        public ActionResult SelectUser(string UserIds)
        {
            if (UserIds != null)
            {
                Response.Cookies["SelectedUser"].Value = UserIds;
                Response.Cookies["SelectedUser"].Expires = DateTime.Now.AddYears(1);
                //Response.Cookies["SelectedAlliance"].Value = allianceID.Value.ToString();
                //Response.Cookies["SelectedAlliance"].Expires = DateTime.Now.AddYears(1);

            }

            return RedirectToAction("Index", "City");
        }

        // GET: City/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // GET: City/Create
        public ActionResult Create()
        {
            ViewBag.ResourceTypeID = new SelectList(db.ResourceTypes, "ResourceTypeID", "Type");
            return View();
        }

        // POST: City/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCityModel city)
        {

            if (ModelState.IsValid)
            {
                db.CreateExistingCitySetup(city.UserName, city.Password, city.CityName, city.CityX, city.CityY, city.Alliance, city.ResourceTypeID, city.SHLevel, User.Identity.Name);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ResourceTypeID = new SelectList(db.ResourceTypes, "ResourceTypeID", "Type", city.ResourceTypeID);
            return View(city);
        }


        // GET: City/CreateFull
        public ActionResult CreateFull()
        {
            ViewBag.ResourceTypeID = new SelectList(db.ResourceTypes, "ResourceTypeID", "Type");
            City newCity = new City();
            newCity.Login = new Login();
            newCity.Login.DelayTier = Login.AllowDelays.Min360;

            newCity.CityInfo = new CityInfo();

            return View(newCity);
        }
        
        // POST: City/CreateFull
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFull(City city)
        {
            if (string.IsNullOrWhiteSpace(city.Login.UserName))
            {
                ModelState.AddModelError(city.Login.UserName, "UserName is required.");
            }

            if (city.CityInfo.Rally)
            {
                if (!city.CityInfo.RallyX.HasValue)
                {
                    ModelState.AddModelError(String.Empty, "RallyX is required when rallying.");
                }
                if (!city.CityInfo.RallyY.HasValue)
                {
                    ModelState.AddModelError(String.Empty, "RallyY is required when rallying.");
                }
            }

            if (city.CityInfo.Shield && city.CityInfo.Rally)
            {
                ModelState.AddModelError(String.Empty, "You can not both rally and shield a city.");
            }

            if (ModelState.IsValid)
            {
                //Setup the Login defaults
                city.Login.InProcess = "0";
                city.Login.LastRun = DateTime.Now;
                city.Login.CreateDate = DateTime.Now;
                city.Login.Active = true;
                city.Login.PaidThrough = DateTime.Today.AddMonths(-1);

                //Set the CityInfo Defaults
                city.CityInfo.LastAthenaGift = DateTime.Today;
                city.CityInfo.LastBank = DateTime.Today;
                city.CityInfo.LastRally = DateTime.Today;
                city.CityInfo.LastShield = DateTime.Today;
                city.CityInfo.LastTreasury = DateTime.Today;
                city.CityInfo.LastUpgrade = DateTime.Today;
                city.CityInfo.TreasuryDue = DateTime.Today;
                
                //Add the userCity
                UserCity uc = new UserCity();
                uc.Email = User.Identity.Name;
                city.UserCities.Add(uc);

                db.Logins.Add(city.Login);
                db.Cities.Add(city);
                db.CityInfoes.Add(city.CityInfo);
                db.UserCities.Add(uc);

                //Now add the costs
                CityPayItem itemHours = new CityPayItem();
                switch (city.Login.LoginDelayMin)
                {
                    case (int)Login.AllowDelays.Min360:
                        itemHours.PayItem = db.PayItems.Find((int)PayItems.Hour6);
                        break;
                    case (int)Login.AllowDelays.Min180:
                        itemHours.PayItem = db.PayItems.Find((int)PayItems.Hour3);
                        break;
                    case (int)Login.AllowDelays.Min60:
                        itemHours.PayItem = db.PayItems.Find((int)PayItems.Hour1);
                        break;
                    default:
                        itemHours.PayItem = db.PayItems.Find((int)PayItems.Hour1);
                        break;
                }
                db.CityPayItems.Add(itemHours);
                city.CityPayItems.Add(itemHours);

                if (city.CityInfo.Bank)
                {
                    CityPayItem itemBank = new CityPayItem();
                    itemBank.PayItem = db.PayItems.Find((int)PayItems.Bank);
                    db.CityPayItems.Add(itemBank);
                    city.CityPayItems.Add(itemBank);
                }
                if (city.CityInfo.Rally)
                {
                    CityPayItem itemRally = new CityPayItem();
                    itemRally.PayItem = db.PayItems.Find((int)PayItems.Rally);
                    db.CityPayItems.Add(itemRally);
                    city.CityPayItems.Add(itemRally);
                }
                if (city.CityInfo.Upgrade)
                {
                    CityPayItem itemUpgrade = new CityPayItem();
                    itemUpgrade.PayItem = db.PayItems.Find((int)PayItems.Upgrade);
                    db.CityPayItems.Add(itemUpgrade);
                    city.CityPayItems.Add(itemUpgrade);
                }

                if (city.CityInfo.Shield)
                {
                    CityPayItem itemShield = new CityPayItem();
                    itemShield.PayItem = db.PayItems.Find((int)PayItems.Shield);
                    db.CityPayItems.Add(itemShield);
                    city.CityPayItems.Add(itemShield);
                }

                if (city.CityInfo.Treasury)
                {
                    CityPayItem itemTreasury = new CityPayItem();
                    itemTreasury.PayItem = db.PayItems.Find((int)PayItems.Treasury);
                    db.CityPayItems.Add(itemTreasury);
                    city.CityPayItems.Add(itemTreasury);
                }
                
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    throw e;
                }
                return RedirectToAction("Index","Payment");
            }
            ViewBag.ResourceTypeID = new SelectList(db.ResourceTypes, "ResourceTypeID", "Type", city.ResourceTypeID);
            return View(city);
        }

        // GET: City/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }

            //Fixing because it wasn't trimmed when it was put in
            city.Alliance = city.Alliance.Trim();


            ViewBag.LoginID = new SelectList(db.Logins, "LoginID", "UserName", city.LoginID);
            ViewBag.ResourceTypeID = new SelectList(db.ResourceTypes, "ResourceTypeID", "Type", city.ResourceTypeID);
            ViewBag.CityID = new SelectList(db.CityInfoes, "CityID", "RedeemCode", city.CityID);
            return View(city);
        }


        // GET: City/Delete/5
        public ActionResult Delete(int? cityID)
        {
            if (cityID == null || !cityID.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cities.Find(cityID);

            if (city == null)
            {
                return HttpNotFound();
            }
            
            //Check city access if this is not an admin user
            if (!User.IsInRole("Admin"))
            {
                if (city.UserCities.Where(x => x.Email == User.Identity.Name).Count() == 0)
                {
                    return HttpNotFound();
                }
            }
            return View(city);
        }

        // POST: City/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int cityID)
        {
            City city = db.Cities.Find(cityID);

            if (city == null)
            {
                return HttpNotFound();
            }

            //Check city access if this is not an admin user
            if (!User.IsInRole("Admin"))
            {
                if (city.UserCities.Where(x => x.Email == User.Identity.Name).Count() == 0)
                {
                    return HttpNotFound();
                }
            }

            IEnumerable<Log> logs = db.Logs.Where(x => x.LoginID == city.LoginID);
            db.Logs.RemoveRange(logs);

            CityInfo ci = db.CityInfoes.Find(city.CityID);
            db.CityInfoes.Remove(ci);

            List<SubscriptionItem> subItems = new List<SubscriptionItem>();
            foreach (CityPayItem  payItem in city.CityPayItems)
            {
                subItems.AddRange(payItem.SubscriptionItems);
            }

            db.SubscriptionItems.RemoveRange(subItems);

            var cpi = db.CityPayItems.Where(x => x.CityID == city.CityID);
            db.CityPayItems.RemoveRange(cpi);

            var userCities = db.UserCities.Where(x=> x.CityID == city.CityID);
            db.UserCities.RemoveRange(userCities);

            db.Cities.Remove(city);

            Login login = db.Logins.Find(city.LoginID);
            db.Logins.Remove(login);
            
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UpdateAthenaGift()
        {
            foreach (CityInfo info in db.CityInfoes)
            {
                info.CollectAthenaGift = true;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult ViewImages(int? cityid)
        {
            if (!cityid.HasValue)
            {
                ViewBag.Images = new Dictionary<int, Dictionary<string, string>>();
                return PartialView("_ViewImages");
            }

            City city = db.Cities.Find(cityid);
            if (city == null)
            {
                return HttpNotFound();
            }

            try
            {
                Dictionary<string, string> images = new Dictionary<string, string>();
                if (System.IO.Directory.Exists(Server.MapPath("/Images/" + cityid)))
                {
                    DirectoryInfo di = new DirectoryInfo(Server.MapPath("/Images/" + cityid));
                    FileInfo[] files = di.GetFiles();
                    List<FileInfo> orderedFiles = files.OrderBy(f => f.Length).ToList();

                    foreach (FileInfo file in orderedFiles)
                    {
                        if (file.Length > 10000) continue;

                        if (file.Name.EndsWith("jpg"))
                        {
                            images.Add("/Images/" + cityid + "/" + file.Name, file.LastWriteTime.ToString("G"));
                        }
                    }
                }
                ViewBag.Images = images;
            }
            catch
            {
                ViewBag.Images = new Dictionary<int, Dictionary<string, string>>();
            }

            return PartialView("_ViewImages", city);
        }


        public ActionResult ViewImagesLg(int? cityid)
        {
            if (!cityid.HasValue)
            {
                ViewBag.Images = new Dictionary<int, Dictionary<string, string>>();
                return PartialView("_ViewImages");
            }

            City city = db.Cities.Find(cityid);
            if (city == null)
            {
                return HttpNotFound();
            }

            try
            {
                Dictionary<string, string> images = new Dictionary<string, string>();
                if (System.IO.Directory.Exists(Server.MapPath("/Images/" + cityid)))
                {
                    DirectoryInfo di = new DirectoryInfo(Server.MapPath("/Images/" + cityid));
                    FileInfo[] files = di.GetFiles();
                    List<FileInfo> orderedFiles = files.OrderBy(f => f.Length).ToList();
                    
                    foreach (FileInfo file in orderedFiles)
                    {
                        if (file.Length <= 10000) continue;

                        if (file.Name.EndsWith("jpg"))
                        {
                            images.Add("/Images/" + cityid + "/" + file.Name, file.LastWriteTime.ToString("G"));
                        }
                    }
                }
                ViewBag.Images = images;
            }
            catch
            {
                ViewBag.Images = new Dictionary<int, Dictionary<string, string>>();
            }

            return PartialView("_ViewImages", city);
        }

        [HttpPost]
        public ActionResult ViewImages()
        {
            return RedirectToAction("Index");
        }


        public ActionResult CityPopup(int? cityid)
        {
            if (!cityid.HasValue)
            {
                return HttpNotFound();
            }

            City city = db.Cities.Find(cityid);
            if (city == null)
            {
                return HttpNotFound();
            }

            ViewBag.ResourceTypeID = new SelectList(db.ResourceTypes, "ResourceTypeID", "Type", city.ResourceTypeID);
            return PartialView("_ViewCity", city);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(City city)
        {
            if (city == null)
            {
                return HttpNotFound();
            }
            Login origLogin = db.Logins.Find(city.LoginID);

            if (origLogin == null)
            {
                return HttpNotFound();
            }

            if (city.CityInfo.Shield && city.CityInfo.Rally)
            {
                ModelState.AddModelError(String.Empty, "You can not both rally and shield a city.");
            }

            if (!ModelState.IsValid)
            {
                city.Login.UserName = origLogin.UserName;

                ViewBag.ResourceTypeID = new SelectList(db.ResourceTypes, "ResourceTypeID", "Type", city.ResourceTypeID);
                return View(city);
            }
            CityInfo origCityInfo = db.CityInfoes.Find(city.CityID);
            City origCity = db.Cities.Find(city.CityID);
            

            if (origCityInfo == null)
            {
                return HttpNotFound();
            }
            if (origCity == null)
            {
                return HttpNotFound();
            }

            //Get and Update city Pay items first
            UpdatePayItems(origCity, city);


            //Fill the Login
            if (origLogin.Password != city.Login.Password)
            {
                origLogin.LoginAttempts = 0;
            }
            origLogin.Password = city.Login.Password;
            origLogin.PIN = city.Login.PIN;
            origLogin.Active = city.Login.Active;
            origLogin.LoginDelayMin = city.Login.LoginDelayMin;

            origCity.CityName = city.CityName;
            origCity.ResourceTypeID = city.ResourceTypeID;
            origCity.LocationX = city.LocationX;
            origCity.LocationY = city.LocationY;
            origCity.Kingdom = city.Kingdom;
            origCity.Alliance = city.Alliance.Trim();

            //Fill the City Info
            origCityInfo.StrongHoldLevel = city.CityInfo.StrongHoldLevel;
            origCityInfo.TreasuryLevel = city.CityInfo.TreasuryLevel;
            origCityInfo.HasGoldMine = city.CityInfo.HasGoldMine;

            origCityInfo.Bank = city.CityInfo.Bank;
            origCityInfo.RssMarches = city.CityInfo.RssMarches;
            origCityInfo.RSSBankNum = city.CityInfo.RSSBankNum;
            origCityInfo.SilverBankNum = city.CityInfo.SilverBankNum;
            origCityInfo.SilverMarches = city.CityInfo.SilverMarches;
            origCityInfo.Shield = city.CityInfo.Shield;
            origCityInfo.Upgrade = city.CityInfo.Upgrade;
            origCityInfo.NeedRSS = city.CityInfo.NeedRSS;
            origCityInfo.Rally = city.CityInfo.Rally;
            origCityInfo.RallyX = city.CityInfo.RallyX;
            origCityInfo.RallyY = city.CityInfo.RallyY;
            origCityInfo.TreasuryLevel = city.CityInfo.TreasuryLevel;
            origCityInfo.Treasury = city.CityInfo.Treasury;

            if (ModelState.IsValid)
            {
                db.Entry(origCity).State = EntityState.Modified;
                db.Entry(origCityInfo).State = EntityState.Modified;
                db.Entry(origLogin).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    throw e;
                }

                return RedirectToAction("Index");
            }

            city.Login.UserName = origLogin.UserName;
            ViewBag.ResourceTypeID = new SelectList(db.ResourceTypes, "ResourceTypeID", "Type", city.ResourceTypeID);
            return View(city);
        }

        private void UpdatePayItems(City origCity, City newCity)
        {
            IQueryable<CityPayItem> cpi = db.CityPayItems.Where(x => x.CityID == origCity.CityID);

            //Check LoginDelay
            if (origCity.Login.DelayTier != newCity.Login.DelayTier)
            {
                //Out with the old
                CityPayItem pi;
                if (cpi.Where(x => x.PayItemID == (int)PayItems.Hour6).Count() > 0)
                {
                    pi = cpi.Where(x => x.PayItemID == (int)PayItems.Hour6).First();
                    if (db.SubscriptionItems.Where(x => x.CityPayItemID == pi.CityPayItemID).Count() > 0)
                    {
                        SubscriptionItem subItem = db.SubscriptionItems.Where(x => x.CityPayItemID == pi.CityPayItemID).First();
                        db.SubscriptionItems.Remove(subItem);
                    }
                    origCity.CityPayItems.Remove(pi);
                    db.CityPayItems.Remove(pi);
                }
                if (cpi.Where(x => x.PayItemID == (int)PayItems.Hour3).Count() > 0)
                {
                    pi = cpi.Where(x => x.PayItemID == (int)PayItems.Hour3).First();
                    if (db.SubscriptionItems.Where(x => x.CityPayItemID == pi.CityPayItemID).Count() > 0)
                    {
                        SubscriptionItem subItem = db.SubscriptionItems.Where(x => x.CityPayItemID == pi.CityPayItemID).First();
                        db.SubscriptionItems.Remove(subItem);
                    }
                    origCity.CityPayItems.Remove(pi);
                    db.CityPayItems.Remove(pi);
                }
                if (cpi.Where(x => x.PayItemID == (int)PayItems.Hour1).Count() > 0)
                {
                    pi = cpi.Where(x => x.PayItemID == (int)PayItems.Hour1).First();
                    if (db.SubscriptionItems.Where(x => x.CityPayItemID == pi.CityPayItemID).Count() > 0)
                    {
                        SubscriptionItem subItem = db.SubscriptionItems.Where(x => x.CityPayItemID == pi.CityPayItemID).First();
                        db.SubscriptionItems.Remove(subItem);
                    }
                    origCity.CityPayItems.Remove(pi);
                    db.CityPayItems.Remove(pi);
                }

                //In with the new one
                CityPayItem itemHours = new CityPayItem();
                switch (newCity.Login.LoginDelayMin)
                {
                    case (int)Login.AllowDelays.Min360:
                        itemHours.PayItem = db.PayItems.Find((int)PayItems.Hour6);
                        break;
                    case (int)Login.AllowDelays.Min180:
                        itemHours.PayItem = db.PayItems.Find((int)PayItems.Hour3);
                        break;
                    case (int)Login.AllowDelays.Min60:
                        itemHours.PayItem = db.PayItems.Find((int)PayItems.Hour1);
                        break;
                    default:
                        itemHours.PayItem = db.PayItems.Find((int)PayItems.Hour1);
                        break;
                }
                db.CityPayItems.Add(itemHours);
                origCity.CityPayItems.Add(itemHours);
            }

            //Check Bank
            if (origCity.CityInfo.Bank != newCity.CityInfo.Bank)
            {                
                if (origCity.CityInfo.Bank)
                {
                    //remove it
                    CityPayItem pi = cpi.Where(x => x.PayItemID == (int)PayItems.Bank).First();
                    if (db.SubscriptionItems.Where(x => x.CityPayItemID == pi.CityPayItemID).Count() > 0)
                    {
                        SubscriptionItem subItem = db.SubscriptionItems.Where(x => x.CityPayItemID == pi.CityPayItemID).First();
                        db.SubscriptionItems.Remove(subItem);
                    }
                    origCity.CityPayItems.Remove(pi);
                    db.CityPayItems.Remove(pi);
                }
                else
                {
                    //Add it
                    CityPayItem newPI = new CityPayItem();
                    newPI.PayItem = db.PayItems.Find((int)PayItems.Bank);
                    db.CityPayItems.Add(newPI);
                    origCity.CityPayItems.Add(newPI);
                }
            }

            //Check Shield
            if (origCity.CityInfo.Shield != newCity.CityInfo.Shield)
            {
                if (origCity.CityInfo.Shield)
                {
                    //remove it
                    CityPayItem pi = cpi.Where(x => x.PayItemID == (int)PayItems.Shield).First();
                    if (db.SubscriptionItems.Where(x => x.CityPayItemID == pi.CityPayItemID).Count() > 0)
                    {
                        SubscriptionItem subItem = db.SubscriptionItems.Where(x => x.CityPayItemID == pi.CityPayItemID).First();
                        db.SubscriptionItems.Remove(subItem);
                    }
                    origCity.CityPayItems.Remove(pi);
                    db.CityPayItems.Remove(pi);
                }
                else
                {
                    //Add it
                    CityPayItem newPI = new CityPayItem();
                    newPI.PayItem = db.PayItems.Find((int)PayItems.Shield);
                    db.CityPayItems.Add(newPI);
                    origCity.CityPayItems.Add(newPI);
                }
            }

            //Check Rally
            if (origCity.CityInfo.Rally != newCity.CityInfo.Rally)
            {
                if (origCity.CityInfo.Rally)
                {
                    //remove it
                    CityPayItem pi = cpi.Where(x => x.PayItemID == (int)PayItems.Rally).First();
                    if (db.SubscriptionItems.Where(x => x.CityPayItemID == pi.CityPayItemID).Count() > 0)
                    {
                        SubscriptionItem subItem = db.SubscriptionItems.Where(x => x.CityPayItemID == pi.CityPayItemID).First();
                        db.SubscriptionItems.Remove(subItem);
                    }
                    origCity.CityPayItems.Remove(pi);
                    db.CityPayItems.Remove(pi);
                }
                else
                {
                    //Add it
                    CityPayItem newPI = new CityPayItem();
                    newPI.PayItem = db.PayItems.Find((int)PayItems.Rally);
                    db.CityPayItems.Add(newPI);
                    origCity.CityPayItems.Add(newPI);
                }
            }

            //Check Upgrade
            if (origCity.CityInfo.Upgrade != newCity.CityInfo.Upgrade)
            {
                if (origCity.CityInfo.Upgrade)
                {
                    //remove it
                    CityPayItem pi = cpi.Where(x => x.PayItemID == (int)PayItems.Upgrade).First();
                    if (db.SubscriptionItems.Where(x => x.CityPayItemID == pi.CityPayItemID).Count() > 0)
                    {
                        SubscriptionItem subItem = db.SubscriptionItems.Where(x => x.CityPayItemID == pi.CityPayItemID).First();
                        db.SubscriptionItems.Remove(subItem);
                    }
                    origCity.CityPayItems.Remove(pi);
                    db.CityPayItems.Remove(pi);
                }
                else
                {
                    //Add it
                    CityPayItem newPI = new CityPayItem();
                    newPI.PayItem = db.PayItems.Find((int)PayItems.Upgrade);
                    db.CityPayItems.Add(newPI);
                    origCity.CityPayItems.Add(newPI);
                }
            }

            //Check Treasury
            if (origCity.CityInfo.Treasury != newCity.CityInfo.Treasury)
            {
                if (origCity.CityInfo.Treasury)
                {
                    //remove it
                    CityPayItem pi = cpi.Where(x => x.PayItemID == (int)PayItems.Treasury).First();
                    if (db.SubscriptionItems.Where(x => x.CityPayItemID == pi.CityPayItemID).Count() > 0)
                    {
                        SubscriptionItem subItem = db.SubscriptionItems.Where(x => x.CityPayItemID == pi.CityPayItemID).First();
                        db.SubscriptionItems.Remove(subItem);
                    }
                    origCity.CityPayItems.Remove(pi);
                    db.CityPayItems.Remove(pi);
                }
                else
                {
                    //Add it
                    CityPayItem newPI = new CityPayItem();
                    newPI.PayItem = db.PayItems.Find((int)PayItems.Treasury);
                    db.CityPayItems.Add(newPI);
                    origCity.CityPayItems.Add(newPI);
                }
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
