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
    public class LoginController : Controller
    {
        private GowEntities db = new GowEntities();

        // GET: Login
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            int? a =  db.GetAdminNonShield().FirstOrDefault();
            ViewBag.AdminState = (a.Value > 0);
            ViewBag.BetaState = ((db.GetBetaNonShield().FirstOrDefault().Value) > 0);
            return View(db.Logins.OrderByDescending(x => x.InProcess).ThenBy(x => System.Data.Entity.DbFunctions.AddMinutes(x.LastRun,(x.LoginDelayMin))).ToList());
        }

        // GET: Login/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = db.Logins.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        [Authorize]
        public ActionResult ViewLogin(int? cityID)
        {
            if (cityID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cities.Find(cityID);

            if (city == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Login login = db.Logins.Find(city.LoginID);
            if (login == null)
            {
                return HttpNotFound();
            }

            ViewBag.CityName = city.CityName;

            return PartialView("_ViewCityLogin",login);
        }


        // GET: Login/Details/5
        [Authorize]
        public ActionResult ReRunLogin(int? loginID)
        {
            if (loginID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = db.Logins.Find(loginID);
            if (login == null)
            {
                return HttpNotFound();
            }

            login.LastRun = login.LastRun.AddMinutes(-1 * login.LoginDelayMin);
            db.SaveChanges();

            return RedirectToAction("Index", "City");
        }

        [Authorize]
        public ActionResult ToggleActive(int? loginID)
        {
            if (loginID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = db.Logins.Find(loginID);
            if (login == null)
            {
                return HttpNotFound();
            }

            login.Active = !login.Active;
            db.SaveChanges();

            return RedirectToAction("Index", "City");
        }
        // GET: Login/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "LoginID,UserName,Password,Active,LastRun,InProcess,PIN")] Login login)
        {
            if (ModelState.IsValid)
            {
                //db.Logins.Add(login);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(login);
        }

        // GET: Login/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = db.Logins.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // POST: Login/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "LoginID,UserName,Password,Active,LastRun,InProcess,LoginDelayMin,PIN")] Login login)
        {
            if (ModelState.IsValid)
            {
                Login dbLogin = db.Logins.Find(login.LoginID);
                dbLogin.Active = login.Active;
                dbLogin.InProcess = login.InProcess;
                dbLogin.LastRun = login.LastRun;
                dbLogin.LoginDelayMin = login.LoginDelayMin;
                dbLogin.PIN = login.PIN;
                dbLogin.LoginAttempts = login.LoginAttempts;

                //db.Entry(login).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(login);
        }

        // GET: Login/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = db.Logins.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // POST: Login/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            //Login login = db.Logins.Find(id);
            //db.Logins.Remove(login);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult ToggleAdmin(bool newState)
        {
            //Piece p = db.Pieces.Where(m => m.PieceName == PieceName && m.PieceLevel == PieceLevel).FirstOrDefault();
            db.UpdateAdminNonShield(newState); 
            return Json(newState, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult ToggleBeta(bool newState)
        {
            //Piece p = db.Pieces.Where(m => m.PieceName == PieceName && m.PieceLevel == PieceLevel).FirstOrDefault();
            db.UpdateBetaNonShield(newState);
            return Json(newState, JsonRequestBehavior.AllowGet);
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
