using Agl;
using Agl.Common;
using Agl.Service;
using Agl.Service.Contract;
using Microsoft.Practices.Unity;
using RestSharp;

namespace Cats
{
    class Program
    {
        private static IUnityContainer _container;
        static void Main(string[] args)
        {
            LoadContaier();
            var program = _container.Resolve<Application>();
            program.Run();
        }

        public static void LoadContaier()
        {
            _container = new UnityContainer();
            _container.RegisterType<IRestClient, RestClient>("RestClient", new InjectionConstructor());
            _container.RegisterType<IAppConfig, AppConfig>("AppConfig");
            _container.RegisterType<IAgl, AglService>("AglService", 
                new InjectionConstructor(_container.Resolve<IAppConfig>("AppConfig"), _container.Resolve<IRestClient>("RestClient")));
        }
    }
}
