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
using System.Net.Mail;

using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Security;
using WebApplication1;
using Microsoft.Ajax.Utilities;
using static Enchere.Controllers.EvaluationsController;

namespace Enchere.Controllers
{

    [Authorize]
    public class EvaluationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Evaluations
        public ActionResult Index()
        {           
            var ObjetId = (int)Session["ObjetId"];
            var obj = db.Objets.Where(a => a.Id == ObjetId).Single();
              var evaluations = db.Evaluations.Where(a => a.Vendeur == obj.User.UserName);                    
           
            return View(evaluations.ToList());
        }

        // GET: Evaluations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evaluation evaluation = db.Evaluations.Find(id);
            if (evaluation == null)
            {
                return HttpNotFound();
            }
            return View(evaluation);
        }

        // GET: Evaluations/Create
        public ActionResult Create()
        {
            var ObjetId = (int)Session["ObjetId"];

            var obj = db.Objets.Where(a => a.Id == ObjetId).Single();
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
        public ActionResult Create([Bind(Include = "Id,DateEvaluation,Cote,Commentaire,Vendeur,UserId")] Evaluation evaluation)
        {
            if (ModelState.IsValid)
            {
                evaluation.UserId= User.Identity.GetUserId();
                evaluation.DateEvaluation = DateTime.Now;

                var ObjetId = (int)Session["ObjetId"];

                var obj = db.Objets.Where(a => a.Id == ObjetId).Single();
                   evaluation.Vendeur = obj.User.UserName;

                //check sum cote
                double total=0;

                //On regarde si il y a une evaluation existante pour l'utilisateur donné
                if (db.Evaluations.Where(a => a.Vendeur == obj.User.UserName).Any())
                {
                    total = (from eval in db.Evaluations

                             where eval.Vendeur == obj.User.UserName
                             select eval.Cote).Sum();

                }

                evaluation.TotalCote = total+evaluation.Cote;

                //send notification
              

                ViewBag.total= evaluation.TotalCote;
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

                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Civilite", evaluation.UserId);
            return View(evaluation);
        }



        private void envoiMail(ApplicationUser user)
        {
            SmtpClient SmtpServer = new SmtpClient("smtp.live.com");
            var mail = new MailMessage();
            //recherche de la cote actuelle
            var coteUser =  db.Evaluations.OrderByDescending(p => p.Id).FirstOrDefault().TotalCote;

            mail.From = new MailAddress("munarela@hotmail.com");
            mail.To.Add(user.Email);
            mail.Subject = "Urgent";
            mail.IsBodyHtml = true;
            //le message du body
            string body = "Nom expéditeur: " + "admin" + "<br>" +
                "email expéditeur: " + "munarela@hotmail.com" + "<br>" +
                "objet de message: " + "Cote critique "+ "<br>" +
                "le message pour "+user.UserName +": <b>Nous vous informons que votre cote à atteint le seuil critique de "+coteUser ;

            mail.Body = body;

            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new NetworkCredential("munarela@hotmail.com", "Web123456");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);



        }

        // GET: Evaluations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evaluation evaluation = db.Evaluations.Find(id);
            if (evaluation == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Civilite", evaluation.UserId);
            return View(evaluation);
        }

        // POST: Evaluations/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DateEvaluation,Cote,Commentaire,Vendeur,UserId")] Evaluation evaluation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evaluation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Civilite", evaluation.UserId);
            return View(evaluation);
        }

        // GET: Evaluations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evaluation evaluation = db.Evaluations.Find(id);
            if (evaluation == null)
            {
                return HttpNotFound();
            }
            return View(evaluation);
        }

        // POST: Evaluations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Evaluation evaluation = db.Evaluations.Find(id);
            db.Evaluations.Remove(evaluation);
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



        // Rapport #4 
        //Cote de popularité des membres incluant le niveau de la cote, le nombre d’évaluations.
        

                [Authorize]
        public ActionResult GetListeEvalMembre()
        {
           
            var group = (from gr in db.Evaluations
                       
                            group gr by  gr.Vendeur).Select(ac => new RaportModelEval
                            {

                                Membre = ac.FirstOrDefault().Vendeur,
                                NbrEval = ac.Count(),
                                CotePo = ac.Sum(acs => acs.Cote)
                              
                            });


            return View(group.ToList());
        }




    }
}
