using System.Threading.Tasks;

namespace EWardsApiClient
{
    public interface IEwardsApiClientService
    {
        Task<dynamic> CheckCustomer(dynamic message);
    }
}