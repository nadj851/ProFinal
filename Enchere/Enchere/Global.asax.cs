using System;
using System.Collections.Generic;
using System.Linq;
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
            aTimer.Interval = 5000;
            //aTimer.Interval = 60000;
            aTimer.Enabled = true;
        }



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
            //            Controllers.AccountController.EnvoiMessage(item.User.Email, body, subject);
            //        }
            //    }

            //}


        }


    }
}
