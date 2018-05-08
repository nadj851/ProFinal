using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using Enchere.Models;

namespace WebApplication1.Models
{
    // Vous pouvez ajouter des données de profil pour l'utilisateur en ajoutant plus de propriétés à votre classe ApplicationUser ; consultez http://go.microsoft.com/fwlink/?LinkID=317594 pour en savoir davantage.
    public class ApplicationUser : IdentityUser
    {

        public string Civilite { get; set; }
        public string Prenom { get; set; }
        public string UserType { get; set; }
        public string Langue { get; set; }
        public string Tel { get; set; }
        public string Adresse { get; set; }
        public virtual ICollection <Objet> objets { get; set; }
                                           // public DateTime DateIns { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Notez qu'authenticationType doit correspondre à l'élément défini dans CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Ajouter les revendications personnalisées de l’utilisateur ici
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Enchere.Models.Categorie> Categories { get; set; }

        public System.Data.Entity.DbSet<Enchere.Models.Objet> Objets { get; set; }

        public System.Data.Entity.DbSet<Enchere.Models.Encheree> Encherees { get; set; }

        public System.Data.Entity.DbSet<Enchere.Models.Evaluation> Evaluations { get; set; }

        
    }
}