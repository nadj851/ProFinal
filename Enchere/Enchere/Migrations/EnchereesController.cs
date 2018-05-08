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

namespace Enchere.Migrations
{
    public class EnchereesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

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
            var ObjetId = Convert.ToInt32(Request.QueryString["id"]);
            
            var enchere = new Enchere.Models.Encheree();
            enchere.UserId = UserId;
            enchere.ObjetId = ObjetId;
            enchere.Message = en.Message;
            enchere.enchereNiveau = en.enchereNiveau;
            enchere.enchereDate = DateTime.Now;
            enchere.niveauMax = checkNiveauMax(en.enchereNiveau);

            db.Encherees.Add(enchere);
            db.SaveChanges();
            ViewBag.Result = "Envoyer avec success";

            return View();
        }

        private double checkNiveauMax(double enchereNiveau)
        {
            var ObjetId = (int)Session["ObjetId"];
            // var check = db.Encherees.Where(a => a.ObjetId == ObjetId).ToList();
            double max = db.Encherees.Where(a => a.ObjetId == ObjetId).Max(m => m.niveauMax);

            return 0.1;
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
