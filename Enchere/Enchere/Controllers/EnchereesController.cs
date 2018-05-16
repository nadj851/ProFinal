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
        private Encheree newOffre;
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
        public ActionResult Appliquer(Enchere.Models.Encheree offreEnchere)
        {
            var UserId = User.Identity.GetUserId();
            var ObjetId = (int)Session["ObjetId"];
            Objet objet = (Objet)db.Objets.Where(a => a.Id == ObjetId).First();

            string message = "";
            double niveauActuel = 0;
            bool bonPrixDepart = false;

            Encheree derniereOffre = offreEnchere;
            DateTime dateLimite = objet.objetDateInsc.AddDays(objet.objetDureeVente);

            if (db.Encherees.Where(a => a.ObjetId == ObjetId).Any())
            {
                niveauActuel = db.Encherees.Where(a => a.ObjetId == ObjetId).ToList().Max(m => m.enchereNiveau);
                derniereOffre = db.Encherees.Where(a => a.ObjetId == ObjetId).ToList().MaxBy(m => m.enchereNiveau);
                bonPrixDepart = true;
            }
            else
            {
                if (offreEnchere.enchereNiveau > objet.objetPrixDepart)
                {
                    bonPrixDepart = true;                    
                }
            }
                        
             

            if (offreEnchere.enchereNiveau > niveauActuel && bonPrixDepart &&
                UserId != derniereOffre.UserId && dateLimite > DateTime.Now)
            {
                var offre = checkEnchere(offreEnchere, niveauActuel);
                offre.UserId = UserId;
                offre.ObjetId = ObjetId;
                offre.Message = offreEnchere.Message;
                offre.enchereNiveau = offreEnchere.enchereNiveau;
                offre.enchereDate = DateTime.Now;
                offre.niveauMax = offreEnchere.niveauMax;


                db.Encherees.Add(offre);
                message = "Bravo! Vous venez de surrenchérir!!";
                db.SaveChanges();

                if (newOffre != null)
                {
                    newOffre.ObjetId = ObjetId;
                    newOffre.enchereDate = DateTime.Now;
                    newOffre.Message = "Surrencherissement automatique";

                    db.Encherees.Add(newOffre);
                    message = "Bien Esseyé sauf qu'il va falloir surrenchérir pour gagner!!";
                }
                db.SaveChanges();

                TempData["Result"] = message;

                return RedirectToAction("Index");

            }
            else
            {
                if(derniereOffre.UserId == UserId)
                {
                    message = "Vous ne devez pas encherir.Vous détenez déjà l'enchere avec une offre de :"+ niveauActuel +"$.";
                }
                else if (dateLimite < DateTime.Now)
                {
                    message = "Erreur!! L'enchere est Expiré depuis " + dateLimite + ".";
                }
                else if (!bonPrixDepart)
                {
                    message = "L'offre que vous venez de faire est inférieure ou égale au prix initial qui est de " + objet.objetPrixDepart + "$";
                }
                else
                {
                    message = "Le montant que vous proposé est inférieur ou égal au niveau actuel qui est de " + niveauActuel + " $.";
                }
                ViewBag.Result = message;
                return View();
            }


        }


        private Encheree checkEnchere(Encheree offreActuelle, double niveauActuel)
        {
            var ObjetId = (int)Session["ObjetId"];

            newOffre = null;

            if (db.Encherees.Where(a => a.ObjetId == ObjetId).Any())
            {
                var offreMax = db.Encherees.Where(a => a.ObjetId == ObjetId).ToList().MaxBy(m => m.niveauMax);

                if (offreActuelle.enchereNiveau >= offreMax.niveauMax)
                {
                    offreActuelle.niveauMax = offreActuelle.enchereNiveau;
                    if(offreActuelle.enchereNiveau != offreMax.niveauMax)
                    {
                    offreActuelle.enchereNiveau = offreMax.niveauMax + 1;
                    }

                    //notifier User ici par email
                    var idUser = offreMax.UserId;

                    return offreActuelle;

                }
                else
                {
                    //Des tests doivent encore etre faits
                    offreActuelle.niveauMax = offreMax.niveauMax;

                    newOffre = new Encheree();
                    newOffre.niveauMax = offreMax.niveauMax;
                    newOffre.enchereNiveau = offreActuelle.enchereNiveau + 1;
                    newOffre.UserId = offreMax.UserId;
                    return offreActuelle;

                }

            }
            else
            {
                offreActuelle.niveauMax = offreActuelle.enchereNiveau;
                return offreActuelle;
            }           

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
