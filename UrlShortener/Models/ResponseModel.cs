namespace UrlShortener.Models
{
    public class ResponseModel
    {
        public string ReturnUrl { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
