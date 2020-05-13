using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OnlineBanking.Models;

namespace OnlineBanking.Controllers
{    public class HomeController : Controller
    {
       
        static  private ApplicationDbContext db = new ApplicationDbContext();
       private UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
         public ActionResult PutMoney()
        { return View();
        }
    [HttpPost]
       public ActionResult PutMoney(string s)
        { int mon = int.Parse(s);
          manager.FindById(User.Identity.GetUserId()).KlBalance += mon;
          db.SaveChanges();
          return RedirectToAction("Index");
        }
        public ActionResult RemovMon()
        {
         return View();
        }
        [HttpPost]
        public ActionResult RemovMon(string s)
        {   int mon = int.Parse(s);
            manager.FindById(User.Identity.GetUserId()).KlBalance -= mon;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //для звичайного коистувача
        public ActionResult ChangPersDet()
        {   var currentUser = manager.FindById(User.Identity.GetUserId());
            return View(currentUser);
        }
        //для звичайного користувача
        [HttpPost]
        public ActionResult ChangPersDet (ApplicationUser s)
        {   manager.FindById(User.Identity.GetUserId()).KlName = s.KlName;
            manager.FindById(User.Identity.GetUserId()).KlSurname = s.KlSurname;
            manager.FindById(User.Identity.GetUserId()).KlAddress = s.KlAddress;
            manager.FindById(User.Identity.GetUserId()).KlPassportNum = s.KlPassportNum;
            manager.FindById(User.Identity.GetUserId()).KlPhone = s.KlPhone;
            manager.FindById(User.Identity.GetUserId()).Email = s.Email;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult TransToOthAc()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> TransToOthAc(string s,string Id)
        { string istrortranz="";
            ViewBag.Povid = "";
            int mon = int.Parse(s);
            if (manager.FindById(User.Identity.GetUserId()).KlBalance > mon)
            {
                var Db = new ApplicationDbContext();
                var user = Db.Users.First(u => u.Id == Id);
                manager.FindById(User.Identity.GetUserId()).KlBalance -= mon;
                user.KlBalance += mon;
                var time = System.DateTime.Now.ToString();
                var nampolz = manager.FindById(User.Identity.GetUserId()).UserName;
                var namadr = user.UserName;
                istrortranz = "Operation performed: " + time+ ". Has sent user" + nampolz + " amount "+mon+" to user: "+ namadr;
                manager.FindById(User.Identity.GetUserId()).Istor+=istrortranz;
               
                db.SaveChanges();
                Db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                await Db.SaveChangesAsync();
               
                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.Povid = "You specify the amount that exceeds the amount of money in your account. Please enter less than the amount";
                return View("TransToOthAc");
            }
        }
        public ActionResult ViHist()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = HttpContext.User.Identity.Name;
            var istor = identity.Claims.Where(c => c.Type =="Istoria").Select(c => c.Value).SingleOrDefault();
            ViewBag.Ist= manager.FindById(User.Identity.GetUserId()).Istor.ToString();
            return View();
        }

        public ActionResult AboutKl()
        {   var currentUser = manager.FindById(User.Identity.GetUserId());
            ViewBag.KlIma = currentUser.KlName;
            ViewBag.KlSURNam = currentUser.KlSurname;
            ViewBag.KLAdr = currentUser.KlAddress;
            ViewBag.KlPasNum = currentUser.KlPassportNum;
            ViewBag.KlTelNum = currentUser.KlPhone;
            ViewBag.Email = currentUser.Email;
            return View();
        }
        public ActionResult BALANSKL()
        {   db = new ApplicationDbContext();
            manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            ViewBag.KlBal = currentUser.KlBalance;
            return View();
        }

        public ActionResult TotNumCl()
        {    using (var context = new ApplicationDbContext())
            {  ViewBag.TotNumCL = context.Users.Select(u => u.KlName).Count();
            }
            return PartialView();
        }
        public ActionResult TotMoBank()
        {
            using (var context = new ApplicationDbContext())
            {
                ViewBag.TotMoney = context.Users.Select(u => u.KlBalance).Sum();
            }
            return View();
        }
           public ActionResult ListOllKl()
        {    using (var context = new ApplicationDbContext())
            { return View(context.Users.ToList());
            }
            
        }
        public ActionResult DetailsTeacher(string id =" ")
        {   ApplicationUser teacher;
            using (var context = new ApplicationDbContext())
            {  teacher = context.Users.Find(id);
                if (teacher == null)
                { return HttpNotFound();
                }
                return View(teacher); 
           }           
        }
         //для адміна
        public ActionResult ChangClient(string id = " ")
        {
            ApplicationUser teacher;
            using (var context = new ApplicationDbContext())
            {
                teacher = context.Users.Find(id);
                if (teacher == null)
                {
                    return HttpNotFound();
                }
                return View(teacher);
            }
        }
       //для адміна
        public async Task<ActionResult> ChangPersDetSav(ApplicationUser model)
        {
            if (ModelState.IsValid)
            {
                var Db = new ApplicationDbContext();
                var user = Db.Users.First(u => u.UserName == model.UserName);
                // Update the user data:
                user.KlName = model.KlName;
                user.KlSurname = model.KlSurname;
                user.KlAddress = model.KlAddress;
                user.KlBalance = model.KlBalance;
                user.KlPassportNum = model.KlPassportNum;
                user.KlPhone = model.KlPhone;
                user.Email = model.Email;
                Db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                await Db.SaveChangesAsync();
                return RedirectToAction("ListOllKl");
            }
            
            return View(model);
        }
        //для адміна get зняття коштыв
        public ActionResult WithdrawMoney(string id = " ")
        {
            ApplicationUser adm_kl;
            using (var context = new ApplicationDbContext())
            {
                adm_kl = context.Users.Find(id);
                if (adm_kl == null)
                {
                    return HttpNotFound();
                }
                return View(adm_kl);
            }
        }
        //для адміна пост зняття коштыв
        [HttpPost]
        public async Task<ActionResult> WithdrawMoney(ApplicationUser model, string s)
        {     int mon = int.Parse(s);
            if (ModelState.IsValid)
            {   var Db = new ApplicationDbContext();
                var user = Db.Users.First(u => u.UserName == model.UserName);
                // Update the user data:
                user.KlName = model.KlName;
                user.KlSurname = model.KlSurname;
                user.KlAddress = model.KlAddress;
                user.KlBalance = model.KlBalance;
                user.KlBalance-= mon;
                user.KlPassportNum = model.KlPassportNum;
                user.KlPhone = model.KlPhone;
                user.Email = model.Email;
                Db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                await Db.SaveChangesAsync();
                return RedirectToAction("ListOllKl");
            }

            return View(model);
        }

        public ActionResult Index()
        { return View();
        }
           public ActionResult About()
        {  ViewBag.Message = "A site is written with teacher of mathematics Sikorskyi  Ruslan.";
           return View();
        }

        public ActionResult Contact()
        {   ViewBag.Message = "Ukraine";
            return View();
        }
    }
}