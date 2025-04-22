namespace HTAS2_Dev.Models
{
    public class Role
    {
        public int ID { get; set; }
        public string ROLE { get; set; }
        public string ROLE_DESCRIPTION { get; set; }
        public string REMARKS { get; set; }
        public bool IS_ACTIVE { get; set; }
        public bool IS_DELETED { get; set; }
        public string IP_ADRESS { get; set; }
        public string LAT_LONG { get; set; }
        public string APP_SOURCE { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public string MODIFIED_BY { get; set; }
        public DateTime? MODIFIED_DATE { get; set; }
    }
}

