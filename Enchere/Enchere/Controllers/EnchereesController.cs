using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Enchere.Models;
using WebApplication1.Models;
using Microsoft.AspNet.Identity;
using MoreLinq;

namespace Enchere.Migrations
{
    public class EnchereesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private Encheree newEnchere;
        // GET: Encherees
        public ActionResult Index()
        {
            var encherees = db.Encherees.Include(e => e.objet).Include(e => e.user);
            return View(encherees.ToList());
        }

        // GET: Encherees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Encheree encheree = db.Encherees.Find(id);
            if (encheree == null)
            {
                return HttpNotFound();
            }
            return View(encheree);
        }

        [Authorize]
        public ActionResult Appliquer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Appliquer(Enchere.Models.Encheree en)
        {
            var UserId = User.Identity.GetUserId();
            var ObjetId = (int)Session["ObjetId"];
            double niveauActuel=0;

            if (db.Encherees.Where(a => a.ObjetId == ObjetId).Any())
            {
                niveauActuel = db.Encherees.Where(a => a.ObjetId == ObjetId).ToList().Max(m => m.enchereNiveau);
            }
            

            if (en.enchereNiveau > niveauActuel)
            {
                var enchere = checkEnchere(en, niveauActuel);
                enchere.UserId = UserId;
                enchere.ObjetId = ObjetId;
                enchere.Message = en.Message;
                enchere.enchereNiveau = en.enchereNiveau;
                enchere.enchereDate = DateTime.Now;
                enchere.niveauMax = en.niveauMax;


                db.Encherees.Add(enchere);
                TempData["Result"] = "Bravo! Vous venez de surrenchérir!!";
                db.SaveChanges();

                if (newEnchere != null)
                {
                    newEnchere.ObjetId = ObjetId;
                    newEnchere.enchereDate = DateTime.Now;
                    newEnchere.Message = "Surrencherissement automatique";

                    db.Encherees.Add(newEnchere);
                    TempData["Result"] = "Bien Esseyé sauf qu'il va falloir surrenchérir pour gagner!!";
                }

                db.SaveChanges();


                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.Result = "Le montant que vous proposé est inférieur ou égal au niveau actuel qui est de " + niveauActuel + " $.";
                return View();
            }


        }


        private Encheree checkEnchere(Encheree enchereActuelle, double niveauActuel)
        {
            var ObjetId = (int)Session["ObjetId"];
            newEnchere = null;

            if (db.Encherees.Where(a => a.ObjetId == ObjetId).Any())
            {
                var enchereMax = db.Encherees.Where(a => a.ObjetId == ObjetId).ToList().MaxBy(m => m.niveauMax);

                if (enchereActuelle.enchereNiveau >= enchereMax.niveauMax)
                {
                    enchereActuelle.niveauMax = enchereActuelle.enchereNiveau;
                    if(enchereActuelle.enchereNiveau != enchereMax.niveauMax)
                    {
                    enchereActuelle.enchereNiveau = enchereMax.niveauMax + 1;
                    }

                    //notifier User ici par email
                    var idUser = enchereMax.UserId;

                    return enchereActuelle;

                }
                else
                {
                    //Des tests doivent encore etre faits
                    enchereActuelle.niveauMax = enchereMax.niveauMax;

                    newEnchere = new Encheree();
                    newEnchere.niveauMax = enchereMax.niveauMax;
                    newEnchere.enchereNiveau = enchereActuelle.enchereNiveau + 1;
                    newEnchere.UserId = enchereMax.UserId;
                    return enchereActuelle;

                }

            }
            else
            {
                enchereActuelle.niveauMax = enchereActuelle.enchereNiveau;
                return enchereActuelle;
            }           

        }

        private double checkNiveauMax(double enchereNiveau)
        {
            var ObjetId = (int)Session["ObjetId"];

            if (db.Encherees.Where(a => a.ObjetId == ObjetId).Any())
            {
                var max = db.Encherees.Where(a => a.ObjetId == ObjetId).ToList().MaxBy(m => m.niveauMax);


            }


            return enchereNiveau;
        }
        // GET: Encherees/Create
        public ActionResult Create()
        {
            ViewBag.ObjetId = new SelectList(db.Objets, "Id", "objetNom");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Civilite");
            return View();
        }

        // POST: Encherees/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,enchereNiveau,Message,niveauMax,enchereDate,ObjetId,UserId")] Encheree encheree)
        {
            if (ModelState.IsValid)
            {
                db.Encherees.Add(encheree);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ObjetId = new SelectList(db.Objets, "Id", "objetNom", encheree.ObjetId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Civilite", encheree.UserId);
            return View(encheree);
        }

        // GET: Encherees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Encheree encheree = db.Encherees.Find(id);
            if (encheree == null)
            {
                return HttpNotFound();
            }
            ViewBag.ObjetId = new SelectList(db.Objets, "Id", "objetNom", encheree.ObjetId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Civilite", encheree.UserId);
            return View(encheree);
        }

        // POST: Encherees/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,enchereNiveau,Message,niveauMax,enchereDate,ObjetId,UserId")] Encheree encheree)
        {
            if (ModelState.IsValid)
            {
                db.Entry(encheree).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ObjetId = new SelectList(db.Objets, "Id", "objetNom", encheree.ObjetId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Civilite", encheree.UserId);
            return View(encheree);
        }

        // GET: Encherees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Encheree encheree = db.Encherees.Find(id);
            if (encheree == null)
            {
                return HttpNotFound();
            }
            return View(encheree);
        }

        // POST: Encherees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Encheree encheree = db.Encherees.Find(id);
            db.Encherees.Remove(encheree);
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
