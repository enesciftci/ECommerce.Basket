namespace ECommerce.Basket.Models.InfrastuctureModels
{
    public class ECommerceDatabaseSettings : IECommerceDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IECommerceDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
