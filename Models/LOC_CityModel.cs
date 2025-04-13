namespace API_Cosumes.Models
{
    public class LOC_CityModel
    {
        public int CityID { get; set; }
        public string CityName { get; set; }
        public int StateID { get; set; }
        public string StateName { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
    public class LOC_CityDropdownModel
    {
        public int CityID { get; set;}
        public string CityName { get; set;}
    }
}
