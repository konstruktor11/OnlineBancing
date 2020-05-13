using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace OnlineBanking.Models
{
    // Чтобы добавить данные профиля для пользователя, можно добавить дополнительные свойства в класс ApplicationUser. Дополнительные сведения см. по адресу: http://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public string KlName { get; set; }
        public string KlSurname { get; set;}
        public string KlAddress { get; set; }
        public string KlPassportNum { get; set; }
        public string KlPhone { get; set; }
        public decimal KlBalance { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            var istClaim = Claims.FirstOrDefault(c => c.ClaimType == "Istoria");
            if (istClaim != null)
                userIdentity.AddClaim(new Claim(istClaim.ClaimType, istClaim.ClaimValue));

            //userIdentity.AddClaim(new Claim("Istoria", this.Istor.ToString()));
            return userIdentity;
        }
        public string Istor { get; set; }

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
    }
}