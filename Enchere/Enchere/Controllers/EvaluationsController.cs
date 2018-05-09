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

namespace Enchere.Controllers
{
    public class EvaluationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Evaluations
        public ActionResult Index()
        {
            var evaluations = db.Evaluations.Include(e => e.User);
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
                var total = (from eval in db.Evaluations

                             where eval.Vendeur == obj.User.UserName
                             select eval.Cote).Sum();

                evaluation.TotalCote = total+evaluation.Cote;

                //send notification
                if (evaluation.TotalCote == -6)
                {
                    envoiMail(obj.User);
                }
                else if(evaluation.TotalCote < -6 )
                {
                    // Unlock the user account
                    var UserId = User.Identity.GetUserId();
                    var u = db.Users.Where(a => a.Id == obj.User.Id).Single();
                     u.LockoutEnabled = false;

                }

                ViewBag.total= evaluation.TotalCote;
                db.Evaluations.Add(evaluation);
                db.SaveChanges();                                       
                             
                
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Civilite", evaluation.UserId);
            return View(evaluation);
        }



        private void envoiMail(ApplicationUser user)
        {
            SmtpClient SmtpServer = new SmtpClient("smtp.live.com");
            var mail = new MailMessage();
            mail.From = new MailAddress(user.Email);
            mail.To.Add(user.Email);
            mail.Subject = "Urgent";
            mail.IsBodyHtml = true;
            //le message du body
            string body = "Nom expéditeur: " + "admin" + "<br>" +
                "email expéditeur: " + "munarela@hotmail.com" + "<br>" +
                "objet de message: " + "Urgent cote "+ "<br>" +
                "le message : <b>" + "votre cote est atteindre -6 faite attention svp " + "</b>";

            mail.Body = body;

            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential(user.Email, "Web123456");
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





    }
}
