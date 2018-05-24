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
using System.IO;
using Microsoft.AspNet.Identity;
using MoreLinq;
using System.Net.Mail;

namespace Enchere.Controllers
{
    [Authorize]
    //[Authorize(Roles = "Admin")]
    public class ObjetsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Objets
        public ActionResult Index()
        {
            var objets = db.Objets.Include(o => o.Category);
            return View(objets.ToList());
        }

        // GET: Objets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Objet objet = db.Objets.Find(id);
            if (objet == null)
            {
                return HttpNotFound();
            }
            return View(objet);
        }

        // GET: Objets/Create
        public ActionResult Create()
        {
            ViewBag.categoryId = new SelectList(db.Categories, "Id", "CategoryName");
            return View();
        }

        // POST: Objets/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Objet objet, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {

                string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                upload.SaveAs(path);
                objet.objetImage = upload.FileName;
                objet.UserId = User.Identity.GetUserId();
                objet.objetDateInsc = DateTime.Now;
                db.Objets.Add(objet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.categoryId = new SelectList(db.Categories, "Id", "CategoryName", objet.categoryId);
            return View(objet);
        }

        // GET: Objets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Objet objet = db.Objets.Find(id);
            if (objet == null)
            {
                return HttpNotFound();
            }
            ViewBag.categoryId = new SelectList(db.Categories, "Id", "CategoryName", objet.categoryId);
            return View(objet);
        }

        // POST: Objets/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Objet objet, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string oldPath = Path.Combine(Server.MapPath("~/Uploads"), objet.objetImage);


                if (upload != null)
                {
                    System.IO.File.Delete(oldPath);
                    string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                    upload.SaveAs(path);
                    objet.objetImage = upload.FileName;

                }


                db.Entry(objet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categoryId = new SelectList(db.Categories, "Id", "CategoryName", objet.categoryId);
            return View(objet);
        }

        // GET: Objets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Objet objet = db.Objets.Find(id);
            if (objet == null)
            {
                return HttpNotFound();
            }
            return View(objet);
        }

        // POST: Objets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Objet objet = db.Objets.Find(id);
            db.Objets.Remove(objet);
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
        // Rapport #2 
        //Liste des objets vendus par un membre incluant l’information sur le prix
        //de départ et le prix final.       

        //[Authorize(Roles = "Vendeur,Admin")]
        public ActionResult GetListeObjetVenduWithPrix()
        {
            var groupe = (from fuss in db.Objets
                          join ench in db.Encherees
                          on fuss.Id equals ench.ObjetId
                          select new { fuss, ench });

            var liste = from o in groupe
                        group o by new { o.fuss.User.UserName, o.fuss.Id }

                          into gr
                        select new RaportObjetVenduPrixModel
                        {
                            MembreVendeur = gr.FirstOrDefault().fuss.User.UserName,
                            ObjetName = gr.Select(a => a.fuss.objetNom).FirstOrDefault(),
                            ObjetCategorie = gr.Select(a => a.fuss.Category.CategoryName).FirstOrDefault(),
                            PrixInitial = gr.Select(a => a.fuss.objetPrixDepart).FirstOrDefault(),
                            PrixFinal = gr.Select(a => a.ench.niveauMax).Max()

                        };
            return View(liste.ToList());
        }

        //objets vendu par chaque membre
        [Authorize]
        public ActionResult GetObjetmise()
        {
            var UserId = User.Identity.GetUserId();

            var objets = db.Objets.Where(a => a.UserId == UserId);
            return View(objets.ToList());
        }


        //objets vendu par chaque membre
        [Authorize]
        public ActionResult GetObjetAchte()
        {
            var UserId = User.Identity.GetUserId();           

            if (db.Encherees.Where(a => a.UserId == UserId).Any())
            {
                var objets = db.Encherees.GroupBy(l => l.ObjetId).
                    Select(g => g.OrderByDescending(c => c.UserId).FirstOrDefault()).
                    Where(u => u.UserId == UserId);
                
                return View(objets.ToList());
                //var objets = db.Encherees.Where(a => a.UserId == UserId);
                //return View(objets.DistinctBy(a => a.ObjetId).ToList());
            }
            else{
                return View();

            }


           
        }

  // GET: Evaluations/Create
        public ActionResult CreateEval(int objetId)
        {
             Session["ObjetId"]= objetId;
            var ObjetId = (int)Session["ObjetId"];

            var obj = db.Objets.Where(a => a.Id == objetId).Single();
            ViewBag.Vendeur = obj.User.UserName;

            ViewBag.Name = User.Identity.Name;
            //ViewBag.UserId = new SelectList(db.Users, "Id", "Civilite");
            return View(); 

        }



        // POST: Evaluations/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEval([Bind(Include = "Id,DateEvaluation,Cote,Commentaire,Vendeur,UserId")] Evaluation evaluation)
        {
            if (ModelState.IsValid)
            {
                evaluation.UserId = User.Identity.GetUserId();
                evaluation.DateEvaluation = DateTime.Now;

                var ObjetId = (int)Session["ObjetId"];

                var obj = db.Objets.Where(a => a.Id == ObjetId).Single();
                evaluation.Vendeur = obj.User.UserName;

                //check sum cote
                double total = 0;

                //On regarde si il y a une evaluation existante pour l'utilisateur donné
                if (db.Evaluations.Where(a => a.Vendeur == obj.User.UserName).Any())
                {
                    total = (from eval in db.Evaluations

                             where eval.Vendeur == obj.User.UserName
                             select eval.Cote).Sum();

                }

                evaluation.TotalCote = total + evaluation.Cote;

                //send notification


                ViewBag.total = evaluation.TotalCote;
                db.Evaluations.Add(evaluation);
                db.SaveChanges();
                if (evaluation.TotalCote <= -6)
                {
                    envoiMail(obj.User);
                }
                if (evaluation.TotalCote <= -9)
                {
                    var UserId = User.Identity.GetUserId();
                    var u = db.Users.Where(a => a.Id == obj.User.Id).Single();

                    //pour desactiver un compte
                    //https://stackoverflow.com/questions/30452104/mvc-5-identity-2-0-lockout-doesnt-work
                    //Il faut mettre a jour la colone lockout enable et donner une date jusqua quand ce sera bloquer sinon sa ne marchera pas
                    u.LockoutEnabled = true;
                    u.LockoutEndDateUtc = new DateTime(DateTime.Now.AddDays(7).Ticks);



                }

                return RedirectToAction("Index","Evaluations");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Civilite", evaluation.UserId);
            return View(evaluation);
        }


        private void envoiMail(ApplicationUser user)
        {
            SmtpClient SmtpServer = new SmtpClient("smtp.live.com");
            var mail = new MailMessage();
            //recherche de la cote actuelle
            var coteUser = db.Evaluations.OrderByDescending(p => p.Id).FirstOrDefault().TotalCote;
            string body = "";

            //Condition pour envoie du courriel dans la langue du membre
            if (user.Langue == "Anglais")
            {
                body = "Sender name: " + "admin" + "<br>" +
                "Sender Email: " + "munarela@hotmail.com" + "<br>" +
                "Subject message: " + "critical rating " + "<br>" +
                "Message for " + user.UserName + ": <b>We inform you that your rating has reached the critical threshold of " + coteUser;

            }
            else
            {
                body = "Nom expéditeur: " + "admin" + "<br>" +
                "email expéditeur: " + "munarela@hotmail.com" + "<br>" +
                "objet de message: " + "Cote critique " + "<br>" +
                "le message pour " + user.UserName + ": <b>Nous vous informons que votre cote à atteint le seuil critique de " + coteUser;

            }
            mail.From = new MailAddress("munarela@hotmail.com");
            mail.To.Add(user.Email);
            mail.Subject = "Urgent";
            mail.IsBodyHtml = true;
            //le message du body
           
            mail.Body = body;

            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new NetworkCredential("munarela@hotmail.com", "Web123456");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);



        }

        [HttpPost]
        public ActionResult UploadFiles(IEnumerable<HttpPostedFileBase> files)
        {
            foreach (var file in files)
            {
                string filePath = Guid.NewGuid() + Path.GetExtension(file.FileName);
                file.SaveAs(Path.Combine(Server.MapPath("~/Uploads"), filePath));
                //Here you can write code for save this information in your database if you want
            }
            return Json("file uploaded successfully");
        }




    }
}
