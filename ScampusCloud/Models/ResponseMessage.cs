namespace ScampusCloud.Models
{
    public class ResponseMessage
    {
        public bool IsSuccess { get; set; }
        public string Response_Code { get; set; }
        public string Response_Message { get; set; }
        public dynamic Response_Body { get; set; }
        public string CurrentCulture { get; set; }
        public string RedirectUrl { get; set; }
    }
}
