using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GowWebSite.Models;

namespace GowWebSite.Controllers
{
    public class CityController : Controller
    {
        private GowEntities db = new GowEntities();

        // GET: City
        public ActionResult Index()
        {
            ViewBag.Alliances = db.Alliances.ToList();
            var orderedCities = db.Cities.OrderBy(x => x.CityName);
            if (Request.Cookies["SelectedAlliance"] != null)
            {
                int allianceID = Int32.Parse(Request.Cookies["SelectedAlliance"].Value);
                orderedCities = orderedCities.Where(x => x.AllianceID == allianceID).OrderBy(x => x.CityName);
                ViewBag.AllianceID = new SelectList(db.Alliances, "AllianceID", "Name", allianceID );
            }
            else
            {
                ViewBag.AllianceID = new SelectList(db.Alliances, "AllianceID", "Name");
            }

            var cities = orderedCities.Include(c => c.Alliance).Include(c => c.Login).Include(c => c.ResourceType).Include(c => c.CityInfo);
            return View(cities.ToList());
        }

        [HttpPost]
        public ActionResult Index(int? allianceID)
        {
            IQueryable<City> cities = null;
            if (allianceID.HasValue)
            {
                cities = db.Cities.Where(x => x.AllianceID == allianceID).OrderBy(x => x.CityName);
                Response.Cookies["SelectedAlliance"].Value = allianceID.Value.ToString();
                Response.Cookies["SelectedAlliance"].Expires = DateTime.Now.AddYears(1);

                ViewBag.AllianceID = new SelectList(db.Alliances, "AllianceID", "Name", allianceID);
            }
            else
            {
                cities = db.Cities.OrderBy(x => x.CityName);
                ViewBag.AllianceID = new SelectList(db.Alliances, "AllianceID", "Name");
            }
            
            var cities2 = cities.Include(c => c.Alliance).Include(c => c.Login).Include(c => c.ResourceType).Include(c => c.CityInfo);


            return View(cities2.ToList());
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
            ViewBag.AllianceID = new SelectList(db.Alliances, "AllianceID", "Name");
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
                db.CreateExistingCitySetup(city.UserName, city.Password, city.CityName, city.CityX, city.CityY, city.AllianceID, city.ResourceTypeID, city.SHLevel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AllianceID = new SelectList(db.Alliances, "AllianceID", "Name", city.AllianceID);
            ViewBag.ResourceTypeID = new SelectList(db.ResourceTypes, "ResourceTypeID", "Type", city.ResourceTypeID);
            return View(city);
        }


        // GET: City/CreateFull
        public ActionResult CreateFull()
        {
            ViewBag.AllianceID = new SelectList(db.Alliances, "AllianceID", "Name");
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

            if (db.Cities.Count(x=>x.AllianceID==city.AllianceID) >= 98)
            {
                ModelState.AddModelError("AllianceID", "There are already 98 cities in that alliance.  You can not add more.");
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
            if (city.Rally && db.CityInfoes.Where(x => x.City.AllianceID == city.AllianceID && x.RallyX == city.RallyX && x.RallyY == city.RallyY).Count() > 0)
            {
                ModelState.AddModelError("Rally", "A city in your alliance already has that rally target.");
            }
            
            if (ModelState.IsValid)
            {
                db.CreateExistingCitySetupFull(city.UserName, city.Password,
                        city.CityName, city.PIN,city.CityX, city.CityY, city.AllianceID, city.ResourceTypeID, 
                        city.SHLevel, city.RSSBank, city.SilverBank, city.RSSMarches, city.SilverMarches, 
                        city.Upgrade, city.LoginDelayMin, city.Shield, city.LastShieldDate, city.Bank, 
                        city.Rally, city.RallyX, city.RallyY);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AllianceID = new SelectList(db.Alliances, "AllianceID", "Name", city.AllianceID);
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
            ViewBag.AllianceID = new SelectList(db.Alliances, "AllianceID", "Name", city.AllianceID);
            ViewBag.LoginID = new SelectList(db.Logins, "LoginID", "UserName", city.LoginID);
            ViewBag.ResourceTypeID = new SelectList(db.ResourceTypes, "ResourceTypeID", "Type", city.ResourceTypeID);
            ViewBag.CityID = new SelectList(db.CityInfoes, "CityID", "RedeemCode", city.CityID);
            return View(city);
        }

        // POST: City/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(City city)
        {
            if (ModelState.IsValid)
            {
                db.Entry(city).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AllianceID = new SelectList(db.Alliances, "AllianceID", "Name", city.AllianceID);
            ViewBag.LoginID = new SelectList(db.Logins, "LoginID", "UserName", city.LoginID);
            ViewBag.ResourceTypeID = new SelectList(db.ResourceTypes, "ResourceTypeID", "Type", city.ResourceTypeID);
            ViewBag.CityID = new SelectList(db.CityInfoes, "CityID", "RedeemCode", city.CityID);
            return View(city);
        }

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
                info.CollectAthenaGift=true;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
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
