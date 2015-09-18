using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WisconsinTrackClubWebsite.Models;

namespace WisconsinTrackClubWebsite.Controllers
{
    public class UserRunEntriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserRunEntries
        public ActionResult Index()
        {
            return View(db.UserRunEntry.ToList());
        }

        // GET: UserRunEntries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRunEntry userRunEntry = db.UserRunEntry.Find(id);
            if (userRunEntry == null)
            {
                return HttpNotFound();
            }
            return View(userRunEntry);
        }

        // GET: UserRunEntries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserRunEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserRunEntryId")] UserRunEntry userRunEntry)
        {
            if (ModelState.IsValid)
            {
                db.UserRunEntry.Add(userRunEntry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userRunEntry);
        }

        // GET: UserRunEntries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRunEntry userRunEntry = db.UserRunEntry.Find(id);
            if (userRunEntry == null)
            {
                return HttpNotFound();
            }
            return View(userRunEntry);
        }

        // POST: UserRunEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserRunEntryId")] UserRunEntry userRunEntry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userRunEntry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userRunEntry);
        }

        // GET: UserRunEntries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRunEntry userRunEntry = db.UserRunEntry.Find(id);
            if (userRunEntry == null)
            {
                return HttpNotFound();
            }
            return View(userRunEntry);
        }

        // POST: UserRunEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserRunEntry userRunEntry = db.UserRunEntry.Find(id);
            db.UserRunEntry.Remove(userRunEntry);
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
