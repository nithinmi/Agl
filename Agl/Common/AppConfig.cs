using System.Configuration;

namespace Agl.Common
{
    public class AppConfig : IAppConfig
    {
        public string AglApiUrl
        {
            get { return ConfigurationManager.AppSettings["AglApiUrl"]; }
        }
    }
}
