namespace Nabd.Core.Settings
{
   
    public class FrontendSettings
    {
        public string BaseUrl { get; set; } = string.Empty;

       
        public string PaymentSuccessUrl { get; set; } = "/booking/success";
        public string PaymentFailedUrl { get; set; } = "/booking/failed";
    }
}