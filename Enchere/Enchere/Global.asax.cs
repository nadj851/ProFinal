using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Timers;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApplication1.Models;

namespace WebApplication1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);



            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 300000;
            //aTimer.Interval = 60000;
            aTimer.Enabled = true;
        }

        static DateTime dateFixeEnvoiRapport = DateTime.Today;

        // Specify what you want to happen when the Elapsed event is raised.
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            //var list = db.Objets.ToList();
            //foreach (var item in list)
            //{
            //    if (!item.notifie && item.Statut == Enchere.Models.enumStatutObjet.EV && ((item.objetDateInsc.AddDays(item.objetDureeVente).Ticks - DateTime.Now.Ticks) / 10000) <= 86400000)
            //    {
            //        if (!db.Encherees.Where(a => a.objet.Id == item.Id).Any())
            //        {
            //            item.notifie = true;
            //            System.Diagnostics.Debug.WriteLine(" Sending Message Regarding Article: " + item.objetNom);
            //            string subject = item.objetNom + " expirera bientot";
            //            string body = "l'objet" + item.objetNom + " expirera bientot et il n'y a eu aucune enchère à date. Pensez à offrir un meilleur prix";
            //            System.Diagnostics.Debug.WriteLine("Time: " + System.DateTime.Now.ToLongTimeString());
            //            Controllers.AccountController.EnvoiMessage(item.User.Email, body, subject);
            //            return;
            //        }

            //    }

            //}

            var userList = db.Users.ToList();
            foreach (var item in userList)
            {
                if (item.dernierEnvoiRapport < dateFixeEnvoiRapport|| item.dernierEnvoiRapport==null) {

                    System.Diagnostics.Debug.WriteLine("Sending email to user" +item.UserName +"Time: " + System.DateTime.Now.ToLongTimeString());
                    //Envoyer le rapport
                    Boolean anglais = false;
                    if (item.Langue == "Anglais") anglais = true;
                    string subject = "Rapport Mensuel pour l'utilisateur " + item.Prenom;
                    if (anglais) subject = "your mensual report";
                    string body = "";
                    
                    body += "Votre rapport<br>";
                    if (anglais) body += "your report";
                    if (db.Encherees.Where(a => a.UserId == item.Id).Any())
                    {
                        var objets = db.Encherees.GroupBy(l => l.ObjetId).
                            Select(g => g.OrderByDescending(c => c.UserId).FirstOrDefault()).
                            Where(u => u.UserId == item.Id);

                        var listObjetAchete = objets.ToList();
                       
                        body += "Nom de l'objet   |  date de vente <br>";
                        if (anglais) body  += "Object Name  |  Sale Date <br>";
                        foreach (var itemObjet in listObjetAchete)
                        {

                            body += itemObjet.objet.objetNom + " " + itemObjet.objet.objetDateInsc.AddDays(itemObjet.objet.objetDureeVente).ToLongDateString() + "<br>";

                        }

                        //var objets = db.Encherees.Where(a => a.UserId == UserId);
                        //return View(objets.DistinctBy(a => a.ObjetId).ToList());
                    }
                    else {
                        body += "vous n'avez aucune enchère pour ce mois";
                        if(anglais) body += "No report for this month";
                    }

                   
                    Controllers.AccountController.EnvoiMessage(item.Email, body, subject);
                    item.dernierEnvoiRapport = dateFixeEnvoiRapport;
                    System.Diagnostics.Debug.WriteLine(body);
                    return;
                }
                
            }
        }


    }
}
