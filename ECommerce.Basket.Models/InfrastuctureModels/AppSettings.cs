namespace ECommerce.Basket.Models.InfrastuctureModels
{
    public class AppSettings:IAppSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecurityKey { get; set; }
    }

    public interface IAppSettings
    {
        string Issuer { get; set; }
        string Audience { get; set; }
        string SecurityKey { get; set; }
    }
}
