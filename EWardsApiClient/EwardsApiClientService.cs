using System;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace EWardsApiClient
{
    public class EwardsApiClientService : IEwardsApiClientService
    {
        private const string strApiKey = "x-api-key";
        private readonly string _baseUrl;
        private readonly string _customerCheckUrl;
        private readonly string _apiKey;
        private readonly IRestClient _restClient;

        public EwardsApiClientService(string BaseUrl, string CustomerCheckUrl,string ApiKey, IRestClient restClient)
        {
            _baseUrl = BaseUrl;
            _customerCheckUrl = CustomerCheckUrl;
            _apiKey = ApiKey;
            _restClient = restClient;
        }

        public async Task<dynamic> CheckCustomer(dynamic message)
        {
            try
            {
                _restClient.BaseUrl = new Uri(_baseUrl);

                var request = new RestRequest(_customerCheckUrl, Method.POST, DataFormat.Json);

                request.AddHeader(strApiKey, _apiKey);

                request.AddJsonBody(
                    new
                    {
                        customer_key = message.CustomerKey,
                        merchant_id = message.MerchantId,
                        customer_mobile = message.CustomerMobile,
                        bill_amount = message.BillAmount
                    });

                _restClient.ThrowOnAnyError = true; // add only if you want to know any internal error of rest client

                var response = await _restClient.ExecuteAsync(request, Method.POST, new CancellationTokenSource().Token);

                return response.Content;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}