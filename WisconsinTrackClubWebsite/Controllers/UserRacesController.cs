using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WisconsinTrackClubWebsite.Models;
using WisconsinTrackClubWebsite.Models.ViewModels;
using Microsoft.AspNet.Identity;


namespace WisconsinTrackClubWebsite.Controllers
{
    public class UserRacesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserRaces
        public ActionResult Index()
        {
            var Races = (from ra in db.Races
             where ra != null
             orderby ra.Date ascending
             select ra).ToList();
            return View(Races);
        }

        public ActionResult UserIndex()
        {
            var UserRaces = (from ra in db.UserRaces
             where ra.Race != null
             orderby ra.Race.Date ascending
             select ra).ToList();
            return View(UserRaces);
        }

        [HttpGet]
        public ActionResult EditRace(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRace race = db.UserRaces.Find(id);

            if (race == null)
            {
                return HttpNotFound();
            }
            return View("Edit", race);
        }

        [HttpPost]
        public ActionResult EditRace([Bind(Include = "UserRaceId, HighLights")] UserRace race)
        {
            if (ModelState.IsValid)
            {
                var raceToEdit = db.UserRaces.Find(race.UserRaceId);
                raceToEdit.HighLights = race.HighLights;
                db.SaveChanges();
                return RedirectToAction("MyRaces", "Home");
            }

            return View("Edit", race);
        }


        // GET: UserRaces/Edit/5
        public ActionResult Add(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Race race = db.Races.Find(id);
            string userId = this.User.Identity.GetUserId();
            var userRaces = db.Users.Find(userId).Races;

            UserRace userRace = new UserRace()
            {
                Race = race                
            };


            userRaces.Add(userRace);

            db.SaveChanges();
            var contain = db.UserRaces.Where(c => c.Race.RaceId == race.RaceId && c.ApplicationUser.Profile.Id == userId).ToList();
            if (contain.Count == 1)
            {
                userRace = contain[0];
            }
            else if (contain.Count > 1)
            {
                return RedirectToAction("MyRaces", "Home");
            }
            else
            {
                return HttpNotFound();
            }

            if (race == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("EditRace", new { id = userRace.UserRaceId });
        }

        // POST: UserRaces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "RaceId,RaceName,Location,Date")] Race race)
        {
            if (ModelState.IsValid)
            {
                
                db.SaveChanges();
            }
            return View(race);
        }

        // GET: UserRaces/Delete/5
        public ActionResult Remove(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRace userRace = db.UserRaces.Find(id);

            if (userRace == null)
            {
                return HttpNotFound();
            }

            RemoveUserRaceViewModel vm = new RemoveUserRaceViewModel();
            vm.UserRaceId = userRace.UserRaceId;

            Race race = db.Races.Find(userRace.Race.RaceId);
            vm.Race = race;
            return View(vm);
        }

        // POST: UserRaces/Delete/5
        [HttpPost, ActionName("Remove")]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveConfirmed(int id)
        {
            //Race race = db.Races.Find(id);
            UserRace race = db.UserRaces.Find(id);
            db.UserRaces.Remove(race);
            db.SaveChanges();
            return RedirectToAction("MyRaces", "Home");
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
