using Enchere.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace Enchere.Controllers
{
    [Authorize(Roles= "Admin")]
    public class ApplicationUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ApplicationUsers
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: ApplicationUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // GET: ApplicationUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ApplicationUsers/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Civilite,Prenom,UserType,Langue,Tel,Adresse,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(applicationUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(applicationUser);
        }

        // GET: ApplicationUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: ApplicationUsers/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Civilite,Prenom,UserType,Langue,Tel,Adresse,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(applicationUser);
        }

        // GET: ApplicationUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: ApplicationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            db.Users.Remove(applicationUser);
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




        [Authorize]
        //[Authorize(Roles = "Vendeur,Admin")]
        public ActionResult listObjetVendu(string id)
        {
            var UserId = db.Users.Find(id).Id;
            ViewBag.Vendeur = db.Users.Find(id).UserName;

            var encheres = from obj in db.Encherees
                           join objet in db.Objets
                           on obj.ObjetId equals objet.Id
                           where objet.UserId == UserId
                           select obj;

            var groupe = from o in encheres
                         group o by o.objet.objetNom
                         into gr
                         select new ObjetViewModel
                         {
                             ObjetNom = gr.Key,
                             Items = gr
                         };
            return View(groupe.ToList());
        }

        [Authorize]
        //[Authorize(Roles = "Vendeur,Admin")]
        public ActionResult desactiver(string id)
        {
            var UserId = db.Users.Find(id).Id;
            //var UserId = User.Identity.GetUserId();
            var u = db.Users.Where(a => a.Id == UserId).Single();
            Boolean etat = u.LockoutEnabled;
            //pour desactiver un compte
            //https://stackoverflow.com/questions/30452104/mvc-5-identity-2-0-lockout-doesnt-work
            //Il faut mettre a jour la colone lockout enable et donner une date jusqua quand ce sera bloquer sinon sa ne marchera pas
            if (etat)
            {
                u.LockoutEnabled = false;
                u.LockoutEndDateUtc = null;
            }
            else
            {
                u.LockoutEnabled = true;
                u.LockoutEndDateUtc = new DateTime(DateTime.Now.AddDays(7).Ticks);
            }
            db.SaveChanges();
            return RedirectToAction("Index", "ApplicationUsers");
           
        }


    }
}
