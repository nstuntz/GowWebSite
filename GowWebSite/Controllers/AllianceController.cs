using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GowWebSite.Models;

namespace GowWebSite.Views
{
    [Authorize(Roles = "Admin")]
    public class AllianceController : Controller
    {
        private GowEntities db = new GowEntities();

        // GET: Alliance
        public ActionResult Index()
        {
            return View(db.Alliances.ToList());
        }

        // GET: Alliance/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alliance alliance = db.Alliances.Find(id);
            if (alliance == null)
            {
                return HttpNotFound();
            }
            return View(alliance);
        }

        // GET: Alliance/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Alliance/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AllianceID,Kingdom,CityX,CityY,Name")] Alliance alliance)
        {
            if (ModelState.IsValid)
            {
                db.Alliances.Add(alliance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(alliance);
        }

        // GET: Alliance/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alliance alliance = db.Alliances.Find(id);
            if (alliance == null)
            {
                return HttpNotFound();
            }
            return View(alliance);
        }

        // POST: Alliance/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AllianceID,Kingdom,CityX,CityY,Name")] Alliance alliance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alliance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(alliance);
        }

        // GET: Alliance/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alliance alliance = db.Alliances.Find(id);
            if (alliance == null)
            {
                return HttpNotFound();
            }
            return View(alliance);
        }

        // POST: Alliance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Alliance alliance = db.Alliances.Find(id);
            db.Alliances.Remove(alliance);
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
