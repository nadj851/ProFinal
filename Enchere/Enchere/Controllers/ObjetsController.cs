﻿using System;
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
        public ActionResult Create(Objet objet,HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {

                string path = Path.Combine(Server.MapPath("~/Uploads"),upload.FileName);
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
        public ActionResult Edit(Objet objet,HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string oldPath = Path.Combine(Server.MapPath("~/Uploads"), objet.objetImage);
               

                if (upload!=null)
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

    }
}
