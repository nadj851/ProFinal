using Enchere.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using CaptchaMvc.HtmlHelpers;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
           
            return View(db.Categories.ToList());
        }


        public ActionResult Details(int objetId)
        {
            var objet = db.Objets.Find(objetId);
            if(objet==null)
            {
                return HttpNotFound();
            }

            Session["ObjetId"] = objetId;
            return View(objet); 
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
            var ObjetId =(int) Session["ObjetId"];
            var check = db.Encherees.Where(a => a.ObjetId == ObjetId && a.UserId == UserId).ToList();

            //if (check.Count<1)
            //{
            var enchere = new Enchere.Models.Encheree();
            enchere.UserId = UserId;
            enchere.ObjetId = ObjetId;
            enchere.Message =en.Message;
            enchere.enchereNiveau = en.enchereNiveau;
            enchere.enchereDate = DateTime.Now;

            db.Encherees.Add(enchere);
            db.SaveChanges();
                ViewBag.Result = "Envoyer avec success";
            //}
            //else
            //{
            //    ViewBag.Result = "deja participer deja pour cette objet désolé";
            //}
            return View();
        }

        [Authorize]
        public ActionResult GetObjetByUser()
        {
            var UserId = User.Identity.GetUserId();

            var objets = db.Encherees.Where(a => a.UserId == UserId);
            return View(objets.ToList());
        }

        [Authorize]
        public ActionResult DetailEnchere(int id)
        {
            var enchere = db.Encherees.Find(id);
            if (enchere == null)
            {
                return HttpNotFound();
            }

            
            return View(enchere);
        }




        // GET: Roles/Edit/5
        public ActionResult Edit(int id)
        {
            var enchere = db.Encherees.Find(id);
            if (enchere == null)
            {
                return HttpNotFound();
            }
            return View(enchere);
        }

        // POST: Roles/Edit/5
        [HttpPost]
        public ActionResult Edit(Enchere.Models.Encheree enchere)
        {

            // TODO: Add update logic here

            if (ModelState.IsValid)
            {
                db.Entry(enchere).State = EntityState.Modified;
                enchere.enchereDate = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("GetObjetByUser");

            }

            return View(enchere);
        }


    
        public ActionResult Delete(int id)
        {
            var enchere= db.Encherees.Find(id);
            if (enchere == null)
            {
                return HttpNotFound();
            }
            return View(enchere);
        }

        
        [HttpPost]
        public ActionResult Delete(Encheree ench)
        {
            // TODO: Add delete logic here
            var en =  db.Encherees.Find(ench.Id);
            db.Encherees.Remove(en);
            db.SaveChanges();
            return RedirectToAction("GetObjetByUser");

        }

        [Authorize]
        //[Authorize(Roles = "Vendeur,Admin")]
        public ActionResult GetObjetByVendeur()
        {
            var UserId = User.Identity.GetUserId();

            var encheres = from obj in db.Encherees
                         join objet in db.Objets
                         on obj.ObjetId equals objet.Id
                         where objet.UserId==UserId
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



        



        public ActionResult About()
        {
            ViewBag.Message = "Munarela Enchere vous souhaite la bienvenue.";

            return View();
        }


        [HttpGet]
        public ActionResult Contact()
        {


            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactModel contact)
        {
            if (!this.IsCaptchaValid("Validate your captcha")||!ModelState.IsValid)
            {
                ViewBag.ErrMessage = "Entrez la bonne reponse";
                return View();
            }
            else {
             //version fonctionne le 08/05/2018    
            //comment faire pour envoyer un message à partir de la boite email de l'administrateur munarela@hotmail.com
            SmtpClient SmtpServer = new SmtpClient("smtp.live.com");
            var mail = new MailMessage();
            mail.From = new MailAddress("munarela@hotmail.com");
            mail.To.Add("munarela@hotmail.com");
            mail.Subject = contact.Subject;
            mail.IsBodyHtml = true;
            //le message du body
            string body = "Nom expéditeur: " + contact.Nom + "<br>" +
                "email expéditeur: " + contact.Email + "<br>" +
                "objet de message: " + contact.Subject + "<br>" +
                "le message : <b>" + contact.Message + "</b>";

            mail.Body = body;
                      
            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("munarela@hotmail.com", "Web123456");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);

            
            return RedirectToAction("Index");
            }
        }


        public ActionResult Recherche()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Recherche(string searchName)
        {
            var result = db.Objets.Where(a => a.objetNom.Contains(searchName)
              || a.objetDescription.Contains(searchName)
              || a.Category.CategoryName.Contains(searchName)
              || a.Category.CategoryDescription.Contains(searchName)).ToList();

            return View(result);
        }


        

    }
}
/*commentaire de l'ancien envoi de message qui ne fonctionnait pas*/
//try
//{
//    var mail = new MailMessage();
//var loginInfo = new NetworkCredential("munarela@hotmail.com", "Web123456");
//mail.From = new MailAddress(contact.Email);
//mail.To.Add(new MailAddress("munarela@hotmail.com"));
//mail.Subject = contact.Subject;

//mail.IsBodyHtml = true;
//string body = "Nom expéditeur " + contact.Nom+"<br>"+
//    "email expéditeur "+contact.Email+ "<br>"+
//    "objet de message "+contact.Subject+ "<br>"+
//    "le message : <b>" + contact.Message+"</b>";

//mail.Body = body;
////465
////587
//var smtpClient = new SmtpClient("smtp.Live.com", 587);

//smtpClient.EnableSsl = true;
//smtpClient.Credentials = loginInfo;
//smtpClient.Send(mail);
//    Console.WriteLine(mail);
//}
//catch(Exception ex)
//{ Console.WriteLine(ex); }