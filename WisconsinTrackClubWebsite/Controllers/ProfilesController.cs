using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WisconsinTrackClubWebsite.Models;
using Microsoft.AspNet.Identity;

namespace WisconsinTrackClubWebsite.Controllers
{
    public class ProfilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Profiles
        public ActionResult Index()
        {
            var profiles = db.Profiles.Include(p => p.ApplicationUser);
            return View(profiles.ToList());
        }

        // GET: Profiles/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // GET: Profiles/Create
        public ActionResult Create()
        {
            List<KeyValuePair<string, string>> yearList = new List<KeyValuePair<string, string>>();
            yearList.Add(new KeyValuePair<string, string>("Freshman", "Freshmen"));
            yearList.Add(new KeyValuePair<string, string>("Sophomore", "Sophomore"));
            yearList.Add(new KeyValuePair<string, string>("Junior", "Junior"));
            yearList.Add(new KeyValuePair<string, string>("Senior", "Senior"));
            yearList.Add(new KeyValuePair<string, string>("Graduate", "Graduate"));
            yearList.Add(new KeyValuePair<string, string>("Other", "Other"));
            List<KeyValuePair<string, string>> sexList = new List<KeyValuePair<string, string>>();
            sexList.Add(new KeyValuePair<string, string>("Male", "Male"));
            sexList.Add(new KeyValuePair<string, string>("Female", "Female"));
            sexList.Add(new KeyValuePair<string, string>("Other", "Other"));
            List<KeyValuePair<string, string>> stateList = new List<KeyValuePair<string, string>>();
            stateList.Add(new KeyValuePair<string, string>("Alabama", "Alabama"));
            stateList.Add(new KeyValuePair<string, string>("Alaska", "Alaska"));
            stateList.Add(new KeyValuePair<string, string>("Arizona", "Arizona"));
            stateList.Add(new KeyValuePair<string, string>("Arkansas", "Arkansas"));
            stateList.Add(new KeyValuePair<string, string>("California", "California"));
            stateList.Add(new KeyValuePair<string, string>("Colorado", "Colorado"));
            stateList.Add(new KeyValuePair<string, string>("Connecticut", "Connecticut"));
            stateList.Add(new KeyValuePair<string, string>("Delaware", "Delaware"));
            stateList.Add(new KeyValuePair<string, string>("Florida", "Florida"));
            stateList.Add(new KeyValuePair<string, string>("Georgia", "Georgia"));
            stateList.Add(new KeyValuePair<string, string>("Hawaii", "Hawaii"));
            stateList.Add(new KeyValuePair<string, string>("Idaho", "Idaho"));
            stateList.Add(new KeyValuePair<string, string>("Illinois", "Illinois"));
            stateList.Add(new KeyValuePair<string, string>("Indiana", "Indiana"));
            stateList.Add(new KeyValuePair<string, string>("Iowa", "Iowa"));
            stateList.Add(new KeyValuePair<string, string>("Kansas", "Kansas"));
            stateList.Add(new KeyValuePair<string, string>("Kentucky", "Kentucky"));
            stateList.Add(new KeyValuePair<string, string>("Louisiana", "Louisiana"));
            stateList.Add(new KeyValuePair<string, string>("Maine", "Maine"));
            stateList.Add(new KeyValuePair<string, string>("Maryland", "Maryland"));
            stateList.Add(new KeyValuePair<string, string>("Massachusetts", "Massachusetts"));
            stateList.Add(new KeyValuePair<string, string>("Michigan", "Michigan"));
            stateList.Add(new KeyValuePair<string, string>("Minnesota", "Minnesota"));
            stateList.Add(new KeyValuePair<string, string>("Mississippi", "Mississippi"));
            stateList.Add(new KeyValuePair<string, string>("Missouri", "Missouri"));
            stateList.Add(new KeyValuePair<string, string>("Montana", "Montana"));
            stateList.Add(new KeyValuePair<string, string>("Nebraska", "Nebraska"));
            stateList.Add(new KeyValuePair<string, string>("Nevada", "Nevada"));
            stateList.Add(new KeyValuePair<string, string>("New Hampshire", "New Hampshire"));
            stateList.Add(new KeyValuePair<string, string>("New Jersey", "New Jersey"));
            stateList.Add(new KeyValuePair<string, string>("New Mexico", "New Mexico"));
            stateList.Add(new KeyValuePair<string, string>("New York", "New York"));
            stateList.Add(new KeyValuePair<string, string>("North Carolina", "North Carolina"));
            stateList.Add(new KeyValuePair<string, string>("North Dakota", "North Dakota"));
            stateList.Add(new KeyValuePair<string, string>("Ohio", "Ohio"));
            stateList.Add(new KeyValuePair<string, string>("Oklahoma", "Oklahoma"));
            stateList.Add(new KeyValuePair<string, string>("Oregon", "Oregon"));
            stateList.Add(new KeyValuePair<string, string>("Pennsylvania", "Pennsylvania"));
            stateList.Add(new KeyValuePair<string, string>("Rhode Island", "Rhode Island"));
            stateList.Add(new KeyValuePair<string, string>("South Carolina", "South Carolina"));
            stateList.Add(new KeyValuePair<string, string>("South Dakota", "South Dakota"));
            stateList.Add(new KeyValuePair<string, string>("Tennessee", "Tennessee"));
            stateList.Add(new KeyValuePair<string, string>("Texas", "Texas"));
            stateList.Add(new KeyValuePair<string, string>("Utah", "Utah"));
            stateList.Add(new KeyValuePair<string, string>("Vermont", "Vermont"));
            stateList.Add(new KeyValuePair<string, string>("Virginia", "Virginia"));
            stateList.Add(new KeyValuePair<string, string>("Washington", "Washington"));
            stateList.Add(new KeyValuePair<string, string>("West Virginia", "West Virginia"));
            stateList.Add(new KeyValuePair<string, string>("Wisconsin", "Wisconsin"));
            stateList.Add(new KeyValuePair<string, string>("Wyoming", "Wyoming"));
            stateList.Add(new KeyValuePair<string, string>("Other", "Other"));





            ViewBag.Sexes = sexList;
            ViewBag.Years = yearList;
            ViewBag.States = stateList;
            
            string userId = this.User.Identity.GetUserId();
            var user = db.Users.Find(userId);

            Profile vm = new Profile() { Email = user.Email, IsVisible = false };

            return View(vm);
        }

        // POST: Profiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "YearInSchool,FirstName,LastName,PhoneNumber,Sex,Major,Approved,Birthday,Email,IsVisible,HasCar,EmergencyContactName,EmergencyContactPhoneNumber,BackgroundColor")] Profile profile)
        {
            List<KeyValuePair<string, string>> yearList = new List<KeyValuePair<string, string>>();
            yearList.Add(new KeyValuePair<string, string>("Freshman", "Freshmen"));
            yearList.Add(new KeyValuePair<string, string>("Sophomore", "Sophomore"));
            yearList.Add(new KeyValuePair<string, string>("Junior", "Junior"));
            yearList.Add(new KeyValuePair<string, string>("Senior", "Senior"));
            yearList.Add(new KeyValuePair<string, string>("Graduate", "Graduate"));
            yearList.Add(new KeyValuePair<string, string>("Other", "Other"));
            List<KeyValuePair<string, string>> sexList = new List<KeyValuePair<string, string>>();
            sexList.Add(new KeyValuePair<string, string>("Male", "Male"));
            sexList.Add(new KeyValuePair<string, string>("Female", "Female"));
            sexList.Add(new KeyValuePair<string, string>("Other", "Other"));
            List<KeyValuePair<string, string>> stateList = new List<KeyValuePair<string, string>>();
            stateList.Add(new KeyValuePair<string, string>("Alabama", "Alabama"));
            stateList.Add(new KeyValuePair<string, string>("Alaska", "Alaska"));
            stateList.Add(new KeyValuePair<string, string>("Arizona", "Arizona"));
            stateList.Add(new KeyValuePair<string, string>("Arkansas", "Arkansas"));
            stateList.Add(new KeyValuePair<string, string>("California", "California"));
            stateList.Add(new KeyValuePair<string, string>("Colorado", "Colorado"));
            stateList.Add(new KeyValuePair<string, string>("Connecticut", "Connecticut"));
            stateList.Add(new KeyValuePair<string, string>("Delaware", "Delaware"));
            stateList.Add(new KeyValuePair<string, string>("Florida", "Florida"));
            stateList.Add(new KeyValuePair<string, string>("Georgia", "Georgia"));
            stateList.Add(new KeyValuePair<string, string>("Hawaii", "Hawaii"));
            stateList.Add(new KeyValuePair<string, string>("Idaho", "Idaho"));
            stateList.Add(new KeyValuePair<string, string>("Illinois", "Illinois"));
            stateList.Add(new KeyValuePair<string, string>("Indiana", "Indiana"));
            stateList.Add(new KeyValuePair<string, string>("Iowa", "Iowa"));
            stateList.Add(new KeyValuePair<string, string>("Kansas", "Kansas"));
            stateList.Add(new KeyValuePair<string, string>("Kentucky", "Kentucky"));
            stateList.Add(new KeyValuePair<string, string>("Louisiana", "Louisiana"));
            stateList.Add(new KeyValuePair<string, string>("Maine", "Maine"));
            stateList.Add(new KeyValuePair<string, string>("Maryland", "Maryland"));
            stateList.Add(new KeyValuePair<string, string>("Massachusetts", "Massachusetts"));
            stateList.Add(new KeyValuePair<string, string>("Michigan", "Michigan"));
            stateList.Add(new KeyValuePair<string, string>("Minnesota", "Minnesota"));
            stateList.Add(new KeyValuePair<string, string>("Mississippi", "Mississippi"));
            stateList.Add(new KeyValuePair<string, string>("Missouri", "Missouri"));
            stateList.Add(new KeyValuePair<string, string>("Montana", "Montana"));
            stateList.Add(new KeyValuePair<string, string>("Nebraska", "Nebraska"));
            stateList.Add(new KeyValuePair<string, string>("Nevada", "Nevada"));
            stateList.Add(new KeyValuePair<string, string>("New Hampshire", "New Hampshire"));
            stateList.Add(new KeyValuePair<string, string>("New Jersey", "New Jersey"));
            stateList.Add(new KeyValuePair<string, string>("New Mexico", "New Mexico"));
            stateList.Add(new KeyValuePair<string, string>("New York", "New York"));
            stateList.Add(new KeyValuePair<string, string>("North Carolina", "North Carolina"));
            stateList.Add(new KeyValuePair<string, string>("North Dakota", "North Dakota"));
            stateList.Add(new KeyValuePair<string, string>("Ohio", "Ohio"));
            stateList.Add(new KeyValuePair<string, string>("Oklahoma", "Oklahoma"));
            stateList.Add(new KeyValuePair<string, string>("Oregon", "Oregon"));
            stateList.Add(new KeyValuePair<string, string>("Pennsylvania", "Pennsylvania"));
            stateList.Add(new KeyValuePair<string, string>("Rhode Island", "Rhode Island"));
            stateList.Add(new KeyValuePair<string, string>("South Carolina", "South Carolina"));
            stateList.Add(new KeyValuePair<string, string>("South Dakota", "South Dakota"));
            stateList.Add(new KeyValuePair<string, string>("Tennessee", "Tennessee"));
            stateList.Add(new KeyValuePair<string, string>("Texas", "Texas"));
            stateList.Add(new KeyValuePair<string, string>("Utah", "Utah"));
            stateList.Add(new KeyValuePair<string, string>("Vermont", "Vermont"));
            stateList.Add(new KeyValuePair<string, string>("Virginia", "Virginia"));
            stateList.Add(new KeyValuePair<string, string>("Washington", "Washington"));
            stateList.Add(new KeyValuePair<string, string>("West Virginia", "West Virginia"));
            stateList.Add(new KeyValuePair<string, string>("Wisconsin", "Wisconsin"));
            stateList.Add(new KeyValuePair<string, string>("Wyoming", "Wyoming"));
            stateList.Add(new KeyValuePair<string, string>("Other", "Other"));


            ViewBag.Sexes = sexList;
            ViewBag.Years = yearList;
            ViewBag.States = stateList;
            if (ModelState.IsValid)
            {
                string userId = this.User.Identity.GetUserId();
                profile.Id = userId;
                profile.BackgroundColor = "#B40404";
                db.Profiles.Add(profile);
                db.SaveChanges();
                return RedirectToAction("Manage", "Account");
            }

            return View(profile);
        }

        // GET: Profiles/Edit/5
        public ActionResult Edit(bool approved)
        {
            string userId = this.User.Identity.GetUserId();
            
            Profile profile = db.Profiles.Find(userId);
            profile.Approved = approved;

            List<KeyValuePair<string, string>> yearList = new List<KeyValuePair<string, string>>();
            yearList.Add(new KeyValuePair<string, string>("Freshman", "Freshmen"));
            yearList.Add(new KeyValuePair<string, string>("Sophomore", "Sophomore"));
            yearList.Add(new KeyValuePair<string, string>("Junior", "Junior"));
            yearList.Add(new KeyValuePair<string, string>("Senior", "Senior"));
            yearList.Add(new KeyValuePair<string, string>("Graduate", "Graduate"));
            yearList.Add(new KeyValuePair<string, string>("Other", "Other"));

            List<KeyValuePair<string, string>> sexList = new List<KeyValuePair<string, string>>();
            sexList.Add(new KeyValuePair<string, string>("Male", "Male"));
            sexList.Add(new KeyValuePair<string, string>("Female", "Female"));
            sexList.Add(new KeyValuePair<string, string>("Other", "Other"));

            List<KeyValuePair<string, string>> colorsList = new List<KeyValuePair<string, string>>();
            colorsList.Add(new KeyValuePair<string, string>("#B40404", "Red"));
            colorsList.Add(new KeyValuePair<string, string>("#6600CC", "Purple"));
            colorsList.Add(new KeyValuePair<string, string>("#6db8f2", "Blue"));
            colorsList.Add(new KeyValuePair<string, string>("#009933", "Green"));
            colorsList.Add(new KeyValuePair<string, string>("#ffcc66", "Orange"));
            //colorsList.Add(new KeyValuePair<string, string>("-moz-linear-gradient(center top , rgba(17, 2, 49, 0.75), rgba(17, 2, 49, 0)), -moz-linear-gradient(left bottom , #EB6670, #F67F7C, #F9AC97, #F1B79F, #E1BEA7, #C3BCB0, #8AABB2, #4E95B2, #2588B6)", "gradient"));


            List<KeyValuePair<string, string>> stateList = new List<KeyValuePair<string, string>>();
            stateList.Add(new KeyValuePair<string, string>("Alabama", "Alabama"));
            stateList.Add(new KeyValuePair<string, string>("Alaska", "Alaska"));
            stateList.Add(new KeyValuePair<string, string>("Arizona", "Arizona"));
            stateList.Add(new KeyValuePair<string, string>("Arkansas", "Arkansas"));
            stateList.Add(new KeyValuePair<string, string>("California", "California"));
            stateList.Add(new KeyValuePair<string, string>("Colorado", "Colorado"));
            stateList.Add(new KeyValuePair<string, string>("Connecticut", "Connecticut"));
            stateList.Add(new KeyValuePair<string, string>("Delaware", "Delaware"));
            stateList.Add(new KeyValuePair<string, string>("Florida", "Florida"));
            stateList.Add(new KeyValuePair<string, string>("Georgia", "Georgia"));
            stateList.Add(new KeyValuePair<string, string>("Hawaii", "Hawaii"));
            stateList.Add(new KeyValuePair<string, string>("Idaho", "Idaho"));
            stateList.Add(new KeyValuePair<string, string>("Illinois", "Illinois"));
            stateList.Add(new KeyValuePair<string, string>("Indiana", "Indiana"));
            stateList.Add(new KeyValuePair<string, string>("Iowa", "Iowa"));
            stateList.Add(new KeyValuePair<string, string>("Kansas", "Kansas"));
            stateList.Add(new KeyValuePair<string, string>("Kentucky", "Kentucky"));
            stateList.Add(new KeyValuePair<string, string>("Louisiana", "Louisiana"));
            stateList.Add(new KeyValuePair<string, string>("Maine", "Maine"));
            stateList.Add(new KeyValuePair<string, string>("Maryland", "Maryland"));
            stateList.Add(new KeyValuePair<string, string>("Massachusetts", "Massachusetts"));
            stateList.Add(new KeyValuePair<string, string>("Michigan", "Michigan"));
            stateList.Add(new KeyValuePair<string, string>("Minnesota", "Minnesota"));
            stateList.Add(new KeyValuePair<string, string>("Mississippi", "Mississippi"));
            stateList.Add(new KeyValuePair<string, string>("Missouri", "Missouri"));
            stateList.Add(new KeyValuePair<string, string>("Montana", "Montana"));
            stateList.Add(new KeyValuePair<string, string>("Nebraska", "Nebraska"));
            stateList.Add(new KeyValuePair<string, string>("Nevada", "Nevada"));
            stateList.Add(new KeyValuePair<string, string>("New Hampshire", "New Hampshire"));
            stateList.Add(new KeyValuePair<string, string>("New Jersey", "New Jersey"));
            stateList.Add(new KeyValuePair<string, string>("New Mexico", "New Mexico"));
            stateList.Add(new KeyValuePair<string, string>("New York", "New York"));
            stateList.Add(new KeyValuePair<string, string>("North Carolina", "North Carolina"));
            stateList.Add(new KeyValuePair<string, string>("North Dakota", "North Dakota"));
            stateList.Add(new KeyValuePair<string, string>("Ohio", "Ohio"));
            stateList.Add(new KeyValuePair<string, string>("Oklahoma", "Oklahoma"));
            stateList.Add(new KeyValuePair<string, string>("Oregon", "Oregon"));
            stateList.Add(new KeyValuePair<string, string>("Pennsylvania", "Pennsylvania"));
            stateList.Add(new KeyValuePair<string, string>("Rhode Island", "Rhode Island"));
            stateList.Add(new KeyValuePair<string, string>("South Carolina", "South Carolina"));
            stateList.Add(new KeyValuePair<string, string>("South Dakota", "South Dakota"));
            stateList.Add(new KeyValuePair<string, string>("Tennessee", "Tennessee"));
            stateList.Add(new KeyValuePair<string, string>("Texas", "Texas"));
            stateList.Add(new KeyValuePair<string, string>("Utah", "Utah"));
            stateList.Add(new KeyValuePair<string, string>("Vermont", "Vermont"));
            stateList.Add(new KeyValuePair<string, string>("Virginia", "Virginia"));
            stateList.Add(new KeyValuePair<string, string>("Washington", "Washington"));
            stateList.Add(new KeyValuePair<string, string>("West Virginia", "West Virginia"));
            stateList.Add(new KeyValuePair<string, string>("Wisconsin", "Wisconsin"));
            stateList.Add(new KeyValuePair<string, string>("Wyoming", "Wyoming"));
            stateList.Add(new KeyValuePair<string, string>("Other", "Other"));

            ViewBag.Sexes = sexList;
            ViewBag.Years = yearList;
            ViewBag.Colors = colorsList;
            ViewBag.States = stateList;
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // POST: Profiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,YearInSchool,FirstName,LastName,PhoneNumber,Email,State,Sex,Major,Approved,Birthday,IsVisible,HasCar,EmergencyContactName,EmergencyContactPhoneNumber,BackgroundColor")] Profile profile)
        {
            List<KeyValuePair<string, string>> yearList = new List<KeyValuePair<string, string>>();
            yearList.Add(new KeyValuePair<string, string>("Freshman", "Freshmen"));
            yearList.Add(new KeyValuePair<string, string>("Sophomore", "Sophomore"));
            yearList.Add(new KeyValuePair<string, string>("Junior", "Junior"));
            yearList.Add(new KeyValuePair<string, string>("Senior", "Senior"));
            yearList.Add(new KeyValuePair<string, string>("Graduate", "Graduate"));
            yearList.Add(new KeyValuePair<string, string>("Other", "Other"));
            List<KeyValuePair<string, string>> sexList = new List<KeyValuePair<string, string>>();
            sexList.Add(new KeyValuePair<string, string>("Male", "Male"));
            sexList.Add(new KeyValuePair<string, string>("Female", "Female"));
            sexList.Add(new KeyValuePair<string, string>("Other", "Other"));
            List<KeyValuePair<string, string>> colorsList = new List<KeyValuePair<string, string>>();
            colorsList.Add(new KeyValuePair<string, string>("#B40404", "Red"));
            colorsList.Add(new KeyValuePair<string, string>("#6600CC", "Purple"));
            colorsList.Add(new KeyValuePair<string, string>("#6db8f2", "Blue"));
            colorsList.Add(new KeyValuePair<string, string>("#009933", "Green"));
            colorsList.Add(new KeyValuePair<string, string>("#ffcc66", "Orange"));
            //colorsList.Add(new KeyValuePair<string, string>("-moz-linear-gradient(center top , rgba(17, 2, 49, 0.75), rgba(17, 2, 49, 0)), -moz-linear-gradient(left bottom , #EB6670, #F67F7C, #F9AC97, #F1B79F, #E1BEA7, #C3BCB0, #8AABB2, #4E95B2, #2588B6)", "gradient"));
            List<KeyValuePair<string, string>> stateList = new List<KeyValuePair<string, string>>();
            stateList.Add(new KeyValuePair<string, string>("Alabama", "Alabama"));
            stateList.Add(new KeyValuePair<string, string>("Alaska", "Alaska"));
            stateList.Add(new KeyValuePair<string, string>("Arizona", "Arizona"));
            stateList.Add(new KeyValuePair<string, string>("Arkansas", "Arkansas"));
            stateList.Add(new KeyValuePair<string, string>("California", "California"));
            stateList.Add(new KeyValuePair<string, string>("Colorado", "Colorado"));
            stateList.Add(new KeyValuePair<string, string>("Connecticut", "Connecticut"));
            stateList.Add(new KeyValuePair<string, string>("Delaware", "Delaware"));
            stateList.Add(new KeyValuePair<string, string>("Florida", "Florida"));
            stateList.Add(new KeyValuePair<string, string>("Georgia", "Georgia"));
            stateList.Add(new KeyValuePair<string, string>("Hawaii", "Hawaii"));
            stateList.Add(new KeyValuePair<string, string>("Idaho", "Idaho"));
            stateList.Add(new KeyValuePair<string, string>("Illinois", "Illinois"));
            stateList.Add(new KeyValuePair<string, string>("Indiana", "Indiana"));
            stateList.Add(new KeyValuePair<string, string>("Iowa", "Iowa"));
            stateList.Add(new KeyValuePair<string, string>("Kansas", "Kansas"));
            stateList.Add(new KeyValuePair<string, string>("Kentucky", "Kentucky"));
            stateList.Add(new KeyValuePair<string, string>("Louisiana", "Louisiana"));
            stateList.Add(new KeyValuePair<string, string>("Maine", "Maine"));
            stateList.Add(new KeyValuePair<string, string>("Maryland", "Maryland"));
            stateList.Add(new KeyValuePair<string, string>("Massachusetts", "Massachusetts"));
            stateList.Add(new KeyValuePair<string, string>("Michigan", "Michigan"));
            stateList.Add(new KeyValuePair<string, string>("Minnesota", "Minnesota"));
            stateList.Add(new KeyValuePair<string, string>("Mississippi", "Mississippi"));
            stateList.Add(new KeyValuePair<string, string>("Missouri", "Missouri"));
            stateList.Add(new KeyValuePair<string, string>("Montana", "Montana"));
            stateList.Add(new KeyValuePair<string, string>("Nebraska", "Nebraska"));
            stateList.Add(new KeyValuePair<string, string>("Nevada", "Nevada"));
            stateList.Add(new KeyValuePair<string, string>("New Hampshire", "New Hampshire"));
            stateList.Add(new KeyValuePair<string, string>("New Jersey", "New Jersey"));
            stateList.Add(new KeyValuePair<string, string>("New Mexico", "New Mexico"));
            stateList.Add(new KeyValuePair<string, string>("New York", "New York"));
            stateList.Add(new KeyValuePair<string, string>("North Carolina", "North Carolina"));
            stateList.Add(new KeyValuePair<string, string>("North Dakota", "North Dakota"));
            stateList.Add(new KeyValuePair<string, string>("Ohio", "Ohio"));
            stateList.Add(new KeyValuePair<string, string>("Oklahoma", "Oklahoma"));
            stateList.Add(new KeyValuePair<string, string>("Oregon", "Oregon"));
            stateList.Add(new KeyValuePair<string, string>("Pennsylvania", "Pennsylvania"));
            stateList.Add(new KeyValuePair<string, string>("Rhode Island", "Rhode Island"));
            stateList.Add(new KeyValuePair<string, string>("South Carolina", "South Carolina"));
            stateList.Add(new KeyValuePair<string, string>("South Dakota", "South Dakota"));
            stateList.Add(new KeyValuePair<string, string>("Tennessee", "Tennessee"));
            stateList.Add(new KeyValuePair<string, string>("Texas", "Texas"));
            stateList.Add(new KeyValuePair<string, string>("Utah", "Utah"));
            stateList.Add(new KeyValuePair<string, string>("Vermont", "Vermont"));
            stateList.Add(new KeyValuePair<string, string>("Virginia", "Virginia"));
            stateList.Add(new KeyValuePair<string, string>("Washington", "Washington"));
            stateList.Add(new KeyValuePair<string, string>("West Virginia", "West Virginia"));
            stateList.Add(new KeyValuePair<string, string>("Wisconsin", "Wisconsin"));
            stateList.Add(new KeyValuePair<string, string>("Wyoming", "Wyoming"));
            stateList.Add(new KeyValuePair<string, string>("Other", "Other"));

            ViewBag.Sexes = sexList;
            ViewBag.Years = yearList;
            ViewBag.Colors = colorsList;
            ViewBag.States = stateList;
            if (ModelState.IsValid)
            {
                
                db.Entry(profile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Manage", "Account");
            }

            return View(profile);
        }

        // GET: Profiles/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // POST: Profiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Profile profile = db.Profiles.Find(id);
            db.Profiles.Remove(profile);
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
