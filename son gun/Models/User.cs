using Microsoft.AspNetCore.Identity;

namespace son_gun.Models
{
    public class User:IdentityUser
    {
        public string Name{ get; set; }
        public string Surname { get; set; }
    }
}
