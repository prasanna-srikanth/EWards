using Microsoft.Extensions.DependencyInjection;
using RestSharp;

namespace EWardsApiClient
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddEwardsApiClientLibrary(this IServiceCollection services)
        {
            services.AddTransient<IRestClient, RestClient>();
            services.AddTransient<IEwardsApiClientService>
                (s=> new EwardsApiClientService
                ("https://hwxr8k5ubg.execute-api.ap-south-1.amazonaws.com",
                "customercheck/merchant/posCustomerCheck", "xxx",s.GetService<IRestClient>()));
            return services;
        }
    }
}
