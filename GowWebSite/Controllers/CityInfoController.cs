﻿using System;
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
    public class CityInfoController : Controller
    {
        private GowEntities db = new GowEntities();

        // GET: CityInfo
        public ActionResult Index()
        {
            var cityInfoes = db.CityInfoes.Include(c => c.City);
            return View(cityInfoes.ToList());
        }

        // GET: CityInfo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CityInfo cityInfo = db.CityInfoes.Find(id);
            if (cityInfo == null)
            {
                return HttpNotFound();
            }            
            var cityLogs = db.Logs.Where(l => l.LoginID == cityInfo.City.LoginID);
            cityLogs = cityLogs.OrderByDescending(l => l.LogDate);
            ViewBag.Logs = cityLogs.Take(50).ToList();
            return View(cityInfo);
        }

        // GET: CityInfo/Create
        public ActionResult Create()
        {
            ViewBag.CityID = new SelectList(db.Cities, "CityID", "CityName");
            return View();
        }

        // POST: CityInfo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CityInfo cityInfo)
        {
            if (ModelState.IsValid)
            {
                db.CityInfoes.Add(cityInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CityID = new SelectList(db.Cities, "CityID", "CityName", cityInfo.CityID);
            return View(cityInfo);
        }

        // GET: CityInfo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CityInfo cityInfo = db.CityInfoes.Find(id);
            if (cityInfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityID = new SelectList(db.Cities, "CityID", "CityName", cityInfo.CityID);
            return View(cityInfo);
        }

        // POST: CityInfo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CityInfo cityInfo)
        {
            if (ModelState.IsValid)
            {
                if (cityInfo.RedeemCode == null)
                {
                    cityInfo.RedeemCode = string.Empty;
                }

                db.Entry(cityInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityID = new SelectList(db.Cities, "CityID", "CityName", cityInfo.CityID);
            return View(cityInfo);
        }

        // GET: CityInfo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CityInfo cityInfo = db.CityInfoes.Find(id);
            if (cityInfo == null)
            {
                return HttpNotFound();
            }
            return View(cityInfo);
        }

        // POST: CityInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //CityInfo cityInfo = db.CityInfoes.Find(id);
            //db.CityInfoes.Remove(cityInfo);
            //db.SaveChanges();
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
