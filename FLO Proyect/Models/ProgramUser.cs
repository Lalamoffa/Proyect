using Microsoft.AspNetCore.Identity;

namespace FLO_Proyect.Models
{
    public class ProgramUser: IdentityUser
    {
      
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
    }
}
