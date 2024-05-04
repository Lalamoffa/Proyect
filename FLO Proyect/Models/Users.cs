namespace FLO_Proyect.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<Orders> Orders { get; set; }

    }
}
