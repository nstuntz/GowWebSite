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
    [Authorize(Roles = "Admin,MachineOwner")]
    public class LogController : Controller
    {
        private GowEntities db = new GowEntities();
        
        // GET: Log
        public ActionResult Index(string machineID)
        {
            ViewBag.Machines = db.Logs.Select(x => x.MachineID).Distinct().OrderBy(x => x).ToList();
            var logs = db.Logs.Include(l => l.Login);
            if (!string.IsNullOrWhiteSpace(machineID))
            {
                return View(logs.Where(x => x.MachineID == machineID).OrderByDescending(x => x.LogDate).Take(100).ToList());
            }
            else
            {
                return View(logs.OrderByDescending(x => x.LogDate).Take(100).ToList());
            }
        }

        // GET: Log/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Log log = db.Logs.Find(id);
            if (log == null)
            {
                return HttpNotFound();
            }
            return View(log);
        }



        // GET: Log/Details/5
        public ActionResult CityLogs(int? loginID)
        {
            if (loginID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            City city = db.Cities.FirstOrDefault(x => x.LoginID == loginID);

            if (city == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IQueryable<Log> logs = db.Logs.Where(x => x.LoginID == loginID);

            if (logs == null || logs.Count() == 0)
            {
                return HttpNotFound();
            }

            ViewBag.CityName = city.CityName;

            return PartialView("_ViewLogs", logs.Take(50).ToList());
        }

        // GET: Log/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Log/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LogID,MachineID,Severity,LoginID,Message,LogDate")] Log log)
        {
            if (ModelState.IsValid)
            {
                db.Logs.Add(log);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(log);
        }

        // GET: Log/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Log log = db.Logs.Find(id);
            if (log == null)
            {
                return HttpNotFound();
            }
            return View(log);
        }

        // POST: Log/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LogID,MachineID,Severity,LoginID,Message,LogDate")] Log log)
        {
            if (ModelState.IsValid)
            {
                db.Entry(log).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(log);
        }

        // GET: Log/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Log log = db.Logs.Find(id);
            if (log == null)
            {
                return HttpNotFound();
            }
            return View(log);
        }

        // POST: Log/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Log log = db.Logs.Find(id);
            db.Logs.Remove(log);
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
