namespace IndustryRegApi.Models
{
    public class IndustryFileDetails
    {
        public int SLNO { get; set; } 
        public string UNITID { get; set; }
        public string INVESTORID { get; set; }
        public string FILETYPE { get; set; }
        public string FILEPATH { get; set; }
        public string FILENAME { get; set; }
        public string FILEDESCRIPTION { get; set; }
        public string DEPTID { get; set; }
        public string APPROVALID { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DT { get; set; }
        public string MODIFIED_BY { get; set; }
        public DateTime? MODIFIED_DT { get; set; } 
        public string IRQID { get; set; }
        public string RESPONSEFILE_BY { get; set; }
        public string DPRVERIFICATION_STATUS { get; set; }
        public string DPRVERIFICATION_BY { get; set; }
        public DateTime? DPRVERIFICATION_DATE { get; set; }
        public string DPRVERIFICATION_BYIP { get; set; }
        public IFormFile file { get; set; }
    }
}
