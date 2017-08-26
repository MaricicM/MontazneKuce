using Microsoft.Owin;
using Owin;
using MontazneKuce.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

[assembly: OwinStartupAttribute(typeof(MontazneKuce.Startup))]
namespace MontazneKuce
{
    public partial class Startup
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            if (KreirajRoluAdmin())
            {
                KreirajAdmin1();
            }
            KreirajRoluKorisnik();
        }

        public bool KreirajRoluAdmin()
        {
            var roleStore = new RoleStore<IdentityRole>(db);
            var rm = new RoleManager<IdentityRole>(roleStore);
            if (!rm.RoleExists("admin"))
            {
                var rola = new IdentityRole { Name = "admin" };
                var rezultat = rm.Create(rola);
                if (rezultat.Succeeded)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public void KreirajRoluKorisnik()
        {
            var roleStore = new RoleStore<IdentityRole>(db);
            var rm = new RoleManager<IdentityRole>(roleStore);
            if (!rm.RoleExists("korisnik"))
            {
                var rola = new IdentityRole { Name = "korisnik" };
                var rezultat = rm.Create(rola);
            }
        }

        public void KreirajAdmin1()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var userStore = new UserStore<ApplicationUser>(db);
            var um = new UserManager<ApplicationUser>(userStore);

            um.PasswordValidator = new PasswordValidator { RequiredLength = 3 };
            var korisnik = new ApplicationUser { Ime = "Pera", Prezime = "Peric", UserName = "admin1" };
            var rezultat = um.Create(korisnik, "123");
            if (rezultat.Succeeded)
            {
                um.AddToRole(korisnik.Id, "admin");
            }
        }
    }
}
