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

            AddAllowedUsedCitiesToViewBag();

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
        public ActionResult CreateFull(bool premium = false, bool basic = false)
        {
            ViewBag.ResourceTypeID = new SelectList(db.ResourceTypes, "ResourceTypeID", "Type");
            City newCity = new City();
            newCity.Login = new Login();
            newCity.Login.DelayTier = Login.AllowDelays.Min360;
            newCity.PremiumCity = premium;
            newCity.BasicCity = basic;
            newCity.Login.Active = true;
            newCity.Login.DelayTier = Login.AllowDelays.Min180;
            newCity.CityInfo = new CityInfo();
            newCity.CityInfo.RSSBankNum = 1;
            newCity.CityInfo.SilverBankNum = 1;

            ViewBag.PremiumCity = premium;
            ViewBag.BasicCity = basic;

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
                ModelState.AddModelError(String.Empty, "UserName is required.");
            }
            
            if (db.Logins.Where(x => x.UserName == city.Login.UserName).Count() > 0)
            {
                ModelState.AddModelError(String.Empty, "UserName is already in use.  You must use a different one.");
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
                bool isBasicCity = city.BasicCity && AllowBasicCityCreate();
                bool isPremiumCity = city.PremiumCity && AllowPremiumCityCreate();

                //Setup the Login defaults
                city.Login.InProcess = "0";
                city.Login.LastRun = DateTime.Now;
                city.Login.CreateDate = DateTime.Now;
                city.Login.Active = true;

                if (isBasicCity || isPremiumCity)
                {
                    city.Login.PaidThrough = DateTime.Today.AddDays(DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month) - DateTime.Today.Day + 1);
                }
                else
                {
                    city.Login.PaidThrough = DateTime.Today.AddMonths(-1);
                }
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


                if (!(isBasicCity || isPremiumCity))
                {
                    //Now add the costs
                    CityPayItem itemHours = new CityPayItem();
                    switch (city.Login.LoginDelayMin)
                    {
                        case (int)Login.AllowDelays.Min360:
                            itemHours.PayItem = db.PayItems.Find((int)PayItemEnum.Hour6);
                            break;
                        case (int)Login.AllowDelays.Min180:
                            itemHours.PayItem = db.PayItems.Find((int)PayItemEnum.Hour3);
                            break;
                        case (int)Login.AllowDelays.Min60:
                            itemHours.PayItem = db.PayItems.Find((int)PayItemEnum.Hour1);
                            break;
                        default:
                            itemHours.PayItem = db.PayItems.Find((int)PayItemEnum.Hour1);
                            break;
                    }
                    db.CityPayItems.Add(itemHours);
                    city.CityPayItems.Add(itemHours);
                }
                if (!city.PremiumCity)
                {
                    if (city.CityInfo.Upgrade)
                    {
                        CityPayItem itemUpgrade = new CityPayItem();
                        itemUpgrade.PayItem = db.PayItems.Find((int)PayItemEnum.Upgrade);
                        db.CityPayItems.Add(itemUpgrade);
                        city.CityPayItems.Add(itemUpgrade);
                    }

                    if (city.CityInfo.Treasury)
                    {
                        CityPayItem itemTreasury = new CityPayItem();
                        itemTreasury.PayItem = db.PayItems.Find((int)PayItemEnum.Treasury);
                        db.CityPayItems.Add(itemTreasury);
                        city.CityPayItems.Add(itemTreasury);
                    }
                }

                if (city.BasicCity)
                {
                    CityPayItem item = new CityPayItem();
                    item.PayItem = db.PayItems.Find((int)PayItemEnum.BasicCity); 
                    item.Paid = true;
                    db.CityPayItems.Add(item);
                    city.CityPayItems.Add(item);
                }

                if (city.PremiumCity)
                {
                    CityPayItem item = new CityPayItem();
                    item.PayItem = db.PayItems.Find((int)PayItemEnum.PremiumCity);
                    item.Paid = true;
                    db.CityPayItems.Add(item);
                    city.CityPayItems.Add(item);
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
            
            ViewBag.PremiumCity = city.PremiumCity;
            ViewBag.BasicCity = city.BasicCity;

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

            ViewBag.PremiumCity = city.CityPayItems.Where(x => x.PayItemID == (int)PayItemEnum.PremiumCity).Count() > 0;
            ViewBag.BasicCity = city.CityPayItems.Where(x => x.PayItemID == (int)PayItemEnum.BasicCity).Count() > 0;
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

            bool premiumCity = origCity.CityPayItems.Where(x => x.PayItemID == (int)PayItemEnum.PremiumCity).Count() > 0;
            bool basicCity = origCity.CityPayItems.Where(x => x.PayItemID == (int)PayItemEnum.BasicCity).Count() > 0;

            if (!premiumCity && !basicCity)
            {
                //Check LoginDelay
                if (origCity.Login.DelayTier != newCity.Login.DelayTier)
                {
                    //Out with the old
                    CityPayItem pi;
                    if (cpi.Where(x => x.PayItemID == (int)PayItemEnum.Hour6).Count() > 0)
                    {
                        pi = cpi.Where(x => x.PayItemID == (int)PayItemEnum.Hour6).First();
                        origCity.CityPayItems.Remove(pi);
                        db.CityPayItems.Remove(pi);
                    }
                    if (cpi.Where(x => x.PayItemID == (int)PayItemEnum.Hour3).Count() > 0)
                    {
                        pi = cpi.Where(x => x.PayItemID == (int)PayItemEnum.Hour3).First();
                        origCity.CityPayItems.Remove(pi);
                        db.CityPayItems.Remove(pi);
                    }
                    if (cpi.Where(x => x.PayItemID == (int)PayItemEnum.Hour1).Count() > 0)
                    {
                        pi = cpi.Where(x => x.PayItemID == (int)PayItemEnum.Hour1).First();
                        origCity.CityPayItems.Remove(pi);
                        db.CityPayItems.Remove(pi);
                    }

                    //In with the new one
                    CityPayItem itemHours = new CityPayItem();
                    switch (newCity.Login.LoginDelayMin)
                    {
                        case (int)Login.AllowDelays.Min360:
                            itemHours.PayItem = db.PayItems.Find((int)PayItemEnum.Hour6);
                            break;
                        case (int)Login.AllowDelays.Min180:
                            itemHours.PayItem = db.PayItems.Find((int)PayItemEnum.Hour3);
                            break;
                        case (int)Login.AllowDelays.Min60:
                            itemHours.PayItem = db.PayItems.Find((int)PayItemEnum.Hour1);
                            break;
                        default:
                            itemHours.PayItem = db.PayItems.Find((int)PayItemEnum.Hour1);
                            break;
                    }
                    db.CityPayItems.Add(itemHours);
                    origCity.CityPayItems.Add(itemHours);
                }
            }

            if (!premiumCity)
            {
                //Check Upgrade
                if (origCity.CityInfo.Upgrade != newCity.CityInfo.Upgrade)
                {
                    if (origCity.CityInfo.Upgrade)
                    {
                        //remove it
                        CityPayItem pi = cpi.Where(x => x.PayItemID == (int)PayItemEnum.Upgrade).First();
                        origCity.CityPayItems.Remove(pi);
                        db.CityPayItems.Remove(pi);
                    }
                    else
                    {
                        //Add it
                        CityPayItem newPI = new CityPayItem();
                        newPI.PayItem = db.PayItems.Find((int)PayItemEnum.Upgrade);
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
                        CityPayItem pi = cpi.Where(x => x.PayItemID == (int)PayItemEnum.Treasury).First();
                        origCity.CityPayItems.Remove(pi);
                        db.CityPayItems.Remove(pi);
                    }
                    else
                    {
                        //Add it
                        CityPayItem newPI = new CityPayItem();
                        newPI.PayItem = db.PayItems.Find((int)PayItemEnum.Treasury);
                        db.CityPayItems.Add(newPI);
                        origCity.CityPayItems.Add(newPI);
                    }
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

        private bool AllowBasicCityCreate()
        {
            var userPayBasicCities = db.UserPayItems.Where(x => x.Email == User.Identity.Name && x.PayItem.ItemType == (int)PayItemTypeEnum.BasicCityPackage);
            if (userPayBasicCities.Count() == 0)
            {
                return false;
            }

            int used = db.UserCities.Where(x => x.Email == User.Identity.Name).Count(x => x.City.CityPayItems.Where(z => z.PayItem.ItemType == (int)PayItemTypeEnum.BasicCity).Count() > 0);
            int allowed = userPayBasicCities.Sum(x => x.PayItem.Number);

            return used < allowed;
        }

        private bool AllowPremiumCityCreate()
        {

            var userPayBasicCities = db.UserPayItems.Where(x => x.Email == User.Identity.Name && x.PayItem.ItemType == (int)PayItemTypeEnum.PremiumCityPackage);
            if (userPayBasicCities.Count() == 0)
            {
                return false;
            }

            int used = db.UserCities.Where(x => x.Email == User.Identity.Name).Count(x => x.City.CityPayItems.Where(z => z.PayItem.ItemType == (int)PayItemTypeEnum.PremiumCity).Count() > 0);
            int allowed = userPayBasicCities.Sum(x => x.PayItem.Number);

            return used < allowed;
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
