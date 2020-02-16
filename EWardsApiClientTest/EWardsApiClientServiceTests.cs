using EWardsApiClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Threading;
using System.Threading.Tasks;

namespace EWardsApiClientTest
{

    [TestClass]
    public class EWardsApiClientServiceTests
    {

        [TestMethod]
        public async Task PosCustomerCheckReturnsExpectedData()
        {
            //Arrange
            var content = "{\"message\":\"Forbidden\"}";
            var client = MockRestClient(content);
            var BaseUrl = "https://someuri.com/";
            var CustomerCheckUrl = "test/test";
            var ApiKey = "ApiKey";
            var expectedMessage = "Forbidden";
            dynamic message = new JObject();
            message.CustomerKey = "Foo";
            message.MerchantId = "bar";
            message.CustomerMobile = "123456789";
            message.BillAmount = "100"; 

            //Act
            IEwardsApiClientService ewardsApiClient = new EwardsApiClientService(BaseUrl, CustomerCheckUrl, ApiKey, client);
            var response = await ewardsApiClient.CheckCustomer(message);
            JObject output = JObject.Parse(response);

            //Assert
            Assert.IsTrue(expectedMessage == (string)output["message"]);
        }

        private static IRestClient MockRestClient(dynamic content)
        {
            var response = new Mock<IRestResponse<dynamic>>();
            response.Setup(x => x.Content).Returns(content);
            var mockIRestClient = new Mock<IRestClient>();

            mockIRestClient
              .Setup(x => x.ExecuteAsync(It.IsAny<RestRequest>(), It.IsAny<Method>(), It.IsAny<CancellationToken>()))
              .ReturnsAsync(response.Object);

            return mockIRestClient.Object;
        }

    }
}
