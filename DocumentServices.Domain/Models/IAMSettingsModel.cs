namespace DocumentService.Models
{
    public class IAMSettings
    {
        public string Url { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }

    public class AuthConfiguration
    {
        public string GrantType { get; set; }
    }

}
