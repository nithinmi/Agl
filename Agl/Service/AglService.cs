using System;
using System.Net;
using Agl.Common;
using Agl.Service.Contract;
using Newtonsoft.Json;
using RestSharp;

namespace Agl.Service
{
    public class AglService : IAgl
    {
        private IRestClient _restClient;
        private IAppConfig _appConfig;
        public AglService(IAppConfig appConfig, IRestClient restClient)
        {
            _restClient = restClient;
            _appConfig = appConfig;
            _restClient.BaseUrl = new Uri(_appConfig.AglApiUrl);
        }
        public T Get<T>(string resource)    
        {
            var request = new RestRequest(resource, Method.GET) { RequestFormat = DataFormat.Json };

            //var json = _restClient.Execute(request).Content;

            var restResponse = _restClient.Execute(request);
            return restResponse.StatusCode == HttpStatusCode.OK
                ? JsonConvert.DeserializeObject<T>(restResponse.Content)
                : default(T);
        }
    }
}
