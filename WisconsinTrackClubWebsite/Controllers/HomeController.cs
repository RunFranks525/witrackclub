using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WisconsinTrackClubWebsite.Models.ViewModels;
using WisconsinTrackClubWebsite.Models;
using Microsoft.AspNet.Identity;
using Google.GData.Client;
using Google.GData.Spreadsheets;
using System.Text;

namespace WisconsinTrackClubWebsite.Controllers
{
    public class CssViewResult : PartialViewResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = "text/css";
            base.ExecuteResult(context);
        }
    }
    
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            
            if (!User.Identity.IsAuthenticated)
            {
                ViewBag.ProfilePicture = "../Assets/cow.jpg";
                Session["backColor"] = "#B40404";
                if (db.Information.Count() != 0)
                {
                    var announcementData = (from p in db.Information
                                            where p.Type == "Announcement"
                                            orderby p.Id descending
                                            select p).Take(1);
                    List<Information> announcements = announcementData.ToList();

                    if (announcements.Count != 0)
                    {
                        ViewBag.Announcement = announcements[0];
                    }

                    var currentEventData = (from p in db.Information
                                            where p.Type == "CurrentEvent"
                                            orderby p.Id descending
                                            select p).Take(1);
                    List<Information> currentEvents = currentEventData.ToList();

                    if (currentEvents.Count != 0)
                    {
                        ViewBag.CurrentEvent = currentEvents[0];
                    }
                }
                else
                {
                    ViewBag.Announcement = null;
                }
                return View();
            }
            else
            {
                var userId = User.Identity.GetUserId();
                var profiles = (from u in db.Users
                               where u.Id == userId
                               select u.Profile).ToList();
                if(profiles.Count != 0)
                {
                    var profile = profiles[0];
                    ViewBag.Profile = profile;
                    Session["backColor"] = profile.BackgroundColor;
                    if (profile != null && !String.IsNullOrEmpty(profile.FacebookProviderKey))
                    {
                        string url = "https://graph.facebook.com/" + profile.FacebookProviderKey + "/picture?type=large";
                        ViewBag.ProfilePicture = url;
                    }
                    else
                    {
                        ViewBag.ProfilePicture = "../Assets/cow.jpg";
                    }
                }
                if (db.Information.Count() != 0)
                {
                    var announcementData = (from p in db.Information
                                            where p.Type == "Announcement"
                                            orderby p.Id descending
                                            select p).Take(1);
                    List<Information> announcements = announcementData.ToList();

                    if (announcements.Count != 0)
                    {
                        ViewBag.Announcement = announcements[0];
                    }

                    var currentEventData = (from p in db.Information
                                            where p.Type == "CurrentEvent"
                                            orderby p.Id descending
                                            select p).Take(1);
                    List<Information> currentEvents = currentEventData.ToList();

                    if (currentEvents.Count != 0)
                    {
                        ViewBag.CurrentEvent = currentEvents[0];
                    }
                }
                else
                {
                    ViewBag.Announcement = null;
                }
                return View(db.UserRaces.ToList());



            }
            
             
        }

        public ActionResult Feed()
        {
            if (User.Identity.IsAuthenticated)
            {
                FeedViewModel feed = new FeedViewModel();
                var ur = db.UserRaces;
                feed.UserRaces = ur.ToList();
                                 
              
                string userId = User.Identity.GetUserId();
                var Prof = db.Users.Find(userId);
                feed.Profile = Prof;
                Information information = new Information();
                feed.Post = information;
                var posts = from p in db.Information
                            where p.Type == "Post"
                            orderby p.Id descending
                            select p;
                feed.Posts = posts.ToList();
                return View("Feed", feed);
            }
            else
            {

                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public ActionResult LikePost(int? id)
        {
            var post = db.Information.Find(id);
            post.Count++;
            FeedViewModel feed = new FeedViewModel();
            feed.UserRaces = db.UserRaces.ToList();
            var temp = User.Identity.GetUserName();

            var profiles = from u in db.Users
                           where u.UserName == temp
                           select u.Profile;
            string userId = User.Identity.GetUserId();
            var Prof = db.Users.Find(userId);
            feed.Profile = Prof;
            db.SaveChanges();
            return PartialView("_LikePost", post);
        }

        [HttpPost]
        public ActionResult PublishPost([Bind(Include ="Id,UpdateTitle,Info,Importance,Type")] Information post)
        {
            FeedViewModel feed = new FeedViewModel();
            post.UpdateTitle = Profile.UserName + " Post";
            post.Importance = false;
            post.Type = "Post";

            if (ModelState.IsValid)
            {
                var Prof = db.Profiles.Find(User.Identity.GetUserId());
                post.Author = Prof;
                db.Information.Add(post);
                db.SaveChanges();
                Information newPost = new Information();
                feed.Post = newPost;
                string userId = User.Identity.GetUserId();
                var profile = db.Users.Find(userId);
                feed.Profile = profile;
                feed.UserRaces = db.UserRaces.ToList();
                var posts = from p in db.Information
                                        where p.Type == "Post"
                                        orderby p.Id descending
                                        select p;
                feed.Posts = posts.ToList();
                return RedirectToAction("Feed");
            }

            return null;
        }

        [HttpPost]
        public ActionResult Like(int? id)
        {
            var race = db.UserRaces.Find(id);
            race.Count++;
            FeedViewModel feed = new FeedViewModel();
            feed.UserRaces = db.UserRaces.ToList();
            var temp = User.Identity.GetUserName();

            var profiles = from u in db.Users
                           where u.UserName == temp
                           select u.Profile;
            string userId = User.Identity.GetUserId();
            var Prof = db.Users.Find(userId);
            feed.Profile = Prof;
            db.SaveChanges();
            return PartialView("_Like", race);
        }

        public ActionResult OfficersAndContacts()
        {
            List<Person> officers = db.Officers.ToList();
            OfficerViewModel ovm = new OfficerViewModel();
            ovm.people = officers;
            
            return View("OfficersContactsList", ovm);
        }

        [HttpPost]
        public ActionResult OfficerLoader(int officerId)
        {
            var officer = db.Officers.Find(officerId);
            OfficerViewModel ovm = new OfficerViewModel();
            ovm.OfficerInContext = officer;

            return PartialView("_Officers", ovm);
        }

        public ActionResult About()
        {
            return View("About");
        }

        public ActionResult Events()
        {
            return View("Events");
        }

        public ActionResult Bylaws()
        {
            return View("Bylaws");
        }

        public ActionResult Workouts()
        {
            return View("Workouts");
        }

        public ActionResult Membership()
        {
            return View("Membership");
        }

        public ActionResult Apparel()
        {
            return View("Apparel");
        }

        public ActionResult SummerTrack()
        {
            return View("SummerTrack");
        }

        public ActionResult Moovin()
        {
            return View("Moovin5k");
        }

        public ActionResult PastResults()
        {
            return View("PastResults");
        }

        public ActionResult Performance()
        {
            return View("Performance");
        }

        public ActionResult CurrentSchedule()
        {
            var Races = (from ra in db.Races
                          where ra.Date > System.DateTime.Now
                          orderby ra.Date ascending
                          select ra).ToList();
            return View("CurrentSchedule", Races);
        }

        public ActionResult CapitolMile()
        {
            return View("CapitolMile");
        }

        public ActionResult ManagedRaces()
        {
            return View("ManagedRaces");
        }

        public ActionResult Track()
        {
            return View("Track");
        }

        public ActionResult CrossCountry()
        {
            return View("CrossCountry");
        }

        public ActionResult AreaRuns()
        {
            return View("AreaRuns");
        }

        public ActionResult Moosletter()
        {
            return View("Moosletter");
        }

        public ActionResult TC101()
        {
            return View("TC101");
        }

        public ActionResult MyForms()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            if (ModelState.IsValid)
            {
                var temp = User.Identity.GetUserId();
                var userFormList = (from uf in db.UserForms where uf.ApplicationUser.Id == temp select new UserFormsViewModel() { Name = uf.Form.Name, Description = uf.Form.Description, Url = uf.Form.Url, IsComplete = uf.IsComplete }).ToList<UserFormsViewModel>();
                MyFormsViewModel mfvm = new MyFormsViewModel();
                mfvm.MyForms = userFormList;
                mfvm.UserProfile = db.Profiles.Find(temp);
                return View("Forms", mfvm);
            }
            
            return View();
        }

        public ActionResult MyRaces()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            if (ModelState.IsValid)
            {
                string userId = this.User.Identity.GetUserId();
                var userRaces = (from ur in db.UserRaces
                                 where ur.ApplicationUser.Id == userId
                                 select new RaceViewModel()
                                 {
                                     Location = ur.Race.Location,
                                     Date = ur.Race.Date,
                                     Name = ur.Race.RaceName,
                                     RaceId = ur.UserRaceId,
                                     HighLights = ur.HighLights
                                 }).ToList<RaceViewModel>();
                MyRacesViewModel mrvm = new MyRacesViewModel();
                mrvm.MyRaces = userRaces;
                mrvm.UserProfile = db.Profiles.Find(userId);
                return View("RaceTracker", mrvm);
            }
            return View();
        }

        public ActionResult PaceCalc()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            PaceCalculator paceCalc = new PaceCalculator();
            return View("PaceCalc");
        }

        [HttpPost]
        public ActionResult CalculatePace(PaceCalculator model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            int pace = ((model.Minutes + (model.Seconds/100))/ model.Miles);
            string[] responses = new string[5];
            responses[0] = "You killed it!";
            responses[1] = "Damnnn. You smokin'.";
            responses[2] = "Great Job!";
            responses[3] = "Impressive!";
            responses[4] = "You were mooooovin!";

            Random random = new Random();
            int num = random.Next(0, 4);
            string generatedString = responses[num];
            string information = "You ran " + model.Miles + " miles averaging " + pace + " minutes/mile. " + generatedString;
            return Content(information);

        }

        public ActionResult MileLog()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            return View("MileLog");
        }

        public ActionResult PrivacyPolicy()
        {
            return View("PrivacyPolicy");
        }

        public ActionResult Members()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            if (ModelState.IsValid)
            {
                MembersViewModel mvm = new MembersViewModel();
                mvm.people = db.Profiles.ToList();
                mvm.UserProfile = db.Profiles.Find(User.Identity.GetUserId());
                return View("Members", mvm);
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult Approved(string form1Complete, string form2Complete, string form3Complete, string form4Complete, string form5Complete, string userId)
        {
            if (ModelState.IsValid)
            {
                //Reference
                //var memberNorm = form1Complete;
                //var supplemental = form2Complete;
                //var memberConsent = form3Complete;
                //var authorizedDriver = form4Complete;
                //var dues = form5Complete;
                bool form1Checked = form1Complete == "Form1";
                bool form2Checked = form2Complete == "Form2";
                bool form3Checked = form3Complete == "Form3";
                bool form4Checked = form4Complete == "Form4";
                bool form5Checked = form5Complete == "Form5";

                var userForms = (from uf in db.UserForms
                                 where uf.ApplicationUser.Id.Equals(userId)
                                 select uf).ToList();

                if (form1Checked && form2Checked && form3Checked && form4Checked && form5Checked)
                {
                    for (int i = 0; i < userForms.Count; i++)
                    {
                        userForms[i].IsComplete = true;
                        db.Entry(userForms[i]).State = System.Data.Entity.EntityState.Modified;
                    }
                }
                else
                {
                    userForms[0].IsComplete = form1Checked;
                    db.Entry(userForms[0]).State = System.Data.Entity.EntityState.Modified;

                    userForms[1].IsComplete = form2Checked;
                    db.Entry(userForms[1]).State = System.Data.Entity.EntityState.Modified;

                    userForms[2].IsComplete = form3Checked;
                    db.Entry(userForms[2]).State = System.Data.Entity.EntityState.Modified;

                    userForms[3].IsComplete = form4Checked;
                    db.Entry(userForms[3]).State = System.Data.Entity.EntityState.Modified;

                    userForms[4].IsComplete = form5Checked;
                    db.Entry(userForms[4]).State = System.Data.Entity.EntityState.Modified;
                }

                string emailFrom;
                string emailTo;
                string emailSubject;
                string emailBody;

                var prof = db.Profiles.Find(userId);
                bool allComplete = userForms.All(form => form.IsComplete);

                prof.Approved = allComplete;
                db.Entry(prof).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                if (allComplete)
                {
                    emailFrom = "stfranklin@wisc.edu";
                    emailSubject = "You've Been Approved at Wisconsin Track Club";
                    emailBody = "<div><h1>Wisconsin Track Club</h1><div style=\"margin-left: -15px; margin-right: -15px;\"><div style=\"padding: 25px; margin: 5px; background-color: whitesmoke\"><div style=\"margin-left: -15px; margin-right: -15px; background-color: #b65f5d; color: white; padding: 2%;\"><div>"
                         + "<h4 style=\"padding: 5%; margin-bottom: 20px; font-size: x-large;\">You've Been Approved!</h4></div></div><hr style=\"border-color: black; box-shadow: 5px 5px 0px #888888;\" /><p style=\"text-indent: 40px; font-size: medium; text-align: left; padding:10px; \"> Your status with Wisconsin Track Club has just been approved on our website. Thanks for getting all neccessary information into us.</p >"
                         + "<br /><p> Check out your profile at </p ><a href = \"http://www.witrackclub.org\"> www.witrackclub.org </a >!</p ></div></div></div>";
                }
                else
                {
                    emailFrom = "witrackclub@gmail.com";
                    emailSubject = "Status Update at Wisconsin Track Club";
                    emailBody = "<div><h1>Wisconsin Track Club</h1><div style=\"margin-left: -15px; margin-right: -15px;\"><div style=\"padding: 25px; margin: 5px; background-color: whitesmoke\"><div style=\"margin-left: -15px; margin-right: -15px; background-color: #b65f5d; color: white; padding: 2%;\"><div>"
                             + "<h4 style=\"padding: 5%; margin-bottom: 20px; font-size: x-large;\">Your status has been updated</h4></div></div><hr style=\"border-color: black; box-shadow: 5px 5px 0px #888888;\" /><p style=\"text-indent: 40px; font-size: medium; text-align: left; padding:10px; \"> Your status with Wisconsin Track Club has just been updated on our website.</p>"
                             + "<br /><p> To see what you still need to get in to us, check out your profile at </p ><a href = \"http://www.witrackclub.org\"> www.witrackclub.org </a >!</p ></div></div></div>";
                }
                emailTo = prof.Email;
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                message.From = new System.Net.Mail.MailAddress(emailFrom);
                message.To.Add(new System.Net.Mail.MailAddress(emailTo));
                message.IsBodyHtml = true;
                message.BodyEncoding = Encoding.UTF8;
                message.Subject = emailSubject;
                message.Body = emailBody;

                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                client.Send(message);


                return RedirectToAction("AdminInterface");
            }
            return View("Index");
        }

        public ActionResult Email()
        {
            return View("_EmailApproval");
        }

        public ActionResult Contact()
        {
            return View("Contact");
        }

        [HttpPost]
        public ActionResult RemoveUser(string userId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            if (ModelState.IsValid)
            {

                var prof = db.Profiles.Find(userId);
                db.Profiles.Remove(prof);
                
                db.SaveChanges();
                db.Users.Remove(db.Users.Find(userId));
                db.SaveChanges();
                List<Profile> people = db.Profiles.ToList();
                return PartialView("_Remove", people);
            }
            return View("Index");

        }

        public ActionResult AdminInterface()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            if (User.IsInRole("Admin"))
            {
            AdminInterfaceViewModel aivm = new AdminInterfaceViewModel();
            ViewBag.People = db.Profiles.ToList();

            Information information = new Information();
            aivm.Information = information;
            aivm.RaceRegistrations = (from rr in db.RaceRegistration
                                     where rr.RegisteredRace.Date > System.DateTime.Now
                                     orderby rr.RegisteredRace.RaceId ascending
                                     select rr).ToList();
            string userId = User.Identity.GetUserId();
            var prof = db.Profiles.Find(userId);
            aivm.Current = prof;
                
                var userFormList = db.UserForms.ToList();
                Dictionary<string, List<UserForm>> userFormsCollection = new Dictionary<string, List<UserForm>>();
                foreach(var person in ViewBag.People)
                {
                    List<UserForm> usersForms = new List<UserForm>();
                    foreach(var form in userFormList)
                    {
                        if (form.ApplicationUser.Id.Equals(person.Id))
                        {
                            usersForms.Add(form);
                        }
                    }
                    userFormsCollection.Add(person.Id, usersForms);
                }
                aivm.UserFormCollection = userFormsCollection;
                

                //foreach(var race in db.Races.ToList())
                //{
                //    aivm.Races.Add(new KeyValuePair<int, Race>(race.RaceId, race));
                //}

                //foreach (string key in userFormsCollection.Keys)
                //{
                //    if (key.Equals(ViewBag.People[0].Id))
                //    {
                //       foreach(var form in userFormsCollection[key])
                //        {

                //        }
                //    }
                //}
                ViewBag.Forms = db.Forms.ToList();
                
                return View("AdminInterface", aivm);
            }
            return RedirectToAction("Manage", "Account");
        }

        [HttpGet]
        public ActionResult ProfileStatus(string userId, bool one, bool two, bool three, bool four, bool five)
        {


            return null;
        }

        public ActionResult RaceRegistration()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            RaceRegistrationViewModel rrvm = new RaceRegistrationViewModel();
            string userId = User.Identity.GetUserId();
            var Prof = db.Users.Find(userId);
            rrvm.Profile = Prof.Profile;
            rrvm.Races = (from ra in db.Races
                         where ra.Date >= System.DateTime.Now
                         orderby ra.Date ascending
                          select ra).ToList();
            var raceRegistrations = (from r in db.RaceRegistration
                                     where r.RegisteredPerson.Id == Prof.Profile.Id
                                     select r).Distinct();
            //var usersT = from u in db.RaceRegistration
            //            where u.RegisteredPerson.Approved
     
            //            select u;
            var users = db.RaceRegistration.Include("RegisteredPerson").Where(x => x.RegisteredPerson.Approved).Select(x => x);
                       
            rrvm.Runners = users.ToList();
            List<RaceRegistration> racesForUser = raceRegistrations.ToList();
            rrvm.RaceRegistrations = racesForUser;

            Dictionary<String, List<RaceRegistration>> raceRegistrationsCollection = new Dictionary<string, List<Models.RaceRegistration>>();
            foreach(var race in rrvm.Runners)
            {
                String raceName = race.RegisteredRace.RaceName;
                if (!raceRegistrationsCollection.ContainsKey(raceName))
                {
                    raceRegistrationsCollection.Add(raceName, rrvm.Runners.Where(model => model.RegisteredRace.RaceName.Equals(raceName)).ToList());
                }
            }
            rrvm.raceRegistrationsCollection = raceRegistrationsCollection;
                                                       
                
            if (Prof.Profile.Approved)
            {
                return View("RaceRegistration", rrvm);
            }

            return RedirectToAction("Manage", "Account");
        }

        [HttpGet]
        public ActionResult GoToRegistration(int raceId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                var Prof = db.Users.Find(userId);
                var profile = Prof.Profile;
                var race = db.Races.Find(raceId);
                RaceRegistration raceRegister = new RaceRegistration();
                raceRegister.RegisteredPerson = profile;
                raceRegister.RegisteredRace = race;
                raceRegister.Registered = true;
                RaceRegistrationViewModel rrvm = new RaceRegistrationViewModel();
                rrvm.Profile = profile;
                rrvm.RegistrationInFocus = raceRegister;
                return View("RegistrationInformation", raceRegister);      
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult RegisterEntry([Bind(Include = "RaceRegistrationId,RegisteredRace,RegisteredPerson,CanDrive,CarCapacity,Departure")] RaceRegistration registration, string userId, int raceId)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    var Prof = db.Users.Find(userId);
                    var race = db.Races.Find(raceId);
                    RaceRegistration registrationEntry = registration;
                    registrationEntry.RegisteredPerson = Prof.Profile;
                    registrationEntry.RegisteredRace = race;
                    if (registrationEntry.Departure == DateTime.MinValue)
                    {
                        registrationEntry.Departure = System.DateTime.Now;
                        
                    }
                    db.RaceRegistration.Add(registrationEntry);
                    db.SaveChanges();
                    return RedirectToAction("RaceRegistration");
                }
            }
            else
            {
                    return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("Index");
        }

        

        [HttpGet]
        public ActionResult RaceCancellation(int raceId)
        {
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                var Prof = db.Users.Find(userId);
                var profile = Prof.Profile;
                var race = db.Races.Find(raceId);
                var raceRegistration = from r in db.RaceRegistration
                                       where r.RegisteredPerson.Id == profile.Id && r.RegisteredRace.RaceId == race.RaceId
                                       select r;
                List<RaceRegistration> rr = raceRegistration.ToList();
                if (rr.Count != 0)
                {
                    RaceRegistration raceRegister = rr[0];
                    db.RaceRegistration.Remove(raceRegister);
                    db.SaveChanges();
                }
                return RedirectToAction("RaceRegistration");
            }
            return View();
        }

        
        [HttpPost]
        public ActionResult publishAnnouncement(string updateTitle, string info, bool importance)
        {
            string userId = User.Identity.GetUserId();
            if (String.IsNullOrEmpty(updateTitle) || String.IsNullOrEmpty(info) || importance == null)
            {
                AdminInterfaceViewModel aivm = new AdminInterfaceViewModel();
                aivm.People = db.Profiles.ToList();
                Information newUpdate = new Information();
                aivm.Information = newUpdate;
                var prof = db.Profiles.Find(userId);
                aivm.Current = prof;
                aivm.ErrorMessage = "Not all the necessary fields were completed. Please try again. Be sure to include a title, the necessary information, and the level of importance.";
                return PartialView("_Announcements", aivm);
            }
            if (User.IsInRole("Admin"))
            {
                if (ModelState.IsValid)
                {
                    Information information = new Information();
                    information.UpdateTitle = updateTitle;
                    information.Info = info;
                    information.Importance = importance;
                    var prof = db.Profiles.Find(userId);
                    information.Author = prof;
                    Random rnd = new Random();
                    information.Id = rnd.Next(1, 100000);
                    information.Type = "Announcement";
                    db.Information.Add(information);
                    db.SaveChanges();

                    AdminInterfaceViewModel aivm = new AdminInterfaceViewModel();
                    aivm.People = db.Profiles.ToList();
                    Information newUpdate = new Information();
                    aivm.Information = newUpdate;
                    aivm.Current = prof;
                    aivm.ErrorMessage = String.Empty;
                    return PartialView("_Announcements", aivm);

                }
            }

            
            return RedirectToAction("Manage", "Account");
        }

        [HttpPost]
        public ActionResult publishCurrentEvent(string updateTitle, string info, bool importance){
            string userId = User.Identity.GetUserId();
            if (String.IsNullOrEmpty(updateTitle) || String.IsNullOrEmpty(info) || importance == null)
            {
                AdminInterfaceViewModel aivm = new AdminInterfaceViewModel();
                aivm.People = db.Profiles.ToList();
                Information newUpdate = new Information();
                aivm.Information = newUpdate;
                var prof = db.Profiles.Find(userId);
                aivm.Current = prof;
                aivm.ErrorMessage = "Not all the necessary fields were completed. Please try again. Be sure to include a title, the necessary information, and the level of importance.";
                return PartialView("_CurrentEvent", aivm);
            }
            if (User.IsInRole("Admin"))
            {
                if (ModelState.IsValid)
                {
                    Information information = new Information();
                    information.UpdateTitle = updateTitle;
                    information.Info = info;
                    information.Importance = importance;
                    var prof = db.Profiles.Find(userId);
                    information.Author = prof;
                    Random rnd = new Random();
                    information.Id = rnd.Next(1, 100000);
                    information.Type = "CurrentEvent";
                    db.Information.Add(information);
                    db.SaveChanges();

                    AdminInterfaceViewModel aivm = new AdminInterfaceViewModel();
                    aivm.People = db.Profiles.ToList();
                    Information newUpdate = new Information();
                    aivm.Information = newUpdate;
                    aivm.Current = prof;
                    aivm.ErrorMessage = String.Empty;
                    return View("_CurrentEvent", aivm);
                }
            }

            return null;
        }

        [HttpPost]
        public ActionResult RemoveEventEntry(int raceId, string runnerId)
        {
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                var Prof = db.Users.Find(runnerId);
                var profile = Prof.Profile;
                var race = db.Races.Find(raceId);
                var raceRegistration = from r in db.RaceRegistration
                                       where r.RegisteredPerson.Id == profile.Id && r.RegisteredRace.RaceId == race.RaceId
                                       select r;
                List<RaceRegistration> rr = raceRegistration.ToList();
                if (rr.Count != 0)
                {
                    RaceRegistration raceRegister = rr[0];
                    db.RaceRegistration.Remove(raceRegister);
                    db.SaveChanges();
                    var racesLeft = db.RaceRegistration.ToList();
                    return PartialView("_RaceRegisteredMembers", racesLeft);
                }
            }

            return View("Index");
        }

        [HttpGet]
        public ActionResult UserProfile(String id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            

            var user = db.Users.Find(id);
            var profile = user.Profile;
            if (profile != null)
            {
                UserProfileViewModel upvm = new UserProfileViewModel();
                upvm.User = profile;
                if (!String.IsNullOrEmpty(profile.FacebookProviderKey))
                {
                    string url = "https://graph.facebook.com/" + profile.FacebookProviderKey + "/picture?type=large";
                    ViewBag.ProfilePicture = url;
                }
                else
                {
                    ViewBag.ProfilePicture = "../Assets/cow.png";
                }
                if (ModelState.IsValid)
                {
                    var posts = (from p in db.Information
                                where p.Author.Id == id && p.Type.Equals("Post")
                                select p).ToList();
                    var races = (from r in db.UserRaces
                                 where r.ApplicationUser.Id == id
                                 select r).ToList();
                    upvm.Races = races;
                    upvm.Races.Reverse();
                    upvm.Posts = posts;
                    upvm.Posts.Reverse();
                    
                }
                return View("User", upvm);
            }
            else
            {
               return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult RaceProfile(int id)
        {
            if(User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                var usersInRace = (from r in db.RaceRegistration
                                  where r.RegisteredRace.RaceId == id
                                  select r.RegisteredPerson).ToList();
                var registrationEntries = (from r in db.RaceRegistration
                                          where r.RegisteredRace.RaceId == id
                                          select r).ToList();
                
                RaceProfileViewModel rpvm = new RaceProfileViewModel();
                rpvm.User = db.Users.Find(User.Identity.GetUserId()).Profile;
                rpvm.Comment = new Information();
                rpvm.Comments = (from r in db.Information
                                where r.Type.EndsWith(id.ToString())
                                select r).ToList();
                if (rpvm.Comments.Count != 0)
                {
                    rpvm.Comments.Reverse();
                }
                rpvm.Race = db.Races.Find(id);
                if (usersInRace.Count != 0)
                {
                    rpvm.RegisteredPeople = usersInRace;
                }
                else
                {
                    rpvm.RegisteredPeople = null;
                }
                if (registrationEntries.Count != 0)
                {
                    rpvm.entries = registrationEntries;
                }
                else
                {
                    rpvm.entries = null;
                }

                return View("RaceProfile", rpvm);
            }
            else
            {
                    return RedirectToAction("Login", "Account");
            }
        }

        [HttpGet]
        public ActionResult RaceDetails()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                RaceViewModel rvm = new RaceViewModel();
                rvm.Races = (from ra in db.Races
                             where ra.Date > System.DateTime.Now
                             orderby ra.Date ascending
                             select ra).ToList();
                             
                rvm.Profile = db.Users.Find(User.Identity.GetUserId()).Profile;
                return View("RacesList", rvm);
            }
        }

        [HttpPost]
        public ActionResult PublishRaceComment(int raceId, string authorId, String commentInfo){
            if (ModelState.IsValid)
            {
                var profile = db.Users.Find(authorId).Profile;
                var race = db.Races.Find(raceId);
                Information newComment = new Information();
                newComment.Author = profile;
                newComment.Info = commentInfo;
                newComment.Type = "RaceComment" + raceId.ToString();
                db.Information.Add(newComment);
                db.SaveChanges();
                return RedirectToAction("Manage", "Account");
            }




            return View("Index");
        }
        
        
    }
}