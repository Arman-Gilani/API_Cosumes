namespace API_Cosumes.Models
{
    public class LOC_StateModel
    {
        public int StateID { get; set; }
        public string StateName { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
    public class LOC_StateDropDownModel
    {
        public int StateID { get; set; }
        public string StateName { get; set; }
    }
}
