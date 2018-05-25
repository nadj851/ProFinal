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
            var list = db.Objets.ToList();
            foreach (var item in list)
            {
                if (!item.notifie && item.Statut == Enchere.Models.enumStatutObjet.EV && ((item.objetDateInsc.AddDays(item.objetDureeVente).Ticks - DateTime.Now.Ticks) / 10000) <= 86400000)
                {
                    if (!db.Encherees.Where(a => a.objet.Id == item.Id).Any())
                    {
                        item.notifie = true;
                        System.Diagnostics.Debug.WriteLine(" Sending Message Regarding Article: " + item.objetNom);
                        string subject = item.objetNom + " expirera bientot";
                        string body = "l'objet" + item.objetNom + " expirera bientot et il n'y a eu aucune enchère à date. Pensez à offrir un meilleur prix";
                        System.Diagnostics.Debug.WriteLine("Time: " + System.DateTime.Now.ToLongTimeString());
                        Controllers.AccountController.EnvoiMessage(item.User.Email, body, subject);
                        return;
                    }

                }

            }

            var userList = db.Users.ToList();
            foreach (var item in userList)
            {
                if (item.dernierEnvoiRapport < dateFixeEnvoiRapport|| item.dernierEnvoiRapport==null) {

                    System.Diagnostics.Debug.WriteLine("Sending email to user" + "Time: " + System.DateTime.Now.ToLongTimeString());
                    //Envoyer le rapport
                    string subject = "Rapport Mensuel pour l'utilisateur " + item.Prenom;
                    string body = "";
                    body += "Votre rapport";
                    Controllers.AccountController.EnvoiMessage(item.Email, body, subject);
                    item.dernierEnvoiRapport = dateFixeEnvoiRapport;
                    return;
                }
                
            }
        }


    }
}
