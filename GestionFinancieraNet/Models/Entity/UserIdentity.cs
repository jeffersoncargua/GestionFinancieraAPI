using Microsoft.AspNetCore.Identity;

namespace GestionFinancieraNet.Models.Entity
{
    public class UserIdentity : IdentityUser
    {
        public required string Phone { get; set; }
        public required string Location { get; set; }
    }
}
