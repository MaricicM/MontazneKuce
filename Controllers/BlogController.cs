using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MontazneKuce.Models;
using System.Net;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace MontazneKuce.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private MontazneKuceContext db = new MontazneKuceContext();
        private ApplicationDbContext kontekst = new ApplicationDbContext();

        public ActionResult Index(int? id)
        {
            ViewBag.TemaId = id;
            return View(db.Teme);
        }

        public ActionResult NovaTema()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NovaTema (string naziv)
        {
            if (naziv == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tema t1 = new Tema { Naziv = naziv, DatumOtvaranja = DateTime.Now, DatumPoslednjegPosta = DateTime.Now - new TimeSpan(24,0,0) };
            try
            {
                db.Teme.Add(t1);
                db.SaveChanges();
                ViewBag.Greska = null;
            }
            catch (Exception e)
            {
                ViewBag.Greska = e.Message;
            }
            return RedirectToAction("/Index");
        }

        public PartialViewResult _DaLiSteSigurni(int? id, int brojPostova = 0)
        {
            Tema t1 = new Tema();
            if (id == null)
            {
                ViewBag.Greska = "Nepravilan zahtev";
                return PartialView(t1);
            }
            t1 = db.Teme.FirstOrDefault(t => t.TemaId == id);
            if (t1 == null)
            {
                ViewBag.Greska = "Ne postoji tema";
                return PartialView(t1);
            }
            ViewBag.BrojPostova = brojPostova;
            return PartialView(t1);
        }
        [ValidateAntiForgeryToken]
        public ActionResult UkloniTemu(int id)
        {
            Tema t1 = db.Teme.Find(id);
            List<Post> postovi = t1.Postovi.ToList();           
            try
            {
                foreach (Post po in postovi)
                {
                    db.Postovi.Remove(po);
                }
                db.Teme.Remove(t1);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                ViewBag.Poruka = e.Message;
            }
            ViewBag.Poruka = "Tema uklonjena";
            return RedirectToAction("Index");
        }

        public PartialViewResult _NoviPost (int? id)
        {
            if (id == null)
            {
                ViewBag.Greska = "Nepravilan zahtev";
            }

            Tema t1 = db.Teme.FirstOrDefault(t => t.TemaId == id);
            if (t1 == null)
            {
                ViewBag.Greska = "Ne postoji tema";
            }

            var korisnik = User.Identity;
            return PartialView(new Post { KorisnikId = korisnik.GetUserId(), TemaId = (int)id });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult _NoviPost(Post ulaz)
        {
            if (ModelState.IsValid)
            {
                ulaz.DatumPoslednjeIzmene = DateTime.Now;
                ulaz.DatumPosta = DateTime.Now;
                Tema t1 = db.Teme.Find(ulaz.TemaId);
                try
                {
                    t1.DatumPoslednjegPosta = DateTime.Now;
                    db.Postovi.Add(ulaz);
                    db.SaveChanges();
                    ViewBag.TemaId = ulaz.TemaId;
                }
                catch (Exception e)
                {
                    ViewBag.Greska = e.Message;
                }
                return PartialView("_PostoviNaTemu", db.Teme.Find(ulaz.TemaId).Postovi);
            }
            return PartialView(ulaz);
        }

        public PartialViewResult _IzmeniPost(int? id)
        {
            Post p1 = new Post();
            if (id == null)
            {
                ViewBag.Greska = "Nepravilan zahtev";
                return PartialView(p1);
            }
            p1 = db.Postovi.FirstOrDefault(p => p.PostId == id);
            if (p1 == null)
            {
                ViewBag.Greska = "Ne postoji post";
                return PartialView();
            }
            return PartialView(p1);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult _IzmeniPost(Post ulaz)
        {
            if (ModelState.IsValid)
            {
                var t1 = db.Teme.Find(ulaz.TemaId);
                var post = db.Postovi.Find(ulaz.PostId);
                try
                {
                    t1.DatumPoslednjegPosta = DateTime.Now;
                    post.Naziv = ulaz.Naziv; post.Sadrzaj = ulaz.Sadrzaj; post.DatumPoslednjeIzmene = DateTime.Now;
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewBag.Greska = e.Message;
                }
                return PartialView("_PostoviNaTemu", db.Teme.Find(ulaz.TemaId).Postovi);
            }
            return PartialView(ulaz);
        }

        public PartialViewResult _UkloniPost(int id)
        {
            Post p = db.Postovi.Find(id);
            db.Postovi.Remove(p);
            db.SaveChanges();
            return PartialView("_PostoviNaTemu", db.Teme.Find(p.TemaId).Postovi);
        }

        public PartialViewResult _PostoviNaTemu (int? id)
        {
            if (id == null)
            {
                ViewBag.Greska = "Nepravilan zahtev";
                return PartialView();
            }
            Tema t1 = db.Teme.FirstOrDefault(t=> t.TemaId == id);
            if (t1 == null)
            {
                ViewBag.Greska = "Ne postoji tema.";
                return PartialView();
            }
            ViewBag.NazivTeme = t1.Naziv;
            ViewBag.TemaId = t1.TemaId;
            return PartialView(db.Teme.Find(id).Postovi);
        }
        public PartialViewResult _ImeISlika(string id, int postId)
        {
            var korisnik = kontekst.Users.FirstOrDefault(k => k.Id == id);
            if (korisnik == null)
            {
                ViewBag.Greska = "Nije pronadjen korisnik";
            }
            ViewBag.PostId = postId;
            return PartialView(korisnik);
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