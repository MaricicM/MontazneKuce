using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MontazneKuce.Models;
using System.Net;

namespace MontazneKuce.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdministracijaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(int? id)
        {
            if (id == 1)
            {
                return View(db.Users.Where(u => u.Roles.Any(r => db.Roles.FirstOrDefault(ro => ro.Name == "korisnik").Id == r.RoleId)));
            }
            return View();
        }

        public PartialViewResult _VratiKorisnike()
        {
            return PartialView(db.Users.Where(u => u.Roles.Any(r => db.Roles.FirstOrDefault(ro => ro.Name == "korisnik").Id == r.RoleId)));
        }

        public PartialViewResult _VratiRole()
        {
            List<RolaView> rv = new List<RolaView>();
            foreach (var item in db.Roles)
            {
                IEnumerable<ApplicationUser> lk = db.Users.Where(u => u.Roles.Any(r => r.RoleId == item.Id));
                rv.Add(new RolaView { Id = item.Id, Naziv = item.Name, BrojKorisnika = lk});
            }
            return PartialView(rv);
        }

        public PartialViewResult _KorisniciIzRole (string id)
        {
            IEnumerable<ApplicationUser> lk = db.Users.Where(u => u.Roles.Any(r => r.RoleId == id));
            return PartialView("_VratiKorisnike",lk);
        }

        public ActionResult DaLiSteSigurni(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser kor = db.Users.Find(id);
            if (kor == null)
            {
                return HttpNotFound();
            }
            return View(kor);
        }

        public ActionResult IzbaciKorisnika(string id)
        {
            ViewBag.Poruka = "";
            try
            {
                ApplicationUser kor = db.Users.Find(id);
                ViewBag.Poruka = "Korisnik: " + kor.UserName + " je izbacen sa sajta";
                db.Users.Remove(kor);
                db.SaveChanges();
            }
            catch (Exception)
            {
                ViewBag.Poruka = "Greska u komunikaciji sa bazom";
            }            
            return View("Index", db.Users.Where(u => u.Roles.Any(r => db.Roles.FirstOrDefault(ro => ro.Name == "korisnik").Id == r.RoleId)));
        }

        public int VratiBrojKorisnika(string id)
        {
            int br = 0;
            foreach (ApplicationUser kor in db.Users)
            {
                br = (kor.Roles.Any(r => r.RoleId == id)) ? br + 1 : br;
            }
            return br;
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