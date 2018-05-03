using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using WebApplication1.Models;

[assembly: OwinStartupAttribute(typeof(WebApplication1.Startup))]
namespace WebApplication1
{
    public partial class Startup
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateDefaultRolesAndUsers();

        }


        public void CreateDefaultRolesAndUsers()
        {
           var roleManager=new RoleManager<IdentityRole> (new RoleStore<IdentityRole>(db));
            var userManager =new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            IdentityRole role = new IdentityRole();
            if (!roleManager.RoleExists("Admin"))
            {
                role.Name = "Admin";
                roleManager.Create(role);
                ApplicationUser user = new ApplicationUser();
                user.UserName = "admin";
                user.Email = "admin@admin.com";
                user.Civilite = "Monsieur";
                user.Prenom = "admin";
                user.UserType = "Admin";
                user.Langue = "Anglais";
                user.Tel = "5147582310";
                user.Adresse = "canada";
                var check = userManager.Create(user,"Web21*");

                if (check.Succeeded)
                {
                    userManager.AddToRole(user.Id,"Admin");
                }

            }
        }


    }
}
