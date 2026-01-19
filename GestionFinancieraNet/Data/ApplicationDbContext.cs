using GestionFinancieraNet.Models.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestionFinancieraNet.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserIdentity>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {

        }

        public DbSet<UserIdentity> Users { get; set; }

        /*//protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    SeedRoles(builder);
        //}*/

        /// <summary>
        /// Este metodo permite colocar los roles que necesitara el sistema, luego de agregarlos a la base de datos, se debe comnetar
        /// para que no se vuelvan a registrar y ocasione errores a futuro
        /// </summary>
        /// <param name="builder"></param>
        /*private static void SeedRoles(ModelBuilder builder)
        //{
        //    builder.Entity<IdentityRole>().HasData(
        //        new IdentityRole()
        //        {
        //            Name = "Admin",
        //            ConcurrencyStamp = "1",
        //            NormalizedName = "ADMIN",
        //        },
        //        new IdentityRole()
        //        {
        //            Name = "Collab",
        //            ConcurrencyStamp = "2",
        //            NormalizedName = "COLLAB",
        //        },
        //        new IdentityRole()
        //        {
        //            Name = "Costumer",
        //            ConcurrencyStamp = "3",
        //            NormalizedName = "COSTUMER",
        //        }
        //    );
        //}*/
    }

}
