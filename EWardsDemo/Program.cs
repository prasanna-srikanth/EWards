using EWardsApiClient;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace EWardsDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
            .AddEwardsApiClientLibrary()
            .BuildServiceProvider();

            dynamic message = new JObject();
            message.CustomerKey = "Foo";
            message.MerchantId = "bar";
            message.CustomerMobile = "123456789";
            message.BillAmount = "100";

            var ewardsApiClientService = serviceProvider.GetService<IEwardsApiClientService>();
            var response = await ewardsApiClientService.CheckCustomer(message);
            Console.WriteLine(response);

        }
    }
}
