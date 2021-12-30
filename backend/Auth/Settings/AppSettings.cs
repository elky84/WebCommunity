
namespace Auth.Settings
{
    public class AppSettings : IAppSettings
    {
        public string Secret { get; set; }
    }

    public interface IAppSettings
    {
        string Secret { get; set; }
    }
}
