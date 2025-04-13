namespace API_Cosumes.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public int StateID { get; set; }
        public string? StateName { get; set; }
        public int CityID { get; set; }
        public string? CityName { get; set; }
    }
}
