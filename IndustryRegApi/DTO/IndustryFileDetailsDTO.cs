using IndustryRegApi.Models;

namespace IndustryRegApi.DTO
{
    public class IndustryFileDetailsDTO:IndustryFileDetails
    {
        public string UNITID { get; set; }
        public string INVESTORID { get; set; }
        public string FILETYPE { get; set; }
        public string FILEPATH { get; set; }
        public string FILENAME { get; set; }
        public string FILEDESCRIPTION { get; set; }
    }
}
