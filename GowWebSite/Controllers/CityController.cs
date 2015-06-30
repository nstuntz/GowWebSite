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
            return View(new CreateCityFullModel());
        }

        // POST: City/CreateFull
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFull(CreateCityFullModel city)
        {
            if (!city.LastShieldDate.HasValue)
            {
                city.LastShieldDate = new DateTime(2004, 1, 1, 1, 1, 1);
            }

            if (city.SHLevel == 21 && city.LoginDelayMin < 120)
            {
                city.LoginDelayMin = 120;
            }
            if (city.SHLevel >= 14 && city.LoginDelayMin < 60)
            {
                city.LoginDelayMin = 60;
            }

            //Check the rally target
            //if (city.Rally && db.CityInfoes.Where(x => x.City.AllianceID == city.AllianceID && x.RallyX == city.RallyX && x.RallyY == city.RallyY).Count() > 0)
            //{
            //    ModelState.AddModelError("Rally", "A city in your alliance already has that rally target.");
            //}

            if (ModelState.IsValid)
            {
                db.CreateExistingCitySetupFull(city.UserName, city.Password,
                        city.CityName, city.PIN, city.CityX, city.CityY, city.Alliance, city.ResourceTypeID,
                        city.SHLevel, city.RSSBank, city.SilverBank, city.RSSMarches, city.SilverMarches,
                        false, city.LoginDelayMin, city.Shield, city.LastShieldDate, city.Bank,
                        city.Rally, city.RallyX, city.RallyY, city.HasGoldMine, User.Identity.Name);

                db.SaveChanges();
                return RedirectToAction("Index");
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
            ViewBag.LoginID = new SelectList(db.Logins, "LoginID", "UserName", city.LoginID);
            ViewBag.ResourceTypeID = new SelectList(db.ResourceTypes, "ResourceTypeID", "Type", city.ResourceTypeID);
            ViewBag.CityID = new SelectList(db.CityInfoes, "CityID", "RedeemCode", city.CityID);
            return View(city);
        }

        // POST: City/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(City city)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(city).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.LoginID = new SelectList(db.Logins, "LoginID", "UserName", city.LoginID);
        //    ViewBag.ResourceTypeID = new SelectList(db.ResourceTypes, "ResourceTypeID", "Type", city.ResourceTypeID);
        //    ViewBag.CityID = new SelectList(db.CityInfoes, "CityID", "RedeemCode", city.CityID);
        //    return View(city);
        //}

        // GET: City/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: City/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //City city = db.Cities.Find(id);
            //db.Cities.Remove(city);
            //db.SaveChanges();
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
            //Fill the Login
            origLogin.Password = city.Login.Password;
            origLogin.PIN = city.Login.PIN;
            origLogin.LoginDelayMin = city.Login.LoginDelayMin;

            origCity.CityName = city.CityName;
            origCity.ResourceTypeID = city.ResourceTypeID;
            origCity.LocationX = city.LocationX;
            origCity.LocationY = city.LocationY;
            origCity.Kingdom = city.Kingdom;
            origCity.Alliance = city.Alliance;

            //Fill the City Info
            origCityInfo.StrongHoldLevel = city.CityInfo.StrongHoldLevel;
            origCityInfo.TreasuryLevel = city.CityInfo.TreasuryLevel;
            origCityInfo.HasGoldMine = city.CityInfo.HasGoldMine;

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
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            city.Login.UserName = origLogin.UserName;
            ViewBag.ResourceTypeID = new SelectList(db.ResourceTypes, "ResourceTypeID", "Type", city.ResourceTypeID);
            return View(city);
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
